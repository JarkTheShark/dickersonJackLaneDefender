using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float spawnRate = 2;
    private float spawnTime = 4;
    private int enemyChoice;
    private int spawnPosition;
    private int enemyMoveSpeed;
    private Enemies enemies;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChooseEnemy", 0f, spawnRate);
        InvokeRepeating("ChooseLane", 0f, spawnRate);
        InvokeRepeating("Spawn", 3f, spawnTime);

    }

    public void Spawn()
    {
      GameObject ChosenOne = Instantiate(choice, targetPlacement.transform.position, Quaternion.identity);
        ChosenOne.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * enemyMoveSpeed, 0);
        
    }

    public void ChooseLane()
    {
        spawnPosition = Random.Range(1, 6);
        if (spawnPosition == 1 )
        {
            targetPlacement.transform.position = target1.transform.position;
        }
        else if (spawnPosition == 2 )
        {
            targetPlacement.transform.position= target2.transform.position;
        }
        else if (spawnPosition == 3)
        {
            targetPlacement.transform.position = target3.transform.position;
        }
        else if (spawnPosition == 4)
        {
            targetPlacement.transform.position = target4.transform.position;
        }
        else if (spawnPosition == 5)
        {
            targetPlacement.transform.position = target5.transform.position;
        }
    }
    
    public void ChooseEnemy()
    {
        enemyChoice = Random.Range(1, 4);
        if(enemyChoice == 1)
        {
            choice = snail;
            enemyMoveSpeed = 1;
        }
        else if (enemyChoice == 2)
        {
            choice = slime;
            enemyMoveSpeed = 2;
        }
        else if (enemyChoice == 3)
        {
            choice = snake;
            enemyMoveSpeed = 3;
        }
    }

    public void GameOver()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
