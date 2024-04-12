using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F6 RID: 1782
	internal class NativeRealTimeRoom : BaseReferenceHolder
	{
		// Token: 0x06003367 RID: 13159 RVA: 0x001CBEDE File Offset: 0x001CA2DE
		internal NativeRealTimeRoom(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x001CBEE7 File Offset: 0x001CA2E7
		internal string Id()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => RealTimeRoom.RealTimeRoom_Id(base.SelfPtr(), out_string, size));
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x001CBEFA File Offset: 0x001CA2FA
		internal IEnumerable<MultiplayerParticipant> Participants()
		{
			return PInvokeUtilities.ToEnumerable<MultiplayerParticipant>(RealTimeRoom.RealTimeRoom_Participants_Length(base.SelfPtr()), (UIntPtr index) => new MultiplayerParticipant(RealTimeRoom.RealTimeRoom_Participants_GetElement(base.SelfPtr(), index)));
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x001CBF18 File Offset: 0x001CA318
		internal uint ParticipantCount()
		{
			return RealTimeRoom.RealTimeRoom_Participants_Length(base.SelfPtr()).ToUInt32();
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x001CBF38 File Offset: 0x001CA338
		internal Types.RealTimeRoomStatus Status()
		{
			return RealTimeRoom.RealTimeRoom_Status(base.SelfPtr());
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x001CBF45 File Offset: 0x001CA345
		protected override void CallDispose(HandleRef selfPointer)
		{
			RealTimeRoom.RealTimeRoom_Dispose(selfPointer);
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x001CBF4D File Offset: 0x001CA34D
		internal static NativeRealTimeRoom FromPointer(IntPtr selfPointer)
		{
			if (selfPointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeRealTimeRoom(selfPointer);
		}
	}
}
