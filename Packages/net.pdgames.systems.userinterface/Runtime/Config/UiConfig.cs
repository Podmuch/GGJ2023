using UnityEngine;
using UnityEngine.UI;

namespace PDGames.UserInterface
{
    [CreateAssetMenu(fileName = "UiConfig", menuName = "Configs/UiConfig")]
    public sealed class UiConfig : ScriptableObject
    {
        public const string ResourcePath = "Configs/UiConfig";

        [Header("Resources")]
        public string WindowsPath = "Windows/";

        [Header("Canvas")] 
        public string UiRootName;
        public RenderMode RenderMode;
        
        [Header("Canvas Scaler")]
        public CanvasScaler.ScaleMode ScaleMode;
        public Vector2 ReferenceResolution;
        public CanvasScaler.ScreenMatchMode ScreenMatchMode;
        [Range(0.0f, 1.0f)]
        public float MatchWidthOrHeight;
    }
}