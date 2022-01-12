using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
public class Test1 : BaseMono
{
    protected override void Awake()
    {
        base.Awake();
        
        foreach(var rectTran in GetComponentsInChildren<RectTransform>())
        {
            rectTran.offsetMax = Vector2.zero;
            rectTran.offsetMin = Vector2.zero;
        }

    }
}
