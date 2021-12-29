using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public enum EnumBoomType
    {
        None,
        PigBoom,
        BirdBoom
    }
    public class Boom : MonoBehaviour
    {
        private void Awake()
        {

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
