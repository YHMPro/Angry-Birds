using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class GreenBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_GoodsType = EnumGoodsType.GreenBird;
        }
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new GreenBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add("GreenBird", config);
        }
    }
}
