using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
/**
  弹窗
 */
public class Window : MonoBehaviour
{
    public Text nickName; //玩家昵称
    public Text grade; //玩家排名
    
    public Button panelButton; //查看玩家详情按钮
    public Button closebutton; //退出查看详情页面按钮

    public GameObject win; //弹窗
    
    public Text nameText; //弹窗中玩家昵称
    public Text rankText; //弹窗中玩家排名
    // Start is called before the first frame update
    void Start()
    {
        win.SetActive(false);
        panelButton.onClick.AddListener(PopWin);
        closebutton.onClick.AddListener(close);
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
