using UnityEngine;
using UnityEngine.UI;

namespace PDGames.UserInterface
{
    [RequireComponent(typeof(CanvasRenderer))]
    public sealed class EmptyGraphic : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}