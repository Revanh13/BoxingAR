using System.Collections;
using UnityEngine;

public class FistCollider : MonoBehaviour
{
	public Animator animator;

	private Boxer playerBoxer;
	private bool isHit = false;

	public string anotherTag;

	private void Start()
	{
		playerBoxer = gameObject.GetComponentInParent<Boxer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		//кулак ударился с врагом
		if (other.gameObject.tag == anotherTag && isHit == false)
		{
			isHit = true;

			StartCoroutine(Hit());
			Boxer enemyBoxer = other.gameObject.GetComponentInParent<Boxer>();

			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Block"))
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
		yield return new WaitForSeconds(2f);
		isHit = false;
		print("hit is ready");
	}
}
