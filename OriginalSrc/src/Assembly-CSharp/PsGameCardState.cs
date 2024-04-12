using System;
using UnityEngine;

// Token: 0x0200022C RID: 556
public class PsGameCardState : BasicState
{
	// Token: 0x0600104F RID: 4175 RVA: 0x00097914 File Offset: 0x00095D14
	public PsGameCardState(Action _exitAction, Type _main, Type _left = null, Type _right = null)
	{
		this.m_exitAction = _exitAction;
		this.m_mainType = _main;
		this.m_leftType = _left;
		this.m_rightType = _right;
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x00097939 File Offset: 0x00095D39
	public override void Enter(IStatedObject _parent)
	{
		this.m_utilityEntity = EntityManager.AddEntity();
		this.CreateUI();
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0009794C File Offset: 0x00095D4C
	public virtual void CreateUI()
	{
		this.m_baseCanvas = new UIScrollableCanvas(null, "GameCard");
		this.m_baseCanvas.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_baseCanvas.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_baseCanvas.SetDepthOffset(20f);
		this.m_baseCanvas.RemoveDrawHandler();
		this.m_baseCanvas.m_maxScrollInertialX = 0f;
		this.m_baseCanvas.m_maxScrollInertialY = 0f;
		int num = 1;
		if (this.m_leftType != null)
		{
			num++;
		}
		if (this.m_rightType != null)
		{
			num++;
		}
		this.m_scrollableCanvas = new UIScrollableSnappingCanvas(this.m_baseCanvas, "ScrollableCanvas", num);
		this.m_scrollableCanvas.RemoveDrawHandler();
		this.m_scrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_scrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_scrollableCanvas.SetMargins(0.025f, 0.025f, 0.025f, 0.025f, RelativeTo.ScreenWidth);
		this.m_scrollableCanvas.SetVerticalAlign(0f);
		this.m_scrollableCanvas.m_maxScrollInertialX = 200f * (float.Parse(Screen.width.ToString()) / 1024f);
		if (num == 1)
		{
			this.m_scrollableCanvas.m_maxScrollInertialX = 0f;
		}
		this.m_scrollableCanvas.m_maxScrollInertialY = 0f;
		float num2 = 0f;
		if (this.m_leftType != null && this.m_rightType == null)
		{
			this.m_scrollableCanvas.SetScrollPosition(1f, 0f);
			num2 = 1f;
		}
		else if (this.m_leftType == null && this.m_rightType != null)
		{
			this.m_scrollableCanvas.SetScrollPosition(0f, 0f);
		}
		else
		{
			this.m_scrollableCanvas.SetScrollPosition(0.5f, 0f);
			num2 = 0.5f;
		}
		this.m_scrollableCanvasContent = new UIHorizontalList(this.m_scrollableCanvas, "hlist");
		this.m_scrollableCanvasContent.SetSpacing(0.125f, RelativeTo.ScreenHeight);
		this.m_scrollableCanvasContent.SetMargins(0.125f, 0.125f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_scrollableCanvasContent.SetVerticalAlign(0f);
		this.m_scrollableCanvasContent.SetHorizontalAlign(num2);
		this.m_scrollableCanvasContent.RemoveDrawHandler();
		object[] array = new object[] { this.m_scrollableCanvasContent };
		if (this.m_leftType != null)
		{
			this.m_leftContent = Activator.CreateInstance(this.m_leftType, array) as UIComponent;
		}
		if (this.m_mainType != null)
		{
			this.m_mainContent = Activator.CreateInstance(this.m_mainType, array) as UIComponent;
		}
		if (this.m_rightType != null)
		{
			this.m_rightContent = Activator.CreateInstance(this.m_rightType, array) as UIComponent;
		}
		this.CreateOverlayHeader();
		if (this.m_leftContent != null)
		{
			this.m_leftContent.UpdateUniqueCamera();
		}
		if (this.m_mainContent != null)
		{
			this.m_mainContent.UpdateUniqueCamera();
		}
		if (this.m_rightContent != null)
		{
			this.m_rightContent.UpdateUniqueCamera();
		}
		this.m_baseCanvas.Update();
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x00097C70 File Offset: 0x00096070
	public virtual void CreateOverlayHeader()
	{
		this.m_overlayCamera = CameraS.AddCamera("FullscreenPopupCamera", true, 3);
		PsMetagameManager.ShowResources(this.m_overlayCamera, true, true, true, false, 0.03f, false, false, false);
		this.m_overlayCanvas = new UICanvas(this.m_baseCanvas, false, "OverlayCanvas", null, string.Empty);
		this.m_overlayCanvas.RemoveTouchAreas();
		this.m_overlayCanvas.RemoveDrawHandler();
		this.m_upperLeftArea = new UIHorizontalList(this.m_overlayCanvas, "UpperLeft");
		this.m_upperLeftArea.SetMargins(0.025f, RelativeTo.ScreenShortest);
		this.m_upperLeftArea.SetAlign(0f, 1f);
		this.m_upperLeftArea.RemoveDrawHandler();
		this.m_upperLeftArea.RemoveTouchAreas();
		this.m_upperLeftArea.SetDepthOffset(-50f);
		this.m_upperLeftArea.m_camera = this.m_overlayCamera;
		this.m_closeButton = new PsUIGenericButton(this.m_upperLeftArea, 0.25f, 0.25f, 0.005f, "Button");
		this.m_closeButton.SetSound("/UI/ButtonBack");
		this.m_closeButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x00097DA5 File Offset: 0x000961A5
	public override void Execute()
	{
		if (this.m_closeButton.m_hit)
		{
			this.m_exitAction.Invoke();
		}
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x00097DC2 File Offset: 0x000961C2
	public override void Exit()
	{
		this.m_baseCanvas.Destroy();
		PsMetagameManager.HideResources();
		CameraS.RemoveCamera(this.m_overlayCamera);
		this.m_overlayCamera = null;
	}

	// Token: 0x0400130A RID: 4874
	private Entity m_utilityEntity;

	// Token: 0x0400130B RID: 4875
	private UIScrollableCanvas m_baseCanvas;

	// Token: 0x0400130C RID: 4876
	private UICanvas m_overlayCanvas;

	// Token: 0x0400130D RID: 4877
	private UICanvas m_upperArea;

	// Token: 0x0400130E RID: 4878
	private UIHorizontalList m_upperLeftArea;

	// Token: 0x0400130F RID: 4879
	private UIHorizontalList m_upperRightArea;

	// Token: 0x04001310 RID: 4880
	private UICanvas m_lowerArea;

	// Token: 0x04001311 RID: 4881
	private UIHorizontalList m_lowerLeftArea;

	// Token: 0x04001312 RID: 4882
	private UIHorizontalList m_lowerRightArea;

	// Token: 0x04001313 RID: 4883
	public PsUIGenericButton m_closeButton;

	// Token: 0x04001314 RID: 4884
	private PsUIProfileImage m_profile;

	// Token: 0x04001315 RID: 4885
	private Action m_exitAction;

	// Token: 0x04001316 RID: 4886
	private Type m_mainType;

	// Token: 0x04001317 RID: 4887
	private Type m_leftType;

	// Token: 0x04001318 RID: 4888
	private Type m_rightType;

	// Token: 0x04001319 RID: 4889
	public UIComponent m_mainContent;

	// Token: 0x0400131A RID: 4890
	public UIComponent m_leftContent;

	// Token: 0x0400131B RID: 4891
	public UIComponent m_rightContent;

	// Token: 0x0400131C RID: 4892
	public UIScrollableSnappingCanvas m_scrollableCanvas;

	// Token: 0x0400131D RID: 4893
	public UIHorizontalList m_scrollableCanvasContent;

	// Token: 0x0400131E RID: 4894
	private Camera m_overlayCamera;
}
