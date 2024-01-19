using System.Reflection;

namespace UdrdLib
{
    /// <summary>
    /// プロパティの値セットコマンド
    /// </summary>
    public class SetCommand
    {
        /// <summary>
        /// セットしたプロパティのパス
        /// </summary>
        public string PropertyPath { get;  }
        /// <summary>
        /// セットされた値
        /// </summary>
        public object? Value { get; }

        public SetCommand(string propertyPath, object? value)
        {
            PropertyPath = propertyPath;
            Value = value;
        }
    }
}
