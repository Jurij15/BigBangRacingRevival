using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x0200070F RID: 1807
	internal class PlayerSelectUIResponse : BaseReferenceHolder, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x001CD727 File Offset: 0x001CBB27
		internal PlayerSelectUIResponse(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x001CD730 File Offset: 0x001CBB30
		internal CommonErrorStatus.UIStatus Status()
		{
			return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetStatus(base.SelfPtr());
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x001CD740 File Offset: 0x001CBB40
		private string PlayerIdAtIndex(UIntPtr index)
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_GetElement(this.SelfPtr(), index, out_string, size));
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x001CD772 File Offset: 0x001CBB72
		public IEnumerator<string> GetEnumerator()
		{
			return PInvokeUtilities.ToEnumerator<string>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_Length(base.SelfPtr()), new Func<UIntPtr, string>(this.PlayerIdAtIndex));
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x001CD790 File Offset: 0x001CBB90
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x001CD798 File Offset: 0x001CBB98
		internal uint MinimumAutomatchingPlayers()
		{
			return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMinimumAutomatchingPlayers(base.SelfPtr());
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x001CD7A5 File Offset: 0x001CBBA5
		internal uint MaximumAutomatchingPlayers()
		{
			return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMaximumAutomatchingPlayers(base.SelfPtr());
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x001CD7B2 File Offset: 0x001CBBB2
		protected override void CallDispose(HandleRef selfPointer)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_Dispose(selfPointer);
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x001CD7BA File Offset: 0x001CBBBA
		internal static PlayerSelectUIResponse FromPointer(IntPtr pointer)
		{
			if (PInvokeUtilities.IsNull(pointer))
			{
				return null;
			}
			return new PlayerSelectUIResponse(pointer);
		}
	}
}
