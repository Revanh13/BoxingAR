using easyar;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
	private Boxer boxer2;

	public Slider healthSlider;
	public Slider staminaSlider;

	public Slider healthSliderEnemy;
	public Slider staminaSliderEnemy;

	public Text textTime;

	public GameObject nextRound;
	private bool isFirst = true;

	public GameObject wining;
	public GameObject losing;

	// Start is called before the first frame update
	void Start()
	{
		StartNewRound();
		boxer1 = player1.GetComponent<Boxer>();
		boxer2 = player2.GetComponent<Boxer>();
		nextRound.SetActive(false);
		wining.SetActive(false);
		losing.SetActive(false);
	}

	void Update()
	{
		healthSlider.value = boxer1.HP;
		staminaSlider.value = boxer1.Stamina;

		healthSliderEnemy.value = boxer2.HP;
		staminaSliderEnemy.value = boxer2.Stamina;
	}


	//метод начала нового раунда
	public void StartNewRound()
	{
		NumberOfRound++;
		if (NumberOfRound < 4)
		{
			TimeLeftForEndOfRound = TimeOfRound;
			Time.timeScale = 1.0f;
			player1.transform.position = new Vector3(-6.67f, -5.66f, -4.92f);
			player2.transform.position = new Vector3(6.55f, 7.41f, -4.92f);
			//player1.SetActive(true);
			//player2.SetActive(true);
			//player2.GetComponent<Enemy>().StartCorHit();
			
			if(!isFirst)
			{
				if (boxer1.HP < 71) boxer1.HP += 30;
				else boxer1.HP = 100;
				if (boxer2.HP < 71) boxer2.HP += 30;
				else boxer2.HP = 100;
				boxer1.Stamina = 100;
				boxer2.Stamina = 100;
			}
			isFirst = false;
			nextRound.SetActive(false);
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
			textTime.text = TimeLeftForEndOfRound.ToString();
			TimeLeftForEndOfRound -= 1f;
			yield return new WaitForSeconds(1f);
		}

		if (winner != null)
			print("Knock-Out!");
		else
		{
			GameObject.FindObjectOfType<AudioManager>().Play("Gong");
			print("Out of Time!");
			nextRound.SetActive(true);
			//Time.timeScale = 0.0f;
			//player1.SetActive(false);
			//player2.SetActive(false);
			//player2.GetComponent<Enemy>().StopAllCoroutines();
		}

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
			{
				print("Winner - " + winner.name);
				
				if (winner == player1)
				{
					wining.SetActive(true);
					//player1.SetActive(false);
					//player2.SetActive(false);
					//player2.GetComponent<Enemy>().StopAllCoroutines();
				}
				else
				{
					winner = player2;
					losing.SetActive(true);
					//player1.SetActive(false);
					//player2.SetActive(false);
					//player2.GetComponent<Enemy>().StopAllCoroutines();
				}
			}

			NumberOfRound = 4;
		}

	}

	public void GetWinnerByLoser(GameObject loser)
	{
		if (loser == player1)
		{
			winner = player2;
			losing.SetActive(true);
			//player1.SetActive(false);
			//player2.SetActive(false);
			//player2.GetComponent<Enemy>().StopAllCoroutines();
		}
		else if (loser == player2)
		{
			winner = player1;
			wining.SetActive(true);
			//player1.SetActive(false);
			//player2.SetActive(false);
			//player2.GetComponent<Enemy>().StopAllCoroutines();
		}

	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(0);
	}
}
