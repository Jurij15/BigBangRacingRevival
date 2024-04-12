using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000725 RID: 1829
	internal class TurnBasedMatchConfig : BaseReferenceHolder
	{
		// Token: 0x060034F4 RID: 13556 RVA: 0x001CF171 File Offset: 0x001CD571
		internal TurnBasedMatchConfig(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x001CF17C File Offset: 0x001CD57C
		private string PlayerIdAtIndex(UIntPtr index)
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => TurnBasedMatchConfig.TurnBasedMatchConfig_PlayerIdsToInvite_GetElement(this.SelfPtr(), index, out_string, size));
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x001CF1AE File Offset: 0x001CD5AE
		internal IEnumerator<string> PlayerIdsToInvite()
		{
			return PInvokeUtilities.ToEnumerator<string>(TurnBasedMatchConfig.TurnBasedMatchConfig_PlayerIdsToInvite_Length(base.SelfPtr()), new Func<UIntPtr, string>(this.PlayerIdAtIndex));
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x001CF1CC File Offset: 0x001CD5CC
		internal uint Variant()
		{
			return TurnBasedMatchConfig.TurnBasedMatchConfig_Variant(base.SelfPtr());
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x001CF1D9 File Offset: 0x001CD5D9
		internal long ExclusiveBitMask()
		{
			return TurnBasedMatchConfig.TurnBasedMatchConfig_ExclusiveBitMask(base.SelfPtr());
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x001CF1E6 File Offset: 0x001CD5E6
		internal uint MinimumAutomatchingPlayers()
		{
			return TurnBasedMatchConfig.TurnBasedMatchConfig_MinimumAutomatchingPlayers(base.SelfPtr());
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x001CF1F3 File Offset: 0x001CD5F3
		internal uint MaximumAutomatchingPlayers()
		{
			return TurnBasedMatchConfig.TurnBasedMatchConfig_MaximumAutomatchingPlayers(base.SelfPtr());
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x001CF200 File Offset: 0x001CD600
		protected override void CallDispose(HandleRef selfPointer)
		{
			TurnBasedMatchConfig.TurnBasedMatchConfig_Dispose(selfPointer);
		}
	}
}
