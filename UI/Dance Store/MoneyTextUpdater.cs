using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MoneyTextUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] IntEvent onMoneyUpdateByCurrency;
    [SerializeField] CurrencyManager _currencyManager; // BUG Find object not working


    private void OnEnable()
    {
        onMoneyUpdateByCurrency.RegisterListener(UpdateMoney);
    }
    private void OnDisable()
    {
        onMoneyUpdateByCurrency.UnregisterListener(UpdateMoney);

    }

    private void UpdateMoney(int money)
    {
        Debug.Log($"Money Text Updated");
        _moneyText.text = money.ToString();

    }
}
