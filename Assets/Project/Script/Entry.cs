﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Bird_VS_Boar.LevelConfig;
namespace Bird_VS_Boar
{
    public class Entry : MonoBehaviour
    {
        public Bird b;
        public string NowSceneKey;
        private void Awake()
        {
            ConfigInfoMgr.ConfigInfoInit();
            GameLogic.Init();
            GameManager.Init();
            MonoSingletonFactory<Camera2D>.GetSingleton();
            MonoSingletonFactory<FlyPath>.GetSingleton();
            //MonoSingletonFactory<Audio2DMgr>.GetSingleton();
            if (MonoSingletonFactory<Camera2D>.SingletonExist)
            {
                MonoSingletonFactory<Camera2D>.GetSingleton().SetLimit(3, 5, 4, 5);
            }
         
            //Object[] atlas = Resources.LoadAll("BUTTONS_SHEET_1");
            //foreach(var sprite in atlas)
            //{
            //    sprite.name = "SP";
            //    Debug.Log(sprite);
            //}
        }
        
        // Start is called before the first frame update
        void OnEnable()
        {
           
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
            LevelConfigBuilder.NowSceneKey = NowSceneKey;
        }
    }
}
