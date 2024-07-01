using DG.Tweening;
using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : Singleton<MainMenu>
{
    [Header("PLAYER")]
    public Player player;
    public LevelManager playerLM;
    private Vector3 initialPos;

    [Header("UPGRADES")]
    public int[] upgradeCost = new int[] { 150, 300, 450, 600, 750, 900, 1050, 1200, 1350, 1500, 1650, 1800,
    1950, 2100, 2250, 2400, 2550, 2700, 2850, 3000, 3150, 3300, 3450,
    3600, 3750, 3900, 4050, 4200, 4350, 4500 };
    public int[] upgradeLevel = new int[] {5, 10, 15, 20, 25, 30,
    35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105,
    110, 115, 120, 125, 130, 135, 140, 145, 150};

    //stats
    private int level;
    private int damage;
    private int food;
    private int health;

    //Rank
    public int levelRank;
    public int damageRank;
    public int foodRank;
    public int stage;

    [Header("TEXT")]
    public TextMeshProUGUI levelNumber;
    public TextMeshProUGUI damageNumber;
    public TextMeshProUGUI foodNumber;
    public TextMeshProUGUI levelCost;
    public TextMeshProUGUI damageCost;
    public TextMeshProUGUI foodCost;

    [Header("CURRENCY")]
    public static int gold;
    public static int diamond;

    //UI
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI diamondText;

    //Turn off at start
    public GameObject joystickPanel;
    public CameraFollow cameraFollow;

    //Animator
    public Animator animator;

    public GameController gameController;

    public Button startGame;

    public Image coinPf;
    public RectTransform coinParent;
    public RectTransform coinfinalpos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerLM = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelManager>();
        animator = GetComponent<Animator>();
        initialPos = player.transform.position;
        PlayerPrefsInit();
        OnInit();
    }


    public void PlayerPrefsInit()
    {
        if (!PlayerPrefs.HasKey("Gold"))
        {
            PlayerPrefs.SetInt("Gold", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Gold");
        }
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Diamond");
        }
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Level");
        }
        if (!PlayerPrefs.HasKey("Damage"))
        {
            PlayerPrefs.SetInt("Damage", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Damage");
        }
        if (!PlayerPrefs.HasKey("Food"))
        {
            PlayerPrefs.SetInt("Food", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Food");
        }
        if (!PlayerPrefs.HasKey("Stage"))
        {
            PlayerPrefs.SetInt("Stage", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Stage");
        }
    }
    public void OnInit()
    {
        stage = PlayerPrefs.GetInt("Stage");
        gold = PlayerPrefs.GetInt("Gold");
        diamond = PlayerPrefs.GetInt("Diamond");
        levelRank = PlayerPrefs.GetInt("Level");
        damageRank = PlayerPrefs.GetInt("Damage");
        foodRank = PlayerPrefs.GetInt("Food");
        level = upgradeLevel[levelRank];
        damage = damageRank + 1;
        food = foodRank + 1;
        health = level + 10;
        levelCost.text = upgradeCost[levelRank].ToString();
        damageCost.text = upgradeCost[damageRank].ToString();
        foodCost.text = upgradeCost[foodRank].ToString();
        levelNumber.text = upgradeLevel[levelRank].ToString();
        damageNumber.text = damage.ToString();
        foodNumber.text = food.ToString();
        goldText.text = gold.ToString();
        diamondText.text = diamond.ToString();
        joystickPanel.gameObject.SetActive(false);
        startGame.onClick.AddListener(() => StartGame());
    }

    private void Update()
    {
        goldText.text = gold.ToString();
        diamondText.text = diamond.ToString();
    }

    private void StartGame()
    {
        playerLM.currentLevel = level;
        player.attackDamage = damage;
        player.foodPerDrop = food;
        cameraFollow.OnInit();
        joystickPanel.gameObject.SetActive(true);
        animator.SetBool("Start", true);
        gameController.gameObject.SetActive(true);
        GameController.Instance.OnInit();
        startGame.gameObject.SetActive(false);
        player.canvas.gameObject.SetActive(true);
    }

    public void UpgradeLevel()
    {
        if (gold >= upgradeCost[levelRank])
        {
            if (levelRank == upgradeLevel.Length - 1)
            {
                levelCost.text = "MAX";
                return;
            }
            else
            {
                gold -= upgradeCost[levelRank];
                levelRank++;
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetInt("Level", levelRank);
            }
            level = upgradeLevel[levelRank];
            levelNumber.text = level.ToString();
            levelCost.text = upgradeCost[levelRank].ToString();
            if (levelRank == upgradeLevel.Length - 1)
            {
                levelRank = upgradeLevel.Length - 1;
                levelCost.text = "MAX";
                return;
            }
        }
    }

    public void UpgradeDamage()
    {
        if (gold >= upgradeCost[damageRank])
        {
            if (damageRank == upgradeLevel.Length - 1)
            {
                damageCost.text = "MAX";
                return;
            }
            else
            {
                gold -= upgradeCost[damageRank];
                damageRank++;
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetInt("Damage", damageRank);
            }
            damage++;
            damageNumber.text = damage.ToString();
            damageCost.text = upgradeCost[damageRank].ToString();
            if (damageRank == upgradeLevel.Length - 1)
            {
                damageRank = upgradeLevel.Length - 1;
                damageCost.text = "MAX";
                return;
            }
        }
    }

    public void UpgradeFood()
    {
        if (gold >= upgradeCost[foodRank])
        {
            if (foodRank == upgradeLevel.Length - 1)
            {
                foodCost.text = "MAX";
                return;
            }
            else
            {
                gold -= upgradeCost[foodRank];
                foodRank++;
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetInt("Food", foodRank);
            }
            food++;
            foodNumber.text = food.ToString();
            foodCost.text = upgradeCost[foodRank].ToString();
            if (foodRank == upgradeLevel.Length - 1)
            {
                foodRank = upgradeLevel.Length -1;
                foodCost.text = "MAX";
                return;
            }
        }
    }

    public void AddCurrency(int amount, string type)
    {
        if (type == "gold")
        {
            gold += amount;
            PlayerPrefs.SetInt("Gold", gold);
        }
        else if (type == "diamond")
        {
            diamond += amount;
            PlayerPrefs.SetInt("Diamond", diamond);
        }
    }

    public void ReturnToMenu ()
    {
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        player.canvas.gameObject.SetActive(false);
        for (int i = 0; i < player.targets.Count; i++)
        {
            player.targets.RemoveAt(i);
        }
        player.target = null;
        player.isDead = false;
        player.ChangeAnimation(Character.AnimationState.Idle);
        player.transform.position = initialPos;
        joystickPanel.gameObject.SetActive(false);
        cameraFollow.OnMainMenu();
        gameController.gameObject.SetActive(false);
        for (int i = 0; i < playerLM.currentLevel; i++)
        {
            Image coin = Instantiate(coinPf, player.transform.position, Quaternion.identity).GetComponent<Image>();
            coin.transform.SetParent(coinParent);
            coin.rectTransform.anchoredPosition = Vector2.zero;
            coin.rectTransform.DOAnchorPos(coinfinalpos.position, 1f).OnComplete(() => Destroy(coin.gameObject, 1f));
        }
        gold += playerLM.currentLevel * 10;
        PlayerPrefs.SetInt("Gold", gold);
        animator.SetBool("Return", true);
        startGame.gameObject.SetActive(true);
    }

    private void DuringMainMenu()
    {
        animator.SetBool("Start", false);
        animator.SetBool("Return", false);
    }
}
