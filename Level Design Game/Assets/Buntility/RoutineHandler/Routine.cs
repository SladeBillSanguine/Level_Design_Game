/* ===========================================================
 * ROUTINE
 * 
 * A Routine, which does a task over time. 
 * A callback needs to be supplied which will trigger whatever happens
 * afterwards.
 * 
 * [SETUP]
 * Use the "RoutineCaster"-Prefab and put it into a Scene which does not get deleted.
 * 
 * [1] Requesting Routine:
 * Contact RoutineHub.GetRoutine() to get a Routine. It will do the
 * instantiation process.
 * 
 * [2] Use DoRoutine(TIME, CALLBACK) to start the Routine.
 * The Callback needs to be a method without params, which
 * will initiate what is supposed to happen after the Routine ended.
 * 
 * [3] To pause a Routine, use: RoutineHub.PauseRoutines = true;
 * 
 * [4] IMPORTANT: Always clean up Routines!
 * If a GO requested one and if it gets destroyed, make sure to
 * cast _myRoutine.KillRoutine(); in "OnDisable"
 * ===========================================================
 */

using System.Collections;
using UnityEngine;
using System;

namespace Buntility.Routines
{
    public class Routine : MonoBehaviour
    {
        Coroutine _routine;
        public bool IsInRoutine { get { return (_routine != null); } }

        Action _callback;


        private void OnDestroy()
        {
            StopRoutine();
        }

        // -------------------------------------------
        // PUBLIC_ACCESSORS
        public void DoRoutine(float inTime, Action inCallback)
        {
            StopRoutine();
            _callback = inCallback;
            _routine = StartCoroutine(doDelay(inTime));
        }

        public void KillRoutine()
        {
            StopRoutine();
            Destroy(gameObject);
        }


        public void StopRoutine()
        {
            _callback = null;
            if (_routine != null)
                StopCoroutine(_routine);
        }

        // -------------------------------------------
        // DELAY_ROUTINE
        IEnumerator doDelay(float inTime)
        {
            float timer = 0f;
            while (timer < inTime)
            {
                while (RoutineHub.PauseRoutines)
                {
                    yield return null;
                }

                timer += Time.deltaTime;
                yield return null;
            }
            _routine = null;

            _callback?.Invoke();
        }

        // -------------------------------------------
        // UTILITY
        
    }

}
