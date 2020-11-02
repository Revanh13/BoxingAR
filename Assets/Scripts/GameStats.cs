using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
	public int NumberOfRound = 0;               //какой сейчас раунд

	[Range(1, 90)]
	public int TimeOfRound = 40;             //длительность каждого раунда

	public float TimeLeftForEndOfRound = 0f;    //время до окончания раунда

	[HideInInspector]
	public GameObject winner = null;

	public GameObject player1;
	public GameObject player2;

	private Boxer boxer1;

	public Slider healthSlider;
	public Slider staminaSlider;


	// Start is called before the first frame update
	void Start()
	{
		StartNewRound();
		boxer1 = player1.GetComponent<Boxer>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			StartNewRound();

		healthSlider.value = boxer1.HP;
		staminaSlider.value = boxer1.Stamina;
	}


	//метод начала нового раунда
	public void StartNewRound()
	{
		NumberOfRound++;
		if (NumberOfRound < 4)
		{
			TimeLeftForEndOfRound = TimeOfRound;
			StartCoroutine(timer());
		}
		else
			print("Out of Rounds!");
	}

	//счетчик каждого раунда
	IEnumerator timer()
	{
		//пока оставшееся время раунда больше-равно нуля и победителя еще нет
		while (TimeLeftForEndOfRound > -1 && winner == null)
		{
			TimeLeftForEndOfRound -= 1f;
			yield return new WaitForSeconds(1f);
		}

		if (winner != null)
			print("Knock-Out!");
		else
			print("Out of Time!");

		TryEndGame();
	}

	//попытка завершить игру
	public void TryEndGame()
	{
		bool needStopGame = false;  //надо ли завершить игру

		if (winner != null) //если есть победитель
		{
			needStopGame = true;    //завершаем игру
		}
		else if (NumberOfRound > 2) //если раунды закончились
		{
			int score1 = player1.GetComponent<Boxer>().Score;
			int score2 = player2.GetComponent<Boxer>().Score;

			//если очки первого больше - он победитель
			if (score1 > score2)
				winner = player1;
			//если очки второго больше - он победитель
			else if (score2 > score1)
				winner = player2;
			//если поровну - нет победителя
			else
				winner = null;

			needStopGame = true;    //завершаем игру
		}

		if (needStopGame)
		{
			//заканчиваем игру
			if (winner == null)
				print("No Winner:(");
			else
				print("Winner - " + winner.name);

			NumberOfRound = 4;
		}

	}

	public void GetWinnerByLoser(GameObject loser)
	{
		if (loser == player1)
			winner = player2;
		else if (loser == player2)
			winner = player1;

	}
}
