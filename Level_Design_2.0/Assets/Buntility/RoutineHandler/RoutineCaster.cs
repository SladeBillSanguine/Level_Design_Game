/* ===========================================================
 * ROUTINE_CASTER
 * 
 * IMPORTANT: Use RoutineHub to Instantiate Routines!!! 
 * 
 * RoutineCaster is a HOLDER for all Routines.
 * To Debug, you can look into the hierarchy so you can see all running Routines.
 * ===========================================================
 */
using UnityEngine;

namespace Buntility.Routines
{
    public class RoutineCaster : MonoBehaviour
    {
        [SerializeField] GameObject _routinePrefab;
        

        private void OnEnable()
            => RoutineHub.SubscribeToRoutineHub(this);

        private void OnDisable()
            => RoutineHub.UnsubscribeRoutineHub();

        public Routine InstantiateRoutine()
        {
            GameObject newRoutine = Instantiate(_routinePrefab, transform);
            return newRoutine.GetComponent<Routine>();
        }
    }

}
