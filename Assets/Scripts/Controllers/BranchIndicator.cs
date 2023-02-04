using UnityEngine;

public class BranchIndicator : MonoBehaviour
{
    [SerializeField] 
    private Transform indicatorParent; 
    [SerializeField] 
    private SpriteRenderer indicatorSprite;
    
    public void Initialize()
    {
        
    }

    public void SetPosition(Vector2 position)
    {
        indicatorParent.position = position;
    }
}
