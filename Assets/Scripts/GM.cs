using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static GM instance;
    private LevelManager LM;
    private Player player;

    public static int playerTotalHP = 100;
    public static int playerCurrentHP;
    private static int playerTotalShield = 100;
    private static int playerCurrentShield;
    public int playerShotStrenght = 1;

    public static bool soundIsOn;
    public static bool vibrationIsOn;

    public int totalMoney;
    public int totalPoints;
    public int moneyInThisLevel;
    public int pointsInThisLevel;
    public int level;

    public GameObject playerExplosion;
    public GameObject playerExplosionNoSound;
    public GameObject enemyExplosion;
    public GameObject asteroidExplosion;

    public enum GameLevel
    {
        Level01,
        Level02,
        Level03,
        Level04,
        Level05,
        Level06,
        Level07,
        Level08,
        Level09,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
        Level15,
        Level16,
        Level17,
        Level18,
        Level19,
        Level20
    }

    public GameLevel gameLevel;

    public Canvas GMCanvas;
    public Canvas PauseCanvas;
    public Canvas GameOverCanvas;
    public Canvas WinCanvas;
    /*public Canvas LoseCanvas;*/

    public Text GameOverText;
    public Text MoneyText;
    public Text PointsText;
    public Text LevelText;
    public Text PowerText;

    public Text GameOverMoneyText;
    public Text GameOverPointsText;
    public Text WinMoneyText;
    public Text WinPointText;


    public Text gameOverText;
    public Text winText;

    /* public Image Narate1;
     public Image Narate2;
     public Image Narate3;*/

    public BarsStats healthBarStat;
    public Text playerHealthText;

    public Button GoToNextLevelButton;

    public bool isPaused = false;

    public bool isWinCanvasOpen = false;
    public bool isGameOverCanvasOpen = false;

    public void ResetGame()
    {
        StartCoroutine(WaiterMethod());
    }

    IEnumerator WaiterMethod()
    {
        yield return new WaitForSeconds(0.1f);
        if (Application.loadedLevelName == "Game") 
        {
            playerCurrentHP = playerTotalHP;
            playerCurrentShield = playerTotalShield;
            //SetWhichLevelAndStartGame();
            GMCanvas.enabled = true;
            PauseCanvas.enabled = false;
            GameOverText.enabled = false;
            GameOverCanvas.enabled = false;
            WinCanvas.enabled = false;
            GMUpdateHPBar(); UpdatePlayerHPText(); ShowLevelText();
        }
        else if (Application.loadedLevelName == "MainMenu")
        {
            GameOverText.enabled = false;
            GMCanvas.enabled = false;
            PauseCanvas.enabled = false;
            GameOverCanvas.enabled = false;
            WinCanvas.enabled = false;
        }
    }

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        GetSoundAndVibeBools();
        //playerCurrentHP = playerTotalHP;
        GetHPAndShotForThisLevel();
        playerCurrentShield = playerTotalShield;
        SetWhichLevelAndStartGame();
        GameOverText.enabled = false;
        totalPoints = PlayerPrefs.GetInt("TotalPoints");
        totalMoney = PlayerPrefs.GetInt("TotalMoney");

        //PlayerPrefs.SetInt("TotalPoints",0);
        //PlayerPrefs.SetInt("TotalMoney",0);


        Time.timeScale = 1;
        // just to test
        //totalPoints = 2222;
        //totalMoney = 2222;
        GameOverText.enabled = false;
        GMCanvas.enabled = false;
        PauseCanvas.enabled = false;
        GameOverCanvas.enabled = false;
        WinCanvas.enabled = false;
        isWinCanvasOpen = false;
        isGameOverCanvasOpen = false;

    }

    void GetSoundAndVibeBools()
    {
        int vibe = PlayerPrefs.GetInt("Vibe");
        if (vibe == 0) { vibrationIsOn = false; }else if (vibe == 1) { vibrationIsOn = true; }
        int sound = PlayerPrefs.GetInt("Sound");
        if (sound == 0) { soundIsOn = false; } else if (sound == 1) { soundIsOn = true; }
    }

    void SetWhichLevelAndStartGame()
    {
        int whichLevel = PlayerPrefs.GetInt("OpenLevel");

        if (whichLevel == 1) { gameLevel = GameLevel.Level01; }
        else if (whichLevel == 2) { gameLevel = GameLevel.Level02; }
        else if (whichLevel == 3) { gameLevel = GameLevel.Level03; }
        else if (whichLevel == 4) { gameLevel = GameLevel.Level04; }
        else if (whichLevel == 5) { gameLevel = GameLevel.Level05; }
        else if (whichLevel == 6) { gameLevel = GameLevel.Level06; }
        else if (whichLevel == 7) { gameLevel = GameLevel.Level07; }
        else if (whichLevel == 8) { gameLevel = GameLevel.Level08; }
        else if (whichLevel == 9) { gameLevel = GameLevel.Level09; }
        else if (whichLevel == 10) { gameLevel = GameLevel.Level10; }
        else if (whichLevel == 11) { gameLevel = GameLevel.Level11; }
        else if (whichLevel == 12) { gameLevel = GameLevel.Level12; }
        else if (whichLevel == 13) { gameLevel = GameLevel.Level13; }
        else if (whichLevel == 14) { gameLevel = GameLevel.Level14; }
        else if (whichLevel == 15) { gameLevel = GameLevel.Level15; }
        else if (whichLevel == 16) { gameLevel = GameLevel.Level16; }
        else if (whichLevel == 17) { gameLevel = GameLevel.Level17; }
        else if (whichLevel == 18) { gameLevel = GameLevel.Level18; }
        else if (whichLevel == 19) { gameLevel = GameLevel.Level19; }
        else if (whichLevel == 20) { gameLevel = GameLevel.Level20; }

        level = whichLevel;
        if (Application.loadedLevelName == "Game") { CreateLevel(); }
        if (Application.loadedLevelName == "MainMenu") { GMCanvas.enabled = false; }
    }
	
    void CreateLevel()
    {
        GameObject lm = GameObject.FindWithTag("LM");
        if (lm != null) { LM = lm.GetComponent<LevelManager>(); }
        if(gameLevel==GameLevel.Level01){ LM.SetLevel(1); }
        else if (gameLevel == GameLevel.Level02) { LM.SetLevel(2); }
        else if (gameLevel == GameLevel.Level03) { LM.SetLevel(3); }
        else if (gameLevel == GameLevel.Level04) { LM.SetLevel(4); }
        else if (gameLevel == GameLevel.Level05) { LM.SetLevel(5); }
        else if (gameLevel == GameLevel.Level06) { LM.SetLevel(6); }
        else if (gameLevel == GameLevel.Level07) { LM.SetLevel(7); }
        else if (gameLevel == GameLevel.Level08) { LM.SetLevel(8); }
        else if (gameLevel == GameLevel.Level09) { LM.SetLevel(9); }
        else if (gameLevel == GameLevel.Level10) { LM.SetLevel(10); }
        else if (gameLevel == GameLevel.Level11) { LM.SetLevel(11); }
        else if (gameLevel == GameLevel.Level12) { LM.SetLevel(12); }
        else if (gameLevel == GameLevel.Level13) { LM.SetLevel(13); }
        else if (gameLevel == GameLevel.Level14) { LM.SetLevel(14); }
        else if (gameLevel == GameLevel.Level15) { LM.SetLevel(15); }
        else if (gameLevel == GameLevel.Level16) { LM.SetLevel(16); }
        else if (gameLevel == GameLevel.Level17) { LM.SetLevel(17); }
        else if (gameLevel == GameLevel.Level18) { LM.SetLevel(18); }
        else if (gameLevel == GameLevel.Level19) { LM.SetLevel(19); }
        else if (gameLevel == GameLevel.Level20) { LM.SetLevel(20); }
        GM.instance.playerShotStrenght = 1;
        GM.instance.playerShotStrenght = PlayerPrefs.GetInt("ShotForThisLevel");
    }
    
	void Start ()
    {
        GetHPandShot();
        winText.enabled = false;
        gameOverText.enabled = false;
        GoToNextLevelButton.enabled = true;
    }

    public void GetHPAndShotForThisLevel()
    {
        playerShotStrenght = PlayerPrefs.GetInt("ShotForThisLevel");
        playerTotalHP = PlayerPrefs.GetInt("HPForThisLevel");

        // to fix permanent
        if (PlayerPrefs.GetInt("PermanentShotStrength") < 1) { PlayerPrefs.SetInt("PermanentShotStrength", 1); }
        if (PlayerPrefs.GetInt("PermanentPlayerHP") < 100) { PlayerPrefs.SetInt("PermanentPlayerHP", 100); }

    }

    public void GetHPandShot()
    {
        playerShotStrenght = PlayerPrefs.GetInt("PermanentShotStrength");
        playerTotalHP = PlayerPrefs.GetInt("PermanentPlayerHP");
        PlayerPrefs.SetInt("ShotForThisLevel", PlayerPrefs.GetInt("PermanentShotStrength"));
        PlayerPrefs.SetInt("HPForThisLevel", PlayerPrefs.GetInt("PermanentPlayerHP"));
        if (playerTotalHP < 100) { playerTotalHP = 100; PlayerPrefs.SetInt("HPForThisLevel", 100); }
        if (playerShotStrenght < 1) { playerShotStrenght = 1; PlayerPrefs.SetInt("ShotForThisLevel", 1); }
    }

    void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {

            if (Application.loadedLevelName == "Game")
            {                
                if (isWinCanvasOpen == true || isGameOverCanvasOpen == true)
                { return; }

                // should return if gameOvercanvas enabled

                if (isPaused)
                {
                    isPaused = false;
                    GMCanvas.enabled = true;
                    PauseCanvas.enabled = false;
                    WinCanvas.enabled = false;
                    Time.timeScale = 1;
                }
                else if (!isPaused)
                {
                    isPaused = true;
                    GMCanvas.enabled = false;
                    PauseCanvas.enabled = true;
                    WinCanvas.enabled = false;
                    Time.timeScale = 0f;
                }
            }
            /*else if(Application.loadedLevelName == "MainMenu")
            {
                QuitGame();
            }*/
        }
        if(Application.loadedLevelName == "Game")
        {
            ShowLevelText();
            ShowMoneyText();
            ShowPointsText();
            ShowPowerText();
        }
	}

    public void QuitGameScene()
    {
        PlayerPrefs.SetInt("ShotForThisLevel", PlayerPrefs.GetInt("PermanentShotStrength"));
        PlayerPrefs.SetInt("HPForThisLevel", PlayerPrefs.GetInt("PermanentPlayerHP"));
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("TotalMoney", totalMoney);
        PlayerPrefs.SetInt("TotalPoints", totalPoints);
        SetSoundAndVibeBools();
        Application.Quit();
    }

    void SetSoundAndVibeBools()
    {
        if (vibrationIsOn == false) { PlayerPrefs.SetInt("Vibe", 0); }
        else if (vibrationIsOn == true) { PlayerPrefs.SetInt("Vibe", 1); } 
        if(soundIsOn == false) { PlayerPrefs.SetInt("Sound", 0); }
        else if (soundIsOn == true) { PlayerPrefs.SetInt("Sound", 1); } 
    }

    public void UpdatePlayerHP(int hp)
    {
        playerCurrentHP += hp;
        if (playerCurrentHP <= 0) { playerCurrentHP = 0; GameOver();
            moneyInThisLevel = 0; pointsInThisLevel = 0;
        }
        if (playerCurrentHP >= playerTotalHP) { playerCurrentHP = playerTotalHP; }
        UpdatePlayerHPText(); GMUpdateHPBar();
    }

    public void UpdatePlayerHPText()
    {
        playerHealthText.text = "" + playerCurrentHP + "/" + playerTotalHP /*+ " %"*/;
    }

    public void GMUpdateHPBar()
    {
        if (healthBarStat != null)
        {
            healthBarStat.currentValue1 = GM.playerCurrentHP;
            //healthBarStat.currentValue1 = PlayerPrefs.GetInt("HPForThisLevel");
            //healthBarStat.maxValue1 = GM.playerTotalHP;
            healthBarStat.maxValue1 = PlayerPrefs.GetInt("HPForThisLevel");
            healthBarStat.UpdateValue();
        }
    }

    public void GameOver()
    {
        gameOverText.enabled = true;
        //GameOverText.enabled = true;
        WinCanvas.enabled = false;
        isGameOverCanvasOpen = true;
        GameObject pl = GameObject.FindWithTag("Player");
        if (pl != null) { player = pl.GetComponent<Player>(); }
        if (soundIsOn)
        { Instantiate(playerExplosion, player.transform.position, Quaternion.identity); }
        if (!soundIsOn)
        { Instantiate(playerExplosionNoSound, player.transform.position, Quaternion.identity); }
        Destroy(player.gameObject);
        StartCoroutine(GameOverCoroutine());
        GetHPandShot(); 
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2f);
        //SceneManager.LoadScene("MainMenu");
        GameOverCanvas.enabled = true;
        gameOverText.enabled = false;
        ShowGameOverMoneyText();
        ShowGameOverPointText();
        Time.timeScale = 0;
    }

    public void ShowMoneyText()
    {
        MoneyText.text = "Money \n  " + moneyInThisLevel ;
    }

    public void ShowPointsText()
    {
        PointsText.text = "Points \n  " + pointsInThisLevel;
    }

    public void ShowLevelText()
    {
        LevelText.text = "Level \n  " + level;
    }

    public void ShowPowerText()
    {
        PowerText.text = "Power \n  " + playerShotStrenght;
    }

    public void ButtonGamePausedExit()
    {
        moneyInThisLevel = 0; pointsInThisLevel = 0;
        QuitGameScene();
        SceneManager.LoadScene("MainMenu");
        isWinCanvasOpen = false;
        isGameOverCanvasOpen = false;
        WinCanvas.enabled = false;
    }

    public void ButtonGamePausedResume()
    {
        isPaused = false;
        GMCanvas.enabled = true;
        PauseCanvas.enabled = false;
        WinCanvas.enabled = false;
        Time.timeScale = 1;
        isWinCanvasOpen = false;
        isGameOverCanvasOpen = false;
    }

    public void ButtonGamePausedRestart()
    {
        PauseCanvas.enabled = false;
        GMCanvas.enabled = true;
        WinCanvas.enabled = false;
        moneyInThisLevel = 0; pointsInThisLevel = 0;
        isPaused = false;
        Time.timeScale = 1;
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
        //SceneManager.LoadScene("Game");
        isWinCanvasOpen = false;
        isGameOverCanvasOpen = false;
        QuitGameScene();
    }

    public void ButtonGameNextLevel()
    {        
        /*PauseCanvas.enabled = false;
        GMCanvas.enabled = true;
        WinCanvas.enabled = false;
        moneyInThisLevel = 0; pointsInThisLevel = 0;
        isPaused = false;
        Time.timeScale = 1;

        GameObject lm = GameObject.FindWithTag("LM");
        if (lm != null) { LM = lm.GetComponent<LevelManager>(); }
        if (gameLevel == GameLevel.Level01) { LM.SetLevel(level++); }

        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
        //SceneManager.LoadScene("Game");
        isWinCanvasOpen = false;
        isGameOverCanvasOpen = false;
        QuitGameScene();*/

        PlayerPrefs.SetInt("OpenLevel", level++);
        SceneManager.LoadScene("Game");
    }

    public void ShowGameOverMoneyText()
    {
        GameOverMoneyText.text = "Money: " + GM.instance.totalMoney + " + 0 = " + GM.instance.totalMoney;
    }

    public void ShowGameOverPointText()
    {
        GameOverPointsText.text = "Points: " + GM.instance.totalPoints + " + 0 = " + GM.instance.totalPoints;
    }


    public void ShowWinMoneyText()
    {
        int a = GM.instance.totalMoney + GM.instance.moneyInThisLevel;
        WinMoneyText.text = "Money: " + GM.instance.totalMoney + " + " + GM.instance.moneyInThisLevel + " = " + a;
    }

    public void ShowWinPointText()
    {
        int b = GM.instance.totalPoints + GM.instance.pointsInThisLevel;
        WinPointText.text = "Points: " + GM.instance.totalPoints + " + " + GM.instance.pointsInThisLevel + " = " +b;
    }

    public void WinText()
    {
        winText.text = "Excellent";
    }

    public void LoseText()
    {
        gameOverText.text = "Game over";
    }

}
