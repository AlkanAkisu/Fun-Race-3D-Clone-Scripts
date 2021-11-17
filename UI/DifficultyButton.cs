using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyButton : MonoBehaviour
{

    [SerializeField] Image _arrow;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Color _selectColor = Color.green, _deselectColor = Color.white;
    [SerializeField] DifficultyModes _difficulty;
    [SerializeField] DifficultyModeChanged onDifficultyModeChanged;
    bool _isSelected;
    public bool IsSelected => _isSelected;

    private void Awake()
    {
        _isSelected = false;
    }

    [NaughtyAttributes.Button]
    public void Select()
    {
        if (_isSelected) return;

        ChangeTextColor(_selectColor);
        ButtonAppearence(isShown: true);
        RaiseDifficultyChanged();
        _isSelected = true;
    }


    [NaughtyAttributes.Button]
    public void Deselect()
    {
        ChangeTextColor(_deselectColor);
        ButtonAppearence(isShown: false);
        _isSelected = false;

    }

    private void ChangeTextColor(Color color)
    {
        _text.color = color;
    }

    private void ButtonAppearence(bool isShown)
    {
        _arrow.gameObject.SetActive(isShown);
    }

    private void RaiseDifficultyChanged()
    {
        onDifficultyModeChanged?.Raise(_difficulty);
    }


}
