using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject snake;
    public GameObject snail;
    public GameObject slime;
    public GameObject choice;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;
    public GameObject targetPlacement;
    private float spawnRate = 5f;
    private int enemyChoice;
    private int spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChooseEnemy", 0f, spawnRate);
        InvokeRepeating("ChooseLane", 0f, spawnRate);
        InvokeRepeating("Spawn", 3f, spawnRate);

    }

    public void Spawn()
    {
       
    }

    public void ChooseLane()
    {

    }

    public void ChooseEnemy(GameObject ChosenOne)
    {
        enemyChoice = Random.Range(1, 4);
        if(enemyChoice == 1)
        {
            choice = snail;
        }
        else if (enemyChoice == 2)
        {
            choice = slime;
        }
        else if (enemyChoice == 3)
        {
            choice = snake;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
