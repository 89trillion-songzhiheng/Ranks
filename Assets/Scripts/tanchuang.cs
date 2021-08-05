using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * 弹窗
 */
public class tanchuang : MonoBehaviour
{
    //列表
    public GameObject item;
    
    public Button but;

    public Button butt;
    //弹窗
    public GameObject win;
    // Start is called before the first frame update
    void Start()
    {
        win.SetActive(false);
        but.onClick.AddListener(tan);
        butt.onClick.AddListener(bi);
    }

    public void tan()
    {
        win.SetActive(true);
        win.transform.Find("pname").GetComponent<Text>().text = item.transform.Find("nickName").GetComponent<Text>().text;
        win.transform.Find("ran").GetComponent<Text>().text = item.transform.Find("wenpai").GetComponent<Text>().text;
    }
    public void bi()
    {
        win.SetActive(false);
        // win.transform.Find("pname").GetComponent<Text>().text = item.transform.Find("wenpai").GetComponent<Text>().text;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
