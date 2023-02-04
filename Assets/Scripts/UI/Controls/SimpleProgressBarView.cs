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

        public void SetIcon(Sprite icon)
        {
            iconImage.sprite = icon;
        }

        public void SetValue(float value)
        {
            progressBar.value = value;
        }
    }
}