using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Farme.Tool;
namespace Bird_VS_Boar.LevelConfig
{
    public class LevelConfigBuilder //: EditorWindow
    {
        /// <summary>
        /// 关卡索引
        /// </summary>
        public static int LevelIndex = -1;
        /// <summary>
        /// 关卡类型
        /// </summary>
        public static EnumGameLevelType LevelType = EnumGameLevelType.None;
        [MenuItem("场景配置数据构建工具/读取配置表数据")]
        public static void ReadConfigTableData()
        {
            LevelConfigManager.ReadConfigTableData();
        }
        [MenuItem("场景配置数据构建工具/构建场景配置数据")]
        public static void BuilderSceneConfigData()
        {
            LevelConfig config = new LevelConfig();
            config.LevelType = LevelType;
            //查找场景内的弹弓
            SlingShot slingShot = Object.FindObjectOfType<SlingShot>();
            if (slingShot != null)
            {
                config.SlingShotPosition.SetValue(slingShot.transform.position);
            }
            else
            {
                Debuger.Log("场景内缺少弹弓");
            }
            //查找场景内的相机
            Camera2D camer2D = Object.FindObjectOfType<Camera2D>();
            if (camer2D != null)
            {
                config.Camera2DPosition.SetValue(camer2D.transform.position);
            }
            else
            {
                Debuger.Log("场景内缺少Camera2D相机");
            }
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
            LevelConfigManager.AddLevelConfig(LevelType.ToString()+"_"+LevelIndex, config);         
        }
        [MenuItem("场景配置数据构建工具/清除场景配置数据")]
        public static void ClearSceneConfigData()
        {
            LevelConfigManager.RemoveLevelConfig(LevelType.ToString()+"_"+LevelIndex);
        }
        [MenuItem("场景配置数据构建工具/保存场景数据")]
        public static void SaveSceneConfigData()
        {
            LevelConfigManager.SaveLevelConfig();
        }


        //[MenuItem("场景配置数据构建工具/打开配置窗口", false, 50)]
        //static void ShowMyWindow()
        //{
        //    LevelConfigBuilder window = EditorWindow.GetWindow<LevelConfigBuilder>();//创建一个功能窗口
        //    window.Show();//显示功能窗口
        //}
        
        //private void OnGUI()
        //{
        //    GUILayout.Label("场景配置数据构建工具窗口");
        //    GUILayout.Space(20);
        //    GUILayout.Label("关卡索引:");
        //    LevelIndex = GUILayout.TextField(LevelIndex);

            
        //    //if (GUILayout.Button("点击我"))
        //    //{
        //    //    GameObject go = new GameObject(name);
        //    //    Debug.Log("创建了一个空的游戏对象");
        //    //    Undo.RegisterCreatedObjectUndo(go, "Creat gameobject");//将创建的操作记录记录上这样可以撤回已经创建的对象
        //    //}
        //}
    }
}
