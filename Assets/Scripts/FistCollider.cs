﻿using System.Collections;
using UnityEngine;

public class FistCollider : MonoBehaviour
{
	public Animator animator;

	public GameObject particles;
	public Transform particlesSpawnPoint;

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
			Boxer enemyBoxer = other.gameObject.GetComponentInParent<Boxer>();
			if (enemyBoxer.HP > 0)
			{
				isHit = true;

				StartCoroutine(Hit());

				if (animator.GetCurrentAnimatorStateInfo(0).IsName("Block"))
				{
					if (playerBoxer.Stamina >= 20f)
						playerBoxer.Stamina -= 20f;
					else playerBoxer.Stamina = 1f;
				}
				else
				{
					enemyBoxer.GotHit(playerBoxer.currentAttackMode);
					GameObject.FindObjectOfType<AudioManager>().Play("Hit");
					if (gameObject.CompareTag("Player"))
						Instantiate(particles, particlesSpawnPoint.position, Quaternion.identity);
					playerBoxer.AddScore();
				}
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
