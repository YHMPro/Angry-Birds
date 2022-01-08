using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class YellowBird : SkillBird
    {
        protected override void Awake()
        {
            m_BirdType = EnumBirdType.YellowBird;
            base.Awake();
        }

        

        protected override void OnSkillUpdate()
        {
            base.OnSkillUpdate();
            PlaySkillAudio();
            m_Rig2D.velocity += m_Rig2D.velocity.normalized * 5.0f;
        }       
    }
}
