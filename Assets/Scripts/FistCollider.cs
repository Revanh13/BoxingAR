using System.Collections;
using UnityEngine;

public class FistCollider : MonoBehaviour
{
	public Animator animator;

	private Boxer playerBoxer;
	private bool isHit = false;

	private void Start()
	{
		playerBoxer = gameObject.GetComponentInParent<Boxer>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		//кулак ударился с врагом
		if (collision.gameObject.tag == "Enemy" && isHit == false)
		{
			isHit = true;			

			StartCoroutine(Hit());
			Boxer enemyBoxer = collision.gameObject.GetComponentInParent<Boxer>();

			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			{
				if (playerBoxer.Stamina >= 20f)
					playerBoxer.Stamina -= 20f;
				else playerBoxer.Stamina = 1f;
			}
			else
			{
				enemyBoxer.GotHit(playerBoxer.currentAttackMode);
			}
		}
	}

	IEnumerator Hit()
	{
		yield return new WaitForSeconds(4f);
		isHit = false;
		print("hit is ready");
	}
}
