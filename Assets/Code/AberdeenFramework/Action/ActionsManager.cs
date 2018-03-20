using UnityEngine.UI;
using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionsManager : Singleton<ActionsManager> {
    public Button[] Buttons;
    private List<Action> actionCalls = new List<Action>();
    // Use this for initialization
    void Start () {
        for (int i = 0; i < Buttons.Length; i++)
        {
            var index = i;
            Buttons[index].onClick.AddListener(delegate () {
                OnButtonClick(index);
            });
        }

        ClearButtons();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ClearButtons()
    {
        foreach (var b in Buttons)
            b.gameObject.SetActive(false);

        actionCalls.Clear();

    }
    public void OnButtonClick(int index)
    {
        actionCalls[index]();
    }
    public void AddButton(Sprite pic, Action onClick)
    {
        int index = actionCalls.Count;
        Buttons[index].gameObject.SetActive(true);
        Buttons[index].GetComponent<Image>().sprite = pic;
        actionCalls.Add(onClick);
    }
}
