using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200067B RID: 1659
	internal static class Score
	{
		// Token: 0x06003015 RID: 12309
		[DllImport("gpg")]
		internal static extern ulong Score_Value(HandleRef self);

		// Token: 0x06003016 RID: 12310
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool Score_Valid(HandleRef self);

		// Token: 0x06003017 RID: 12311
		[DllImport("gpg")]
		internal static extern ulong Score_Rank(HandleRef self);

		// Token: 0x06003018 RID: 12312
		[DllImport("gpg")]
		internal static extern void Score_Dispose(HandleRef self);

		// Token: 0x06003019 RID: 12313
		[DllImport("gpg")]
		internal static extern UIntPtr Score_Metadata(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);
	}
}
