using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Extend;
namespace Bird_VS_Boar
{
    public class WhiteBird : SkillBird
    {
        [SerializeField]
        /// <summary>
        /// 反作用速度
        /// </summary>
        private float m_ReactionSpeed = 8;
        [SerializeField]
        /// <summary>
        /// 鸡蛋发射速度
        /// </summary>
        private float m_EggShootSpeed = 25;
        protected override void Awake()
        {
            m_BirdType = EnumBirdType.WhiteBird;
            base.Awake();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnSkillUpdate()
        {
            m_Rig2D.velocity = Vector2.up* m_ReactionSpeed;
            m_Anim.SetTrigger("IsHurt");
            ProducetEgg();           
        }     
    
        private void ProducetEgg()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!GoReusePool.Take(typeof(Egg).Name, out GameObject go))
                {
                    if (!OtherConfigInfo.Exists)
                    {
                        return;
                    }
                    if (!GoLoad.Take(OtherConfigInfo.GetSingleton().GetEggPrefabPath(), out go))
                    {
                        return;
                    }
                }
                Egg egg = go.InspectComponent<Egg>();
                go.transform.position = transform.position;
                Vector2 velocity = Vector2.down;
                float angle = Vector2.SignedAngle(Vector2.right, velocity.normalized);
                angle += (i == 0) ? 15 : ((i == 1) ? 0 : -15);
                velocity = new Vector2(Mathf.Cos(angle / 180.0f * Mathf.PI), Mathf.Sin(angle / 180.0f * Mathf.PI));
                egg.Velocity = velocity * m_EggShootSpeed;      
            }         
        }       
    }
}
