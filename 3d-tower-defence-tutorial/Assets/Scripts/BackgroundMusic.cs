using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private void Awake()
    {
        var musicObjects = GameObject.FindGameObjectsWithTag("Music");
        
        // there should only ever be a single music game object
        if (musicObjects.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
