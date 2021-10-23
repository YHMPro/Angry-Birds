using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Angry_Birds
{
    public class YellowBirdConfigInfo :BirdConfigInfo
    {
        public override float Mass
        {
            get
            {
                return 1;
            }
        }
        protected override string[] SelectAudioPaths
        {
            get
            {
                return new string[] {
                    "Prefabs/Bird/BirdYellow/Audio/BYSelect1"
                };
            }
        }
        protected override string[] FlyAudioPaths
        {
            get
            {
                return new string[] {
                    "Prefabs/Bird/BirdYellow/Audio/BYFly1"
                };
            }
        }
        protected override string[] CrashAudioPaths
        {
            get
            {
                return new string[] {
                    "Prefabs/Bird/BirdYellow/Audio/BYCo1",
                    "Prefabs/Bird/BirdYellow/Audio/BYCo2",
                    "Prefabs/Bird/BirdYellow/Audio/BYCo3",
                    "Prefabs/Bird/BirdYellow/Audio/BYCo4",
                    "Prefabs/Bird/BirdYellow/Audio/BYCo5",
                };
            }
        }

        protected override string[] SkillAudioPaths
        {
            get
            {
                return new string[] {
                    "Prefabs/Bird/BirdYellow/Audio/BYSkill1"            
                };
            }
        }
    }
}
