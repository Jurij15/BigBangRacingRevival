using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x020005DB RID: 1499
	public class Participant : IComparable<Participant>
	{
		// Token: 0x06002BB1 RID: 11185 RVA: 0x001BD36C File Offset: 0x001BB76C
		internal Participant(string displayName, string participantId, Participant.ParticipantStatus status, Player player, bool connectedToRoom)
		{
			this.mDisplayName = displayName;
			this.mParticipantId = participantId;
			this.mStatus = status;
			this.mPlayer = player;
			this.mIsConnectedToRoom = connectedToRoom;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x001BD3C1 File Offset: 0x001BB7C1
		public string DisplayName
		{
			get
			{
				return this.mDisplayName;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x001BD3C9 File Offset: 0x001BB7C9
		public string ParticipantId
		{
			get
			{
				return this.mParticipantId;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x001BD3D1 File Offset: 0x001BB7D1
		public Participant.ParticipantStatus Status
		{
			get
			{
				return this.mStatus;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x001BD3D9 File Offset: 0x001BB7D9
		public Player Player
		{
			get
			{
				return this.mPlayer;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x001BD3E1 File Offset: 0x001BB7E1
		public bool IsConnectedToRoom
		{
			get
			{
				return this.mIsConnectedToRoom;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x001BD3E9 File Offset: 0x001BB7E9
		public bool IsAutomatch
		{
			get
			{
				return this.mPlayer == null;
			}
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x001BD3F4 File Offset: 0x001BB7F4
		public override string ToString()
		{
			return string.Format("[Participant: '{0}' (id {1}), status={2}, player={3}, connected={4}]", new object[]
			{
				this.mDisplayName,
				this.mParticipantId,
				this.mStatus.ToString(),
				(this.mPlayer != null) ? this.mPlayer.ToString() : "NULL",
				this.mIsConnectedToRoom
			});
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x001BD468 File Offset: 0x001BB868
		public int CompareTo(Participant other)
		{
			return this.mParticipantId.CompareTo(other.mParticipantId);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x001BD47C File Offset: 0x001BB87C
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
			if (obj.GetType() != typeof(Participant))
			{
				return false;
			}
			Participant participant = (Participant)obj;
			return this.mParticipantId.Equals(participant.mParticipantId);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x001BD4CE File Offset: 0x001BB8CE
		public override int GetHashCode()
		{
			return (this.mParticipantId == null) ? 0 : this.mParticipantId.GetHashCode();
		}

		// Token: 0x04003086 RID: 12422
		private string mDisplayName = string.Empty;

		// Token: 0x04003087 RID: 12423
		private string mParticipantId = string.Empty;

		// Token: 0x04003088 RID: 12424
		private Participant.ParticipantStatus mStatus = Participant.ParticipantStatus.Unknown;

		// Token: 0x04003089 RID: 12425
		private Player mPlayer;

		// Token: 0x0400308A RID: 12426
		private bool mIsConnectedToRoom;

		// Token: 0x020005DC RID: 1500
		public enum ParticipantStatus
		{
			// Token: 0x0400308C RID: 12428
			NotInvitedYet,
			// Token: 0x0400308D RID: 12429
			Invited,
			// Token: 0x0400308E RID: 12430
			Joined,
			// Token: 0x0400308F RID: 12431
			Declined,
			// Token: 0x04003090 RID: 12432
			Left,
			// Token: 0x04003091 RID: 12433
			Finished,
			// Token: 0x04003092 RID: 12434
			Unresponsive,
			// Token: 0x04003093 RID: 12435
			Unknown
		}
	}
}
