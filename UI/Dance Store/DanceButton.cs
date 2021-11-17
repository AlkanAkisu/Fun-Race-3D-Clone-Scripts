using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class DanceButton : MonoBehaviour
{
    [SerializeField] DanceMovesEvent onDanceMovesEvent;
    [SerializeField] DanceMoves move;
    [SerializeField] TextMeshProUGUI text;

    private void OnValidate()
    {
        int i = (int)move;
        string danceName = System.Enum.GetNames(typeof(DanceMoves))[i];
        danceName = Regex.Replace(danceName, "(\\B[A-Z])", " $1");
        text.text = danceName;
    }

    public void OnClick()
    {
        onDanceMovesEvent.Raise(move);
    }
}
