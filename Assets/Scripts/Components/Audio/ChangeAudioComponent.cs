using UnityEngine;

namespace Assets.Scripts.Components.Audio
{
    public class ChangeAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        public void ChangeAudio()
        {
            if (_audioClip != null)
            {
                _audioSource.clip = _audioClip;
                _audioSource.Play();
            }

        }
    }
}
