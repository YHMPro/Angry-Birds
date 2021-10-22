﻿using System.Collections;
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
            MonoSingletonFactory<Camera2D>.GetSingleton();
            MonoSingletonFactory<FlyPath>.GetSingleton().BindBird(b);
            MonoSingletonFactory<SlingShot>.GetSingleton().BindBird(b);




        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
