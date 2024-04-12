using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000717 RID: 1815
	internal class RealtimeRoomConfigBuilder : BaseReferenceHolder
	{
		// Token: 0x0600347C RID: 13436 RVA: 0x001CE200 File Offset: 0x001CC600
		internal RealtimeRoomConfigBuilder(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x001CE209 File Offset: 0x001CC609
		internal RealtimeRoomConfigBuilder PopulateFromUIResponse(PlayerSelectUIResponse response)
		{
			RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_PopulateFromPlayerSelectUIResponse(base.SelfPtr(), response.AsPointer());
			return this;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x001CE220 File Offset: 0x001CC620
		internal RealtimeRoomConfigBuilder SetVariant(uint variantValue)
		{
			uint num = ((variantValue != 0U) ? variantValue : uint.MaxValue);
			RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_SetVariant(base.SelfPtr(), num);
			return this;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x001CE248 File Offset: 0x001CC648
		internal RealtimeRoomConfigBuilder AddInvitedPlayer(string playerId)
		{
			RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_AddPlayerToInvite(base.SelfPtr(), playerId);
			return this;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x001CE257 File Offset: 0x001CC657
		internal RealtimeRoomConfigBuilder SetExclusiveBitMask(ulong bitmask)
		{
			RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_SetExclusiveBitMask(base.SelfPtr(), bitmask);
			return this;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x001CE266 File Offset: 0x001CC666
		internal RealtimeRoomConfigBuilder SetMinimumAutomatchingPlayers(uint minimum)
		{
			RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_SetMinimumAutomatchingPlayers(base.SelfPtr(), minimum);
			return this;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x001CE275 File Offset: 0x001CC675
		internal RealtimeRoomConfigBuilder SetMaximumAutomatchingPlayers(uint maximum)
		{
			RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_SetMaximumAutomatchingPlayers(base.SelfPtr(), maximum);
			return this;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x001CE284 File Offset: 0x001CC684
		internal RealtimeRoomConfig Build()
		{
			return new RealtimeRoomConfig(RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_Create(base.SelfPtr()));
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x001CE296 File Offset: 0x001CC696
		protected override void CallDispose(HandleRef selfPointer)
		{
			RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_Dispose(selfPointer);
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x001CE29E File Offset: 0x001CC69E
		internal static RealtimeRoomConfigBuilder Create()
		{
			return new RealtimeRoomConfigBuilder(RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_Construct());
		}
	}
}
