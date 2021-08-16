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
        public Text  Grade; //排名(文字)
        public Image GradeImage; //排名(图片)
        public Text  nickName; //玩家姓名
        public Image Rank; //段位图片
        public Text  Trophy;  //奖杯
        
        public override void CollectViews()
        {
            base.CollectViews();
            
            //获取设置组件
            root.GetComponentAtPath("nickName", out nickName);
            root.GetComponentAtPath("BackgroundImage", out backgroundImage);
            root.GetComponentAtPath("GradeImage", out GradeImage);
            root.GetComponentAtPath("Grade", out Grade);
            root.GetComponentAtPath("Trophy", out Trophy);
            root.GetComponentAtPath("Rank", out Rank);
        }
    }
}