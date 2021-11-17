using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{

    [SerializeField] int WinReward = 40, LoseReward = 10;
    [SerializeField] FloatEvent onSetRewardMoney;
    [SerializeField] FloatEvent onSetCurrentMoneyText;
    [SerializeField] IntEvent onMoneyUpdateByCurrency;
    [SerializeField] CustomEvent onDanceMenuOpened;

    [SerializeField] int debug;
    [SerializeField, ReadOnly] private int _currency;
    const string currencyPPName = "currencyInt";
    public int Currency => _currency;
    System.Random Rand = new System.Random();

    private void Awake()
    {
        _currency = GetCurrency();
        UpdateDanceText();
    }
    private void SetCurrency(int number)
    {
        _currency = number;
        PlayerPrefs.SetInt(currencyPPName, Currency);
        UpdateDanceText();
    }

    public int GetCurrency()
    {
        _currency = PlayerPrefs.GetInt(currencyPPName, 0);
        return Currency;
    }

    public void AddCurrency(int addAmount)
    {
        SetCurrency(GetCurrency() + addAmount);
    }

    public void PlayerFinished(bool isWon)
    {

        onSetCurrentMoneyText.Raise(Currency);

        int reward = (isWon ? WinReward : LoseReward) + Rand.Next(-5, +5);

        AddCurrency(reward);

        onSetRewardMoney.Raise(reward);

    }
    public bool Buy(int amount)
    {
        if (CanBuy(amount))
        {
            SetCurrency(GetCurrency() - amount);
            return true;
        }

        Debug.Log($"Not enough Money. Money:{Currency}");
        return false;

    }
    public void Add3K()
    {
        AddCurrency(3000);
    }

    [NaughtyAttributes.Button] private void AddDebugMoney() => AddCurrency(debug);
    [NaughtyAttributes.Button] public void Reset() => SetCurrency(0);


    public bool CanBuy(int amount) => Currency >= amount;



    private void UpdateDanceText()
    {
        onMoneyUpdateByCurrency.Raise(GetCurrency());
    }

    private void OnEnable()
    {
        onDanceMenuOpened.RegisterListener(UpdateDanceText);
    }
    private void OnDisable()
    {
        onDanceMenuOpened.UnregisterListener(UpdateDanceText);

    }

}
