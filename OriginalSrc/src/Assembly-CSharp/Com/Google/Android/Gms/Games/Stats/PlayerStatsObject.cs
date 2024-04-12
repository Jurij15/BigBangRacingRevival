using System;
using Google.Developers;

namespace Com.Google.Android.Gms.Games.Stats
{
	// Token: 0x0200061C RID: 1564
	public class PlayerStatsObject : JavaObjWrapper, PlayerStats
	{
		// Token: 0x06002E0F RID: 11791 RVA: 0x001C23BE File Offset: 0x001C07BE
		public PlayerStatsObject(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x001C23C7 File Offset: 0x001C07C7
		public static float UNSET_VALUE
		{
			get
			{
				return JavaObjWrapper.GetStaticFloatField("com/google/android/gms/games/stats/PlayerStats", "UNSET_VALUE");
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x001C23D8 File Offset: 0x001C07D8
		public static int CONTENTS_FILE_DESCRIPTOR
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/games/stats/PlayerStats", "CONTENTS_FILE_DESCRIPTOR");
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x001C23E9 File Offset: 0x001C07E9
		public static int PARCELABLE_WRITE_RETURN_VALUE
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/games/stats/PlayerStats", "PARCELABLE_WRITE_RETURN_VALUE");
			}
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x001C23FA File Offset: 0x001C07FA
		public float getAverageSessionLength()
		{
			return base.InvokeCall<float>("getAverageSessionLength", "()F", new object[0]);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x001C2412 File Offset: 0x001C0812
		public float getChurnProbability()
		{
			return base.InvokeCall<float>("getChurnProbability", "()F", new object[0]);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x001C242A File Offset: 0x001C082A
		public int getDaysSinceLastPlayed()
		{
			return base.InvokeCall<int>("getDaysSinceLastPlayed", "()I", new object[0]);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x001C2442 File Offset: 0x001C0842
		public int getNumberOfPurchases()
		{
			return base.InvokeCall<int>("getNumberOfPurchases", "()I", new object[0]);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x001C245A File Offset: 0x001C085A
		public int getNumberOfSessions()
		{
			return base.InvokeCall<int>("getNumberOfSessions", "()I", new object[0]);
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x001C2472 File Offset: 0x001C0872
		public float getSessionPercentile()
		{
			return base.InvokeCall<float>("getSessionPercentile", "()F", new object[0]);
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x001C248A File Offset: 0x001C088A
		public float getSpendPercentile()
		{
			return base.InvokeCall<float>("getSpendPercentile", "()F", new object[0]);
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x001C24A2 File Offset: 0x001C08A2
		public float getSpendProbability()
		{
			return base.InvokeCall<float>("getSpendProbability", "()F", new object[0]);
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x001C24BA File Offset: 0x001C08BA
		public float getHighSpenderProbability()
		{
			return base.InvokeCall<float>("getHighSpenderProbability", "()F", new object[0]);
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x001C24D2 File Offset: 0x001C08D2
		public float getTotalSpendNext28Days()
		{
			return base.InvokeCall<float>("getTotalSpendNext28Days", "()F", new object[0]);
		}

		// Token: 0x0400317E RID: 12670
		private const string CLASS_NAME = "com/google/android/gms/games/stats/PlayerStats";
	}
}
