using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000641 RID: 1601
	internal static class Event
	{
		// Token: 0x06002EA8 RID: 11944
		[DllImport("gpg")]
		internal static extern ulong Event_Count(HandleRef self);

		// Token: 0x06002EA9 RID: 11945
		[DllImport("gpg")]
		internal static extern UIntPtr Event_Description(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002EAA RID: 11946
		[DllImport("gpg")]
		internal static extern UIntPtr Event_ImageUrl(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002EAB RID: 11947
		[DllImport("gpg")]
		internal static extern Types.EventVisibility Event_Visibility(HandleRef self);

		// Token: 0x06002EAC RID: 11948
		[DllImport("gpg")]
		internal static extern UIntPtr Event_Id(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002EAD RID: 11949
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool Event_Valid(HandleRef self);

		// Token: 0x06002EAE RID: 11950
		[DllImport("gpg")]
		internal static extern void Event_Dispose(HandleRef self);

		// Token: 0x06002EAF RID: 11951
		[DllImport("gpg")]
		internal static extern IntPtr Event_Copy(HandleRef self);

		// Token: 0x06002EB0 RID: 11952
		[DllImport("gpg")]
		internal static extern UIntPtr Event_Name(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);
	}
}
