using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Bird_VS_Boar.LevelConfig;
using Farme.UI;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Farme.Audio;
using Bird_VS_Boar.Data;
namespace Bird_VS_Boar
{
    public class Entry : MonoBehaviour
    {
        [Header("关卡是否通过")]
        public bool IsThrough = false;
        [Header("关卡提供的硬币数量")]
        public int CoinNum = 0;
        [Header("关卡索引")]
        public int LevelIndex = -1;
        [Header("关卡类型")]
        public EnumGameLevelType LevelType=EnumGameLevelType.None;
        
        private void Awake()
        {
            //PlayerPrefs.SetFloat("GlobalVolume", 1);
            //PlayerPrefs.SetFloat("ButtonVolume", 1);
            //PlayerPrefs.SetFloat("BgVolume", 1);
            //PlayerPrefs.SetFloat("EffectVolume", 1);
            //PlayerPrefs.Save();    
            LevelConfigManager.ReadConfigTableData();//读取配置表数据
            MonoSingletonFactory<ShareMono>.GetSingleton(null, false);
            MonoSingletonFactory<DataManager>.GetSingleton(null, false);
            NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().InitConfigInfo();//创建单例并实例化配置信息
        }

        public void Start()
        {
            if (GoLoad.Take("FarmeLockFile/WindowRoot", out GameObject windowRootGo))
            {
                WindowRoot windowRoot = MonoSingletonFactory<WindowRoot>.GetSingleton(windowRootGo, false);
                windowRoot.CreateWindow("GameLoginWindow", RenderMode.ScreenSpaceCamera, (window) =>
                {
                    window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                    window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式
                    window.Canvas.sortingOrder = 0;//设置画布层级
                    window.CreatePanel<GameLoginPanel>("UI/GameLoginWindow/GameLoginPanel", "GameLoginPanel", EnumPanelLayer.BOTTOM, (panel) =>//加载面板
                    {

                    });
                });
                windowRoot.CreateWindow("GameGlobalWindow", RenderMode.ScreenSpaceCamera, (window) =>
                {
                    window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                    window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式
                    window.Canvas.sortingOrder = 1;//设置画布层级
                    window.CreatePanel<GameSetPanel>("UI/GameGlobalWindow/GameSetPanel", "GameSetPanel", EnumPanelLayer.TOP, (panel) =>//加载面板
                    {
                        panel.SetState(EnumPanelState.Hide, () =>
                        {
                            panel.RefreshPanel();
                        });
                    });
                });
            }
        }
        // Start is called before the first frame update
        void OnEnable()
        {
           
        }

        private void OnDisable()
        {
        }
        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
                
            //}
        }


        private void OnValidate()
        {
            //LevelConfigBuilder.LevelIndex = LevelIndex;
            //LevelConfigBuilder.LevelType = LevelType;
            //LevelConfigBuilder.IsThrough = IsThrough;
            //LevelConfigBuilder.CoinNum = CoinNum;
        }
    }
}
