using System;
using Com.Google.Android.Gms.Common.Api;
using Com.Google.Android.Gms.Games;
using Com.Google.Android.Gms.Games.Stats;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x0200060A RID: 1546
	internal class AndroidClient : IClientImpl
	{
		// Token: 0x06002D50 RID: 11600 RVA: 0x001C04E8 File Offset: 0x001BE8E8
		public PlatformConfiguration CreatePlatformConfiguration(PlayGamesClientConfiguration clientConfig)
		{
			AndroidPlatformConfiguration androidPlatformConfiguration = AndroidPlatformConfiguration.Create();
			using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
			{
				androidPlatformConfiguration.SetActivity(activity.GetRawObject());
				androidPlatformConfiguration.SetOptionalIntentHandlerForUI(delegate(IntPtr intent)
				{
					IntPtr intentRef = AndroidJNI.NewGlobalRef(intent);
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						try
						{
							AndroidClient.LaunchBridgeIntent(intentRef);
						}
						finally
						{
							AndroidJNI.DeleteGlobalRef(intentRef);
						}
					});
				});
				if (clientConfig.IsHidingPopups)
				{
					androidPlatformConfiguration.SetOptionalViewForPopups(this.CreateHiddenView(activity.GetRawObject()));
				}
			}
			return androidPlatformConfiguration;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x001C0574 File Offset: 0x001BE974
		public TokenClient CreateTokenClient(bool reset)
		{
			if (this.tokenClient == null)
			{
				this.tokenClient = new AndroidTokenClient();
			}
			else if (reset)
			{
				this.tokenClient.Signout();
			}
			return this.tokenClient;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x001C05A8 File Offset: 0x001BE9A8
		private IntPtr CreateHiddenView(IntPtr activity)
		{
			if (AndroidClient.invisible == null || AndroidClient.invisible.GetRawObject() == IntPtr.Zero)
			{
				AndroidClient.invisible = new AndroidJavaObject("android.view.View", new object[] { activity });
				AndroidClient.invisible.Call("setVisibility", new object[] { 4 });
				AndroidClient.invisible.Call("setClickable", new object[] { false });
			}
			return AndroidClient.invisible.GetRawObject();
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x001C063C File Offset: 0x001BEA3C
		private static void LaunchBridgeIntent(IntPtr bridgedIntent)
		{
			object[] array = new object[2];
			jvalue[] array2 = AndroidJNIHelper.CreateJNIArgArray(array);
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.games.bridge.NativeBridgeActivity"))
				{
					using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
					{
						IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(androidJavaClass.GetRawClass(), "launchBridgeIntent", "(Landroid/app/Activity;Landroid/content/Intent;)V");
						array2[0].l = activity.GetRawObject();
						array2[1].l = bridgedIntent;
						AndroidJNI.CallStaticVoidMethod(androidJavaClass.GetRawClass(), staticMethodID, array2);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.e("Exception launching bridge intent: " + ex.Message);
				Logger.e(ex.ToString());
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(array, array2);
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x001C073C File Offset: 0x001BEB3C
		public void Signout()
		{
			if (this.tokenClient != null)
			{
				this.tokenClient.Signout();
			}
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x001C0754 File Offset: 0x001BEB54
		public void GetPlayerStats(IntPtr apiClient, Action<CommonStatusCodes, GooglePlayGames.BasicApi.PlayerStats> callback)
		{
			GoogleApiClient googleApiClient = new GoogleApiClient(apiClient);
			AndroidClient.StatsResultCallback statsResultCallback;
			try
			{
				statsResultCallback = new AndroidClient.StatsResultCallback(delegate(int result, Com.Google.Android.Gms.Games.Stats.PlayerStats stats)
				{
					Debug.Log("Result for getStats: " + result);
					GooglePlayGames.BasicApi.PlayerStats playerStats = null;
					if (stats != null)
					{
						playerStats = new GooglePlayGames.BasicApi.PlayerStats();
						playerStats.AvgSessonLength = stats.getAverageSessionLength();
						playerStats.DaysSinceLastPlayed = stats.getDaysSinceLastPlayed();
						playerStats.NumberOfPurchases = stats.getNumberOfPurchases();
						playerStats.NumberOfSessions = stats.getNumberOfSessions();
						playerStats.SessPercentile = stats.getSessionPercentile();
						playerStats.SpendPercentile = stats.getSpendPercentile();
						playerStats.ChurnProbability = stats.getChurnProbability();
						playerStats.SpendProbability = stats.getSpendProbability();
						playerStats.HighSpenderProbability = stats.getHighSpenderProbability();
						playerStats.TotalSpendNext28Days = stats.getTotalSpendNext28Days();
					}
					callback.Invoke((CommonStatusCodes)result, playerStats);
				});
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				callback.Invoke(CommonStatusCodes.DeveloperError, null);
				return;
			}
			PendingResult<Stats_LoadPlayerStatsResultObject> pendingResult = Games.Stats.loadPlayerStats(googleApiClient, true);
			pendingResult.setResultCallback(statsResultCallback);
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x001C07CC File Offset: 0x001BEBCC
		public void SetGravityForPopups(IntPtr apiClient, Gravity gravity)
		{
			GoogleApiClient googleApiClient = new GoogleApiClient(apiClient);
			Games.setGravityForPopups(googleApiClient, (int)(gravity | Gravity.CENTER_HORIZONTAL));
		}

		// Token: 0x0400315B RID: 12635
		internal const string BridgeActivityClass = "com.google.games.bridge.NativeBridgeActivity";

		// Token: 0x0400315C RID: 12636
		private const string LaunchBridgeMethod = "launchBridgeIntent";

		// Token: 0x0400315D RID: 12637
		private const string LaunchBridgeSignature = "(Landroid/app/Activity;Landroid/content/Intent;)V";

		// Token: 0x0400315E RID: 12638
		private TokenClient tokenClient;

		// Token: 0x0400315F RID: 12639
		private static AndroidJavaObject invisible;

		// Token: 0x0200060B RID: 1547
		private class StatsResultCallback : ResultCallbackProxy<Stats_LoadPlayerStatsResultObject>
		{
			// Token: 0x06002D58 RID: 11608 RVA: 0x001C08D7 File Offset: 0x001BECD7
			public StatsResultCallback(Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats> callback)
			{
				this.callback = callback;
			}

			// Token: 0x06002D59 RID: 11609 RVA: 0x001C08E6 File Offset: 0x001BECE6
			public override void OnResult(Stats_LoadPlayerStatsResultObject arg_Result_1)
			{
				this.callback.Invoke(arg_Result_1.getStatus().getStatusCode(), arg_Result_1.getPlayerStats());
			}

			// Token: 0x04003161 RID: 12641
			private Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats> callback;
		}
	}
}
