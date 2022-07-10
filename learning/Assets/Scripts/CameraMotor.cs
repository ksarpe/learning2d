using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void FixedUpdate()
    {
        Vector3 desired = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if(deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                desired.x = deltaX - boundX;
            }
            else
            {
                desired.x = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                desired.y = deltaY - boundY;
            }
            else
            {
                desired.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(desired.x, desired.y, 0);
    }

}
