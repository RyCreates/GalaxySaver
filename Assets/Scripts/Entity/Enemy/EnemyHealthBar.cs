using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour
{
    public BarsStats healthBarStat;
    public Slider slider;
    public GameObject bigExplosion;
    public GameObject bigExplosionNoSound;
    private LevelManager lm;
    public bool explodeOnlyOnce = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerShot")
        {
            StartCoroutine(Hurt());
        }
    }

    public void BossExplosion()
    {
        if (!explodeOnlyOnce)
        {
            if (bigExplosion != null)
            {
                if (GM.soundIsOn)
                { Instantiate(bigExplosion, this.transform.position, Quaternion.identity); }
            }
            if (bigExplosion != null)
            {
                if (!GM.soundIsOn)
                { Instantiate(bigExplosionNoSound, this.transform.position, Quaternion.identity); }
            }
            GameObject ee = GameObject.FindWithTag("LM");
            if (ee != null)
            {
                lm = ee.GetComponent<LevelManager>();
                lm.currentWave++;
                lm.CallCreateLevelCoroutine();
            }
            explodeOnlyOnce = true;
        }
    }

    public void EnemyUpdateHPBar()
    {
        if (healthBarStat != null)
        {
            healthBarStat.currentValue1 = this.gameObject.GetComponent<Enemy>().currentHP;
            //healthBarStat.currentValue1 = PlayerPrefs.GetInt("HPForThisLevel");
            //healthBarStat.maxValue1 = GM.playerTotalHP;
            healthBarStat.maxValue1 = this.gameObject.GetComponent<Enemy>().totalHP;
            healthBarStat.UpdateValue();
            if (slider != null )
            {
                slider.maxValue = this.gameObject.GetComponent<Enemy>().totalHP;
                slider.value = this.gameObject.GetComponent<Enemy>().currentHP;
            }
        }
    }

    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(0.2f);
        EnemyUpdateHPBar();
    }
}
