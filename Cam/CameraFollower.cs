using UnityEngine;

namespace SP.Cam
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] Transform target;

        private void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
