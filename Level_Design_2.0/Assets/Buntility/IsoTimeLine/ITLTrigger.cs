/* ======================================================
 * ITL_TRIGGER
 * (Isolated Timeline Trigger)
 * 
 * This Script:
 * [1] triggers Timelines
 * [2] activates the according VCam and sets a higher Priority
 * 
 * [SETUP]
 * [1] Build an Empty, add ITLTrigger
 * [2] Create Timeline, reference it to _timeline in ITLTrigger
 * [3] [OPTIONAL] Add a _vCam
 * [4] After editing Timeline, trigger it with "TriggerTimeline()"
 * 
 * If in doubt, add "Sample_Iso" to your Scene and try it out!
 * ===========================================================
 */

using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using Buntility.Routines;
using Buntility.DialogueSystem;

namespace Buntility.IsoTimeline
{
    public class ITLTrigger : MonoBehaviour
    {
        [SerializeField] PlayableDirector _timeline;

        [SerializeField] CinemachineVirtualCamera _vCam;

        // if you set this Variable to FALSE, then it can be triggered additionally to other Timelines!
        [SerializeField] bool _onlySinglePlay = true;

        Routine _routine;


        // ---------------------------------
        // CONSTRUCTION
        private void OnEnable()
        {
            if (_vCam != null)
            {
                _vCam.gameObject.SetActive(false);
            }

            ITLHub.SubscribeToVCamReset(resetVCam);
            _routine = RoutineHub.GetRoutine(name);
        }
        private void OnDisable()
        {
            ITLHub.UnSubscribeVCamReset(resetVCam);
            if (_routine != null)
            {
                RoutineHub.ReturnRoutine(_routine);
                _routine = null;
            }
        }


        // ---------------------------------
        // DO_TIMELINE
        public void TriggerTimeline()
        {
            if (ITLHub.ActiveITL && _onlySinglePlay) return;
            Debug.Log("PlayingTimeline");
            ITLHub.StartTimeline(this);

            _timeline.Play();

            _routine.DoRoutine((float)_timeline.duration, finishMovement);
        }

        void finishMovement()
        {
            //Debug.Log("Finished");
            ITLHub.StopTimeLine();
        }


        // -------------------------------
        // SET_VCAM
        public void SetVCamActive()
        {
            if (_vCam == null)
                return;

            _vCam.gameObject.SetActive(true);
            setVCam(ITLHub.VCAM_PRIO_ACTIVE);
        }

        void resetVCam()
        {
            if (_vCam == null)
                return;

            setVCam(ITLHub.VCAM_PRIO_INACTIVE);
            _vCam.gameObject.SetActive(false);
        }

        void setVCam(int inPriority)
        {
            //Debug.Log($"{name}: SetVcam: {inPriority}");
            _vCam.Priority = inPriority;
        }
    }
}
