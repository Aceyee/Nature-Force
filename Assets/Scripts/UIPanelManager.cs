using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPanelManager : MonoBehaviour {
    //用字典管理所有panel
    private Dictionary<string, GameObject> dic = new Dictionary<string,
        GameObject>();
    //用栈记录访问路径
    private Stack<GameObject> history = new Stack<GameObject>();
    //取得所有panel
    private GameObject[] allPanels;
    //取得所有导航按钮
    private GameObject[] allUINavigateButtons;
    //当时显示的面板
    private string ClickedButtonName;
    public GameObject currentPanel;
    private GameObject newPanel;
    public Material land;
    

    //初始化
    void Start()
    {
        allPanels = GameObject.FindGameObjectsWithTag("UIFunctionPanel");
        allUINavigateButtons = GameObject.FindGameObjectsWithTag("UINavigateButton");
        
        //将所有panel放到字典中管理
        foreach (var item in allPanels)
        {
            dic.Add(item.name.Substring(1), item);
            if (item != currentPanel)
            {
                item.SetActive(false);
            }
        }
        //将所有导航按钮注册点击事件
        foreach (var item in allUINavigateButtons)
        {
            Button btn = item.GetComponent<Button>();
            btn.onClick.AddListener(
                delegate() {
                    OnButtonClick(btn);
                }
            );
        }
    }
    //统一的切换主界面面板接口
    public void ShowPanel(string panelName)
    {
        if (panelName == "Play")
        {
            SceneManager.LoadScene("Main");
        }
        else if (panelName == "BackToStart")
        {
            SceneManager.LoadScene("Start");
        }else if(panelName == "Back")
        {
            GoBack();
        }
        else if (dic.ContainsKey(panelName))
        {
            history.Push(currentPanel);
            newPanel = dic[panelName];
        }   

        check();
    }

    //统一的切换特殊面板接口 比如强化装备界面
    public void ShowSpecialPanel(string panelName)
    {
        if (dic.ContainsKey(panelName))
        {
            history.Push(currentPanel);
            newPanel = dic[panelName];
        }
        check();
    }
    public void GoBack()
    {
        newPanel = history.Pop();
        check();
    }
    //所有按钮点击事件的处理
    public void OnButtonClick(Button btn)
    {
        string s = btn.name.Substring(3);
        ShowPanel(s);
    }

    public void check()
    {
        if (newPanel != null && currentPanel != null)
        {

            currentPanel.SetActive(false);
            currentPanel = newPanel;
            currentPanel.SetActive(true);
        }
    }
    //检测鼠标右键，如果点击右键，则返回上一层面板
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1) && history.Count > 0)
        {
            newPanel = history.Pop();
            check();
        }
    }
}
