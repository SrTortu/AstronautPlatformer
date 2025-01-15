using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]private Transform shootPoint;
    private void Start()
    {
        shootPoint = transform.Find("shoot_point");
    }
    public void Aim(Vector3 position)
    {
        Vector3 direction = position - transform.position;

     

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float rotationAmount;

        // Invert gun relative to mouse
        Vector3 localScale = transform.localScale;
       // localScale.y = angle > 90 || angle < -90 ? -1 : 1;
        localScale.x = angle > 90 || angle < -90 ? -1 : 1;
        rotationAmount = angle > 90 || angle < -90 ? 180 : 0;
        angle = angle > 90 || angle < -90 ? angle*-1 : angle*1;
        transform.rotation = Quaternion.Euler(rotationAmount, 0, angle);
        transform.localScale = localScale;
        
    }

    public void FlipShootPoint (float angle)
    {
        
        //shootPoint.rotation = Quaternion.Euler(0, angle, 0);
        //Debug.Log($"entre shoot poitn $angle"+ angle);
    }
}
