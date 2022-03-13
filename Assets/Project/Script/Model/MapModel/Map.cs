using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 地图
    /// </summary>
    public class Map : MonoBehaviour,IDied
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        private void OnEnable()
        {
            GameManager.AddDiedTarget(this);
        }

        private void OnDisable()
        {
            GameManager.RemoveDiedTarget(this);
        }

        public void Died(bool isDestroy = false)
        {
            if (isDestroy)
            {
                //Destroy(gameObject);
            }
            else
            {
                GoReusePool.Put(GameManager.NowLevelType.ToString() +"/"+ GameManager.NowLevelIndex.ToString(), this.gameObject);//回收该障碍物
            }
        }


    }
}
