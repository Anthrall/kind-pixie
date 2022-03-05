using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public int bad_end, good_end, characters;
    public static DataHolder instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(transform.gameObject);
    }
}
