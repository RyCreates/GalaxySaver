using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuLevelManager : MonoBehaviour
{
    public Button L1;
    public Button L2;
    public Button L3;
    public Button L4;
    public Button L5;
    public Button L6;
    public Button L7;
    public Button L8;
    public Button L9;
    public Button L10;
    public Button L11;
    public Button L12;
    public Button L13;
    public Button L14;
    public Button L15;
    public Button L16;
    public Button L17;
    public Button L18;
    public Button L19;
    public Button L20;

    //locks images
    public Image I1;
    public Image I2;
    public Image I3;
    public Image I4;
    public Image I5;
    public Image I6;
    public Image I7;
    public Image I8;
    public Image I9;
    public Image I10;
    public Image I11;
    public Image I12;
    public Image I13;
    public Image I14;
    public Image I15;
    public Image I16;
    public Image I17;
    public Image I18;
    public Image I19;
    public Image I20;

    void Start()
    {
        L1.gameObject.SetActive(true);
        I1.enabled = false;

        L2.gameObject.SetActive(false);
        I2.enabled = true;

        L3.gameObject.SetActive(false);
        I3.enabled = true;

        L4.gameObject.SetActive(false);
        I4.enabled = true;

        L5.gameObject.SetActive(false);
        I5.enabled = true;

        L6.gameObject.SetActive(false);
        I6.enabled = true;

        L7.gameObject.SetActive(false);
        I7.enabled = true;

        L8.gameObject.SetActive(false);
        I8.enabled = true;

        L9.gameObject.SetActive(false);
        I9.enabled = true;

        L10.gameObject.SetActive(false);
        I10.enabled = true;

        L11.gameObject.SetActive(false);
        I11.enabled = true;

        L12.gameObject.SetActive(false);
        I12.enabled = true;

        L13.gameObject.SetActive(false);
        I13.enabled = true;

        L14.gameObject.SetActive(false);
        I14.enabled = true;

        L15.gameObject.SetActive(false);
        I15.enabled = true;

        L16.gameObject.SetActive(false);
        I16.enabled = true;

        L17.gameObject.SetActive(false);
        I17.enabled = true;

        L18.gameObject.SetActive(false);
        I18.enabled = true;

        L19.gameObject.SetActive(false);
        I19.enabled = true;

        L20.gameObject.SetActive(false);
        I20.enabled = true;
        
        L1.gameObject.SetActive(true);
        I1.enabled = false;

        if (PlayerPrefs.GetInt("L02Open") == 1)
        {
            L2.gameObject.SetActive(true);
            I2.enabled = false;
        }
        if (PlayerPrefs.GetInt("L03Open") == 1)
        {
            L3.gameObject.SetActive(true);
            I3.enabled = false;
        }
        if (PlayerPrefs.GetInt("L04Open") == 1)
        {
            L4.gameObject.SetActive(true);
            I4.enabled = false;
        }
        if (PlayerPrefs.GetInt("L05Open") == 1)
        {
            L5.gameObject.SetActive(true);
            I5.enabled = false;
        }
        if (PlayerPrefs.GetInt("L06Open") == 1)
        {
            L6.gameObject.SetActive(true);
            I6.enabled = false;
        }
        if (PlayerPrefs.GetInt("L07Open") == 1)
        {
            L7.gameObject.SetActive(true);
            I7.enabled = false;
        }
        if (PlayerPrefs.GetInt("L08Open") == 1)
        {
            L8.gameObject.SetActive(true);
            I8.enabled = false;
        }
        if (PlayerPrefs.GetInt("L09Open") == 1)
        {
            L9.gameObject.SetActive(true);
            I9.enabled = false;
        }
        if (PlayerPrefs.GetInt("L10Open") == 1)
        {
            L10.gameObject.SetActive(true);
            I10.enabled = false;
        }
        if (PlayerPrefs.GetInt("L11Open") == 1)
        {
            L11.gameObject.SetActive(true);
            I11.enabled = false;
        }
        if (PlayerPrefs.GetInt("L12Open") == 1)
        {
            L12.gameObject.SetActive(true);
            I12.enabled = false;
        }
        if (PlayerPrefs.GetInt("L13Open") == 1)
        {
            L13.gameObject.SetActive(true);
            I13.enabled = false;
        }
        if (PlayerPrefs.GetInt("L14Open") == 1)
        {
            L14.gameObject.SetActive(true);
            I14.enabled = false;
        }
        if (PlayerPrefs.GetInt("L15Open") == 1)
        {
            L15.gameObject.SetActive(true);
            I15.enabled = false;
        }
        if (PlayerPrefs.GetInt("L16Open") == 1)
        {
            L16.gameObject.SetActive(true);
            I16.enabled = false;
        }
        if (PlayerPrefs.GetInt("L17Open") == 1)
        {
            L17.gameObject.SetActive(true);
            I17.enabled = false;
        }
        if (PlayerPrefs.GetInt("L18Open") == 1)
        {
            L18.gameObject.SetActive(true);
            I18.enabled = false;
        }
        if (PlayerPrefs.GetInt("L19Open") == 1)
        {
            L19.gameObject.SetActive(true);
            I19.enabled = false;
        }
        if (PlayerPrefs.GetInt("L20Open") == 1)
        {
            L20.gameObject.SetActive(true);
            I20.enabled = false;
        }
        //OpenAll();
        //Nunulinimas();
    }

    void Nunulinimas()
    {
        PlayerPrefs.SetInt("L01Open",1); PlayerPrefs.SetInt("L02Open", 0); PlayerPrefs.SetInt("L03Open", 0); PlayerPrefs.SetInt("L04Open", 0);
        PlayerPrefs.SetInt("L05Open", 0); PlayerPrefs.SetInt("L06Open", 0); PlayerPrefs.SetInt("L07Open", 0); PlayerPrefs.SetInt("L08Open",0);
        PlayerPrefs.SetInt("L09Open", 0); PlayerPrefs.SetInt("L10Open", 0); PlayerPrefs.SetInt("L11Open", 0); PlayerPrefs.SetInt("L12Open",0);
        PlayerPrefs.SetInt("L13Open", 0); PlayerPrefs.SetInt("L14Open", 0); PlayerPrefs.SetInt("L15Open", 0); PlayerPrefs.SetInt("L16Open",0);
        PlayerPrefs.SetInt("L17Open", 0); PlayerPrefs.SetInt("L18Open", 0); PlayerPrefs.SetInt("L19Open", 0); PlayerPrefs.SetInt("L20Open", 0);
        PlayerPrefs.SetInt("TotalPoints",0);
        PlayerPrefs.SetInt("TotalMoney",0);
        PlayerPrefs.SetInt("PermanentShotStrength",1);
        PlayerPrefs.SetInt("PermanentPlayerHP",100);
    }

    void OpenAll()
    {
        PlayerPrefs.SetInt("L01Open", 1); PlayerPrefs.SetInt("L02Open", 1); PlayerPrefs.SetInt("L03Open", 1); PlayerPrefs.SetInt("L04Open", 1);
        PlayerPrefs.SetInt("L05Open", 1); PlayerPrefs.SetInt("L06Open", 1); PlayerPrefs.SetInt("L07Open", 1); PlayerPrefs.SetInt("L08Open", 1);
        PlayerPrefs.SetInt("L09Open", 1); PlayerPrefs.SetInt("L10Open", 1); PlayerPrefs.SetInt("L11Open", 1); PlayerPrefs.SetInt("L12Open", 1);
        PlayerPrefs.SetInt("L13Open", 1); PlayerPrefs.SetInt("L14Open", 1); PlayerPrefs.SetInt("L15Open", 1); PlayerPrefs.SetInt("L16Open", 1);
        PlayerPrefs.SetInt("L17Open", 1); PlayerPrefs.SetInt("L18Open", 1); PlayerPrefs.SetInt("L19Open", 1); PlayerPrefs.SetInt("L20Open", 1);
        PlayerPrefs.SetInt("TotalPoints", 1000);
        PlayerPrefs.SetInt("TotalMoney", 1000);
    }
}
