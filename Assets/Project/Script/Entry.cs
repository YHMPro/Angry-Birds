using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Bird_VS_Boar.LevelConfig;
using Farme.UI;
using UnityEngine.UI;
using System.Text.RegularExpressions;
namespace Bird_VS_Boar
{
    public class Entry : MonoBehaviour
    {
        public Bird b;
        [Header("关卡索引")]
        public int LevelIndex = -1;
        [Header("关卡类型")]
        public EnumGameLevelType LevelType=EnumGameLevelType.None;
        
        private void Awake()
        {
            //Regex regex = new Regex(@"qew");
            //Debug.Log(regex.IsMatch("qwe_rt"));
            MonoSingletonFactory<ShareMono>.GetSingleton(null, false);
            LevelConfigManager.ReadConfigTableData();//读取配置表数据
            ConfigInfoMgr.ConfigInfoInit(); 
            GameManager.NowLevelType= LevelType;
            //GameManager.Init();
            if(GoLoad.Take("FarmeLockFile/WindowRoot",out GameObject windowRoot))
            {
                MonoSingletonFactory<WindowRoot>.GetSingleton(windowRoot, false).CreateWindow("GameLoginWindow", RenderMode.ScreenSpaceOverlay, (window) =>
                {
                    window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                    window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式
                    window.CreatePanel<GameLevelPanel>("UI/GameLoginWindow/GameLevelPanel", "GameLevelPanel", EnumPanelLayer.MIDDLE, (panel) =>//加载面板
                    {

                    });
                    //window.CreatePanel<GameInterfacePanel>("UI/GameSceneWindow/GameInterfacePanel", "GameInterfacePanel", EnumPanelLayer.MIDDLE, (panel) =>
                    //{

                    //});
                    //window.CreatePanel<GameOverPanel>("UI/GameSceneWindow/GameOverPanel", "GameOverPanel", EnumPanelLayer.TOP, (panel) =>
                    //{
                    //    panel.SetState(EnumPanelState.Hide);
                    //});
                });
            }
            
            return;
            
            //Object[] atlas = Resources.LoadAll("BUTTONS_SHEET_1");
            //foreach(var sprite in atlas)
            //{
            //    sprite.name = "SP";
            //    Debug.Log(sprite);
            //}
        }

        public void Start()
        {
            
        }
        // Start is called before the first frame update
        void OnEnable()
        {
           
        }

        private void OnDisable()
        {
            Debug.Log("失火");
        }
        // Update is called once per frame
        void Update()
        {
            //if(Input.GetKeyDown(KeyCode.Space))
            //{
            //    if(!GoReusePool.Take("BlackBird", out GameObject go))
            //    {
            //        if(GoLoad.Take("BlackBirdConfig/BlackBird", out go))
            //        {
            //            if(!go.TryGetComponent(out BlackBird bird))
            //            {
            //                go.AddComponent<BlackBird>();
            //            }                       
            //        }       
            //    }
            //    GameLogic.NowComeBird = go.GetComponent<BlackBird>();
            //    MonoSingletonFactory<Camera2D>.GetSingleton();
            //    MonoSingletonFactory<FlyPath>.GetSingleton();
            //    MonoSingletonFactory<Audio2DMgr>.GetSingleton();
            //    MonoSingletonFactory<SlingShot>.GetSingleton().BindBird();
            //}
        }


        private void OnValidate()
        {
            GameManager.NowLevelIndex = LevelIndex;
            GameManager.NowLevelType = LevelType;
            LevelConfigBuilder.LevelIndex = LevelIndex;
            LevelConfigBuilder.LevelType = LevelType;
        }
    }
}
