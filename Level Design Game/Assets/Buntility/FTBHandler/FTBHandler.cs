/* ===========================================================
 * FTB_HANDLER
 * 
 * Fades in UI Panels which blacken the Screen.
 * 
 * By default there is a Debug-Panel in the FTB-Prefab. You may delete it within the Prefab if not needed.
 * 
 * [1] FadeToBlack (FTB)
 * Trigger with DoFTB / UndoFTB
 * Default-Fadetime is 1f, but you may change _FTBTime;
 * 
 * [2] Fade in Cinematic Bars
 * Trigger with DoCinematic / UndoCinematic
 * Default-Fadetime is 1f, but you may change _cinematicTime;
 * 
 * In case you want to have specific times, simply trigger:
 *      DoFTB(MY_TIME);
 *      DoCinematic(MY_TIME);
 * 
 * By default both options BLOCK RAYCASTS.
 * 
 * If necessary, you may directly contact FTBLocalHandler to trigger FTBs by adding the FadeToBlack-namespace
 * Do NOT bypass FTBLocalHandler!
 * ===========================================================
 */

using Buntility.FadeToBlack;

public static class FTBHandler
{
    static FTBLocalHandler _handler;


    private static float _FTBTime = 1f;
    public static float FTBTime { get { return _FTBTime; } }

    private static float _cinematicTime = 1f;
    public static float CinematicTime { get { return _cinematicTime; } }


  
    // -----------------------------------------------------
    // PUBLIC_ACCESSORS
    /// <summary>
    /// Fades IN FTB-Panel
    /// </summary>
    /// <param name="inSpeed">Optional Time for the FadeIn</param>
    public static void DoFTB(float inSpeed = -1) 
        => _handler.DoFTB((inSpeed == -1) ? _FTBTime : inSpeed);

    /// <summary>
    /// Fades OUT FTB-Panel
    /// </summary>
    /// <param name="inSpeed">Optional Time for the FadeIn</param>
    public static void UndoFTB(float inSpeed = -1)
        => _handler.UndoFTB((inSpeed == -1) ? _FTBTime : inSpeed);

    public static void ForceBlack()
        => _handler.ForceBlack();
    public static void ForceAlpha()
        => _handler.ForceAlpha();

    /// <summary>
    /// Fades IN Cinematic-Panels
    /// </summary>
    /// <param name="inSpeed">Optional Time for the FadeIn</param>
    public static void DoCinematic(float inSpeed = -1)
        => _handler.DoCinematic((inSpeed == -1) ? _cinematicTime : inSpeed);

    /// <summary>
    /// Fades OUT Cinematic-Panels
    /// </summary>
    /// <param name="inSpeed">Optional Time for the FadeIn</param>
    public static void UndoCinematic(float inSpeed = -1)
        => _handler.UndoCinematic((inSpeed == -1) ? _cinematicTime : inSpeed);


    // -----------------------------------------------------
    // SUBSCRIPTION
    /// <summary>
    /// The instance FTBLocalHandler automatically subscribes to FTBHandler once instantiated/enabled
    /// </summary>
    /// <param name="inHandler">The current instance of FTBLocalHandler</param>
    public static void SubscribeFTBLocalHandler(FTBLocalHandler inHandler)
        => _handler = inHandler;

    /// <summary>
    /// The instance FTBLocalHandler automatically unssubscribes from FTBHandler once disabled/destroyed
    /// </summary>
    /// <param name="inHandler">The current instance of FTBLocalHandler</param>
    public static void UnsubscribeFTBLocalHandler()
        => _handler = null;
}
