using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 作弊面板
    /// </summary>
    public class GameCheatPanel : BasePanel
    {
        private ElasticBtn m_CheatBtn;
        
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<ElasticBtn>();

            m_CheatBtn=GetComponent<ElasticBtn>("CheatBtn");
        }

        protected override void Start()
        {
            base.Start();
            m_CheatBtn.onClick.AddListener(OnCheat);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_CheatBtn.interactable = true;
        }
        private void OnCheat()
        {
            m_CheatBtn.interactable = false;
            StartCoroutine(IECoinAdd());
        }

        IEnumerator IECoinAdd()
        {
            for (int i = 0; i < 20; i++)
            {
                GameLogic.CoinAdd();
                yield return new WaitForSeconds(0.05f);
            }
            m_CheatBtn.interactable = true;
        }
    }
}
