using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class RaceFinishRewards : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] CustomEvent onAfterMenuOpened;
    [SerializeField] FloatEvent onSetRewardMoney;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] float seconds = 1f;
    [SerializeField] MMFeedbacks fadeOutAnim;
    [SerializeField] FloatEvent onSetCurrentMoneyText;
    [SerializeField] float animationDelay;

    #endregion

    #region Private Fields
    int rewardAmount;

    #endregion

    #region Public Properties


    #endregion

    #region Dependencies


    #endregion

    public void AddAnimation(int to)
    {
        fadeOutAnim.PlayFeedbacks();
        DOTween.To(
            () => int.Parse(textMesh.text),
            (a) => textMesh.text = a.ToString(),
            to,
            seconds
        );
    }
    [NaughtyAttributes.Button]
    private void DebugAdd20()
    {
        AddMoney(19);
    }

    public void AddMoney(int addAmount)
    {
        var from = int.Parse(textMesh.text);
        AddAnimation(from + addAmount);
    }
    [NaughtyAttributes.Button]



    private void AfterGameMenuOpened()
    {
        Invoke(nameof(AddReward), animationDelay);
    }

    public void AddReward() => AddMoney(rewardAmount);
    
    private void SetCurrentMoneyText(float money)
    {
        int moneyIntger = (int)money;
        textMesh.text = moneyIntger.ToString();

    }

    private void SetRewardMoney(float money)
    {
        int moneyInteger = (int)money;
        rewardText.text = moneyInteger.ToString();
        rewardAmount = moneyInteger;
    }
    private void OnEnable()
    {
        onSetRewardMoney.RegisterListener(SetRewardMoney);
        onSetCurrentMoneyText.RegisterListener(SetCurrentMoneyText);
        onAfterMenuOpened.RegisterListener(AfterGameMenuOpened);
    }
    private void OnDisable()
    {
        onSetRewardMoney.UnregisterListener(SetRewardMoney);
        onSetCurrentMoneyText.UnregisterListener(SetCurrentMoneyText);
        onAfterMenuOpened.UnregisterListener(AfterGameMenuOpened);

    }

}
