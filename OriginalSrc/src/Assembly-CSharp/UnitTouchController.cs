using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class UnitTouchController : Controller
{
	// Token: 0x06000353 RID: 851 RVA: 0x00031CBA File Offset: 0x000300BA
	public UnitTouchController(Camera _overlayCamera = null)
	{
		this.m_overlayCamera = _overlayCamera;
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00031CC9 File Offset: 0x000300C9
	public override void Open()
	{
		if (this.m_open)
		{
			this.Close();
		}
		this.m_open = true;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x00031CE3 File Offset: 0x000300E3
	public override void Close()
	{
		base.Close();
		if (this.m_open)
		{
			this.m_open = false;
			Controller.RemoveAllButtons();
		}
	}

	// Token: 0x04000444 RID: 1092
	private Camera m_overlayCamera;
}
