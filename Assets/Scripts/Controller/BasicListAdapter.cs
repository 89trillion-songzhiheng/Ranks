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
		public Text countDown; //倒计时对象
		public JsonRead jsonRead; //读取json函数
		public Text selfTrophy; //最上方本人奖杯数
		public Text selfName; //最上方本人姓名
		public SimpleDataHelper<MyListItemModel> Data { get; private set; }
		
		private int countDownNumber = 0; //倒计时整型
		private int day = 0; //倒计时：天
		private int hour = 0; //倒计时：时
		private float imageSizeScale = 0.2f; //设置段位图片自定义缩放大小
		private int minute = 0; //倒计时：分
		private int second = 0; //倒计时：秒
		
		#region OSA implementation
		protected override void Awake()
		{
			Data = new SimpleDataHelper<MyListItemModel>(this);
			
			//获取倒计时字段数据
			countDownNumber = jsonRead.jsonNode["countDown"];
			
			//开启倒计时
			StartCoroutine(startCount());
			
			base.Awake();

			
		}
		
		/// <summary> MyMethod is a method in the MyClass class.
		/// 倒计时协程
		/// </summary>
		IEnumerator startCount()
		{
			//存放时间字符串
		    StringBuilder stringBuilder = new StringBuilder(); 
			
			while (countDownNumber != 0)
			{
				yield return new WaitForSeconds(1);
				RetrieveDataAndUpdate();
				
				day = countDownNumber / (60 * 60 * 24);
				hour = countDownNumber / (60 * 60) - day * 24;
				minute = countDownNumber / 60 - hour * 60 - day * 24 * 60;
				second = countDownNumber - minute * 60 - hour * 60 * 60 - day * 24 * 60 * 60;
				
				stringBuilder.Append(day).Append("d")
					.Append(hour).Append("h")
					.Append(minute).Append("m")
					.Append(second).Append("s");
				countDown.text = stringBuilder.ToString();
				stringBuilder.Clear();
				
				countDownNumber--;
			}
		}
		
		protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new MyListItemViewsHolder();
			instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
			
			//获取该ID信息，并放置头榜位置
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
			newOrRecycled.trophy.text = model.trophy.ToString();
			newOrRecycled.gradeImage.sprite =Resources.Load<Sprite>("picture/rank_"+(newOrRecycled.ItemIndex+1));
			newOrRecycled.grade.text = (newOrRecycled.ItemIndex + 1).ToString();
			newOrRecycled.rankImage.sprite = Resources.Load<Sprite>("picture/rank/arenaBadge_"+rankImage);
			newOrRecycled.rankImage.rectTransform.sizeDelta = new Vector2(newOrRecycled.rankImage.sprite.rect.width * imageSizeScale,
				newOrRecycled.rankImage.sprite.rect.height * imageSizeScale);
			newOrRecycled.backgroundImage.sprite = Resources.Load<Sprite>("picture/rank list_" +(newOrRecycled.ItemIndex+1));
			
			//前三名使用奖牌展示排名，后续排名使用数字,并设置背景色的通透度
			if (newOrRecycled.ItemIndex >2)
			{
				newOrRecycled.gradeImage.color = new Color(1, 1, 1, 0);
				newOrRecycled.backgroundImage.color = new Color(1, 1, 1, 0.4f);
			}
			else
			{
				newOrRecycled.gradeImage.color = new Color(1, 1, 1, 1);
				newOrRecycled.backgroundImage.color = new Color(1, 1, 1, 1);
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
