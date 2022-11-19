/* ===========================================================
 * ROUTINE_HUB
 * 
 * Instantiates Routines.
 * Routines do..
 * [1] a Delay
 * [2] send back a Callback
 * 
 * Set PauseRoutines to "true" if you want all Routines to
 * stop.
 * ===========================================================
 */

using UnityEngine;
using System.Collections.Generic;
using Buntility.Routines;

public static class RoutineHub
{
    static RoutineCaster _routineCaster;

    const string PAUSED = "paused";
    const string RUNNING = "running";

    static bool _pauseRoutines;
    public static bool PauseRoutines
    {
        get { return _pauseRoutines; }
        set
        {
            _pauseRoutines = value;
            //debugPaused();
        }
    }

    static List<Routine> _unusedRoutines;

    const string ROUTINE_NAME = "Routine: ";

    // ---------------------------------------
    // CONSTRUCTOR
    static RoutineHub()
    {
        _unusedRoutines = new List<Routine>();
    }



    // ---------------------------------------
    // PUBLIC_ACCESSORS
    public static Routine GetRoutine(string inName)
    {
        Routine newRoutine = null;

        if (_unusedRoutines.Count != 0)
        {
            newRoutine = _unusedRoutines[0];
            _unusedRoutines.RemoveAt(0);
        } else
        {
            newRoutine = _routineCaster.InstantiateRoutine();
        }

        newRoutine.name = ROUTINE_NAME + inName;
        return newRoutine;
    }

    public static void ReturnRoutine(Routine inRoutine)
    {
        inRoutine.StopRoutine();
        _unusedRoutines.Add(inRoutine);
    } 
 
    // ---------------------------------------
    // SUBSCRIPTION
    public static void SubscribeToRoutineHub(RoutineCaster inCaster) 
        => _routineCaster = inCaster;
    public static void UnsubscribeRoutineHub() 
        => _routineCaster = null;


    // ---------------------------------------
    // UTILITY

    static void debugPaused()
    {
        Debug.Log($"RoutineHub: Routines are {getPausedState()}");
    }
    static string getPausedState()
    {
        return (_pauseRoutines) ? PAUSED : RUNNING;
    }

}
