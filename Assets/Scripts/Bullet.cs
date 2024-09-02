using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    public AudioClip shootClip;
    public Animator explosion;
 
    /// <summary>
    /// gets the rigidbody
    /// </summary>
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// gives bullet velocity as well as plays audio and animations
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot(Vector2 direction)
    {
        rb2D.velocity = new Vector2(5, 0);
        AudioSource.PlayClipAtPoint(shootClip, transform.position);
        explosion.SetBool("IsShot", true);
    }

    /// <summary>
    /// plays animation when collides with enmy
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Snail" || other.tag == "Snake" || other.tag == "Slime")
        {
            explosion.SetBool("IsDestroyed", true);
        }
    }

    /// <summary>
    /// destroys bullet after collides with enemy
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Snail" || other.tag == "Snake" || other.tag == "Slime")
        {
            Destroy(gameObject);
        }

    }

    /// <summary>
    /// if bullet goes too far, destroys it
    /// </summary>
    private void Update()
    {
        if(transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }
}
