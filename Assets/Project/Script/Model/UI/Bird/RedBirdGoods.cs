using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class RedBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_GoodsType = EnumGoodsType.RedBird;
        }     
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new RedBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add("RedBird", config);                  
        }
    }
}
