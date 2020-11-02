using easyar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public FloatingJoystick floatingJoystick;
    private float speed = 5f;
    private Animator animator;

	private void Start()
	{
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
	}

	public void Update()
    {
        if (floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - floatingJoystick.Horizontal,
                transform.position.y + floatingJoystick.Vertical, transform.position.z), Time.deltaTime * speed);

            if (Mathf.Abs(floatingJoystick.Vertical) > Mathf.Abs(floatingJoystick.Horizontal))
            {
                animator.SetFloat("SpeedWalk", Mathf.Abs(floatingJoystick.Vertical));
                animator.SetBool("isSide", false);
            }
            else
            {
                animator.SetFloat("SpeedSideWalk", Mathf.Abs(floatingJoystick.Horizontal));
                animator.SetBool("isSide", true);
            }
        }
        else
        {
            animator.SetFloat("SpeedWalk", 0);
            animator.SetFloat("SpeedSideWalk", 0);
        }
    }
}
