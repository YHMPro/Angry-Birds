using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Farme.UI;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 货物面板
    /// </summary>
    public class GoodsPanel : BasePanel
    {
        private Text m_EggText;
        private Image m_EggImg;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Text>();
            RegisterComponentsTypes<Image>();
            m_EggText=GetComponent<Text>("EggText");
            m_EggImg = GetComponent<Image>("EggImg");
        }

        protected override void Start()
        {
            base.Start();
        }

        public RectTransform GetEggImgTransform()
        {
            return m_EggImg.transform as RectTransform;
        }

        #region 刷新面板
        public void RefreshPanel()
        {
            m_EggText.text = GameLogic.CoinNum.ToString();
            if(InterfaceManager.GetInterfaceLi<IGoods>(typeof(IGoods).Name,out List<IGoods> iGoodsLi))
            {
                foreach(var iGoods in iGoodsLi)
                {
                    iGoods.Refresh();
                }
            }
               
           
        }
        #endregion
    }
}
