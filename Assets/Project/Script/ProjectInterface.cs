
namespace Bird_VS_Boar
{
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
