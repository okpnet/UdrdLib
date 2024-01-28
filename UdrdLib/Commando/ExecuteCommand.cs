using System.ComponentModel;

namespace UdrdLib.Commando
{
    public abstract class ExecuteCommand:IExecuteCommand
    {
        /// <summary>
        /// セットしたプロパティのパス
        /// </summary>
        public string PropertyPath { get; }


        public ExecuteCommand(string propertyPath)
        {
            PropertyPath = propertyPath;
        }
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract bool ToPrev<T>(T item) where T : class, INotifyPropertyChanged ;
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract bool ToNext<T>(T item) where T : class, INotifyPropertyChanged ;
    }
}
