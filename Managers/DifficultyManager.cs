using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    const string DiffPPKey = "difficulty";


    public DifficultyModes CurrentDifficultyMode { get; private set; }

    [SerializeField] List<AIIntelligenceBasedDifficulty> intellBasedDiff;

    private void Start()
    {
        InjectIntelligence();
    }

    private void InjectIntelligence()
    {
        var difficultyIntelligence = intellBasedDiff.Find(d => d.difficulty == CurrentDifficultyMode);

        var bots = FindObjectsOfType<AIMoveController>();
        foreach (var (i, bot) in bots.Enumerate())
        {
            bot.InjectIntelligence(difficultyIntelligence.intelligences[i]);
        }
    }
    

    public void SetDifficulty(DifficultyModes diff)
    {
        CurrentDifficultyMode = diff;
        int index = (int)diff;
        PlayerPrefs.SetInt(DiffPPKey, index);

        Debug.Log($"set {diff}");
        InjectIntelligence();

    }

    public DifficultyModes GetDiff()
    {
        int index = PlayerPrefs.GetInt(DiffPPKey, 1);
        var difficultyMode = (DifficultyModes)System.Enum.GetValues(typeof(DifficultyModes)).GetValue(index);
        CurrentDifficultyMode = difficultyMode;


        return difficultyMode;
    }


}

[System.Serializable]
class AIIntelligenceBasedDifficulty
{
    public string name;
    public DifficultyModes difficulty;
    public AIIntelligence[] intelligences = new AIIntelligence[2];
}
