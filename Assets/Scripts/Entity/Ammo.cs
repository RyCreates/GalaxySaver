using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
    private PlayerShooting player;
    private Rigidbody rb;
    private Vector3 direction = new Vector3(0, 0, -1);

    void Start()
    {
        GameObject am = GameObject.FindWithTag("Player");
        if (am != null) { player = am.GetComponent<PlayerShooting>(); }
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * 1.5f;
    }

	public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            if(player!=null)
            {
                player.GetComponent<PlayerShooting>().ChangePlayerShotStrenght(1);
            }
            Destroy(this.gameObject);
        }
    }
}
