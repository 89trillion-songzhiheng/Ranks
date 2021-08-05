using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 场景变换
 */
public class change : MonoBehaviour
{
    //开始场景
    public Canvas st;
    //游戏场景
    public Canvas gam;
    //进入按钮
    public Button but;
    //退出按钮
    public Button butt;
    // Start is called before the first frame update
    void Start()
    {
        st.enabled = true;
        gam.enabled = false;
        but.onClick.AddListener(go);
        butt.onClick.AddListener(back);
    }

    public void go()
    {
        st.enabled = false;
        gam.enabled = true;
    }

    public void back()
    {
        st.enabled = true;
        gam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
