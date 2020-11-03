using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
	public Animator animator;
	public Transform model;

    private float distance;
    private float speed = 2.0f;

    private float hitChance = 70f;
    private float blockChance = 70f;
	

	void Start()
    {
		StartCoroutine(HitChance());
	}

    void Update()
    {
        Vector3 posOfPlayer = player.position;
        transform.LookAt(posOfPlayer, Vector3.forward * (-1));

		distance = Vector3.Distance(player.position, transform.position);

		
		//print(distance); 

		if (distance > 4f)
		{
			transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
		}
		else
		{
			transform.position = transform.position;
		}
	}

    IEnumerator HitChance()
	{
        while(true)
		{
			print("itWork");

            yield return new WaitForSeconds(2f);
            float chancehit = Random.Range(1, 100);
			float chanseblock = Random.Range(1, 100);

            if (chancehit < hitChance && distance < 4.5f)
			{
				Attack(Random.Range(1, 3));
				print("itAttack");
			}
			else if (chanseblock < blockChance && distance < 4.5f)
			{
				Defence();
				print("itDefence");
			}
		}
	}

	void Attack(int mode)
	{
		
		if (mode == 1)
		{
			//если атака - вперед
			animator.SetTrigger("HitHook");
		}
		else if (mode == 2)
		{
			//если атака - боковая
			animator.SetTrigger("HitSide");
		}
		else
		{
			//если атака - апперкот
			animator.SetTrigger("HitApercut");
		}

		StartCoroutine(Hit());
		gameObject.GetComponent<Boxer>().currentAttackMode = mode;
		gameObject.GetComponent<Boxer>().StaminaLose(mode);
		
	}

	void Defence()
	{
		//создать коллайдер блока - стену с тагом "shield"
		//player.getChild(0).GetComponent<Collider>().enabled = false;

		//animator.SetBool("isBlock", true);

		animator.SetTrigger("Block");
		StartCoroutine(Hit());
	}

	IEnumerator Hit()
	{
		yield return new WaitForFixedUpdate();
		animator.applyRootMotion = true;
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		animator.applyRootMotion = false;
		gameObject.GetComponent<Boxer>().currentAttackMode = 0;
		ResetTransform();
	}

	private void ResetTransform()
    {
        model.localPosition = Vector3.zero;
        model.localRotation = Quaternion.Euler(new Vector3(0f, 33f, 0f));
    }
}
