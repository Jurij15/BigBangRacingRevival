using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006E3 RID: 1763
	internal sealed class IosPlatformConfiguration : PlatformConfiguration
	{
		// Token: 0x060032BA RID: 12986 RVA: 0x001CAA01 File Offset: 0x001C8E01
		private IosPlatformConfiguration(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x001CAA0A File Offset: 0x001C8E0A
		internal void SetClientId(string clientId)
		{
			Misc.CheckNotNull<string>(clientId);
			IosPlatformConfiguration.IosPlatformConfiguration_SetClientID(base.SelfPtr(), clientId);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x001CAA1F File Offset: 0x001C8E1F
		protected override void CallDispose(HandleRef selfPointer)
		{
			IosPlatformConfiguration.IosPlatformConfiguration_Dispose(selfPointer);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x001CAA27 File Offset: 0x001C8E27
		internal static IosPlatformConfiguration Create()
		{
			return new IosPlatformConfiguration(IosPlatformConfiguration.IosPlatformConfiguration_Construct());
		}
	}
}
