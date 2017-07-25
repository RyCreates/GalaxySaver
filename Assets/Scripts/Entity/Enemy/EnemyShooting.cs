using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public float timeToShoot;
    public float shootingRate = 2f;
    public float shootDelay = 4f;
    public int shotDamage;
    public GameObject enemyShot;
    public Transform shootingPosition1;
    public Transform shootingPosition2;
    public Transform shootingPosition3;
    public Transform shootingPosition4;
    int a, b, c;

    public bool changeShotMovementDirection = false;

    private EnemyShot enShot;

    void Start ()
    {
        int index = Random.Range(-1, 1);
        InvokeRepeating("Shoot", shootDelay+index, shootingRate+index);
	}
	
    public void SetShotVector(int x, int y, int z)
    {
        a = x; b = y; c = z;
    }
	
    public void Shoot()
    {
        if (shootingPosition1 != null)
        {
            GameObject g = (GameObject) Instantiate(enemyShot, shootingPosition1.position, Quaternion.identity);
            g.GetComponent<EnemyShot>().damage *= shotDamage;
            if (changeShotMovementDirection)
            {
                g.GetComponent<EnemyShot>().ChangeMovementDirection(a,b,c);
            }
        }
        if(shootingPosition2!=null)
        {
            GameObject g = (GameObject)Instantiate(enemyShot, shootingPosition2.position, Quaternion.identity);
            g.GetComponent<EnemyShot>().damage *= shotDamage;
        }
        if (shootingPosition3 != null)
        {
            GameObject g = (GameObject)Instantiate(enemyShot, shootingPosition3.position, Quaternion.identity);
            g.GetComponent<EnemyShot>().damage *= shotDamage;
        }
        if (shootingPosition4 != null)
        {
            GameObject g = (GameObject)Instantiate(enemyShot, shootingPosition4.position, Quaternion.identity);
            g.GetComponent<EnemyShot>().damage *= shotDamage;
        }
    }
}
