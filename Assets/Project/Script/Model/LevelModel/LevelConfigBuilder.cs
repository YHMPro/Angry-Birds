using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Bird_VS_Boar.LevelConfig
{
    public class LevelConfigBuilder 
    {      
        /// <summary>
        /// 当前场景的key
        /// </summary>
        public static string NowSceneKey = "";
        [MenuItem("场景配置数据构建工具/读取配置表数据")]
        public static void ReadConfigTableData()
        {
            LevelConfigManager.ReadConfigTableData();
        }
        [MenuItem("场景配置数据构建工具/构建场景配置数据")]
        public static void BuilderSceneConfigData()
        {         

            LevelConfig config = new LevelConfig();
            //查找场景内所有的猪
            Pig[] pigs = Object.FindObjectsOfType<Pig>();
            foreach (Pig pig in pigs)
            {
                config.PigConfigs.Add(pig.GetPigConfig());
            }
            //查找场景内所有的障碍物
            Barrier[] barriers = Object.FindObjectsOfType<Barrier>();
            foreach (Barrier barrier in barriers)
            {
                config.BarrierConfigs.Add(barrier.GetBarrierConfig());
            }
            LevelConfigManager.AddLevelConfig(NowSceneKey, config);         
        }
        [MenuItem("场景配置数据构建工具/清除场景配置数据")]
        public static void ClearSceneConfigData()
        {
            LevelConfigManager.RemoveLevelConfig(NowSceneKey);
        }
        [MenuItem("场景配置数据构建工具/保存场景数据")]
        public static void SaveSceneConfigData()
        {
            LevelConfigManager.SaveLevelConfig();
        }

    }
}
