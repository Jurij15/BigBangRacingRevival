using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native
{
	// Token: 0x02000621 RID: 1569
	internal static class CallbackUtils
	{
		// Token: 0x06002E25 RID: 11813 RVA: 0x001C252C File Offset: 0x001C092C
		internal static Action<T> ToOnGameThread<T>(Action<T> toConvert)
		{
			if (toConvert == null)
			{
				return delegate
				{
				};
			}
			return delegate(T val)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					toConvert.Invoke(val);
				});
			};
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x001C256C File Offset: 0x001C096C
		internal static Action<T1, T2> ToOnGameThread<T1, T2>(Action<T1, T2> toConvert)
		{
			if (toConvert == null)
			{
				return delegate
				{
				};
			}
			return delegate(T1 val1, T2 val2)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					toConvert.Invoke(val1, val2);
				});
			};
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x001C25AC File Offset: 0x001C09AC
		internal static Action<T1, T2, T3> ToOnGameThread<T1, T2, T3>(Action<T1, T2, T3> toConvert)
		{
			if (toConvert == null)
			{
				return delegate
				{
				};
			}
			return delegate(T1 val1, T2 val2, T3 val3)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					toConvert.Invoke(val1, val2, val3);
				});
			};
		}
	}
}
