using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Prime31;
using Server;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class PsPurchaseHelper
{
	// Token: 0x060017D4 RID: 6100 RVA: 0x0010132C File Offset: 0x000FF72C
	private static void CreateIAPWaitingPopup()
	{
		PsPurchaseHelper.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterShopPurchaseWait), null, null, null, true, true, InitialPage.Center, false, false, false);
		PsPurchaseHelper.StoreShopUpdateAction();
		TweenS.AddTransformTween(PsPurchaseHelper.m_waitingPopup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x00101397 File Offset: 0x000FF797
	private static void DestroyIAPWaitingPopup()
	{
		if (PsPurchaseHelper.m_waitingPopup != null)
		{
			PsPurchaseHelper.m_waitingPopup.Destroy();
			PsPurchaseHelper.m_waitingPopup = null;
		}
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x001013B3 File Offset: 0x000FF7B3
	public static void StoreShopUpdateAction()
	{
		if (PsMetagameManager.d_shopUpdateAction != null)
		{
			PsPurchaseHelper.m_tempShopUpdateAction = PsMetagameManager.d_shopUpdateAction;
			PsMetagameManager.d_shopUpdateAction = null;
		}
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x001013CF File Offset: 0x000FF7CF
	public static void UnstoreShopUpdateAction()
	{
		if (PsPurchaseHelper.m_tempShopUpdateAction != null)
		{
			PsMetagameManager.d_shopUpdateAction = PsPurchaseHelper.m_tempShopUpdateAction;
			PsPurchaseHelper.m_tempShopUpdateAction = null;
			PsMetagameManager.d_shopUpdateAction.Invoke();
		}
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x001013F8 File Offset: 0x000FF7F8
	public static void PurchaseUpgrade(string _identifier, Action _purchased)
	{
		ShopUpgradeItemData item = PsMetagameManager.GetShopUpgradeItem(_identifier);
		if (item == null)
		{
			return;
		}
		int priceForUpgrade = PsUpgradeManager.GetPriceForUpgrade(item);
		if (PsMetagameManager.m_playerStats.coins >= priceForUpgrade)
		{
			PsMetagameManager.m_playerStats.CumulateCoins(-priceForUpgrade);
			PsPurchaseHelper.UpgradeItemPurchased(item);
			if (_purchased != null)
			{
				_purchased.Invoke();
			}
		}
		else
		{
			PsPurchaseHelper.StoreShopUpdateAction();
			PsMetagameManager.m_coinsToDiamondsConvertAmount = priceForUpgrade - PsMetagameManager.m_playerStats.coins;
			new PsPurchaseHelper.ConvertDiamondsToCoinsFlow(delegate
			{
				PsPurchaseHelper.UnstoreShopUpdateAction();
				if (PsMetagameManager.m_playerStats.diamonds >= PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true))
				{
					int num = PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true);
					int coinsToDiamondsConvertAmount = PsMetagameManager.m_coinsToDiamondsConvertAmount;
					PsMetagameManager.m_playerStats.CumulateCoins(-PsMetagameManager.m_playerStats.coins);
					PsMetagameManager.m_playerStats.CumulateDiamonds(-PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true));
					PsMetagameManager.m_coinsToDiamondsConvertAmount = 0;
					PsPurchaseHelper.UpgradeItemPurchased(item);
					if (_purchased != null)
					{
						_purchased.Invoke();
					}
					FrbMetrics.SpendVirtualCurrency("coin_substitute", "gems", (double)num);
				}
				else
				{
					PsPurchaseHelper.GetDiamonds();
				}
			}, new Action(PsPurchaseHelper.UnstoreShopUpdateAction));
		}
	}

	// Token: 0x060017D9 RID: 6105 RVA: 0x001014C0 File Offset: 0x000FF8C0
	private static void UpgradeItemPurchased(ShopUpgradeItemData _item)
	{
		string text = _item.m_identifier.ToLower();
		int purchaseCount = _item.m_purchaseCount;
		int priceForUpgrade = PsUpgradeManager.GetPriceForUpgrade(_item);
		PsUpgradeManager.IncreaseResources(_item.m_identifier, 1);
		_item.m_purchaseCount++;
		PsPurchaseHelper.UpgradePurchasedData data = new PsPurchaseHelper.UpgradePurchasedData();
		data.pathJson = null;
		data.setData = new Hashtable();
		data.customisations = PsCustomisationManager.GetUpdatedData(PsUpgradeManager.GetUpdatedData(null));
		data.achievements = PsAchievementManager.GetUpdatedAchievements(true);
		data.sendMetrics = false;
		data.chests = PsGachaManager.GetUpdatedData();
		data.editorResources = PsMetagameManager.m_playerStats.GetUpdatedEditorResources();
		data.price = priceForUpgrade;
		data.identifier = text;
		data.purchaseCount = purchaseCount;
		bool suspicious = false;
		if (PsMetagameManager.m_suspiciousActivity)
		{
			suspicious = true;
			PsMetagameManager.m_suspiciousActivity = false;
		}
		new PsServerQueueFlow(null, delegate
		{
			HttpC httpC = Player.SetData(data.setData, data.pathJson, data.customisations, data.achievements, data.chests, data.editorResources, new Action<HttpC>(PsPurchaseHelper.PurchaseSUCCEED), new Action<HttpC>(PsPurchaseHelper.PurchaseFAILED), null, suspicious);
			httpC.objectData = data;
		}, new string[] { "SetData" });
		PsMetagameManager.m_playerStats.SetDirty(false);
		SoundS.PlaySingleShot("/UI/Purchase", Vector2.zero, 1f);
	}

	// Token: 0x060017DA RID: 6106 RVA: 0x00101618 File Offset: 0x000FFA18
	public static void PurchaseChest(string _identifier, Action _purchasedCallback = null)
	{
		GachaType gachaType = (GachaType)Enum.Parse(typeof(GachaType), _identifier);
		int shopPrice = PsGachaManager.GetShopPrice(gachaType);
		if (PsMetagameManager.m_playerStats.diamonds >= shopPrice)
		{
			BossBattles.AlterBothVehicleHandicaps(BossBattles.GachaTypeHandicap(gachaType));
			PsMetagameManager.m_playerStats.CumulateDiamonds(-shopPrice);
			PsGachaManager.m_lastOpenedGacha = gachaType;
			PsGachaManager.m_lastGachaRewards = PsGachaManager.OpenGacha(new PsGacha(gachaType), -1, true);
			PsMetrics.ChestOpened("Shop");
			FrbMetrics.ChestOpened("shop");
			FrbMetrics.SpendVirtualCurrency("shop_chest_" + gachaType.ToString().ToLower(), "gems", (double)shopPrice);
			PsPurchaseHelper.StoreShopUpdateAction();
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterOpenGacha), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
				PsPurchaseHelper.UnstoreShopUpdateAction();
				if (_purchasedCallback != null)
				{
					_purchasedCallback.Invoke();
				}
			});
		}
		else
		{
			PsPurchaseHelper.GetDiamonds();
		}
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x00101720 File Offset: 0x000FFB20
	public static void PurchaseIAP(string _identifier, Action _purchasedCallback = null, Action _closeCompletedPopupCallback = null)
	{
		if (PsIAPManager.CanMakePayments())
		{
			PsPurchaseHelper.CreateIAPWaitingPopup();
			PsMetagameManager.PurchaseConsumable(_identifier, delegate(string s)
			{
				PsPurchaseHelper.IAPPurchaseSUCCEED(s, _identifier, _closeCompletedPopupCallback);
				if (_purchasedCallback != null)
				{
					_purchasedCallback.Invoke();
				}
			}, new Action<string, string, bool>(PsPurchaseHelper.IAPPurchaseERROR));
		}
		else
		{
			TouchAreaS.Enable();
		}
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x00101798 File Offset: 0x000FFB98
	public static void PurchaseCoins(int _coinAmount)
	{
		int num = PsMetagameManager.CoinsToDiamonds(_coinAmount, false);
		if (PsMetagameManager.m_playerStats.diamonds >= num)
		{
			PsMetagameManager.m_playerStats.CumulateDiamonds(-num);
			PsMetagameManager.m_playerStats.CumulateCoins(_coinAmount);
			PsPurchaseHelper.CoinPurchasedData data = new PsPurchaseHelper.CoinPurchasedData();
			data.setData = new Hashtable();
			data.price = num;
			data.amount = _coinAmount;
			bool suspicious = false;
			if (PsMetagameManager.m_suspiciousActivity)
			{
				suspicious = true;
				PsMetagameManager.m_suspiciousActivity = false;
			}
			new PsServerQueueFlow(null, delegate
			{
				HttpC httpC = Player.SetData(data.setData, data.pathJson, data.customisations, data.achievements, data.chests, data.editorResources, new Action<HttpC>(PsPurchaseHelper.PurchaseSUCCEED), new Action<HttpC>(PsPurchaseHelper.PurchaseFAILED), null, suspicious);
				httpC.objectData = data;
			}, new string[] { "SetData" });
			PsMetagameManager.m_playerStats.SetDirty(false);
			SoundS.PlaySingleShot("/UI/Purchase", Vector2.zero, 1f);
		}
		else
		{
			PsPurchaseHelper.GetDiamonds();
		}
	}

	// Token: 0x060017DD RID: 6109 RVA: 0x00101884 File Offset: 0x000FFC84
	private static void GetDiamonds()
	{
		if (Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() is PsUIBaseState)
		{
			PsUIBaseState psUIBaseState = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() as PsUIBaseState;
			if (psUIBaseState.m_baseCanvas.m_mainContent is PsUICenterShopAll)
			{
				PsPurchaseHelper.StoreShopUpdateAction();
				PsUICenterShopAll psUICenterShopAll = psUIBaseState.m_baseCanvas.m_mainContent as PsUICenterShopAll;
				new PsGetDiamondsFlow(new Action(PsPurchaseHelper.UnstoreShopUpdateAction), new Action(PsPurchaseHelper.UnstoreShopUpdateAction), new Action(psUICenterShopAll.GetMoreGems));
				return;
			}
		}
		for (int i = 0; i < PsState.m_openPopups.Count; i++)
		{
			if (PsState.m_openPopups[i] is PsUIBasePopup)
			{
				PsUIBasePopup psUIBasePopup = PsState.m_openPopups[i] as PsUIBasePopup;
				if (psUIBasePopup.m_mainContent is PsUICenterShopAll)
				{
					PsPurchaseHelper.StoreShopUpdateAction();
					PsUICenterShopAll psUICenterShopAll2 = psUIBasePopup.m_mainContent as PsUICenterShopAll;
					new PsGetDiamondsFlow(new Action(PsPurchaseHelper.UnstoreShopUpdateAction), new Action(PsPurchaseHelper.UnstoreShopUpdateAction), new Action(psUICenterShopAll2.GetMoreGems));
					return;
				}
			}
		}
		new PsGetDiamondsFlow(null, null, null);
	}

	// Token: 0x060017DE RID: 6110 RVA: 0x001019FC File Offset: 0x000FFDFC
	public static void IAPPurchaseSUCCEED(string _message, string _identifier, Action _closePopupCallback = null)
	{
		Debug.Log("IAPPurchaseSUCCEED " + _message + ", identifier: " + _identifier, null);
		PsPurchaseHelper.DestroyIAPWaitingPopup();
		PsPurchaseHelper.StoreShopUpdateAction();
		PsPurchaseHelper.m_completedPopup = new PsUIBasePopup(typeof(PsUIIapCompletedPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
		string text = "Completed!";
		string text2 = "menu_icon_shop";
		IAPProduct iapproductById = PsIAPManager.GetIAPProductById(_identifier);
		if (iapproductById != null)
		{
			text = iapproductById.title;
		}
		(PsPurchaseHelper.m_completedPopup.m_mainContent as PsUIIapCompletedPopup).CreateContent(text, text2);
		PsPurchaseHelper.m_completedPopup.SetAction("Exit", delegate
		{
			if (PsPurchaseHelper.m_completedPopup != null)
			{
				PsPurchaseHelper.m_completedPopup.Destroy();
				PsPurchaseHelper.m_completedPopup = null;
			}
			PsPurchaseHelper.UnstoreShopUpdateAction();
			if (_closePopupCallback != null)
			{
				_closePopupCallback.Invoke();
			}
		});
		if (PsState.m_inIapFlow)
		{
			Debug.Log("IAP PURCHASE SUCCESFUL", null);
			PsState.m_inIapFlow = false;
			TouchAreaS.Enable();
			SoundS.PlaySingleShot("/UI/Purchase", Vector2.zero, 1f);
		}
		PsMetagameManager.IAPDebug("IAPDebug_CompletedSucceed", new KeyValuePair<string, object>[0]);
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x00101AF0 File Offset: 0x000FFEF0
	public static void IAPPurchaseERROR(string _error, string _id, bool _result)
	{
		Debug.Log(string.Concat(new object[] { "IAPPurchaseERROR ", _error, " ", _id, " ", _result }), null);
		PsPurchaseHelper.DestroyIAPWaitingPopup();
		if (PsState.m_inIapFlow)
		{
			Debug.Log("IAP purchase failed: " + _error, null);
			TouchAreaS.Enable();
			if (string.IsNullOrEmpty(_id))
			{
				if (_error == "User canceled")
				{
					PsState.m_inIapFlow = false;
					TouchAreaS.Enable();
				}
				else
				{
					ServerManager.m_dontShowLoginPopup = true;
					ServerManager.ThrowServerErrorException("Error", _error, null, delegate
					{
						if (PsPurchaseHelper.m_completedPopup == null)
						{
							PsPurchaseHelper.UnstoreShopUpdateAction();
						}
						PsState.m_inIapFlow = false;
						TouchAreaS.Enable();
						ServerManager.m_dontShowLoginPopup = false;
					}, StringID.TRY_AGAIN_SERVER);
				}
			}
			else
			{
				PsState.m_inIapFlow = false;
				ServerManager.m_dontShowLoginPopup = true;
				ServerManager.ThrowServerErrorException("Error", _error, null, delegate
				{
					ServerManager.m_dontShowLoginPopup = false;
					if (PsPurchaseHelper.m_completedPopup == null)
					{
						PsPurchaseHelper.UnstoreShopUpdateAction();
					}
				}, StringID.TRY_AGAIN_SERVER);
			}
		}
		PsMetagameManager.IAPDebug("IAPDebug_CompletedError", new KeyValuePair<string, object>[0]);
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x00101C0C File Offset: 0x0010000C
	public static void PurchaseSUCCEED(HttpC _httpC)
	{
		Debug.Log("PURCHASE SUCCEED", null);
		if (_httpC.objectData is PsPurchaseHelper.CoinPurchasedData)
		{
			PsPurchaseHelper.CoinPurchasedData coinPurchasedData = _httpC.objectData as PsPurchaseHelper.CoinPurchasedData;
			int num = coinPurchasedData.amount;
			FrbMetrics.SpendVirtualCurrency("coin_pack_" + coinPurchasedData.amount, "gems", (double)coinPurchasedData.price);
			FrbMetrics.ReceiveVirtualCurrency("coins", (double)num, "coin_pack_" + coinPurchasedData.amount);
		}
		else if (_httpC.objectData is PsPurchaseHelper.ChestPurchasedData)
		{
			PsPurchaseHelper.ChestPurchasedData chestPurchasedData = _httpC.objectData as PsPurchaseHelper.ChestPurchasedData;
			PsMetrics.ChestOpened("Shop");
			FrbMetrics.ChestOpened("shop");
		}
		else if (_httpC.objectData is PsPurchaseHelper.UpgradePurchasedData)
		{
			PsPurchaseHelper.UpgradePurchasedData upgradePurchasedData = _httpC.objectData as PsPurchaseHelper.UpgradePurchasedData;
			FrbMetrics.SpendVirtualCurrency("upgrade_" + upgradePurchasedData.identifier.ToLower(), "coins", (double)upgradePurchasedData.price);
		}
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x00101D1C File Offset: 0x0010011C
	public static void PurchaseFAILED(HttpC _httpC)
	{
		Debug.Log("PURCHASE FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_httpC.www.error);
		Player.ProgressionAndSetDataObject data = _httpC.objectData as Player.ProgressionAndSetDataObject;
		if (data == null)
		{
			data = new Player.ProgressionAndSetDataObject();
			data.setData = new Hashtable();
		}
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, delegate
		{
			bool flag = false;
			if (PsMetagameManager.m_suspiciousActivity)
			{
				flag = true;
				PsMetagameManager.m_suspiciousActivity = false;
			}
			HttpC httpC = Player.SetData(data.setData, data.pathJson, data.customisations, data.achievements, data.chests, data.editorResources, new Action<HttpC>(PsPurchaseHelper.PurchaseSUCCEED), new Action<HttpC>(PsPurchaseHelper.PurchaseFAILED), null, flag);
			httpC.objectData = data;
			return httpC;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x04001AA3 RID: 6819
	private static PsUIBasePopup m_waitingPopup;

	// Token: 0x04001AA4 RID: 6820
	private static PsUIBasePopup m_completedPopup;

	// Token: 0x04001AA5 RID: 6821
	private static Action m_tempShopUpdateAction;

	// Token: 0x0200032E RID: 814
	private class ConvertDiamondsToCoinsFlow : Flow
	{
		// Token: 0x060017E4 RID: 6116 RVA: 0x00101DE0 File Offset: 0x001001E0
		public ConvertDiamondsToCoinsFlow(Action _proceed, Action _cancel)
			: base(_proceed, _cancel, null)
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUICenterNotEnoughCoinsConversion), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_popup.SetAction("Exit", new Action(this.Exit));
			this.m_popup.SetAction("Upgrade", new Action(this.Buy));
			(this.m_popup.m_mainContent as PsUICenterNotEnoughCoinsConversion).m_customShopAction = new Action(this.Buy);
			if (PsMetagameManager.m_menuResourceView != null)
			{
				this.m_lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
			}
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			PsMetagameManager.ShowResources(null, true, false, true, false, 0.03f, false, false, false);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00101ED6 File Offset: 0x001002D6
		private void DestroyContent()
		{
			this.m_popup.Destroy();
			this.m_popup = null;
			if (this.m_lastResourceView != null)
			{
				PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastResourceView);
			}
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00101F05 File Offset: 0x00100305
		private void Buy()
		{
			this.DestroyContent();
			this.Proceed.Invoke();
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00101F18 File Offset: 0x00100318
		private void Exit()
		{
			this.DestroyContent();
			if (this.Cancel != null)
			{
				this.Cancel.Invoke();
			}
		}

		// Token: 0x04001AB4 RID: 6836
		private PsUIBasePopup m_popup;

		// Token: 0x04001AB5 RID: 6837
		private LastResourceView m_lastResourceView;
	}

	// Token: 0x0200032F RID: 815
	public class ChestPurchasedData : Player.ProgressionAndSetDataObject
	{
		// Token: 0x04001AB6 RID: 6838
		public ObscuredInt price;

		// Token: 0x04001AB7 RID: 6839
		public GachaType gachaType;
	}

	// Token: 0x02000330 RID: 816
	public class UpgradePurchasedData : Player.ProgressionAndSetDataObject
	{
		// Token: 0x04001AB8 RID: 6840
		public ObscuredInt price;

		// Token: 0x04001AB9 RID: 6841
		public string identifier;

		// Token: 0x04001ABA RID: 6842
		public ObscuredInt purchaseCount;
	}

	// Token: 0x02000331 RID: 817
	public class CoinPurchasedData : Player.ProgressionAndSetDataObject
	{
		// Token: 0x04001ABB RID: 6843
		public ObscuredInt price;

		// Token: 0x04001ABC RID: 6844
		public ObscuredInt amount;
	}
}
