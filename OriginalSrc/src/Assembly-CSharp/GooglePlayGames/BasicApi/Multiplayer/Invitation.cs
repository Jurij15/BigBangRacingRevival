using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x020005D4 RID: 1492
	public class Invitation
	{
		// Token: 0x06002B7B RID: 11131 RVA: 0x001BD18B File Offset: 0x001BB58B
		internal Invitation(Invitation.InvType invType, string invId, Participant inviter, int variant)
		{
			this.mInvitationType = invType;
			this.mInvitationId = invId;
			this.mInviter = inviter;
			this.mVariant = variant;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x001BD1B0 File Offset: 0x001BB5B0
		public Invitation.InvType InvitationType
		{
			get
			{
				return this.mInvitationType;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x001BD1B8 File Offset: 0x001BB5B8
		public string InvitationId
		{
			get
			{
				return this.mInvitationId;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x001BD1C0 File Offset: 0x001BB5C0
		public Participant Inviter
		{
			get
			{
				return this.mInviter;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x001BD1C8 File Offset: 0x001BB5C8
		public int Variant
		{
			get
			{
				return this.mVariant;
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x001BD1D0 File Offset: 0x001BB5D0
		public override string ToString()
		{
			return string.Format("[Invitation: InvitationType={0}, InvitationId={1}, Inviter={2}, Variant={3}]", new object[] { this.InvitationType, this.InvitationId, this.Inviter, this.Variant });
		}

		// Token: 0x04003074 RID: 12404
		private Invitation.InvType mInvitationType;

		// Token: 0x04003075 RID: 12405
		private string mInvitationId;

		// Token: 0x04003076 RID: 12406
		private Participant mInviter;

		// Token: 0x04003077 RID: 12407
		private int mVariant;

		// Token: 0x020005D5 RID: 1493
		public enum InvType
		{
			// Token: 0x04003079 RID: 12409
			RealTime,
			// Token: 0x0400307A RID: 12410
			TurnBased,
			// Token: 0x0400307B RID: 12411
			Unknown
		}
	}
}
