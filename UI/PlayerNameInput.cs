using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerNameInput : MonoBehaviour
{
    TMP_InputField inputField;
    Player player;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        player = FindObjectOfType<Player>();
        inputField.text = player.ActorName;
    }
    public void SetPlayerName(string str)
    {
        player.ActorName = str;
    }
    

}
