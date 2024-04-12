using System;
using System.Collections.Generic;
using Prime31;

namespace InAppPurchases
{
	// Token: 0x0200052B RID: 1323
	public class IAPBase
	{
		// Token: 0x06002709 RID: 9993 RVA: 0x001A8DC0 File Offset: 0x001A71C0
		public IAPBase()
		{
			if (IAPBase.instance == null)
			{
				IAPBase.instance = this;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x001A8DD8 File Offset: 0x001A71D8
		public IAPBase Instance
		{
			get
			{
				if (IAPBase.instance == null)
				{
					IAPBase.instance = new IAPBase();
				}
				return IAPBase.instance;
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x001A8DF3 File Offset: 0x001A71F3
		public virtual bool HavePendingPurchases()
		{
			return false;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x001A8DF6 File Offset: 0x001A71F6
		public virtual void Initialize(string _publicKey, Action _callback)
		{
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x001A8DF8 File Offset: 0x001A71F8
		public virtual void PurchaseConsumable(string _id, string _nonce, Action<string, bool, string, string> _callback = null)
		{
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x001A8DFA File Offset: 0x001A71FA
		public virtual void PurchaseNonConsumable(string _id, string _nonce, Action<string, bool, string, string> _callback = null)
		{
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x001A8DFC File Offset: 0x001A71FC
		public virtual void CompletePendingPurchases(Action<string, bool, string, string> _callback = null)
		{
			_callback.Invoke(null, false, "No pending transactions", null);
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x001A8E0C File Offset: 0x001A720C
		public virtual void requestProductData(string[] iosProductIdentifiers, string[] androidSkus, Action<List<IAPProduct>> completionHandler)
		{
			Debug.Log("IAPBase: Requesting product data.", null);
			this._productListReceivedAction = completionHandler;
			GoogleIAB.queryInventory(androidSkus);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x001A8E26 File Offset: 0x001A7226
		public virtual void restoreCompletedTransactions(Action<string> completionHandler)
		{
		}

		// Token: 0x04002C72 RID: 11378
		protected static IAPBase instance = new IAPBase();

		// Token: 0x04002C73 RID: 11379
		protected Action<List<IAPProduct>> _productListReceivedAction;
	}
}
