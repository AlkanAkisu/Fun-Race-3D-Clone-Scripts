using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AfterGameMenu : UIBase
{

    [SerializeField] StringArrayEvent onWinnersAnnounced;
    [SerializeField] Image winner1, vsWinner, winner2, loser2, loser3;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] CustomEvent onAfterMenuOpened;


    private void TryOut()
    {
        CustomEvent[] _events = new CustomEvent[] { onAfterMenuOpened };
        Action[] _actions = new Action[] { Open };


        CustomEvent<String[]>[] _stringEvents = new CustomEvent<String[]>[] { onWinnersAnnounced };
        Action<String[]>[] _actionsString = new Action<String[]>[] { WinnersAnnounced };

        Dictionary<CustomEvent<String[]>, Action<String[]>> stringDict;

        stringDict = Utils.EventActionDict<string[]>(
            (onWinnersAnnounced, WinnersAnnounced)
        );

        

        stringDict.RegisterListeners();



    }

   



    public bool IsPlayerWin { get; private set; }


    [NaughtyAttributes.Button]
    public override void Open()
    {
        _system.gameObject.SetActive(true);
        onAfterMenuOpened.Raise();
    }
    [NaughtyAttributes.Button]
    public override void Close()
    {

        _system.gameObject.SetActive(false);
    }



    private void WinnersAnnounced(string[] winners)
    {
        StartCoroutine(IEnumWinnersAnnounced(winners));

    }
    IEnumerator IEnumWinnersAnnounced(string[] winners)
    {
        Close();
        yield return new WaitForEndOfFrame();
        Open();
        var images = HandleLeader(winners.Length);
        HandleNames(winners, images);
        HandleButton();

    }

    private void HandleButton()
    {
        const string lostButtonText = "RACE AGAIN";
        const string winButtonText = "NEXT RACE!";

        IsPlayerWin = FindObjectOfType<RaceFinishedManager>().IsPlayerPassed;

        string text = IsPlayerWin ? winButtonText : lostButtonText;

        buttonText.text = text;


    }

    private void HandleNames(string[] winners, Image[] images)
    {
        foreach ((int i, var value) in winners.Enumerate())
        {
            var tmp = images[i].GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = value;
        }
    }

    private Image[] HandleLeader(int numberOfPlayer)
    {
        DisableAll();
        if (numberOfPlayer <= 3)
        {

            if (numberOfPlayer == 2)
            {
                vsWinner.gameObject.SetActive(true);
                loser2.gameObject.SetActive(true);
                return new Image[] { vsWinner, loser2 };
            }
            else if (numberOfPlayer == 3)
            {
                winner1.gameObject.SetActive(true);
                winner2.gameObject.SetActive(true);
                loser3.gameObject.SetActive(true);
                return new Image[] { winner1, winner2, loser3 };
            }

        }

        Debug.LogError("Unexpected Number of Player");
        return null;

    }

    private void DisableAll()
    {
        vsWinner.gameObject.SetActive(false);
        winner1.gameObject.SetActive(false);
        loser2.gameObject.SetActive(false);
        winner2.gameObject.SetActive(false);
        loser3.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        onWinnersAnnounced.RegisterListener(WinnersAnnounced);
    }
    private void OnDisable()
    {
        onWinnersAnnounced.UnregisterListener(WinnersAnnounced);
    }



}
