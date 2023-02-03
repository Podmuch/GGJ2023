using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoxColliders.Controls
{
    public sealed class SimpleToggleView : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField]
        private Image background;
        [SerializeField] 
        private TextMeshProUGUI label;

        [Header("Parameters")] 
        [SerializeField]
        private Color activeBgColor = Color.white;
        [SerializeField]
        private Color inactiveBgColor = Color.gray;
        [SerializeField]
        private Color activeLabelColor = Color.black;
        [SerializeField]
        private Color inactiveLabelColor = Color.white;

        public void SetState(bool isActive)
        {
            if (isActive)
            {
                background.color = activeBgColor;
                label.color = activeLabelColor;
            }
            else
            {
                background.color = inactiveBgColor;
                label.color = inactiveLabelColor;
            }
        }
    }
}