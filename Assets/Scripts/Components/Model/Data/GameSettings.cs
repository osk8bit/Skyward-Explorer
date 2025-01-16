﻿using Assets.Scripts.Components.Model.Data.Properties;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data
{
    [CreateAssetMenu(menuName = "Defs/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _music;
        [SerializeField] private FloatPersistentProperty _sfx;

        public FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Sfx => _sfx;


        private static GameSettings _instance;
        public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

        private static GameSettings LoadGameSettings()
        {
            return Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _music = new FloatPersistentProperty(1, SoundSettings.Music.ToString());
            _sfx = new FloatPersistentProperty(1, SoundSettings.Sfx.ToString());
        }

        private void OnValidate()
        {
            Music.Validate();
            Sfx.Validate();
        }

        public enum SoundSettings
        {
            Music,
            Sfx
        }
    }
}
