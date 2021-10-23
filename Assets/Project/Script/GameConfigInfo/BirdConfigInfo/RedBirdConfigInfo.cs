
namespace Angry_Birds
{
    public class RedBirdConfigInfo : BirdConfigInfo
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
                return new string[] { "Prefabs/Bird/RedBird/Audio/BRSelect1" };
            }
        }
        protected override string[] FlyAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/RedBird/Audio/BRFly1" };
            }
        }
        protected override string[] CrashAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/RedBird/Audio/BRCo1" };
            }
        }
    }
}
