using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class LittleBlueBird : Bird
    {
        protected override void Awake()
        {
            m_BirdType = EnumBirdType.LittleBlueBird;
            base.Awake();
        }

        protected override void OnEnable()
        {
            GameManager.AddDiedTarget(this);
            GameManager.AddBird(this);
            base.OnEnable();
           
        }

        protected override void OnDisable()
        {
            GameManager.RemoveDiedTarget(this);
            GameManager.RemoveBird(this);
            RemoveOnBirdFlyUpdate_Common();
            RecyclyAudio();//回收音效
          
            
        }
        protected override void Start()
        {
            transform.localScale = Vector3.one;
            base.Start();        
        }

        protected override void OnMouseDown()
        {
            
        }

        protected override void OnBirdFlyBreak()
        {
            m_Rig2D.freezeRotation = false;
            base.OnBirdFlyBreak();           
        }
         
        protected override void InitTrailRenderer()
        {
            m_TRenderer.startWidth = 0.075f;
            m_TRenderer.endWidth = 0.025f;
            m_TRenderer.time = 0.5f;
            m_TRenderer.startColor = Color.white;
            m_TRenderer.endColor = new Color32(0, 0, 0, 0);
        }
    }
}
