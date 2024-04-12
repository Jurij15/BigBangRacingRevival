using System;

// Token: 0x0200059E RID: 1438
public class UIListPropertySelectionPopup : UIScrollableCanvas
{
	// Token: 0x060029C8 RID: 10696 RVA: 0x001B7834 File Offset: 0x001B5C34
	public UIListPropertySelectionPopup(UIModel _model, string _fieldName)
		: base(null, "UIListPropertySelectionPopup")
	{
		this.m_model = _model;
		this.m_fieldName = _fieldName;
		this.m_maxScrollInertialY = 0f;
		this.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.5f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.85f, RelativeTo.ScreenHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIPopup.PopupDrawHandler));
		this.m_scrollableCanvas = new UIScrollableCanvas(uicanvas, string.Empty);
		this.m_scrollableCanvas.RemoveDrawHandler();
		this.m_verticalArea = new UIVerticalList(this.m_scrollableCanvas, string.Empty);
		this.m_verticalArea.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.m_upperLeftArea = new UIHorizontalList(this, "UpperLeft");
		this.m_upperLeftArea.SetMargins(0.025f, RelativeTo.ScreenShortest);
		this.m_upperLeftArea.SetAlign(0f, 1f);
		this.m_upperLeftArea.RemoveDrawHandler();
		this.m_upperLeftArea.RemoveTouchAreas();
		this.m_upperLeftArea.SetDepthOffset(-50f);
		this.m_closeButton = new PsUIGenericButton(this.m_upperLeftArea, 0.25f, 0.25f, 0.005f, "Button");
		this.m_closeButton.SetText("Cancel", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_closeButton.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_closeButton.Update();
		this.Update();
	}

	// Token: 0x060029C9 RID: 10697 RVA: 0x001B79EA File Offset: 0x001B5DEA
	public virtual void Close()
	{
		this.Destroy();
	}

	// Token: 0x060029CA RID: 10698 RVA: 0x001B79F2 File Offset: 0x001B5DF2
	public override void Step()
	{
		if (this.m_closeButton.m_hit)
		{
			this.Destroy();
			return;
		}
		base.Step();
	}

	// Token: 0x04002EE9 RID: 12009
	protected UIScrollableCanvas m_scrollableCanvas;

	// Token: 0x04002EEA RID: 12010
	protected UIVerticalList m_verticalArea;

	// Token: 0x04002EEB RID: 12011
	private UIHorizontalList m_upperLeftArea;

	// Token: 0x04002EEC RID: 12012
	private PsUIGenericButton m_closeButton;
}
