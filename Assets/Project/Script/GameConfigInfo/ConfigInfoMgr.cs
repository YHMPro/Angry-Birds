using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class ConfigInfoMgr 
    {
        public static void ConfigInfoInit()
        {
            NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().InitConfigInfo();//创建单例并实例化配置信息
            #region 猪
            PigConfigInfo.PigConfigInfoDic.Add("YoungPig", new YoungPigConfigInfo());
            #endregion

            #region 障碍物
            BarrierConfigInfo.BarrierConfigInfoDic.Add("Ice", new IceConfigInfo());

            #endregion
            ////红色小鸟
            //BirdConfigInfo.BirdConfigInfoDic.Add("RedBird", new RedBirdConfigInfo());
            ////黑色
            //BirdConfigInfo.BirdConfigInfoDic.Add("BlackBird", new BlackBirdConfigInfo());
            ////蓝色
            //BirdConfigInfo.BirdConfigInfoDic.Add("BlueBird", new BlueBirdConfigInfo());
            ////绿色
            //BirdConfigInfo.BirdConfigInfoDic.Add("GreenBird", new GreenBirdConfigInfo());
            ////粉色
            //BirdConfigInfo.BirdConfigInfoDic.Add("PinkBird", new PinkBirdConfigInfo());
            ////Van
            //BirdConfigInfo.BirdConfigInfoDic.Add("VanBird", new VanBirdConfigInfo());
            ////白色
            //BirdConfigInfo.BirdConfigInfoDic.Add("WhiteBird", new WhiteBirdConfigInfo());
            ////黄色
            //BirdConfigInfo.BirdConfigInfoDic.Add("YellowBird", new YellowBirdConfigInfo());
        }
    }
}
