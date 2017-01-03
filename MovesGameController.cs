using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovesGameController : MonoBehaviour {

	public float startMovesAvailable;
	private float totalMovesAvailable;
	private float movesBar;
	private string totalMovesText;

	public Image progressBar;
	public Text progressText;
	public Text bonusMoves;

	public GameObject gameWonPanel;

	public GameObject gameLosePanel;

	[SerializeField]
	private int numberOfRounds;


	[SerializeField]
	private Sprite bgImage;

	//[SerializeField]
	//private string bgImageName;

	public Sprite[] puzzles;

	public List<Sprite> gamePuzzles = new List<Sprite>();

	public List<Button> btns = new List<Button>();

	private bool firstGuess, secondGuess;
	private string firstGuessPuzzle, secondGuessPuzzle;
	private int firstGuessIndex, secondGuessIndex;

	private int countGuesses;
	private int countCorrectGuesses;
	private int totalCountCorrectGuesses;
	private int gameGuesses;

	AddButtons addButtons;

	void Awake()
	{
		puzzles = Resources.LoadAll<Sprite> ("Sprites/GamePlay/Cards");
	//	bgImage = Resources.Load<Sprite> ("Sprites/GamePlay/Back/" + bgImageName);

	}

	void Start()
	{
		totalMovesAvailable = startMovesAvailable;

		GetButtons ();
		AddGamePuzzles ();
		AddListeners ();
		Shuffle (gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;

			Debug.Log ("Moves Game ");
			totalMovesText = "Moves " + totalMovesAvailable;
			progressText.text = totalMovesText;
			movesBar = 1 / totalMovesAvailable;

	}

	public void NewRoundGrid()
	{
		GetButtons ();
		AddGamePuzzles ();
		AddListeners ();
		Shuffle (gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;

		Debug.Log ("Moves Game ");
		totalMovesText = "Moves " + totalMovesAvailable;
		progressText.text = totalMovesText;
		movesBar = 1 / totalMovesAvailable;

		progressBar.fillAmount = 1;
	}

	void GetButtons()
	{
		Debug.Log ("GetButtons called");
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");
		Debug.Log ("Total buttons found = " + objects.Length);

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
		Debug.Log ("You are picking a Puzzle Button named " + name);

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

			if (firstGuessPuzzle == secondGuessPuzzle) 
			{
				if(totalMovesAvailable > 0)
				{
					Debug.Log ("The Puzzles Match! ^^,");
					//totalMovesAvailable--;
					//totalMovesText = "Moves " + totalMovesAvailable;
					//progressBar.fillAmount -= movesBar;
					//progressText.text = totalMovesText;
				}
			} 
			else 
			{
				totalMovesAvailable--;
				totalMovesText = "Moves " + totalMovesAvailable;
				progressBar.fillAmount -= movesBar;
				progressText.text = totalMovesText;
				Debug.Log ("The Puzzles Dont Match! =[");
			}
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
			Debug.Log ("Game Finished");
			Debug.Log ("You got a total of " + totalCountCorrectGuesses + " many guess(es) correct!");

			GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");

			for (int i = 0; i < objects.Length; i++) 
			{
				Destroy (objects[i].gameObject);
			}

			StartCoroutine(NewRoundBuild ());
			numberOfRounds++;
			totalMovesAvailable += countCorrectGuesses;
			bonusMoves.text = "+" + countCorrectGuesses + " MOVES";
		}
	}

	IEnumerator NewRoundBuild()
	{
		yield return new WaitForSeconds (0.5f);

		countCorrectGuesses = 0;
		btns.Clear ();
		gamePuzzles.Clear ();

		AddButtons addButtons = GetComponent<AddButtons>();
		if (numberOfRounds > 0) {
			addButtons.MovesNewRound (2);
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
			if (totalMovesAvailable == 0) 
			{
				Debug.Log ("Game Lost!");
				gameLosePanel.SetActive (true);
				//GameManager.Instance.LoseLive ();
			}
	}
}
