using UnityEngine;

public class persistentscript : MonoBehaviour
{
    public string u_name = "Lorem Ipsum";
    public string u_ID = "3141592654";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        persistentscript[] ps = FindObjectsOfType<persistentscript>();
        if (ps.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
