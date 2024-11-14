using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class AudioUtills
    {
        public const string SfxSourceTag = "SfxAudioSource";
        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}
