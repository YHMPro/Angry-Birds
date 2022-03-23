using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
using Bird_VS_Boar.LevelConfig;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 地图管理器
    /// </summary>
    public class MapManager 
    {    
        /// <summary>
        /// 加载地图
        /// </summary>
        /// <returns></returns>
        public static bool LoadMap()
        {
            //ClearMapCache();
            if (!GoReusePool.Take(GameManager.NowLevelType.ToString() + "/Map" /*+GameManager.NowLevelIndex*/, out GameObject mapGo, null))
            {
                if (!GoLoad.Take(GameManager.NowSeasonConfigInfo.GetLevelMapPrefabPath(), out mapGo))
                {
                    Debuger.Log(GameManager.NowSeasonConfigInfo.GetLevelMapPrefabPath() + "地图加载失败!!!");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 清除地图缓存
        /// </summary>
        private static void ClearMapCache()
        {
            if (GameManager.NowLevelIndex >= 3)
            {
                //移除NowLevelIndex-2的地图缓存
                if (GoReusePool.Take(GameManager.NowLevelType.ToString() + "/" + (GameManager.NowLevelIndex - 2), out GameObject map1, null))
                {
                    map1.GetComponent<IDied>().Died(true);
                }

            }
            if (GameManager.NowLevelIndex <= (LevelConfigManager.GetLevelNum(GameManager.NowLevelType) - 2))
            {
                //移除NowLevelIndex+2的地图缓存
                if (GoReusePool.Take(GameManager.NowLevelType.ToString() + "/" + (GameManager.NowLevelIndex + 2), out GameObject map2, null))
                {
                    map2.GetComponent<IDied>().Died(true);
                }
            }
        }    
    }
}
