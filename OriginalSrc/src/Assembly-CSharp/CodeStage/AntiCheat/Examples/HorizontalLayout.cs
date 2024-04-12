using System;
using UnityEngine;

namespace CodeStage.AntiCheat.Examples
{
	// Token: 0x02000477 RID: 1143
	internal class HorizontalLayout : IDisposable
	{
		// Token: 0x06001F25 RID: 7973 RVA: 0x0017F7D6 File Offset: 0x0017DBD6
		public HorizontalLayout(params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(options);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0017F7E4 File Offset: 0x0017DBE4
		public void Dispose()
		{
			GUILayout.EndHorizontal();
		}
	}
}
