using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int health = 100;
    public int points = 50;
    public int turretCost;

    public int currentWave = 0;

    public int waveOneLimit;
    public int waveOneAmount;

    public int waveTwoLimit;
    public int waveTwoAmount;

    public int waveThreeLimit;
    public int waveThreeAmount;

    public int waveFourLimit;
    public int waveFourAmount;

    public GameObject enemy;
    public GameObject strongEnemy;

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;

    public bool canSpawn = true;
    public float timer;
    public int enemiesKilled;

    public Text hp;
    public Text pointsText;
    public Text wave;
    public Text enemiesKilledText;
    public GameObject restartButton;

   
    public Camera playerCam;

    public int enemiesInScene = 0;

    public bool waveInProgress = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        SwitchCamera();
        UpdateUI();
        WaveController();
        GameEnd();
        isWaveInProgress();
    }

    private void SwitchCamera()
    {
        Camera[] cams = FindObjectsOfType<Camera>();
        if (Input.GetKeyDown(KeyCode.E))
        {
           

            cams[1].gameObject.SetActive(true);
        }
        
        



    }
    public void Wave1()
    {
        if (waveOneAmount < waveOneLimit && canSpawn == true)
        {
            Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
            waveOneAmount++;
            enemiesInScene++;
            canSpawn = false;
            StartCoroutine(spawnDelay(.6f));

        }
        

    }

    public void Wave2()
    {
        
        if (waveTwoAmount < waveTwoLimit && canSpawn == true)
        {
            
            Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
            Instantiate(enemy, spawnPoint2.transform.position, spawnPoint1.transform.rotation);
            waveTwoAmount += 2;
            enemiesInScene += 2;
            canSpawn = false;
            StartCoroutine(spawnDelay(.5f));

        }
        turretCost = 100;
    }

    public void Wave3()
    {
        if (waveThreeAmount < waveThreeLimit && canSpawn == true)
        {
            Instantiate(strongEnemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
            Instantiate(strongEnemy, spawnPoint2.transform.position, spawnPoint1.transform.rotation);
            waveThreeAmount += 2;
            enemiesInScene += 2;
            canSpawn = false;
            StartCoroutine(spawnDelay(.2f));
        }
    }

    public void Wave4()
    {
        if (waveFourAmount < waveFourLimit && canSpawn == true)
        {
            Instantiate(strongEnemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
            Instantiate(strongEnemy, spawnPoint2.transform.position, spawnPoint1.transform.rotation);
            Instantiate(strongEnemy, spawnPoint3.transform.position, spawnPoint1.transform.rotation);
            waveFourAmount += 3;
            enemiesInScene += 3;
            canSpawn = false;
            StartCoroutine(spawnDelay(.15f));
        }
    }

    public void WaveController()
    {
        if (currentWave == 1)
        {
            Wave1();
        }
        if (currentWave == 2)
        {
            Wave2();
        }
        if (currentWave == 3)
        {
            Wave3();
        }
        if (currentWave == 4)
        {
            Wave4();
        }
    }

    public void isWaveInProgress()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            waveInProgress = true;
        }
        else
        {
            waveInProgress = false;
        }

         
    }

    public void NextWave()
    {
        if (!waveInProgress)
        {
            currentWave++;
        }
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GameEnd()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
            restartButton.SetActive(true);
        }
    }

    public void UpdateUI()
    {
        wave.text = "Wave: " + currentWave.ToString();
        pointsText.text = "Points: " + points.ToString();
        enemiesKilledText.text = "Total Kills: " + enemiesKilled.ToString();
        hp.text = "Health: " + health.ToString();
    }

    IEnumerator spawnDelay(float delay)
    {
       
        yield return new WaitForSeconds(delay);
        canSpawn = true;

    }




}
