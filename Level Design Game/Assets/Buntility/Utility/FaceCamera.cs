/* =========================================
 * FACE_CAMERA
 * 
 * Forces the GO to always rotate into the direction of MainCamera.
 * 
 * If it does not work (It rotates in the wrong direction, that might especially happen with TMPro), make an empty GO and inside rotate the object accordingly
 */
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
