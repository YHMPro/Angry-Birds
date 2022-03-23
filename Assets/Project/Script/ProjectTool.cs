using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class ProjectTool
    {
        /// <summary>
        /// 解析RES路径
        /// </summary>
        /// <param name="resPath"></param>
        /// <returns>data[0]:包名  data[1]:资源名</returns>
        public static string[] ParsingRESPath(string resPath)
        {
            string[] data = resPath.Split('/');
            if(data.Length>2)
            {
                //忽略第一个元素
                data[0] = data[1];
                data[1] = data[2];
                data[2] = null;
            }           
            data[0] = data[0].ToLower();//转小写字母
            return data;
        }
    }
}
