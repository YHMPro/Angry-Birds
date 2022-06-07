using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Farme.Tool;
using System.Text.RegularExpressions;
using Farme;
using System.Text;
namespace Bird_VS_Boar.LevelConfig
{
    /// <summary>
    /// 关卡配置管理器
    /// </summary>
    public class LevelConfigManager 
    {
        private static Dictionary<string, LevelConfig> m_LevelConfigDic = new Dictionary<string, LevelConfig>();
        public static string m_FilePath = null;
        public static string FilePath
        {
            get
            {
                return m_FilePath;
            }
            set { m_FilePath = value; }
        }
        /// <summary>
        /// 获取关卡类型中的关卡数量
        /// </summary>
        /// <param name="LevelType">关卡类型</param>
        /// <returns></returns>
        public static int GetLevelNum(EnumGameLevelType LevelType)
        {
            int levelNum = 0;
            Regex regex = new Regex(@LevelType.ToString());          
            foreach(var levelKey in m_LevelConfigDic.Keys)
            {
                levelNum += regex.IsMatch(levelKey) ? 1 : 0;            
            }
            return levelNum;
        }
        /// <summary>
        /// 添加关卡配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="levelConfig"></param>
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
        /// <summary>
        /// 移除关卡配置信息
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveLevelConfig(string key)
        {
            if (m_LevelConfigDic.ContainsKey(key))
            {
                m_LevelConfigDic.Remove(key);
                Debuger.Log("移除场景配置数据:" + key);
            }
        }
        /// <summary>
        /// 重置关卡数据
        /// </summary>
        public static void ResetLevelData()
        {
            foreach(var key in m_LevelConfigDic.Keys)
            {
                LevelConfig levelConfig = m_LevelConfigDic[key];            
                levelConfig.IsThrough = key.Split('_')[1] == "1";
                levelConfig.LevelHistoryRating = 0;
                levelConfig.LevelHistoryScore = 0;               
            }
        }
        /// <summary>
        /// 保存关卡配置信息
        /// </summary>
        public static void SaveLevelConfig()
        {          
            string json = JsonConvert.SerializeObject(m_LevelConfigDic);
            Debuger.Log("保存后配置表数据:" + json);
            FileInfo fileInfo = new FileInfo(m_FilePath);
            using(FileStream fs= fileInfo.Exists? fileInfo.OpenWrite(): fileInfo.Create())
            {
                byte[] data=Encoding.UTF8.GetBytes(json);
                fs.Write(data, 0, data.Length);
            }          
        }
        /// <summary>
        /// 获取关卡配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static LevelConfig GetLevelConfig(string key)
        {
            if(m_LevelConfigDic.TryGetValue(key, out LevelConfig levelConfig))
            {
                return levelConfig;
            }
            return null;
        }     
        /// <summary>
        /// 读取配置信息表数据
        /// </summary>
        public static void ReadConfigTableData()
        {
            string json;
            if (!File.Exists(m_FilePath))
            {
                json = AssetBundleLoad.LoadAsset<TextAsset>("config", "LevelConfig").text;
                Debuger.Log("从AB包读取配置表数据:" + json);
            }
            else 
            {
                FileInfo fileInfo = new FileInfo(m_FilePath);
                using (FileStream fs = fileInfo.OpenRead())
                {
                    byte[] data = new byte[fs.Length];
                    int length = fs.Read(data, 0, data.Length);
                    json = Encoding.UTF8.GetString(data, 0, length);
                    Debuger.Log("从本地读取配置表数据:" + json);
                }
            }        
            m_LevelConfigDic = JsonConvert.DeserializeObject<Dictionary<string, LevelConfig>>(json);
        }
    }
}
