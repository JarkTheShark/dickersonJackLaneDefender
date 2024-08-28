using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    //public AudioClip destroySound;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction)
    {
        rb2D.velocity = new Vector2(5, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //AudioSource.PlayClipAtPoint(destroySound, transform.position);
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
