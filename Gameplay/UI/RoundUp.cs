using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundUp : MonoBehaviour {

	public Text roundNumber;
	public Text correctPairs;

	int _roundNumber;
	int _correctMatches;

	string _shareString;
	NativeShare _nShare;

	public GameObject shareButton;

	void Awake()
	{
		NativeShare nShare = shareButton.GetComponent<NativeShare> ();
	}

	public void RoundUpText(int roundNumberno, int correctMatches)
	{
	//	int updatedroundnumber = roundNumberno - 1;

	//	_roundNumber = updatedroundnumber;
		_roundNumber = roundNumberno;
		_correctMatches = correctMatches;

		roundNumber.text = "you passed " + _roundNumber + " rounds";
		correctPairs.text = ""+ correctMatches + " Pairs Found!";
	}

	public void ShareButtonPress()
	{
		//_shareString = ("I just got to Round " + _roundNumber + " after scoring " + _correctMatches + " correct Matches. Try and beat me! #Calvinmarlo #Learnmusic").ToString();
		_shareString = ("I just got to Round " + _roundNumber + " after scoring " + _correctMatches + " correct Matches. Try and beat me! #MeM").ToString();


		NativeShare nShare = GetComponent<NativeShare> ();
		nShare.ShareScreenshotWithText (_shareString);
	}
}
