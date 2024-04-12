using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000705 RID: 1797
	internal class ParticipantResults : BaseReferenceHolder
	{
		// Token: 0x060033FB RID: 13307 RVA: 0x001CCF3A File Offset: 0x001CB33A
		internal ParticipantResults(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x001CCF43 File Offset: 0x001CB343
		internal bool HasResultsForParticipant(string participantId)
		{
			return ParticipantResults.ParticipantResults_HasResultsForParticipant(base.SelfPtr(), participantId);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x001CCF51 File Offset: 0x001CB351
		internal uint PlacingForParticipant(string participantId)
		{
			return ParticipantResults.ParticipantResults_PlaceForParticipant(base.SelfPtr(), participantId);
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x001CCF5F File Offset: 0x001CB35F
		internal Types.MatchResult ResultsForParticipant(string participantId)
		{
			return ParticipantResults.ParticipantResults_MatchResultForParticipant(base.SelfPtr(), participantId);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x001CCF6D File Offset: 0x001CB36D
		internal ParticipantResults WithResult(string participantId, uint placing, Types.MatchResult result)
		{
			return new ParticipantResults(ParticipantResults.ParticipantResults_WithResult(base.SelfPtr(), participantId, placing, result));
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x001CCF82 File Offset: 0x001CB382
		protected override void CallDispose(HandleRef selfPointer)
		{
			ParticipantResults.ParticipantResults_Dispose(selfPointer);
		}
	}
}
