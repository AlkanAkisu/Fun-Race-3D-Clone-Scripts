using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class DanceStorage : ScriptableObject
{
    [SerializeField] List<DanceMoves> DanceMovesRetrieved;
    [SerializeField] DanceMoves _currentDanceMove;
    [SerializeField] string id;
    const string danceDancesRetrievedPPName = "danceIndexes", danceCurrentDancePPName = "currentDance";


    public DanceMoves CurrentDanceMove
    {
        get
        {
            if (DanceMovesRetrieved.Capacity == 0)
            {
                ResetStorage();
            }

            return _currentDanceMove;
        }
        set
        {
            _currentDanceMove = value;
        }
    }

    public void ResetStorage()
    {
        DanceMovesRetrieved.Clear();
        DanceMovesRetrieved.Add(DanceMoves.Wave);
        _currentDanceMove = DanceMoves.Wave;
    }

    public void EquipNewDanceMove(DanceMoves dance)
    {
        DanceMovesRetrieved.Add(dance);

    }

    private void SaveDanceMovesRetrieved()
    {

        int[] danceIndexes = DanceMovesRetrieved.Select(d => (int)d).ToArray();

        PlayerPrefsX.SetIntArray(danceDancesRetrievedPPName, danceIndexes);

    }

    private void GetDanceMovesRetrieved()
    {
        int[] danceIndexes = PlayerPrefsX.GetIntArray(danceDancesRetrievedPPName);

        DanceMovesRetrieved = danceIndexes.Select(i => DanceFromIndex(i)).ToList();

    }
    private void SaveCurrentDance()
    {
        PlayerPrefs.SetInt(danceCurrentDancePPName, (int)_currentDanceMove);

    }

    private void GetCurrentDance()
    {
        int danceIndex = PlayerPrefs.GetInt(danceCurrentDancePPName);


        _currentDanceMove = DanceFromIndex(danceIndex);

    }

    public void Save()
    {
        SaveCurrentDance();
        SaveDanceMovesRetrieved();
    }

    private DanceMoves DanceFromIndex(int index) => (DanceMoves)System.Enum.GetValues(typeof(DanceMoves)).GetValue(index);




    public bool IsEquipped(DanceMoves dance) => DanceMovesRetrieved.Contains(dance);
    public bool IsCurrent(DanceMoves dance) => CurrentDanceMove == dance;


    private void OnEnable()
    {
        GetDanceMovesRetrieved();
        GetCurrentDance();
    }
    private void OnDisable()
    {
        Save();
    }

}
