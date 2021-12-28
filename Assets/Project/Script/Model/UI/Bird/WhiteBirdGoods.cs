using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class WhiteBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_GoodsType = EnumGoodsType.WhiteBird;
        }
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new WhiteBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add("WhiteBird", config);
        }
    }
}
