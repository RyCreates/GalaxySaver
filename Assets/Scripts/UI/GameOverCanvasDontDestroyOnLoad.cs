using UnityEngine;
using System.Collections;

public class GameOverCanvasDontDestroyOnLoad : MonoBehaviour {

    public static GameOverCanvasDontDestroyOnLoad instance;

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
