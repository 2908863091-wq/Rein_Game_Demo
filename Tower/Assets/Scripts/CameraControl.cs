using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    private static CameraControl Instance; 
    [Header("移动设置")]
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float deceleration = 6f;

    [Header("缩放设置")]
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float minHeight = 2f;
    [SerializeField] private float maxHeight = 100f;

    private Vector2 moveInput;
    private float currentMoveSpeed;
    private Vector3 myPosition;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction zoomAction;

    private void Start()
    {
        currentMoveSpeed = 0f;

        // 获取组件
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null && playerInput.actions != null)
        {
            moveAction = playerInput.actions["Move"];
            zoomAction = playerInput.actions["Zoom"];
        }

        myPosition = transform.position;
    }

    private void Update()
    {
        // 读取输入
        if (moveAction != null) moveInput = moveAction.ReadValue<Vector2>();
        if (zoomAction != null) HandleZoom();

        HandleMovement();
    }

    private void HandleMovement()
    {
        // 计算速度
        if (moveInput.magnitude > 0.1f)
        {
            currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, moveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, 0f, deceleration * Time.deltaTime);
        }

        // 应用移动
        if (currentMoveSpeed > 0.1f)
        {
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * currentMoveSpeed * Time.deltaTime;
            transform.position += movement;
        }
    }

    private void HandleZoom()
    {
        // 读取鼠标滚轮
        Vector2 scroll = zoomAction.ReadValue<Vector2>();

        // 只在有滚轮输入时调整高度
        if (Mathf.Abs(scroll.y) > 0.01f)
        {
            // 计算新高度（滚轮向下 = 降低高度，向上 = 升高高度）
            float newHeight = transform.position.y - scroll.y * scrollSpeed;
            newHeight = Mathf.Clamp(newHeight, minHeight, maxHeight);

            // 直接设置新高度
            transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
        }
    }
    public void ReSetPosition()
    {
        transform.position = myPosition;
    }
}