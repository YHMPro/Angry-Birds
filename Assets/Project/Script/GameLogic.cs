using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Angry_Birds
{
    public class GameLogic 
    {
        private static Bird nowComeBird = null;
        /// <summary>
        /// 当前登场的小鸟
        /// </summary>
        public static Bird NowComeBird
        {
            get
            {
                return nowComeBird;
            }
            set//测试代码
            {
                nowComeBird = value;
            }
        }
        /// <summary>
        /// 小鸟是否是当前登场的小鸟
        /// </summary>
        /// <param name="bird"></param>
        /// <returns></returns>
        public static bool BirdIsNowComeBirdLogic(Bird bird)
        {
            return NowComeBird == bird;
        }




    }
}
