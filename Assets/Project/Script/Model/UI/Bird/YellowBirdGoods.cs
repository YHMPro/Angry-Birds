using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.Extend;
namespace Bird_VS_Boar
{
    public class YellowBirdGoods : Goods
    {
        protected override void Awake()
        {
            m_BirdType = EnumBirdType.YellowBird;
            base.Awake();          
        }
      
        protected override void AddBirdCom(GameObject bird)
        {
            GameLogic.NowComeBird = bird.InspectComponent<YellowBird>();
        }

    }
}
