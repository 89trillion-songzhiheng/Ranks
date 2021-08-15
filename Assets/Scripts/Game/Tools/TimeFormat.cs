using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> MyMethod is a method in the MyClass class.
/// 将倒计时数据格式化
/// </summary>
public class TimeFormat : MonoBehaviour
{
    public int day = 0; //倒计时：天
    public int hour = 0; //倒计时：时
    public int minute = 0; //倒计时：分
    public int second = 0; //倒计时：秒
    
    public void InitTime(Text countDown, int countDownNumber)
    {
        if (countDownNumber >= 86400) //天,
        {
            day = Convert.ToInt16(countDownNumber / 86400);
            hour = Convert.ToInt16((countDownNumber % 86400) / 3600);
            minute = Convert.ToInt16((countDownNumber % 86400 % 3600) / 60);
            second = Convert.ToInt16(countDownNumber % 86400 % 3600 % 60);
				
            countDown.text = string.Concat(day, "d ", hour, "h ", minute, "m ", second, "s");;
        }
        else if (countDownNumber >= 3600)//时,
        {
            hour = Convert.ToInt16(countDownNumber / 3600);
            minute = Convert.ToInt16((countDownNumber % 3600) / 60);
            second = Convert.ToInt16(countDownNumber % 3600 % 60);
				
            countDown.text = string.Concat(hour, "h ", minute, "m ", second, "s");
        }
        else if (countDownNumber >= 60)//分
        {
            minute = Convert.ToInt16(countDownNumber / 60);
            second = Convert.ToInt16(countDownNumber % 60);
            countDown.text = string.Concat(minute, "m ", second, "s");
        }
        else
        {
            second = Convert.ToInt16(countDownNumber);
            countDown.text = string.Concat(second, "s");
        }
    }
}
