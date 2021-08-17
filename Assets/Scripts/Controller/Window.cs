using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


/// <summary> MyMethod is a method in the MyClass class.
/// 弹窗
/// </summary>
public class Window : MonoBehaviour
{
    public Button closeButton; //退出弹窗页面按钮
    public Text grade; //玩家排名
    public Text nameText; //弹窗中玩家昵称
    public Text nickName; //玩家昵称
    public Button panelButton; //查看玩家详情按钮
    public Text rankText; //弹窗中玩家排名
    public GameObject win; //弹窗
    
    void Start()
    {
        win.SetActive(false);
        
        panelButton.onClick.AddListener(PopWin);
        closeButton.onClick.AddListener(close);
    }

    public void PopWin()
    {
        win.SetActive(true);
        
        nameText.text = nickName.text;
        rankText.text = grade.text;
    }
    
    public void close()
    {
        win.SetActive(false);
    }
}
