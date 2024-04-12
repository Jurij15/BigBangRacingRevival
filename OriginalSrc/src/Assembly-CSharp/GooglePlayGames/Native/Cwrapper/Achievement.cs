using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000623 RID: 1571
	internal static class Achievement
	{
		// Token: 0x06002E33 RID: 11827
		[DllImport("gpg")]
		internal static extern uint Achievement_TotalSteps(HandleRef self);

		// Token: 0x06002E34 RID: 11828
		[DllImport("gpg")]
		internal static extern UIntPtr Achievement_Description(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002E35 RID: 11829
		[DllImport("gpg")]
		internal static extern void Achievement_Dispose(HandleRef self);

		// Token: 0x06002E36 RID: 11830
		[DllImport("gpg")]
		internal static extern UIntPtr Achievement_UnlockedIconUrl(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002E37 RID: 11831
		[DllImport("gpg")]
		internal static extern ulong Achievement_LastModifiedTime(HandleRef self);

		// Token: 0x06002E38 RID: 11832
		[DllImport("gpg")]
		internal static extern UIntPtr Achievement_RevealedIconUrl(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002E39 RID: 11833
		[DllImport("gpg")]
		internal static extern uint Achievement_CurrentSteps(HandleRef self);

		// Token: 0x06002E3A RID: 11834
		[DllImport("gpg")]
		internal static extern Types.AchievementState Achievement_State(HandleRef self);

		// Token: 0x06002E3B RID: 11835
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool Achievement_Valid(HandleRef self);

		// Token: 0x06002E3C RID: 11836
		[DllImport("gpg")]
		internal static extern ulong Achievement_LastModified(HandleRef self);

		// Token: 0x06002E3D RID: 11837
		[DllImport("gpg")]
		internal static extern ulong Achievement_XP(HandleRef self);

		// Token: 0x06002E3E RID: 11838
		[DllImport("gpg")]
		internal static extern Types.AchievementType Achievement_Type(HandleRef self);

		// Token: 0x06002E3F RID: 11839
		[DllImport("gpg")]
		internal static extern UIntPtr Achievement_Id(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002E40 RID: 11840
		[DllImport("gpg")]
		internal static extern UIntPtr Achievement_Name(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);
	}
}
