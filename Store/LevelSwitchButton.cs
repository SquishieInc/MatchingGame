using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
/*

public class LevelSwitchButton : MonoBehaviour {

	[SerializeField]
	private int coinPrice;

	[SerializeField]
	private float iapPrice;

	[SerializeField]
	private int iapToBuy;

	[SerializeField]
	private int amountPaid;

	public Text buttonIapPrice;
	public Text buttonCoinPrice;

	public GameObject loadingScreen;

	public GameObject purchaseWindow;

	public GameObject iapPurnchaseButton;
	public Text iapPurnchaseButtonText;

	public GameObject coinPurnchaseButton;
	public Text coinPurnchaseButtonText;

	public Text purchaseBoxText;

	private string buttonName;

	public GameObject Store;

	private string currentSceneName;
	public Button levelButton;

	// Use this for initialization
	void Start () 
	{
		currentSceneName = SceneManager.GetActiveScene ().name;
		Debug.Log ("Current Scene Loaded = " + currentSceneName);

		if (buttonName == currentSceneName) {
			Debug.Log ("Current Scene Loaded = " + currentSceneName + " & button name = " + buttonName);
			levelButton.GetComponent<Button> ().interactable = false;
		}

		buttonName = gameObject.name;
		Debug.Log ("This button is called " + buttonName);

		amountPaid = PlayerPrefs.GetInt (buttonName + "Price", 0);
		Debug.Log ("Amount paid for " + buttonName + " = " + amountPaid);

		if (amountPaid == 0) 
		{

			buttonCoinPrice.text = "" + coinPrice;
			buttonIapPrice.text = "" + iapPrice;
		}
		if (amountPaid > 0) 
		{
			buttonCoinPrice.text = "";
			buttonIapPrice.text = "";
		}
	}

	public void OnButtonPressed()
	{
		if (amountPaid == 0) 
		{
			purchaseWindow.SetActive (true);
			iapPurnchaseButton.SetActive (true);
			coinPurnchaseButton.SetActive (true);

			purchaseWindow.GetComponent<LevelPurchasePanel> ().ChangeWindowContent (buttonName, iapPrice, coinPrice, buttonName, iapToBuy);

			//iapPurnchaseButtonText.text = "" + iapPrice;
			//coinPurnchaseButtonText.text = "" + coinPrice;

			//purchaseBoxText.text = "How would you like to Purchase ";
		}

		if(amountPaid > 0)
		{
			loadingScreen.SetActive (true);
			Store.SetActive (false);
			loadingScreen.GetComponent<LoadingBarInGame> ().LevelToLoad (buttonName);
		}
	}

	public void TurnOffPrices()
	{
		amountPaid = PlayerPrefs.GetInt (buttonName + "Price", 0);
		buttonCoinPrice.text = "";
		buttonIapPrice.text = "";
	}
}
*/