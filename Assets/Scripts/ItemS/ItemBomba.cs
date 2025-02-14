using UnityEngine;


public class ItemError : Item
{
    #region Constants

    const float ERROR_FORCE = 10000;
    const float ERROR_DOWN_POS = 2.5f;

    #endregion

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
            jetpack.GetComponent<Rigidbody2D>().AddForce(Vector2.down * ERROR_FORCE);
            Player player = collision.gameObject.GetComponent<Player>();
            player.DamageOn();
            player.playerGun.DetachHook();

            if (jetpack.transform.position.y > 1)
            {
                jetpack.transform.Translate(Vector2.down * ERROR_DOWN_POS);
            }

            Recolected();
        }
    }

    #endregion
}