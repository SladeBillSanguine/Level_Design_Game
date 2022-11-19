/* =========================================
 * AUTO_TRIGGER
 * 
 * Triggers _dialogueStarter as soon as this object gets enabled!
 * Disable the GO if you do not want to trigger it right away when the scene loads
 */

using Buntility.Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buntility.DialogueSystem
{
    public class AutoTrigger : MonoBehaviour
    {
        [SerializeField] DiaElement _dialogueStarter;

        Routine _routine;
        const string ROUTINE_ID = "AutoTrigger";

        private void OnEnable()
        {
            _routine = RoutineHub.GetRoutine(ROUTINE_ID + name);
            _routine.DoRoutine(.1f, triggerDialogue);
        }

        void triggerDialogue()
        {
            _dialogueStarter.TriggerDialogue();
            RoutineHub.ReturnRoutine(_routine);
            _routine = null;
        }
    }
}