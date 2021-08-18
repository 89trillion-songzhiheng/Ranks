# Ranks

**整体大纲**
1.解析json文件，获取解析的数据
2.展示数据列表，并将数据进行降序排列
3.使用osa插件完成元素服用
4.设置图片适配 

 **目录结构**   
├── Config 
│   ├── JsonRead.cs //json数据读取类
│   └── JsonRead.cs.meta
├── Config.meta
├── Controller
│   ├── BasicListAdapter.cs //osa读取展示item
│   ├── BasicListAdapter.cs.meta
│   ├── ViewChange.cs  //场景转换控制类
│   ├── ViewChange.cs.meta
│   ├── Window.cs //弹窗控制类
│   └── Window.cs.meta
├── Controller.meta
├── Data  
│   ├── MyListItemModel.cs  //设置对应json的字段 
│   ├── MyListItemModel.cs.meta
│   ├── MyListItemViewsHolder.cs ////设置对应的UI组件
│   └── MyListItemViewsHolder.cs.meta
└── Data.meta

**界面结构**
 Hierarchy：
    1.GameView  //展示排行榜的画布
      1)OSA     //展示排行榜
      2)ButtonPanel  //放置后退按钮
      3)Popups   //弹窗
      4)Countdown  //倒计时
      5)CountdownText //倒计时标题
      6)TrophyBackImage //奖杯背景图
      7)TrophyImage  //奖杯图片
      8)TopRank  //头榜
      9)StartView //初始页面的画布
     
         
**流程图**

![image](https://github.com/89trillion-songzhiheng/Ranks/blob/main/Assets/Picture/RankPic.png)
