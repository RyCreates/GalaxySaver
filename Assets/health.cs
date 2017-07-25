using UnityEngine;
using System.Collections;

public class health : MonoBehaviour
{
    public int hp = 10;

    private Rigidbody rb;
    private Vector3 direction = new Vector3(0, 0, -1);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * 1.5f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GM.instance.UpdatePlayerHP(+hp);
            Destroy(this.gameObject);
        }
    }
}
