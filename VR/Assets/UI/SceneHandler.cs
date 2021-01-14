﻿/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public GameObject welcomeSceenDisplay;
    public GameObject tasksScreenDisplay;
    public GameObject upgradesScreenDisplay;
    public UpgradeManager um;
    private int upgradeAmount = 100;
    public GameObject player;
    public ScoreToUI scoreToUI;

    public FillTaskList ftl;
    public Scrollbar sb;
    public GameObject content;

    private bool pointerOnScroll;
    void Awake()
    {
        //laserPointer.PointerIn += PointerInside;
        //laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        Debug.Log(e.target.name);
        if (e.target.name == "ButtonBegin")
        {
            welcomeSceenDisplay.SetActive(false);
            tasksScreenDisplay.SetActive(true);
            ftl.UpdateTaskList();
            scoreToUI.ScoreToUIText();
            sb.value = 1;
        }

        if (e.target.name == "BtnNavUpgrades")
        {
            tasksScreenDisplay.SetActive(false);
            upgradesScreenDisplay.SetActive(true);
            scoreToUI.ScoreToUIText();
        }

        if (e.target.name == "BtnNavTasks")
        {
            sb.value = 1;
            upgradesScreenDisplay.SetActive(false);
            tasksScreenDisplay.SetActive(true);
            scoreToUI.ScoreToUIText();
            ftl.UpdateTaskList();
        }

        if (e.target.name == "BtnUpgrade")
        {
            
            int totalPoints = TaskManager.GetPoints();
            if (totalPoints >= upgradeAmount)
            {
                scoreToUI.pointsUntilUpgrade += 100;
                upgradeAmount += 100;
                scoreToUI.CalculateRemaining();
                um.Upgrade();
            }
        }

        if (e.target.CompareTag("UIClickable"))
        {
            foreach (Transform child in content.transform)
            {
                child.GetComponent<Image>().color = Color.black;
            }
            e.target.gameObject.GetComponent<Image>().color = Color.green;
        }

        if (e.target.name == "MoveScrollUp")
        {
            sb.value += 0.1f;
        }

        if (e.target.name == "MoveScrollDown")
        {
            sb.value -= 0.1f;
        }
    }
}