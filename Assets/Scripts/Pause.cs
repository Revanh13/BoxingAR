using UnityEngine;

public class Pause : MonoBehaviour
{
	public GameObject objectAR;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			buttonPause();

		autoPauser();
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
		if (Time.timeScale == 1f)
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
			Time.timeScale = 0f;
		}
		//если надо выключить паузу
		else
		{
			print("unpaused");
			Time.timeScale = 1f;
		}
	}
}
