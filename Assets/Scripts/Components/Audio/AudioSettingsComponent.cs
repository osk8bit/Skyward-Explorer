using Assets.Scripts.Components.Model.Data.Properties;
using Assets.Scripts.Components.Model.Data;
using System;
using UnityEngine;
using static Assets.Scripts.Components.Model.Data.GameSettings;

namespace Assets.Scripts.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsComponent : MonoBehaviour
    {
        [SerializeField] private SoundSettings _mode;

        private FloatPersistentProperty _model;
        private AudioSource _source;
        private void Start()
        {
            _source = GetComponent<AudioSource>();

            _model = FindProperty();
            _model.OnChanged += OnSoundSettingsChanged;
            OnSoundSettingsChanged(_model.Value, _model.Value);

        }

        private void OnSoundSettingsChanged(float newValue, float oldValue)
        {
            _source.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSettings.Music:
                    return GameSettings.I.Music;
                case SoundSettings.Sfx:
                    return GameSettings.I.Sfx;

            }

            throw new ArgumentException("Undefined model");
        }

        private void OnDestroy()
        {
            _model.OnChanged -= OnSoundSettingsChanged;
        }
    }
}
