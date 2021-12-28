using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class YellowBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_GoodsType = EnumGoodsType.YellowBird;
        }
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new YellowBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add("YellowBird", config);
        }


    }
}
