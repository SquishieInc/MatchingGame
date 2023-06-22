using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenter : MonoBehaviour {

	//public string leaderboardID;  //don't forget to enter the ID in the inspector!

	void Start(){
		AuthenticateToGameCenter();
	}
	//Authenticates to GameCenter
	public static void  AuthenticateToGameCenter()
	{
		#if UNITY_IPHONE
		Social.localUser.Authenticate(success =>
			{
				if (success)
				{
					Debug.Log("Authentication successful");
				}
				else
				{
					Debug.Log("Authentication failed");
				}
			});
		#endif
	}

	//call this function to show the leaderboards
	public void Leaderboards(){
		#if UNITY_IPHONE
		Social.ShowLeaderboardUI();
		#endif
	}

	public void ReportScore(long scoreLB, string leaderboardID){
		#if UNITY_IPHONE
		Social.ReportScore(scoreLB, leaderboardID, success => {
			if (success){
				Debug.Log("Reported score successfully");
			} else {
				Debug.Log("Failed to report score");
			}
		});
		#endif
	}
}
