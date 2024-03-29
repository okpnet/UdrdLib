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
        /// 戻るのとき
        /// </summary>
        readonly Stack<CommandBridge> redoStack = new();
        /// <summary>
        /// プロパティセットコマンドをスタックする
        /// </summary>
        readonly Stack<CommandBridge> executeStack  = new Stack<CommandBridge>();

        bool commandPrevOrNext = false;

        /// <summary>
        /// 監視対象のインスタンスの追加｡newItem引数がTrueのとき､新規の変更なしとして追加する｡Falseのときは変更なしとして追加する｡
        /// </summary>`
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="newItem"></param>
        public void AddObserveItem<T>(T item, bool newItem = true) where T : class, INotifyPropertyChanged
        {
            if (facades.Any(t => ReferenceEquals(t.Item, item))) return;
            facades.Add(
                    new CommandFacade(
                        item,
                        newItem ? StateType.AddUnchange : StateType.ModifyUnchange,
                        Observer.Create<CommandBridge>(t => AddStack(t)),
                        Observer.Create<(INotifyPropertyChanged,bool)>(t=>AddObserveItem(t.Item1,t.Item2)))
                );
        }
        /// <summary>
        /// 1つ前の状態に戻す
        /// </summary>
        /// <returns></returns>
        public bool ToPrev()
        {
            
            commandPrevOrNext= true;
            try
            {
                if(executeStack.TryPop(out var result))
                {
                    redoStack.Push(result);
                    result.ToPrev();
                    return true;
                }
                return false;
            }
            finally
            {
                commandPrevOrNext= false;
            }
        }
        /// <summary>
        /// 戻した状態を1つ進める
        /// </summary>
        /// <returns></returns>
        public bool ToNext()
        {
            commandPrevOrNext = true;
            try
            {
                if (redoStack.TryPop(out var result))
                {
                    executeStack.Push(result);
                    result.ToNext();
                    return true;
                }
                return false;
            }
            finally
            {
                commandPrevOrNext = false;
            }
        }
        /// <summary>
        /// 変更をSTACKに追加
        /// </summary>
        /// <param name="commandAdapter"></param>
        void AddStack(CommandBridge commandAdapter)
        {
            if (commandPrevOrNext) return;
            executeStack.Push(commandAdapter);
            redoStack.Clear();
        }
    }
}
