using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.Extend;
namespace Bird_VS_Boar
{
    public class BlackBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_BirdType = EnumBirdType.BlackBird;
        }       
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new BlackBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add(EnumBirdType.BlackBird, config);
        }

        protected override void AddBirdCom(GameObject bird)
        {
            GameLogic.NowComeBird = bird.InspectComponent<BlackBird>();
        }
    }
}
