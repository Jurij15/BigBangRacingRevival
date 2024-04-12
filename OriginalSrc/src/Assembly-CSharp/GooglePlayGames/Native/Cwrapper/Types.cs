using System;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200069C RID: 1692
	internal static class Types
	{
		// Token: 0x0200069D RID: 1693
		internal enum DataSource
		{
			// Token: 0x040031FF RID: 12799
			CACHE_OR_NETWORK = 1,
			// Token: 0x04003200 RID: 12800
			NETWORK_ONLY
		}

		// Token: 0x0200069E RID: 1694
		internal enum LogLevel
		{
			// Token: 0x04003202 RID: 12802
			VERBOSE = 1,
			// Token: 0x04003203 RID: 12803
			INFO,
			// Token: 0x04003204 RID: 12804
			WARNING,
			// Token: 0x04003205 RID: 12805
			ERROR
		}

		// Token: 0x0200069F RID: 1695
		internal enum AuthOperation
		{
			// Token: 0x04003207 RID: 12807
			SIGN_IN = 1,
			// Token: 0x04003208 RID: 12808
			SIGN_OUT
		}

		// Token: 0x020006A0 RID: 1696
		internal enum ImageResolution
		{
			// Token: 0x0400320A RID: 12810
			ICON = 1,
			// Token: 0x0400320B RID: 12811
			HI_RES
		}

		// Token: 0x020006A1 RID: 1697
		internal enum AchievementType
		{
			// Token: 0x0400320D RID: 12813
			STANDARD = 1,
			// Token: 0x0400320E RID: 12814
			INCREMENTAL
		}

		// Token: 0x020006A2 RID: 1698
		internal enum AchievementState
		{
			// Token: 0x04003210 RID: 12816
			HIDDEN = 1,
			// Token: 0x04003211 RID: 12817
			REVEALED,
			// Token: 0x04003212 RID: 12818
			UNLOCKED
		}

		// Token: 0x020006A3 RID: 1699
		internal enum EventVisibility
		{
			// Token: 0x04003214 RID: 12820
			HIDDEN = 1,
			// Token: 0x04003215 RID: 12821
			REVEALED
		}

		// Token: 0x020006A4 RID: 1700
		internal enum LeaderboardOrder
		{
			// Token: 0x04003217 RID: 12823
			LARGER_IS_BETTER = 1,
			// Token: 0x04003218 RID: 12824
			SMALLER_IS_BETTER
		}

		// Token: 0x020006A5 RID: 1701
		internal enum LeaderboardStart
		{
			// Token: 0x0400321A RID: 12826
			TOP_SCORES = 1,
			// Token: 0x0400321B RID: 12827
			PLAYER_CENTERED
		}

		// Token: 0x020006A6 RID: 1702
		internal enum LeaderboardTimeSpan
		{
			// Token: 0x0400321D RID: 12829
			DAILY = 1,
			// Token: 0x0400321E RID: 12830
			WEEKLY,
			// Token: 0x0400321F RID: 12831
			ALL_TIME
		}

		// Token: 0x020006A7 RID: 1703
		internal enum LeaderboardCollection
		{
			// Token: 0x04003221 RID: 12833
			PUBLIC = 1,
			// Token: 0x04003222 RID: 12834
			SOCIAL
		}

		// Token: 0x020006A8 RID: 1704
		internal enum ParticipantStatus
		{
			// Token: 0x04003224 RID: 12836
			INVITED = 1,
			// Token: 0x04003225 RID: 12837
			JOINED,
			// Token: 0x04003226 RID: 12838
			DECLINED,
			// Token: 0x04003227 RID: 12839
			LEFT,
			// Token: 0x04003228 RID: 12840
			NOT_INVITED_YET,
			// Token: 0x04003229 RID: 12841
			FINISHED,
			// Token: 0x0400322A RID: 12842
			UNRESPONSIVE
		}

		// Token: 0x020006A9 RID: 1705
		internal enum MatchResult
		{
			// Token: 0x0400322C RID: 12844
			DISAGREED = 1,
			// Token: 0x0400322D RID: 12845
			DISCONNECTED,
			// Token: 0x0400322E RID: 12846
			LOSS,
			// Token: 0x0400322F RID: 12847
			NONE,
			// Token: 0x04003230 RID: 12848
			TIE,
			// Token: 0x04003231 RID: 12849
			WIN
		}

		// Token: 0x020006AA RID: 1706
		internal enum MatchStatus
		{
			// Token: 0x04003233 RID: 12851
			INVITED = 1,
			// Token: 0x04003234 RID: 12852
			THEIR_TURN,
			// Token: 0x04003235 RID: 12853
			MY_TURN,
			// Token: 0x04003236 RID: 12854
			PENDING_COMPLETION,
			// Token: 0x04003237 RID: 12855
			COMPLETED,
			// Token: 0x04003238 RID: 12856
			CANCELED,
			// Token: 0x04003239 RID: 12857
			EXPIRED
		}

		// Token: 0x020006AB RID: 1707
		internal enum MultiplayerEvent
		{
			// Token: 0x0400323B RID: 12859
			UPDATED = 1,
			// Token: 0x0400323C RID: 12860
			UPDATED_FROM_APP_LAUNCH,
			// Token: 0x0400323D RID: 12861
			REMOVED
		}

		// Token: 0x020006AC RID: 1708
		internal enum MultiplayerInvitationType
		{
			// Token: 0x0400323F RID: 12863
			TURN_BASED = 1,
			// Token: 0x04003240 RID: 12864
			REAL_TIME
		}

		// Token: 0x020006AD RID: 1709
		internal enum RealTimeRoomStatus
		{
			// Token: 0x04003242 RID: 12866
			INVITING = 1,
			// Token: 0x04003243 RID: 12867
			CONNECTING,
			// Token: 0x04003244 RID: 12868
			AUTO_MATCHING,
			// Token: 0x04003245 RID: 12869
			ACTIVE,
			// Token: 0x04003246 RID: 12870
			DELETED
		}

		// Token: 0x020006AE RID: 1710
		internal enum SnapshotConflictPolicy
		{
			// Token: 0x04003248 RID: 12872
			MANUAL = 1,
			// Token: 0x04003249 RID: 12873
			LONGEST_PLAYTIME,
			// Token: 0x0400324A RID: 12874
			LAST_KNOWN_GOOD,
			// Token: 0x0400324B RID: 12875
			MOST_RECENTLY_MODIFIED
		}

		// Token: 0x020006AF RID: 1711
		internal enum VideoCaptureMode
		{
			// Token: 0x0400324D RID: 12877
			UNKNOWN = -1,
			// Token: 0x0400324E RID: 12878
			FILE,
			// Token: 0x0400324F RID: 12879
			STREAM
		}

		// Token: 0x020006B0 RID: 1712
		internal enum VideoQualityLevel
		{
			// Token: 0x04003251 RID: 12881
			UNKNOWN = -1,
			// Token: 0x04003252 RID: 12882
			SD,
			// Token: 0x04003253 RID: 12883
			HD,
			// Token: 0x04003254 RID: 12884
			XHD,
			// Token: 0x04003255 RID: 12885
			FULLHD
		}

		// Token: 0x020006B1 RID: 1713
		internal enum VideoCaptureOverlayState
		{
			// Token: 0x04003257 RID: 12887
			UNKNOWN = -1,
			// Token: 0x04003258 RID: 12888
			SHOWN = 1,
			// Token: 0x04003259 RID: 12889
			STARTED,
			// Token: 0x0400325A RID: 12890
			STOPPED,
			// Token: 0x0400325B RID: 12891
			DISMISSED
		}
	}
}
