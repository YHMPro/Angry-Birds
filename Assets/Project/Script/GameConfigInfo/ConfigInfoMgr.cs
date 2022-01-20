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
            PigConfigInfo.PigConfigInfoDic.Add(EnumPigType.YoungPig, new YoungPigConfigInfo());
            PigConfigInfo.PigConfigInfoDic.Add(EnumPigType.OldPig, new OldPigConfigInfo());
            PigConfigInfo.PigConfigInfoDic.Add(EnumPigType.RockPig, new RockPigConfigInfo());
            #endregion

            #region 障碍物
            BarrierConfigInfo.BarrierConfigInfoDic.Add(EnumBarrierType.Ice, new IceConfigInfo());
            BarrierConfigInfo.BarrierConfigInfoDic.Add(EnumBarrierType.Wood, new WoodConfigInfo());
            BarrierConfigInfo.BarrierConfigInfoDic.Add(EnumBarrierType.Rock, new RockConfigInfo());
            #endregion

            #region 季节
            SeasonConfigInfo.SeasonConfigInfoDic.Add(EnumGameLevelType.Spring,new SpringConfigInfo());
            SeasonConfigInfo.SeasonConfigInfoDic.Add(EnumGameLevelType.Summer, new SummerConfigInfo());
            SeasonConfigInfo.SeasonConfigInfoDic.Add(EnumGameLevelType.Autumn, new AutumnConfigInfo());
            SeasonConfigInfo.SeasonConfigInfoDic.Add(EnumGameLevelType.Winter, new WinterConfigInfo());
            #endregion        
        }
    }
}
