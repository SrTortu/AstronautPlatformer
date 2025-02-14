using UnityEngine;


public class ItemError : Item
{
    const float ERROR_FORCE = 10000;
    const float ERROR_DOWN_POS = 2.5f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
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
}