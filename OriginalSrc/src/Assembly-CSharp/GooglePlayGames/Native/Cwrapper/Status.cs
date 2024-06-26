﻿using System;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200068A RID: 1674
	internal static class Status
	{
		// Token: 0x0200068B RID: 1675
		internal enum ResponseStatus
		{
			// Token: 0x040031CA RID: 12746
			VALID = 1,
			// Token: 0x040031CB RID: 12747
			VALID_BUT_STALE,
			// Token: 0x040031CC RID: 12748
			ERROR_LICENSE_CHECK_FAILED = -1,
			// Token: 0x040031CD RID: 12749
			ERROR_INTERNAL = -2,
			// Token: 0x040031CE RID: 12750
			ERROR_NOT_AUTHORIZED = -3,
			// Token: 0x040031CF RID: 12751
			ERROR_VERSION_UPDATE_REQUIRED = -4,
			// Token: 0x040031D0 RID: 12752
			ERROR_TIMEOUT = -5
		}

		// Token: 0x0200068C RID: 1676
		internal enum FlushStatus
		{
			// Token: 0x040031D2 RID: 12754
			FLUSHED = 4,
			// Token: 0x040031D3 RID: 12755
			ERROR_INTERNAL = -2,
			// Token: 0x040031D4 RID: 12756
			ERROR_NOT_AUTHORIZED = -3,
			// Token: 0x040031D5 RID: 12757
			ERROR_VERSION_UPDATE_REQUIRED = -4,
			// Token: 0x040031D6 RID: 12758
			ERROR_TIMEOUT = -5
		}

		// Token: 0x0200068D RID: 1677
		internal enum AuthStatus
		{
			// Token: 0x040031D8 RID: 12760
			VALID = 1,
			// Token: 0x040031D9 RID: 12761
			ERROR_INTERNAL = -2,
			// Token: 0x040031DA RID: 12762
			ERROR_NOT_AUTHORIZED = -3,
			// Token: 0x040031DB RID: 12763
			ERROR_VERSION_UPDATE_REQUIRED = -4,
			// Token: 0x040031DC RID: 12764
			ERROR_TIMEOUT = -5
		}

		// Token: 0x0200068E RID: 1678
		internal enum UIStatus
		{
			// Token: 0x040031DE RID: 12766
			VALID = 1,
			// Token: 0x040031DF RID: 12767
			ERROR_INTERNAL = -2,
			// Token: 0x040031E0 RID: 12768
			ERROR_NOT_AUTHORIZED = -3,
			// Token: 0x040031E1 RID: 12769
			ERROR_VERSION_UPDATE_REQUIRED = -4,
			// Token: 0x040031E2 RID: 12770
			ERROR_TIMEOUT = -5,
			// Token: 0x040031E3 RID: 12771
			ERROR_CANCELED = -6,
			// Token: 0x040031E4 RID: 12772
			ERROR_UI_BUSY = -12,
			// Token: 0x040031E5 RID: 12773
			ERROR_LEFT_ROOM = -18
		}

		// Token: 0x0200068F RID: 1679
		internal enum MultiplayerStatus
		{
			// Token: 0x040031E7 RID: 12775
			VALID = 1,
			// Token: 0x040031E8 RID: 12776
			VALID_BUT_STALE,
			// Token: 0x040031E9 RID: 12777
			ERROR_INTERNAL = -2,
			// Token: 0x040031EA RID: 12778
			ERROR_NOT_AUTHORIZED = -3,
			// Token: 0x040031EB RID: 12779
			ERROR_VERSION_UPDATE_REQUIRED = -4,
			// Token: 0x040031EC RID: 12780
			ERROR_TIMEOUT = -5,
			// Token: 0x040031ED RID: 12781
			ERROR_MATCH_ALREADY_REMATCHED = -7,
			// Token: 0x040031EE RID: 12782
			ERROR_INACTIVE_MATCH = -8,
			// Token: 0x040031EF RID: 12783
			ERROR_INVALID_RESULTS = -9,
			// Token: 0x040031F0 RID: 12784
			ERROR_INVALID_MATCH = -10,
			// Token: 0x040031F1 RID: 12785
			ERROR_MATCH_OUT_OF_DATE = -11,
			// Token: 0x040031F2 RID: 12786
			ERROR_REAL_TIME_ROOM_NOT_JOINED = -17
		}

		// Token: 0x02000690 RID: 1680
		internal enum CommonErrorStatus
		{
			// Token: 0x040031F4 RID: 12788
			ERROR_INTERNAL = -2,
			// Token: 0x040031F5 RID: 12789
			ERROR_NOT_AUTHORIZED = -3,
			// Token: 0x040031F6 RID: 12790
			ERROR_TIMEOUT = -5
		}

		// Token: 0x02000691 RID: 1681
		internal enum SnapshotOpenStatus
		{
			// Token: 0x040031F8 RID: 12792
			VALID = 1,
			// Token: 0x040031F9 RID: 12793
			VALID_WITH_CONFLICT = 3,
			// Token: 0x040031FA RID: 12794
			ERROR_INTERNAL = -2,
			// Token: 0x040031FB RID: 12795
			ERROR_NOT_AUTHORIZED = -3,
			// Token: 0x040031FC RID: 12796
			ERROR_TIMEOUT = -5
		}
	}
}
