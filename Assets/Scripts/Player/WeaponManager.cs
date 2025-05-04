using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;              // 放置武器模型的父物体（在人物身上）
    public List<Slot> equipSlots;               // 所有 UI 装备槽（Slot 脚本）
    public float radius = 2f;                   // 环绕距离

    public void LoadWeaponsFromSlots()
    {
        // 清除旧武器模型
        foreach (Transform child in weaponHolder)
        {
            Destroy(child.gameObject);
        }

        int count = 0;
        int totalWeapons = 0;
        foreach (var slot in equipSlots)
        {
            if (IsSlotWeapon(slot))
            {
                totalWeapons++;
            }
        }

        if (totalWeapons == 0)
        {
            Debug.LogWarning("没有武器可以加载");
            return;
        }
        // 先计算有多少个武器
        foreach (var slot in equipSlots)
        {
            // 获取 itemPrefab
            GameObject prefab = slot.itemPrefab;
            if (prefab == null)
            {
                Debug.LogWarning("Slot 中 prefab 为 null");
                continue;
            }

            // 计算 2D 环绕位置（XY 平面）
            float angle = 360f / totalWeapons * count;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
            Vector3 spawnPos = transform.position + offset;

            // 实例化武器，并作为 weaponHolder 的子物体
            GameObject weaponObj = Instantiate(prefab, spawnPos, Quaternion.identity, weaponHolder);

            // 设置武器初始朝向（如果需要旋转武器）
            Vector3 dir = (transform.position - weaponObj.transform.position).normalized;
            float weaponAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weaponObj.transform.rotation = Quaternion.Euler(0, 0, weaponAngle);

            // 确保武器的局部缩放为1
            //weaponObj.transform.localScale = Vector3.one;

            // 累计计数
            count++;
        }
    }

    private bool IsSlotWeapon(Slot slot)
    {
        if (slot.transform.childCount == 0) return false;

        var itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
        if (itemUI != null )
        {
            return true;
        }

        return false;
    }
}
