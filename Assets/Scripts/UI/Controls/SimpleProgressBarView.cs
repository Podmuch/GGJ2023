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
        [SerializeField] 
        private RectTransform neededValueParentIndicator;
        [SerializeField] 
        private RectTransform neededValueIndicator;

        public void SetIcon(Sprite icon)
        {
            iconImage.sprite = icon;
        }

        public void SetBarValue(float value)
        {
            progressBar.value = value;
        }
        
        public void SetNeededIndicatorValue(float value)
        {
            var width = neededValueParentIndicator.rect.width;

            neededValueIndicator.anchoredPosition = new Vector2(value * width, neededValueIndicator.anchoredPosition.y);
        }

        public void SetTextValue(float value)
        {
            progressValue.text = Mathf.RoundToInt(value).ToString();
        }
    }
}