using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : UIBase
{
    #region Serialize Fields
    [SerializeField] DifficultyModeChanged onDifficultyModeChanged;
    [SerializeField] DifficultyModes currentDiff;
    [SerializeField] DifficultyDictionary diffButtons;
    [SerializeField] CustomEvent onSettingsOpened;
    [SerializeField] CustomEvent onSettingsClosed;
    private DifficultyManager difficultyManager;




    #endregion

    private void Awake()
    {
        difficultyManager = FindObjectOfType<DifficultyManager>();
        currentDiff = difficultyManager.GetDiff();
        DifficultyModeChanged(currentDiff);

        diffButtons[currentDiff].Select();
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
    private void DifficultyModeChanged(DifficultyModes diff)
    {
        currentDiff = diff;
        HandleSelection(diff);
        difficultyManager.SetDifficulty(diff);
    }

    private void HandleSelection(DifficultyModes diff)
    {
        foreach (var button in diffButtons)
            if (button.Key != diff)
                button.Value.Deselect();
    }

    private void OnEnable()
    {
        onSettingsOpened.RegisterListener(Open);
        onSettingsClosed.RegisterListener(Close);
        onDifficultyModeChanged.RegisterListener(DifficultyModeChanged);
    }

    private void OnDisable()
    {
        onSettingsOpened.UnregisterListener(Open);
        onSettingsClosed.UnregisterListener(Close);
        onDifficultyModeChanged.UnregisterListener(DifficultyModeChanged);

    }

    private void OnValidate()
    {
        onDifficultyModeChanged?.Raise(currentDiff);
    }

}

public enum DifficultyModes
{
    Easy,
    Medium,
    Hard,
}

[System.Serializable]
public class DifficultyDictionary : SerializableDictionary<DifficultyModes, DifficultyButton> { }
