using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public FloatingJoystick floatingJoystick;
	private float speed = 3.5f;
	private Animator animator;
	private Transform transformModel;

	public GameObject buttonSafe;

	[HideInInspector] public bool buttonLock = false;


	private void Start()
	{
		animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
		transformModel = transform.GetChild(0).gameObject.transform;
		ResetTransform();
		buttonSafe.SetActive(false);
	}

	public void Update()
	{
		if (floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0)
		{
			if (buttonLock == false)
			{
				buttonSafe.SetActive(true);

				//                                                                         заменить на + в билде
				transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + floatingJoystick.Horizontal,
					transform.position.y + floatingJoystick.Vertical, transform.position.z), Time.deltaTime * speed);

				if (floatingJoystick.Vertical > 0) animator.SetBool("isBack", false);
				else animator.SetBool("isBack", true);
				if (floatingJoystick.Horizontal > 0) animator.SetBool("isRight", true);
				else animator.SetBool("isRight", false);

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
		}
		else
		{
			buttonSafe.SetActive(false);
			animator.SetFloat("SpeedWalk", 0);
			animator.SetFloat("SpeedSideWalk", 0);
		}
	}

	public void ResetTransform()
	{
		transformModel.localPosition = Vector3.zero;
		transformModel.localRotation = Quaternion.Euler(new Vector3(0f, 33f, 0f));
	}
}
