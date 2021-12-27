using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class GreenBird : SkillBird
    {
        protected override void Awake()
        {
            
            base.Awake();
        }

      
        protected override void OnSkillUpdate()
        {           
            base.OnSkillUpdate();
            PlaySkillAudio();
            m_Rig2D.velocity = new Vector2(-m_Rig2D.velocity.x, m_Rig2D.velocity.y);
        }      
    }
}
