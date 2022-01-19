using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Extend;
namespace Bird_VS_Boar
{
    public class WhiteBird : SkillBird
    {
        protected override void Awake()
        {
            m_BirdType = EnumBirdType.WhiteBird;
            base.Awake();
        }
        protected override void OnSkillUpdate()
        {
            base.OnSkillUpdate();
            m_Rig2D.isKinematic = true;
            m_Rig2D.velocity = Vector2.zero;
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,ProductEggUpdate);
        }     
        private float m_Interval = 10;
        private float m_Time = 0;
        private void ProductEggUpdate()
        {
            if (Time.time >= m_Time)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,ProductEggUpdate);
                m_Anim.SetTrigger("IsSkill");
                MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(m_Anim.AnimatorClipTimeLength("WhiteBirdSkill"), ProducetEgg);                           
            }
        }
        private void ProducetEgg()
        {
            GameObject go;
            if (!GoReusePool.Take(typeof(Egg).Name, out go))
            {               
                if(!NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    return;
                }
                if (!GoLoad.Take(NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().GetEggPrefabPath(), out go))
                {
                    return;
                }
            }
            m_Anim.SetTrigger("IsDefault");
            PlaySkillAudio();
            go.transform.position = transform.position;
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,ProductEggUpdate);
            m_Time = Time.time + m_Interval;
        }       
    }
}
