using System.Collections;
using UnityEngine;

public class Boxer : MonoBehaviour
{
	public int HP = 100;
	public float Stamina = 100f;
	public int Score = 0;

	private float staminaReloadSpead = 6f; // Скорость восстановления стамины

	public int currentAttackMode = 0;   //текущая атака

	GameStats GameStats;        //ссылка на геймстатс

	public Animator animator;

	Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		GameStats = FindObjectOfType<GameStats>();
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Stamina < 100)
			Stamina += Time.deltaTime * staminaReloadSpead;

		rb.velocity = Vector3.zero;
		//rb.angularVelocity = Vector3.zero;
	}

	//IEnumerator StaminaUp()
	//{
	//	yield return new
	//}

	//метод добавления очков при попадании по врагу
	public void AddScore()
	{
		if (currentAttackMode == 1)
		{
			//если получилась атака - прямой удар
			Score += 5;
		}
		if (currentAttackMode == 2)
		{
			//если получилась атака - боковой удар
			Score += 10;
		}
		if (currentAttackMode == 3)
		{
			//если получилась атака - апперкот
			Score += 20;
		}
	}

	//метод получения урона
	public void GotHit(int mode)
	{
		if (mode == 1)  //получил удар от прямого
		{
			HP -= 5;
			animator.SetTrigger("isHurt");
		}
		if (mode == 2)  //получил удар от бокового
		{
			HP -= 10;
			animator.SetTrigger("isHurt");
		}
		if (mode == 3)  //получил удар от апперкота
		{
			HP -= 20;
			animator.SetTrigger("isHurt");
		}

		if (HP <= 0)
		{
			HP = 0;
			//персонаж умер
			//в геймстат обьявляем победителя
			GameStats.GetWinnerByLoser(gameObject);

			animator.applyRootMotion = true;
			animator.SetTrigger("Lose");
		}
	}

	public void StaminaLose(int mode)
	{
		if (mode == 1)  // прямой
		{
			Stamina -= 10;
		}
		if (mode == 2)  // боковой
		{
			Stamina -= 25;
		}
		if (mode == 3)  // аперкот
		{
			Stamina -= 40;
		}
	}
}
