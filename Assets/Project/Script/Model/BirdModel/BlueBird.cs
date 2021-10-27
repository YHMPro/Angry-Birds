using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class BlueBird : SkillBird
    {
        protected override void Awake()
        {
            m_Config = NotMonoSingletonFactory<BlueBirdConfig>.GetSingleton();
            base.Awake();
        }

        protected override void OnSkillUpdate()
        {
            PlaySkillAudio();
            ProductLittleBlueBird();
        }

        protected  void ProductLittleBlueBird()
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject go;
                if(!GoReusePool.Take(typeof(LittleBlueBird).Name,out go))
                {
                    if (!GoLoad.Take(m_Config.SelfResPath, out go))
                    {
                        return;
                    }
                }
                LittleBlueBird blueBird;              
                if (!go.TryGetComponent(out blueBird))
                {
                    blueBird = go.AddComponent<LittleBlueBird>();
                }
                go.transform.position = transform.position;              
                Vector2 velocity = m_Rig2D.velocity;
                float angle = Vector2.SignedAngle(Vector2.right, velocity.normalized);
                angle += i == 0 ? 10 : i == 1 ? 0 : -10;
                velocity = new Vector2(Mathf.Cos(angle / 180.0f * Mathf.PI), Mathf.Sin(angle / 180.0f * Mathf.PI));
                blueBird.SetBirdFlyVelocity(velocity * m_Rig2D.velocity.magnitude);
                MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(blueBird.OnBirdFlyUpdate_Common);
                blueBird.ActiveTrailRenderer(true);                
            }
            GoReusePool.Put(GetType().Name, gameObject);
        }     
    }
}
