using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TagBasedActivator : MonoBehaviour
{
    [Header("玩家状态管理器")]
    [SerializeField] private PlayerStateManager playerStateManager;
    
    // 存储找到的物体
    private List<GameObject> blueWObjects = new List<GameObject>();
    private List<GameObject> redWObjects = new List<GameObject>();
    
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4; 

    void Start()
    {
        Initialize();
         //ChangeWeapon(1);
    }
    


void ChangeWeapon(int number)
    {
        switch (number)
        {
            case 1:
                weapon1.SetActive(true);
                weapon2.SetActive(false);
                weapon3.SetActive(false);
                weapon4.SetActive(false);
                break;
            case 2:
                weapon1.SetActive(false);
                weapon2.SetActive(true);
                weapon3.SetActive(false);
                weapon4.SetActive(false);
                break;
            case 3:
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                weapon3.SetActive(true);
                weapon4.SetActive(false);
                break;
            case 4:
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                weapon3.SetActive(false);
                weapon4.SetActive(true);
                break;
        }
    }

    void Initialize()
    {
        // 尝试自动查找玩家状态管理器
        if (playerStateManager == null)
        {
            playerStateManager = FindObjectOfType<PlayerStateManager>();
            
            if (playerStateManager == null)
            {
                Debug.LogError("TagBasedActivator: 未找到PlayerStateManager！请确保场景中有玩家对象并附加了PlayerStateManager脚本。");
                enabled = false;
                return;
            }
        }
        
        // 查找所有标签为BlueW和RedW的物体
        FindAllTaggedObjects();
        
        // 订阅状态变化事件
        playerStateManager.OnStateChanged += HandleStateChange;
        
        // 初始化状态
        HandleStateChange(playerStateManager.IsState1);
        
        Debug.Log($"TagBasedActivator初始化完成 - 找到 {blueWObjects.Count} 个BlueW物体和 {redWObjects.Count} 个RedW物体");
    }
    
    void FindAllTaggedObjects()
    {
        // 查找所有BlueW标签的物体
        GameObject[] blueWArray = GameObject.FindGameObjectsWithTag("BlueW");
        blueWObjects.Clear();
        blueWObjects.AddRange(blueWArray);
        
        // 查找所有RedW标签的物体
        GameObject[] redWArray = GameObject.FindGameObjectsWithTag("RedW");
        redWObjects.Clear();
        redWObjects.AddRange(redWArray);
    }
    
    private void HandleStateChange(bool isState1)
    {
        // 根据状态激活/禁用物体
        if (isState1)
        {
            // 状态1: 激活BlueW物体，禁用RedW物体
            SetObjectsActive(blueWObjects, true);
            SetObjectsActive(redWObjects, false);
           // ChangeWeapon(1);
        }
        else
        {
            // 状态2: 激活RedW物体，禁用BlueW物体
            SetObjectsActive(blueWObjects, false);
            SetObjectsActive(redWObjects, true);
            //ChangeWeapon(2);
        }
    }
    
    // 批量设置物体激活状态
    private void SetObjectsActive(List<GameObject> objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(active);
            }
        }
    }
    
    // 更新：如果需要动态刷新物体列表（例如场景中物体有变化）
    public void RefreshTaggedObjects()
    {
        FindAllTaggedObjects();
        
        // 重新应用当前状态
        if (playerStateManager != null)
        {
            HandleStateChange(playerStateManager.IsState1);
        }
        
        Debug.Log($"已刷新物体列表 - 当前有 {blueWObjects.Count} 个BlueW物体和 {redWObjects.Count} 个RedW物体");
    }
    
    void OnDestroy()
    {
        // 取消订阅事件
        if (playerStateManager != null)
        {
            playerStateManager.OnStateChanged -= HandleStateChange;
        }
    }
}
