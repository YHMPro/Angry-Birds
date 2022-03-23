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
    public class Goods : BaseMono,IGoods
    {
        [SerializeField]
        /// <summary>
        /// 小鸟类型
        /// </summary>
        protected EnumBirdType m_BirdType=EnumBirdType.None;
        protected UIBtn m_GoodsBtn;
        protected Text m_Price;
        protected BirdConfigInfo m_ConfigInfo;
        protected Image m_GoodsImgFilled;
        protected override void Awake()
        {
            base.Awake();
            InterfaceManager.AddInterface<IGoods>(typeof(IGoods).Name,this);
            m_ConfigInfo = BirdConfigInfo.GetBirdConfigInfo(m_BirdType);
            RegisterComponentsTypes<Text>();
            RegisterComponentsTypes<Image>();
            m_GoodsBtn = GetComponent<UIBtn>();
            m_GoodsImgFilled = GetComponent<Image>("GoodsImgFilled");
            m_Price =GetComponent<Text>("PriceText");
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            Refresh();
        }
        protected override void Start()
        {
            base.Start();
            m_GoodsBtn.OnPointerClickEvent.AddListener(OnGoodsClick);
            m_Price.text = m_ConfigInfo.Coin.ToString();
        }

        protected override void OnDestroy()
        {
            InterfaceManager.RemoveInterface<IGoods>(typeof(IGoods).Name, this);
            base.OnDestroy();
        }
        /// <summary>
        /// 货物点击事件监听
        /// </summary>
        private void OnGoodsClick()
        {    
            if(m_ConfigInfo.Coin>GameLogic.CoinNum)
            {
                return;
            }
            if(!GameLogic.IsBuy|| (GameLogic.NowComeBird!=null))
            {
                return;
            }            
            if (!GoReusePool.Take(m_BirdType.ToString(), out GameObject goods))
            {         
                if (!GoLoad.Take(m_ConfigInfo.GetBirdPrefabPath(), out goods))
                {
                    return;
                }
            }
            MesgManager.MesgTirgger(ProjectEvents.CoinUpdateEvent, m_ConfigInfo);//更新硬币
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

        public void Refresh()
        {
            m_GoodsImgFilled.fillAmount = (m_ConfigInfo.Coin > GameLogic.CoinNum) ? 0 : 1;
        }
    }
}
