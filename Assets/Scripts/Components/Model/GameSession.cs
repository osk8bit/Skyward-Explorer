using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Utils.Disposables;
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

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public InventoryModel Inventory { get; private set; }

        public StatsModel StatsModel { get; private set; }

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
                InitModels();
                DontDestroyOnLoad(this);
                Instance = this;
                StartSession();
            }

        }


        private void InitModels()
        {
            Inventory = new InventoryModel(Data);
            _trash.Retain(Inventory);

            StatsModel = new StatsModel(_data);
            _trash.Retain(StatsModel);
        }

        public void Save()
        {
            _save = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _save.Clone();

            _trash.Dispose();
            InitModels();

        }

        private void StartSession()
        {
            if (!SceneManager.GetSceneByName("Hud").isLoaded)
            {
                LoadUI();
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
            _trash.Dispose();
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

