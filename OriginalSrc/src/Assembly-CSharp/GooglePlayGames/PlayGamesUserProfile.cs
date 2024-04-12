using System;
using System.Collections;
using GooglePlayGames.OurUtils;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x02000605 RID: 1541
	public class PlayGamesUserProfile : IUserProfile
	{
		// Token: 0x06002D25 RID: 11557 RVA: 0x001BD4EC File Offset: 0x001BB8EC
		internal PlayGamesUserProfile(string displayName, string playerId, string avatarUrl)
		{
			this.mDisplayName = displayName;
			this.mPlayerId = playerId;
			this.mAvatarUrl = avatarUrl;
			this.mImageLoading = false;
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x001BD512 File Offset: 0x001BB912
		protected void ResetIdentity(string displayName, string playerId, string avatarUrl)
		{
			this.mDisplayName = displayName;
			this.mPlayerId = playerId;
			if (this.mAvatarUrl != avatarUrl)
			{
				this.mImage = null;
				this.mAvatarUrl = avatarUrl;
			}
			this.mImageLoading = false;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x001BD54A File Offset: 0x001BB94A
		public string userName
		{
			get
			{
				return this.mDisplayName;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x001BD552 File Offset: 0x001BB952
		public string id
		{
			get
			{
				return this.mPlayerId;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x001BD55A File Offset: 0x001BB95A
		public bool isFriend
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x001BD55D File Offset: 0x001BB95D
		public UserState state
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x001BD560 File Offset: 0x001BB960
		public Texture2D image
		{
			get
			{
				if (!this.mImageLoading && this.mImage == null && !string.IsNullOrEmpty(this.AvatarURL))
				{
					Debug.Log("Starting to load image: " + this.AvatarURL);
					this.mImageLoading = true;
					PlayGamesHelperObject.RunCoroutine(this.LoadImage());
				}
				return this.mImage;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x001BD5CA File Offset: 0x001BB9CA
		public string AvatarURL
		{
			get
			{
				return this.mAvatarUrl;
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x001BD5D4 File Offset: 0x001BB9D4
		internal IEnumerator LoadImage()
		{
			if (!string.IsNullOrEmpty(this.AvatarURL))
			{
				WWW www = new WWW(this.AvatarURL);
				while (!www.isDone)
				{
					yield return null;
				}
				if (www.error == null)
				{
					this.mImage = www.texture;
				}
				else
				{
					this.mImage = Texture2D.blackTexture;
					Debug.Log("Error downloading image: " + www.error);
				}
				this.mImageLoading = false;
			}
			else
			{
				Debug.Log("No URL found.");
				this.mImage = Texture2D.blackTexture;
				this.mImageLoading = false;
			}
			yield break;
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x001BD5F0 File Offset: 0x001BB9F0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			PlayGamesUserProfile playGamesUserProfile = obj as PlayGamesUserProfile;
			return playGamesUserProfile != null && StringComparer.Ordinal.Equals(this.mPlayerId, playGamesUserProfile.mPlayerId);
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x001BD638 File Offset: 0x001BBA38
		public override int GetHashCode()
		{
			return typeof(PlayGamesUserProfile).GetHashCode() ^ this.mPlayerId.GetHashCode();
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x001BD655 File Offset: 0x001BBA55
		public override string ToString()
		{
			return string.Format("[Player: '{0}' (id {1})]", this.mDisplayName, this.mPlayerId);
		}

		// Token: 0x0400314D RID: 12621
		private string mDisplayName;

		// Token: 0x0400314E RID: 12622
		private string mPlayerId;

		// Token: 0x0400314F RID: 12623
		private string mAvatarUrl;

		// Token: 0x04003150 RID: 12624
		private volatile bool mImageLoading;

		// Token: 0x04003151 RID: 12625
		private Texture2D mImage;
	}
}
