/* =========================================
 * DIA_DELAY
 * 
 * In case you are using an IsoTimeline.cs or FTBTrigger.cs, the action will take place, but afterwards the Dialogue will be stuck. Use this script to force the dialogue to continue after a defined time.
 * 
 * Important: you need to trigger DiaDelay.cs individually with the UnityEvent _onStartEvent in DiaElement.cs
 */

using Buntility.Routines;
using UnityEngine;

namespace Buntility.DialogueSystem
{
    public class DiaDelay : MonoBehaviour
    {
        [SerializeField] float _delayTime;

        Routine _routine;
        const string ROUTINE_ID = "DiaDelay";

        public void StartDiaDelay()
        {
            //Debug.Log("DiaDelay triggered");
            _routine = RoutineHub.GetRoutine(name + ROUTINE_ID);
            _routine.DoRoutine(_delayTime, diaDelayFinished);
        }

        void diaDelayFinished()
        {
            //Debug.Log("DiaDelay finished");
            DiaSys.NextDia();
            RoutineHub.ReturnRoutine(_routine);
            _routine = null;
        }
    }
}
