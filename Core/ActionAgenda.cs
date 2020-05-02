using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Core
{
    public class ActionAgenda : MonoBehaviour
    {
        ActionInterface actionCurrent;
        public void Act(ActionInterface action)
        {
            if (actionCurrent == action) return;
            if (actionCurrent != null)
            {
                actionCurrent.Cancel();
            }
            actionCurrent = action;          
        }

        public void CancelCurrentAction()
        {
            Act(null);
        }
    }
}
