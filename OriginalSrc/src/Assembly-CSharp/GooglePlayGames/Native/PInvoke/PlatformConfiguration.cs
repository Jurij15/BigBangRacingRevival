using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000709 RID: 1801
	internal abstract class PlatformConfiguration : BaseReferenceHolder
	{
		// Token: 0x06003414 RID: 13332 RVA: 0x001C9DBB File Offset: 0x001C81BB
		protected PlatformConfiguration(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x001C9DC4 File Offset: 0x001C81C4
		internal HandleRef AsHandle()
		{
			return base.SelfPtr();
		}
	}
}
