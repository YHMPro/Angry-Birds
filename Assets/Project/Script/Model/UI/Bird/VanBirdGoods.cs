using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.Extend;
namespace Bird_VS_Boar
{
    public class VanBirdGoods : Goods
    {
        protected override void Awake()
        {
            base.Awake();
            m_BirdType = EnumBirdType.VanBird;
        }
        protected override void BirdConfigInfoSet(out BirdConfigInfo config)
        {
            config = new VanBirdConfigInfo();
            config.InitConfigInfo();
            BirdConfigInfo.BirdConfigInfoDic.Add(EnumBirdType.VanBird, config);
        }
        protected override void AddBirdCom(GameObject bird)
        {
            GameLogic.NowComeBird = bird.InspectComponent<VanBird>();
        }
    }
}
