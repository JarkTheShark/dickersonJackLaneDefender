using System.Collections;
using System.Collections.Generic;
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

    public PlayerInput playerInput;
    private InputAction quit;
    private InputAction move;
    private InputAction shoot;
    private InputAction restart;



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
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    private void Shoot_started(InputAction.CallbackContext obj)
    {
        isFiring = true;
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
    }
}
