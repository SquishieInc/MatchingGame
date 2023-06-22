/*

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGamesScript : MonoBehaviour {

	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.Activate ();

		SignIn ();
	}

	void SignIn()
	{
		Social.localUser.Authenticate (success => { });
	}

	#region Achievements

	static public void UnlockAchievement(string id)
	{
		Social.ReportProgress (id, 100, success => { });
	}

	static public void IncrementAchievement(string id, int stepsToIncrement)
	{
		PlayGamesPlatform.Instance.IncrementAchievement (id, stepsToIncrement, success => { });
	}

	static public void ShowAchievementsUI()
	{
		Social.ShowAchievementsUI();
	}

	#endregion /Achievements

	#region Leaderboards

	static public void AddScoreToLeaderboard(string leaderboardId, long score)
	{
		Social.ReportScore (score, leaderboardId, success => { });
	}

	static public void ShowLeaderboard()
	{
		Social.ShowLeaderboardUI ();
	}

	#endregion /Leaderboards
}
*/