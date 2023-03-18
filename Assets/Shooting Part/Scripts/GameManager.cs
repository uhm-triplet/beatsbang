using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerState playerState;
    // public Enemy bossA;
    // public Enemy bossB;
    // public Enemy bossC;
    public Boss boss;
    public float playTime = 754f;
    public int stage = 0;
    public int enemyACnt = 0;
    public int enemyBCnt = 0;
    public int enemyCCnt = 0;
    public int enemyDCnt = 0;

    public GameObject gamePanel;

    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI stageTxt;
    // public TextMeshProUGUI killTxt;
    // public TextMeshProUGUI deathTxt;
    public TextMeshProUGUI scoreTxt;

    public TextMeshProUGUI playerHealthTxt;
    public TextMeshProUGUI playerAmmoTxt;
    // public TextMeshProUGUI playerCoinTxt;

    public TextMeshProUGUI EnemyATxt;
    public TextMeshProUGUI EnemyBTxt;
    public TextMeshProUGUI EnemyCTxt;

    public Image hammerImg;
    public Image handGunImg;
    public Image subMachineGunImg;
    public TextMeshProUGUI weaponAmmoTxt;
    public TextMeshProUGUI grenadeCountTxt;

    public Image bossImg;
    public RectTransform bossHealthGroup;
    public RectTransform bossHealthBar;

    public Transform[] enemyZones;
    public GameObject[] enemies;
    public List<int> enemyList;

    public GameObject gameOverUI;
    public GameObject stageClearUI;

    public GameManagerRhythm rhythmManager;

    void Awake()
    {
        weaponAmmoTxt.text = "   - / -";
        hammerImg.color = new Color(1, 1, 1, 0);
        handGunImg.color = new Color(1, 1, 1, 0);
        subMachineGunImg.color = new Color(1, 1, 1, 0);
        // bossAImg.color = new Color(1, 1, 1, 0);
        // bossBImg.color = new Color(1, 1, 1, 0);
        // bossCImg.color = new Color(1, 1, 1, 0);
        enemyList = new List<int>();

        StartCoroutine(SpawnEnemy());

    }
    void Update()
    {
        if (playTime > 0)
        {
            playTime -= Time.deltaTime;
        }
        else
        {
            playTime = 0;
        }
    }

    IEnumerator SpawnEnemy()
    {

        for (int i = 0; i < 40; i++)
        {
            if (stage == 1)
            {
                enemyList.Add(0);
                enemyACnt++;
            }
            else if (stage == 2)
            {
                int ran = Random.Range(0, 2);
                enemyList.Add(ran);
                switch (ran)
                {
                    case 0:
                        enemyACnt++;
                        break;
                    case 1:
                        enemyBCnt++;
                        break;
                }
            }
            else if (stage == 3)
            {
                int ran = Random.Range(0, 3);
                enemyList.Add(ran);
                switch (ran)
                {
                    case 0:
                        enemyACnt++;
                        break;
                    case 1:
                        enemyBCnt++;
                        break;
                    case 2:
                        enemyCCnt++;
                        break;
                }
            }
            else if (stage == 4 && i <= 20)
            {
                int ran = Random.Range(0, 3);
                enemyList.Add(ran);
                switch (ran)
                {
                    case 0:
                        enemyACnt++;
                        break;
                    case 1:
                        enemyBCnt++;
                        break;
                    case 2:
                        enemyCCnt++;
                        break;
                }
            }

        }

        if (stage == 4)
        {
            enemyDCnt++;
            GameObject instantEnemy = Instantiate(enemies[3], enemyZones[0].position, enemyZones[0].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.target = playerState.transform;
            enemy.gameManager = this;
            boss = instantEnemy.GetComponent<Boss>();
        }

        while (enemyList.Count > 0)
        {
            int ranZone = Random.Range(0, 4);
            GameObject instantEnemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.target = playerState.transform;
            enemy.gameManager = this;
            enemyList.RemoveAt(0);
            yield return new WaitForSeconds(2f);
        }

        while (playTime > 0 || (enemyACnt + enemyBCnt + enemyCCnt + enemyDCnt > 0))
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        StageEnd();

    }

    void StageEnd()
    {
        Debug.Log("Stage Clear");
        PlayerPrefs.SetInt($"Stage{stage}Clear", 1);
        PlayerPrefs.SetInt($"Stage{stage}Score", rhythmManager.score);
        if (PlayerPrefs.GetInt($"Stage{stage}BestScore") < PlayerPrefs.GetInt($"Stage{stage}Score"))
        {
            PlayerPrefs.SetInt($"Stage{stage}BestScore", rhythmManager.score);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        stageClearUI.SetActive(true);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        PlayerPrefs.SetInt($"Stage{stage}Score", rhythmManager.score);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        int minute = (int)(playTime / 60);
        int second = (int)(playTime % 60);
        timerTxt.text = string.Format("{0:00}", minute) + ":" + string.Format("{0:00}", second);
        stageTxt.text = "Stage : " + stage;

        playerHealthTxt.text = playerState.health + " / " + playerState.maxHealth;
        playerAmmoTxt.text = playerState.ammo + " / " + playerState.maxAmmo;
        scoreTxt.text = playerState.score.ToString();

        EnemyATxt.text = enemyACnt.ToString();
        EnemyBTxt.text = enemyBCnt.ToString();
        EnemyCTxt.text = enemyCCnt.ToString();

        grenadeCountTxt.text = playerState.hasGrenades + " / " + playerState.maxHasGrenades;




        // if (stage == 1)
        // {
        //     bossHealthBar.localScale = new Vector3((float)bossA.currentHealth / bossA.maxHealth, 1, 1);
        //     bossAImg.color = new Color(1, 1, 1, 1);
        //     bossBImg.color = new Color(1, 1, 1, 0);
        //     bossCImg.color = new Color(1, 1, 1, 0);
        //     bossDImg.color = new Color(1, 1, 1, 0);
        // }
        // else if (stage == 2)
        // {
        //     bossHealthBar.localScale = new Vector3((float)bossB.currentHealth / bossB.maxHealth, 1, 1);
        //     bossAImg.color = new Color(1, 1, 1, 0);
        //     bossBImg.color = new Color(1, 1, 1, 1);
        //     bossCImg.color = new Color(1, 1, 1, 0);
        //     bossDImg.color = new Color(1, 1, 1, 0);
        // }
        // else if (stage == 3)
        // {
        //     bossHealthBar.localScale = new Vector3((float)bossC.currentHealth / bossC.maxHealth, 1, 1);
        //     bossAImg.color = new Color(1, 1, 1, 0);
        //     bossBImg.color = new Color(1, 1, 1, 0);
        //     bossCImg.color = new Color(1, 1, 1, 1);
        //     bossDImg.color = new Color(1, 1, 1, 0);
        // }
        if (stage == 4)
        {
            // bossImg.color = new Color(1, 1, 1, 1);
            bossHealthGroup.anchoredPosition = Vector3.down * 67;
            bossHealthBar.localScale = new Vector3((float)boss.currentHealth / boss.maxHealth, 1, 1);
        }
        else
        {
            bossHealthGroup.anchoredPosition = Vector3.up * 200;
        }

    }
}
