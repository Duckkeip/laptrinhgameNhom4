using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1, -10); // <-- Sửa z về -10
    public float smoothTime = 0.2f;

    public float idleThreshold = 3f;
    public float zoomOutMultiplier = 0.5f;// he so duoi < 1 thi lai gan ,>1 la zoom xa
    public float zoomSpeed = 0.45f;
    public float defaultSize = 3f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastTargetPosition;
    private float idleTimer = 0f;

    
    void Start(){
        if (Camera.main != null)
        {
            Debug.Log("da co cam ");
            Camera.main.orthographicSize = defaultSize;
        }
        
        lastTargetPosition = target.position;
    }
    void LateUpdate()
    {   
        if (target == null || Camera.main == null) return;

    // Kiểm tra đứng yên
    if (Vector3.Distance(target.position, lastTargetPosition) < 0.001f)
    {
        idleTimer += Time.deltaTime;
    }
    else
    {
        idleTimer = 0f;
    }

    lastTargetPosition = target.position;

    // Tính kích thước zoom hiện tại
    float targetSize = defaultSize;
    if (idleTimer > idleThreshold)
    {
        targetSize = defaultSize * zoomOutMultiplier;
    }

    // Zoom mượt
    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);


        // Di chuyển camera
        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z
        );

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
