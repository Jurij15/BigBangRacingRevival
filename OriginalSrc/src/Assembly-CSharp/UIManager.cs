using System;
using System.Collections.Generic;

// Token: 0x020005B0 RID: 1456
public static class UIManager
{
	// Token: 0x06002A85 RID: 10885 RVA: 0x001BA130 File Offset: 0x001B8530
	public static void Update()
	{
		for (int i = 0; i < UIManager.m_uiComponents.Count; i++)
		{
			UIManager.m_uiComponents[i].Step();
		}
	}

	// Token: 0x06002A86 RID: 10886 RVA: 0x001BA168 File Offset: 0x001B8568
	public static void DestroyUI()
	{
		while (UIManager.m_uiComponents.Count > 0)
		{
			int num = UIManager.m_uiComponents.Count - 1;
			UIManager.m_uiComponents[num].Destroy(false);
			UIManager.m_uiComponents.RemoveAt(num);
		}
	}

	// Token: 0x06002A87 RID: 10887 RVA: 0x001BA1B4 File Offset: 0x001B85B4
	public static void ScreenSizeChanged()
	{
		for (int i = 0; i < UIManager.m_uiComponents.Count; i++)
		{
			UIManager.m_uiComponents[i].Update();
		}
	}

	// Token: 0x04002FBE RID: 12222
	public static List<UIComponent> m_uiComponents = new List<UIComponent>();

	// Token: 0x04002FBF RID: 12223
	public static UICanvas m_canvas;
}
