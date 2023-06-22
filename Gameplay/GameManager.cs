using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public Text coinText;
	public static int coins;
	public Text movesPurchaseText;
	public Button coinsPurchaseButton;
	public Button coinsPurchaseButtonGO;
	private bool extraMovesPurchased;

	public GameObject movesManager;
//	public GameObject timeManager;
	public GameObject movesPurchaseButton;

	private static GameManager _instance;

	public static GameManager Instance
	{
		get{
			if (_instance == null) {
				GameObject gm = new GameObject ("GameManager");
				gm.AddComponent<FacebookManager> ();
			}

			return _instance;
		}
	}

	void Start()
	{
		coins = PlayerPrefs.GetInt ("coins");
		Debug.Log("Coins = " + coins);
		coinText.text = coins + " : Coins";

		if (coins < 30) {
			movesPurchaseText.text = "Not enough coins, watch an Ad for +5 Moves?";
			coinsPurchaseButton.interactable = false;
			coinsPurchaseButtonGO.interactable = false;
		}

		if (extraMovesPurchased == true) {
			movesPurchaseButton.SetActive (false);
		}

	}

	public void UpdateCoins()
	{
		PlayerPrefs.SetInt ("coins", coins);
		coinText.text = coins + " : Coins";
	}

	public void CheckCoinsAmount()
	{
		if (coins < 30) {
			movesPurchaseText.text = "Not enough coins, watch an Ad for +5 Moves?";
			coinsPurchaseButton.interactable = false;
			coinsPurchaseButtonGO.interactable = false;
		}

		if (coins >= 30) {
			coinsPurchaseButton.interactable = true;
			coinsPurchaseButtonGO.interactable = true;
		}
	}

	public void BuyMoreMoves ()
	{
		if (coins >= 30) {
			coins -= 30;
			Debug.Log ("Enough Coins to Purchase");
			PlayerPrefs.SetInt ("coins", coins);
			movesManager.GetComponent<MovesGameController> ().MovesPurchased ();
			movesPurchaseButton.SetActive (false);
		}
	}

	public void DeductCoins()
	{
		coins -= 30;
		PlayerPrefs.SetInt ("coins", coins);
		coinText.text = coins + " : Coins";
	}
}
