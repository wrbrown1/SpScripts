using UnityEngine;
using SP.Move;
using SP.Combat;

namespace SP.Control
{
    public class PlayerCharacterController : MonoBehaviour
    {
        private void Update()
        {
            if (HandlingCombat()) return;
            if (HandlingMovement()) return;
            print("Do nothing");
        }

        private bool HandlingCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(ClickRay());
            foreach(RaycastHit hit in hits)
            {
                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target == null) continue;
                if (Input.GetMouseButtonDown(0)) continue;
                if (!GetComponent<Combatant>().CanAttack(target.gameObject)) continue;
                if(Input.GetMouseButtonDown(1)) {
                    GetComponent<Combatant>().Attack(target.gameObject);                   
                }
                return true;
            }
            return false;
        }

        private bool HandlingMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(ClickRay(), out hit))
            {
                if (Input.GetMouseButton(0))
                {
                    if(Vector3.Distance(transform.position, hit.point) > 2.5f)
                    {
                        GetComponent<Movement>().MoveAction(hit.point);
                    }
                }
                return true;
            }
            return false;
        }

        private static Ray ClickRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
