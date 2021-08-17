using Com.TheFallenGames.OSA.Core;
using frame8.Logic.Misc.Other.Extensions;
using UnityEngine.UI;

namespace Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists
{
    /// <summary> MyMethod is a method in the MyClass class.
    /// 获取对应组件
    /// </summary>
    public class MyListItemViewsHolder : BaseItemViewsHolder
    {
        public Image backgroundImage;
        public Text  grade; //排名(文字)
        public Image gradeImage; //排名(图片)
        public Text  nickName; //玩家姓名
        public Image rankImage; //段位图片
        public Text  trophy;  //奖杯
        
        public override void CollectViews()
        {
            base.CollectViews();
            
            //获取设置组件
            root.GetComponentAtPath("nickName", out nickName);
            root.GetComponentAtPath("BackgroundImage", out backgroundImage);
            root.GetComponentAtPath("GradeImage", out gradeImage);
            root.GetComponentAtPath("Grade", out grade);
            root.GetComponentAtPath("Trophy", out trophy);
            root.GetComponentAtPath("RankImage", out rankImage);
        }
    }
}