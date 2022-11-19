using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Buntility.Routines;

public class LightFlicker : MonoBehaviour
{
    Light _light;
    [SerializeField] float _minTime = .2f;
    [SerializeField] float _maxTime = .6f;

    float _lightIntensity;
    bool _curState = true;
    
    Routine _routine;
    const string ROUTINE_NAME = "LightFlicker";

    private void Awake()
    {
        _light = GetComponent<Light>();
        _lightIntensity = _light.intensity;
    }

    private void OnEnable()
    {
        _routine = RoutineHub.GetRoutine(ROUTINE_NAME + name);
        triggerRoutine();
    }

    void triggerRoutine()
        => _routine.DoRoutine(getRandomTime(), doFlicker);

    void doFlicker()
    {
        _curState = !_curState;

        if (_curState)
        {
            _light.intensity = _lightIntensity;
        } else
        {
            _light.intensity = 0;
        }

        triggerRoutine();
    }



    float getRandomTime()
    {
        return Random.Range(_minTime, _maxTime);
    }


}
