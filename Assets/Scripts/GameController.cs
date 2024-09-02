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

    /// <summary>
    /// Makes the inputs register and turns off text
    /// </summary>
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

    /// <summary>
    /// Restarts on button press
    /// </summary>
    /// <param name="obj"></param>
    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Puts a timer when shooting to prevent spam
    /// </summary>
    /// <param name="obj"></param>
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

    /// <summary>
    /// quits on button press
    /// </summary>
    /// <param name="obj"></param>
    private void Quit_started(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    /// <summary>
    /// makes bool false when not moving
    /// </summary>
    /// <param name="obj"></param>
    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isMoving = false;
    }

    /// <summary>
    /// makes bool true when moving
    /// </summary>
    /// <param name="obj"></param>
    private void Move_started(InputAction.CallbackContext obj)
    {
        isMoving = true;
    }

    /// <summary>
    /// Loses a life and kills when hit
    /// </summary>
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

    /// <summary>
    /// updates score based on enemies killed
    /// </summary>
    public void UpdateScore()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();
    }

    /// <summary>
    /// checks if moving and firing and does actions based on such
    /// </summary>
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

    /// <summary>
    /// moves and fires if moving/firing and saves highscore
    /// </summary>
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
