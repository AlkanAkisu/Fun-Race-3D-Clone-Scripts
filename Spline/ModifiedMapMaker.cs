using UnityEngine;
using Dreamteck.Splines;
using System.Linq;
using System;
using NaughtyAttributes;
using System.Collections;

[RequireComponent(typeof(SplineStorage))]

public class ModifiedMapMaker : MonoBehaviour
{
    private const string MapName = "[Map]";
    #region Serialize Fields
    [SerializeField] bool isDebug;

    [SerializeField, ShowIf(nameof(isDebug))] PlatformSO[] _platformAssets;
    [SerializeField, Expandable, HideIf(nameof(isDebug)), OnValueChanged(nameof(MakeMap))] MapSO _map;


    [SerializeField, HideIf(nameof(isDebug)), OnValueChanged(nameof(MakeMap))] bool _handledByMapManager;
    [SerializeField, HideIf(nameof(_handledByMapManager))] bool _runInAwake;

    [SerializeField] CustomEvent onLoadingFinished;


    #endregion

    #region Private Fields

    Transform[] _transforms;
    private PlatformSO[] PlatformAssets => isDebug ? _platformAssets : _map?._platformAssets ?? _platformAssets;
    private GameObject[] Objects => PlatformAssets.Select(t => t.gameObject).ToArray();

    #endregion

    #region Public Properties


    #endregion

    #region Dependencies

    SplineStorage _splineStorage;
    private MapManager mapManager;

    private SplineStorage SplineStorage => _splineStorage ?? (_splineStorage = GetComponent<SplineStorage>());

    public bool HandledByMapManager => _handledByMapManager;

    #endregion
    void Start()
    {

        mapManager = GetComponent<MapManager>();

        if (_handledByMapManager)
        {
            mapManager.InjectMap(doInstantiate: true);

        }
        else if (_runInAwake)
            MakeMap();
    }



    public void GetInjectedMap(MapSO map) => _map = map;
    public void CreateMap() => MakeMap();

    // PRIVATES

    [NaughtyAttributes.Button]

    void MakeMap() => StartCoroutine(IE_MakeMap());

    IEnumerator IE_MakeMap()
    {
        int N = Objects.Length;
        _transforms = new Transform[N];



        SplineStorage.InitSplines(N);



        Vector3 pos = transform.position;
        DeleteOldMap();

        var parent = new GameObject(MapName);
        parent.transform.position = Vector3.zero;

        for (int i = 0; i < N; i++)
        {

            var platform = Instantiate(Objects[i], parent.transform);
            if (Application.isPlaying)
            {
                yield return null;
            }

            Vector3 _splineDifference = GetDifference(platform);

            var instantiatePos = PutSplineInPos(pos, _splineDifference);


            platform.transform.position = instantiatePos;


            var splineComputer = platform.GetComponentInChildren<SplineComputer>();
            if (Application.isPlaying)
            {
                yield return null;
            }
            SplineStorage.AddSpline(splineComputer, i);


            pos = GetLastSplinePoint(splineComputer);
            if (Application.isPlaying)
            {
                yield return null;

            }

        }

        Action<MoveController> startFromFirstSpline = e => e.GetComponent<SplineChanger>().StartFromFirstSpline();


        FindObjectsOfType<MoveController>().ToList().ForEach(startFromFirstSpline);

        yield return null;
        if (Application.isPlaying)
        {
            yield return null;
            onLoadingFinished?.Raise();
        }

    }
    [Button("Delete Map")]
    private static void DeleteOldMap()
    {

        var obj = GameObject.Find(MapName);
        if (obj)
        {
            GameObject.DestroyImmediate(obj);
        }
    }

    private Vector3 GetDifference(GameObject obj)
    {
        SplineComputer splineComputer = obj.GetComponentInChildren<SplineComputer>();

        Vector3 firstPointPosition = splineComputer.GetPoint(0).position;
        var mainTrans = obj.transform.position;

        return mainTrans - firstPointPosition;
    }
    private Vector3 PutSplineInPos(Vector3 splinePos, Vector3 differenceFromObj) => splinePos + differenceFromObj;

    private Vector3 GetLastSplinePoint(SplineComputer spline) => spline.GetPoint(spline.pointCount - 1).position;



}
