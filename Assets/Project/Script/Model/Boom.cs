using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;

namespace Bird_VS_Boar
{
    /// <summary>
    /// Boom类型
    /// </summary>
    public enum EnumBoomType
    {
        None,
        PigBoom,
        BirdBoom
    }
    public class Boom : MonoBehaviour
    {
        /// <summary>
        /// 当前最大的层级
        /// </summary>
        private static int m_NowMaxOrderInLayer = 0;
        /// <summary>
        /// 场上分数的实例数量
        /// </summary>
        private static int m_BoomNum = 0;
        /// <summary>
        /// 精灵渲染器
        /// </summary>
        private SpriteRenderer m_Sr = null;
        private void Awake()
        {
            m_Sr = GetComponent<SpriteRenderer>();
            if (m_NowMaxOrderInLayer == 0)
            {
                if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    m_NowMaxOrderInLayer = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().BoomOrderInLayer;
                }
            }
        }

        private void OnEnable()
        {
            Debuger.Log("层级累加(Boom)");
            m_Sr.sortingOrder = m_NowMaxOrderInLayer;//设置自身层级
            ++m_NowMaxOrderInLayer;
            ++m_BoomNum;
        }

        private void OnDisable()
        {
            --m_BoomNum;
            if (m_BoomNum == 0)
            {
                if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    Debuger.Log("层级重置(Boom)");
                    m_NowMaxOrderInLayer = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().BoomOrderInLayer;
                }
            }
        }

        public static void OpenBoom(EnumBoomType boomType,Vector3 pos)
        {
            if(boomType == EnumBoomType.None)
            {
                return;
            }
            if (!GoReusePool.Take(typeof(Boom).Name, out GameObject go))
            {
                if (!NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    return;
                }
                if (!GoLoad.Take(NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().GetBoomPrefabPath(), out go))
                {
                    return;
                }
            }
            go.transform.position = pos;
            go.GetComponent<Animator>().SetTrigger(boomType.ToString());
        }
        [SerializeField]
        private void CloseBoom()
        {
            GoReusePool.Put(GetType().Name, gameObject);                          
        }
    }
}
