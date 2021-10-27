using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 配置类型
    /// </summary>
    public enum EnumConfigType
    {
        NoneConfig,
        RedBirdConfig,
        BlackBirdConfig,
        BlueBirdConfig,
        GreenBirdConfig,
        PinkBirdConfig,
        VanBirdConfig,
        WhiteBirdConfig,
        YellowBirdConfig
    }
    public class ProjectTool
    {
        public static BaseConfig GetConfig(EnumConfigType configType)
        {
            BaseConfig config = null;
            switch (configType)
            {
                case EnumConfigType.BlackBirdConfig:
                    {
                        config = NotMonoSingletonFactory<BlackBirdConfig>.GetSingleton();
                        break;
                    }
                case EnumConfigType.BlueBirdConfig:
                    {
                        config = NotMonoSingletonFactory<BlueBirdConfig>.GetSingleton();
                        break;
                    }
                case EnumConfigType.GreenBirdConfig:
                    {
                        config = NotMonoSingletonFactory<GreenBirdConfig>.GetSingleton();
                        break;
                    }
                case EnumConfigType.NoneConfig:
                    {
                        config = null;
                        break;
                    }
                case EnumConfigType.PinkBirdConfig:
                    {
                        config = NotMonoSingletonFactory<PinkBirdConfig>.GetSingleton();
                        break;
                    }
                case EnumConfigType.RedBirdConfig:
                    {
                        config = NotMonoSingletonFactory<RedBirdConfig>.GetSingleton();
                        break;
                    }
                case EnumConfigType.VanBirdConfig:
                    {
                        config = NotMonoSingletonFactory<VanBirdConfig>.GetSingleton();
                        break;
                    }
                case EnumConfigType.WhiteBirdConfig:
                    {
                        config = NotMonoSingletonFactory<WhiteBirdConfig>.GetSingleton();
                        break;
                    }
                case EnumConfigType.YellowBirdConfig:
                    {
                        config = NotMonoSingletonFactory<YellowBirdConfig>.GetSingleton();
                        break;
                    }
            }
            return config;         
        }

        public static EnumConfigType GoodsTypeToConfigType(EnumGoodsType goodsType)
        {
            EnumConfigType configType = EnumConfigType.NoneConfig;
            switch (goodsType)
            {
                case EnumGoodsType.BlackBird:
                    {
                        configType = EnumConfigType.BlackBirdConfig;
                        break;
                    }
                case EnumGoodsType.BlueBird:
                    {
                        configType = EnumConfigType.BlueBirdConfig;
                        break;
                    }
                case EnumGoodsType.GreenBird:
                    {
                        configType = EnumConfigType.GreenBirdConfig;
                        break;
                    }
                case EnumGoodsType.None:
                    {
                        configType = EnumConfigType.NoneConfig;
                        break;
                    }
                case EnumGoodsType.PinkBird:
                    {
                        configType = EnumConfigType.PinkBirdConfig;
                        break;
                    }
                case EnumGoodsType.RedBird:
                    {
                        configType = EnumConfigType.RedBirdConfig;
                        break;
                    }
                case EnumGoodsType.VanBird:
                    {
                        configType = EnumConfigType.VanBirdConfig;
                        break;
                    }
                case EnumGoodsType.WhiteBird:
                    {
                        configType = EnumConfigType.WhiteBirdConfig;
                        break;
                    }
                case EnumGoodsType.YellowBird:
                    {
                        configType = EnumConfigType.YellowBirdConfig;
                        break;
                    }
            }
            return configType;
        }
    }
}
