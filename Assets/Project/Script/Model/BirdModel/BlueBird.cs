using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Extend;
namespace Bird_VS_Boar
{
    public class BlueBird : SkillBird
    {
        protected override void Awake()
        {
            m_BirdType = EnumBirdType.BlueBird;
            base.Awake();
        }
        
        protected override void OnSkillUpdate()
        {
            PlaySkillAudio();
            ProductLittleBlueBird();
            Died();//小鸟死亡
        }

        protected void ProductLittleBlueBird()
        {           
            for (int i = 0; i < 3; i++)
            {        
                
                if (!GoReusePool.Take(EnumBirdType.LittleBlueBird.ToString(),out GameObject go))
                {                
                    if (!GoLoad.Take(m_ConfigInfo.GetBirdPrefabPath(), out go))
                    {
                        return;
                    }
                }
                LittleBlueBird blueBird = go.InspectComponent<LittleBlueBird>();                          
                go.transform.position = transform.position;              
                Vector2 velocity = m_Rig2D.velocity;
                float angle = Vector2.SignedAngle(Vector2.right, velocity.normalized);
                angle += (i == 0) ? 10 : ((i == 1) ? 0 : -10);
                velocity = new Vector2(Mathf.Cos(angle / 180.0f * Mathf.PI), Mathf.Sin(angle / 180.0f * Mathf.PI));
                blueBird.Velocity = velocity * m_Rig2D.velocity.magnitude;
                blueBird.AddOnBirdFlyUpdate_Common();
                blueBird.ActiveTrailRenderer(true);                
            }
           
        }     
    }
}
