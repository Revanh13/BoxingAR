using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButton : MonoBehaviour 
{
	public Animator animator;

	private void OnMouseDown()
	{
		animator.SetBool("isBlock", true);
	}

	private void OnMouseUp()
	{
		animator.SetBool("isBlock", false);
	}
}
