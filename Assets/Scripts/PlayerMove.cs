using easyar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public FloatingJoystick floatingJoystick;
    private float speed = 10f;

    public void Update()
    {
        Vector3 direction = Vector3.up * floatingJoystick.Vertical + Vector3.left * floatingJoystick.Horizontal;
        direction.z = transform.position.z;
        
        if (floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0)
		{
            transform.position = Vector3.Lerp(transform.position, direction, Time.deltaTime * speed);
		}
    }
}
