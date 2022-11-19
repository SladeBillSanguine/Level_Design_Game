/* ===========================================================
 * FTB_LOCAL_HANDLER
 * 
 * Use FTBHandler to access those Functions!
 * ===========================================================
 */

using UnityEngine;

namespace Buntility.FadeToBlack
{
    public class FTBLocalHandler : MonoBehaviour
    {
        [SerializeField] UIFaderFTB _FTB;
        [SerializeField] UIFaderFTB _cinematic;

        // ---------------------------------------------
        // CONSTRUCTION
        private void OnEnable()
            => FTBHandler.SubscribeFTBLocalHandler(this);
        private void OnDisable()
            => FTBHandler.UnsubscribeFTBLocalHandler();


        // ---------------------------------------------
        // PUBLIC_ACCESSORS
        public void DoFTB(float inSpeed) 
            => _FTB.DoFadeIn(inSpeed);
        public void UndoFTB(float inSpeed) 
            => _FTB.DoFadeOut(inSpeed);

        public void ForceBlack()
            => _FTB.ForceBlack();
        public void ForceAlpha()
            => _FTB.ForceAlpha();

        public void DoCinematic(float inSpeed) 
            => _cinematic.DoFadeIn(inSpeed);
        public void UndoCinematic(float inSpeed) 
            => _cinematic.DoFadeOut(inSpeed);
    }
}
