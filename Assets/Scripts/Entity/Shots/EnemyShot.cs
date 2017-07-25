using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour
{    
    public float speed = 5f;
    private Rigidbody rb;
    public int damage = 1;
    public Vector3 moveDirection1 ;
    public bool canChangeShotDirection = false;

    public void Start()
    {
        int index = Random.Range(-2, 2);
        rb = GetComponent<Rigidbody>();
        if (!canChangeShotDirection) { rb.velocity = transform.forward * (speed + index); }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GM.instance.UpdatePlayerHP(-damage);
        }
        if (other.tag == "PlayerShot" || other.tag == "Ammo" || other.tag == "Enemy" || other.tag == "EnemyShot" || other.tag == "Health")
        {
            return;
        }
        Destroy(this.gameObject);
    }

    public void ChangeMovementDirection(int xDir, int yDir, int zDir)
    {
        canChangeShotDirection = true;
        moveDirection1 = new Vector3(xDir, yDir, zDir);
        rb = GetComponent<Rigidbody>();
        rb.velocity = moveDirection1 * speed;
    }
}
