using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005EE RID: 1518
	public struct PlayGamesClientConfiguration
	{
		// Token: 0x06002C35 RID: 11317 RVA: 0x001BDE00 File Offset: 0x001BC200
		private PlayGamesClientConfiguration(PlayGamesClientConfiguration.Builder builder)
		{
			this.mEnableSavedGames = builder.HasEnableSaveGames();
			this.mInvitationDelegate = builder.GetInvitationDelegate();
			this.mMatchDelegate = builder.GetMatchDelegate();
			this.mScopes = builder.getScopes();
			this.mHidePopups = builder.IsHidingPopups();
			this.mRequestAuthCode = builder.IsRequestingAuthCode();
			this.mForceRefresh = builder.IsForcingRefresh();
			this.mRequestEmail = builder.IsRequestingEmail();
			this.mRequestIdToken = builder.IsRequestingIdToken();
			this.mAccountName = builder.GetAccountName();
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x001BDE85 File Offset: 0x001BC285
		public bool EnableSavedGames
		{
			get
			{
				return this.mEnableSavedGames;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x001BDE8D File Offset: 0x001BC28D
		public bool IsHidingPopups
		{
			get
			{
				return this.mHidePopups;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x001BDE95 File Offset: 0x001BC295
		public bool IsRequestingAuthCode
		{
			get
			{
				return this.mRequestAuthCode;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x001BDE9D File Offset: 0x001BC29D
		public bool IsForcingRefresh
		{
			get
			{
				return this.mForceRefresh;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x001BDEA5 File Offset: 0x001BC2A5
		public bool IsRequestingEmail
		{
			get
			{
				return this.mRequestEmail;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x001BDEAD File Offset: 0x001BC2AD
		public bool IsRequestingIdToken
		{
			get
			{
				return this.mRequestIdToken;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x001BDEB5 File Offset: 0x001BC2B5
		public string AccountName
		{
			get
			{
				return this.mAccountName;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x001BDEBD File Offset: 0x001BC2BD
		public string[] Scopes
		{
			get
			{
				return this.mScopes;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x001BDEC5 File Offset: 0x001BC2C5
		public InvitationReceivedDelegate InvitationDelegate
		{
			get
			{
				return this.mInvitationDelegate;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x001BDECD File Offset: 0x001BC2CD
		public MatchDelegate MatchDelegate
		{
			get
			{
				return this.mMatchDelegate;
			}
		}

		// Token: 0x040030D5 RID: 12501
		public static readonly PlayGamesClientConfiguration DefaultConfiguration = new PlayGamesClientConfiguration.Builder().Build();

		// Token: 0x040030D6 RID: 12502
		private readonly bool mEnableSavedGames;

		// Token: 0x040030D7 RID: 12503
		private readonly string[] mScopes;

		// Token: 0x040030D8 RID: 12504
		private readonly bool mRequestAuthCode;

		// Token: 0x040030D9 RID: 12505
		private readonly bool mForceRefresh;

		// Token: 0x040030DA RID: 12506
		private readonly bool mHidePopups;

		// Token: 0x040030DB RID: 12507
		private readonly bool mRequestEmail;

		// Token: 0x040030DC RID: 12508
		private readonly bool mRequestIdToken;

		// Token: 0x040030DD RID: 12509
		private readonly string mAccountName;

		// Token: 0x040030DE RID: 12510
		private readonly InvitationReceivedDelegate mInvitationDelegate;

		// Token: 0x040030DF RID: 12511
		private readonly MatchDelegate mMatchDelegate;

		// Token: 0x020005EF RID: 1519
		public class Builder
		{
			// Token: 0x06002C42 RID: 11330 RVA: 0x001BDF41 File Offset: 0x001BC341
			public PlayGamesClientConfiguration.Builder EnableSavedGames()
			{
				this.mEnableSaveGames = true;
				return this;
			}

			// Token: 0x06002C43 RID: 11331 RVA: 0x001BDF4B File Offset: 0x001BC34B
			public PlayGamesClientConfiguration.Builder EnableHidePopups()
			{
				this.mHidePopups = true;
				return this;
			}

			// Token: 0x06002C44 RID: 11332 RVA: 0x001BDF55 File Offset: 0x001BC355
			public PlayGamesClientConfiguration.Builder RequestServerAuthCode(bool forceRefresh)
			{
				this.mRequestAuthCode = true;
				this.mForceRefresh = forceRefresh;
				return this;
			}

			// Token: 0x06002C45 RID: 11333 RVA: 0x001BDF66 File Offset: 0x001BC366
			public PlayGamesClientConfiguration.Builder RequestEmail()
			{
				this.mRequestEmail = true;
				return this;
			}

			// Token: 0x06002C46 RID: 11334 RVA: 0x001BDF70 File Offset: 0x001BC370
			public PlayGamesClientConfiguration.Builder RequestIdToken()
			{
				this.mRequestIdToken = true;
				return this;
			}

			// Token: 0x06002C47 RID: 11335 RVA: 0x001BDF7A File Offset: 0x001BC37A
			public PlayGamesClientConfiguration.Builder SetAccountName(string accountName)
			{
				this.mAccountName = accountName;
				return this;
			}

			// Token: 0x06002C48 RID: 11336 RVA: 0x001BDF84 File Offset: 0x001BC384
			public PlayGamesClientConfiguration.Builder AddOauthScope(string scope)
			{
				if (this.mScopes == null)
				{
					this.mScopes = new List<string>();
				}
				this.mScopes.Add(scope);
				return this;
			}

			// Token: 0x06002C49 RID: 11337 RVA: 0x001BDFA9 File Offset: 0x001BC3A9
			public PlayGamesClientConfiguration.Builder WithInvitationDelegate(InvitationReceivedDelegate invitationDelegate)
			{
				this.mInvitationDelegate = Misc.CheckNotNull<InvitationReceivedDelegate>(invitationDelegate);
				return this;
			}

			// Token: 0x06002C4A RID: 11338 RVA: 0x001BDFB8 File Offset: 0x001BC3B8
			public PlayGamesClientConfiguration.Builder WithMatchDelegate(MatchDelegate matchDelegate)
			{
				this.mMatchDelegate = Misc.CheckNotNull<MatchDelegate>(matchDelegate);
				return this;
			}

			// Token: 0x06002C4B RID: 11339 RVA: 0x001BDFC7 File Offset: 0x001BC3C7
			public PlayGamesClientConfiguration Build()
			{
				return new PlayGamesClientConfiguration(this);
			}

			// Token: 0x06002C4C RID: 11340 RVA: 0x001BDFCF File Offset: 0x001BC3CF
			internal bool HasEnableSaveGames()
			{
				return this.mEnableSaveGames;
			}

			// Token: 0x06002C4D RID: 11341 RVA: 0x001BDFD7 File Offset: 0x001BC3D7
			internal bool IsRequestingAuthCode()
			{
				return this.mRequestAuthCode;
			}

			// Token: 0x06002C4E RID: 11342 RVA: 0x001BDFDF File Offset: 0x001BC3DF
			internal bool IsHidingPopups()
			{
				return this.mHidePopups;
			}

			// Token: 0x06002C4F RID: 11343 RVA: 0x001BDFE7 File Offset: 0x001BC3E7
			internal bool IsForcingRefresh()
			{
				return this.mForceRefresh;
			}

			// Token: 0x06002C50 RID: 11344 RVA: 0x001BDFEF File Offset: 0x001BC3EF
			internal bool IsRequestingEmail()
			{
				return this.mRequestEmail;
			}

			// Token: 0x06002C51 RID: 11345 RVA: 0x001BDFF7 File Offset: 0x001BC3F7
			internal bool IsRequestingIdToken()
			{
				return this.mRequestIdToken;
			}

			// Token: 0x06002C52 RID: 11346 RVA: 0x001BDFFF File Offset: 0x001BC3FF
			internal string GetAccountName()
			{
				return this.mAccountName;
			}

			// Token: 0x06002C53 RID: 11347 RVA: 0x001BE007 File Offset: 0x001BC407
			internal string[] getScopes()
			{
				return (this.mScopes != null) ? this.mScopes.ToArray() : new string[0];
			}

			// Token: 0x06002C54 RID: 11348 RVA: 0x001BE02A File Offset: 0x001BC42A
			internal MatchDelegate GetMatchDelegate()
			{
				return this.mMatchDelegate;
			}

			// Token: 0x06002C55 RID: 11349 RVA: 0x001BE032 File Offset: 0x001BC432
			internal InvitationReceivedDelegate GetInvitationDelegate()
			{
				return this.mInvitationDelegate;
			}

			// Token: 0x040030E0 RID: 12512
			private bool mEnableSaveGames;

			// Token: 0x040030E1 RID: 12513
			private List<string> mScopes;

			// Token: 0x040030E2 RID: 12514
			private bool mHidePopups;

			// Token: 0x040030E3 RID: 12515
			private bool mRequestAuthCode;

			// Token: 0x040030E4 RID: 12516
			private bool mForceRefresh;

			// Token: 0x040030E5 RID: 12517
			private bool mRequestEmail;

			// Token: 0x040030E6 RID: 12518
			private bool mRequestIdToken;

			// Token: 0x040030E7 RID: 12519
			private string mAccountName;

			// Token: 0x040030E8 RID: 12520
			private InvitationReceivedDelegate mInvitationDelegate = delegate
			{
			};

			// Token: 0x040030E9 RID: 12521
			private MatchDelegate mMatchDelegate = delegate
			{
			};
		}
	}
}
