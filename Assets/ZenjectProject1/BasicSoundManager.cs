using UnityEngine;

namespace ZenjectProject1
{
    public class BasicSoundManager : ISoundManager
    {
        public void Play()
        {
            Debug.Log("Music plays 1");
        }

        public void Stop()
        {
            Debug.Log("Music stops 1");
        }
    }
}