using Assets.Scripts.Components.Model.Data.Properties;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Widgets
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _value;

        private FloatPersistentProperty _model;

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        private void OnSliderValueChanged(float value)
        {
            _model.Value = value;
        }
        private void OnValueChanged(float newValue, float oldValue)
        {
            var textValue = Mathf.Round(newValue * 100);
            _value.text = textValue.ToString();

            _slider.normalizedValue = newValue;
        }

        public void SetModel(FloatPersistentProperty model)
        {
            _model = model;
            model.Subscribe(OnValueChanged);
            OnValueChanged(model.Value, model.Value);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}
