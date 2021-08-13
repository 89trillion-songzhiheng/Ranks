using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/**
 * 场景变换
 */
public class ViewChange : MonoBehaviour
{
    public Canvas startView; //开始场景
    public Canvas gameView; //游戏场景
    
    public Button startButton; //进入按钮
    public Button backButton; //退出按钮
    
    // Start is called before the first frame update
    void Start()
    {
        startView.enabled = true;
        gameView.enabled = false;
        startButton.onClick.AddListener(startGame);
        backButton.onClick.AddListener(back);
    }

    public void startGame()
    {
        startView.enabled = false;
        gameView.enabled = true;
    }

    public void back()
    {
        startView.enabled = true;
        gameView.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
