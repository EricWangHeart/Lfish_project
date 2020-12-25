using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEventCenter
{
    public class EventCenter
    {
        private static Dictionary<PEventCenter.PEventType, Delegate> m_EventTable = new Dictionary<PEventCenter.PEventType, Delegate>();

        private static void OnListenerAdding(PEventCenter.PEventType PEventType, Delegate callBack)
        {
            if (!m_EventTable.ContainsKey(PEventType))
            {
                m_EventTable.Add(PEventType, null);
            }
            Delegate d = m_EventTable[PEventType];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", PEventType, d.GetType(), callBack.GetType()));
            }
        }
        private static void OnListenerRemoving(PEventCenter.PEventType PEventType, Delegate callBack)
        {
            if (m_EventTable.ContainsKey(PEventType))
            {
                Delegate d = m_EventTable[PEventType];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", PEventType));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", PEventType, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", PEventType));
            }
        }
        private static void OnListenerRemoved(PEventCenter.PEventType PEventType)
        {
            if (m_EventTable[PEventType] == null)
            {
                m_EventTable.Remove(PEventType);
            }
        }
        //no parameters
        public static void AddListener(PEventCenter.PEventType PEventType, CallBack callBack)
        {
            OnListenerAdding(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack) m_EventTable[PEventType] + callBack;
        }
        //Single parameters
        public static void AddListener<T>(PEventCenter.PEventType PEventType, CallBack<T> callBack)
        {
            OnListenerAdding(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T>) m_EventTable[PEventType] + callBack;
        }
        //two parameters
        public static void AddListener<T, X>(PEventCenter.PEventType PEventType, CallBack<T, X> callBack)
        {
            OnListenerAdding(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T, X>) m_EventTable[PEventType] + callBack;
        }
        //three parameters
        public static void AddListener<T, X, Y>(PEventCenter.PEventType PEventType, CallBack<T, X, Y> callBack)
        {
            OnListenerAdding(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T, X, Y>) m_EventTable[PEventType] + callBack;
        }
        //four parameters
        public static void AddListener<T, X, Y, Z>(PEventCenter.PEventType PEventType, CallBack<T, X, Y, Z> callBack)
        {
            OnListenerAdding(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T, X, Y, Z>) m_EventTable[PEventType] + callBack;
        }
        //five parameters
        public static void AddListener<T, X, Y, Z, W>(PEventCenter.PEventType PEventType, CallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerAdding(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T, X, Y, Z, W>) m_EventTable[PEventType] + callBack;
        }

        //no parameters
        public static void RemoveListener(PEventCenter.PEventType PEventType, CallBack callBack)
        {
            OnListenerRemoving(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack) m_EventTable[PEventType] - callBack;
            OnListenerRemoved(PEventType);
        }
        //single parameters
        public static void RemoveListener<T>(PEventCenter.PEventType PEventType, CallBack<T> callBack)
        {
            OnListenerRemoving(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T>) m_EventTable[PEventType] - callBack;
            OnListenerRemoved(PEventType);
        }
        //two parameters
        public static void RemoveListener<T, X>(PEventCenter.PEventType PEventType, CallBack<T, X> callBack)
        {
            OnListenerRemoving(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T, X>) m_EventTable[PEventType] - callBack;
            OnListenerRemoved(PEventType);
        }
        //three parameters
        public static void RemoveListener<T, X, Y>(PEventCenter.PEventType PEventType, CallBack<T, X, Y> callBack)
        {
            OnListenerRemoving(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T, X, Y>) m_EventTable[PEventType] - callBack;
            OnListenerRemoved(PEventType);
        }
        //four parameters
        public static void RemoveListener<T, X, Y, Z>(PEventCenter.PEventType PEventType, CallBack<T, X, Y, Z> callBack)
        {
            OnListenerRemoving(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T, X, Y, Z>) m_EventTable[PEventType] - callBack;
            OnListenerRemoved(PEventType);
        }
        //five parameters
        public static void RemoveListener<T, X, Y, Z, W>(PEventCenter.PEventType PEventType, CallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerRemoving(PEventType, callBack);
            m_EventTable[PEventType] = (CallBack<T, X, Y, Z, W>) m_EventTable[PEventType] - callBack;
            OnListenerRemoved(PEventType);
        }

        //no parameters
        public static bool ExistListener(PEventCenter.PEventType PEventType, CallBack callBack)
        {
            Delegate d;
            if (!m_EventTable.TryGetValue(PEventType, out d))
                return false;
            List<Delegate> ds = new List<Delegate>(d.GetInvocationList());
            return ds.IndexOf(callBack as Delegate) != -1;
        }
        //single parameters
        public static bool ExistListener<T>(PEventCenter.PEventType PEventType, CallBack<T> callBack)
        {
            Delegate d;
            if (!m_EventTable.TryGetValue(PEventType, out d))
                return false;
            List<Delegate> ds = new List<Delegate>(d.GetInvocationList());
            return ds.IndexOf(callBack as Delegate) != -1;
        }
        //two parameters
        public static bool ExistListener<T, X>(PEventCenter.PEventType PEventType, CallBack<T, X> callBack)
        {
            Delegate d;
            if (!m_EventTable.TryGetValue(PEventType, out d))
                return false;
            List<Delegate> ds = new List<Delegate>(d.GetInvocationList());
            return ds.IndexOf(callBack as Delegate) != -1;
        }
        //three parameters
        public static bool ExistListener<T, X, Y>(PEventCenter.PEventType PEventType, CallBack<T, X, Y> callBack)
        {
            Delegate d;
            if (!m_EventTable.TryGetValue(PEventType, out d))
                return false;
            List<Delegate> ds = new List<Delegate>(d.GetInvocationList());
            return ds.IndexOf(callBack as Delegate) != -1;
        }
        //four parameters
        public static bool ExistListener<T, X, Y, Z>(PEventCenter.PEventType PEventType, CallBack<T, X, Y, Z> callBack)
        {
            Delegate d;
            if (!m_EventTable.TryGetValue(PEventType, out d))
                return false;
            List<Delegate> ds = new List<Delegate>(d.GetInvocationList());
            return ds.IndexOf(callBack as Delegate) != -1;
        }
        //five parameters
        public static bool ExistListener<T, X, Y, Z, W>(PEventCenter.PEventType PEventType, CallBack<T, X, Y, Z, W> callBack)
        {
            Delegate d;
            if (!m_EventTable.TryGetValue(PEventType, out d))
                return false;
            List<Delegate> ds = new List<Delegate>(d.GetInvocationList());
            return ds.IndexOf(callBack as Delegate) != -1;
        }

        //no parameters
        public static void Broadcast(PEventCenter.PEventType PEventType)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(PEventType, out d))
            {
                CallBack callBack = d as CallBack;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", PEventType));
                }
            }
        }
        //single parameters
        public static void Broadcast<T>(PEventCenter.PEventType PEventType, T arg)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(PEventType, out d))
            {
                CallBack<T> callBack = d as CallBack<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", PEventType));
                }
            }
        }
        //two parameters
        public static void Broadcast<T, X>(PEventCenter.PEventType PEventType, T arg1, X arg2)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(PEventType, out d))
            {
                CallBack<T, X> callBack = d as CallBack<T, X>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", PEventType));
                }
            }
        }
        //three parameters
        public static void Broadcast<T, X, Y>(PEventCenter.PEventType PEventType, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(PEventType, out d))
            {
                CallBack<T, X, Y> callBack = d as CallBack<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", PEventType));
                }
            }
        }
        //four parameters
        public static void Broadcast<T, X, Y, Z>(PEventCenter.PEventType PEventType, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(PEventType, out d))
            {
                CallBack<T, X, Y, Z> callBack = d as CallBack<T, X, Y, Z>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", PEventType));
                }
            }
        }
        //five parameters
        public static void Broadcast<T, X, Y, Z, W>(PEventCenter.PEventType PEventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(PEventType, out d))
            {
                CallBack<T, X, Y, Z, W> callBack = d as CallBack<T, X, Y, Z, W>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", PEventType));
                }
            }
        }
    }
}