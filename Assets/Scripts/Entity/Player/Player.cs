using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerShot" && other.tag!="Ammo" && other.tag != "Health")
        {
            if (GM.vibrationIsOn)
            { Handheld.Vibrate(); } 
            Destroy(other.gameObject);
        }
    }


}
