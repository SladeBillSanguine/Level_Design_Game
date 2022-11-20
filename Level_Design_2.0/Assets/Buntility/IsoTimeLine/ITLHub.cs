/* ==========================================================
 * ITL_HUB
 * (Isolated Timeline Hub)
 * 
 * IMPORTANT: Use ITLTrigger to trigger Timelines!
 * 
 * ITLHub ensures that the current VCam got the highest Priority. Furthermore additional Timeline Triggers will be stopped, as long as ITLTrigger.OnlySinglePlay == TRUE;
 * ===========================================================
 */

using System;


namespace Buntility.IsoTimeline
{
    public static class ITLHub
    {
        public const int VCAM_PRIO_INACTIVE = 0;
        public const int VCAM_PRIO_ACTIVE = 2;

        static bool _activeITL;
        public static bool ActiveITL { get { return _activeITL; } }

        static Action _resetVCams;


        public static void StartTimeline(ITLTrigger inTrigger)
        {
            _activeITL = true;
            _resetVCams?.Invoke();
            inTrigger.SetVCamActive();
        }

        public static void StopTimeLine()
        {
            _activeITL = false;
            _resetVCams?.Invoke();
        }


        public static void SubscribeToVCamReset(Action inAction)
            => _resetVCams += inAction;
        public static void UnSubscribeVCamReset(Action inAction)
            => _resetVCams -= inAction;
    }
}
