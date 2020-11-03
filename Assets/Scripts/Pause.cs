using UnityEngine;

public class Pause : MonoBehaviour
{
	public GameObject objectAR;
	public Enemy enemy;
	private bool isFirst = true;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			buttonPause();

		if (objectAR.activeInHierarchy)
			PauseAll(false);

		if (!objectAR.activeInHierarchy)
			PauseAll(true);
		//autoPauser();
	}

	//автоматическое установление паузы при потере метки
	void autoPauser()
	{
		if (!objectAR.activeSelf)
			PauseAll(true);
	}

	//кнопка паузы
	public void buttonPause()
	{
		if (Time.timeScale == 1.0f)
		{
			PauseAll(true);
		}
		else
		{
			PauseAll(false);
		}
	}

	//паузим или распаузим игру
	void PauseAll(bool setOn)
	{
		//если надо включить паузу
		if (setOn)
		{
			print("paused");
			Time.timeScale = 0.0f;
			StopAllCoroutines();
			isFirst = true;
		}
		//если надо выключить паузу
		else
		{
			print("unpaused");
			Time.timeScale = 1.0f;

			if (isFirst)
			{
				isFirst = false;
				enemy.StartCorHit();
			}
		}
	}
}
