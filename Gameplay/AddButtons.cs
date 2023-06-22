using UnityEngine;
using System.Collections;

public class AddButtons : MonoBehaviour {

	[SerializeField]
	private int numberOfButtons;

	[SerializeField]
	private Transform gameGridT;

	[SerializeField]
	private GameObject btn;

	MovesGameController movesGameController;

	TimeGameController timerGameController;


	void Awake()
	{
		for (int i = 0; i < numberOfButtons; i++) 
		{
			GameObject button = Instantiate (btn);
			button.name = "" + i;
			button.transform.SetParent (gameGridT, false);
		}
	}

	public void MovesNewRound(int updateButtonCount)
	{
		MovesGameController movesGameController = GetComponent<MovesGameController>();
		numberOfButtons += updateButtonCount;
		for (int i = 0; i < numberOfButtons; i++) 
		{
			GameObject button = Instantiate (btn);
			button.name = "" + i;
			button.transform.SetParent (gameGridT, false);
		}
		movesGameController.NewRoundGrid ();
	}

	public void TimerNewRound(int updateButtonCount)
	{
		TimeGameController timerGameController = GetComponent<TimeGameController>();
		numberOfButtons += updateButtonCount;
		for (int i = 0; i < numberOfButtons; i++) 
		{
			GameObject button = Instantiate (btn);
			button.name = "" + i;
			button.transform.SetParent (gameGridT, false);
		}
		timerGameController.NewRoundGrid ();
	}
}
