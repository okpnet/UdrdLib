using LinqHelper;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.WebSockets;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;

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
        /// インスタンス
        /// </summary>
        public INotifyPropertyChanged Item { get; set; }
        /// <summary>
        /// インスタンスの状態
        /// </summary>
        public StateType State { get; set; }
        /// <summary>
        /// アダプタを登録するイベント
        /// </summary>
        internal IObserver<CommandBridge> AddAdapter { get; init; }

        internal IObserver<(INotifyPropertyChanged,bool)> AddFacade { get; init; }

        public CommandFacade(INotifyPropertyChanged item, StateType state)
        {
            Item = item;
            State = state;
            Init();
        }
        /// <summary>
        /// ファサードの初期化
        /// </summary>
        protected void Init()
        {

            var arrayProperties = Item.GetType().
                GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
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
                        Subscribe(t => CollectionChanged(t.Sender, property.Name, t.EventArgs);
                    );
            }

            disposables.Add(
                Observable.FromEventPattern(Item, nameof(Item.PropertyChanged)).Subscribe(t =>
                {//コマンドを追加
                    State = State == StateType.AddUnchange ? StateType.Add : StateType.Modify;
                    if (t.Sender is INotifyPropertyChanged sender && t.EventArgs is PropertyChangedEventArgs e)
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
        protected void PropertyChanged(INotifyPropertyChanged sender,PropertyChangedEventArgs eventArgs)
        {
            if(
                eventArgs.PropertyName is string path && 
                Item.GetType().GetPropertyFromPath(path) is PropertyInfo)
            {
                var value=Item.GetPropertyValueFromPath(path);
                AddAdapter.OnNext(new CommandBridge(Item, new ExecuteCommand(path, value,OperateType.Set)));
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
                    if (e.NewItems?.Count > 0 && e.NewItems[0] is object value)
                    {
                        if(value is INotifyPropertyChanged item)
                        {
                            AddFacade.OnNext((item, true));       
                        }
                        AddAdapter.OnNext(new CommandBridge(Item, new ExecuteCommand(propertyPath,value , OperateType.CollectionAdd)));
                    }
                    break;
            }
        }
    }
}
