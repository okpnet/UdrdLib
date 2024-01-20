using System.ComponentModel;

namespace UdrdLib
{
    /// <summary>
    /// インスタンス追加最初のコマンド
    /// </summary>
    public class InitExecuteCommand : ExecuteCommand, IExecuteCommand
    {
        public InitExecuteCommand(string propertyPath, object? value, OperateType operaiton) : base(propertyPath, value, operaiton)
        {
        }
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool ToPrev<T>(T item)
        {
            if (Value is not IEnumerable<IExecuteCommand> commands)
            {
                return false;
            }
            foreach (var command in commands)
            {
                command.ToPrev(item);
            }
            return true;
        }
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool ToNext<T>(T item)
        {
            if (Value is not IEnumerable<IExecuteCommand> commands)
            {
                return false;
            }
            foreach (var command in commands)
            {
                command.ToNext(item);
            }
            return true;
        }
    }
}
