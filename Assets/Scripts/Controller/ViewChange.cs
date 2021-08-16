using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary> MyMethod is a method in the MyClass class.
/// 场景变换
/// </summary>
public class ViewChange : MonoBehaviour
{
    public Button backButton; //退出按钮
    public Button startButton; //进入按钮
    public GameObject startView; //开始场景
    
    void Start()
    {
        startView.SetActive(true);
        
        startButton.onClick.AddListener(startGame);
        backButton.onClick.AddListener(back);
    }

    public void startGame()
    {
        startView.SetActive(false);
    }

    public void back()
    {
        startView.SetActive(true);
    }
}
