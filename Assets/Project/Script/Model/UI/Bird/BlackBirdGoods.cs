using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class BlackBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_GoodsType = EnumGoodsType.BlackBird;
        }       
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new BlackBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add("BlackBird", config);
        }
    }
}
