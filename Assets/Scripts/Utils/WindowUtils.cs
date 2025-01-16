using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resorsePath)
        {
            var window = Resources.Load<GameObject>(resorsePath);
            var canvas = GameObject.FindWithTag("MainUiCanvas").GetComponent<Canvas>();
            Object.Instantiate(window, canvas.transform);
        }
    }
}
