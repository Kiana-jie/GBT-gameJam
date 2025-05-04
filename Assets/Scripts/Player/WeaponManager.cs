using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;              // ��������ģ�͵ĸ����壨���������ϣ�
    public List<Slot> equipSlots;               // ���� UI װ���ۣ�Slot �ű���
    public float radius = 2f;                   // ���ƾ���

    public void LoadWeaponsFromSlots()
    {
        // ���������ģ��
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
            Debug.LogWarning("û���������Լ���");
            return;
        }
        // �ȼ����ж��ٸ�����
        foreach (var slot in equipSlots)
        {
            // ��ȡ itemPrefab
            GameObject prefab = slot.itemPrefab;
            if (prefab == null)
            {
                Debug.LogWarning("Slot �� prefab Ϊ null");
                continue;
            }

            // ���� 2D ����λ�ã�XY ƽ�棩
            float angle = 360f / totalWeapons * count;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
            Vector3 spawnPos = transform.position + offset;

            // ʵ��������������Ϊ weaponHolder ��������
            GameObject weaponObj = Instantiate(prefab, spawnPos, Quaternion.identity, weaponHolder);

            // ����������ʼ���������Ҫ��ת������
            Vector3 dir = (transform.position - weaponObj.transform.position).normalized;
            float weaponAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weaponObj.transform.rotation = Quaternion.Euler(0, 0, weaponAngle);

            // ȷ�������ľֲ�����Ϊ1
            //weaponObj.transform.localScale = Vector3.one;

            // �ۼƼ���
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
