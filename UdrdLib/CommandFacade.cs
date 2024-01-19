using LinqHelper;
using System.ComponentModel;
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

        public CommandFacade( INotifyPropertyChanged item, StateType state)
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
                AddAdapter.OnNext(new CommandBridge(Item, new SetCommand(path, value)));
            }
        }
    }
}
