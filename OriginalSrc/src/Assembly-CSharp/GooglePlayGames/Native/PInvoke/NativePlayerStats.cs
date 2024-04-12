using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F5 RID: 1781
	internal class NativePlayerStats : BaseReferenceHolder
	{
		// Token: 0x06003355 RID: 13141 RVA: 0x001CBD7D File Offset: 0x001CA17D
		internal NativePlayerStats(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x001CBD86 File Offset: 0x001CA186
		internal bool Valid()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_Valid(base.SelfPtr());
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x001CBD93 File Offset: 0x001CA193
		internal bool HasAverageSessionLength()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_HasAverageSessionLength(base.SelfPtr());
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x001CBDA0 File Offset: 0x001CA1A0
		internal float AverageSessionLength()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_AverageSessionLength(base.SelfPtr());
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x001CBDAD File Offset: 0x001CA1AD
		internal bool HasChurnProbability()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_HasChurnProbability(base.SelfPtr());
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x001CBDBA File Offset: 0x001CA1BA
		internal float ChurnProbability()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_ChurnProbability(base.SelfPtr());
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x001CBDC7 File Offset: 0x001CA1C7
		internal bool HasDaysSinceLastPlayed()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_HasDaysSinceLastPlayed(base.SelfPtr());
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x001CBDD4 File Offset: 0x001CA1D4
		internal int DaysSinceLastPlayed()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_DaysSinceLastPlayed(base.SelfPtr());
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x001CBDE1 File Offset: 0x001CA1E1
		internal bool HasNumberOfPurchases()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_HasNumberOfPurchases(base.SelfPtr());
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x001CBDEE File Offset: 0x001CA1EE
		internal int NumberOfPurchases()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_NumberOfPurchases(base.SelfPtr());
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x001CBDFB File Offset: 0x001CA1FB
		internal bool HasNumberOfSessions()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_HasNumberOfSessions(base.SelfPtr());
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x001CBE08 File Offset: 0x001CA208
		internal int NumberOfSessions()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_NumberOfSessions(base.SelfPtr());
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x001CBE15 File Offset: 0x001CA215
		internal bool HasSessionPercentile()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_HasSessionPercentile(base.SelfPtr());
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x001CBE22 File Offset: 0x001CA222
		internal float SessionPercentile()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_SessionPercentile(base.SelfPtr());
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x001CBE2F File Offset: 0x001CA22F
		internal bool HasSpendPercentile()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_HasSpendPercentile(base.SelfPtr());
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x001CBE3C File Offset: 0x001CA23C
		internal float SpendPercentile()
		{
			return GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_SpendPercentile(base.SelfPtr());
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x001CBE49 File Offset: 0x001CA249
		protected override void CallDispose(HandleRef selfPointer)
		{
			GooglePlayGames.Native.Cwrapper.PlayerStats.PlayerStats_Dispose(selfPointer);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x001CBE54 File Offset: 0x001CA254
		internal GooglePlayGames.BasicApi.PlayerStats AsPlayerStats()
		{
			GooglePlayGames.BasicApi.PlayerStats playerStats = new GooglePlayGames.BasicApi.PlayerStats();
			playerStats.Valid = this.Valid();
			if (this.Valid())
			{
				playerStats.AvgSessonLength = this.AverageSessionLength();
				playerStats.ChurnProbability = this.ChurnProbability();
				playerStats.DaysSinceLastPlayed = this.DaysSinceLastPlayed();
				playerStats.NumberOfPurchases = this.NumberOfPurchases();
				playerStats.NumberOfSessions = this.NumberOfSessions();
				playerStats.SessPercentile = this.SessionPercentile();
				playerStats.SpendPercentile = this.SpendPercentile();
				playerStats.SpendProbability = -1f;
			}
			return playerStats;
		}
	}
}
