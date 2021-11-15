using UnityEngine;
using UnityEngine.Audio;
namespace Farme
{
    /// <summary>
    /// 接口基类
    /// </summary>
    public interface IInterfaceBase { }

    #region 数据接口
    /// <summary>
    /// 数据接口
    /// </summary>
    public interface IData:IInterfaceBase
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        void DataInit();
    }
    #endregion

    #region 特殊接口
    /// <summary>
    /// 位置同步接口
    /// </summary>
    public interface ILocationSync: IInterfaceBase
    {
        /// <summary>
        /// 位置同步
        /// </summary>
        /// <param name="targetSync">同步目标</param>
        void LocationSync(Transform targetSync);
    }
    #endregion

    #region 指针事件接口  ( 独立约束 -> 具体过程 )
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IEvent : IInterfaceBase
    {

    }
    /// <summary>
    /// 指针事件接口
    /// </summary>
    public interface IPointerEvent : IEvent
    {

    }
    /// <summary>
    /// 指针事件屏蔽接口
    /// </summary>
    public interface IPointerEventShield : IPointerEvent
    {
        /// <summary>
        /// 指针事件屏蔽
        /// </summary>
        /// <typeparam name="T">依据类型</typeparam>
        /// <param name="gist">依据</param>
        void PointerEventShield<T>(T gist);
    }
    /// <summary>
    /// 指针事件重置接口
    /// </summary>
    public interface IPointerEventReset : IPointerEvent
    {
        /// <summary>
        /// 指针事件重置
        /// </summary>
        /// <typeparam name="T">依据类型</typeparam>
        /// <param name="gist">依据</param>
        void PointerEventReset<T>(T gist);
    }
    #endregion

    #region UGUI扩展接口
    #region CheckBox指针事件接口  ( 集中约束  ->  具体 )
    /// <summary>
    /// Box指针事件接口
    /// </summary>
    public interface ICheckBoxPointerEvent: IPointerEventShield, IPointerEventReset
    {

    }
    #endregion
    #endregion


}
