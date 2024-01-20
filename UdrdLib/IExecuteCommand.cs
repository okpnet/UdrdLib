using System.ComponentModel;

namespace UdrdLib
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
        /// セットされた値
        /// </summary>
        object? Value { get; }
        /// <summary>
        /// どんな操作がされたか
        /// </summary>
        OperateType Operation { get; }
        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ToExcecute<T>(T item) where T : class, INotifyPropertyChanged;
    }
}
