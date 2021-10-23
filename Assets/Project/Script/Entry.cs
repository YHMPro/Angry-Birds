using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class Entry : MonoBehaviour
    {
        public Bird b;
        private void Awake()
        {
           
            

        }
        // Start is called before the first frame update
        void Start()
        {
            GameLogic.NowComeBird = b;
            MonoSingletonFactory<Camera2D>.GetSingleton();
            MonoSingletonFactory<FlyPath>.GetSingleton();
            MonoSingletonFactory<Audio2DMgr>.GetSingleton();
            MonoSingletonFactory<SlingShot>.GetSingleton().BindBird();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
