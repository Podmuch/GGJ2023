using System;
using PDGames.UserInterface;
using TMPro;
using UnityEngine;

namespace BoxColliders.Windows
{
    [Serializable]
    public sealed class LoadingWindowView : BaseWindowView
    {
        [SerializeField] 
        private TextMeshProUGUI progressLabel = default;
        
        public void UpdateView(float value, string localeKey, string param1, string param2)
        {
            progressLabel.SetText(string.Format($"{localeKey}" +" {0}/{1}", param1, param2));
        }
    }
}