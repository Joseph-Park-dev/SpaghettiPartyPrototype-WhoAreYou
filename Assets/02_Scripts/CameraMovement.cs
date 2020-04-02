using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float boundX;
    [SerializeField] private float boundY;
    float camSpeed = 5.0f;

    private void Update()
    {
        ClampCamera();
    }

    void ClampCamera()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = player.transform.position;
        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(cursorPos.x, playerPos.x - boundX, playerPos.x + boundX),
            Mathf.Clamp(cursorPos.y, 1.65f, playerPos.y + boundY),
            -10
            );
        Vector3 cameraPos = Vector3.Lerp(
            transform.position,
            clampedPos,
            camSpeed * Time.deltaTime);
        transform.position = cameraPos;
    }
}
