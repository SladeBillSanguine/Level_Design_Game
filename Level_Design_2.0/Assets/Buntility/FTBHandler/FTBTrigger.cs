/* =========================================
 * FTB_TRIGGER
 * 
 * Use this script to trigger the FTBHandler from a Dialogue (DiaElement.cs)
 * 
 * [1] Add this to your DiaElement
 * [2] Configure the type and time.
 *      [a] doFTB: fades to black, and then back to alpha within _ftbTime
 *      [b] fadeToBlack: fades to black within _ftbTime. 
 *          Use this is you want the screen to remain black for a while!
 *      [c] fadeToAlpha: fades to alpha within _ftbTime.
 *      [d] forceBlack: immediately turns the screen black
 *      [e] forceAlpha: immediately turns on the alpha
 *      [f] cinematicOn: activates Filmbars within _ftbTime
 *      [g] cinematicOff: deactivates Filmbars within _ftbTime
 */


using UnityEngine;
using Buntility.Routines;
using Buntility.DialogueSystem;

namespace Buntility.FadeToBlack
{
    public class FTBTrigger : MonoBehaviour
    {
        [SerializeField] FTBTriggerType _type;
        [SerializeField] float _ftbTime;
        Routine _routine;
        const string FTB = "[FTB]";


        enum FTBTriggerType
        {
            doFTB,
            fadeToBlack,
            fadeToAlpha,
            forceBlack,
            forceAlpha,
            cinematicOn,
            cinematicOff
        }

        private void OnDisable()
        {
            if (_routine != null)
            {
                RoutineHub.ReturnRoutine(_routine);
                _routine = null;
            }
        }

        public void TriggerFTB()
        {
            //Debug.Log($"FTB triggered: {_type}");
            switch (_type)
            {
                case FTBTriggerType.doFTB:
                    _routine = RoutineHub.GetRoutine(name + FTB);
                    _routine.DoRoutine(_ftbTime / 2, ftbIsBlack);
                    FTBHandler.DoFTB(_ftbTime / 2);
                    return;
                case FTBTriggerType.fadeToBlack:
                    _routine = RoutineHub.GetRoutine(name + FTB);
                    _routine.DoRoutine(_ftbTime, finishAction);
                    FTBHandler.DoFTB(_ftbTime);
                    return;
                case FTBTriggerType.fadeToAlpha:
                    _routine = RoutineHub.GetRoutine(name + FTB);
                    _routine.DoRoutine(_ftbTime, finishAction);
                    FTBHandler.UndoFTB(_ftbTime);
                    return;
                case FTBTriggerType.forceBlack:
                    FTBHandler.ForceBlack();
                    return;
                case FTBTriggerType.forceAlpha:
                    FTBHandler.ForceAlpha();
                    return;
                case FTBTriggerType.cinematicOn:
                    _routine = RoutineHub.GetRoutine(name + FTB);
                    _routine.DoRoutine(_ftbTime, finishAction);
                    FTBHandler.DoCinematic(_ftbTime);
                    return;
                case FTBTriggerType.cinematicOff:
                    _routine = RoutineHub.GetRoutine(name + FTB);
                    _routine.DoRoutine(_ftbTime, finishAction);
                    FTBHandler.UndoCinematic(_ftbTime);
                    return;
            }
        }

        void ftbIsBlack()
        {
            _routine.DoRoutine(_ftbTime / 2, finishAction);
            FTBHandler.UndoFTB(_ftbTime / 2);
        }

        void finishAction()
        {
            //Debug.Log("FTB finished");
            RoutineHub.ReturnRoutine(_routine);
            _routine = null;
        }
    }
}
