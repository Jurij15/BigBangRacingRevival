using System;
using System.Collections.Generic;
using MiniJSON;
using Prime31;
using Server;

namespace InAppPurchases
{
	// Token: 0x0200052A RID: 1322
	public class GoogleIAP : IAPBase
	{
		// Token: 0x060026F8 RID: 9976 RVA: 0x001A8E34 File Offset: 0x001A7234
		public GoogleIAP()
		{
			GoogleIABManager.queryInventorySucceededEvent += delegate(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
			{
				this.pendingPurchases = purchases;
				if (this.pendingPurchases != null && this.pendingPurchases.Count > 0)
				{
					List<string> list = new List<string>();
					List<string> list2 = new List<string>();
					for (int i = 0; i < this.pendingPurchases.Count; i++)
					{
						if (this.pendingPurchases[i] != null)
						{
							list2.Add(this.pendingPurchases[i].productId);
							list.Add(this.pendingPurchases[i].developerPayload);
						}
					}
					PsMetagameManager.IAPDebug("IAPDebug_PendingPurchasesReceived", 2, new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("count", this.pendingPurchases.Count),
						new KeyValuePair<string, object>("productIds", list2),
						new KeyValuePair<string, object>("payloads", list)
					});
				}
				Debug.Log("GoogleIAP: Inventory query succeeded", null);
				List<IAPProduct> list3 = new List<IAPProduct>();
				foreach (GoogleSkuInfo googleSkuInfo in skus)
				{
					list3.Add(new IAPProduct(googleSkuInfo));
				}
				if (this._productListReceivedAction != null)
				{
					this._productListReceivedAction.Invoke(list3);
				}
				Debug.Log("products found: " + list3.Count, null);
			};
			GoogleIABManager.queryInventoryFailedEvent += delegate(string error)
			{
				Debug.Log("fetching prouduct data failed: " + error, null);
				if (this._productListReceivedAction != null)
				{
					this._productListReceivedAction.Invoke(null);
				}
			};
			GoogleIABManager.purchaseSucceededEvent += delegate(GooglePurchase purchase)
			{
				if (purchase != null)
				{
					PsMetagameManager.IAPDebug("IAPDebug_StoreSucceeded", new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("payload", purchase.developerPayload),
						new KeyValuePair<string, object>("orderId", purchase.orderId),
						new KeyValuePair<string, object>("productId", purchase.productId),
						new KeyValuePair<string, object>("purchaseState", purchase.purchaseState.ToString())
					});
				}
				else
				{
					PsMetagameManager.IAPDebug("IAPDebug_StoreSucceeded", new KeyValuePair<string, object>[0]);
				}
				if (this._purchaseCompletionAction != null)
				{
					this._purchaseCompletionAction.Invoke(purchase, true, null);
				}
			};
			GoogleIABManager.purchaseFailedEvent += delegate(string error, int response)
			{
				PsMetagameManager.IAPDebug("IAPDebug_StoreFailed", new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("error", error)
				});
				Debug.Log("purchase failed: " + error, null);
				if (this._purchaseCompletionAction != null)
				{
					this._purchaseCompletionAction.Invoke(null, false, error);
				}
			};
			GoogleIABManager.consumePurchaseSucceededEvent += delegate(GooglePurchase purchase)
			{
			};
			GoogleIABManager.consumePurchaseFailedEvent += delegate(string error)
			{
			};
			IAPBase.instance = this;
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x001A8EE0 File Offset: 0x001A72E0
		public override void Initialize(string _publicKey, Action _callback)
		{
			GoogleIAB.init(_publicKey);
			IAPBase.instance = this;
			if (_callback != null)
			{
				_callback.Invoke();
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x001A8EFC File Offset: 0x001A72FC
		public override void PurchaseConsumable(string _id, string _nonce, Action<string, bool, string, string> _callback)
		{
			string androidIdentifier = Manager.GetServerProductById(_id).androidIdentifier;
			if (!string.IsNullOrEmpty(androidIdentifier))
			{
				PsMetagameManager.IAPDebug("IAPDebug_StartStore", new KeyValuePair<string, object>[0]);
				Debug.Log("Android PurchaseConsumable: " + androidIdentifier, null);
				this._purchaseCompletionAction = delegate(GooglePurchase transaction, bool success, string error)
				{
					this.PurchaseConsumableCallback(_id, transaction, success, error, _callback);
				};
				this._purchaseRestorationAction = null;
				GoogleIAB.purchaseProduct(_id, _nonce);
			}
			else
			{
				Debug.Log("No product found.. cancelling..", null);
				_callback.Invoke(null, false, "Product not found", null);
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x001A8FAC File Offset: 0x001A73AC
		public override void PurchaseNonConsumable(string _id, string _nonce, Action<string, bool, string, string> _callback)
		{
			string androidIdentifier = Manager.GetServerProductById(_id).androidIdentifier;
			if (!string.IsNullOrEmpty(androidIdentifier))
			{
				Debug.Log("Android PurchaseNonConsumable: " + androidIdentifier, null);
				this._purchaseCompletionAction = delegate(GooglePurchase transaction, bool success, string error)
				{
					this.PurchaseNonConsumableCallback(_id, transaction, success, error, _callback);
				};
				this._purchaseRestorationAction = null;
				GoogleIAB.purchaseProduct(_id, _nonce);
			}
			else
			{
				Debug.Log("No product found.. cancelling..", null);
				_callback.Invoke(null, false, "Product not found", null);
			}
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x001A904C File Offset: 0x001A744C
		public void CompletePurchase(GooglePurchase _transaction, Action<string, bool, string, string> _callback)
		{
			IAPProduct iapproduct = null;
			string text = "Product not found.";
			try
			{
				iapproduct = PsIAPManager.GetIAPProductById(_transaction.productId);
			}
			catch (Exception ex)
			{
				text = ex.Message;
			}
			if (iapproduct != null)
			{
				double priceFromString = ClientTools.GetPriceFromString(iapproduct.price);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("product", _transaction.productId);
				dictionary.Add("price", priceFromString);
				dictionary.Add("currencyCode", iapproduct.currencyCode);
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				dictionary2.Add("metrics", dictionary);
				dictionary2.Add("receipt-data", _transaction.originalJson);
				dictionary2.Add("signature", _transaction.signature);
				string text2 = Json.Serialize(dictionary2);
				PsMetagameManager.IAPDebug("IAPDebug_CompletePurchase", new KeyValuePair<string, object>[0]);
				HttpC httpC = InAppPurchase.CompletePurchase(text2, delegate(HttpC cb)
				{
					this.PurchaseSuccess(cb, _callback, _transaction.productId, _transaction.orderId, _transaction);
				}, delegate(HttpC cb)
				{
					this.ReceiptValidationFail(cb, _transaction, _callback, _transaction.productId);
				}, null);
				httpC.objectData = _transaction;
			}
			else
			{
				PsMetagameManager.IAPDebug("IAPDebug_CompletePurchaseError", new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("error", text)
				});
				_callback.Invoke(_transaction.productId, false, "Product not found", _transaction.orderId);
			}
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x001A91E4 File Offset: 0x001A75E4
		public override bool HavePendingPurchases()
		{
			return this.pendingPurchases != null && this.pendingPurchases.Count > 0;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x001A9204 File Offset: 0x001A7604
		public override void CompletePendingPurchases(Action<string, bool, string, string> _callback)
		{
			PsMetagameManager.IAPDebug("IAPDebug_CompletePendingPurchase", 2, new KeyValuePair<string, object>[0]);
			this._purchaseCompletionAction = null;
			if (this.pendingPurchases != null && this.pendingPurchases.Count > 0)
			{
				GooglePurchase googlePurchase = this.pendingPurchases[0];
				this.pendingPurchases.Remove(googlePurchase);
				if (googlePurchase != null)
				{
					this.CompletePurchase(googlePurchase, _callback);
				}
				else
				{
					_callback.Invoke(null, false, "Purchase is null", null);
				}
			}
			else
			{
				_callback.Invoke(null, false, "No purchases to refresh", null);
			}
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x001A9294 File Offset: 0x001A7694
		public void PurchaseConsumableCallback(string _productId, GooglePurchase _transaction, bool _result, string _error, Action<string, bool, string, string> _callback)
		{
			PsMetagameManager.IAPDebug("IAPDebug_StartCompleting", new KeyValuePair<string, object>[0]);
			if (_result && _transaction != null && (_transaction.purchaseState == null || _transaction.purchaseState == 2))
			{
				this.CompletePurchase(_transaction, _callback);
			}
			else if (_transaction != null && _transaction.purchaseState == 1)
			{
				_callback.Invoke(null, _result, "Purchase cancelled.", null);
			}
			else if (_transaction == null)
			{
				if (_error.Contains("User canceled"))
				{
					_callback.Invoke(null, _result, "User canceled", null);
				}
				else
				{
					_callback.Invoke(null, _result, "Something went wrong when connecting to Google Play store.", null);
				}
			}
			else
			{
				_callback.Invoke(null, _result, _error, _transaction.orderId);
			}
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x001A9358 File Offset: 0x001A7758
		public void PurchaseNonConsumableCallback(string _productId, GooglePurchase _transaction, bool _result, string _error, Action<string, bool, string, string> _callback)
		{
			if (_result && _transaction != null && (_transaction.purchaseState == null || _transaction.purchaseState == 2))
			{
				this.CompletePurchase(_transaction, _callback);
			}
			else if (_transaction != null && _transaction.purchaseState == 1)
			{
				_callback.Invoke(null, _result, "Purchase cancelled.", null);
			}
			else if (_transaction == null)
			{
				if (_error.Contains("User canceled"))
				{
					_callback.Invoke(null, _result, "User canceled", null);
				}
				else
				{
					_callback.Invoke(null, _result, "Something went wrong when connecting to Google Play store.", null);
				}
			}
			else
			{
				_callback.Invoke(null, _result, _error, _transaction.orderId);
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x001A940C File Offset: 0x001A780C
		public void PurchaseSuccess(HttpC _request, Action<string, bool, string, string> _callback, string _productId, string _transactionId, GooglePurchase _transaction)
		{
			PsMetagameManager.IAPDebug("IAPDebug_PurchaseSuccess", 2, new KeyValuePair<string, object>[0]);
			if (!_transaction.developerPayload.Contains("nonconsume") && !_transaction.productId.Contains("NoConsume"))
			{
				PsMetagameManager.IAPDebug("IAPDebug_ConsumeProduct", new KeyValuePair<string, object>[0]);
				GoogleIAB.consumeProduct(_productId);
			}
			Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_request.www.text);
			if (dictionary != null)
			{
				PlayerData playerData = ClientTools.ParsePlayerData(dictionary);
				PsMetagameManager.SetPlayer(playerData);
				PsCustomisationManager.SetData(dictionary);
				PsTimedSpecialOffer psTimedSpecialOffer = ClientTools.ParseTimedSpecialOffer(dictionary);
				PsMetagameManager.InitializeTimedSpecialOffers(new PsTimedSpecialOffer[] { psTimedSpecialOffer });
				_callback.Invoke(_productId, true, null, _transactionId);
			}
			else
			{
				_callback.Invoke(_productId, false, "Receipt validation failed.", _transactionId);
			}
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x001A94CC File Offset: 0x001A78CC
		public void ReceiptValidationFail(HttpC _request, GooglePurchase _transaction, Action<string, bool, string, string> _callback, string _productId)
		{
			PsMetagameManager.IAPDebug("IAPDebug_ValidationFailed", new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("error", _request.www.error)
			});
			string networkError = ServerErrors.GetNetworkError(_request.www.error);
			if (_request.www.error.StartsWith("500"))
			{
				GoogleIAB.consumeProduct(_productId);
				ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, null, delegate
				{
					_callback.Invoke(_productId, false, "Receipt validation failed.", null);
				}, StringID.TRY_AGAIN_SERVER);
			}
			else
			{
				ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, null, delegate
				{
					this.CompletePurchase(_transaction, _callback);
				}, StringID.TRY_AGAIN_SERVER);
			}
		}

		// Token: 0x04002C6B RID: 11371
		public List<GooglePurchase> pendingPurchases = new List<GooglePurchase>();

		// Token: 0x04002C6C RID: 11372
		private Action<GooglePurchase, bool, string> _purchaseCompletionAction;

		// Token: 0x04002C6D RID: 11373
		private const string CONSUMABLE_PAYLOAD = "consume";

		// Token: 0x04002C6E RID: 11374
		private const string NON_CONSUMABLE_PAYLOAD = "nonconsume";

		// Token: 0x04002C6F RID: 11375
		private Action<string> _purchaseRestorationAction;
	}
}
