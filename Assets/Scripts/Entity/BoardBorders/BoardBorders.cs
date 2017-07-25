using UnityEngine;
using System.Collections;

public class BoardBorders : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

}
