using Assets.Scripts.Components.Model.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        public PlayerData Data => _data;


        private PlayerData _save;
        public static GameSession Instance { get; private set; }

        private void Awake()
        {
            var exsistSession = GetExsistSession();
            if (exsistSession != null)
            {
                exsistSession.StartSession();
                Destroy(gameObject);
            }
            else
            {
                Save();
                DontDestroyOnLoad(this);
                Instance = this;
                StartSession();
            }
        }

         public void Save()
        {
            _save = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _save.Clone();

        }

        private void StartSession()
        {
            LoadUI();
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        private void LoadUI()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        private GameSession GetExsistSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                    return gameSession;
            }
            return null;
        }

        public readonly List<string> _removedItems = new List<string>();

        public bool RestoreState(string itemId)
        {
            return _removedItems.Contains(itemId);
        }

        public void StoreState(string itemId)
        {
            if (!_removedItems.Contains(itemId))
                _removedItems.Add(itemId);
        }
    }

}

