using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F4 RID: 1780
	internal class NativePlayer : BaseReferenceHolder
	{
		// Token: 0x0600334C RID: 13132 RVA: 0x001CBCEC File Offset: 0x001CA0EC
		internal NativePlayer(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x001CBCF5 File Offset: 0x001CA0F5
		internal string Id()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Player.Player_Id(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x001CBD08 File Offset: 0x001CA108
		internal string Name()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Player.Player_Name(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x001CBD1B File Offset: 0x001CA11B
		internal string AvatarURL()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Player.Player_AvatarUrl(base.SelfPtr(), Types.ImageResolution.ICON, out_string, out_size));
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x001CBD2E File Offset: 0x001CA12E
		protected override void CallDispose(HandleRef selfPointer)
		{
			GooglePlayGames.Native.Cwrapper.Player.Player_Dispose(selfPointer);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x001CBD36 File Offset: 0x001CA136
		internal GooglePlayGames.BasicApi.Multiplayer.Player AsPlayer()
		{
			return new GooglePlayGames.BasicApi.Multiplayer.Player(this.Name(), this.Id(), this.AvatarURL());
		}
	}
}
