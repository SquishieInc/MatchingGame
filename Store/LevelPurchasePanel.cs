using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/*

public class LevelPurchasePanel : MonoBehaviour {

	[SerializeField]
	private int coinPriceLPP;

	[SerializeField]
	private float iapPriceLPP;
	private int iapToBuy;

	private string purchaseButtonName;

	private GameObject purchaseButton;

	public Text windowText;
	public Text iapPriceText;
	public Text coinPriceText;
	public GameObject playButton;
	public GameObject loadingScreen;
	public GameObject processingPanel;

	public GameObject iapPurnchaseButton;
	public GameObject coinPurnchaseButton;

	GameManager gameManager;
	static private int gmcoins;


	void Start()
	{
		gmcoins = GameManager.coins;
	}

	void Awake()
	{
		//GameObject processingPanel = GameObject.Find ("Processing");
	}

	public void ChangeWindowContent(string buttonText, float iapPrice, int coinPrice, string buttonName, int iap)
	{
		windowText.text = "How would you like to Purchase " + buttonText;
		iapPriceText.text = "" + iapPrice;
		coinPriceText.text = "" + coinPrice;

		iapToBuy = iap;

		Debug.Log ("String = " + buttonText + ", float = " + iapPrice + ", coinprice = " + coinPrice);

		coinPriceLPP = coinPrice;
		iapPriceLPP = iapPrice;
		purchaseButtonName = buttonName;

		purchaseButton = GameObject.Find (purchaseButtonName);
	}

	public void OnCoinPurchase()
	{
		if (gmcoins >= coinPriceLPP) 
		{
			PlayerPrefs.SetInt (purchaseButtonName + "Price", coinPriceLPP);


			windowText.text = "Would you Like to Play " + purchaseButtonName + " now?";

			iapPurnchaseButton.SetActive (false);
			coinPurnchaseButton.SetActive (false);

			playButton.SetActive (true);

			purchaseButton.GetComponent<LevelSwitchButton> ().TurnOffPrices ();
		}
		if(gmcoins < coinPriceLPP)
		{
			windowText.text = "Not enough Coins, Would you Like to Play " + purchaseButtonName + " now?";

			iapPurnchaseButton.SetActive (true);
			coinPurnchaseButton.SetActive (false);

			playButton.SetActive (false);
		}

	}

	public void OnIapPurchase()
	{
		//GameObject processingPanel = GameObject.Find ("Processing");
		processingPanel.SetActive (true);

		if(iapToBuy == 1)
		{
			IAPManager.Instance.BuyFreshTurboScent();
			Debug.Log ("TurboScent");
		}

		if(iapToBuy == 2)
		{
			IAPManager.Instance.BuyServQuick();
			Debug.Log ("ServQuick");
		}

		if(iapToBuy == 3)
		{
			IAPManager.Instance.BuyStarFall();
			Debug.Log ("StarFall");
		}

		if(iapToBuy == 4)
		{
			IAPManager.Instance.BuyWinter();
			Debug.Log ("Winter");
		}

		//iapPurnchaseButton.SetActive (false);
		//coinPurnchaseButton.SetActive (false);

		//playButton.SetActive (true);

		//purchaseButton.GetComponent<LevelSwitchButton> ().TurnOffPrices();
	}

	public void PlayButtonPressed()
	{
		playButton.SetActive (false);
		loadingScreen.SetActive (true);
		loadingScreen.GetComponent<LoadingBarInGame> ().LevelToLoad (purchaseButtonName);
	}

	public void PurchaseComplete()
	{
		//GameObject processingPanel = GameObject.Find ("Processing");
		processingPanel.SetActive (false);

		PlayerPrefs.SetInt (purchaseButtonName + "Price", coinPriceLPP);

		windowText.text = "Would you Like to Play " + purchaseButtonName + " now?";

		iapPurnchaseButton.SetActive (false);
		coinPurnchaseButton.SetActive (false);

		playButton.SetActive (true);

		purchaseButton.GetComponent<LevelSwitchButton> ().TurnOffPrices();
	}

	public void PurchaseFailed()
	{
		windowText.text = "Purchase failed, Try again later";

		processingPanel.SetActive (false);

		iapPurnchaseButton.SetActive (false);
		coinPurnchaseButton.SetActive (false);

		playButton.SetActive (false);
	}
}
*/