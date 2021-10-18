using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const string MouseScrollWheelAxis = "Mouse ScrollWheel";

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;
    public float minX = -20f;
    public float maxX = 40f;
    public float minZ = -20f;
    public float maxZ = 40f;

    private void Update()
    {
        if (GameManager.GameInProgress)
        {
            HandlePan();
            HandleZoom();
        }
    }

    private void HandlePan()
    {
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y > Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < 0 + panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x < 0 + panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        // place boundaries on how far we can pan
        var position = transform.position;

        position.x = Mathf.Clamp(transform.position.x, minX, maxX);
        position.z = Mathf.Clamp(transform.position.z, minZ, maxZ);

        transform.position = position;
    }

    private void HandleZoom()
    {
        var scroll = Input.GetAxis(MouseScrollWheelAxis);
        var position = transform.position;

        position.y -= scroll * scrollSpeed * Time.deltaTime * 1000;
        position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;
    }
}
