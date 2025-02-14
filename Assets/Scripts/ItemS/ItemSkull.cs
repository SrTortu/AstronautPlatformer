using UnityEngine;
using System;

public class ItemNose : Item
{
    const float NOSE_DAMAGE = -0.1f;

    #region Unity Callbacks

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();
            collision.gameObject.GetComponent<Player>().DamageOn();
            jetpack.AddRegRatio(NOSE_DAMAGE);
            Recolected();
        }
    }

    #endregion
}