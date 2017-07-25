using UnityEngine;
using System.Collections;

public class PauseCanvasDontDestroyOnLoad : MonoBehaviour {

    public static PauseCanvasDontDestroyOnLoad instance;

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
