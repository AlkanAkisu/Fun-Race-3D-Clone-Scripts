using UnityEngine;

public class Player : Actor
{

    const string playerNamePP = "playerName";

    [NaughtyAttributes.ShowNativeProperty]
    public override string ActorName
    {
        get
        {
            _actorName = PlayerPrefs.GetString(playerNamePP, "Player");
            return _actorName;
        }
        set
        {
            _actorName = value;
            PlayerPrefs.SetString(playerNamePP, _actorName);
        }
    }

    [SerializeField] string _actorName;


    private void OnValidate()
    {
        ActorName = _actorName;
    }

}