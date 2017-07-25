using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;
    public GameObject Enemy6;
    public GameObject Enemy7;
    public GameObject Enemy8;
    public GameObject Enemy9;
    public GameObject Enemy10;
    public GameObject Enemy11;
    public GameObject Enemy12;
    public GameObject Enemy13;
    public GameObject Enemy14Boss;
    public GameObject Enemy15Boss;
    public GameObject Enemy15;
    public GameObject Enemy16Boss;
    public GameObject Enemy18Boss;
    public GameObject Enemy20Boss;
    public GameObject FriendlyShip1;
    public GameObject Asteroid;
    public GameObject Asteroid1;
    public GameObject Asteroid2;
    public GameObject Asteroid3;
    public GameObject Ammo;
    public GameObject health;

    public GameObject BG1;
    public GameObject BG2;
    public GameObject BG3;
    public GameObject currentBG;

    public GameObject soundOfBackground;

    public bool wave1 = false;
    public bool wave2 = false;
    public bool wave3 = false;
    public bool wave4 = false;
    public bool wave5 = false;
    public bool wave6 = false;
    public bool wave7 = false;
    public bool wave8 = false;
    public bool wave9 = false;
    public bool wave10 = false;
    public bool wave11 = false;
    public bool wave12 = false;
    public bool shouldBeatTheBoss = false;
    public bool bossIsBeated = false;

    
    public int wavesInThisLevel;
    public int currentWave;
    public int level;
    public int counter;
    public int screenWidth = 5;
    public float waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed = 5f;

    public Text waveText;
    public Text winText;
    public Text gameOverText;

    //public Text GameOverMoneyText;
    //public Text GameOverPointText;
    public Text WinMoneyText;
    public Text WinPointText;

     
    //public Canvas GameOverCanvas;
    public Canvas WinCanvas;


    protected virtual void Start()
    {
        GameObject p = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        currentWave = 1;
        winText.enabled = false;
        gameOverText.enabled = false;
        GM.instance.WinCanvas.enabled = false;
        //GameOverCanvas.enabled = false;
        waveText.enabled = false;
        if (level != 0)
        { StartCoroutine(CreateLevel()); }
        else if(level==0)
        {StartCoroutine(GetLevel()); }
        GM.instance.ResetGame();
        counter = 0;
        soundOfBackground.SetActive(false);
        if (GM.soundIsOn) { soundOfBackground.SetActive(true); }
    }

    public void SetLevel(int l)
    {
        level = l;
        if (level < 5) { wavesInThisLevel = 5; }
        else if (level < 10) { wavesInThisLevel = 7; }
        else if (level < 14) { wavesInThisLevel = 10; }
        //else if (level == 14) { wavesInThisLevel = 10; } shop
        else if (level < 20) { wavesInThisLevel = 12; }
        else { wavesInThisLevel = 4; }
        GM.instance.level = level;
    }

    IEnumerator GetLevel()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(CreateLevel());
    }

    IEnumerator CreateLevel()
    {
        if(level == 4 || level == 8 || level == 11 || level == 18 )
        {
            currentBG = BG1;
            BG2.SetActive(false);
            BG3.SetActive(false);
        }
        else if(level == 20)
        {
            currentBG = BG2;
            BG1.SetActive(false);
            BG3.SetActive(false);
        }
        else
        {
            currentBG = BG3;
            BG1.SetActive(false);
            BG2.SetActive(false);
        }


        if (wavesInThisLevel < currentWave)
        {
            GM.instance.winText.enabled = true;
            yield return new WaitForSeconds(2f); //SceneManager.LoadScene("MainMenu"); // o gal ismesti lentele ar eiti i next level, ar i main menu ar exit game ?
            GM.instance.winText.enabled = false;

            GM.instance.isWinCanvasOpen = true;
            if (level == 20) { GM.instance.GoToNextLevelButton.enabled = false; }
            GM.playerCurrentHP = 10000;
            GM.instance.WinCanvas.enabled = true;
            Time.timeScale = 0;
            //winText.enabled = true;
            
            
            GM.instance.QuitGameScene();
            GM.instance.ShowWinMoneyText();
            GM.instance.ShowWinPointText();
            GM.instance.totalMoney += GM.instance.moneyInThisLevel;
            GM.instance.totalPoints += GM.instance.pointsInThisLevel;
            GM.instance.moneyInThisLevel = 0;
            GM.instance.pointsInThisLevel = 0;

            if (level == 1) PlayerPrefs.SetInt("L02Open", 1);
            if (level == 2) { PlayerPrefs.SetInt("L03Open", 1); PlayerPrefs.SetInt("L05Open", 1); }
            if (level == 3) PlayerPrefs.SetInt("L04Open", 1);
            if (level == 5) PlayerPrefs.SetInt("L06Open", 1);
            if (level == 6) { PlayerPrefs.SetInt("L07Open", 1); PlayerPrefs.SetInt("L12Open", 1); }
            if (level == 7) { PlayerPrefs.SetInt("L08Open", 1); PlayerPrefs.SetInt("L09Open", 1); }
            if (level == 9) PlayerPrefs.SetInt("L10Open", 1);
            if (level == 10) PlayerPrefs.SetInt("L11Open", 1);
            if (level == 12) PlayerPrefs.SetInt("L13Open", 1);
            if (level == 13) { PlayerPrefs.SetInt("L14Open", 1); PlayerPrefs.SetInt("L15Open", 1); }
            if (level == 15) PlayerPrefs.SetInt("L16Open", 1);
            if (level == 16) PlayerPrefs.SetInt("L17Open", 1);
            if (level == 17) { PlayerPrefs.SetInt("L18Open", 1); PlayerPrefs.SetInt("L19Open", 1); }
            if (level == 19) PlayerPrefs.SetInt("L20Open", 1);
        }


        yield return new WaitForSeconds(0.2f);
        waveText.enabled = true;
        yield return new WaitForSeconds(2f);
        waveText.enabled = false;
        if (level == 1)
        {
            //if (currentWave == 1) { StartCoroutine(CreateEnemy14()); }      // FOR TESTING       
            if (currentWave == 1) { StartCoroutine(SmallAsteroidRain(-1,-4));}
            if (currentWave == 2) { StartCoroutine(NormalAsteroidRain1( -1, -3)); } // add ammo
            if (currentWave == 3) { StartCoroutine(NormalAsteroidRain2( -1, -3)); } // add ammo
            if (currentWave == 4) { StartCoroutine(SmallEnemy13Group1()); }
            if (currentWave == 5) { StartCoroutine(SmallEnemy13Group2()); }
        }
        if (level == 2)
        {
            if (currentWave == 1) { StartCoroutine(SmallEnemy13Group1()); }
            if (currentWave == 2) { StartCoroutine(NormalAsteroidRain2(-1, -4)); } // add ammo
            if (currentWave == 3) { StartCoroutine(NormalAsteroidRain1(-1, -5)); } // add ammo
            if (currentWave == 4) { StartCoroutine(SmallEnemy12Group2()); }
            if (currentWave == 5) { StartCoroutine(SmallEnemy12Group1()); }
        }
        if (level == 3)
        {
            if (currentWave == 1) { StartCoroutine(NormalEnemy13Group1()); } // add ammo
            if (currentWave == 2) { StartCoroutine(NormalEnemy12Group2()); }
            if (currentWave == 3) { StartCoroutine(NormalEnemy12Group1()); } // add ammo
            if (currentWave == 4) { StartCoroutine(NormalEnemy13Group2()); }
            if (currentWave == 5) { StartCoroutine(HugeAsteroidRain1FromRight(-1,-6)); }  
        }
        if(level == 4) // extra level
        {
            if (currentWave == 1) { StartCoroutine(SmallEnemy8and10Group1()); } // add ammo            
            if (currentWave == 2) { StartCoroutine(NormalEnemy8and10Group2()); } // add ammo  
            if (currentWave == 3) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo  
            if (currentWave == 4) { StartCoroutine(SmallEnemy8and10Group2()); }
            if (currentWave == 5) { StartCoroutine(NormalEnemy8and10Group3()); }
        }
        if(level==5)
        {
            if (currentWave == 1) { StartCoroutine(HugeAsteroidRain1FromRight(-1, -5)); }
            if (currentWave == 2) { StartCoroutine(NormalEnemy12And13Group1()); } // add ammo  
            if (currentWave == 3) { StartCoroutine(NormalEnemy12And13Group2()); }
            if (currentWave == 4) { StartCoroutine(NormalEnemy12And13Group1()); } // add ammo  
            if (currentWave == 5) { StartCoroutine(NormalEnemy12And13Group2()); }
            if (currentWave == 6) { StartCoroutine(HugeAsteroidRain1FromRight(-1, -5)); }
            if (currentWave == 7) { StartCoroutine(NormalEnemy2And11Group1()); }
        }
        if (level == 6) // only with 3,4,5,6,7, enemies
        {
            if (currentWave == 1) { StartCoroutine(NormalEnemy3And4And5Group1()); } // add ammo
            if (currentWave == 2) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 3) { StartCoroutine(NormalEnemy3And4And5And6And7Group1()); } // add hp
            if (currentWave == 4) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 5) { StartCoroutine(NormalEnemy3And4And5And6And7Group3()); } // add hp
            if (currentWave == 6) { StartCoroutine(NormalEnemy3And4And5And6And7Group4()); } // add ammo
            if (currentWave == 7) { StartCoroutine(NormalEnemy6And7Group1()); }
        }
        if (level == 7) 
        {
            if (currentWave == 1) { StartCoroutine(HugeAsteroidRain2FromRight(-1, -5)); }
            if (currentWave == 2) { StartCoroutine(HugeAsteroidRain2FromLeft(1,-5)); } // from left
            if (currentWave == 3) { StartCoroutine(HugeAsteroidRain1FromRight(-1, -5)); } // from right
            if (currentWave == 4) { StartCoroutine(HugeAsteroidRain2FromLeft(1, -6)); }
            if (currentWave == 5) { StartCoroutine(HugeAsteroidRain2FromRight(-1, -6)); }
            if (currentWave == 6) { StartCoroutine(HugeAsteroidRain1FromLeft(1,-5)); }
            if (currentWave == 7) { StartCoroutine(SmallEnemy8Group1()); }
        }
        if (level == 8) 
        {
            if (currentWave == 1) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 2) { StartCoroutine(OneEnemy10()); }   
            if (currentWave == 3) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 4) { StartCoroutine(OneEnemy10()); }  
            if (currentWave == 5) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 6) { StartCoroutine(OneEnemy10()); }  
            if (currentWave == 7) { StartCoroutine(CreateEnemy14()); } 
        }
        if (level == 9) 
        {
            if (currentWave == 1) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 2) { StartCoroutine(HugeAsteroidRain2FromLeft(1, -5)); } // from left
            if (currentWave == 3) { StartCoroutine(HugeAsteroidRain1FromRight(-1, -5)); } // from right
            if (currentWave == 4) { StartCoroutine(HugeAsteroidRain2FromLeft(1, -6)); }
            if (currentWave == 5) { StartCoroutine(HugeAsteroidRain2FromRight(-1, -6)); }
            if (currentWave == 6) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 7) { StartCoroutine(NormalEnemy8and10Group2()); } // add ammo 
        }
        if (level == 10) 
        {
            if (currentWave == 1) { StartCoroutine(HugeAsteroidRain2FromLeft(1, -5)); }
            if (currentWave == 2) { StartCoroutine(HugeAsteroidRain1FromRight(-1, -5)); } // from left
            if (currentWave == 3) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 4) { StartCoroutine(HugeAsteroidRain1FromLeft(1, -6)); }
            if (currentWave == 5) { StartCoroutine(NormalEnemy8and10Group2()); } // add ammo 
            if (currentWave == 6) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 7) { StartCoroutine(HugeAsteroidRain2FromRight(-1, -6)); }
            if (currentWave == 8) { StartCoroutine(NormalEnemy8and10Group2()); } // add ammo 
            if (currentWave == 9) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 10) { StartCoroutine(HugeAsteroidRain2FromRight(-1, -6)); }
        }
        if (level == 11) // extra level
        {
            if (currentWave == 1) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 2) { StartCoroutine(OneEnemy10()); }  
            if (currentWave == 3) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 4) { StartCoroutine(OneEnemy10()); }  
            if (currentWave == 5) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 6) { StartCoroutine(OneEnemy10()); }  
            if (currentWave == 7) { StartCoroutine(OneEnemy10()); }  
            if (currentWave == 8) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 9) { StartCoroutine(OneEnemy10()); }  
            if (currentWave == 10) { StartCoroutine(CreateEnemy15Boss()); }
        }
        if (level == 12) // very easy
        {
            if (currentWave == 1) { StartCoroutine(SmallEnemy15Group1()); }  // add ammo
            if (currentWave == 2) { StartCoroutine(NormalEnemy3And4And5And6And7Group1()); } // add hp
            if (currentWave == 3) { StartCoroutine(SmallEnemy15Group2()); }  // add ammo
            if (currentWave == 4) { StartCoroutine(NormalEnemy3And4And5And6And7Group1()); } // add hp
            if (currentWave == 5) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 6) { StartCoroutine(NormalEnemy3And4And5And6And7Group1()); } // add hp
            if (currentWave == 7) { StartCoroutine(SmallEnemy15Group3()); }  // add ammo
            if (currentWave == 8) { StartCoroutine(SmallEnemy15Group1()); }  // add ammo
            if (currentWave == 9) { StartCoroutine(SmallEnemy15Group2()); }   // add ammo
            if (currentWave == 10) { StartCoroutine(LongEnemy1Group1()); }
        }
        if(level==13) // only with 3,4,5,6,7, enemies
        {
            if (currentWave == 1) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 2) { StartCoroutine(NormalEnemy3And4And5Group1()); } // add ammo
            if (currentWave == 3) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 4) { StartCoroutine(NormalEnemy3And4And5And6And7Group1()); } // add hp
            if (currentWave == 5) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 6) { StartCoroutine(NormalEnemy3And4And5And6And7Group3()); } // add hp
            if (currentWave == 7) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 8) { StartCoroutine(NormalEnemy3And4And5And6And7Group4()); } // add ammo
            if (currentWave == 9) { StartCoroutine(NormalEnemy6And7Group1()); }
            if (currentWave == 10) { StartCoroutine(NormalEnemy6And7Group1()); }
        }
        if (level == 15) 
        {
            if (currentWave == 1) { StartCoroutine(HugeAsteroidRain1FromRight(-1, -7)); } 
            if (currentWave == 2) { StartCoroutine(HugeAsteroidRain2FromLeft(1, -7)); }
            if (currentWave == 3) { StartCoroutine(HugeAsteroidRain2FromRight(-1, -6)); }
            if (currentWave == 4) { StartCoroutine(HugeAsteroidRain1FromLeft(1, -7)); }
            if (currentWave == 5) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 6) { StartCoroutine(HugeAsteroidRain2FromRight(-1, -6)); }
            if (currentWave == 7) { StartCoroutine(HugeAsteroidRain1FromRight(-1, -6)); }
            if (currentWave == 8) { StartCoroutine(HugeAsteroidRain2FromRight(-1, -6)); }
            if (currentWave == 9) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 10) { StartCoroutine(NormalEnemy8and10Group2()); }  // add ammo 
            if (currentWave == 11) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 12) { StartCoroutine(NormalEnemy8and10Group2()); } // add ammo 
        }
        if (level == 16) // only with 3,4,5,6,7, enemies
        {
            if (currentWave == 1) { StartCoroutine(NormalEnemy3And4And5Group1()); } // add ammo
            if (currentWave == 2) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 3) { StartCoroutine(NormalEnemy3And4And5And6And7Group1()); } // add hp
            if (currentWave == 4) { StartCoroutine(NormalEnemy3And4And5Group1()); } // add ammo
            if (currentWave == 5) { StartCoroutine(NormalEnemy3And4And5And6And7Group3()); } // add hp
            if (currentWave == 6) { StartCoroutine(NormalEnemy3And4And5Group1()); } // add ammo
            if (currentWave == 7) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 8) { StartCoroutine(NormalEnemy3And4And5And6And7Group4()); } // add ammo
            if (currentWave == 9) { StartCoroutine(NormalEnemy3And4And5And6And7Group3()); } // add hp
            if (currentWave == 10) { StartCoroutine(NormalEnemy3And4And5And6And7Group4()); } // add ammo
            if (currentWave == 11) { StartCoroutine(NormalEnemy3And4And5And6And7Group4()); } // add ammo
            if (currentWave == 12) { StartCoroutine(NormalEnemy6And7Group1()); }
        }
        if (level == 17) // only with 3,4,5,6,7, enemies
        {
            if (currentWave == 1) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 2) { StartCoroutine(NormalEnemy3And4And5Group1()); } // add ammo
            if (currentWave == 3) { StartCoroutine(NormalEnemy3And4And5And6And7Group1()); } // add hp
            if (currentWave == 4) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 5) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 6) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 7) { StartCoroutine(NormalEnemy3And4And5And6And7Group3()); } // add hp
            if (currentWave == 8) { StartCoroutine(NormalEnemy3And4And5And6And7Group4()); } // add ammo
            if (currentWave == 9) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 10) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 11) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 12) { StartCoroutine(NormalEnemy3And4And5Group1()); } // add ammo
        }
        if (level == 18) // extra level
        {
            if (currentWave == 1) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 2) { StartCoroutine(OneEnemy10()); }
            if (currentWave == 3) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 4) { StartCoroutine(OneEnemy10()); }
            if (currentWave == 5) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 6) { StartCoroutine(OneEnemy10()); }
            if (currentWave == 7) { StartCoroutine(OneEnemy10()); }
            if (currentWave == 8) { StartCoroutine(OneEnemy10()); }  // add ammo
            if (currentWave == 9) { StartCoroutine(OneEnemy10()); }
            if (currentWave == 10) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 11) { StartCoroutine(NormalEnemy8and10Group2()); } // add ammo 
            if (currentWave == 12) { StartCoroutine(CreateEnemy18Boss()); }
        }
        if (level == 19)
        {
            if (currentWave == 1) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 2) { StartCoroutine(NormalEnemy3And4And5And6And7Group4()); } // add ammo
            if (currentWave == 3) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 4) { StartCoroutine(NormalEnemy3And4And5And6And7Group4()); } // add ammo
            if (currentWave == 5) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 6) { StartCoroutine(NormalEnemy3And4And5And6And7Group2()); }
            if (currentWave == 7) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 8) { StartCoroutine(NormalEnemy3And4And5Group2()); } // add ammo x2
            if (currentWave == 9) { StartCoroutine(BigEnemy8and10Group1()); }
            if (currentWave == 10) { StartCoroutine(HugeAsteroidRain2FromLeft(1, -6)); }
            if (currentWave == 11) { StartCoroutine(HugeAsteroidRain2FromRight(1, -6)); }
            if (currentWave == 12) { StartCoroutine(HugeAsteroidRain1FromLeft(1, -6)); }
        }
        if (level == 20)
        {
            if (currentWave == 1) { StartCoroutine(NormalEnemy8and10Group1()); } // add ammo 
            if (currentWave == 2) { StartCoroutine(BigEnemy8and10Group1()); } 
            if (currentWave == 3) { StartCoroutine(BigEnemy8and10Group2()); }
            if (currentWave == 4) { StartCoroutine(CreateEnemy20Boss()); } // Boss
        }
    }

    IEnumerator CreateEnemy18Boss()
    {
        if(currentBG!=null){currentBG.GetComponent<BGMove>().BGStopMove();}

        GameObject e = (GameObject)Instantiate(Enemy18Boss, new Vector3(0, 0, 23), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        e.GetComponent<Enemy>().useMovementPatern = true;
        e.GetComponent<Enemy>().ChangeMovementDirection(0, 0, -1, 1);

        //trial shot
        e.GetComponent<EnemyShooting>().SetShotVector(1, 0, 4);
        e.GetComponent<EnemyShooting>().changeShotMovementDirection = true;
        e.GetComponent<EnemyShooting>().Shoot();
        e.GetComponent<EnemyShooting>().changeShotMovementDirection = false;

        yield return new WaitForSeconds(10f);
        //e.GetComponent<Enemy>().ChangeMovementDirection(0, 0, 0, 0);
        e.GetComponent<Enemy>().moveByPatternt = true;
    }

    IEnumerator CreateEnemy20Boss()
    {
        if (currentBG != null) { currentBG.GetComponent<BGMove>().BGStopMove(); }

        GameObject e = (GameObject)Instantiate(Enemy20Boss, new Vector3(0, 0, 23), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        e.GetComponent<Enemy>().useMovementPatern = true;
        e.GetComponent<Enemy>().ChangeMovementDirection(0, 0, -1, 1);


        e.GetComponent<EnemyShooting>().changeShotMovementDirection = true;
        e.GetComponent<EnemyShooting>().Shoot();
        e.GetComponent<EnemyShooting>().changeShotMovementDirection = false;

        yield return new WaitForSeconds(10f);
        //e.GetComponent<Enemy>().ChangeMovementDirection(0, 0, 0, 0);
        e.GetComponent<Enemy>().moveByPatternt = true;
    }

    IEnumerator CreateEnemy14() 
    {
        if (currentBG != null) { currentBG.GetComponent<BGMove>().BGStopMove(); }

        GameObject e = (GameObject)Instantiate(Enemy14Boss, new Vector3(0, 0, 23), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        e.GetComponent<Enemy>().useMovementPatern=true;
        e.GetComponent<Enemy>().ChangeMovementDirection(0, 0, -1, 1);
        
        e.GetComponent<EnemyShooting>().changeShotMovementDirection = true; 
        e.GetComponent<EnemyShooting>().Shoot();
        e.GetComponent<EnemyShooting>().changeShotMovementDirection = false;

        yield return new WaitForSeconds(10f);
        e.GetComponent<Enemy>().moveByPatternt = true;
    }

    IEnumerator CreateEnemy15Boss() 
    {

        if (currentBG != null) { currentBG.GetComponent<BGMove>().BGStopMove(); }
        GameObject e = (GameObject)Instantiate(Enemy15Boss, new Vector3(0, 0, 23), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        e.GetComponent<Enemy>().useMovementPatern = true;
        e.GetComponent<Enemy>().ChangeMovementDirection(0, 0, -1, 1);
        
        e.GetComponent<EnemyShooting>().changeShotMovementDirection = true;
        e.GetComponent<EnemyShooting>().Shoot();
        e.GetComponent<EnemyShooting>().changeShotMovementDirection = false;

        yield return new WaitForSeconds(10f);
        e.GetComponent<Enemy>().moveByPatternt = true;

    }

    IEnumerator OneEnemy10()
    {
        int index = Random.Range(-4, 4);
        GameObject e = (GameObject)Instantiate(Enemy10, new Vector3(index, 0, 18), Quaternion.identity); 
        yield return new WaitForSeconds(1f);
        counter++;
        if(counter == 1 || counter==3 || counter==5 || counter==8)
        {
            int r = Random.Range(-screenWidth, screenWidth);
            Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        }
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator LongEnemy1Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy1, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.9f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy1, new Vector3(-2.5f, 0, 18), Quaternion.identity);

        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy15Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy15, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(-4, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy15Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy15, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(-4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(0, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy15Group3()
    {
        GameObject e = (GameObject)Instantiate(Enemy15, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy15, new Vector3(2, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy9Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy9, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(3f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(-4, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        //int r = Random.Range(-screenWidth, screenWidth);
        //Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy9Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy9, new Vector3(-1f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(-3, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        //int r = Random.Range(-screenWidth, screenWidth);
        //Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy9Group3()
    {
        GameObject e = (GameObject)Instantiate(Enemy9, new Vector3(-4f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(3f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(4, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy9Group4()
    {
        GameObject e = (GameObject)Instantiate(Enemy9, new Vector3(4f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(-3f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy9, new Vector3(0, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        //int r = Random.Range(-screenWidth, screenWidth);
        //Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy8Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy8, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallAsteroidRain(int x, int z) // x and z vectors direction
    {
        int asteroidSpeed = 7;
        int index = Random.Range(0, 2);if (index == 0) { Asteroid = Asteroid1; }else if (index == 1){Asteroid = Asteroid2;}else if(index == 2){ Asteroid = Asteroid3;}
        GameObject a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);


        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed-2);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalAsteroidRain1(int x, int z) // x and z vectors direction
    {
        int asteroidSpeed = 7;
        int index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        GameObject a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 3) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(9, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);

        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed-2);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalAsteroidRain2(int x, int z) // x and z vectors direction
    {
        int asteroidSpeed = 7;
        int index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        GameObject a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed + 2);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0,z, 2, asteroidSpeed+5);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed+1);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0,z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0,z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0,z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0,z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0,z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(9, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);

        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed-2); 
        StartCoroutine(CreateLevel());
    }

    IEnumerator HugeAsteroidRain1FromRight(int x, int z) // x and z vectors direction
    { 
        int asteroidSpeed = 7;
        int index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        GameObject a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed + 4);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed + 1);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed + 5);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(9, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed-2);
        StartCoroutine(CreateLevel());
    }

    IEnumerator HugeAsteroidRain1FromLeft(int x, int z) // x and z vectors direction
    {
        int asteroidSpeed = 7;
        int index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        GameObject a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed + 4);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-0.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.8f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed + 1);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed + 5);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-0.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5.2f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7.7f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6.3f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-9, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3.7f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-9, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.6f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);

        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed-2);
        StartCoroutine(CreateLevel());
    }

    IEnumerator HugeAsteroidRain2FromRight(int x, int z) // x and z vectors direction
    {
        int asteroidSpeed = 7;
        int index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        GameObject a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed + 4);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);

        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);

        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed + 1);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed + 5);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(9, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);

        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 6, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed); index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.13f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2.5f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        a = (GameObject)Instantiate(Asteroid, new Vector3(7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(9f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);

        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed-2);
        StartCoroutine(CreateLevel());
    }

    IEnumerator HugeAsteroidRain2FromLeft(int x, int z) // x and z vectors direction
    {
        int asteroidSpeed = 7;
        int index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        GameObject a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed + 4);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed + 1);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed + 5);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(0, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 4, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-5, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-9, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-8, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.5f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-7, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 1, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-9f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.2f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(4f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(2, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-6, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.3f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-9f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(3, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.4f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-9f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(1, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 3, asteroidSpeed);
        yield return new WaitForSeconds(0.1f);
        index = Random.Range(0, 2); if (index == 0) { Asteroid = Asteroid1; } else if (index == 1) { Asteroid = Asteroid2; } else if (index == 2) { Asteroid = Asteroid3; }
        a = (GameObject)Instantiate(Asteroid, new Vector3(-10f, 0, 18), Quaternion.identity); a.GetComponent<Asteroid>().CreateAsteroid(x, 0, z, 2, asteroidSpeed);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy13Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy13, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-4, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    } 

    IEnumerator SmallEnemy13Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy13, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(1, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy13Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy13, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(5, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy13Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy13, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(3, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy12Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy12, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-4, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);

        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy12Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy12, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(4, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }
    
    IEnumerator NormalEnemy12Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy12, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(5, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy12Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy12, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.6f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.2f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(3, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy8and10Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy8, new Vector3(-1f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2f, 0, 18), Quaternion.identity);yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-3.5f, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator BigEnemy8and10Group1() 
    {
        GameObject e = (GameObject)Instantiate(Enemy8, new Vector3(-1f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-3.5f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        yield return new WaitForSeconds(1f);

        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy8and10Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy8, new Vector3(1f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-3, 0, 18), Quaternion.identity);yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f); 
        e = (GameObject)Instantiate(Enemy8, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.5f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(4f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f); 
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(4f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator BigEnemy8and10Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy8, new Vector3(1f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(4f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(4f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(4f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(4f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(4f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator SmallEnemy8and10Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy8, new Vector3(1f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(-4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(3.8f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4.5f, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }
    
    IEnumerator SmallEnemy8and10Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy8, new Vector3(1f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(4f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy8and10Group3()
    {
        GameObject e = (GameObject)Instantiate(Enemy8, new Vector3(1f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2f, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-5, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy10, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(5.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy8, new Vector3(-3.5f, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }
    
    IEnumerator NormalEnemy12And13Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy12, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.3f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(0, 0, 18), Quaternion.identity);

        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);

        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy12And13Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy13, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.4f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy13, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy12, new Vector3(5, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy2And11Group1() // fast weak ships
    {
        GameObject e = (GameObject)Instantiate(Enemy11, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy11, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy11, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy11, new Vector3(-1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy11, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy11, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy11, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy11, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy11, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy2, new Vector3(5, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed-3f);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy3And4And5Group1()  
    {
        GameObject e = (GameObject)Instantiate(Enemy3, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.6f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(5, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);


        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);

        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy3And4And5Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy4, new Vector3(-3f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);

        e = (GameObject)Instantiate(Enemy4, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.1f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.9f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-5, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy3And4And5And6And7Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy3, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.4f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(health, new Vector3(r, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(3, 0, 18), Quaternion.identity);

        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy3And4And5And6And7Group2()
    {
        GameObject e = (GameObject)Instantiate(Enemy4, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(4, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }
    
    IEnumerator NormalEnemy3And4And5And6And7Group3()
    {
        GameObject e = (GameObject)Instantiate(Enemy3, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(-1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(health, new Vector3(r, 0, 18), Quaternion.identity);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.5f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.3f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(2.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(3, 0, 18), Quaternion.identity);

        r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);

        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    IEnumerator NormalEnemy3And4And5And6And7Group4()
    {
        GameObject e = (GameObject)Instantiate(Enemy4, new Vector3(-3f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-0f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-2f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.2f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy4, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2f);
        e = (GameObject)Instantiate(Enemy5, new Vector3(1, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(4, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(0, 0, 18), Quaternion.identity); yield return new WaitForSeconds(0.8f);
        e = (GameObject)Instantiate(Enemy3, new Vector3(-3, 0, 18), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        int r = Random.Range(-screenWidth, screenWidth);
        Instantiate(Ammo, new Vector3(r, 0, 18), Quaternion.identity);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }
    
    IEnumerator NormalEnemy6And7Group1()
    {
        GameObject e = (GameObject)Instantiate(Enemy6, new Vector3(-5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(3, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.5f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(5, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-2, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.7f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(1.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-3.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(2.8f);
        e = (GameObject)Instantiate(Enemy7, new Vector3(4.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.5f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-0.5f, 0, 18), Quaternion.identity); yield return new WaitForSeconds(1.8f);
        e = (GameObject)Instantiate(Enemy6, new Vector3(-5, 0, 18), Quaternion.identity);

        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    public void Update()
    { 
        WaveText();
        /*if(shouldBeatTheBoss)
        {
            if(bossIsBeated)
            {
                BossKilled();
                // start explosion
            }
        }*/
    }

    IEnumerator BossKilled()
    {
        yield return new WaitForSeconds(1f);
        currentWave++;
        yield return new WaitForSeconds(waitTimeAfterWaveOfEnemiesAreSpawnedDependsOnEnemiesMoveSpeed);
        StartCoroutine(CreateLevel());
    }

    public void WaveText()
    {
        waveText.text = "Wave " + currentWave + "/" + wavesInThisLevel;
    }

    public void WinText()
    {
        winText.text = "Excellent";
    }

    public void LoseText()
    {
        gameOverText.text = "Game over";
    }

    public void CallCreateLevelCoroutine()
    {
        StartCoroutine(CreateLevel());
    }

    public void ShowWinMoneyText()
    {
        WinMoneyText.text = "Money: " + GM.instance.totalMoney + " + " + GM.instance.moneyInThisLevel + " = " + GM.instance.totalMoney + GM.instance.moneyInThisLevel;
    }

    public void ShowWinPointText() 
    {
        WinPointText.text = "Points: " + GM.instance.totalPoints + " + " + GM.instance.pointsInThisLevel + " = " + GM.instance.totalPoints + GM.instance.pointsInThisLevel;
    }

    public void ShowGameOverMoneyText()
    {
        //GameOverMoneyText.text = "Money: " + GM.instance.totalMoney + " + 0 = " + GM.instance.totalMoney;
    }

    public void ShowGameOverPointText()
    {
        //GameOverMoneyText.text = "Points: " + GM.instance.totalPoints + " + 0 = " + GM.instance.totalPoints;
    }

}
