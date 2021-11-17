using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField, NaughtyAttributes.Required] CustomEvent onNextLevel;
    [SerializeField, NaughtyAttributes.Required] CustomEvent onReloadLevel;
    const string MapPPKey = "map_index";
    private ModifiedMapMaker mapMaker;

    public ModifiedMapMaker MapMaker => (mapMaker = GetComponent<ModifiedMapMaker>());

    public void InjectMap(bool doInstantiate = true)
    {
        var index = GetMapIndex();
        var map = GetMapAtIndex(index);

        MapMaker.GetInjectedMap(map);

        if (doInstantiate)
            MapMaker.CreateMap();
    }

    private MapSO GetMapAtIndex(int index)
    {

        MapSO map = Resources.Load<MapSO>($"Maps/Map {index}");
        if (map == null)
        {
            SetIndex(1);
            map = Resources.Load<MapSO>($"Maps/Map {1}");

        }
        return map;
    }

    private int GetMapIndex() => PlayerPrefs.GetInt(MapPPKey, 1);

    private void SetIndex(int index)
    {
        PlayerPrefs.SetInt(MapPPKey, index);
    }
    private void IncreaseIndex()
    {
        int index = GetMapIndex();
        PlayerPrefs.SetInt(MapPPKey, index + 1);
    }


    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    private void NextLevel(bool reloadScene = true)
    {
        IncreaseIndex();
        InjectMap();
        if (reloadScene)
            ReloadScene();
    }

    public void ReloadLevel()
    {
        ReloadScene();
    }


    [NaughtyAttributes.Button]
    public void Reset()
    {
        SetIndex(1);
        InjectMap(doInstantiate: false);
    }


    private void NextLevel() => NextLevel(true);

    [NaughtyAttributes.Button]
    private void NextLevelButton() => NextLevel(false);

    private void OnEnable()
    {
        onNextLevel.RegisterListener(NextLevel);
        onReloadLevel.RegisterListener(ReloadLevel);
    }

    private void OnDisable()
    {
        onNextLevel.UnregisterListener(NextLevel);
        onReloadLevel.UnregisterListener(ReloadLevel);
    }



}
