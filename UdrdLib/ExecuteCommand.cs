using LinqHelper;
using System.ComponentModel;
using System.Reflection;

namespace UdrdLib
{
    /// <summary>
    /// プロパティの値セットコマンド
    /// </summary>
    public class ExecuteCommand: IExecuteCommand
    {
        /// <summary>
        /// セットしたプロパティのパス
        /// </summary>
        public string PropertyPath { get;  }
        /// <summary>
        /// セットされた値
        /// </summary>
        public object? Value { get; }
        /// <summary>
        /// どんな操作がされたか
        /// </summary>
        public OperateType Operation { get; }

        public ExecuteCommand(string propertyPath, object? value, OperateType operaiton)
        {
            PropertyPath = propertyPath;
            Value = value;
            Operation = operaiton;
        }
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool ToExcecute<T>(T item) where T : class,INotifyPropertyChanged
        {
            try
            {
                item.SetPropertyValueFromPath(PropertyPath, Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
