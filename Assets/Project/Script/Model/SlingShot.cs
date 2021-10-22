using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class SlingShot : BaseMono
    {      
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<LineRenderer>();
        }

        protected override void Start()
        {
            base.Start();


        }


        public void RendererLine( )
        {

        }
    }
}
