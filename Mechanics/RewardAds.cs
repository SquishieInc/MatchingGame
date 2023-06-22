
using UnityEngine;
using System.Collections;

using UnityEngine.Advertisements;

public class RewardAds : MonoBehaviour {

	public string androidAdId = "1206948";
	public string iosAdId = "1206949";

	public GameObject movesManager;
//	public GameObject timeManager;
	GameManager gameManager;

	public GameObject movesPurchasePanel;
	public GameObject addMovesButton;

	public bool adReady;

	void Start()
	{
		adReady = true;
	}

	void Awake ()
	{
		if (Advertisement.isSupported) 
		{
			if(Application.platform == RuntimePlatform.Android)
			{
				Advertisement.Initialize(androidAdId, false);
			}
			else if(Application.platform == RuntimePlatform.IPhonePlayer)
			{
				Advertisement.Initialize(iosAdId, false);
			}
		}

		GameManager gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}


	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}


	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			Debug.Log ("Video completed. User rewarded ");
//			gameManager.DeductCoins ();
			movesManager.GetComponent<MovesGameController> ().MovesPurchased ();
			addMovesButton.SetActive (false);
			movesPurchasePanel.SetActive (false);

			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
