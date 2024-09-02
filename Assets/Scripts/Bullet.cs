using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    public AudioClip shootClip;
    public Animator explosion;
 
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction)
    {
        rb2D.velocity = new Vector2(5, 0);
        AudioSource.PlayClipAtPoint(shootClip, transform.position);
        explosion.SetBool("IsShot", true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Snail" || other.tag == "Snake" || other.tag == "Slime")
        {
            explosion.SetBool("IsDestroyed", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Snail" || other.tag == "Snake" || other.tag == "Slime")
        {
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        if(transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }
}
