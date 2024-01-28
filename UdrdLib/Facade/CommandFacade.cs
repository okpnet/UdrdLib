using LinqHelper;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using UdrdLib.Commando;

namespace UdrdLib
{
    /// <summary>
    /// コマンドとインスタンスを登録するファサード
    /// </summary>
    public class CommandFacade:IDisposable
    {
        /// <summary>
        /// イベント
        /// </summary>
        CompositeDisposable disposables = new();
        /// <summary>
        /// アダプタを登録するイベント
        /// </summary>
        IObserver<CommandBridge> addCommandBridgeObserver;
        /// <summary>
        /// コレクションの子の履歴を追加する
        /// </summary>
        IObserver<(INotifyPropertyChanged, bool)> addFacadeObserver;
        /// <summary>
        /// インスタンス
        /// </summary>
        public INotifyPropertyChanged Item { get; set; }
        /// <summary>
        /// インスタンスの状態
        /// </summary>
        public StateType State { get; set; }


        public CommandFacade(INotifyPropertyChanged item, StateType state, IObserver<CommandBridge> addCommand, IObserver<(INotifyPropertyChanged, bool)> addFacade)
        {
            Item = item;
            State = state;
            addCommandBridgeObserver = addCommand;
            addFacadeObserver = addFacade;
            Init();
        }
        /// <summary>
        /// ファサードの初期化
        /// </summary>
        protected void Init()
        {
            var properties = Item.GetType().
                GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var arrayProperties = properties
                .Where(t=>t.PropertyType.IsGenericType && t.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>));


            foreach(var property in arrayProperties ) 
            {
                if (property.GetValue(Item, null) is not INotifyCollectionChanged collection)
                {

                    continue;
                }
                disposables.Add(
                        Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                            h => collection.CollectionChanged += h, h => collection.CollectionChanged -= h).
                        Subscribe(t =>
                        {
                            CollectionChanged(t.Sender, property.Name, t.EventArgs);
                        })
                    );
            }

            disposables.Add(
                Observable.FromEventPattern(Item, nameof(Item.PropertyChanged)).Subscribe(t =>
                {//変更後のコマンドを追加
                    if (t.Sender is INotifyPropertyChanged sender && t.EventArgs is PropertyChangedEventHasArgs e)
                    {
                        PropertyChanged(sender, e);
                    }
                })
            );
        }
        public void Dispose()
        {
            disposables.Clear();
            disposables.Dispose();
        }
        /// <summary>
        /// 追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected void PropertyChanged(INotifyPropertyChanged sender, PropertyChangedEventHasArgs eventArgs)
        {
            if(
                eventArgs.PropertyName is string path && 
                Item.GetType().GetPropertyFromPath(path) is PropertyInfo)
            {
                State = State == StateType.AddUnchange ? StateType.Add : StateType.Modify;
                addCommandBridgeObserver.OnNext(new CommandBridge(Item, new PropertyChangeCommand(path, eventArgs.BeforeValue,eventArgs.AfterValue)));
            }
        }
        /// <summary>
        /// コレクションの変更
        /// </summary>
        /// <param name="e"></param>
        protected void CollectionChanged(object? sender,string propertyPath,NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems is null || e.NewItems.Count == 0) break;
                    State = State == StateType.AddUnchange ? StateType.Add : StateType.Modify;
                    foreach (var item in e.NewItems)
                    {
                        if(item is INotifyPropertyChanged notifyChange)
                        {
                            addFacadeObserver.OnNext((notifyChange, true));       
                        }
                        addCommandBridgeObserver.OnNext(new CommandBridge(Item, new CollectionExecuteCommand(propertyPath,item , OperateType.CollectionAdd)));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems is null || e.OldItems.Count==0) break;
                    State = State == StateType.AddUnchange ? StateType.Add : StateType.Modify;
                    foreach (var item in e.OldItems)
                    {
                        addCommandBridgeObserver.OnNext(new CommandBridge(Item, new CollectionExecuteCommand(propertyPath, item, OperateType.CollectionRemove)));
                    }
                    break;
            }
        }
    }
}
