using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
	{
		public TextAsset jsonfile; //json文件
		public Text selfTrophy; //最上方本人奖杯数
		public Text selfName; //最上方本人姓名
		public Text  countDown; //倒计时对象
        
		private int countDownNumber; //倒计时整型
		private int length; //存储jsonArray长度
		private  string jsonData; //存储json数据
		private int day = 0; //倒计时：天
		private int hour = 0; //倒计时：时
		private int minute = 0; //倒计时：分
		private int second = 0; //倒计时：秒
		
		public SimpleDataHelper<MyListItemModel> Data { get; private set; }
		
		#region OSA implementation
		protected override void Awake()
		{
			jsonData=jsonfile.text;
			Data = new SimpleDataHelper<MyListItemModel>(this);
			var n = JSONNode.Parse (jsonData);
			length  = n["list"].Count;
			countDown.text = n["countDown"].ToString();
			countDownNumber = int.Parse(countDown.text);
			
			InitTime();
			
			base.Awake();
			
			//开启倒计时
			StartCoroutine(startCount());
			
		}
		
        //倒计时协程
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
				countDown.text = day + ("d ") + hour + ("h ") + minute + ("m ") + second + ("s");
				
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
			
			if (newOrRecycled.ItemIndex <= 2)
			{
				newOrRecycled.GradeImage.sprite =Resources.Load<Sprite>("picture/rank_"+(newOrRecycled.ItemIndex+1));
				newOrRecycled.GradeImage.color = new Color(1, 1, 1, 1);
				
				//设置图片宽高
				if (newOrRecycled.ItemIndex == 0)
				{
					newOrRecycled.GradeImage.rectTransform.sizeDelta = new Vector2(86, 78);
				}
				
				if (newOrRecycled.ItemIndex == 1)
				{
					newOrRecycled.GradeImage.rectTransform.sizeDelta = new Vector2(83, 77);
				}
				
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
			
			var jsonNode = JSONNode.Parse (jsonData);
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
			for(int i=0;i<length-1;i++)
			{
				for(int j=length-1;j>=i+1;j--) 
				{
					if(jsonList[j-1].trophy<jsonList[j].trophy) {
						MyListItemModel tem=jsonList[j-1];
						jsonList[j-1]=jsonList[j];
						jsonList[j]= tem;
					}
				}
			}
			OnDataRetrieved(jsonList);
		}

		void OnDataRetrieved(MyListItemModel[] newItems)
		{
			//每次刷新时，先将之前的数据清空，否则会造成数据迭代
			Data.InsertItemsAtEnd(newItems);
			Data.ResetItems(newItems);
		}

		//根据获取的json数据，将时间格式化
		public void InitTime()
		{
			if (countDownNumber >= 86400) //天,
			{
				day = Convert.ToInt16(countDownNumber / 86400);
				hour = Convert.ToInt16((countDownNumber % 86400) / 3600);
				minute = Convert.ToInt16((countDownNumber % 86400 % 3600) / 60);
				second = Convert.ToInt16(countDownNumber % 86400 % 3600 % 60);
				
				countDown.text = day + ("d ") + hour + ("h ") + minute + ("m ") + second + ("s");
			}
			else if (countDownNumber >= 3600)//时,
			{
				hour = Convert.ToInt16(countDownNumber / 3600);
				minute = Convert.ToInt16((countDownNumber % 3600) / 60);
				second = Convert.ToInt16(countDownNumber % 3600 % 60);
				
				countDown.text = hour + ("h ") + minute + ("m ") + second + ("s");
			}
			else if (countDownNumber >= 60)//分
			{
				minute = Convert.ToInt16(countDownNumber / 60);
				second = Convert.ToInt16(countDownNumber % 60);
				countDown.text = minute + ("m ") + second + ("s");
			}
			else
			{
				second = Convert.ToInt16(countDownNumber);
				countDown.text = second + ("s");
			}
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
