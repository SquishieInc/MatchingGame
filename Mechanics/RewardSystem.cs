using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RewardSystem : MonoBehaviour {

	public GameObject rewardPanel;
	public GameObject mainMenu;
	public GameObject collectRewardButton;
	public Text rewardAmountText;
	public Button[] dayButtons;
	public int index;

	[Space]
	private int bgindex;
	public GameObject background;
	public Sprite[] backgroundImage;

	[Space]
	public Text[] daysPlayedLoadingText;

	[Space]
	public GameObject loadingScreen;

	public int baseRewardAmount;
	private int rewardsCoinIndex;
	private int rewardedCoins;
	private String levelName;

	DateTime currentDate;
	DateTime oldDate;

	long temp;

	GameManager gameManager;

	void Start()
	{
		gameManager = GetComponent<GameManager> ();

		bgindex = PlayerPrefs.GetInt ("RewardIndex", 0);
		/*
		background.GetComponent<Image> ().sprite = backgroundImage[index];
		string backgroundName = backgroundImage [index].name;
		Debug.Log ("BACKGROUND NAME = " + backgroundName);
		*/

		Scene scene = SceneManager.GetActiveScene ();
		levelName = scene.name.ToString ();
		Debug.Log ("CURRENT SCENE NAME = " + levelName);

		rewardsCoinIndex = PlayerPrefs.GetInt ("rewardsCoinIndex", 1);
//		Debug.Log ("coinMultiplierIndex = " + rewardsCoinIndex);

		foreach (Button btn in dayButtons)
			btn.GetComponent<Button> ().interactable = false;

		//Store the current time when it starts
		currentDate = System.DateTime.Now;
		print("currentDate: " + currentDate);

		//Grab the old time from the player prefs as a long
		if (PlayerPrefs.HasKey ("sysString")) {
			temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));
		}


		//Convert the old time from binary to a DataTime variable
		DateTime oldDate = DateTime.FromBinary(temp);
		print("oldDate: " + oldDate);

		//Subtract oldDate from currentDate days will show better result
		//int daysDifference = currentDate.Day - oldDate.Day;
		int daysDifference = currentDate.Day - oldDate.Day;
		Debug.Log("DIFFERENCE IN DAYS = " + (daysDifference = currentDate.Day - oldDate.Day));

		//if more than 2 days has passed since the game has been played
		if (daysDifference > 1) 
		{
			Debug.Log("MORE THAN 1 DAY HAS PASSED");
			mainMenu.SetActive (false);
			rewardPanel.SetActive (true);

			index = 0;
			rewardsCoinIndex = 1;

			background.GetComponent<Image> ().sprite = backgroundImage[index];
			string backgroundName = backgroundImage [index + 1].name;
			Debug.Log ("BACKGROUND NAME = " + backgroundName);

			PlayerPrefs.SetInt ("RewardIndex", index);
			PlayerPrefs.SetInt ("rewardsCoinIndex", rewardsCoinIndex);
	
			foreach (Button btn in dayButtons)
				btn.GetComponent<Button> ().interactable = false;

			dayButtons [index].GetComponent<Button> ().interactable = true;
		}
			
		//if less than 1 day has passed
		if (daysDifference == 0/*  || daysDifference < 0*/) 
		{
			Debug.Log("LESS THAN 1 DAY HAS PASSED");
			index = PlayerPrefs.GetInt ("RewardIndex");
			string levelNameToLoad = levelName + index.ToString ();
			Debug.Log ("LEVEL TO LOAD = " + levelNameToLoad);

			background.GetComponent<Image> ().sprite = backgroundImage[index];
			string backgroundName = backgroundImage [index + 1].name;
			Debug.Log ("BACKGROUND NAME = " + backgroundName);

			loadingScreen.SetActive (true);
			loadingScreen.GetComponent<LoadingBarInGame>().LevelToLoad(levelNameToLoad, index);

			rewardPanel.SetActive (false);
			/*
			mainMenu.SetActive (true);
			*/

			foreach (Button btn in dayButtons)
				btn.GetComponent<Button> ().interactable = false;

			//dayButtons [index].GetComponent<Button> ().interactable = true;
		}


		//if 1 day has passed, but less than 2 days
		if (daysDifference == 1 || daysDifference < 0) 
		{
			Debug.Log("1 DAY HAS PASSED");
			mainMenu.SetActive (false);
			rewardPanel.SetActive (true);

			background.GetComponent<Image> ().sprite = backgroundImage[index];
			string backgroundName = backgroundImage [index].name;
			Debug.Log ("BACKGROUND NAME = " + backgroundName);

			foreach (Button btn in dayButtons)
				btn.GetComponent<Button> ().interactable = false;

			index = PlayerPrefs.GetInt ("RewardIndex");

			dayButtons [index].GetComponent<Button> ().interactable = true;
		}

		//index = PlayerPrefs.GetInt ("RewardIndex");

		//dayButtons [index].GetComponent<Button> ().interactable = true;

		rewardedCoins = rewardsCoinIndex * baseRewardAmount;
		//		Debug.Log ("Coins Awarded = " + rewardedCoins);

		rewardAmountText.text = "Collect " + rewardedCoins + " Coins";

		/*
		 * Old Daily Reward Method relied on checking if 24 hours had passed.
		 * 
		//Use the Subtract method and store the result as a timespan variable
		TimeSpan difference = currentDate.Subtract(oldDate);
		print("Difference: " + difference);

		//if more than 48 hours has passed since the game has been played
		if (difference.Days >= 2) {
			mainMenu.SetActive (false);
			rewardPanel.SetActive (true);

			index = 0;
			PlayerPrefs.SetInt ("RewardIndex", index);
			PlayerPrefs.SetInt ("rewardsCoinIndex", rewardsCoinIndex);

			foreach (Button btn in dayButtons)
				btn.GetComponent<Button> ().interactable = false;

			dayButtons [index].GetComponent<Button> ().interactable = true;
		}


		//if less than 24 hours has passed
		if (difference.Days < 1) {
			mainMenu.SetActive (true);
			rewardPanel.SetActive (false);

			//foreach (Button btn in dayButtons)
			//	btn.GetComponent<Button> ().interactable = false;

			//dayButtons [index].GetComponent<Button> ().interactable = true;
		}


		//if 24 hours has passed, but less than 48 hours
		if (difference.Days >= 1 && difference.Days < 2) {
			mainMenu.SetActive (false);
			rewardPanel.SetActive (true);

			//foreach (Button btn in dayButtons)
			//	btn.GetComponent<Button> ().interactable = false;

			//dayButtons [index].GetComponent<Button> ().interactable = true;
		}

		index = PlayerPrefs.GetInt ("RewardIndex", 0);

		dayButtons [index].GetComponent<Button> ().interactable = true;

		rewardedCoins = rewardsCoinIndex * baseRewardAmount;
//		Debug.Log ("Coins Awarded = " + rewardedCoins);

		rewardAmountText.text = "Collect " + rewardedCoins + " coins";
		*/



	}

	public void GiveGift()
	{
		index++;
		rewardsCoinIndex++;
//		Debug.Log ("Index = " + index);
//		Debug.Log ("coinMultiplierIndex = " + rewardsCoinIndex);

		GameManager.coins += rewardedCoins;
		gameManager.UpdateCoins ();

		if (index > 6) {
			index = 0;
			rewardsCoinIndex = 1;
			PlayerPrefs.SetInt ("RewardIndex", index);
			PlayerPrefs.SetInt ("rewardsCoinIndex", rewardsCoinIndex);

			//Saving system time
			PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
			print("Saving this date to prefs: " + System.DateTime.Now);

			//setting up menu
			mainMenu.SetActive (true);
			rewardPanel.SetActive (false);

		}
		//saving index
		PlayerPrefs.SetInt ("RewardIndex", index);
		PlayerPrefs.SetInt ("rewardsCoinIndex", rewardsCoinIndex);

		//Saving system time
		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
		print("Saving this date to prefs: " + System.DateTime.Now);

		string levelNameToLoad = levelName + index.ToString ();
		Debug.Log ("LEVEL TO LOAD = " + levelNameToLoad);

		loadingScreen.SetActive (true);
		rewardPanel.SetActive (false);
		loadingScreen.GetComponent<LoadingBarInGame>().LevelToLoad(levelNameToLoad, index);

		//setting up menu
		//mainMenu.SetActive (true);
		//rewardPanel.SetActive (false);
	}

	/*
	void OnApplicationQuit()
	{
		//Savee the current system time as a string in the player prefs class
		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());

		print("Saving this date to prefs: " + System.DateTime.Now);
	}
	*/
}
