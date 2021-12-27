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
            Destroy(GetComponent<BlueBird>());
            if (!BirdConfigInfo.BirdConfigInfoDic.ContainsKey(GetType().Name))
            {
                BirdConfigInfo.BirdConfigInfoDic.Add(GetType().Name, new BlueBirdConfigInfo());
            }
            base.Awake();
        }
        protected override void Start()
        {
            base.Start();
            transform.localScale = Vector3.one;
        }
        protected override void OnMouseDown()
        {
            
        }

        protected override void OnBirdFlyUpdate()
        {
            base.OnBirdFlyUpdate();
            m_Rig2D.freezeRotation = false;
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
