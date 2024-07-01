using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private float size = 1;
    //SELF
    private Character self;

    public GameObject[] skins;

    //LEVEL AND EXP
    public int currentLevel;
    public int expRequired;
    public int currentAnimator;

    public int stage;
    public int[] levelReq = new int[] { 0, 10, 20, 30, 50, 70, 90, 120, 150, 180, 220, 260, 300 };
    //UI
    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        expRequired = levelReq[stage];

        self = GetComponent<Character>();
        skins[currentAnimator].SetActive(true);
        expRequired = 10;
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level \n" + currentLevel;
    }

    public void AddLevel(int LevelUpAmount)
    {
        currentLevel += LevelUpAmount;
        //ChangeAnimator();
        if (currentLevel > expRequired)
        {
            size += 0.05f;
            stage += 1;
            expRequired = levelReq[stage];
            transform.localScale = new Vector3(size, size, size);
        }
        //for (int i = 0; i < skins.Length; i++)
        //{
        //    if (i != currentAnimator)
        //    {
        //        skins[i].SetActive(false);
        //    }
        //    else
        //    {
        //        EffectController.Instance.PlayEffect(self.levelUpFX, transform);
        //        skins[currentAnimator].SetActive(true);
        //    }
        //}
    } 

    public void SubtractLevel(int subtractAmount)
    {
        currentLevel -= subtractAmount;
        if (currentLevel < 0)
        {
            currentLevel = 0;
        }
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    //private void ChangeAnimator()
    //{
    //    switch (currentLevel)
    //    {
    //        case <=11:
    //            currentAnimator = 0;
    //            break;
    //        case <=21:
    //            currentAnimator = 1;
    //            break;
    //        case <=31:
    //            currentAnimator = 2;
    //            break;
    //        case <=51:
    //            currentAnimator = 3;
    //            break;
    //        case <=71:
    //            currentAnimator = 4;
    //            break;
    //        case <=91:
    //            currentAnimator = 5;
    //            break;
    //        case <=121:
    //            currentAnimator = 6;
    //            break;
    //        case <=151:
    //            currentAnimator = 7;
    //            break;
    //        case <=181:
    //            currentAnimator = 8;
    //            break;
    //        case <=221:
    //            currentAnimator = 9;
    //            break;
    //        case <=261:
    //            currentAnimator = 10;
    //            break;
    //        case <=301:
    //            currentAnimator = 11;
    //            break;
    //    }
    //}
}
