using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Widgets
{
    public class ProgressBarWidgetStats : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
}
