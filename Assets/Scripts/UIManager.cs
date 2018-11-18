using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public InputField inputField;
	public Button enterBtn;
	public Button clearBtn;

	public delegate void OnSubmitEvent(string message);
	public static OnSubmitEvent enterClickDelegate;
	public delegate void OnClearEvent();
	public static OnClearEvent clearClickDelegate;

	private void Awake()
	{
		SetRef();
		SetEventListener();
	}

	private void SetRef() 
	{
		if (!inputField)
		{
			inputField = GameObject.Find("InputField").GetComponent<InputField>();
		}
		if (!enterBtn)
		{
			enterBtn = GameObject.Find("EnterButton").GetComponent<Button>();
		}
		if (!clearBtn)
		{
			clearBtn = GameObject.Find("ClearButton").GetComponent<Button>();
		}
	}

	private void SetEventListener()
	{
		enterBtn.onClick.AddListener(EnterMessage);
		clearBtn.onClick.AddListener(ClearMessage);
	}

	private void EnterMessage()
	{
		string message = inputField.text;
		if (message == null || message.Length == 0) 
		{
			Debug.Log("message null");
			return;
		}
		Debug.Log("send " + message);
		enterClickDelegate(message);
	}

	private void ClearMessage()
	{
		inputField.text = "";
		clearClickDelegate();
	}
}
