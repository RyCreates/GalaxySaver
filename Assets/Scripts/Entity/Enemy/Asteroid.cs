using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    public int totalHP = 2;
    public int currentHP; 
    private Rigidbody rb;
    public float speed = 5f; 
    public Vector3 moveDirection;
    public int damage = 1;
    public int points;
    public GameObject asteroidExplosion;
    public GameObject asteroidExplosionNoSound;
    public bool destroyedByThePlayer = false;

    public void CreateAsteroid(int xDir, int yDir, int zDir, int hp, int Speed)  
    {
        moveDirection = new Vector3(xDir, yDir, zDir);
        totalHP = hp;
        if (totalHP > 2) { totalHP *= 2; }
        currentHP = totalHP; damage = totalHP*5;
        transform.localScale *= hp;
        speed = Speed;
        points = hp;
    }

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = moveDirection.normalized * speed;
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GM.instance.UpdatePlayerHP(-damage);
            Destroy(this.gameObject);
            if (GM.soundIsOn)
            { GameObject ex = (GameObject)Instantiate(asteroidExplosion, transform.position, Quaternion.identity); }
            if (!GM.soundIsOn)
            { GameObject ex = (GameObject)Instantiate(asteroidExplosionNoSound, transform.position, Quaternion.identity); }
        }

        if (other.tag == "PlayerShot")
        {
            TakeDamage(other.GetComponent<PlayerShot>().damage);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if(currentHP<=0)
        {
            destroyedByThePlayer = true;
            if (destroyedByThePlayer) { GM.instance.pointsInThisLevel += points; }
            Destroy(this.gameObject);
            if (GM.soundIsOn)
            { GameObject ex = (GameObject)Instantiate(asteroidExplosion, transform.position, Quaternion.identity); }
            if (!GM.soundIsOn)
            { GameObject ex = (GameObject)Instantiate(asteroidExplosionNoSound, transform.position, Quaternion.identity); }
        }
    }

}
