using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KitchenandPlayer
{
    public static Player Instance {  get; private set; }

    [SerializeField]private float speed = 5.0f;
    [SerializeField]private float rotate_speed = 10f;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private LayerMask counterlayer;

    [SerializeField] private float throwForce = 10f;      // 投掷力量
    [SerializeField] private float throwHeight = 2f;      // 投掷高度
    [SerializeField] private float throwTorque = 5f;      // 旋转扭矩

    private BaseBlock currentSelectedBlock;
    private bool is_walking = false;

    [Header("冲刺设置")]
    [SerializeField] private float dashDistance = 3f;         // 冲刺距离
    [SerializeField] private float dashDuration = 0.2f;       // 冲刺时间                                                              
    [SerializeField] private LayerMask wallLayer;             // 墙壁图层
    [SerializeField] private float dashCooldown = 0.05f;          //cd
    private bool isDashing = false;                          // 是否正在冲刺
    private bool canDash = true;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInterAct += GameInput_OnInterAct;
        gameInput.TestAct += GameInput_TestAct;
        gameInput.Rush += GameInput_Rush;
    }

    private void GameInput_Rush(object sender, System.EventArgs shift)
    {
        if (canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private void GameInput_OnInterAct(object sender, System.EventArgs e)
    {
        InterAction();
    }
    private void GameInput_TestAct(object sender, System.EventArgs j)
    {
        //ThrowItem();
        ExtraInterAction();
    }
    void Update()
    {
        SetBlock();
    }
    private void FixedUpdate()
    {
        if (!isDashing)
        {
            PlayerMove();
        }
    }
    private void PlayerMove()
    {
        if (isDashing) return;

        Vector3 direction = gameInput.GetDirection();

        transform.position += direction * Time.deltaTime * speed;

        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotate_speed);
        }

        is_walking = direction != Vector3.zero;
    }
    private void InterAction()
    {
        bool is_collide = Physics.Raycast(transform.position,transform.forward, out RaycastHit hitinfo, 2f,counterlayer);
        if (is_collide)
        {
            if(hitinfo.transform.TryGetComponent<BaseBlock>(out BaseBlock counter))
            {
                counter.InterAct(this);
            }
        }
    }
    private void ExtraInterAction()
    {
        bool is_collide = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo, 2f, counterlayer);
        if (is_collide)
        {
            if (hitinfo.transform.TryGetComponent<BaseBlock>(out BaseBlock counter))
            {
                counter.ExtraInterAct(this);
            }
        }
    }
    private void SetBlock()
    {
        if (currentSelectedBlock != null)
        {
            currentSelectedBlock.SetFalse();
            currentSelectedBlock = null;
        }

        bool is_collide = Physics.Raycast(transform.position, transform.forward,
                                         out RaycastHit hitinfo, 2f, counterlayer);

        if (is_collide)
        {
            if (hitinfo.transform.TryGetComponent<BaseBlock>(out BaseBlock counter))
            {
                counter.SetTrue();
                currentSelectedBlock = counter;
            }
        }
    }
    public bool IsWalking
    {
        get
        { 
            return is_walking;
        }
        
    }
    private void ThrowItem()
    {
        // 检查手上是否有东西
        if (!FoodARU())
        {
            return;
        }

        // 获取手上的厨房物品
        Kitchen kitchen = GetKitchen();
        if (kitchen == null) return;

        // 移除父子关系，准备投掷
        kitchen.transform.SetParent(null);

        // 确保有刚体组件
        Rigidbody rb = kitchen.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = kitchen.gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        // 计算投掷方向（向前方）
        Vector3 throwDirection = transform.forward;

        // 添加向上的分量，让物体有抛物线效果
        Vector3 throwVector = throwDirection * throwForce + Vector3.up * throwHeight;

        // 应用投掷力
        rb.AddForce(throwVector, ForceMode.Impulse);

        // 添加随机旋转，让投掷看起来更自然
        Vector3 randomTorque = new Vector3(
            Random.Range(-throwTorque, throwTorque),
            Random.Range(-throwTorque, throwTorque),
            Random.Range(-throwTorque, throwTorque)
        );
        rb.AddTorque(randomTorque, ForceMode.Impulse);

        // 清空手上的物品
        ClearKitchen();

        Debug.Log("物品已投掷！");
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector3 direction = gameInput.GetDirection();
        if (direction == Vector3.zero)
        {
            direction = transform.forward;
        }
        else
        {
            direction = direction.normalized;
        }

        // 计算能冲刺多远
        float actualDistance = dashDistance;

        // 使用SphereCast检测，考虑玩家半径
        float playerRadius = 0.5f;  // 根据你的角色大小调整
        if (Physics.SphereCast(transform.position, playerRadius, direction, out RaycastHit hit, dashDistance, wallLayer))
        {
            actualDistance = hit.distance - 0.1f;  // 留一点缓冲
            if (actualDistance < 0) actualDistance = 0;
        }

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + direction * actualDistance;

        // 更平滑的移动曲线
        float timer = 0;
        while (timer < dashDuration)
        {
            float t = timer / dashDuration;
            // 使用缓动函数，冲刺开始和结束更平滑
            t = Mathf.Sin(t * Mathf.PI * 0.5f);  // 缓入缓出效果

            Vector3 newPos = Vector3.Lerp(startPos, targetPos, t);

            // 移动前再次检测，确保不会进入墙内
            if (Vector3.Distance(startPos, newPos) > 0.1f)
            {
                Vector3 moveDirection = (newPos - transform.position).normalized;
                float moveDistance = Vector3.Distance(transform.position, newPos);

                if (Physics.SphereCast(transform.position, playerRadius, moveDirection, out RaycastHit checkHit, moveDistance, wallLayer))
                {
                    // 如果会撞墙，停在墙前
                    newPos = transform.position + moveDirection * (checkHit.distance - 0.1f);
                    break;  // 撞墙了，停止冲刺
                }
            }

            transform.position = newPos;
            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}
