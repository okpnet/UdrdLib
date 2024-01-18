using System.Reflection;

namespace UdrdLib
{
    /// <summary>
    /// プロパティの値セットコマンド
    /// </summary>
    public class SetCommand
    {
        /// <summary>
        /// セットしたプロパティ
        /// </summary>
        public PropertyInfo Property { get;  }
        /// <summary>
        /// セットされた値
        /// </summary>
        public object? Value { get; }

        public SetCommand(PropertyInfo propertyInfo, object? value)
        {
            if (propertyInfo.SetMethod is null) throw new NullReferenceException($"{propertyInfo.Name} not exists setter");
            Property = propertyInfo;
            Value = value;
        }
    }
}
