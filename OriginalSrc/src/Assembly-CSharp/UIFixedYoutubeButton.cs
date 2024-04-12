using System;

// Token: 0x02000398 RID: 920
public class UIFixedYoutubeButton : PsUIGenericButton
{
	// Token: 0x06001A5F RID: 6751 RVA: 0x00126980 File Offset: 0x00124D80
	public UIFixedYoutubeButton(UIComponent _parent, string _youtuberName, int _youtubeSubscribers, float _width = 1f, RelativeTo _widthRelativeTo = RelativeTo.ParentWidth, float _gradientSize = 0.25f, float _gradientPos = 0.25f, float _cornerSize = 0.001f, string _tag = "YoutubeButton")
		: base(_parent, _gradientSize, _gradientPos, _cornerSize, _tag)
	{
		this.m_buttonWidth = _width;
		this.m_buttonWidthRelativeTo = _widthRelativeTo;
		this.m_youtubeUsername = _youtuberName;
		this.m_subscribers = _youtubeSubscribers;
		this.SetHeight(0.04f, RelativeTo.ScreenHeight);
		base.SetSandColors();
		this.SetMargins(0f, RelativeTo.ScreenHeight);
		this.SetSpacing(0f, RelativeTo.ScreenHeight);
		this.m_TAC.m_letTouchesThrough = false;
		this.m_buttonCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_buttonCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_buttonCanvas.SetWidth(this.m_buttonWidth, this.m_buttonWidthRelativeTo);
		this.m_buttonCanvas.SetMargins(1.3f, 0f, 0f, 0f, RelativeTo.ParentHeight);
		this.m_buttonCanvas.RemoveDrawHandler();
		this.m_textList = new UICanvas(this.m_buttonCanvas, false, string.Empty, null, string.Empty);
		this.m_textList.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_textList.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_textList.SetMargins(0f, 0.4f, 0f, 0f, RelativeTo.ParentHeight);
		this.m_textList.RemoveDrawHandler();
		this.SetContent();
		UICanvas uicanvas = new UICanvas(this.m_buttonCanvas, false, string.Empty, null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetSize(1f, 1.33f, RelativeTo.ParentHeight);
		uicanvas.SetMargins(-1.15f, 1.15f, 0f, 0f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x00126B58 File Offset: 0x00124F58
	private void SetContent()
	{
		if (this.m_textList != null)
		{
			this.m_textList.DestroyChildren();
		}
		string youtubeUsername = this.m_youtubeUsername;
		string text = ((this.m_subscribers <= -1) ? "#000000" : "D53228");
		UICanvas textList = this.m_textList;
		bool flag = false;
		string empty = string.Empty;
		string text2 = youtubeUsername;
		string font = PsFontManager.GetFont(PsFonts.KGSecondChances);
		float num = 0.45f;
		RelativeTo relativeTo = RelativeTo.ParentHeight;
		bool flag2 = false;
		string text3 = text;
		UITextbox uitextbox = new UITextbox(textList, flag, empty, text2, font, num, relativeTo, flag2, Align.Left, Align.Top, text3, true, null);
		uitextbox.SetMaxRows(1);
		uitextbox.UseDotsWhenWrapping(true);
		uitextbox.SetHeight(0.55f, RelativeTo.ParentHeight);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		if (this.m_subscribers > -1)
		{
			uitextbox.SetVerticalAlign(1f);
			UICanvas uicanvas = new UICanvas(this.m_textList, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.45f, RelativeTo.ParentHeight);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.SetMargins(0f, 0f, 0.025f, 0.025f, RelativeTo.ParentHeight);
			uicanvas.RemoveDrawHandler();
			uicanvas.SetAlign(0f, 0f);
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsMetagameManager.NumberToString(this.m_subscribers) + " subscribers", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "79746c", null);
			uifittedText.SetHorizontalAlign(0f);
		}
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x00126CC6 File Offset: 0x001250C6
	public override void Update()
	{
		this.SetContent();
		base.Update();
	}

	// Token: 0x04001CEC RID: 7404
	private UICanvas m_buttonCanvas;

	// Token: 0x04001CED RID: 7405
	private UICanvas m_textList;

	// Token: 0x04001CEE RID: 7406
	public float m_buttonWidth;

	// Token: 0x04001CEF RID: 7407
	public RelativeTo m_buttonWidthRelativeTo;

	// Token: 0x04001CF0 RID: 7408
	public string m_youtubeUsername;

	// Token: 0x04001CF1 RID: 7409
	public int m_subscribers;
}
