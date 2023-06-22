using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovesGameController : MonoBehaviour {
	
	public AudioClip correctSound;
	public AudioClip wrongSound;
	public AudioClip selectedSound;

	[Space]
	public float startMovesAvailable;
	public float totalMovesAvailable;
	private float movesBar;
	private string totalMovesText;
	public GameObject movesPurchaseButton;
	public GameObject movesGO;
	private bool movesPurchased;

	[Space]
	public GameObject roundNumberPanel;
	Animation _roundNumAnim;
	private int roundNumber;
	public Text guiRoundNumber;
	public Text guiCorrectGuesses;
	public Slider progressBar;
	public Text progressText;
	public GameObject bonusTextObj;
	public Text bonusMoves;

	[Space]
	public GameObject guessFeedbackObj;
	public Text guessFeebackText;

	public GameObject levelObject;
	public GameObject[] levels;
	private int index;
	[Space]
	public GameObject gameWonPanel;

	public GameObject gameLosePanel;

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

	[Space]
	private int countGuesses;
	private int countCorrectGuesses;
	private int totalCountCorrectGuesses;
	private int gameGuesses;
	public GameObject roundUp;

	AddButtons addButtons;

	[Space]
	public GameObject gameCenterButton;
	private GameCenter gameCenter;
	public string roundsLeaderboardID;
	public string guessesLeaderboardID;

	void Awake()
	{
		puzzles = Resources.LoadAll<Sprite> ("Sprites/GamePlay/Cards/" + puzzleShapes);
	//	bgImage = Resources.Load<Sprite> ("Sprites/GamePlay/Back/" + bgImageName);

		guiRoundNumber.text = "Round " + roundNumber;
		guiCorrectGuesses.text = "" + totalCountCorrectGuesses + " Correct";
	}

	void Start()
	{

		movesPurchased = false;
		//Debug.Log("Moves Purchased = " + movesPurchased);

		/*if (GameManager.coins < 30) {
			movesPurchased = true;
		}*/

		Debug.Log("Moves Purchased = " + movesPurchased);
		/*
		index = 0;
		roundNumberPanel.SetActive (true);

		//toggle off their renderer
		foreach (GameObject go in levels)
			go.SetActive (false);

		//we toggle the selected character
		if (levels [index])
			levels [index].SetActive (true);

		*/

		roundNumber = 0;

//		Debug.Log ("Amount of Levels to select = " + levels.Length);
//		Debug.Log ("first level is  = " + levels[index]);
		#if UNITY_IOS
		gameCenter = GetComponent<GameCenter> ();


		gameCenterButton.SetActive(true);
		#endif

		totalMovesAvailable = startMovesAvailable;

		StartCoroutine (NewRoundBuild());
		/*
		GetButtons ();
		AddGamePuzzles ();
		AddListeners ();
		Shuffle (gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;

		Debug.Log ("Moves Game ");
		totalMovesText = "Moves " + totalMovesAvailable;
		progressText.text = totalMovesText;
		movesBar = 1 / totalMovesAvailable;
		*/
	}

	public void NewRoundGrid()
	{
		GetButtons ();
		AddGamePuzzles ();
		AddListeners ();
		Shuffle (gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;

//		Debug.Log ("Moves Game ");
		totalMovesText = "Moves " + totalMovesAvailable;
		progressText.text = totalMovesText;
		movesBar = 1 / totalMovesAvailable;

		progressBar.value = 1;
	}

	void GetButtons()
	{
//		Debug.Log ("GetButtons called");
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");
//		Debug.Log ("Total buttons found = " + objects.Length);

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

		if(movesPurchased == false)
		{
			//Debug.Log("Moves Purchased = " + movesPurchased);
		movesPurchaseButton.SetActive (true);
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
		AudioSource audio = GetComponent<AudioSource>();
		string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
		audio.clip = selectedSound;
		audio.Play ();
		//Debug.Log ("You are picking a Puzzle Button named " + name);

		if (!firstGuess) 
		{
			firstGuess = true;
			firstGuessIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			firstGuessPuzzle = gamePuzzles [firstGuessIndex].name;
			btns [firstGuessIndex].image.sprite = gamePuzzles [firstGuessIndex];
			btns [firstGuessIndex].interactable = false;
		} 

		else if(!secondGuess)
		{
			secondGuess = true;
			secondGuessIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			secondGuessPuzzle = gamePuzzles [secondGuessIndex].name;
			btns [secondGuessIndex].image.sprite = gamePuzzles [secondGuessIndex];
			btns [secondGuessIndex].interactable = false;

			countGuesses++;

			StartCoroutine (CheckIfThePuzzleMatches ());

			if (firstGuessPuzzle == secondGuessPuzzle) 
			{
				if(totalMovesAvailable > 0)
				{
					//Debug.Log ("The Puzzles Match! ^^,");
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
				progressBar.value -= movesBar;
				progressText.text = totalMovesText;
				//Debug.Log ("The Puzzles Dont Match! =[");

				if (totalMovesAvailable == 0) 
				{
					//Debug.Log ("Game Lost!");
					//Debug.Log ("Total correct guesses = " + totalCountCorrectGuesses);

					Application.CaptureScreenshot ("Screenshot.png", 0);
					/*
					#if !UNITY_EDITOR
					string url = Application.persistentDataPath +"/"+Variables.ImageName;
					#endif
					*/
					string path = System.IO.Path.Combine (Application.persistentDataPath, "Screenshot.png");
					Debug.Log ("ScreenShot is saved here - " + path);
					print(Application.dataPath);

					if(movesPurchased)
					{
					Invoke ("GameOverScreen", 1f);
					}
					else if(!movesPurchased)
					{
					Invoke ("MovesGameOverScreen", 1f);
					}

					//gameLosePanel.SetActive (true);
					#if UNITY_IOS

					gameCenter.ReportScore (index, roundsLeaderboardID);
					gameCenter.ReportScore (totalCountCorrectGuesses, guessesLeaderboardID);

					#endif

					roundUp.GetComponent<RoundUp> ().RoundUpText (roundNumber, totalCountCorrectGuesses);

					guiCorrectGuesses.text = "" + totalCountCorrectGuesses + " Correct";
				}
			}
		}
	}

	IEnumerator CheckIfThePuzzleMatches()
	{
		AudioSource audio = GetComponent<AudioSource>();
		yield return new WaitForSeconds (0.5f);

		guessFeedbackObj.SetActive (true);

		if (firstGuessPuzzle == secondGuessPuzzle) 
		{
			guessFeebackText.text = "Correct";
			audio.clip = correctSound;
			audio.Play ();
			yield return new WaitForSeconds (.1f);
			btns [firstGuessIndex].interactable = false;
			btns [secondGuessIndex].interactable = false;

			btns [firstGuessIndex].image.color = new Color (0, 0, 0, 0);
			btns [secondGuessIndex].image.color = new Color (0, 0, 0, 0);

			CheckIfTheGameIsFinished ();
		} 
		else 
		{
			guessFeebackText.text = "Wrong!";
			audio.clip = wrongSound;
			audio.Play ();

			yield return new WaitForSeconds (.1f);

			btns [firstGuessIndex].image.sprite = bgImage;
			btns [secondGuessIndex].image.sprite = bgImage;

			btns [firstGuessIndex].interactable = true;
			btns [secondGuessIndex].interactable = true;

			yield return new WaitForSeconds (.1f);

			Animator buttonAnimator1 = btns [firstGuessIndex].GetComponent<Animator> ();
			Animator buttonAnimator2 = btns [secondGuessIndex].GetComponent<Animator> ();

			buttonAnimator1.SetTrigger(btns [firstGuessIndex].animationTriggers.normalTrigger);
			buttonAnimator2.SetTrigger(btns [secondGuessIndex].animationTriggers.normalTrigger);

		}

		yield return new WaitForSeconds (.1f);

		firstGuess = secondGuess = false;

		yield return new WaitForSeconds (2f);
		guessFeedbackObj.SetActive (false);
	}

	void CheckIfTheGameIsFinished ()
	{
		countCorrectGuesses++;
		totalCountCorrectGuesses++;

		guiCorrectGuesses.text = "" + totalCountCorrectGuesses + " Correct";

		if (countCorrectGuesses == gameGuesses) 
		{
			//Debug.Log ("Game Finished");
			//Debug.Log ("You got a total of " + totalCountCorrectGuesses + " many guess(es) correct!");

			GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");

			for (int i = 0; i < objects.Length; i++) 
			{
				//Destroy (objects[i].gameObject);
			}

			//roundNumberPanel.SetActive (true);
			StartCoroutine(NewRoundBuild ());
			movesPurchaseButton.SetActive (false);
			index++;
			totalMovesAvailable += countCorrectGuesses;

			//roundNumberPanel.SetActive (true);
		}
	}

	IEnumerator NewRoundBuild()
	{
		roundNumber++;
		guiRoundNumber.text = "Round " + roundNumber;
		//Debug.Log("RoundNumber = " + roundNumber);
		levelObject.GetComponent<LevelSelect> ().GoUpALevel ();
		bonusTextObj.SetActive (true);
		if (countCorrectGuesses > 0) {
			bonusMoves.text = "+" + countCorrectGuesses + " MOVES";
		}

		yield return new WaitForSeconds (1f);
		roundNumberPanel.SetActive (true);
		Animation _roundNumAnim = roundNumberPanel.GetComponent<Animation> ();
		Animator _roundNumAnimator = roundNumberPanel.GetComponent<Animator> ();
		_roundNumAnimator.Play ("RoundPanel", 0);
		_roundNumAnim.Play ();

		yield return new WaitForSeconds (_roundNumAnim.clip.length);
		roundNumberPanel.SetActive (false);
		bonusTextObj.SetActive (false);
		countCorrectGuesses = 0;
		btns.Clear ();
		gamePuzzles.Clear ();
		/*
		if(movesPurchased == false)
		{Debug.Log("Moves Purchased = " + movesPurchased);
			movesPurchaseButton.SetActive (true);
		}
		if(movesPurchased == true)
		{Debug.Log("Moves Purchased = " + movesPurchased);
			movesPurchaseButton.SetActive (false);
		}
		*/
		//roundNumberPanel.SetActive (true);
		//levelObject.GetComponent<LevelSelect> ().GoUpALevel ();
		levelObject.GetComponent<LevelSelect> ().ActivateLevel ();
		NewRoundGrid ();


//		Debug.Log ("do something to start next round!");
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

	void GameOverScreen()
	{
		//btns.Clear ();
		//gamePuzzles.Clear ();
		gameLosePanel.SetActive (true);
	}

	void MovesGameOverScreen()
	{
		movesGO.SetActive (true);
	}

	public void CloseMovesGameOverScreen()
	{
		gameLosePanel.SetActive (true);
		movesGO.SetActive (false);
	}

	public void MovesPurchased()
	{
		totalMovesAvailable += 5;
		Debug.Log ("MOVES GAME CONTROLLER; New Moves Available = " + totalMovesAvailable);
		totalMovesText = "Moves " + totalMovesAvailable;
		progressText.text = totalMovesText;
		progressBar.value = 1;
		movesBar = 1 / totalMovesAvailable;
		Debug.Log ("Moves bar has been reset to - " + movesBar);
		bonusTextObj.SetActive (true);
		movesPurchased = true;
		bonusMoves.text = "+ 5 MOVES";
	}
	/*
	void Update()
	{
			if (totalMovesAvailable == 0) 
			{
				Debug.Log ("Game Lost!");
				Debug.Log ("Total correct guesses = " + totalCountCorrectGuesses);

				Application.CaptureScreenshot("Screenshot.png");
				gameLosePanel.SetActive (true);

			gameCenter.ReportScore (index, roundsLeaderboardID);
			gameCenter.ReportScore (totalCountCorrectGuesses, guessesLeaderboardID);

			roundUp.GetComponent<RoundUp> ().RoundUpText (roundNumber, totalCountCorrectGuesses);

				//GameManager.Instance.LoseLive ();
			}
	}
	*/
}
