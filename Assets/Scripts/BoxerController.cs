using System.Collections;
using UnityEngine;

public class BoxerController : MonoBehaviour
{
	public GameObject player;        //ссылка на игрока
	public GameObject evil;          //ссылка на врага
	public PlayerMove playerMove;
	[HideInInspector] public bool ButtonLock = false;  //запрет на запуск новых анимаций во время выполнения анимаций

	private Animator animator;

	private void Start()
	{
		animator = player.transform.GetChild(0).gameObject.GetComponent<Animator>();
		playerMove = player.GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{
		//   смотрим на врага
		Vector3 posOfEvil = evil.transform.position;
		//posOfEvil.z = player.transform.position.z;     //если начнет смотреть в пол или вверх может поможет
		player.transform.LookAt(posOfEvil, Vector3.forward * (-1));

		//   ходьба персонажем
		//
		//
		//

		//   снимаем лок на анимации
		//if (player.GetComponent<Animator>().названиеТекущейАнимации == "idle")
		//      buttonLock = false;
	}

	IEnumerator Hit()
	{
		ButtonLock = true;
		playerMove.buttonLock = true;
		yield return new WaitForFixedUpdate();
		animator.applyRootMotion = true;
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		animator.applyRootMotion = false;
		playerMove.buttonLock = false;
		ButtonLock = false;
	}

	//кнопка атаки вперед
	public void buttonAttackForward() { Attack(1); }

	//кнопка атаки боковой
	public void buttonAttackSide() { Attack(2); }

	//кнопка атаки апперкот
	public void buttonAttackApper() { Attack(3); }

	public void buttonDefence() { Defence(); }

	//запуск атаки
	void Attack(int mode)
	{
		if (ButtonLock == false)
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
			player.GetComponent<Boxer>().currentAttackMode = mode;
		}
	}

	void Defence()
	{
		//создать коллайдер блока - стену с тагом "shield"
		//player.getChild(0).GetComponent<Collider>().enabled = false;
		animator.SetBool("isBlock", true);
		ButtonLock = true;
	}
}
