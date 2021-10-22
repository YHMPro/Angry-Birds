using UnityEngine.Events;
using System.Collections.Generic;
namespace Farme
{
    /// <summary>
    /// 消息托管
    /// </summary>
    public class MesgMgr
    {
        protected MesgMgr() { }
        #region 字段
        /// <summary>
        /// 存储所有的IMesg对象 key:消息名 value:消息
        /// </summary>
        private static Dictionary<string, IMesg> _mDic = null;
        #endregion
        #region 方法
        /// <summary>
        /// 消息监听
        /// </summary>
        /// <param name="mName">消息名</param>
        /// <param name="uA">消息</param>
        public static void MesgListen(string mName, UnityAction uA)
        {
            if(_mDic==null)
            {
                _mDic=new Dictionary<string, IMesg>();
            }
            if (_mDic.TryGetValue(mName, out var m))
            {
                (m as Mesg).UA += uA;
            }
            else
            {
                _mDic.Add(mName, new Mesg(uA));
            }
        }
        /// <summary>
        /// 消息派送
        /// </summary>
        /// <typeparam name="T">传递信息类型</typeparam>
        /// <param name="mName">消息名</param>
        /// <param name="uA">消息</param>
        public static void MesgListen<T>(string mName, UnityAction<T> uA)
        {
            if (_mDic == null)
            {
                _mDic = new Dictionary<string, IMesg>();
            }
            if (_mDic.TryGetValue(mName, out var m))
            {
                (m as Mesg<T>).UA += uA;
            }
            else
            {
                _mDic.Add(mName, new Mesg<T>(uA));
            }
        }
        /// <summary>
        /// 消息触发
        /// </summary>
        /// <param name="mName">消息名</param>
        public static void MesgTirgger(string mName)
        {
            if (_mDic == null)
            {
                _mDic = new Dictionary<string, IMesg>();
            }
            if (_mDic.TryGetValue(mName, out var mInfo))
            {
                (mInfo as Mesg).UA?.Invoke();
            }
        }
        /// <summary>
        /// 消息触发
        /// </summary>
        /// <typeparam name="T">传递信息类型</typeparam>
        /// <param name="mName">消息名称</param>
        /// <param name="mInfo">消息信息</param>
        public static void MesgTirgger<T>(string mName, T mInfo)
        {
            if (_mDic == null)
            {
                _mDic = new Dictionary<string, IMesg>();
            }
            if (_mDic.TryGetValue(mName, out var m))
            {
                (m as Mesg<T>).UA?.Invoke(mInfo);
            }
        }
        /// <summary>
        /// 消息断开监听
        /// </summary>
        /// <param name="mName">消息名</param>
        /// <param name="uA">消息</param>
        public static void MesgBreakListen(string mName, UnityAction uA)
        {
            if (_mDic.TryGetValue(mName, out var m))
            {
                (m as Mesg).UA -= uA;
            }
        }
        /// <summary>
        /// 消息断开监听
        /// </summary>
        /// <typeparam name="T">传递信息类型</typeparam>
        /// <param name="mName">消息名</param>
        /// <param name="uA">消息</param>
        public static void MesgBreakListen<T>(string mName, UnityAction<T> uA)
        {
            if (_mDic.TryGetValue(mName, out var m))
            {
                (m as Mesg<T>).UA -= uA;
            }
        }
        #endregion
        /// <summary>
        /// 创建一个接口
        /// 基于里氏转换原则来实现IMesg接口对自身派生类及自身派生类的重载类的转变
        /// </summary>
        protected interface IMesg { }
        /// <summary>
        /// Mesg类
        /// </summary>
        protected class Mesg : IMesg
        {
            public UnityAction UA;

            public Mesg(UnityAction uA)
            {
                this.UA += uA;
            }
        }
        /// <summary>
        /// Mesg泛型重载类
        /// </summary>
        /// <typeparam name="T">消息信息类型</typeparam>
        protected class Mesg<T> : IMesg
        {
            public UnityAction<T> UA;

            public Mesg(UnityAction<T> uA)
            {
                this.UA += uA;
            }
        }


    }
}
