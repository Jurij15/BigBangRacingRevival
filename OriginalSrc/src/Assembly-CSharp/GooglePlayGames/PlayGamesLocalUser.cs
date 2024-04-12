using System;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x02000602 RID: 1538
	public class PlayGamesLocalUser : PlayGamesUserProfile, ILocalUser, IUserProfile
	{
		// Token: 0x06002CC8 RID: 11464 RVA: 0x001BE9A2 File Offset: 0x001BCDA2
		internal PlayGamesLocalUser(PlayGamesPlatform plaf)
			: base("localUser", string.Empty, string.Empty)
		{
			this.mPlatform = plaf;
			this.emailAddress = null;
			this.mStats = null;
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x001BE9CE File Offset: 0x001BCDCE
		public void Authenticate(Action<bool> callback)
		{
			this.mPlatform.Authenticate(callback);
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x001BE9DC File Offset: 0x001BCDDC
		public void Authenticate(Action<bool, string> callback)
		{
			this.mPlatform.Authenticate(callback);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x001BE9EA File Offset: 0x001BCDEA
		public void Authenticate(Action<bool> callback, bool silent)
		{
			this.mPlatform.Authenticate(callback, silent);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x001BE9F9 File Offset: 0x001BCDF9
		public void Authenticate(Action<bool, string> callback, bool silent)
		{
			this.mPlatform.Authenticate(callback, silent);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x001BEA08 File Offset: 0x001BCE08
		public void LoadFriends(Action<bool> callback)
		{
			this.mPlatform.LoadFriends(this, callback);
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x001BEA17 File Offset: 0x001BCE17
		public IUserProfile[] friends
		{
			get
			{
				return this.mPlatform.GetFriends();
			}
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x001BEA24 File Offset: 0x001BCE24
		public string GetIdToken()
		{
			return this.mPlatform.GetIdToken();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x001BEA31 File Offset: 0x001BCE31
		public bool authenticated
		{
			get
			{
				return this.mPlatform.IsAuthenticated();
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x001BEA3E File Offset: 0x001BCE3E
		public bool underage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x001BEA44 File Offset: 0x001BCE44
		public new string userName
		{
			get
			{
				string text = string.Empty;
				if (this.authenticated)
				{
					text = this.mPlatform.GetUserDisplayName();
					if (!base.userName.Equals(text))
					{
						base.ResetIdentity(text, this.mPlatform.GetUserId(), this.mPlatform.GetUserImageUrl());
					}
				}
				return text;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x001BEAA0 File Offset: 0x001BCEA0
		public new string id
		{
			get
			{
				string text = string.Empty;
				if (this.authenticated)
				{
					text = this.mPlatform.GetUserId();
					if (!base.id.Equals(text))
					{
						base.ResetIdentity(this.mPlatform.GetUserDisplayName(), text, this.mPlatform.GetUserImageUrl());
					}
				}
				return text;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x001BEAF9 File Offset: 0x001BCEF9
		public new bool isFriend
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x001BEAFC File Offset: 0x001BCEFC
		public new UserState state
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x001BEB00 File Offset: 0x001BCF00
		public new string AvatarURL
		{
			get
			{
				string text = string.Empty;
				if (this.authenticated)
				{
					text = this.mPlatform.GetUserImageUrl();
					if (!base.id.Equals(text))
					{
						base.ResetIdentity(this.mPlatform.GetUserDisplayName(), this.mPlatform.GetUserId(), text);
					}
				}
				return text;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x001BEB5C File Offset: 0x001BCF5C
		public string Email
		{
			get
			{
				if (this.authenticated && string.IsNullOrEmpty(this.emailAddress))
				{
					this.emailAddress = this.mPlatform.GetUserEmail();
					this.emailAddress = this.emailAddress ?? string.Empty;
				}
				return (!this.authenticated) ? string.Empty : this.emailAddress;
			}
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x001BEBC8 File Offset: 0x001BCFC8
		public void GetStats(Action<CommonStatusCodes, GooglePlayGames.BasicApi.PlayerStats> callback)
		{
			if (this.mStats == null || !this.mStats.Valid)
			{
				this.mPlatform.GetPlayerStats(delegate(CommonStatusCodes rc, GooglePlayGames.BasicApi.PlayerStats stats)
				{
					this.mStats = stats;
					callback.Invoke(rc, stats);
				});
			}
			else
			{
				callback.Invoke(CommonStatusCodes.Success, this.mStats);
			}
		}

		// Token: 0x0400313C RID: 12604
		internal PlayGamesPlatform mPlatform;

		// Token: 0x0400313D RID: 12605
		private string emailAddress;

		// Token: 0x0400313E RID: 12606
		private GooglePlayGames.BasicApi.PlayerStats mStats;
	}
}
