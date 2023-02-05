using UnityEngine;

public class BranchIndicator : MonoBehaviour
{
    [SerializeField] 
    private Transform indicatorParent; 
    [SerializeField] 
    private SpriteRenderer indicatorSprite;

    private Transform followedTransform;

    private void Update()
    {
        if(followedTransform != null)
        indicatorParent.position = followedTransform.position;
    }

    public void Initialize()
    {
        
    }
    
    public void SetPosition(Transform followedTransform)
    {
        this.followedTransform = followedTransform;
    }
}
