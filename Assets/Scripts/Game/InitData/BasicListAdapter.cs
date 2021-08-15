using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using SimpleJSON;
using UnityEngine.Serialization;
using Timer = System.Threading.Timer;


namespace Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists
{
	/// <summary> MyMethod is a method in the MyClass class.
	/// OSA获取数据
	/// </summary>
	public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
	{
		public Text  countDown; //倒计时对象
		public Text selfTrophy; //最上方本人奖杯数
		public Text selfName; //最上方本人姓名
		public JsonRead jsonRead; //读取json函数
		public TimeFormat timeFormat; //格式化时间
		public SimpleDataHelper<MyListItemModel> Data { get; private set; }
		
		private int countDownNumber; //倒计时整型
		private int day = 0; //倒计时：天
		private int hour = 0; //倒计时：时
		private int minute = 0; //倒计时：分
		private int second = 0; //倒计时：秒
		
		
		
		#region OSA implementation
		protected override void Awake()
		{
			Data = new SimpleDataHelper<MyListItemModel>(this);
			
			//获取倒计时字段数据
			countDown.text = jsonRead.jsonNode["countDown"].ToString();
			countDownNumber = int.Parse(countDown.text);
			
			timeFormat.InitTime(countDown, countDownNumber);
			day = timeFormat.day;
			hour = timeFormat.hour;
			minute = timeFormat.minute;
			second = timeFormat.second;
			
			base.Awake();
			
			//开启倒计时
			StartCoroutine(startCount());
		}
		
		/// <summary> MyMethod is a method in the MyClass class.
		/// 倒计时协程
		/// </summary>
		IEnumerator startCount()
		{
			while (second != 0 || minute != 0 || hour != 0 || day != 0)
			{
				RetrieveDataAndUpdate();
				
				yield return new WaitForSeconds(1);

				if (second == 0)
				{
					minute = minute - 1;
					second = 59;
					
					if (minute == 0)
					{
						hour = hour - 1;
						minute = 59;
						
						if (hour == 0)
						{
							day = day - 1;
							hour = 23;
						}
					}
				}
				second--;
				countDown.text = string.Concat(day, "d ", hour, "h ", minute, "m ", second, "s");
			}
		}
		
		protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new MyListItemViewsHolder();
			instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
			
			
			if (Data[itemIndex].uid.Equals("3716954261"))
			{
				selfTrophy.text = Data[itemIndex].trophy.ToString();
				selfName.text = Data[itemIndex].nickName;
			}
			return instance;
		}
		
		protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)
		{
			int rankImage = 0; //根据奖杯数设置段位图片
			
			MyListItemModel model = Data[newOrRecycled.ItemIndex]; 
			rankImage = Data[newOrRecycled.ItemIndex].trophy / 1000 + 1;
			
			//设置item信息
			newOrRecycled.nickName.text = model.nickName;
			newOrRecycled.Trophy.text = model.trophy.ToString();
			newOrRecycled.GradeImage.sprite =Resources.Load<Sprite>("picture/rank_"+(newOrRecycled.ItemIndex+1));
			newOrRecycled.Grade.text = (newOrRecycled.ItemIndex + 1).ToString();
			newOrRecycled.Rank.sprite = Resources.Load<Sprite>("picture/rank/arenaBadge_"+rankImage);
			
			//前三名使用奖牌展示排名，后续排名使用数字
			if (newOrRecycled.ItemIndex >2)
			{
				newOrRecycled.GradeImage.color = new Color(1, 1, 1, 0);
			}
			else
			{
				newOrRecycled.GradeImage.sprite =Resources.Load<Sprite>("picture/rank_"+(newOrRecycled.ItemIndex+1));
				newOrRecycled.GradeImage.color = new Color(1, 1, 1, 1);
			}
		}
		
		#endregion

		void RetrieveDataAndUpdate()
		{
			StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate());
		}

		//获取json数据
		IEnumerator FetchMoreItemsFromDataSourceAndUpdate()
		{
			yield return new WaitForSeconds(.5f);
			
			OnDataRetrieved(jsonRead.myList);
		}

		void OnDataRetrieved(MyListItemModel[] newItems)
		{
			//每次刷新时，先将之前的数据清空，否则会造成数据迭代
			Data.InsertItemsAtEnd(newItems);
			Data.ResetItems(newItems);
		}
		
		#region data manipulation
		public void AddItemsAt(int index, IList<MyListItemModel> items)
		{
			
			Data.InsertItems(index, items);
		}

		public void RemoveItemsFrom(int index, int count)
		{
			

			Data.RemoveItems(index, count);
		}

		public void SetItems(IList<MyListItemModel> items)
		{
			
			Data.ResetItems(items);
		}
		#endregion
		
	}
}
