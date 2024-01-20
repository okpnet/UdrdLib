using LinqHelper;
using System.ComponentModel;

namespace UdrdLib
{
    /// <summary>
    /// コマンドとアイテム参照の橋渡し
    /// </summary>
    internal class CommandBridge
    {
        /// <summary>
        /// 監視対象インスタンス
        /// </summary>
        public INotifyPropertyChanged Item { get; set; }
        /// <summary>
        /// プロパティの値セットコマンド
        /// </summary>
        public IExecuteCommand Command { get; set; }

        public CommandBridge(INotifyPropertyChanged item,ExecuteCommand command) 
        {
            Item = item;
            Command = command;
        }
        /// <summary>
        /// 保持インスタンスと保持しているコマンドのプロパティセットをを実行
        /// </summary>
        /// <returns></returns>
        public bool ToExecute() => Command.ToExcecute(Item);
    }
}
