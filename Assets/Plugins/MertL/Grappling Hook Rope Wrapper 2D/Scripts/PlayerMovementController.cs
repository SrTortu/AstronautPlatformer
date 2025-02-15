using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeWrapper
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerMovementController : MonoBehaviour
    {
        private Rigidbody2D playerRgbd;
        private SpriteRenderer sprite;

        [SerializeField]
        private float movementSpeed;

        private void Start()
        {
            playerRgbd = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            playerRgbd.velocity = Vector2.ClampMagnitude(playerRgbd.velocity, movementSpeed);

            if (Input.GetKey(KeyCode.W))
                playerRgbd.velocity = new Vector2(playerRgbd.velocity.x, movementSpeed);
            if (Input.GetKey(KeyCode.S))
                playerRgbd.velocity = new Vector2(playerRgbd.velocity.x, -movementSpeed);
            if (Input.GetKey(KeyCode.D))
            {
                playerRgbd.velocity = new Vector2(movementSpeed, playerRgbd.velocity.y);
                sprite.flipX = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerRgbd.velocity = new Vector2(-movementSpeed, playerRgbd.velocity.y);
                sprite.flipX = true;
            }
        }
    }
}