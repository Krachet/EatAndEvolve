using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Player player;
    [SerializeField] public Boss boss;
    //spawn props
    public GameObject breakableObjectsPf;
    public GameObject mushroomPf;
    public int mushroomCount;
    public Transform[] spawnPoints;
    public GameObject spawnObjParent;
    public List<GameObject> spawnedBreakableObj = new List<GameObject>();
    public List<GameObject> drops = new List<GameObject>();

    //spawn enemy
    public GameObject enemyPf;
    public Transform[] enemySpawnPoints;
    public List<Transform> emptyPos = new List<Transform>();
    public int enemyAmount;
    public List<GameObject> spawnedEnemy = new List<GameObject>();

    public GameObject bossPf;
    public bool isFinalBoss;

    //UI
    [SerializeField] GameObject countdown;
    public MainMenu mainMenu;
    public GameObject joystick;

    //Camera
    public CameraFollow cameraFollow;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        if (spawnedBreakableObj.Count > 0)
        {
            spawnedBreakableObj.Clear();
        }
        if (spawnedEnemy.Count > 0)
        {
            spawnedEnemy.Clear();
        }
        if (emptyPos.Count > 0)
        {
            emptyPos.Clear();
        }
    }

    private void Start()
    {
        boss = FindAnyObjectByType<Boss>();
    }

    public void OnInit()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject obj = Instantiate(breakableObjectsPf, spawnPoints[i].position, Quaternion.identity);
            obj.transform.GetComponentInChildren<BreakableObjects>().OnInit();
            obj.transform.SetParent(spawnObjParent.transform);  
        }
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            emptyPos.Add(enemySpawnPoints[i]);
        }
        for (int j = 0; j < enemyAmount; j++)
        {
            int randomPos = Random.Range(0, emptyPos.Count);
            GameObject enemy = ObjectPool.instance.SpawnFromPool("Enemy", emptyPos[randomPos].position, Quaternion.identity);
            enemy.GetComponent<Enemy>().OnInit();
            emptyPos.RemoveAt(randomPos);
        }
        for (int k = 0; k < mushroomCount; k++)
        {
            int randomPos = Random.Range(0, emptyPos.Count);
            GameObject mushroom = Instantiate(mushroomPf, emptyPos[randomPos].position, Quaternion.identity);
            mushroom.transform.SetParent(spawnObjParent.transform);
            emptyPos.RemoveAt(randomPos);
        }
        isFinalBoss = false;
    }
    public List<GameObject> GetSpawnedBreakableObj()
    {
        return spawnedBreakableObj;
    }

    public void OnFinalBoss()
    {
        //OnFinish();
        if (isFinalBoss)
        {
            OnFinish();
            return;
        }
        int randomPos = Random.Range(0, emptyPos.Count);
        GameObject bossSpawn = Instantiate(bossPf, emptyPos[randomPos].position, Quaternion.identity);
        boss = bossSpawn.GetComponent<Boss>();
        emptyPos.RemoveAt(randomPos);
        isFinalBoss = true;
    }

    public void BossFight()
    {
        player.transform.LookAt(boss.transform.position);
        player.isBossFight = true;
        cameraFollow.OnBossFight();
        cameraFollow.isBossFight = true;
        joystick.gameObject.SetActive(false);
        countdown.SetActive(true);
        StartCoroutine(countdown.GetComponent<Countdown>().AnimateTimer());
    }

    public IEnumerator AfterBossFight()
    {
        if (player.levelManager.currentLevel > boss.levelManager.currentLevel)
        {
            player.ChangeAnimation(Character.AnimationState.Finisher);
        }
        else
        {
            boss.ChangeAnimation(Character.AnimationState.Finisher);
        }
        yield return new WaitForSeconds(1f);
        cameraFollow.AfterBossFight();
        player.isBossFight = false;
        if (boss.levelManager.currentLevel < player.levelManager.currentLevel)
        {
            boss.OnDie();
        }
        else
        {
            player.OnDie();
        }
    }

    public void OnFinish()
    {
        foreach (GameObject child in spawnedBreakableObj)
        {
            Destroy(child.transform.parent.gameObject);
        }
        foreach (GameObject enemy in spawnedEnemy)
        {
            enemy.SetActive(false);
        }
        foreach (GameObject drop in drops)
        {
            Destroy(drop);
        }
        emptyPos.Clear();
        spawnedBreakableObj.Clear();
        spawnedEnemy.Clear();
        StartCoroutine(returnToMenu());
    }

    private IEnumerator returnToMenu()
    {
        yield return new WaitForSeconds(1f);
        MainMenu.Instance.ReturnToMenu();
    }
}
