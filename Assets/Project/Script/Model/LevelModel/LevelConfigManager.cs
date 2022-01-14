using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Farme.Tool;
namespace Bird_VS_Boar.LevelConfig
{
    /// <summary>
    /// 关卡配置管理器
    /// </summary>
    public class LevelConfigManager 
    {
        private static Dictionary<string, LevelConfig> m_LevelConfigDic = new Dictionary<string, LevelConfig>();
        private static string m_FilePath = Application.streamingAssetsPath + "/" + "LevelConfig.json";
        public static void AddLevelConfig(string key, LevelConfig levelConfig)
        {                     
            if (!m_LevelConfigDic.ContainsKey(key))
            {             
                m_LevelConfigDic.Add(key, levelConfig);
                Debuger.Log("新添加场景配置数据:" + key);
            }
            else
            {          
                m_LevelConfigDic[key] = levelConfig;
                Debuger.Log("覆盖场景配置数据:" + key);
            }
        }
        public static void RemoveLevelConfig(string key)
        {
            if (m_LevelConfigDic.ContainsKey(key))
            {
                m_LevelConfigDic.Remove(key);
                Debuger.Log("移除场景配置数据:" + key);
            }
        }
        public static void SaveLevelConfig()
        {          
            string json = JsonConvert.SerializeObject(m_LevelConfigDic);
            Debuger.Log("保存后配置表数据:" + json);
            if (!File.Exists(m_FilePath))
            {
                File.Create(m_FilePath);
            }
            else
            {
                File.WriteAllText(m_FilePath, json, System.Text.Encoding.UTF8);
            }     
        }
        public static LevelConfig GetLevelConfig(string key)
        {
            if(m_LevelConfigDic.TryGetValue(key, out LevelConfig levelConfig))
            {
                return levelConfig;
            }
            return null;
        }     
        public static void ReadConfigTableData()
        {
            if (!File.Exists(m_FilePath))
            {
                Debuger.Log("配置文件不存在");
                return;
            }
            string json = File.ReadAllText(m_FilePath, System.Text.Encoding.UTF8);
            Debuger.Log("配置表数据:" + json);
            m_LevelConfigDic = JsonConvert.DeserializeObject<Dictionary<string, LevelConfig>>(json);
        }
    }
}
