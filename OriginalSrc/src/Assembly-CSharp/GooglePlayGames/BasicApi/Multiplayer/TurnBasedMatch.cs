using System;
using System.Collections.Generic;
using System.Linq;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x020005DF RID: 1503
	public class TurnBasedMatch
	{
		// Token: 0x06002BC4 RID: 11204 RVA: 0x001BD7D0 File Offset: 0x001BBBD0
		internal TurnBasedMatch(string matchId, byte[] data, bool canRematch, string selfParticipantId, List<Participant> participants, uint availableAutomatchSlots, string pendingParticipantId, TurnBasedMatch.MatchTurnStatus turnStatus, TurnBasedMatch.MatchStatus matchStatus, uint variant, uint version)
		{
			this.mMatchId = matchId;
			this.mData = data;
			this.mCanRematch = canRematch;
			this.mSelfParticipantId = selfParticipantId;
			this.mParticipants = participants;
			this.mParticipants.Sort();
			this.mAvailableAutomatchSlots = availableAutomatchSlots;
			this.mPendingParticipantId = pendingParticipantId;
			this.mTurnStatus = turnStatus;
			this.mMatchStatus = matchStatus;
			this.mVariant = variant;
			this.mVersion = version;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x001BD843 File Offset: 0x001BBC43
		public string MatchId
		{
			get
			{
				return this.mMatchId;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x001BD84B File Offset: 0x001BBC4B
		public byte[] Data
		{
			get
			{
				return this.mData;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x001BD853 File Offset: 0x001BBC53
		public bool CanRematch
		{
			get
			{
				return this.mCanRematch;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x001BD85B File Offset: 0x001BBC5B
		public string SelfParticipantId
		{
			get
			{
				return this.mSelfParticipantId;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x001BD863 File Offset: 0x001BBC63
		public Participant Self
		{
			get
			{
				return this.GetParticipant(this.mSelfParticipantId);
			}
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x001BD874 File Offset: 0x001BBC74
		public Participant GetParticipant(string participantId)
		{
			foreach (Participant participant in this.mParticipants)
			{
				if (participant.ParticipantId.Equals(participantId))
				{
					return participant;
				}
			}
			Logger.w("Participant not found in turn-based match: " + participantId);
			return null;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x001BD8F4 File Offset: 0x001BBCF4
		public List<Participant> Participants
		{
			get
			{
				return this.mParticipants;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x001BD8FC File Offset: 0x001BBCFC
		public string PendingParticipantId
		{
			get
			{
				return this.mPendingParticipantId;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x001BD904 File Offset: 0x001BBD04
		public Participant PendingParticipant
		{
			get
			{
				return (this.mPendingParticipantId != null) ? this.GetParticipant(this.mPendingParticipantId) : null;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x001BD923 File Offset: 0x001BBD23
		public TurnBasedMatch.MatchTurnStatus TurnStatus
		{
			get
			{
				return this.mTurnStatus;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x001BD92B File Offset: 0x001BBD2B
		public TurnBasedMatch.MatchStatus Status
		{
			get
			{
				return this.mMatchStatus;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x001BD933 File Offset: 0x001BBD33
		public uint Variant
		{
			get
			{
				return this.mVariant;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x001BD93B File Offset: 0x001BBD3B
		public uint Version
		{
			get
			{
				return this.mVersion;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x001BD943 File Offset: 0x001BBD43
		public uint AvailableAutomatchSlots
		{
			get
			{
				return this.mAvailableAutomatchSlots;
			}
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x001BD94C File Offset: 0x001BBD4C
		public override string ToString()
		{
			string text = "[TurnBasedMatch: mMatchId={0}, mData={1}, mCanRematch={2}, mSelfParticipantId={3}, mParticipants={4}, mPendingParticipantId={5}, mTurnStatus={6}, mMatchStatus={7}, mVariant={8}, mVersion={9}]";
			object[] array = new object[10];
			array[0] = this.mMatchId;
			array[1] = this.mData;
			array[2] = this.mCanRematch;
			array[3] = this.mSelfParticipantId;
			array[4] = string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<Participant, string>(this.mParticipants, (Participant p) => p.ToString())));
			array[5] = this.mPendingParticipantId;
			array[6] = this.mTurnStatus;
			array[7] = this.mMatchStatus;
			array[8] = this.mVariant;
			array[9] = this.mVersion;
			return string.Format(text, array);
		}

		// Token: 0x04003094 RID: 12436
		private string mMatchId;

		// Token: 0x04003095 RID: 12437
		private byte[] mData;

		// Token: 0x04003096 RID: 12438
		private bool mCanRematch;

		// Token: 0x04003097 RID: 12439
		private uint mAvailableAutomatchSlots;

		// Token: 0x04003098 RID: 12440
		private string mSelfParticipantId;

		// Token: 0x04003099 RID: 12441
		private List<Participant> mParticipants;

		// Token: 0x0400309A RID: 12442
		private string mPendingParticipantId;

		// Token: 0x0400309B RID: 12443
		private TurnBasedMatch.MatchTurnStatus mTurnStatus;

		// Token: 0x0400309C RID: 12444
		private TurnBasedMatch.MatchStatus mMatchStatus;

		// Token: 0x0400309D RID: 12445
		private uint mVariant;

		// Token: 0x0400309E RID: 12446
		private uint mVersion;

		// Token: 0x020005E0 RID: 1504
		public enum MatchStatus
		{
			// Token: 0x040030A1 RID: 12449
			Active,
			// Token: 0x040030A2 RID: 12450
			AutoMatching,
			// Token: 0x040030A3 RID: 12451
			Cancelled,
			// Token: 0x040030A4 RID: 12452
			Complete,
			// Token: 0x040030A5 RID: 12453
			Expired,
			// Token: 0x040030A6 RID: 12454
			Unknown,
			// Token: 0x040030A7 RID: 12455
			Deleted
		}

		// Token: 0x020005E1 RID: 1505
		public enum MatchTurnStatus
		{
			// Token: 0x040030A9 RID: 12457
			Complete,
			// Token: 0x040030AA RID: 12458
			Invited,
			// Token: 0x040030AB RID: 12459
			MyTurn,
			// Token: 0x040030AC RID: 12460
			TheirTurn,
			// Token: 0x040030AD RID: 12461
			Unknown
		}
	}
}
