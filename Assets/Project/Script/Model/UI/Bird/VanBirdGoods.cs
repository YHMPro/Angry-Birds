using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class VanBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_GoodsType = EnumGoodsType.VanBird;
        }
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new VanBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add("VanBird", config);
        }
    }
}
