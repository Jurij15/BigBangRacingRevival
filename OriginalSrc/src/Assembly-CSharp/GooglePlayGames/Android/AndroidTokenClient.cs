using System;
using System.Collections.Generic;
using Com.Google.Android.Gms.Common.Api;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x0200060C RID: 1548
	internal class AndroidTokenClient : TokenClient
	{
		// Token: 0x06002D5B RID: 11611 RVA: 0x001C0A10 File Offset: 0x001BEE10
		public static AndroidJavaObject GetActivity()
		{
			AndroidJavaObject @static;
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				@static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			return @static;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x001C0A58 File Offset: 0x001BEE58
		public void SetRequestAuthCode(bool flag, bool forceRefresh)
		{
			this.requestAuthCode = flag;
			this.forceRefresh = forceRefresh;
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x001C0A68 File Offset: 0x001BEE68
		public void SetRequestEmail(bool flag)
		{
			this.requestEmail = flag;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x001C0A71 File Offset: 0x001BEE71
		public void SetRequestIdToken(bool flag)
		{
			this.requestIdToken = flag;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x001C0A7A File Offset: 0x001BEE7A
		public void SetWebClientId(string webClientId)
		{
			this.webClientId = webClientId;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x001C0A83 File Offset: 0x001BEE83
		public void SetHidePopups(bool flag)
		{
			this.hidePopups = flag;
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x001C0A8C File Offset: 0x001BEE8C
		public void SetAccountName(string accountName)
		{
			this.accountName = accountName;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x001C0A95 File Offset: 0x001BEE95
		public void AddOauthScopes(string[] scopes)
		{
			if (scopes != null)
			{
				if (this.oauthScopes == null)
				{
					this.oauthScopes = new List<string>();
				}
				this.oauthScopes.AddRange(scopes);
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x001C0ABF File Offset: 0x001BEEBF
		public void Signout()
		{
			this.authCode = null;
			this.email = null;
			this.idToken = null;
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				Debug.Log("Calling Signout in token client");
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.games.bridge.TokenFragment");
				androidJavaClass.CallStatic("signOut", new object[] { AndroidTokenClient.GetActivity() });
			});
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x001C0AF8 File Offset: 0x001BEEF8
		public bool NeedsToRun()
		{
			return this.requestAuthCode || this.requestEmail || this.requestIdToken;
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x001C0B1C File Offset: 0x001BEF1C
		public void FetchTokens(Action<int> callback)
		{
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				this.DoFetchToken(callback);
			});
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x001C0B50 File Offset: 0x001BEF50
		internal void DoFetchToken(Action<int> callback)
		{
			object[] array = new object[9];
			jvalue[] array2 = AndroidJNIHelper.CreateJNIArgArray(array);
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.games.bridge.TokenFragment"))
				{
					using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
					{
						IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(androidJavaClass.GetRawClass(), "fetchToken", "(Landroid/app/Activity;ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;");
						array2[0].l = activity.GetRawObject();
						array2[1].z = this.requestAuthCode;
						array2[2].z = this.requestEmail;
						array2[3].z = this.requestIdToken;
						array2[4].l = AndroidJNI.NewStringUTF(this.webClientId);
						array2[5].z = this.forceRefresh;
						array2[6].l = AndroidJNIHelper.ConvertToJNIArray(this.oauthScopes.ToArray());
						array2[7].z = this.hidePopups;
						array2[8].l = AndroidJNI.NewStringUTF(this.accountName);
						IntPtr intPtr = AndroidJNI.CallStaticObjectMethod(androidJavaClass.GetRawClass(), staticMethodID, array2);
						PendingResult<TokenResult> pendingResult = new PendingResult<TokenResult>(intPtr);
						pendingResult.setResultCallback(new TokenResultCallback(delegate(int rc, string authCode, string email, string idToken)
						{
							this.authCode = authCode;
							this.email = email;
							this.idToken = idToken;
							callback.Invoke(rc);
						}));
					}
				}
			}
			catch (Exception ex)
			{
				Logger.e("Exception launching token request: " + ex.Message);
				Logger.e(ex.ToString());
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(array, array2);
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x001C0D54 File Offset: 0x001BF154
		public string GetEmail()
		{
			return this.email;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x001C0D5C File Offset: 0x001BF15C
		public string GetAuthCode()
		{
			return this.authCode;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x001C0D64 File Offset: 0x001BF164
		public void GetAnotherServerAuthCode(bool reAuthenticateIfNeeded, Action<string> callback)
		{
			object[] array = new object[3];
			jvalue[] array2 = AndroidJNIHelper.CreateJNIArgArray(array);
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.games.bridge.TokenFragment"))
				{
					using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
					{
						IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(androidJavaClass.GetRawClass(), "getAnotherAuthCode", "(Landroid/app/Activity;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;");
						array2[0].l = activity.GetRawObject();
						array2[1].z = reAuthenticateIfNeeded;
						array2[2].l = AndroidJNI.NewStringUTF(this.webClientId);
						IntPtr intPtr = AndroidJNI.CallStaticObjectMethod(androidJavaClass.GetRawClass(), staticMethodID, array2);
						PendingResult<TokenResult> pendingResult = new PendingResult<TokenResult>(intPtr);
						pendingResult.setResultCallback(new TokenResultCallback(delegate(int rc, string authCode, string email, string idToken)
						{
							this.authCode = authCode;
							callback.Invoke(authCode);
						}));
					}
				}
			}
			catch (Exception ex)
			{
				Logger.e("Exception launching auth code request: " + ex.Message);
				Logger.e(ex.ToString());
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(array, array2);
			}
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x001C0EB4 File Offset: 0x001BF2B4
		public string GetIdToken()
		{
			return this.idToken;
		}

		// Token: 0x04003162 RID: 12642
		private const string TokenFragmentClass = "com.google.games.bridge.TokenFragment";

		// Token: 0x04003163 RID: 12643
		private const string FetchTokenSignature = "(Landroid/app/Activity;ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;";

		// Token: 0x04003164 RID: 12644
		private const string FetchTokenMethod = "fetchToken";

		// Token: 0x04003165 RID: 12645
		private const string GetAnotherAuthCodeMethod = "getAnotherAuthCode";

		// Token: 0x04003166 RID: 12646
		private const string GetAnotherAuthCodeSignature = "(Landroid/app/Activity;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;";

		// Token: 0x04003167 RID: 12647
		private bool requestEmail;

		// Token: 0x04003168 RID: 12648
		private bool requestAuthCode;

		// Token: 0x04003169 RID: 12649
		private bool requestIdToken;

		// Token: 0x0400316A RID: 12650
		private List<string> oauthScopes;

		// Token: 0x0400316B RID: 12651
		private string webClientId;

		// Token: 0x0400316C RID: 12652
		private bool forceRefresh;

		// Token: 0x0400316D RID: 12653
		private bool hidePopups;

		// Token: 0x0400316E RID: 12654
		private string accountName;

		// Token: 0x0400316F RID: 12655
		private string email;

		// Token: 0x04003170 RID: 12656
		private string authCode;

		// Token: 0x04003171 RID: 12657
		private string idToken;
	}
}
