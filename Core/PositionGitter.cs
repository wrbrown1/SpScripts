using UnityEngine;

public class PositionGitter : MonoBehaviour
{
    Vector3 startingPosition;
    float timer = 0f;
    int counter = 0;
    private Vector3 velocity = Vector3.zero;
    Vector3 desiredPosition;
    float xPosBase = 0f;
    float xPos = 0f;
    bool needsToJumpBack = true;
    [SerializeField] float gitterRange = 0f;
    [SerializeField] float gitterSmoothingSpeed = 0f;
    private void Start()
    {
        xPosBase = transform.position.x;
    }
    private void Update()
    {
        if (!needsToJumpBack)
        {
            float jump = Random.Range(-gitterRange, gitterRange);
            xPos += jump;
            desiredPosition = new Vector3(xPos, transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, gitterSmoothingSpeed);
            transform.position = smoothedPosition;
            timer = 0f;
            needsToJumpBack = true;
        }
        else
        {
            desiredPosition = new Vector3(xPosBase, transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, gitterSmoothingSpeed);
            transform.position = smoothedPosition;
            xPos = xPosBase;
            timer = 0f;
            needsToJumpBack = false;
        }
    }
}
