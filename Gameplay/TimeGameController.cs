using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TimeGameController : MonoBehaviour {

	public float starterTimer;
	public float timer;
	public float adjustedTimer;
	public float timeInSeconds;
	public float timeToAdd;
	public float secondsToBeAdded;
	private bool pauseTimer;

	public Image progressBar;
	public Text progressText;
	public Text bonusTime;

	public GameObject gameWonPanel;

	public GameObject gameLosePanel;

	[SerializeField]
	private int numberOfRounds;

	[SerializeField]
	private Sprite bgImage;

	[SerializeField]
	private string puzzleShapes;

	//[SerializeField]
	//private string bgImageName;

	public Sprite[] puzzles;

	public List<Sprite> gamePuzzles = new List<Sprite>();

	public List<Button> btns = new List<Button>();

	private bool firstGuess, secondGuess;
	private string firstGuessPuzzle, secondGuessPuzzle;
	private int firstGuessIndex, secondGuessIndex;

	private int countGuesses;
	private float countCorrectGuesses;
	private int totalCountCorrectGuesses;
	private int gameGuesses;

	AddButtons addButtons;

	void Awake()
	{
		puzzles = Resources.LoadAll<Sprite> ("Sprites/GamePlay/Cards/" + puzzleShapes);
	//	bgImage = Resources.Load<Sprite> ("Sprites/GamePlay/Back/" + bgImageName);

	}

	void Start()
	{
		timer = starterTimer;
		adjustedTimer = 1 / (timer * 60);

		GetButtons ();
		AddGamePuzzles ();
		AddListeners ();
		Shuffle (gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;


//		Debug.Log ("Timer Game ");
		timeInSeconds = timer * 60;

		pauseTimer = false;

	}

	public void NewRoundGrid()
	{
		GetButtons ();
		AddGamePuzzles ();
		AddListeners ();
		Shuffle (gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;

		timeInSeconds = timer * 60;

		progressBar.fillAmount = 1f;

		pauseTimer = false;
	}

	void GetButtons()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");

		for (int i = 0; i < objects.Length; i++) 
		{
			btns.Add (objects[i].GetComponent<Button>());
			btns [i].image.sprite = bgImage;
		}
	}

	void AddGamePuzzles()
	{
		int looper = btns.Count;
		int index = 0;

		for (int i = 0; i < looper; i++) 
		{
			if (index == looper / 2) 
			{
				index = 0;
			}
			gamePuzzles.Add (puzzles [index]);

			index++;
		}
	}

	void AddListeners()
	{
		foreach(Button btn in btns) 
		{
			btn.onClick.AddListener(() => PickAPuzzle());
		}
	}

	public void PickAPuzzle()
	{
		string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
//		Debug.Log ("You are picking a Puzzle Button named " + name);
		if (!firstGuess) 
		{
			firstGuess = true;
			firstGuessIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			firstGuessPuzzle = gamePuzzles [firstGuessIndex].name;
			btns [firstGuessIndex].image.sprite = gamePuzzles [firstGuessIndex];
		} 

		else if(!secondGuess)
		{
			secondGuess = true;
			secondGuessIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			secondGuessPuzzle = gamePuzzles [secondGuessIndex].name;
			btns [secondGuessIndex].image.sprite = gamePuzzles [secondGuessIndex];

			countGuesses++;

			StartCoroutine (CheckIfThePuzzleMatches ());
		}
	}

	IEnumerator CheckIfThePuzzleMatches()
	{
		yield return new WaitForSeconds (1f);

		if (firstGuessPuzzle == secondGuessPuzzle) 
		{
			yield return new WaitForSeconds (.5f);
			btns [firstGuessIndex].interactable = false;
			btns [secondGuessIndex].interactable = false;

			btns [firstGuessIndex].image.color = new Color (0, 0, 0, 0);
			btns [secondGuessIndex].image.color = new Color (0, 0, 0, 0);
			CheckIfTheGameIsFinished ();
		} 
		else 
		{
			yield return new WaitForSeconds (.5f);

			btns [firstGuessIndex].image.sprite = bgImage;
			btns [secondGuessIndex].image.sprite = bgImage;
		}

		yield return new WaitForSeconds (.5f);

		firstGuess = secondGuess = false;
	}

	void CheckIfTheGameIsFinished ()
	{
		countCorrectGuesses++;
		totalCountCorrectGuesses++;

		if (countCorrectGuesses == gameGuesses) 
		{
		//	Debug.Log ("Game Finished");
		//	Debug.Log ("You got a total of " + totalCountCorrectGuesses + " many guess(es) correct!");
		//	Debug.Log ("You got a total of " + countCorrectGuesses + " countCorrectGuesses");

			GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");

			for (int i = 0; i < objects.Length; i++) 
			{
				Destroy (objects[i].gameObject);
			}
		//	float addToTimer = (countCorrectGuesses/10);
		//	Debug.Log ("You got a total of seconds added " + addToTimer);
			pauseTimer = true;
			timer = (timeInSeconds/60);
			Debug.Log ("Timer = " + timer + " Seconds");
			timeToAdd = (countCorrectGuesses * (1 / (timer * 60)));
			timer += timeToAdd;
		//	Debug.Log ("Timer now = " + timer + " Seconds");
			adjustedTimer = 1 / (timer * 60);
		//	Debug.Log ("You got a total of seconds added " + (countCorrectGuesses * (1 / (timer * 60))) + " which is added to " + timer);
			StartCoroutine(NewRoundBuild ());
			numberOfRounds++;
		//	Debug.Log("adding " + addToTimer + " Seconds to Timer");
			bonusTime.text = "+" + Mathf.Round((countCorrectGuesses * adjustedTimer)*60) + " SECONDS";
		}
	}

	IEnumerator NewRoundBuild()
	{
		yield return new WaitForSeconds (0.5f);

		countCorrectGuesses = 0;
		btns.Clear ();
		gamePuzzles.Clear ();

//		adjustedTimer = 0;


		AddButtons addButtons = GetComponent<AddButtons>();
		if (numberOfRounds > 0) {
			addButtons.TimerNewRound (2);
		}
	}

	void Shuffle(List<Sprite> list)
	{
		for (int i = 0; i < list.Count; i++) 
		{
			Sprite temp = list [i];
			int randomIndex = Random.Range(0, list.Count);
			list [i] = list [randomIndex];
			list [randomIndex] = temp;
		}
	}

	void Update()
	{
			if (progressBar.fillAmount > 0) 
			{
				int minutes = Mathf.FloorToInt (timeInSeconds / 60F);
				int seconds = Mathf.FloorToInt (timeInSeconds - minutes * 60);

				string niceTime = string.Format ("{0:0}:{1:00}", minutes, seconds);
				progressText.text = "Timer: " + niceTime;
				

			if (!pauseTimer) {
				timeInSeconds -= Time.deltaTime;
			}
//					int minutes = Mathf.FloorToInt (timeInSeconds / 60F);
//					int seconds = Mathf.FloorToInt (timeInSeconds - minutes * 60);

//					string niceTime = string.Format ("{0:0}:{1:00}", minutes, seconds);
//					progressText.text = niceTime;

//					adjustedTimer = 1 / (timer * 60);
					progressBar.fillAmount -= adjustedTimer * Time.deltaTime;

					if (progressBar.fillAmount <= 0f) 
					{
						print ("you lose");

						progressText.text = "";

						gameLosePanel.SetActive (true);
//						GameManager.Instance.LoseLive ();
					}
			}
	}
}
