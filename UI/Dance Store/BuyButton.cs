using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] DanceCurrencyDictionary dancePrice;
    [SerializeField] DanceMovesEvent danceMovesEvent;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] DanceMovesEvent onDanceMoveEquipped;
    [SerializeField] DanceStorage danceStorage;



    [SerializeField] Button button;
    CurrencyManager _currencyManager;

    ButtonState currentState;
    DanceMoves currentDance;

    public enum ButtonState
    {
        NonInteractable,
        Buy,
        Equip
    }

    private void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();
        SetButtonState(ButtonState.NonInteractable);

    }
    private void OnValidate()
    {
        foreach (var dance in System.Enum.GetValues(typeof(DanceMoves)))
        {
            if (!dancePrice.Contains(dance))
            {
                dancePrice.Add(dance, 400);
            }
        }
    }

    private void DanceSelected(DanceMoves move)
    {
        currentDance = move;

        if (DanceHasBought(move))
        {
            SetButtonState(ButtonState.Equip);
        }
        else
        {
            if (CanBuy(move))
                SetButtonState(ButtonState.Buy);
            else
                SetButtonState(ButtonState.NonInteractable);
        }
    }


    private void SetButtonState(ButtonState state)
    {
        currentState = state;
        if (state == ButtonState.NonInteractable)
        {
            SetButtonInteractable(false);
        }
        else
        {
            SetButtonInteractable(true);
            if (state == ButtonState.Buy)
            {
                //TODO Buy state
            }
            else if (state == ButtonState.Equip)
            {

                if (danceStorage.IsCurrent(currentDance))
                {
                    SetButtonText("Equipped");
                    SetButtonInteractable(false);
                }
                else
                {
                    SetButtonText("Equip");
                }
            }
        }
        if (currentState != ButtonState.Equip)
            SetButtonText(GetPrice(currentDance));

    }
    public void BuyButtonClicked()
    {

        bool succesfullBuy;
        switch (currentState)
        {

            case ButtonState.NonInteractable:
                break;

            case ButtonState.Buy:
                succesfullBuy = _currencyManager.Buy(GetPrice(currentDance));

                if (succesfullBuy)
                {

                    NewDanceBought(currentDance);
                }
                break;

            case ButtonState.Equip:

                onDanceMoveEquipped.Raise(currentDance);
                break;

            default:
                break;
        }
        DanceSelected(currentDance);
        danceStorage.Save();
    }

    private void NewDanceBought(DanceMoves move)
    {
        onDanceMoveEquipped.Raise(currentDance);
        danceStorage.EquipNewDanceMove(currentDance);
        danceStorage.CurrentDanceMove = currentDance;
    }


    private void OnEnable()
    {
        danceMovesEvent.RegisterListener(DanceSelected);
    }
    private void OnDisable()
    {
        danceMovesEvent.UnregisterListener(DanceSelected);

    }

    private bool DanceHasBought(DanceMoves move) => danceStorage.IsEquipped(move);
    private void SetButtonText(int amount) => SetButtonText(amount < 0 ? "Buy" : $"Buy [{amount}]");
    private void SetButtonText(string str) => buttonText.text = str;

    private void SetButtonInteractable(bool interactable) => button.interactable = interactable;
    private bool CanBuy(DanceMoves move) => _currencyManager.CanBuy(GetPrice(move));
    private int GetPrice(DanceMoves move) => dancePrice[move];
}


[System.Serializable]
public class DanceCurrencyDictionary : SerializableDictionary<DanceMoves, int> { }

