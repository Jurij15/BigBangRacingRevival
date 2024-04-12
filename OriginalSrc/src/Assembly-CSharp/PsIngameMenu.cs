using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
public static class PsIngameMenu
{
	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000782 RID: 1922 RVA: 0x000527A0 File Offset: 0x00050BA0
	// (set) Token: 0x06000783 RID: 1923 RVA: 0x000527A7 File Offset: 0x00050BA7
	public static PsUIBasePopup m_popupMenu
	{
		get
		{
			return PsIngameMenu._popupMenu;
		}
		set
		{
			PsIngameMenu._popupMenu = value;
		}
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x000527AF File Offset: 0x00050BAF
	public static void CloseAll()
	{
		if (PsIngameMenu.m_popupMenu != null)
		{
			PsIngameMenu.m_popupMenu.Destroy();
			PsIngameMenu.m_popupMenu = null;
		}
		if (PsIngameMenu.m_playMenu != null)
		{
			PsIngameMenu.m_playMenu.Destroy();
			PsIngameMenu.m_playMenu = null;
		}
		PsIngameMenu.CloseController();
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x000527EC File Offset: 0x00050BEC
	public static void OpenController(bool _overlayCamera = false)
	{
		if (PsIngameMenu.m_controller != null)
		{
			PsIngameMenu.CloseController();
		}
		if (_overlayCamera)
		{
			PsIngameMenu.m_overlayCamera = CameraS.AddCamera("ControllerCamera", true, 3);
		}
		if (PsState.m_activeMinigame.m_playerControllerType != null)
		{
			object[] array = new object[] { PsIngameMenu.m_overlayCamera };
			PsIngameMenu.m_controller = Activator.CreateInstance(PsState.m_activeMinigame.m_playerControllerType, array) as Controller;
			PsIngameMenu.m_controller.Open();
		}
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x00052861 File Offset: 0x00050C61
	public static void CloseController()
	{
		if (PsIngameMenu.m_controller != null)
		{
			PsIngameMenu.m_controller.Close();
			PsIngameMenu.m_controller = null;
			if (PsIngameMenu.m_overlayCamera != null)
			{
				CameraS.RemoveCamera(PsIngameMenu.m_overlayCamera);
				PsIngameMenu.m_overlayCamera = null;
			}
		}
	}

	// Token: 0x040007B9 RID: 1977
	private static PsUIBasePopup _popupMenu;

	// Token: 0x040007BA RID: 1978
	public static IPlayMenu m_playMenu;

	// Token: 0x040007BB RID: 1979
	public static Controller m_controller;

	// Token: 0x040007BC RID: 1980
	private static Camera m_overlayCamera;
}
