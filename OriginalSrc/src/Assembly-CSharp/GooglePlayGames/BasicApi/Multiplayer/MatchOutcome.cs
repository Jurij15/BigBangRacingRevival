using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x020005D9 RID: 1497
	public class MatchOutcome
	{
		// Token: 0x06002BAA RID: 11178 RVA: 0x001BD239 File Offset: 0x001BB639
		public void SetParticipantResult(string participantId, MatchOutcome.ParticipantResult result, uint placement)
		{
			if (!this.mParticipantIds.Contains(participantId))
			{
				this.mParticipantIds.Add(participantId);
			}
			this.mPlacements[participantId] = placement;
			this.mResults[participantId] = result;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x001BD272 File Offset: 0x001BB672
		public void SetParticipantResult(string participantId, MatchOutcome.ParticipantResult result)
		{
			this.SetParticipantResult(participantId, result, 0U);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x001BD27D File Offset: 0x001BB67D
		public void SetParticipantResult(string participantId, uint placement)
		{
			this.SetParticipantResult(participantId, MatchOutcome.ParticipantResult.Unset, placement);
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x001BD288 File Offset: 0x001BB688
		public List<string> ParticipantIds
		{
			get
			{
				return this.mParticipantIds;
			}
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x001BD290 File Offset: 0x001BB690
		public MatchOutcome.ParticipantResult GetResultFor(string participantId)
		{
			return (!this.mResults.ContainsKey(participantId)) ? MatchOutcome.ParticipantResult.Unset : this.mResults[participantId];
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x001BD2B5 File Offset: 0x001BB6B5
		public uint GetPlacementFor(string participantId)
		{
			return (!this.mPlacements.ContainsKey(participantId)) ? 0U : this.mPlacements[participantId];
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x001BD2DC File Offset: 0x001BB6DC
		public override string ToString()
		{
			string text = "[MatchOutcome";
			foreach (string text2 in this.mParticipantIds)
			{
				text += string.Format(" {0}->({1},{2})", text2, this.GetResultFor(text2), this.GetPlacementFor(text2));
			}
			return text + "]";
		}

		// Token: 0x0400307C RID: 12412
		public const uint PlacementUnset = 0U;

		// Token: 0x0400307D RID: 12413
		private List<string> mParticipantIds = new List<string>();

		// Token: 0x0400307E RID: 12414
		private Dictionary<string, uint> mPlacements = new Dictionary<string, uint>();

		// Token: 0x0400307F RID: 12415
		private Dictionary<string, MatchOutcome.ParticipantResult> mResults = new Dictionary<string, MatchOutcome.ParticipantResult>();

		// Token: 0x020005DA RID: 1498
		public enum ParticipantResult
		{
			// Token: 0x04003081 RID: 12417
			Unset = -1,
			// Token: 0x04003082 RID: 12418
			None,
			// Token: 0x04003083 RID: 12419
			Win,
			// Token: 0x04003084 RID: 12420
			Loss,
			// Token: 0x04003085 RID: 12421
			Tie
		}
	}
}
