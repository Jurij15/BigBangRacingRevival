using System;
using System.Collections.Generic;
using System.Linq;
using Prime31;

namespace InAppPurchases
{
	// Token: 0x02000527 RID: 1319
	public static class Manager
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x001A8246 File Offset: 0x001A6646
		public static List<ServerProduct> Products
		{
			get
			{
				return Manager.m_products;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x001A824D File Offset: 0x001A664D
		public static bool ProductsReceived
		{
			get
			{
				return Manager.m_productListReceived;
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x001A8254 File Offset: 0x001A6654
		private static ServerProduct ParseProduct(Dictionary<string, object> _productDict)
		{
			ServerProduct serverProduct = new ServerProduct();
			if (_productDict.ContainsKey("identifier"))
			{
				serverProduct.identifier = (string)_productDict["identifier"];
			}
			if (_productDict.ContainsKey("androidIdentifier"))
			{
				serverProduct.androidIdentifier = (string)_productDict["androidIdentifier"];
			}
			if (_productDict.ContainsKey("resource"))
			{
				serverProduct.resource = (string)_productDict["resource"];
			}
			if (_productDict.ContainsKey("amount"))
			{
				serverProduct.amount = Convert.ToInt32(_productDict["amount"]);
			}
			if (_productDict.ContainsKey("visible"))
			{
				serverProduct.visible = Convert.ToBoolean(_productDict["visible"]);
			}
			if (_productDict.ContainsKey("order"))
			{
				serverProduct.order = Convert.ToInt32(_productDict["order"]);
			}
			if (_productDict.ContainsKey("sticker"))
			{
				serverProduct.sticker = (string)_productDict["sticker"];
			}
			if (serverProduct.resource == "bundle" && _productDict.ContainsKey("bundle"))
			{
				serverProduct.bundle = _productDict["bundle"] as Dictionary<string, object>;
			}
			return serverProduct;
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x001A83B0 File Offset: 0x001A67B0
		private static List<ServerProduct> ParseProductList(List<object> products)
		{
			List<ServerProduct> list = new List<ServerProduct>();
			foreach (object obj in products)
			{
				list.Add(Manager.ParseProduct(obj as Dictionary<string, object>));
			}
			return Enumerable.ToList<ServerProduct>(Enumerable.OrderBy<ServerProduct, int>(list, (ServerProduct product) => product.order));
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x001A8440 File Offset: 0x001A6840
		public static bool CanMakePayments()
		{
			return true;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x001A8444 File Offset: 0x001A6844
		public static void Initialize(Dictionary<string, object> _config)
		{
			if (Manager.m_purchaseItems == null)
			{
				Manager.m_purchaseItems = new List<IAPProduct>();
			}
			else
			{
				Manager.m_purchaseItems.Clear();
			}
			Manager.m_productListReceived = false;
			if (_config != null)
			{
				Debug.Log("Got config", null);
				if (_config.ContainsKey("debugLevel"))
				{
					Manager.m_debugLevel = Convert.ToInt32(_config["debugLevel"]);
				}
				if (_config.ContainsKey("items"))
				{
					Manager.m_products = Manager.ParseProductList(_config["items"] as List<object>);
					Manager.m_iosProducts = Enumerable.ToArray<string>(Enumerable.Select<ServerProduct, string>(Manager.m_products, (ServerProduct prod) => prod.identifier));
					Manager.m_androidProducts = Enumerable.ToArray<string>(Enumerable.Select<ServerProduct, string>(Manager.m_products, (ServerProduct prod) => prod.androidIdentifier));
				}
			}
			if (Manager.IapProvider == null)
			{
				Manager.IapProvider = new GoogleIAP();
			}
			Manager.IapProvider.Initialize(Manager.m_androidPK, null);
			Entity entity = EntityManager.AddEntity();
			entity.m_persistent = true;
			TimerS.AddComponent(entity, "IapProductReloadTimer", 0f, 0f, true, delegate(TimerC c)
			{
				Manager.ReloadItems();
			});
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x001A85A4 File Offset: 0x001A69A4
		public static void ReloadItems()
		{
			Debug.Log("Reload Items Called", null);
			if (Manager.AvailableItems.Count <= 0)
			{
				Debug.Log("Requesting products...", null);
				Manager.m_productListReceived = false;
				Manager.IapProvider.requestProductData(Enumerable.ToArray<string>(Manager.m_iosProducts), Enumerable.ToArray<string>(Manager.m_androidProducts), new Action<List<IAPProduct>>(Manager.IapProductListReceived));
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x001A8618 File Offset: 0x001A6A18
		public static ServerProduct GetServerProductById(string _serverProductId)
		{
			ServerProduct serverProduct = Manager.m_products.Find((ServerProduct obj) => obj.identifier == _serverProductId);
			if (serverProduct != null)
			{
				return serverProduct;
			}
			Debug.LogWarning("Product " + _serverProductId + " not found!");
			return new ServerProduct();
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x001A8670 File Offset: 0x001A6A70
		public static ServerProduct GetServerProductByResourceAmount(string _resource, int _amount)
		{
			ServerProduct serverProduct = Manager.m_products.Find((ServerProduct obj) => obj.amount == _amount && obj.resource == _resource);
			if (serverProduct != null)
			{
				return serverProduct;
			}
			Debug.LogWarning(string.Concat(new object[] { "Product ", _amount, " ", _resource, " not found!" }));
			return new ServerProduct();
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x001A86F4 File Offset: 0x001A6AF4
		public static List<IAPProduct> AvailableItems
		{
			get
			{
				return Manager.m_purchaseItems;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x001A86FB File Offset: 0x001A6AFB
		public static void CompletePendingPurchases(Action<string, bool, string, string> _callback)
		{
			Manager.IapProvider.Instance.CompletePendingPurchases(_callback);
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x001A8710 File Offset: 0x001A6B10
		public static void PurchaseConsumable(string _id, string _nonce, Action<string, bool, string, string> _callback)
		{
			PsMetagameManager.IAPDebug("IAPDebug_CallProvider", 2, new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("nonce", _nonce)
			});
			Debug.Log("Calling IapProvider... " + Manager.IapProvider.Instance.GetType().ToString(), null);
			Manager.IapProvider.Instance.PurchaseConsumable(_id, _nonce, _callback);
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x001A877B File Offset: 0x001A6B7B
		public static void PurchaseNonConsumable(string _id, string _nonce, Action<string, bool, string, string> _callback)
		{
			Debug.Log("Calling IapProvider... " + Manager.IapProvider.Instance.GetType().ToString(), null);
			Manager.IapProvider.Instance.PurchaseNonConsumable(_id, _nonce, _callback);
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x001A87B3 File Offset: 0x001A6BB3
		public static void RestoreCompletedTransactions(Action<string> _callback = null)
		{
			if (_callback == null)
			{
				_callback = new Action<string>(Manager.TransactionRestoreCallback);
			}
			Manager.IapProvider.Instance.restoreCompletedTransactions(_callback);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x001A87EC File Offset: 0x001A6BEC
		private static void IapProductListReceived(List<IAPProduct> _products)
		{
			if (_products != null)
			{
				Debug.Log("Product list received, " + _products.Count + " products found.", null);
				Manager.m_purchaseItems = _products;
				foreach (IAPProduct iapproduct in Manager.m_purchaseItems)
				{
					if (iapproduct != null)
					{
						Debug.Log(string.Concat(new string[] { iapproduct.productId, " ", iapproduct.description, " ", iapproduct.title, " ", iapproduct.price, " ", iapproduct.currencyCode }), null);
					}
				}
				Manager.m_productListReceived = true;
			}
			else
			{
				if (Manager.m_purchaseItems == null)
				{
					Manager.m_purchaseItems = new List<IAPProduct>();
				}
				Entity entity = EntityManager.AddEntity();
				entity.m_persistent = true;
				TimerS.AddComponent(entity, "IapProductReloadTimer", 5f, 0f, true, delegate(TimerC c)
				{
					Manager.ReloadItems();
				});
			}
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x001A8934 File Offset: 0x001A6D34
		private static void TransactionRestoreCallback(string _product)
		{
			Debug.Log("restored purchased product: " + _product, null);
		}

		// Token: 0x04002C55 RID: 11349
		public static int m_debugLevel = 3;

		// Token: 0x04002C56 RID: 11350
		private static string m_androidPK = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwgALgBdtptD/+VQP+NqA6r3Wrag32QKgYrMOp3RvkZAbpkQs99sKVH+Ru5ouRWrrJc6NDy8HbyqBV5sTKrDDYAF59PuBFRL7DOSmzojDckul4GlxFk+BCm9BGVKwmSWp2JWu1tzNU9ZxxGh3Y9JZNVHyCBaTUVAIMdpthVNKNFMAdTXDhfbZFs0wcYcGW5E3Nq35QTu1JAlg+LhcjdIbIaIS9v5E3DpieUtZ3Ur/wTUMzpfDXAc2S0fYHSmTejFpJ3CFiFgFkIRqxiTM38VNg9HBJduQsH3sPFshheG6167MVK9mDzXsixTpX2t5l4H1gBIQxNJiFeqNmoPMnoGxSQIDAQAB";

		// Token: 0x04002C57 RID: 11351
		private static string[] m_iosProducts = new string[] { "80gemss", "500gemss", "1200gemss", "2500gemss", "6500gemss", "14000gemss" };

		// Token: 0x04002C58 RID: 11352
		private static string[] m_androidProducts = new string[] { "80gemss", "500gemss", "1200gemss", "2500gemss", "6500gemss", "14000gemss" };

		// Token: 0x04002C59 RID: 11353
		private static List<IAPProduct> m_purchaseItems;

		// Token: 0x04002C5A RID: 11354
		private static List<ServerProduct> m_products = new List<ServerProduct>();

		// Token: 0x04002C5B RID: 11355
		private static bool m_productListReceived;

		// Token: 0x04002C5C RID: 11356
		public static IAPBase IapProvider;
	}
}
