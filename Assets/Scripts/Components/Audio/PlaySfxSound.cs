using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Components.Audio
{
    public class PlaySfxSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private AudioSource _source;

        public void Play()
        {
            if (_source == null)
                _source = AudioUtills.FindSfxSource();

            _source.PlayOneShot(_clip);
        }
    }
}
