using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour
{
    [Header("Evolution parts")]
    [SerializeField] GameObject[] stages;
    [SerializeField] GameObject[] headParts;
    [SerializeField] GameObject[] bodyParts;
    [SerializeField] GameObject[] tailParts;
    [SerializeField] GameObject[] glove;
    private int stage;

    //parts evolution index
    [SerializeField] int index;
    [SerializeField] int headIndex;
    [SerializeField] int bodyIndex;
    [SerializeField] int tailIndex;
    [SerializeField] int gloveIndex;


    public void Start()
    {
        stage = PlayerPrefs.GetInt("Stage");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Evolve();
        }
    }

    public void Evolve()
    {
        stage++;
        EvolutionStage();
        PartsToEvolve(stages, index);
        PartsToEvolve(headParts, headIndex);
        PartsToEvolve(bodyParts, bodyIndex);
        PartsToEvolve(tailParts, tailIndex);
        PartsToEvolve(glove, gloveIndex);
    }

    private void PartsToEvolve(GameObject[] part, int partIndex)
    {
        for (int i = 0; i < part.Length; i++)
        {
            if (i == partIndex)
            {
                part[i].SetActive(true);
            }
            else
            {
                part[i].SetActive(false);
            }
        }
    }

    private void EvolutionStage()
    {
        switch (stage)
        {
            case 0:
                index = 0;
                headIndex = 0;
                bodyIndex = 0;
                tailIndex = 0;
                gloveIndex = 0;
                break;
            case 1:
                index = 0;
                headIndex = 1;
                bodyIndex = 1;
                tailIndex = 1;
                gloveIndex = 1;
                break;
            case 2:
                index = 1;
                headIndex = 2;
                bodyIndex = 2;
                tailIndex = 2;
                gloveIndex = 2;
                break;
            case > 3:
                index = 2;
                headIndex = 3;
                bodyIndex = 3;
                tailIndex = 3;
                gloveIndex = 3;
                break;

        }
    }
}
