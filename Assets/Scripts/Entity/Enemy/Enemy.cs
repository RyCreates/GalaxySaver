using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int totalHP = 2;
    public int currentHP;
    public int damage = 5;
    private Rigidbody rb;
    public float speed = 3f;
    public Vector3 moveDirection;
    public Vector3 moveDirection1; 
    public bool useMovementPatern;
    public GameObject enemyExplosion;
    public GameObject enemyExplosionNoSound;
    public int money;
    public int points;
    public bool destroyedByThePlayer = false;
    public bool moveByPatternt = false;

    public bool isThisBoss;
    
    public void CreateAsteroid(int xDir, int yDir, int zDir)
    {
        moveDirection = new Vector3(xDir, yDir, zDir);
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        currentHP = totalHP;
        if(!useMovementPatern)
        {
            rb.velocity = moveDirection * speed;
        }
	}

    public void ChangeMovementDirection(int xDir, int yDir, int zDir,float speed1)
    {
        moveDirection1 = new Vector3(xDir, yDir, zDir);
        rb.velocity = moveDirection1 * speed1;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            destroyedByThePlayer = true;
            if (destroyedByThePlayer) { GM.instance.moneyInThisLevel += money; GM.instance.pointsInThisLevel += points; }
            if (GM.soundIsOn)
            { GameObject ex = (GameObject)Instantiate(enemyExplosion, transform.position, Quaternion.identity); }
            if (!GM.soundIsOn)
            { GameObject ex = (GameObject)Instantiate(enemyExplosionNoSound, transform.position, Quaternion.identity); }
            StartCoroutine( DestroyGameObject()); 
            if(isThisBoss)
            {
                this.gameObject.GetComponent<EnemyHealthBar>().BossExplosion();
            }           
        }
    } 

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            GM.instance.UpdatePlayerHP(-damage);
            //Destroy(this.gameObject);
            TakeDamage(GM.playerCurrentHP);
            if (GM.soundIsOn)
            { GameObject ex = (GameObject)Instantiate(enemyExplosion, transform.position, Quaternion.identity); }
            if (!GM.soundIsOn)
            { GameObject ex = (GameObject)Instantiate(enemyExplosionNoSound, transform.position, Quaternion.identity); }
        }
        if (other.tag == "PlayerShot")
        {
            TakeDamage( other.GetComponent<PlayerShot>().damage);
            Destroy(other.gameObject);
        }
    }

    private bool moveLeft = false;
    private bool moveRight = true;

    void Update()
    {
        if(moveByPatternt)
        {
            if (moveRight)
            {
                ChangeMovementDirection(1, 0, 0, 0.7f);
                if (this.transform.position.x > 3.4f) { moveRight = false;  moveLeft = true; }
            }
            else if(moveLeft)
            {
                ChangeMovementDirection(-1, 0, 0, 0.7f);
                if (this.transform.position.x < -3.4f) { moveRight = true; moveLeft = false; }
            }
        }
    }    
}
