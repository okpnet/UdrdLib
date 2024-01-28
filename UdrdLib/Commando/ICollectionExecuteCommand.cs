namespace UdrdLib.Commando
{
    public interface ICollectionExecuteCommand:IExecuteCommand
    {
        /// <summary>
        /// どんな操作がされたか
        /// </summary>
        OperateType Operation { get; }
        /// <summary>
        /// セットされるまえの値
        /// </summary>
        object? Value { get; }
    }
}
