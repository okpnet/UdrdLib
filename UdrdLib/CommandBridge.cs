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
        public SetCommand Cmd { get; set; }

        public CommandBridge(INotifyPropertyChanged item,SetCommand command) 
        {
            Item = item;
            Cmd = command;
        }
        /// <summary>
        /// 保持インスタンスと保持しているコマンドのプロパティセットをを実行
        /// </summary>
        /// <returns></returns>
        public bool ToExecute()
        {
            try
            {
                Item.SetPropertyValueFromPath(Cmd.PropertyPath,Cmd.Value);
            }catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
