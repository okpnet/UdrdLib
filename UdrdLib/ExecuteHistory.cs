﻿using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;

namespace UdrdLib
{
    /// <summary>
    /// セット履歴
    /// </summary>
    public class ExecuteHistory
    {
        /// <summary>
        /// Itemインスタンスのリスト｡イベントハンドラ初期化後のインスタンスのステータスとを
        /// </summary>
        IList<CommandFacade> facades = new List<CommandFacade>();
        /// <summary>
        /// プロパティセットコマンドをスタックする
        /// </summary>
        public Stack<CommandAdapter> ExecuteStack { get; } = new Stack<CommandAdapter>();

        /// <summary>
        /// 監視対象のインスタンスの追加｡newItem引数がTrueのとき､新規の変更なしとして追加する｡Falseのときは変更なしとして追加する｡
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="newItem"></param>
        public void AddObserveItem<T>(T item,bool newItem=true) where T : class, INotifyPropertyChanged
        {
            if (facades.Any(t => ReferenceEquals(t.Item, item))) return;
            facades.Add(
                    new CommandFacade(Observer.Create<CommandAdapter>(t => ExecuteStack.Push(t)), item, newItem ? StateType.AddUnchange : StateType.ModifyUnchange)
                );
        }
        /// <summary>
        /// 1つ前の状態に戻す
        /// </summary>
        /// <returns></returns>
        public bool ToPrev() => ExecuteStack.Peek().ToExecute();

        public bool ToNext()
    }
}