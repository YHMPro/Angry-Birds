using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 地图管理器
    /// </summary>
    public class MapManager 
    {
        private static List<Map> m_MapLi = null;
        private static List<Map> MapLi
        {          
            get
            {
                if(m_MapLi == null)
                {
                    m_MapLi = new List<Map>();
                }
                return m_MapLi;
            }
        }
        /// <summary>
        /// 加载地图
        /// </summary>
        /// <returns></returns>
        public static bool LoadMap()
        {
            if (!GoReusePool.Take(GameManager.NowLevelType.ToString() + "/" + GameManager.NowLevelIndex, out GameObject mapGo, null))
            {
                if (!GoLoad.Take("Map/" + GameManager.NowLevelType.ToString() + "/" + GameManager.NowLevelIndex, out mapGo))
                {
                    Debuger.Log(GameManager.NowLevelType.ToString() + "/" + GameManager.NowLevelIndex + "地图加载失败!!!");
                    return false;
                }
            }
            return true;
        }

        public static void AddMapCache(Map map)
        {
            if(!MapLi.Contains(map))
            {
                MapLi.Add(map);
            }
        }
        public static void RemoveMapCache(Map map)
        {
            if (MapLi.Contains(map))
            {
                MapLi.Remove(map);
            }
        }
    }
}
