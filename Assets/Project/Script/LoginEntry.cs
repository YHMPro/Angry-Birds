using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bird_VS_Boar.LevelConfig;
using Farme;
using Bird_VS_Boar.Data;
using Farme.UI;
using UnityEngine.UI;
using Farme.Tool;
using Farme.Net;
namespace Bird_VS_Boar
{
    public class LoginEntry : MonoBehaviour
    {
        private void Awake()
        {
            //Debuger.Enable = false;

            AssetBundleLoad.PackageCatalogueFile_URL = Application.streamingAssetsPath + "/";
            AssetBundleLoad.MainABName = "StandaloneWindows";
            LevelConfigManager.ReadConfigTableData();//读取配置表数据
            MonoSingletonFactory<ShareMono>.GetSingleton(null, false);
            //MonoSingletonFactory<DataManager>.GetSingleton(null, false);
            NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().InitConfigInfo();//创建单例并实例化配置信息
            //WebDownloadTool.WebDownloadText(@"http://localhost/HotFix.txt", (hotfix) =>
            //{
            //    //LuaEnv luaEnv=new LuaEnv();
            //    //luaEnv.DoString(hotfix);
            //    Debuger.Log(hotfix);
             
            //});
          
        }

        private void Start()
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

    }
}
