using Assets.Scripts.Components.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components.LevelManegment
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var session = GameSession.Instance;
            session.LoadLastSave();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
