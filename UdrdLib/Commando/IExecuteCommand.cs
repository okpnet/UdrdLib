using System.ComponentModel;

namespace UdrdLib.Commando
{
    /// <summary>
    /// 実行されたコマンドのインターフェイス
    /// </summary>
    public interface IExecuteCommand
    {
        /// <summary>
        /// セットしたプロパティのパス
        /// </summary>
        string PropertyPath { get; }

        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ToPrev<T>(T item) where T : class, INotifyPropertyChanged;
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ToNext<T>(T item) where T : class, INotifyPropertyChanged;
    }
}
