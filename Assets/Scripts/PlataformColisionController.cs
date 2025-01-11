using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformColisionController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private Collider2D platformCollider;

 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.y < transform.position.y)
            {
                Physics2D.IgnoreCollision(collision, platformCollider, true);
                Debug.Log("Debajo debajo");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Rehabilitar la colisión cuando el jugador salga del área
            Physics2D.IgnoreCollision(collision, platformCollider, false);
        }
    }
}

