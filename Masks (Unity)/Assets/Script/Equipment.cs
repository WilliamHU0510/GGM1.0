using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSimple : MonoBehaviour
{
    [Header("武器对象")]
    public GameObject weapon1; // 状态1的武器
    public GameObject weapon2; // 状态2的武器
    
    [Header("玩家状态管理器")]
    public PlayerStateManager stateManager;
    
    void Start()
    {
        // 自动查找状态管理器
        if (stateManager == null)
        {
            stateManager = FindObjectOfType<PlayerStateManager>();
        }
        
        // 订阅状态变化事件
        if (stateManager != null)
        {
            stateManager.OnStateChanged += OnPlayerStateChanged;
            // 应用初始状态
            OnPlayerStateChanged(stateManager.IsState1);
        }
        else
        {
            Debug.LogWarning("EquipmentSimple: 未找到PlayerStateManager！");
            // 默认显示武器1
            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }
    }
    
    // 玩家状态变化时的处理
    private void OnPlayerStateChanged(bool isState1)
    {
        if (weapon1 == null || weapon2 == null)
        {
            Debug.LogError("武器对象未分配！");
            return;
        }
        
        weapon1.SetActive(isState1);
        weapon2.SetActive(!isState1);
        
        Debug.Log($"切换到 {(isState1 ? "武器1" : "武器2")}");
    }
    
    void OnDestroy()
    {
        // 取消订阅事件
        if (stateManager != null)
        {
            stateManager.OnStateChanged -= OnPlayerStateChanged;
        }
    }
}