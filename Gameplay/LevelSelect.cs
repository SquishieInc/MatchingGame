using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	private GameObject [] levelList;
	public string[] positiveMessage;
	public Text inspirationalText;

	private int index;
	private int messageIndex;

	private int roundNumber;
	public Text RoundNumberText;

	void Start()
	{
		index = -1;
		roundNumber = 0;
		messageIndex = 0;

		levelList = new GameObject[transform.childCount];

		for (int i = 0; i < transform.childCount; i++) 
			levelList [i] = transform.GetChild (i).gameObject;

		//toggle off their renderer
		foreach (GameObject go in levelList)
			go.SetActive (false);

		//we toggle the selected character
//		if (levelList [index])
//			ActivateLevel ();

		RoundNumberText.text = "Round " + roundNumber;
		inspirationalText.text = positiveMessage [0];

//		Debug.Log ("Amount of Levels to select = " + levelList.Length);
//		Debug.Log ("first level is  = " + levelList[index]);
	}

	public void GoDownALevel()
	{
		//Toggle off the current model
		levelList[index].SetActive(false);

		index--;//index -= 1; index = index -1;
		roundNumber--;
		if(index < 0)
			index = 1;

		RoundNumberText.text = "Round " + roundNumber;

//		Debug.Log ("Level selected = " + index);

		//Toggle on the new model
	}

	public void GoUpALevel()
	{
		if(messageIndex > 0)
		messageIndex = Random.Range (1, positiveMessage.Length);
		//Toggle off the current model
		if(index > -1)
		levelList[index].SetActive(false);

		index++;//index -= 1; index = index -1;
		roundNumber++;
//		Debug.Log ("Round NUmber = " + roundNumber);
		if(index == levelList.Length)
			index = levelList.Length;

		RoundNumberText.text = "Round " + roundNumber;
		//messageIndex = Random.Range (0, positiveMessage.Length);
		inspirationalText.text = positiveMessage[messageIndex];


//		Debug.Log ("Level selected = " + index);

		//Toggle on the new model
		//levelList[index].SetActive(true);
	}

	public void ActivateLevel()
	{
		messageIndex = Random.Range (1, positiveMessage.Length);
		levelList[index].SetActive(true);
	}
}