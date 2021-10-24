using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class BlueBird : Bird
    {
        protected override void Awake()
        {
            m_ConfigInfo = NotMonoSingletonFactory<BlueBirdConfigInfo>.GetSingleton();
            base.Awake();
        }

        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(SkillUpdate);//持续监听技能释放指令
        }

        protected override void SkillUpdate()
        {
            base.SkillUpdate();
            if (IsReleaseSkill)
            {
                for(int i=0;i<2;i++)
                {
                    if (GoLoad.Take("Prefabs/Bird/BirdBlue/BirdBlue", out GameObject go))
                    {                 
                        go.transform.position = transform.position;
                        BlueBird blueBird = go.GetComponent<BlueBird>();                      
                        Vector2 velocity = m_Rig2D.velocity;
                        float angle = Vector2.SignedAngle(Vector2.right, velocity.normalized);
                        angle += i % 2 == 0 ? 10 : -10;                        
                        velocity = new Vector2(Mathf.Cos(angle/ 180.0f * Mathf.PI), Mathf.Sin(angle/ 180.0f * Mathf.PI));
                        blueBird.SetBirdRig2DVelocity(velocity * m_Rig2D.velocity.magnitude);
                        MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(blueBird.BirdFlyUpdate);
                        blueBird.ActiveTrailRenderer(true);
                    }
                }
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }
    }
}
