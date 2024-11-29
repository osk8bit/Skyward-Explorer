using Assets.Scripts.Components.Model;
using Assets.Scripts.UI.LevelsLoader;
using UnityEngine;

namespace Assets.Scripts.Components.LevelManegment
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            var session = GameSession.Instance;
            session.Save();

            var loader = FindObjectOfType<LevelLoader>();
            loader.LoadLevel(_sceneName);
        }
    }
}
