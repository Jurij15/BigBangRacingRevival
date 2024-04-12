using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000663 RID: 1635
	internal static class Player
	{
		// Token: 0x06002F6D RID: 12141
		[DllImport("gpg")]
		internal static extern IntPtr Player_CurrentLevel(HandleRef self);

		// Token: 0x06002F6E RID: 12142
		[DllImport("gpg")]
		internal static extern UIntPtr Player_Name(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F6F RID: 12143
		[DllImport("gpg")]
		internal static extern void Player_Dispose(HandleRef self);

		// Token: 0x06002F70 RID: 12144
		[DllImport("gpg")]
		internal static extern UIntPtr Player_AvatarUrl(HandleRef self, Types.ImageResolution resolution, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F71 RID: 12145
		[DllImport("gpg")]
		internal static extern ulong Player_LastLevelUpTime(HandleRef self);

		// Token: 0x06002F72 RID: 12146
		[DllImport("gpg")]
		internal static extern UIntPtr Player_Title(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F73 RID: 12147
		[DllImport("gpg")]
		internal static extern ulong Player_CurrentXP(HandleRef self);

		// Token: 0x06002F74 RID: 12148
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool Player_Valid(HandleRef self);

		// Token: 0x06002F75 RID: 12149
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool Player_HasLevelInfo(HandleRef self);

		// Token: 0x06002F76 RID: 12150
		[DllImport("gpg")]
		internal static extern IntPtr Player_NextLevel(HandleRef self);

		// Token: 0x06002F77 RID: 12151
		[DllImport("gpg")]
		internal static extern UIntPtr Player_Id(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);
	}
}
