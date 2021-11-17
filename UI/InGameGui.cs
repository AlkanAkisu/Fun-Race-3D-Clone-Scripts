using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameGui : UIBase
{
    [SerializeField] Slider PlayerSlider, BotSlider1, BotSlider2;
    [SerializeField] FloatEvent PlayerPercent, Bot1Percent, Bot2Percent;
    [SerializeField] CustomEvent onRaceStarted;
    [SerializeField] CustomEvent onAfterGameMenuOpened;

    private void Awake()
    {
        Close();
    }

    [NaughtyAttributes.Button]
    public override void Open()
    {
        _system.gameObject.SetActive(true);

    }
    [NaughtyAttributes.Button]
    public override void Close()
    {
        _system.gameObject.SetActive(false);
    }
    public void SetBot1(float value) => SetSliderValue(BotSlider1, value);
    public void SetBot2(float value) => SetSliderValue(BotSlider2, value);
    public void SetPlayer(float value) => SetSliderValue(PlayerSlider, value);
    private void SetSliderValue(Slider slider, float val) => slider.value = val;

    private void OnEnable()
    {
        PlayerPercent.RegisterListener(SetPlayer);
        Bot1Percent.RegisterListener(SetBot1);
        Bot2Percent.RegisterListener(SetBot2);
        onRaceStarted.RegisterListener(Open);
        onAfterGameMenuOpened.RegisterListener(Close);
    }
    private void OnDisable()
    {
        PlayerPercent.UnregisterListener(SetPlayer);
        Bot1Percent.UnregisterListener(SetBot1);
        Bot2Percent.UnregisterListener(SetBot2);
        onRaceStarted.UnregisterListener(Open);
        onAfterGameMenuOpened.UnregisterListener(Close);

    }
}
