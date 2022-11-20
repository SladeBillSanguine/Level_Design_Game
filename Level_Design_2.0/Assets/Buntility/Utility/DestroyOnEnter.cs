using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagHub.PLAYER)
        {
            Destroy(gameObject);
        }
    }
}
