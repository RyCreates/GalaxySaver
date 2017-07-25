using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public Canvas MainMenuLevelManagerCanvas;
    public Canvas SettingsCanvas;
    public Canvas ShopCanvas;
    public Canvas L14ShopCanvas;
    public Canvas ExitCanvas;

    public Button SettingsButton;
    public Button ShopButton;

    public Button SettingsSoundOnButton;
    public Button SettingsSoundOffButton;
    public Button SettingsVibeOnButton;
    public Button SettingVibeOffButton;
    public Button SettingsOkButton;

    public Text SettingsSoundOnOffText;
    public Text SettingsVibeOnOffText;
    public Text ShopPointsText;
    public Text ShopMoneyText;
    public Text ShopHealthText;
    public Text ShopShotText;
    public Text L14ShopMoneyText;
    public Text L14ShopPointsText;
    public Text L14ShopHealthText;
    public Text L14ShopShotText;

    public bool isExitCanvasOpen = false;
    public bool isMainMenuCanvasOpen = true;
    public bool isShopCanvasOpen = false;
    public bool isL14ShopCanvasOpen = false;
    public bool isSettingsCanvasOpen = false;

    /*private float timerForExitButton = 2;
    private float timerCounter;*/
    
    void Start()
    {
        /*MainMenuLevelManagerCanvas.enabled = true;
        SettingsCanvas.enabled = false;
        ShopCanvas.enabled = false;
        ExitCanvas.enabled = false;*/

        isMainMenuCanvasOpen = true;
        isShopCanvasOpen = false;
        isSettingsCanvasOpen = false;
        isExitCanvasOpen = false;
        isL14ShopCanvasOpen = false;

        ChangeVibeOnOffText(PlayerPrefs.GetInt("Vibe"));
        ChangeSoundOnOffText(PlayerPrefs.GetInt("Sound"));
    }
    
    void Update()
    {
        if (isMainMenuCanvasOpen) { MainMenuLevelManagerCanvas.enabled = true; }
        else if (!isMainMenuCanvasOpen) { MainMenuLevelManagerCanvas.enabled = false; }
        if (isSettingsCanvasOpen) { SettingsCanvas.enabled = true; }
        else if(!isSettingsCanvasOpen) { SettingsCanvas.enabled = false; }
        if (isShopCanvasOpen) { ShopCanvas.enabled = true; }
        else if (!isShopCanvasOpen) { ShopCanvas.enabled = false; }
        if (isExitCanvasOpen) { ExitCanvas.enabled = true; }
        else if (!isExitCanvasOpen) { ExitCanvas.enabled = false; }
        if (isL14ShopCanvasOpen) { L14ShopCanvas.enabled = true; }
        else if (!isL14ShopCanvasOpen) { L14ShopCanvas.enabled = false; }

        if (Input.GetKeyDown(KeyCode.Escape) /*&& timerCounter<Time.time*/)
        {
            //timerCounter += timerForExitButton;
            if (Application.loadedLevelName == "MainMenu")
            {
                if(isMainMenuCanvasOpen)
                {
                    isExitCanvasOpen = true;
                    isMainMenuCanvasOpen = false;
                }
                else if (isSettingsCanvasOpen)
                {
                    isMainMenuCanvasOpen = true;
                    isSettingsCanvasOpen = false;
                }
                else if (isShopCanvasOpen)
                {
                    isMainMenuCanvasOpen = true;
                    isShopCanvasOpen = false;
                }
                else if(isL14ShopCanvasOpen)
                {
                    isL14ShopCanvasOpen = false;
                    isMainMenuCanvasOpen = true;
                }
                else if (isExitCanvasOpen)
                {
                    isExitCanvasOpen = false;
                    isMainMenuCanvasOpen = true;
                }
            }
        }
    }

    public void MainMenuButtonExitNo()
    {
        /*MainMenuLevelManagerCanvas.enabled = true;
        SettingsCanvas.enabled = false;
        ShopCanvas.enabled = false;
        ExitCanvas.enabled = false;*/
        isMainMenuCanvasOpen = true;
        isShopCanvasOpen = false;
        isSettingsCanvasOpen = false;
        isExitCanvasOpen = false;
        isL14ShopCanvasOpen = false;
    }

    public void MainMenuButtonExitYes()
    {
        GM.instance.QuitGame();
    }

    public void MainMenuButtonShop()
    {
        /*MainMenuLevelManagerCanvas.enabled = false;
        SettingsCanvas.enabled = false;
        ShopCanvas.enabled = true;
        ExitCanvas.enabled = false;*/
        isMainMenuCanvasOpen = false;
        isShopCanvasOpen = true;
        isL14ShopCanvasOpen = false;
        isSettingsCanvasOpen = false;
        isExitCanvasOpen = false;
        ChangeShopMoneyText();
        ChangeShopPointsText();
        ChangeShopHealthText();
        ChangeShopShotText();
    }

    public void MainMenuButtonSettings()
    {
        /*MainMenuLevelManagerCanvas.enabled = false;
        SettingsCanvas.enabled = true;
        ShopCanvas.enabled = false;
        ExitCanvas.enabled = false;*/
        isMainMenuCanvasOpen = false;
        isShopCanvasOpen = false;
        isL14ShopCanvasOpen = false;
        isSettingsCanvasOpen = true;
        isExitCanvasOpen = false;
    }

    public void MainMenuButtonShopOk()
    {
        /*MainMenuLevelManagerCanvas.enabled = true;
        SettingsCanvas.enabled = false;
        ShopCanvas.enabled = false;
        ExitCanvas.enabled = false;*/

        isMainMenuCanvasOpen = true;
        isShopCanvasOpen = false;
        isL14ShopCanvasOpen = false;
        isSettingsCanvasOpen = false;
        isExitCanvasOpen = false;
    }

    public void MainMenuButtonSettingsOk()
    {
        /*MainMenuLevelManagerCanvas.enabled = true;
        SettingsCanvas.enabled = false;
        ShopCanvas.enabled = false;
        ExitCanvas.enabled = false;*/

        isMainMenuCanvasOpen = true;
        isShopCanvasOpen = false;
        isL14ShopCanvasOpen = false;
        isSettingsCanvasOpen = false;
        isExitCanvasOpen = false;
    }

    public void MainMenuButtonSettingsVibeOn()
    {
        PlayerPrefs.SetInt("Vibe",1);
        GM.vibrationIsOn = true;
        ChangeVibeOnOffText(1);
    }

    public void MainMenuButtonSettingsVibeOff()
    {
        PlayerPrefs.SetInt("Vibe", 0);
        GM.vibrationIsOn = false;
        ChangeVibeOnOffText(0);
    }

    public void MainMenuButtonSettingsSoundOn()
    {
        PlayerPrefs.SetInt("Sound", 1);
        GM.soundIsOn = true;
        ChangeSoundOnOffText(1);
    }

    public void MainMenuButtonSettingsSoundOff()
    {
        PlayerPrefs.SetInt("Sound", 0);
        GM.soundIsOn = false;
        ChangeSoundOnOffText(0);
    }

    public void MainMenuButtonShopBuyHpForPoints()
    {
        if (GM.instance.totalPoints >= 200)
        {
            GM.instance.totalPoints -= 200; PlayerPrefs.SetInt("HPForThisLevel", PlayerPrefs.GetInt("HPForThisLevel") + 10);
            ChangeShopPointsText();
            ChangeShopHealthText();
        }
    }

    public void MainMenuButtonShopBuyHpForMoney()
    {
        if(GM.instance.totalMoney >= 10)
        {
            GM.instance.totalMoney -= 10; PlayerPrefs.SetInt("HPForThisLevel", PlayerPrefs.GetInt("HPForThisLevel") + 10);
            ChangeShopHealthText();
            ChangeShopMoneyText();
        }
    }

    public void MainMenuButtonL14ShopBuyHpForMoney()
    {
        if (GM.instance.totalMoney >= 500)
        {
            GM.instance.totalMoney -= 500; PlayerPrefs.SetInt("HPForThisLevel", PlayerPrefs.GetInt("HPForThisLevel") + 10);
            PlayerPrefs.SetInt("PermanentPlayerHP", PlayerPrefs.GetInt("PermanentPlayerHP") + 10);
            ChangeL14ShopHealthText();
            ChangeL14ShopMoneyText(); 
        }
    }

    public void MainMenuButtonShopBuyShotForPoints()
    {
        if (GM.instance.totalPoints >= 200 && PlayerPrefs.GetInt("ShotForThisLevel") < 12)
        {
            GM.instance.totalPoints -= 200; PlayerPrefs.SetInt("ShotForThisLevel", PlayerPrefs.GetInt("ShotForThisLevel") + 1);
            ChangeShopPointsText();
            ChangeShopShotText();
        }
    }

    public void MainMenuButtonShopBuyShotForMoney()
    {
        if (GM.instance.totalMoney >= 10 && PlayerPrefs.GetInt("ShotForThisLevel") < 12)
        {
            GM.instance.totalMoney -= 10; PlayerPrefs.SetInt("ShotForThisLevel", PlayerPrefs.GetInt("ShotForThisLevel") + 1);
            ChangeShopShotText();
            ChangeShopMoneyText();
        }
    }

    public void MainMenuButtonL14ShopBuyShotForMoney()
    {
        if (GM.instance.totalMoney >= 500 && PlayerPrefs.GetInt("PermanentShotStrength") < 12)
        {
            GM.instance.totalMoney -= 500; PlayerPrefs.SetInt("ShotForThisLevel", PlayerPrefs.GetInt("ShotForThisLevel") + 1);
            PlayerPrefs.SetInt("PermanentShotStrength", PlayerPrefs.GetInt("PermanentShotStrength") + 1);
            if (PlayerPrefs.GetInt("ShotForThisLevel") > 12) { PlayerPrefs.SetInt("ShotForThisLevel",12); }

            ChangeL14ShopShotText();
            ChangeL14ShopMoneyText();
        }
    }

    public void ButtonStartL01()
    {
        PlayerPrefs.SetInt("OpenLevel", 01);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL02()
    {
        PlayerPrefs.SetInt("OpenLevel", 02);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL03()
    {
        PlayerPrefs.SetInt("OpenLevel", 03);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL04()
    {
        PlayerPrefs.SetInt("OpenLevel", 04);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL05()
    {
        PlayerPrefs.SetInt("OpenLevel", 05);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL06()
    {
        PlayerPrefs.SetInt("OpenLevel", 06);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL07()
    {
        PlayerPrefs.SetInt("OpenLevel", 07);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL08()
    {
        PlayerPrefs.SetInt("OpenLevel", 08);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL09()
    {
        PlayerPrefs.SetInt("OpenLevel", 09);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL10()
    {
        PlayerPrefs.SetInt("OpenLevel",10);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL11()
    {
        PlayerPrefs.SetInt("OpenLevel", 11);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL12()
    {
        PlayerPrefs.SetInt("OpenLevel", 12);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL13()
    {
        PlayerPrefs.SetInt("OpenLevel", 13);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL14()
    {
        //PlayerPrefs.SetInt("OpenLevel", 14);
        //SceneManager.LoadScene("Game");
        isL14ShopCanvasOpen = true;
        isMainMenuCanvasOpen = false;
        isShopCanvasOpen = false;
        isSettingsCanvasOpen = false;
        isExitCanvasOpen = false;
        ChangeL14ShopShotText();
        ChangeL14ShopHealthText();
        ChangeL14ShopPointsText();
        ChangeL14ShopMoneyText();
    }
    public void ButtonStartL15()
    {
        PlayerPrefs.SetInt("OpenLevel",15);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL16()
    {
        PlayerPrefs.SetInt("OpenLevel", 16);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL17()
    {
        PlayerPrefs.SetInt("OpenLevel", 17);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL18()
    {
        PlayerPrefs.SetInt("OpenLevel", 18);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL19()
    {
        PlayerPrefs.SetInt("OpenLevel", 19);
        SceneManager.LoadScene("Game");
    }
    public void ButtonStartL20()
    {
        PlayerPrefs.SetInt("OpenLevel", 20);
        SceneManager.LoadScene("Game");
    }

    public void ChangeSoundOnOffText(int a)
    {
        if(a==0)
        {
            SettingsSoundOnOffText.text = "Off";
        }
        else
        {
            SettingsSoundOnOffText.text = "On";
        }
    }

    public void ChangeVibeOnOffText(int a)
    {
        if (a == 0)
        {
            SettingsVibeOnOffText.text = "Off";
        }
        else
        {
            SettingsVibeOnOffText.text = "On";
        }
    }

    public void ChangeShopMoneyText()
    {
        ShopMoneyText.text = "Money: " + GM.instance.totalMoney;
    }

    public void ChangeShopPointsText()
    {
        ShopPointsText.text = "Points: " + GM.instance.totalPoints;
    }

    public void ChangeL14ShopMoneyText()
    {
        L14ShopMoneyText.text = "Money: " + GM.instance.totalMoney;
    }

    public void ChangeL14ShopPointsText() 
    {
        L14ShopPointsText.text = "Points: " + GM.instance.totalPoints;
    }

    public void ChangeShopHealthText()
    {
        ShopHealthText.text = "Health: " + PlayerPrefs.GetInt("HPForThisLevel");
    }

    public void ChangeShopShotText()
    {
        ShopShotText.text = "Shot strength: " + PlayerPrefs.GetInt("ShotForThisLevel");
    }

    public void ChangeL14ShopHealthText()
    {
        L14ShopHealthText.text = "Health: " + PlayerPrefs.GetInt("PermanentPlayerHP");
    }

    public void ChangeL14ShopShotText()
    {
        L14ShopShotText.text = "Shot strength: " + PlayerPrefs.GetInt("PermanentShotStrength");
    }
}
