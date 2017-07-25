using UnityEngine;
using System.Collections;

public class WinCanvasDontDestroyOnLoad : MonoBehaviour {

    public static WinCanvasDontDestroyOnLoad instance;

    void Awake()
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
