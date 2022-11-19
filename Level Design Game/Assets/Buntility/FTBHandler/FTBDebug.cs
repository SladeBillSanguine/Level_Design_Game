/* ==================================
 * FTB_DEBUG
 * 
 * This is a debug Script which is supposed to show the Functions of FTBHandler.
 * If not needed, you can remove this script safely!
 */

using UnityEngine;

public class FTBDebug : MonoBehaviour
{
    public void DoFTB()
        => FTBHandler.DoFTB();
    public void UndoFTB()
        => FTBHandler.UndoFTB();

    public void DoFTB(float inTime)
        => FTBHandler.DoFTB(inTime);
    public void UndoFTB(float inTime)
        => FTBHandler.UndoFTB(inTime);

    public void DoCinematic()
        => FTBHandler.DoCinematic();
    public void UndoCinematic()
        => FTBHandler.UndoCinematic();
}
