using System.ComponentModel;

namespace UdrdLib
{
    /// <summary>
    /// コマンドとアイテム参照の橋渡し
    /// </summary>
    public class CommandBridge
    {
        /// <summary>
        /// 監視対象インスタンス
        /// </summary>
        public INotifyPropertyChanged Item { get; set; }
        /// <summary>
        /// プロパティの値セットコマンド
        /// </summary>
        public IExecuteCommand Command { get; set; }

        public CommandBridge(INotifyPropertyChanged item,IExecuteCommand command) 
        {
            Item = item;
            Command = command;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ToNext() => Command.ToNext(Item);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ToPrev()=> Command.ToPrev(Item);
    }
}
