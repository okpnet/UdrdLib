using LinqHelper;
using System.ComponentModel;

namespace UdrdLib.Commando
{
    /// <summary>
    /// プロパティの値セットコマンド
    /// </summary>
    public class PropertyChangeCommand : ExecuteCommand,IPropertyChangeCommand, IExecuteCommand
    {
        /// <summary>
        /// セットされるまえの値
        /// </summary>
        public object? BeforeValue { get; }
        /// <summary>
        /// セットされた後の値
        /// </summary>
        public object? AfterValue { get; }
        public PropertyChangeCommand(string propertyPath, object? bforeValue ,object? afterValue):base(propertyPath)
        {
            BeforeValue = bforeValue;
            AfterValue = afterValue;
        }
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool ToPrev<T>(T item) where T : class
        {
            try
            {
                item.SetPropertyValueFromPath(PropertyPath, BeforeValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
            return true;
        }
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool ToNext<T>(T item) where T : class
        {
            try
            {
                item.SetPropertyValueFromPath(PropertyPath, AfterValue);
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
