using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000726 RID: 1830
	internal class TurnBasedMatchConfigBuilder : BaseReferenceHolder
	{
		// Token: 0x060034FC RID: 13564 RVA: 0x001CF22A File Offset: 0x001CD62A
		private TurnBasedMatchConfigBuilder(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x001CF233 File Offset: 0x001CD633
		internal TurnBasedMatchConfigBuilder PopulateFromUIResponse(PlayerSelectUIResponse response)
		{
			TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_PopulateFromPlayerSelectUIResponse(base.SelfPtr(), response.AsPointer());
			return this;
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x001CF247 File Offset: 0x001CD647
		internal TurnBasedMatchConfigBuilder SetVariant(uint variant)
		{
			TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_SetVariant(base.SelfPtr(), variant);
			return this;
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x001CF256 File Offset: 0x001CD656
		internal TurnBasedMatchConfigBuilder AddInvitedPlayer(string playerId)
		{
			TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_AddPlayerToInvite(base.SelfPtr(), playerId);
			return this;
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x001CF265 File Offset: 0x001CD665
		internal TurnBasedMatchConfigBuilder SetExclusiveBitMask(ulong bitmask)
		{
			TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_SetExclusiveBitMask(base.SelfPtr(), bitmask);
			return this;
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x001CF274 File Offset: 0x001CD674
		internal TurnBasedMatchConfigBuilder SetMinimumAutomatchingPlayers(uint minimum)
		{
			TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_SetMinimumAutomatchingPlayers(base.SelfPtr(), minimum);
			return this;
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x001CF283 File Offset: 0x001CD683
		internal TurnBasedMatchConfigBuilder SetMaximumAutomatchingPlayers(uint maximum)
		{
			TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_SetMaximumAutomatchingPlayers(base.SelfPtr(), maximum);
			return this;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x001CF292 File Offset: 0x001CD692
		internal TurnBasedMatchConfig Build()
		{
			return new TurnBasedMatchConfig(TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_Create(base.SelfPtr()));
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x001CF2A4 File Offset: 0x001CD6A4
		protected override void CallDispose(HandleRef selfPointer)
		{
			TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_Dispose(selfPointer);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x001CF2AC File Offset: 0x001CD6AC
		internal static TurnBasedMatchConfigBuilder Create()
		{
			return new TurnBasedMatchConfigBuilder(TurnBasedMatchConfigBuilder.TurnBasedMatchConfig_Builder_Construct());
		}
	}
}
