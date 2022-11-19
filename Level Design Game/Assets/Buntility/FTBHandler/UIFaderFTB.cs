/* ===========================================================
 * UI_FADER_FTB
 * 
 * Takes care of the Fading for the FTBHandler. Use FTBHandler for access
 * ===========================================================
 */

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Buntility.FadeToBlack
{
    public class UIFaderFTB : MonoBehaviour
    {
        [SerializeField] Image[] _images;

        [SerializeField] bool _hideOnStart = true;

        EFTBState _FTBState = EFTBState.none;

        string _tweenID;

        // ------------------------------------------
        // CONSTRUCTION
        private void Awake()
        {
            _tweenID = Guid.NewGuid().ToString();
            if (_hideOnStart)
            {
                setValue(0);
                setPanels();
            }
        }

        // ------------------------------------------
        // PUBLIC_ACCESSORS
        public void DoFadeIn(float inFadeTime)
        {
            _FTBState = EFTBState.fadingIn;
            setPanels();
            doFade(inFadeTime, 1);
        }
        public void DoFadeOut(float inFadeTime)
        {
            _FTBState = EFTBState.fadingOut;
            doFade(inFadeTime, 0);
        }

        public void ForceBlack()
        {
            _FTBState = EFTBState.fadingIn;
            setPanels();
            setValue(1);
        }
        public void ForceAlpha()
        {
            _FTBState = EFTBState.none;
            setPanels();
            setValue(0);
        }


        // ------------------------------------------
        // DO_FADE
        void doFade(float inFadeTime, float targetValue)
        {
            DOTween.Kill(_tweenID);
            DOTween.To(setValue, _images[0].color.a, targetValue, inFadeTime).OnComplete(finaliseFTB);
        }

        void setValue(float inValue)
        {
            foreach (Image image in _images)
            {
                Color col = image.color;
                col.a = inValue;
                image.color = col;
            }
        }

        void finaliseFTB()
        {
            if (_FTBState == EFTBState.fadingOut)
                setPanels();
            _FTBState = EFTBState.none;
        }


        // ------------------------------------------
        // UTILITY
        void setPanels()
        {
            foreach (Image img in _images)
            {
                if (_FTBState == EFTBState.fadingIn)
                    img.gameObject.SetActive(true);
                else
                    img.gameObject.SetActive(false);
            }
        }
    }


    public enum EFTBState
    {
        none,
        fadingIn,
        fadingOut
    }
}