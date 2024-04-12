using System;
using UnityEngine;

namespace CodeStage.AntiCheat.Examples
{
	// Token: 0x02000478 RID: 1144
	internal class VerticalLayout : IDisposable
	{
		// Token: 0x06001F27 RID: 7975 RVA: 0x0017F7EB File Offset: 0x0017DBEB
		public VerticalLayout(params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(options);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0017F7F9 File Offset: 0x0017DBF9
		public VerticalLayout(GUIStyle style)
		{
			GUILayout.BeginVertical(style, new GUILayoutOption[0]);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0017F80D File Offset: 0x0017DC0D
		public void Dispose()
		{
			GUILayout.EndHorizontal();
		}
	}
}
