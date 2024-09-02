using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject snake;
    public GameObject snail;
    public GameObject slime;
    private GameController controller;

    private int snailHealth;
    private int slimeHealth;

    public AudioClip enemyHit;
    public AudioClip enemyKill;

    public Animator slimeAnimator;
    public Animator snakeAnimator;
    public Animator snailAnimator;

    void Start()
    {
        snailHealth = 5;
        slimeHealth = 3;
        controller = GameObject.FindObjectOfType<GameController>();
    }

    public void FixedUpdate()
    {
        if(snake.transform.position.x < -10 || snail.transform.position.x < -10 || slime.transform.position.x < -10)
        {
            Destroy(gameObject);
            controller.LoseALife();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" && gameObject.tag == "Snake")
        {
            snakeAnimator.SetBool("IsSnakeDead", true);
            AudioSource.PlayClipAtPoint(enemyKill, transform.position);
            controller.UpdateScore();
            Destroy(gameObject);
        }
        else if(other.tag == "Bullet" && gameObject.tag == "Slime")
        {
            slimeHealth--;
            AudioSource.PlayClipAtPoint(enemyHit, transform.position);
            slimeAnimator.SetTrigger("IsSlimeHit");
            if (slimeHealth == 0)
            {
                slimeAnimator.SetBool("IsSlimeDead", true);
                Destroy(gameObject);
                controller.UpdateScore();
                AudioSource.PlayClipAtPoint(enemyKill, transform.position);
                slimeHealth = 3;
            }
        }
        else if(other.tag == "Bullet" && gameObject.tag == "Snail")
        {
            snailHealth--;
            AudioSource.PlayClipAtPoint(enemyHit, transform.position);
            snailAnimator.SetTrigger("IsSnailHit");
            if (snailHealth == 0)
            {
                snailAnimator.SetBool("IsSnailDead", true);
                Destroy(gameObject);
                controller.UpdateScore();
                AudioSource.PlayClipAtPoint(enemyKill, transform.position);
                snailHealth = 5;
            }
        }

        if (other.tag == "Tank")
        {
            //AudioSource.PlayClipAtPoint(destroySound, transform.position);
            Destroy(gameObject);
            controller.LoseALife();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
