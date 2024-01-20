using LinqHelper;
using System.Collections;
using System.Reflection;

namespace UdrdLib
{
    /// <summary>
    /// コレクションのコマンド
    /// </summary>
    public class CollectionExecuteCommand:ExecuteCommand, IExecuteCommand
    {
        public CollectionExecuteCommand(string propertyPath, object? value, OperateType operaiton) : base(propertyPath, value, operaiton)
        {
        }

        public override bool ToExcecute<T>(T item)
        {
            try
            {
                if (item.GetPropertyValueFromPath(PropertyPath) is not ICollection collection)
                {
                    return false;
                }

                switch (Operation)
                {
                    case OperateType.CollectionAdd:
                        if (collection.GetType().GetMethod(nameof(ICollection<object>.Remove)) is MethodInfo removeMethod && Value is not null)
                        {
                            removeMethod.Invoke(collection, new object[] { Value });
                            return true;
                        }
                        break;
                    case OperateType.CollectionRemove:
                        if (collection.GetType().GetMethod(nameof(ICollection<object>.Add)) is MethodInfo addMethod && Value is not null)
                        {
                            addMethod.Invoke(collection, new object[] { Value });
                            return true;
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
