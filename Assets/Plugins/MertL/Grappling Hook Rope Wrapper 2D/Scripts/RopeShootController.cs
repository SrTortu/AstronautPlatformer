using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeWrapper
{
    /// <summary>
    /// Shoots/Releases & Anchors the rope;
    /// Rope is shot by left mouse click towards the mouse position,
    /// Rope is relesead by right mouse click,
    /// Anchoring is done according to triggering of collider.
    /// 
    /// !!! WARNING: Rope anchor point is stationary. It will not move if the anchored object moves !!!
    /// </summary>

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(RopeWrapController))]

    public class RopeShootController : MonoBehaviour
    {
        public Transform player;

        public float ropeShootVelocity;
        private bool isRopeShot;

        private float pushOutIncrement;

        private void Start()
        {
            GetComponent<Collider2D>().enabled = false;
            pushOutIncrement = GetComponent<LineRenderer>().startWidth / 100f;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

        //Shoot or Release the rope according to mouse input
        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && !isRopeShot)
                ShootTheRope();
            else if (Input.GetMouseButtonUp(1) && isRopeShot)
                ReleaseTheRope();
        }

        //When left mouse button is clicked, shoot the rope from player's current position towards position of mouse pointer with a preassigned magnitude of velocity
        //Linerenderer is assigned two vertex points so that it can be drawn on screen.
        private void ShootTheRope()
        {
            isRopeShot = true;
            GetComponent<Collider2D>().enabled = true;
            transform.position = player.position;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootVector = targetPos - transform.position;
            GetComponent<Rigidbody2D>().velocity = ropeShootVelocity * shootVector.normalized;

            GetComponent<RopeWrapController>().RopeIsJustShot();
        }

        //When right mouse button is clicked, cancel the rope by zeroing velocity and disabling collider to avoid any problems while rope is not shot
        //Linerenderer vertex count should be cleared so that rope is not drawn on screen anymore
        private void ReleaseTheRope()
        {
            isRopeShot = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<RopeWrapController>().RopeIsReleased();
        }

        //Anchor the rope if it touches an obstacle
        // !!! REMINDER: Rope anchor point is stationary. It will not move if the anchored object moves !!!
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name != player.name) //Rope cannot anchor on player itself
            {

                GetComponent<Rigidbody2D>().velocity = Vector2.zero;                //Anchor the rope by stopping the rope movement. 
                GetComponent<Collider2D>().enabled = false;             //Disable the collider to avoid any problems
                AnchorTheRope(collision);
                GetComponent<RopeWrapController>().RopeIsAnchored(transform.position);
            }
        }

        private void AnchorTheRope(Collider2D anchoredCollider)
        {
            if (anchoredCollider.GetType() != typeof(PolygonCollider2D) || anchoredCollider.GetComponent<Rigidbody2D>() == null)
                Debug.LogError("Objects the rope anchors and wraps should have PolygonCollider2D" + "\nObjects to be wrapped should also have Rigidbody2D attached as component!");
            else
                PushAnchorOutOfColliderBoundaries((PolygonCollider2D)anchoredCollider);
        }

        //To avoid complications with raycasts, the anchor should be set outside of the collider boundaries.
        //So we push the anchor out of the anchored colliders boundaries and towards the player.
        private void PushAnchorOutOfColliderBoundaries(PolygonCollider2D collider)
        {
            //Check if anchor is in collider boundaries
            if (collider.OverlapPoint(transform.position))
            {
                Vector2 pushVector = (Vector2)player.transform.position - (Vector2)transform.position;
                Vector3 pushVectorClamped = Vector2.ClampMagnitude(pushVector, pushOutIncrement);   //Push towards the player with pushStepAmount
                transform.position += pushVectorClamped;

                PushAnchorOutOfColliderBoundaries(collider);    //Make recursive call until the anchor is out of collider bounds
            }
        }
    }
}