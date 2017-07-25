using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public int damage = 1;
    
    
	public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }    
}
