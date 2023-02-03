using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] 
    private Transform sunParent; 
    [SerializeField] 
    private SpriteRenderer sunSprite;
    
    
    public void Initialize()
    {
        
    }

    public void SetPosition(Vector2 position)
    {
        sunParent.localPosition = position;
    }
}
