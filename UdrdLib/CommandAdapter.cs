using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdrdLib
{

    public class CommandAdapter
    {
        /// <summary>
        /// 監視対象インスタンス
        /// </summary>
        public INotifyPropertyChanged Item { get; set; }
        /// <summary>
        /// プロパティの値セットコマンド
        /// </summary>
        public SetCommand Cmd { get; set; }

        public CommandAdapter() 
        {
        }
        /// <summary>
        /// 保持インスタンスと保持しているコマンドのプロパティセットをを実行
        /// </summary>
        /// <returns></returns>
        public bool ToExecute()
        {
            try
            {
                Cmd.Property.SetValue(Item, Cmd.Value);
            }catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
