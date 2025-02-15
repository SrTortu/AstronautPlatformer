using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeWrapper
{
    /// <summary>
    /// Moves the player towards the closest bend point or anchor point.
    /// </summary>

    [RequireComponent(typeof(RopeWrapController))]

    public class RopeReelController : MonoBehaviour
    {
        private RopeWrapController ropeWrapper;
        private Rigidbody2D playerRgbd;

        [SerializeField]
        private float retractForce;

        private void Start()
        {
            ropeWrapper = GetComponent<RopeWrapController>();
            playerRgbd = ropeWrapper.player.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (ropeWrapper.IsRopeAnchored())
            {
                if (Input.GetKey(KeyCode.R))
                {
                    Vector2 forceVector = ropeWrapper.GetClosestPivot() - playerRgbd.position;
                    forceVector *= retractForce;
                    playerRgbd.AddForce(forceVector);
                }
            }
        }
    }
}