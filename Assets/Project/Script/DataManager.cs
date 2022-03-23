using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bird_VS_Boar.LevelConfig;
using Bird_VS_Boar;
namespace Bird_VS_Boar.Data
{
    /// <summary>
    /// 数据管理
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        public List<GameObject> Test=new List<GameObject>();    




        public void OnApplicationQuit()
        {
            DataSave();
        }

        public void DataClear()
        {
            LevelConfigManager.ResetLevelData();          
        }
        /// <summary>
        /// 数据清除
        /// </summary>
        private void DataSave()
        {
            LevelConfigManager.SaveLevelConfig();
        }
    }
}
