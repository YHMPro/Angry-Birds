using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.UI;
using Farme.Extend;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 货物类型
    /// </summary>
    public enum EnumGoodsType
    {
        None,
        RedBird,
        BlackBird,
        BlueBird,
        GreenBird,
        PinkBird,
        VanBird,
        WhiteBird,
        YellowBird,
    }
    /// <summary>
    /// 货物
    /// </summary>
    public class Goods : BaseMono
    {
        [SerializeField]
        /// <summary>
        /// 小鸟类型
        /// </summary>
        protected EnumBirdType m_BirdType=EnumBirdType.None;
        protected UIBtn m_GoodsBtn;
        protected override void Awake()
        {
            base.Awake();
            m_GoodsBtn = GetComponent<UIBtn>();
        }

        protected override void Start()
        {
            base.Start();
            m_GoodsBtn.OnPointerClickEvent.AddListener(OnGoodsClick);
        }
     
        /// <summary>
        /// 货物点击事件监听
        /// </summary>
        private void OnGoodsClick()
        {    
            if(!GameLogic.IsBuy)
            {
                return;
            }            
            if (!GoReusePool.Take(m_BirdType.ToString(), out GameObject goods))
            {
                if (!BirdConfigInfo.BirdConfigInfoDic.TryGetValue(m_BirdType, out var config))
                {
                    BirdConfigInfoSet(out config);
                }               
                if (!GoLoad.Take(config.GetBirdPrefabPath(), out goods))
                {
                    return;
                }
            }
            MesgManager.MesgTirgger(ProjectEvents.CoinUpdateEvent, m_BirdType);//更新硬币
            AddBirdCom(goods);
            if (MonoSingletonFactory<SlingShot>.SingletonExist)
            {
                MonoSingletonFactory<SlingShot>.GetSingleton().BindBird();
            }
        }
        /// <summary>
        /// 添加小鸟组件
        /// </summary>
        /// <param name="bird"></param>
        protected virtual void AddBirdCom(GameObject bird)
        {
            
        }
        /// <summary>
        /// 小鸟配置信息设置
        /// </summary>
        /// <param name="config"></param>
        protected virtual void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = null;
        }
    }
}
