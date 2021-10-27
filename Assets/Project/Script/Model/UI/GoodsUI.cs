using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.UI;
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
    /// 货物UI
    /// </summary>
    public class GoodsUI : BaseMono
    {
        /// <summary>
        /// 货物类型
        /// </summary>
        public EnumGoodsType GoodsType = EnumGoodsType.None;
        
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Button>();
        }

        protected override void Start()
        {
            base.Start();

            if(GetComponent("Goods",out Button btn))
            {
                btn.onClick.AddListener(OnGoodsClick);
            }
            ///BaseConfig config = ProjectTool.GetConfig(ProjectTool.GoodsTypeToConfigType(GoodsType));



        }

        protected override void OnDestroy()
        {
            if (GetComponent("Goods", out Button btn))
            {
                btn.onClick.RemoveListener(OnGoodsClick);
            }
            base.OnDestroy();
        }
        /// <summary>
        /// 货物点击事件监听
        /// </summary>
        private void OnGoodsClick()
        {
            BaseConfig config = ProjectTool.GetConfig(ProjectTool.GoodsTypeToConfigType(GoodsType));
            config.InitResources();
            Debug.Log(config.SelfResPath);
            GameObject goods;
            if (!GoReusePool.Take(GoodsType.ToString(), out goods))
            {
                if (!GoLoad.Take(config.SelfResPath, out goods))
                {
                    return;
                }
            }
            GameLogic.NowComeBird = goods.GetComponent<Bird>();
            MonoSingletonFactory<SlingShot>.GetSingleton().BindBird();          
        }

        private void GoodsChoose()
        {

        }
    }
}
