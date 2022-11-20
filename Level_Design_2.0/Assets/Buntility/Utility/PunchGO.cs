using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Buntility.Routines;

public class PunchGO : MonoBehaviour
{
    [SerializeField] float _frequency = 3f;
    [SerializeField] float _punchSize = 1.5f;

    Vector3 _punchVector;
    string _tweenID;

    Routine _routine;

    // Start is called before the first frame update
    void Start()
    {
        _punchVector = new Vector3(_punchSize, _punchSize, _punchSize);
        _tweenID = gameObject.GetInstanceID().ToString();
        _routine = RoutineHub.GetRoutine(_tweenID + "PunchGO");

        delayPunch();
    }

    private void OnDisable()
    {
        if (_routine != null)
            RoutineHub.ReturnRoutine(_routine);
        DOTween.Kill(_tweenID);
    }

    void delayPunch()
        => _routine.DoRoutine(_frequency, doPunch);

    void doPunch()
        => transform.DOPunchScale(_punchVector, .5f).OnComplete(delayPunch).SetId(_tweenID);
}
