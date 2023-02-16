using UnityEngine;

public class DontDestroyOnLoadDebug : MonoBehaviour
{
   private void Awake()
   {
      DontDestroyOnLoad(gameObject);
   }
}
