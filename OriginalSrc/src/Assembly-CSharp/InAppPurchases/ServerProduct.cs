using System;
using System.Collections.Generic;

namespace InAppPurchases
{
	// Token: 0x0200052D RID: 1325
	public class ServerProduct
	{
		// Token: 0x04002C7A RID: 11386
		public string identifier = string.Empty;

		// Token: 0x04002C7B RID: 11387
		public string androidIdentifier = string.Empty;

		// Token: 0x04002C7C RID: 11388
		public string resource = string.Empty;

		// Token: 0x04002C7D RID: 11389
		public int amount;

		// Token: 0x04002C7E RID: 11390
		public int order;

		// Token: 0x04002C7F RID: 11391
		public bool visible = true;

		// Token: 0x04002C80 RID: 11392
		public string sticker = string.Empty;

		// Token: 0x04002C81 RID: 11393
		public Dictionary<string, object> bundle;
	}
}
