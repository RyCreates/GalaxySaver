using UnityEngine;
using System.Collections;

public class GameCanvasDontDestroyOnLoad : MonoBehaviour
{
    public static GameCanvasDontDestroyOnLoad instance;

    void Awake ()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
