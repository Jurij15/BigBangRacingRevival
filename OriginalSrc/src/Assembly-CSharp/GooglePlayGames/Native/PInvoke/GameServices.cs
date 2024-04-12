using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006DF RID: 1759
	internal class GameServices : BaseReferenceHolder
	{
		// Token: 0x06003299 RID: 12953 RVA: 0x001CA683 File Offset: 0x001C8A83
		internal GameServices(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x001CA68C File Offset: 0x001C8A8C
		internal bool IsAuthenticated()
		{
			return GameServices.GameServices_IsAuthorized(base.SelfPtr());
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x001CA699 File Offset: 0x001C8A99
		internal void SignOut()
		{
			GameServices.GameServices_SignOut(base.SelfPtr());
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x001CA6A6 File Offset: 0x001C8AA6
		internal void StartAuthorizationUI()
		{
			GameServices.GameServices_StartAuthorizationUI(base.SelfPtr());
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x001CA6B3 File Offset: 0x001C8AB3
		public GooglePlayGames.Native.PInvoke.AchievementManager AchievementManager()
		{
			return new GooglePlayGames.Native.PInvoke.AchievementManager(this);
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x001CA6BB File Offset: 0x001C8ABB
		public LeaderboardManager LeaderboardManager()
		{
			return new LeaderboardManager(this);
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x001CA6C3 File Offset: 0x001C8AC3
		public PlayerManager PlayerManager()
		{
			return new PlayerManager(this);
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x001CA6CB File Offset: 0x001C8ACB
		public StatsManager StatsManager()
		{
			return new StatsManager(this);
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x001CA6D3 File Offset: 0x001C8AD3
		internal HandleRef AsHandle()
		{
			return base.SelfPtr();
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x001CA6DB File Offset: 0x001C8ADB
		protected override void CallDispose(HandleRef selfPointer)
		{
			GameServices.GameServices_Dispose(selfPointer);
		}
	}
}
