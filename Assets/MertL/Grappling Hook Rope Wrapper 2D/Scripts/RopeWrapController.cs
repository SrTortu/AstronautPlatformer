using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RopeWrapper
{
    /// <summary>
    /// This script makes the line renderer wrap around objects that have polygon colliders and rigidbodies.
    /// 
    /// NOTE: Wrapping will only work on objects on RopeWrapLayer!!! 
    /// You will not receive any errors about the absence of layer!
    /// </summary>
    /// 

    [RequireComponent(typeof(LineRenderer))]

    public class RopeWrapController : MonoBehaviour
    {
        public GameObject player;
        private LineRenderer ropeLineRenderer;

        private const string WRAPLAYERNAME = "RopeWrapLayer";

        private List<bool> pivotSwingList = new List<bool>();// if bool is true swing is clockwise


        private bool isRopeAnchored;
        private bool isRopeShot;

        private int pivotsAdded = 0;

        private float oldAngle;
        private float currentAngle;

        private float pushOutIncrement; //This variable is used in pushing anchor and pivot points out of the objects collider to make sure they are not within the boundaries

        private RaycastHit2D rayToClosestPivotPoint;

        private void Start()
        {
            //Check if rope wrapping layer is defined. The rope will only wrap around objects in this layer.
            if (LayerMask.NameToLayer(WRAPLAYERNAME) < 0) //Unity returns -1 if the layer is not defined
                Debug.LogError("This script will not work properly unless a layer with name 'RopeWrapLayer' is defined in layer list! " +
                               "\nThe obstacles that the rope is supposed to wrap around, should be on RopeWrapLayer.");

            ropeLineRenderer = GetComponent<LineRenderer>();

            pushOutIncrement = ropeLineRenderer.startWidth / 10f;
        }

        public void RopeIsJustShot()
        {
            ropeLineRenderer.positionCount = 2;//Rope initially has only start and end points.
            SetRopeEndPoints();
            isRopeShot = true;
        }

        public void RopeIsAnchored(Vector3 anchorPosition)
        {
            if (Physics2D.OverlapPoint(anchorPosition) != null)
                Debug.LogError("Anchor is inside the collider to anchor. This will cause complications in Raycast. Push the anchor out of the collider!");
            else
                isRopeAnchored = true;
        }

        public void RopeIsReleased()
        {
            isRopeAnchored = false;
            isRopeShot = false;
            ropeLineRenderer.positionCount = 0;
            pivotsAdded = 0;
            pivotSwingList.Clear();
        }

        void FixedUpdate()
        {
            if (isRopeAnchored)
            {
                if (IsLineOfSiteToClosestPivotClear() == false)            //The rope should wrap if there is an obstacle between player and the closest pivot/bending point
                    WrapTheRope();
                else if (IsLineOfSightTo2ndClosestPivotClear())            //Since rope does not wrap, we should check if it should Unwrap
                    ClearClosestPivotAndSwingFromList();

                SetRopeEndPoints();            //Regardless of wrap or unwrap, the points of the rope on both ends remain the same (player & anchor points). So we update end points in case the player is moving.
            }
            else if (isRopeShot)
                SetRopeEndPoints();

        }

        #region Wrap Related Region
        bool IsLineOfSiteToClosestPivotClear()
        {
            rayToClosestPivotPoint = SendRayToClosestPivotPoint();

            if (rayToClosestPivotPoint.collider != null)        //If the ray to closest/latest pivot point hits an obstacle, line of site is not clear
                return false;
            else
                return true;
        }

        RaycastHit2D SendRayToClosestPivotPoint()
        {
            Vector2 playerPosition = (Vector2)player.transform.position;

            float ropeDistance = 0;
            Vector2 rayDirection;
            Vector2 closestPivotToPlayer = (Vector2)ropeLineRenderer.GetPosition(1);
            ropeDistance = Vector2.Distance(closestPivotToPlayer, playerPosition);
            rayDirection = closestPivotToPlayer - playerPosition;

            LayerMask layerMask = LayerMask.GetMask(WRAPLAYERNAME);        //We want the rope to wrap only around specific objects on RopeWrapLayer

            RaycastHit2D rayResult = Physics2D.Raycast(playerPosition, rayDirection, ropeDistance, layerMask);        //Send a ray from players position to closest/lates pivot point.

            return rayResult;
        }

        void WrapTheRope()
        {
            Vector2 polygonVertexPoint = GetClosestColliderPointFromRaycastHit(rayToClosestPivotPoint, rayToClosestPivotPoint.collider.gameObject.GetComponent<PolygonCollider2D>());

            AddSwingDirectionForNewPivot(polygonVertexPoint);
            AddLineRenderPivotPoint(polygonVertexPoint);
            PushPivotPointOutwards(rayToClosestPivotPoint.collider.gameObject.GetComponent<Rigidbody2D>());
        }
        #endregion

        #region UnWrap Related Region
        bool IsLineOfSightTo2ndClosestPivotClear()
        {
            bool isClear;
            //First of all, we make sure there are more than 2 pivots (other than start&end points).
            //Which means rope has wrapped before and additional pivots are present. Otherwise there is nothing to "unwrap"
            if (pivotsAdded > 0)
            {
                if (SendRayTo2ndClosestPivotHit() && ropeLineRenderer.positionCount > 2
                    && IsAngleGettingLarger() && IsPivotAngleOnCounterSwingDirection())
                    isClear = true;
                else
                    isClear = false;
            }
            else
                isClear = false;        //If there is nothing to unwrap, there is no 2nd closest pivot. So we return false anyway.

            return isClear;
        }

        bool SendRayTo2ndClosestPivotHit()
        {
            bool isLineToSecondPivotClear = true;

            float ropeDistance = Vector2.Distance((Vector2)ropeLineRenderer.GetPosition(2), (Vector2)player.transform.position);
            Vector2 rayDirection = (Vector2)ropeLineRenderer.GetPosition(2) - (Vector2)player.transform.position;
            LayerMask layerMask = LayerMask.GetMask(WRAPLAYERNAME);

            RaycastHit2D hit = Physics2D.Raycast((Vector2)player.transform.position, rayDirection, ropeDistance * 0.95f, layerMask);

            if (hit.collider != null)
                isLineToSecondPivotClear = false;

            return isLineToSecondPivotClear;
        }
        #endregion

        /// <summary>
        /// This method figures out the closest Polygon collider vertex to a specified Raycast2D hit point in order to assist in 'rope wrapping'
        /// This way, instead of the point where raycast hits the collider, the polygon vertex is set as bending pivot point of rope.
        /// </summary>
        private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
        {
            var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
                position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
                position => polyCollider.transform.TransformPoint(position));

            var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
            return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
        }

        void AddLineRenderPivotPoint(Vector2 polygonHitPoint)
        {
            pivotsAdded++;

            Vector2 playerNextFramePosition = (Vector2)player.transform.position;

            Vector2[] tempPoints = new Vector2[ropeLineRenderer.positionCount + 1];
            tempPoints[0] = ropeLineRenderer.GetPosition(0);
            tempPoints[1] = polygonHitPoint;

            for (int i = 2; i < ropeLineRenderer.positionCount + 1; i++)
                tempPoints[i] = ropeLineRenderer.GetPosition(i - 1);

            ropeLineRenderer.positionCount++;

            for (int i = 0; i < tempPoints.Length; i++)
                ropeLineRenderer.SetPosition(i, (Vector3)tempPoints[i]);
        }

        void AddSwingDirectionForNewPivot(Vector2 polygonHitPoint)
        {
            bool isSwingClockWise = CheckSwingDirectionByPlayerPositon(polygonHitPoint);

            pivotSwingList.Add(isSwingClockWise);
        }

        //Just as the anchor position, we should make sure the pivot/bend points are outside the boudnaries of wrapped object to avoid raycast complications
        void PushPivotPointOutwards(Rigidbody2D rgbdWrapped)
        {
            Vector3 pointToPush = ropeLineRenderer.GetPosition(1);
            Vector2 pushVector = pointToPush - (Vector3)rgbdWrapped.worldCenterOfMass;
            pushVector = Vector2.ClampMagnitude(pushVector, pushOutIncrement * 5f);     //Pushed by half of the line width so that rope is not buried in obstacle when drawn on screen
            pointToPush += (Vector3)pushVector;
            ropeLineRenderer.SetPosition(1, pointToPush);
        }

        bool CheckSwingDirectionByPlayerPositon(Vector2 pivotPosition)
        {
            bool isSwingClockWise = false;
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;

            Vector3 pivotPointNew = (Vector3)pivotPosition;
            Vector3 pivotPointOld = ropeLineRenderer.GetPosition(1);
            Vector3 playerPoint = new Vector3(playerX, playerY, 0); ;

            Vector3 firstVector = pivotPointOld - pivotPointNew;
            Vector3 secondVector = playerPoint - pivotPointNew;

            Vector3 leftHandRuleVector = Vector3.Cross(firstVector, secondVector);

            if (leftHandRuleVector.z > 0)
                isSwingClockWise = true;

            return isSwingClockWise;
        }

        bool IsPivotAngleOnCounterSwingDirection()
        {
            bool isPivotVsPlayerClockWise = false;
            int closestPivotIndex = 1;

            Vector2 pivotPoint = (Vector2)ropeLineRenderer.GetPosition(closestPivotIndex);
            Vector2 playerOldPoint = (Vector2)ropeLineRenderer.GetPosition(closestPivotIndex + 1);
            Vector2 playerNewPoint = player.transform.position;

            Vector2 firstVector = playerOldPoint - pivotPoint;
            Vector2 secondVector = playerNewPoint - pivotPoint;

            Vector3 leftHandRuleVector = Vector3.Cross(firstVector, secondVector);

            if (leftHandRuleVector.z < 0)
                isPivotVsPlayerClockWise = true;

            if (isPivotVsPlayerClockWise == pivotSwingList[pivotSwingList.Count - 1])
                return true;
            else
                return false;
        }

        void ClearClosestPivotAndSwingFromList()
        {
            pivotsAdded--;
            DeleteLastLineRenderBendPoint();
        }

        void DeleteLastLineRenderBendPoint()
        {
            pivotSwingList.RemoveAt(pivotSwingList.Count - 1);

            Vector2[] tempPoints = new Vector2[ropeLineRenderer.positionCount - 1];
            tempPoints[0] = ropeLineRenderer.GetPosition(0);

            for (int i = 1; i < ropeLineRenderer.positionCount - 1; i++)
                tempPoints[i] = ropeLineRenderer.GetPosition(i + 1);

            ropeLineRenderer.positionCount--;

            for (int i = 0; i < tempPoints.Length; i++)
                ropeLineRenderer.SetPosition(i, (Vector3)tempPoints[i]);
        }

        float GetAngleBetweenPoints(Vector3 a, Vector3 b, Vector3 c)
        {
            oldAngle = currentAngle;

            float result = 0;

            float ab = Vector2.Distance(a, b);
            float bc = Vector2.Distance(b, c);
            float ac = Vector2.Distance(a, c);

            float cosB = Mathf.Pow(ac, 2) - Mathf.Pow(ab, 2) - Mathf.Pow(bc, 2);
            cosB /= (2 * ab * bc);

            result = Mathf.Acos(cosB) * Mathf.Rad2Deg;
            currentAngle = result;
            return result;
        }

        bool IsAngleGettingLarger()
        {
            GetAngleBetweenPoints(player.transform.position, ropeLineRenderer.GetPosition(1), ropeLineRenderer.GetPosition(2));

            if (currentAngle > oldAngle)
                return true;
            else
                return false;
        }

        //Rope end points are anchor point (position of this rope game object) and the position of player. These are the first and last points of linerenderer.
        //Even when the rope wraps around an obstacle and there are more linerenderer points, the first and last point remain.
        private void SetRopeEndPoints()
        {
            ropeLineRenderer.SetPosition(0, player.transform.position);     //Line starts with player
            ropeLineRenderer.SetPosition(ropeLineRenderer.positionCount - 1, transform.position);       //Line ends at anchor point 
        }

        public bool IsRopeAnchored()
        {
            return isRopeAnchored;
        }

        public Vector2 GetClosestPivot()
        {
            Vector2 pivot = ropeLineRenderer.GetPosition(1);
            return pivot;
        }
    }
}