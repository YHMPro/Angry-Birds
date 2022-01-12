using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.Tool;
using Farme;

namespace Bird_VS_Boar
{
    public enum EnumScoreType
    {
        None,
        Red_10000,
        Pink_10000,
        Green_10000,
        Blue_10000,
        Yellow_10000,
        Brown_10000
    }
    public class Score : MonoBehaviour
    {
        /// <summary>
        /// 当前最大的层级
        /// </summary>
        private static int m_NowMaxOrderInLayer = 0;
        /// <summary>
        /// 场上分数的实例数量
        /// </summary>
        private static int m_ScoreNum = 0;
        private SpriteRenderer m_Sr;
        private Animator m_Anim;
        private void Awake()
        {
            m_Sr=GetComponent<SpriteRenderer>();
            m_Anim=GetComponent<Animator>();

            if (m_NowMaxOrderInLayer == 0)
            {
                if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    m_NowMaxOrderInLayer = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().ScoreOrderInLayer;
                }
            }
        }
        protected void Start()
        {
            
        }
        private void OnEnable()
        {
            Debuger.Log("层级累加(Score)");
            m_Sr.sortingOrder = m_NowMaxOrderInLayer;//设置自身层级
            ++m_NowMaxOrderInLayer;
            ++m_ScoreNum;
        }
        private void OnDisable()
        {
            --m_ScoreNum;
            if(m_ScoreNum == 0)
            {
                if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    Debuger.Log("层级重置(Score)");
                    m_NowMaxOrderInLayer = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().ScoreOrderInLayer;
                }
            }
        }
        public static void OpenScore(EnumScoreType scoreType,Vector3 pos)
        {
            if(scoreType == EnumScoreType.None)
            {
                return;
            }
            if (!GoReusePool.Take(typeof(Score).Name, out GameObject go))
            {
                if (!NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    return;
                }
                if (!GoLoad.Take(NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().GetScorePrefabPath(), out go))
                {
                    return;
                }
            }
            go.transform.position = pos;
            go.GetComponent<Animator>().SetTrigger(scoreType.ToString());
        }
        /// <summary>
        /// 这是回调事件(关闭分数)
        /// </summary>
        private void CloseScore()
        {
            Debuger.Log("关闭分数");
            GoReusePool.Put(GetType().Name, gameObject);
        }
        [SerializeField]
        /// <summary>
        /// 这是回调事件(记录分数)
        /// </summary>
        /// <param name="score"></param>
        private void RecordScore(int score)
        {
            Debuger.Log("记录分数");
        }      
    }
}
