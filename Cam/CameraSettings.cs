using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform focus;
    [SerializeField] [Range(0, 10)] float smoothSpeed;
    [SerializeField] float lookForwardDistance;

    private void LateUpdate()
    {
        Vector3 desiredPosition = focus.position + offset + (focus.forward * lookForwardDistance);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
