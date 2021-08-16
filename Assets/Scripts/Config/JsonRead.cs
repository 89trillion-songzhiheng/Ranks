using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists;

/// <summary> MyMethod is a method in the MyClass class.
/// 读取json数据
/// </summary>
public class JsonRead : MonoBehaviour
{
    public JSONNode jsonNode; //获取json数据
    private string jsonData; //存储json数据
    private TextAsset jsonfile; //json文件
    private int length; //存储jsonArray长度
    public MyListItemModel[] myList; //存放json数组
    
    private void Awake()
    {
        myList = GetJson();
    }

    public MyListItemModel[] GetJson()
    {
        //初始化解析json数据
        jsonfile = (TextAsset) Resources.Load("RankList");
        jsonData=jsonfile.text;
        jsonNode = JSONNode.Parse (jsonData);
        length  = jsonNode["list"].Count;
        var jsonList = new MyListItemModel[length];
			
        for (int i = 0; i < length; ++i)
        {
            var newItems = jsonNode["list"][i];
            var model = new MyListItemModel()
            {
                uid = newItems["uid"],
                nickName = newItems["nickName"],
                avatar = newItems["avatar"],
                trophy = newItems["trophy"],
                thirdAvatar = newItems["thirdAvatar"],
                onlineStatus = newItems["onlineStatus"],
                role = newItems["role"],
                abb = newItems["abb"]
            };
            jsonList[i] = model;
        }

        //数据排序
        Array.Sort(jsonList, (a, b) =>
        {
            return b.trophy - a.trophy;
        });

        return jsonList;
    }
}
