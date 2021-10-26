using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class WhiteBird : SkillBird
    {
        protected override void Awake()
        {
            m_Config = NotMonoSingletonFactory<WhiteBirdConfig>.GetSingleton();
            base.Awake();
        }


        protected override void OnSkillUpdate()
        {
            base.OnSkillUpdate();
            m_Rig2D.isKinematic = true;
            m_Rig2D.velocity = Vector2.zero;
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(ProductEggUpdate);
        }     
        private float m_Interval = 10;
        private float m_Time = 0;
        private void ProductEggUpdate()
        {
            if (Time.time >= m_Time)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(ProductEggUpdate);
                m_Anim.SetTrigger("IsSkill");
                MonoSingletonFactory<ShareMono>.GetSingleton().DelayUAction(m_Anim.AnimatorClipTimeLength("WhiteBirdSkill"), ProducetEgg);                           
            }
        }
        private void ProducetEgg()
        {
            GameObject go;
            if (!GoReusePool.Take(typeof(Egg).Name, out go))
            {
                if (!GoLoad.Take((m_Config as WhiteBirdConfig).EggPath, out go))
                {
                    return;
                }
            }
            m_Anim.SetTrigger("IsDefault");
            PlaySkillAudio();
            go.transform.position = transform.position;
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(ProductEggUpdate);
            m_Time = Time.time + m_Interval;
        }       
    }
}
