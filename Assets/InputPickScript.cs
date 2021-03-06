﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InputPickScript : MonoBehaviour
{
	// Sprites for found player
	public Sprite singleCheckMark;
	public Sprite multipleCheckMarks;
	public Sprite redXMark;

	// Input field positions
	private Vector3 DisplayPosition = new Vector3(-1.9f, -3, 0);
	private Vector3 HiddenPosition = new Vector3(-15, -3, 0);

	// Reference to the main script
	private DraftTimerScript timerScript;

	public GameObject checkMarkObject;

	// Use this for initialization
	void Start ()
	{
		timerScript = GameObject.Find("DraftTimer").GetComponent<DraftTimerScript>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gameObject.GetComponent<InputField>().IsInteractable())
		{
			if(Input.GetKeyUp(KeyCode.Tab))
			{
				int numMatches = timerScript.playerDatabase.NumMatchingPlayers(gameObject.GetComponent<InputField>().text);
				// Player Found
				if (numMatches == 1)
				{
					gameObject.GetComponent<InputField>().text = timerScript.playerDatabase.GetSearchResult();
					gameObject.GetComponent<InputField>().caretPosition = gameObject.GetComponent<InputField>().text.Length;
				}
			}
		}
	}

	public void Show()
	{
		this.transform.DOMove(DisplayPosition, timerScript.quickAnimationTime);
		gameObject.GetComponent<InputField>().interactable = true;
	}

	public void Hide()
	{
		this.transform.DOMove(HiddenPosition, timerScript.quickAnimationTime);
		gameObject.GetComponent<InputField>().interactable = false;
	}

	public void UpdatePlayersMatched()
	{
		int numMatches = timerScript.playerDatabase.NumMatchingPlayers(gameObject.GetComponent<InputField>().text);

		// Player Found
		if (numMatches == 1)
		{
			checkMarkObject.GetComponent<SpriteRenderer>().sprite = singleCheckMark;
		}
		else if (numMatches == 0)
		{
			checkMarkObject.GetComponent<SpriteRenderer>().sprite = redXMark;
		}
		else
		{
			checkMarkObject.GetComponent<SpriteRenderer>().sprite = multipleCheckMarks;
		}
	}
}
