using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform
    public PlayAreaClamp playAreaClamp; // Reference to the PlayAreaClamp script

    private Camera _camera;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        cameraHalfWidth = _camera.orthographicSize * _camera.aspect;
        cameraHalfHeight = _camera.orthographicSize;
    }

    private void Update()
    {
        if (target != null && playAreaClamp != null)
        {
            float clampedX = Mathf.Clamp(target.position.x,
                                         playAreaClamp.minClamp.x + cameraHalfWidth,
                                         playAreaClamp.maxClamp.x - cameraHalfWidth);

            float clampedY = Mathf.Clamp(target.position.y,
                                         playAreaClamp.minClamp.y + cameraHalfHeight,
                                         playAreaClamp.maxClamp.y - cameraHalfHeight);

            Vector3 desiredPosition = new Vector3(clampedX, clampedY, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
        }
    }
}
