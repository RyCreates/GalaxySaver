using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public float timeToShoot;
    public float shootingRate = 0.5f;
    public int shotStrenght;
    private GameObject playerShot1;
    private GameObject playerShot2;
    private GameObject playerShot3;
    public GameObject playerShotRed;
    public GameObject playerShotBlue;
    public GameObject playerShotGreen;

    public GameObject blueShotStrength1;
    public GameObject blueShotStrength2;
    public GameObject blueShotStrength3;
    public GameObject blueShotStrength4; 


    public Transform shootingPosition1;
    public Transform shootingPosition2;
    public Transform shootingPosition3;

    public bool leftPositionShooting = false;
    public bool middlePositionShooting = true;
    public bool rightPositionShooting = false;

    public enum PlayerGun
    {
        Red,
        Blue,
        Green
    }

    public PlayerGun playerGun;

	void Start ()
    {
        //shotStrenght = GM.instance.playerShotStrenght;
        playerShot1 = blueShotStrength1;
        leftPositionShooting = false;
        middlePositionShooting = true;
        rightPositionShooting = false;
        StartCoroutine(DelayASecond());
    }

    IEnumerator DelayASecond()
    {
        yield return new WaitForSeconds(0.1f);
        shotStrenght = PlayerPrefs.GetInt("ShotForThisLevel");
        ChangePlayerShotStrenght(0);
    }
	
    public void ChangePlayerShotStrenght(int a )
    {
        shotStrenght += a;
        if (shotStrenght > 12) { shotStrenght = 12; }

        if(shotStrenght==1)
        {
            playerShot1 = blueShotStrength1;
            playerShot2 = blueShotStrength1;
            playerShot3 = blueShotStrength1;
            leftPositionShooting = false;
            middlePositionShooting = true;
            rightPositionShooting = false;
        }
        else if(shotStrenght==2)
        {
            playerShot1 = blueShotStrength1;
            playerShot2 = blueShotStrength1;
            playerShot3 = blueShotStrength1;
            leftPositionShooting = true;
            middlePositionShooting = false;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 3)
        {
            playerShot1 = blueShotStrength1;
            playerShot2 = blueShotStrength1;
            playerShot3 = blueShotStrength1;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 4)
        {
            playerShot1 = blueShotStrength2;
            playerShot2 = blueShotStrength1;
            playerShot3 = blueShotStrength1;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 5)
        {
            playerShot1 = blueShotStrength3;
            playerShot2 = blueShotStrength1;
            playerShot3 = blueShotStrength1;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 6)
        {
            playerShot1 = blueShotStrength2;
            playerShot2 = blueShotStrength2;
            playerShot3 = blueShotStrength2;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 7)
        {
            playerShot1 = blueShotStrength3;
            playerShot2 = blueShotStrength2;
            playerShot3 = blueShotStrength2;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 8)
        {
            playerShot1 = blueShotStrength4;
            playerShot2 = blueShotStrength2;
            playerShot3 = blueShotStrength2;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 9)
        {
            playerShot1 = blueShotStrength3;
            playerShot2 = blueShotStrength3;
            playerShot3 = blueShotStrength3;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 10)
        {
            playerShot1 = blueShotStrength4;
            playerShot2 = blueShotStrength3;
            playerShot3 = blueShotStrength3;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 11)
        {
            playerShot1 = blueShotStrength3;
            playerShot2 = blueShotStrength4;
            playerShot3 = blueShotStrength4;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        else if (shotStrenght == 12)
        {
            playerShot1 = blueShotStrength4;
            playerShot2 = blueShotStrength4;
            playerShot3 = blueShotStrength4;
            leftPositionShooting = true;
            middlePositionShooting = true;
            rightPositionShooting = true;
        }
        GM.instance.playerShotStrenght = shotStrenght;
    }
	


	void Update ()
    {
        /*if (playerGun == PlayerGun.Blue) { playerShot1 = playerShotBlue;}
        if (playerGun == PlayerGun.Green) { playerShot1 = playerShotGreen;}
        if (playerGun == PlayerGun.Red) { playerShot1 = playerShotRed;}*/

        if(timeToShoot < Time.time)
        {
            if (shootingPosition1 != null && middlePositionShooting)
            {
                timeToShoot = Time.time + shootingRate;
                GameObject sh = (GameObject)Instantiate(playerShot1, shootingPosition1.position, Quaternion.identity);
                //sh.GetComponent<PlayerShot>().damage *= shotStrenght;
            }
            if (shootingPosition2 != null && leftPositionShooting)
            {
                timeToShoot = Time.time + shootingRate;
                GameObject sh = (GameObject)Instantiate(playerShot2, shootingPosition2.position, Quaternion.identity);
                //sh.GetComponent<PlayerShot>().damage *= shotStrenght;
            }
            if (shootingPosition3 != null && rightPositionShooting)
            {
                timeToShoot = Time.time + shootingRate;
                GameObject sh = (GameObject)Instantiate(playerShot3, shootingPosition3.position, Quaternion.identity);
                //sh.GetComponent<PlayerShot>().damage *= shotStrenght;
            }
        }
    }
}
