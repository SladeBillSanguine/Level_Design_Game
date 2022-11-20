/* =========================================
 * INTRO_HANDLER
 * 
 * This is a BASIC IntroHandler, which simply shows one GO (which should be an UI Element) referenced in GameObject _introObj for float _dispTime.
 * 
 * Afterwards it will destroy itself.
 * 
 * Ensure to add a Callback when calling IntroHandler so it is known that the Intro is done!
 */

using UnityEngine;
using System;
using Buntility.Routines;

namespace Buntility.GameMenu
{
    public class IntroHandler : MonoBehaviour
    {
        [SerializeField] GameObject _introObj;
        [SerializeField] float _dispTime;

        Action _callback;

        Routine _routine;

        // --------------------------------
        // CONSTRUCTION
        private void Awake()
        {
            _introObj.SetActive(false);
        }

        private void Start()
        {
            _routine = RoutineHub.GetRoutine("Intro");
        }


        // --------------------------------
        // TRIGGER_INTRO
        public void DoIntro(Action inCallback)
        {
            _introObj.SetActive(true);
            _callback = inCallback;
            _routine.DoRoutine(_dispTime, finishIntro);
        }

        void finishIntro()
        {
            _introObj.SetActive(false);
            _callback?.Invoke();

            Destroy(gameObject);
        }
    }
}
