using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bird_VS_Boar.LevelConfig;
using Farme;
using Bird_VS_Boar.Data;
using Farme.UI;
using UnityEngine.UI;
using Farme.Tool;
namespace Bird_VS_Boar
{
    public class LoginEntry : MonoBehaviour
    {
        private void Awake()
        {
            AssetBundleLoad.MainABName = "Android";
            AssetBundleLoad.PackageCatalogueFile_URL = Application.streamingAssetsPath+"/";
            LevelConfigManager.FilePath = Application.persistentDataPath + "/LevelConfig.json";
            LevelConfigManager.ReadConfigTableData();//读取配置表数据                                              
            _ =ShareMono.GetSingleton();
            OtherConfigInfo.GetSingleton().InitConfigInfo();//创建单例并实例化配置信息
          
          
        }

        private void Start()
        {
            WindowRoot windowRoot = WindowRoot.GetSingleton();
            windowRoot.CreateWindow("GameLoginWindow", RenderMode.ScreenSpaceCamera, (window) =>
            {
                window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式
                window.CanvasScaler.matchWidthOrHeight = 1;
                window.Canvas.sortingOrder = 0;//设置画布层级
                window.CreatePanel<GameLoginPanel>("UI/GameLoginWindow/GameLoginPanel", "GameLoginPanel", EnumPanelLayer.BOTTOM, (panel) =>//加载面板
                {

                });
            });
            windowRoot.CreateWindow("GameGlobalWindow", RenderMode.ScreenSpaceOverlay, (window) =>
            {
                window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式
                window.CanvasScaler.matchWidthOrHeight = 1;
                window.CreatePanel<GameSetPanel>("UI/GameGlobalWindow/GameSetPanel", "GameSetPanel", EnumPanelLayer.TOP, (panel) =>//加载面板
                {
                    panel.SetState(EnumPanelState.Hide, () =>
                    {
                        panel.RefreshPanel();
                    });
                });
                window.CreatePanel<GameLoadingPanel>("UI/GameGlobalWindow/GameLoadingPanel", "GameLoadingPanel", EnumPanelLayer.SYSTEM, (panel) =>//加载面板
                {
                    panel.SetState(EnumPanelState.Hide, () =>
                    {
                       
                    });
                });
                window.CreatePanel<GameCheatPanel>("UI/GameGlobalWindow/GameCheatPanel", "GameCheatPanel", EnumPanelLayer.BOTTOM, (panel) =>//加载面板
                {
                    panel.SetState(EnumPanelState.Hide, () =>
                    {

                    });
                });
            });     
        }

    }
}
