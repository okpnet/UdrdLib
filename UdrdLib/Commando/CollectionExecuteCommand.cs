using LinqHelper;
using System.Collections;
using System.ComponentModel;

namespace UdrdLib.Commando
{
    /// <summary>
    /// コレクションのコマンド
    /// </summary>
    public class CollectionExecuteCommand : ExecuteCommand, ICollectionExecuteCommand,IExecuteCommand
    {
        /// <summary>
        /// どんな操作がされたか
        /// </summary>
        public OperateType Operation { get; }
        /// <summary>
        /// セットされるまえの値
        /// </summary>
        public object? Value { get; }
        public CollectionExecuteCommand(string propertyPath, object? value, OperateType operaiton) : base(propertyPath)
        {
            Operation = operaiton;
            Value = value;
        }

        public override bool ToPrev<T>(T item) => ToExcecute(item, false);
        public override bool ToNext<T>(T item) => ToExcecute(item, true);

        protected bool ToExcecute<T>(T item, bool foword) where T : class, INotifyPropertyChanged
        {
            try
            {
                if (item.GetPropertyValueFromPath(PropertyPath) is not ICollection collection || Value is null)
                {
                    return false;
                }

                var remove = collection.GetType().GetMethod(nameof(ICollection<object>.Remove));
                var add = collection.GetType().GetMethod(nameof(ICollection<object>.Add));

                if (remove is null || add is null)
                {
                    return false;
                }

                switch (Operation)
                {
                    case OperateType.CollectionAdd:
                        if (foword)
                        {
                            add.Invoke(collection, new object[] { Value });
                        }
                        else
                        {
                            remove.Invoke(collection, new object[] { Value });
                        }
                        break;
                    case OperateType.CollectionRemove:
                        if (foword)
                        {
                            remove.Invoke(collection, new object[] { Value });
                        }
                        else
                        {
                            add.Invoke(collection, new object[] { Value });
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }
            return false;
        }
    }
}
