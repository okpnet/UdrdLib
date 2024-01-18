using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;

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
        public IObserver<CommandAdapter> AddAdapter { get; set; }
        public CommandFacade(IObserver<CommandAdapter> addOvserve, INotifyPropertyChanged item, StateType state)
        {
            AddAdapter=addOvserve;
            Item = item;
            State = state;
            Init();
        }
        /// <summary>
        /// ファサードの初期化
        /// </summary>
        protected void Init()
        {
            var props=Item.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly);

            foreach(var prop in props.Where(t=>t.SetMethod is not null))
            {
                disposables.Add(
                    Observable.FromEventPattern(Item,nameof(Item.PropertyChanged)).Where(t=> ((PropertyChangedEventArgs)t.EventArgs).PropertyName==prop.Name).Subscribe(t =>
                    {//コマンドを追加
                        State = State == StateType.AddUnchange ? StateType.Add : StateType.Modify;

                        if (t.Sender is INotifyPropertyChanged sendItem)
                        {
                            var val = prop.GetValue(Item);
                            AddAdapter.OnNext(new CommandAdapter { Cmd = new SetCommand(prop, val), Item = sendItem });
                        }
                    })
                );
            }
        }
        public void Dispose()
        {
            disposables.Clear();
            disposables.Dispose();
        }
    }
}
