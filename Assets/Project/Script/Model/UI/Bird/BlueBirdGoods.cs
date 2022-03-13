using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.Extend;
namespace Bird_VS_Boar
{
    public class BlueBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_BirdType = EnumBirdType.BlueBird;
        }
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new BlueBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add(EnumBirdType.BlueBird, config);
            BirdConfigInfo.BirdConfigInfoDic.Add(EnumBirdType.LittleBlueBird, config);
        }
        protected override void AddBirdCom(GameObject bird)
        {
            GameLogic.NowComeBird = bird.InspectComponent<BlueBird>();
        }
    }
}
