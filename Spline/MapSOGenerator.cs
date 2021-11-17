using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapSOGenerator : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] List<PlatformSO> platformSOs;
    [SerializeField] PlatformSO finishLine;
    [SerializeField] int numberOfPlatforms;
    [SerializeField] int howManySO;

    int startFrom;


    [NaughtyAttributes.Button("Create Maps")]
    void CreateMultipleSO()
    {

        startFrom = 1;
        MapSO map = Resources.Load<MapSO>($"Maps/Map {startFrom}");
        while (map != null)
        {
            startFrom++;
            map = Resources.Load<MapSO>($"Maps/Map {startFrom}");
        }
        for (int i = startFrom; i < startFrom + howManySO; i++)
        {
            CreateSO(i);
        }

    }


    private void CreateSO(int i)
    {
        MapSO asset = ScriptableObject.CreateInstance<MapSO>();
        List<PlatformSO> platforms = new List<PlatformSO>();

        platformSOs.Shuffle();

        platforms = platformSOs.GetRange(0, numberOfPlatforms);
        platforms.Add(finishLine);

        asset._platformAssets = platforms.ToArray();
        SaveAsAsset(asset, i);
    }

    private void SaveAsAsset(MapSO asset, int i)
    {
        AssetDatabase.CreateAsset(asset, $"Assets/Resources/Maps/Map {i}.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif
}
