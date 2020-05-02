using UnityEngine;
using UnityEngine.AI;
using SP.Core;
using SP.Saving;

namespace SP.Move
{
    public class Movement : MonoBehaviour, ActionInterface, ISaveable
    {
        private void Update()
        {
            UpdateAnimation();
            //GetComponent<Animator>().updateMode(AnimatorUpdateMode.AnimatePhysics);
        }

        private void UpdateAnimation()
        {
            Vector3 agentVelocity = Vector3.zero;
            if (GetComponent<NavMeshAgent>()) agentVelocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localAgentVelocity = transform.InverseTransformDirection(agentVelocity);//Gets the local velocity instead of the world velocity
            float speed = localAgentVelocity.z;
            GetComponent<Animator>().SetFloat("forwardRunSpeed", speed);
        }

        public void MoveTo(Vector3 destination)
        {
            if (GetComponent<NavMeshAgent>() && GetComponent<NavMeshAgent>().enabled)
            {
                GetComponent<NavMeshAgent>().destination = destination;
                GetComponent<NavMeshAgent>().isStopped = false;
            }
        }

        public void MoveAction(Vector3 destination)
        {
            GetComponent<ActionAgenda>().Act(this);
            MoveTo(destination);
        }

        public void Cancel()
        {
            if (GetComponent<NavMeshAgent>() && GetComponent<NavMeshAgent>().enabled)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
            }
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            if(GetComponent<NavMeshAgent>()) GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            if (GetComponent<NavMeshAgent>()) GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
