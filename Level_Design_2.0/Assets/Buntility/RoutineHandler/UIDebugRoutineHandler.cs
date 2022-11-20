/* ===========================================================
 * UI_DEBUG_ROUTINE_HANDLER
 * 
 * This is a DebugScript. You can safely remove the Prefab
 * ===========================================================
 */

using UnityEngine;
using TMPro;
using Buntility.Routines;

public class UIDebugRoutineHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtLog;

    Routine _routine;
    const string ROUTINE_NAME = "UIDebugRoutine";

    string _logString;


    // ----------------------------------
    // PUBLIC_ACCESSORS
    public void InstantiateRoutine()
    {
        if (_routine != null)
        {
            addToLogData("Killing Existing Routine.\n");
            RoutineHub.ReturnRoutine(_routine);
            _routine = null;
        }
        _routine = RoutineHub.GetRoutine(ROUTINE_NAME);
        addToLogData("Routine instantiated.", true);
    }

    public void KillRoutine()
    {
        if (_routine == null)
        {
            noRoutineInstantiated();
            return;
        }

        if (_routine.IsInRoutine)
        {
            addToLogData("Routine running, but the Callback will never happen because:\n");
        }

        RoutineHub.ReturnRoutine(_routine);
        _routine = null;
        addToLogData("Routine killed!", true);
    }

    public void DoRoutine(float inTime = 5f)
    {
        if (_routine == null)
        {
            noRoutineInstantiated();
            return;
        }

        if (_routine.IsInRoutine)
        {
            addToLogData("Routine currently running.\nThe Debugger will not interrupt it.\nYou can interrupt Routines, though!\nRoutine running...", true);
            return;
        }

        _routine.DoRoutine(inTime, finishRoutine);
        addToLogData($"Routine started: {inTime}sec\nRoutine running...", true);
    }

    void finishRoutine()
        => addToLogData("Callback sent.\nRoutine finished!", true);

    void noRoutineInstantiated()
        => addToLogData("No Routine instantiated.\nClick \"Instantiate Routine\" first.", true);

    // ----------------------------------
    // LOG_DATA
    void addToLogData(string inText, bool dump = false)
    {
        _logString += inText;
        if (dump) 
            dumpLogData();
    }
        
    void dumpLogData()
    {
        _txtLog.text = _logString;
        _logString = "";
    }
}
