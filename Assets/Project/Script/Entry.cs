using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class Entry : MonoBehaviour
    {
        public Bird b;
        private void Awake()
        {
            if(MonoSingletonFactory<Camera2D>.SingletonExist)
            {
                MonoSingletonFactory<Camera2D>.GetSingleton().SetLimit(3, 5, 4, 5);
            }
            
        }
        
        // Start is called before the first frame update
        void OnEnable()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(!GoReusePool.Take("VanBird", out GameObject go))
                {
                    if(GoLoad.Take("VanBirdConfig/VanBird", out go))
                    {
                        if(!go.TryGetComponent(out VanBird bird))
                        {
                            go.AddComponent<VanBird>();
                        }                       
                    }       
                }
                GameLogic.NowComeBird = go.GetComponent<VanBird>();
                MonoSingletonFactory<Camera2D>.GetSingleton();
                MonoSingletonFactory<FlyPath>.GetSingleton();
                MonoSingletonFactory<Audio2DMgr>.GetSingleton();
                MonoSingletonFactory<SlingShot>.GetSingleton().BindBird();
            }
        }
    }
}
