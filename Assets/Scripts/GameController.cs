using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Tank;
    public GameObject Target;
    private bool isMoving;
    private bool isFiring;
    public float moveSpeed = 5f;
    private float moveDirection;

    public Bullet bullet;
    public Spawner spawner;
    public Enemies enemies;

    public PlayerInput playerInput;
    private InputAction quit;
    private InputAction move;
    private InputAction shoot;
    private InputAction restart;

    private Stopwatch timer;
    private int fireCounter = 1;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text livesText;
    public TMP_Text endGameText;
    private int lifeCounter = 3;
    private int score = 0;
    public static int highScore;

    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        playerInput.currentActionMap.Enable();
        quit = playerInput.currentActionMap.FindAction("ExitGame");
        move = playerInput.currentActionMap.FindAction("Move");
        shoot = playerInput.currentActionMap.FindAction("Shoot");
        restart = playerInput.currentActionMap.FindAction("Restart");

        move.started += Move_started;
        move.canceled += Move_canceled;
        quit.started += Quit_started;
        shoot.started += Shoot_started;
        restart.started += Restart_started;

        isMoving = false;
        isFiring = false;

        timer = new Stopwatch();

        endGameText.gameObject.SetActive(false);
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    private void Shoot_started(InputAction.CallbackContext obj)
    {
        if (fireCounter == 1)
        {
            isFiring = true;
            fireCounter--;
        }
        else
        {
            timer.Start();
        }
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isMoving = false;
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        isMoving = true;
    }

    public void LoseALife()
    {
        lifeCounter--;
        livesText.text = "Lives: " + lifeCounter.ToString();
        AudioSource.PlayClipAtPoint(deathSound, Tank.transform.position);
        if (lifeCounter == 0)
        {
            endGameText.gameObject.SetActive(true);
            spawner.CancelInvoke();
            Tank.GetComponent<Rigidbody2D>().Sleep();
            if(score > highScore)
            {
                highScore = score;
                highScoreText.text = "Highscore: " + highScore.ToString();
            }
        }
    }

    public void UpdateScore()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isMoving)
        {
            Tank.GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveSpeed * moveDirection);
        }
        else
        {
            Tank.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if(isFiring)
        {
            Bullet theBullet = Instantiate(bullet, Target.transform.position, Target.transform.rotation);
            theBullet.Shoot(transform.forward);
            isFiring = false;
        }
    }

    void Update()
    {
        if(isMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
        if(timer.ElapsedMilliseconds > 300)
        {
            fireCounter = 1;
            timer.Reset();
        }
        PlayerPrefs.SetInt("highScore", highScore);
        PlayerPrefs.Save();
    }
}
