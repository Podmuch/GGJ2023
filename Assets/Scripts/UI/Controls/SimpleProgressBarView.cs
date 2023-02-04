using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoxColliders.Controls
{
    public sealed class SimpleProgressBarView : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField]
        private Image iconImage;
        [SerializeField] 
        private Slider progressBar;
        [SerializeField] 
        private TextMeshProUGUI progressValue;

        public void SetIcon(Sprite icon)
        {
            iconImage.sprite = icon;
        }

        public void SetBarValue(float value)
        {
            progressBar.value = value;
        }

        public void SetTextValue(float value)
        {
            progressValue.text = Mathf.RoundToInt(value).ToString();
        }
    }
}