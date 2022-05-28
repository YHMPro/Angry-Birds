using Farme;
using UnityEngine;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 装备绑定接口
    /// </summary>
    public interface IEquipBinding:IInterfaceBase
    {
        void Binding(Transform bindingTarget);
    }
    /// <summary>
    /// 削弱接口
    /// </summary>
    public interface IWeaken:IInterfaceBase
    {
        Vector2 Velocity { get; set; }
    }
    public interface IGoods:IInterfaceBase
    {
        void Refresh();
    }
    /// <summary>
    /// 时间
    /// </summary>
    public interface ITime :IInterfaceBase
    {
        /// <summary>
        /// 时间继续
        /// </summary>
        void TimeStop();
        /// <summary>
        /// 时间启动
        /// </summary>
        void TimeContinue();
    }
    /// <summary>
    /// 死亡接口
    /// </summary>
    public interface IDied
    {
        GameObject go { get; }
        string Name { get; }
        /// <summary>
        /// 死亡
        /// </summary>
        void Died(bool isDestroy=false);
    }
    /// <summary>
    /// 死亡音效接口(用于障碍物、猪、小鸟等)
    /// </summary>
    public interface IDiedAudio
    {
        /// <summary>
        /// 销毁音效
        /// </summary>
        void DiedAudio();
    }
    /// <summary>
    /// 基础此接口才具备Boom特效
    /// </summary>
    public interface IBoom
    {
        /// <summary>
        /// 打开Boom
        /// </summary>
        void OpenBoom();
    }
    /// <summary>
    /// 基础此接口才具备分数
    /// </summary>
    public interface IScore
    {
        /// <summary>
        /// 打开分数
        /// </summary>
        void OpenScore();
    }
}
