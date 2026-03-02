using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [SerializeField] private GameObject buildEffect;
    private GameObject turretGO;
    private TurretData turret;          // 当前炮塔的数据（指向最初建造时的数据）
    private bool isUpgraded = false;    // 标记是否已经升级过（防止再次升级）
    private Color normalColor;

    private void Awake()
    {
        normalColor = GetComponent<MeshRenderer>().material.color;
    }

    private void OnMouseDown()
    {
        SpawnTurret();
    }

    private void SpawnTurret()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        BuildManager bm = BuildManager.Instance;
        BuildManager.State currentState = bm.state;

        switch (currentState)
        {
            case BuildManager.State.BuildStandard:
            case BuildManager.State.BuildMissile:
            case BuildManager.State.BuildLaser:
                HandleBuild(bm);
                break;
            case BuildManager.State.Upgrade:
                HandleUpgrade(bm);
                break;
            case BuildManager.State.Break:
                HandleBreak(bm);
                break;
        }
    }

    // 建造
    private void HandleBuild(BuildManager bm)
    {
        TurretData turretData = bm.selectedTurretData;
        if (turretData == null || turretData.prefab == null) return;
        if (turretGO != null) return; // 格子已被占用

        if (bm.GetMoney() < turretData.cost)
        {
            bm.MoneyAnimation();
            return;
        }

        turret = turretData;
        turretGO = Instantiate(turretData.prefab, transform.position, Quaternion.identity);
        isUpgraded = false; // 新炮塔未升级
        bm.AddMoney(-turretData.cost);

        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    // 升级（使用 nextPrefab）
    private void HandleUpgrade(BuildManager bm)
    {
        if (turretGO == null) return;               // 没有炮塔
        if (isUpgraded) return;                      // 已经升级过，防止重复升级
        if (turret.nextPrefab == null) return;       // 没有下一级预制体

        if (bm.GetMoney() < turret.upCost)
        {
            bm.MoneyAnimation();
            return;
        }

        // 销毁旧炮塔
        Destroy(turretGO);

        // 实例化新炮塔（使用 nextPrefab）
        turretGO = Instantiate(turret.nextPrefab, transform.position, Quaternion.identity);
        bm.AddMoney(-turret.upCost);
        isUpgraded = true; // 标记已升级

        // ⚠️ 注意：turret 变量仍指向原来的数据（因为无法获取新数据）
        // 这意味着后续拆除时仍按原 cost 返还，且无法再次升级

        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    // 拆除
    private void HandleBreak(BuildManager bm)
    {
        if (turretGO == null) return;

        // 无论是否升级，都按最初建造费用的一半返还
        int refund = turret.cost / 2;
        bm.AddMoney(refund);
        Destroy(turretGO);
        turretGO = null;
        turret = null;
        isUpgraded = false;

        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    private void OnMouseEnter()
    {
        if (turretGO == null && !EventSystem.current.IsPointerOverGameObject())
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material.color = normalColor;
    }
}