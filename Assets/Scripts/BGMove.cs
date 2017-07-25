using UnityEngine;
using System.Collections;

public class BGMove : MonoBehaviour
{

    public float speed = -0.1f;
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
	}
	
    public void BGStopMove()
    {
        rb.velocity = transform.forward * 0;
    }
	

	void Update ()
    {
	
	}
}
