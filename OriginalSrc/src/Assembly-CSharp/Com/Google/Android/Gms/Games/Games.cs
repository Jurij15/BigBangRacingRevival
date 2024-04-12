using System;
using Com.Google.Android.Gms.Common.Api;
using Com.Google.Android.Gms.Games.Stats;
using Google.Developers;

namespace Com.Google.Android.Gms.Games
{
	// Token: 0x02000618 RID: 1560
	public class Games : JavaObjWrapper
	{
		// Token: 0x06002DE9 RID: 11753 RVA: 0x001C2101 File Offset: 0x001C0501
		public Games(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x001C210A File Offset: 0x001C050A
		public static string EXTRA_PLAYER_IDS
		{
			get
			{
				return JavaObjWrapper.GetStaticStringField("com/google/android/gms/games/Games", "EXTRA_PLAYER_IDS");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x001C211B File Offset: 0x001C051B
		public static string EXTRA_STATUS
		{
			get
			{
				return JavaObjWrapper.GetStaticStringField("com/google/android/gms/games/Games", "EXTRA_STATUS");
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x001C212C File Offset: 0x001C052C
		public static object SCOPE_GAMES
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "SCOPE_GAMES", "Lcom/google/android/gms/common/api/Scope;");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x001C2142 File Offset: 0x001C0542
		public static object API
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "API", "Lcom/google/android/gms/common/api/Api;");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x001C2158 File Offset: 0x001C0558
		public static object GamesMetadata
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "GamesMetadata", "Lcom/google/android/gms/games/GamesMetadata;");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x001C216E File Offset: 0x001C056E
		public static object Achievements
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "Achievements", "Lcom/google/android/gms/games/achievement/Achievements;");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x001C2184 File Offset: 0x001C0584
		public static object Events
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "Events", "Lcom/google/android/gms/games/event/Events;");
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x001C219A File Offset: 0x001C059A
		public static object Leaderboards
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "Leaderboards", "Lcom/google/android/gms/games/leaderboard/Leaderboards;");
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x001C21B0 File Offset: 0x001C05B0
		public static object Invitations
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "Invitations", "Lcom/google/android/gms/games/multiplayer/Invitations;");
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x001C21C6 File Offset: 0x001C05C6
		public static object TurnBasedMultiplayer
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "TurnBasedMultiplayer", "Lcom/google/android/gms/games/multiplayer/turnbased/TurnBasedMultiplayer;");
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x001C21DC File Offset: 0x001C05DC
		public static object RealTimeMultiplayer
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "RealTimeMultiplayer", "Lcom/google/android/gms/games/multiplayer/realtime/RealTimeMultiplayer;");
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x001C21F2 File Offset: 0x001C05F2
		public static object Players
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "Players", "Lcom/google/android/gms/games/Players;");
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x001C2208 File Offset: 0x001C0608
		public static object Notifications
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "Notifications", "Lcom/google/android/gms/games/Notifications;");
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x001C221E File Offset: 0x001C061E
		public static object Snapshots
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", "Snapshots", "Lcom/google/android/gms/games/snapshot/Snapshots;");
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x001C2234 File Offset: 0x001C0634
		public static StatsObject Stats
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<StatsObject>("com/google/android/gms/games/Games", "Stats", "Lcom/google/android/gms/games/stats/Stats;");
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x001C224A File Offset: 0x001C064A
		public static string getAppId(GoogleApiClient arg_GoogleApiClient_1)
		{
			return JavaObjWrapper.StaticInvokeCall<string>("com/google/android/gms/games/Games", "getAppId", "(Lcom/google/android/gms/common/api/GoogleApiClient;)Ljava/lang/String;", new object[] { arg_GoogleApiClient_1 });
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x001C226A File Offset: 0x001C066A
		public static string getCurrentAccountName(GoogleApiClient arg_GoogleApiClient_1)
		{
			return JavaObjWrapper.StaticInvokeCall<string>("com/google/android/gms/games/Games", "getCurrentAccountName", "(Lcom/google/android/gms/common/api/GoogleApiClient;)Ljava/lang/String;", new object[] { arg_GoogleApiClient_1 });
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x001C228A File Offset: 0x001C068A
		public static int getSdkVariant(GoogleApiClient arg_GoogleApiClient_1)
		{
			return JavaObjWrapper.StaticInvokeCall<int>("com/google/android/gms/games/Games", "getSdkVariant", "(Lcom/google/android/gms/common/api/GoogleApiClient;)I", new object[] { arg_GoogleApiClient_1 });
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x001C22AA File Offset: 0x001C06AA
		public static object getSettingsIntent(GoogleApiClient arg_GoogleApiClient_1)
		{
			return JavaObjWrapper.StaticInvokeCall<object>("com/google/android/gms/games/Games", "getSettingsIntent", "(Lcom/google/android/gms/common/api/GoogleApiClient;)Landroid/content/Intent;", new object[] { arg_GoogleApiClient_1 });
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x001C22CA File Offset: 0x001C06CA
		public static void setGravityForPopups(GoogleApiClient arg_GoogleApiClient_1, int arg_int_2)
		{
			JavaObjWrapper.StaticInvokeCallVoid("com/google/android/gms/games/Games", "setGravityForPopups", "(Lcom/google/android/gms/common/api/GoogleApiClient;I)V", new object[] { arg_GoogleApiClient_1, arg_int_2 });
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x001C22F3 File Offset: 0x001C06F3
		public static void setViewForPopups(GoogleApiClient arg_GoogleApiClient_1, object arg_object_2)
		{
			JavaObjWrapper.StaticInvokeCallVoid("com/google/android/gms/games/Games", "setViewForPopups", "(Lcom/google/android/gms/common/api/GoogleApiClient;Landroid/view/View;)V", new object[] { arg_GoogleApiClient_1, arg_object_2 });
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x001C2317 File Offset: 0x001C0717
		public static PendingResult<Status> signOut(GoogleApiClient arg_GoogleApiClient_1)
		{
			return JavaObjWrapper.StaticInvokeCall<PendingResult<Status>>("com/google/android/gms/games/Games", "signOut", "(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;", new object[] { arg_GoogleApiClient_1 });
		}

		// Token: 0x0400317B RID: 12667
		private const string CLASS_NAME = "com/google/android/gms/games/Games";
	}
}
