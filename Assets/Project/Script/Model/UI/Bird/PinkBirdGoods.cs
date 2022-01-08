﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class PinkBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_BirdType = EnumBirdType.PinkBird;
        }
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new PinkBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add(EnumBirdType.PinkBird, config);
        }
    }
}
