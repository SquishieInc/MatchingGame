/*

using UnityEngine;

public class GoogleManagerScript : MonoBehaviour {

	public static GoogleManagerScript Instance { get; private set; }
	public static int counter { get; private set; }

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	public void IncrementCounter()
	{
		counter++;
	}

	public void AddingToLeaderboard()
	{
		PlayGamesScript.AddScoreToLeaderboard (GPGSIds.leaderboard_leaderboard, counter);
		counter = 0;
	}
}
*/