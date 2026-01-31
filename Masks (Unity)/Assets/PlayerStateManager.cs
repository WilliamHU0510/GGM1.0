using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // 状态枚举（更易读，但最终会转换为bool）
    public enum PlayerState { State1, State2 }
    
    // 当前状态（true = 状态1，false = 状态2）
    public bool _isState1 = true;
    
    // 切换冷却时间
    private float _switchCooldown = 0.2f;
    private float _lastSwitchTime = -10f; // 初始化为一个很小的值，确保第一次可以切换
    
    // 公共属性供其他脚本访问
    public bool IsState1 => _isState1;
    public bool IsState2 => !_isState1;
    public PlayerState CurrentState => _isState1 ? PlayerState.State1 : PlayerState.State2;
    
    // 事件系统（可选，用于通知其他脚本状态变化）
    public delegate void StateChangedHandler(bool isState1);
    public event StateChangedHandler OnStateChanged;
    
    void Update()
    {
        // 检测Q键按下并检查冷却时间
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= _lastSwitchTime + _switchCooldown)
        {
            ToggleState();
        }
    }
    
    private void ToggleState()
    {
        // 切换状态
        _isState1 = !_isState1;
        _lastSwitchTime = Time.time;
        
        // 触发状态变化事件
        OnStateChanged?.Invoke(_isState1);
        
        // 调试输出（可选）
        Debug.Log($"切换到状态: {(_isState1 ? "状态1" : "状态2")}");
    }
    
    // 公共方法供外部调用
    public void SwitchToState1()
    {
        if (!_isState1 && Time.time >= _lastSwitchTime + _switchCooldown)
        {
            _isState1 = true;
            _lastSwitchTime = Time.time;
            OnStateChanged?.Invoke(true);
        }
    }
    
    public void SwitchToState2()
    {
        if (_isState1 && Time.time >= _lastSwitchTime + _switchCooldown)
        {
            _isState1 = false;
            _lastSwitchTime = Time.time;
            OnStateChanged?.Invoke(false);
        }
    }
    
    public bool CanSwitch()
    {
        return Time.time >= _lastSwitchTime + _switchCooldown;
    }
    
    public float GetCooldownRemaining()
    {
        float timePassed = Time.time - _lastSwitchTime;
        return Mathf.Max(0, _switchCooldown - timePassed);
    }
}
