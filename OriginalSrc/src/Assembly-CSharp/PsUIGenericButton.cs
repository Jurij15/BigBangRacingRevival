using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class PsUIGenericButton : UIHorizontalList
{
	// Token: 0x06001111 RID: 4369 RVA: 0x000A361C File Offset: 0x000A1A1C
	public PsUIGenericButton(UIComponent _parent, float _gradientSize = 0.25f, float _gradientPos = 0.25f, float _cornerSize = 0.005f, string _tag = "Button")
		: base(_parent, _tag)
	{
		this.SetMargins(0.02f, 0.02f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.CreateTouchAreas();
		this.m_TAC.m_letTouchesThrough = true;
		this.m_fillTopColor = DebugDraw.HexToColor("#0077d5");
		this.m_fillBottomColor = DebugDraw.HexToColor("#00b6ea");
		this.m_outlineTopColor = DebugDraw.HexToColor("#58cbe3");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#0160bb");
		this.m_outlineColor = DebugDraw.HexToColor("#2a3f57");
		this.m_textColor = "#FFFFFF";
		this.m_iconColor = Color.gray;
		this.m_gradientSize = _gradientSize;
		this.m_gradientPos = _gradientPos;
		this.m_cornerSize = _cornerSize;
		this.m_customRelease = false;
		this.m_customTweens = false;
		this.m_currentScale = Vector3.one;
		this.m_lateDraw = false;
		this.m_scaledUp = false;
		this.m_scaleAtCreate = false;
		this.m_createScale = Vector3.zero;
		this.m_sound = "/UI/ButtonNormal";
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x000A3739 File Offset: 0x000A1B39
	public void SetReleaseAction(Action _releaseAction)
	{
		this.m_releaseAction = _releaseAction;
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x000A3742 File Offset: 0x000A1B42
	public void SetSound(string _sound)
	{
		this.m_sound = _sound;
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x000A374B File Offset: 0x000A1B4B
	public void SetCustomRelease(bool _val)
	{
		this.m_customRelease = _val;
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x000A3754 File Offset: 0x000A1B54
	public void SetCustomTweens(bool _val)
	{
		this.m_customTweens = _val;
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x000A3760 File Offset: 0x000A1B60
	public void SetGreenColors(bool _enabled = true)
	{
		if (_enabled)
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#47d30f") * 0.9f;
			this.m_fillTopColor.a = 1f;
			this.m_fillBottomColor = DebugDraw.HexToColor("#95ff37");
			this.m_outlineTopColor = DebugDraw.HexToColor("#a0ff19t");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#45c200");
			this.m_outlineColor = DebugDraw.HexToColor("#1f6500");
			this.m_textColor = "#FFFFFF";
			this.m_iconColor = Color.gray;
		}
		else
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#a3f255");
			this.m_fillBottomColor = DebugDraw.HexToColor("#a3f255");
			this.m_outlineTopColor = DebugDraw.HexToColor("#dcff92");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#dcff92");
			this.m_outlineColor = DebugDraw.HexToColor("#FFFFFF");
			this.m_textColor = "#5bc70c";
			this.m_iconColor = Color.gray;
		}
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x000A3860 File Offset: 0x000A1C60
	public void SetBlueColors(bool _enabled = true)
	{
		if (_enabled)
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#0077d5");
			this.m_fillBottomColor = DebugDraw.HexToColor("#00b6ea");
			this.m_outlineTopColor = DebugDraw.HexToColor("#58cbe3");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#0160bb");
			this.m_outlineColor = DebugDraw.HexToColor("#2a3f57");
			this.m_textColor = "#FFFFFF";
			this.m_iconColor = Color.gray;
		}
		else
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#8ae2ff");
			this.m_fillBottomColor = DebugDraw.HexToColor("#8ae2ff");
			this.m_outlineTopColor = DebugDraw.HexToColor("#c5f4ff");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#c5f4ff");
			this.m_outlineColor = DebugDraw.HexToColor("#FFFFFF");
			this.m_textColor = "#4fabe3";
			this.m_iconColor = Color.gray;
		}
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x000A3944 File Offset: 0x000A1D44
	public void SetBlueWhiteColors(bool _enabled = true)
	{
		if (_enabled)
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#0077d5");
			this.m_fillBottomColor = DebugDraw.HexToColor("#00b6ea");
			this.m_outlineTopColor = DebugDraw.HexToColor("#ffffff");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#ffffff");
			this.m_outlineColor = DebugDraw.HexToColor("#ffffff");
			this.m_textColor = "#FFFFFF";
			this.m_iconColor = Color.gray;
		}
		else
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#8ae2ff");
			this.m_fillBottomColor = DebugDraw.HexToColor("#8ae2ff");
			this.m_outlineTopColor = DebugDraw.HexToColor("#c5f4ff");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#c5f4ff");
			this.m_outlineColor = DebugDraw.HexToColor("#FFFFFF");
			this.m_textColor = "#4fabe3";
			this.m_iconColor = Color.gray;
		}
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x000A3A28 File Offset: 0x000A1E28
	public void SetSubTabBlueColors(bool _enabled = true)
	{
		if (_enabled)
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#1778B9");
			this.m_fillBottomColor = DebugDraw.HexToColor("#0C467C");
			this.m_outlineTopColor = DebugDraw.HexToColor("#1778B9");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#0C467C");
			this.m_outlineColor = DebugDraw.HexToColor("#0C467B");
			this.m_textColor = "#89CFE8";
			this.m_iconColor = Color.gray;
		}
		else
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#3EB3E6");
			this.m_fillBottomColor = DebugDraw.HexToColor("#1D8FF4");
			this.m_outlineTopColor = DebugDraw.HexToColor("#3EB3E6");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#1D8FF4");
			this.m_outlineColor = DebugDraw.HexToColor("#18599B");
			this.m_textColor = "#ffffff";
			this.m_iconColor = Color.gray;
		}
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x000A3B0C File Offset: 0x000A1F0C
	public void SetOrangeColors(bool _enabled = true)
	{
		if (_enabled)
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#ff9e0a");
			this.m_fillBottomColor = DebugDraw.HexToColor("#ffc321");
			this.m_outlineTopColor = DebugDraw.HexToColor("#ffd440");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#ffa81f");
			this.m_outlineColor = DebugDraw.HexToColor("#ad4d00");
			this.m_textColor = "#FFFFFF";
			this.m_iconColor = Color.gray;
		}
		else
		{
			this.m_fillTopColor = DebugDraw.HexToColor("#ffd65b");
			this.m_fillBottomColor = DebugDraw.HexToColor("#ffd65b");
			this.m_outlineTopColor = DebugDraw.HexToColor("#ffe6a1");
			this.m_outlineBottomColor = DebugDraw.HexToColor("#ffe6a1");
			this.m_outlineColor = DebugDraw.HexToColor("#FFFFFF");
			this.m_textColor = "#ffa200";
			this.m_iconColor = Color.gray;
		}
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x000A3BF0 File Offset: 0x000A1FF0
	public void SetLovelyRedColors()
	{
		this.m_fillTopColor = DebugDraw.HexToColor("#ff944e");
		this.m_fillBottomColor = DebugDraw.HexToColor("#e74c32");
		this.m_outlineTopColor = DebugDraw.HexToColor("#e74c32");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#ff944e");
		this.m_outlineColor = DebugDraw.HexToColor("#8b1700");
		this.m_textColor = "#FFFFFF";
		this.m_iconColor = Color.gray;
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x000A3C64 File Offset: 0x000A2064
	public void SetTealColors()
	{
		this.m_fillTopColor = DebugDraw.HexToColor("#49b4ae");
		this.m_fillBottomColor = DebugDraw.HexToColor("#3ca19c");
		this.m_outlineTopColor = DebugDraw.HexToColor("#7ed0c5");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#2f8587");
		this.m_outlineColor = DebugDraw.HexToColor("#083a3b");
		this.m_textColor = "#FFFFFF";
		this.m_iconColor = Color.gray;
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x000A3CD8 File Offset: 0x000A20D8
	public void SetRedColors()
	{
		this.m_fillTopColor = DebugDraw.HexToColor("#c90702");
		this.m_fillBottomColor = DebugDraw.HexToColor("#ee2219");
		this.m_outlineTopColor = DebugDraw.HexToColor("#fe5a5a");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#c61706");
		this.m_outlineColor = DebugDraw.HexToColor("#850906");
		this.m_textColor = "#FFFFFF";
		this.m_iconColor = Color.gray;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x000A3D4C File Offset: 0x000A214C
	public void SetGrayColors()
	{
		this.m_fillTopColor = DebugDraw.HexToColor("#8e8e8e");
		this.m_fillBottomColor = DebugDraw.HexToColor("#7D7D7D");
		this.m_outlineTopColor = DebugDraw.HexToColor("#B1B1B1");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#696969");
		this.m_outlineColor = DebugDraw.HexToColor("#2A2A2A");
		this.m_textColor = "#cccccc";
		this.m_iconColor = DebugDraw.HexToColor("#cccccc") * Color.gray;
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x000A3DD0 File Offset: 0x000A21D0
	public void SetDarkGrayColors()
	{
		this.m_fillTopColor = DebugDraw.HexToColor("#555555");
		this.m_fillBottomColor = DebugDraw.HexToColor("#4b4b4b");
		this.m_outlineTopColor = DebugDraw.HexToColor("#4b4b4b");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#555555");
		this.m_outlineColor = DebugDraw.HexToColor("#2A2A2A");
		this.m_textColor = "#8e8e8e";
		this.m_iconColor = DebugDraw.HexToColor("#cccccc") * Color.gray;
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x000A3E54 File Offset: 0x000A2254
	public void SetPurpleColors()
	{
		this.m_fillTopColor = DebugDraw.HexToColor("#7379be");
		this.m_fillBottomColor = DebugDraw.HexToColor("#8c93d8");
		this.m_outlineTopColor = DebugDraw.HexToColor("#a6abe0");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#595e94");
		this.m_outlineColor = DebugDraw.HexToColor("#333654");
		this.m_textColor = "#FFFFFF";
		this.m_iconColor = Color.gray;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x000A3EC8 File Offset: 0x000A22C8
	public void SetMagentaColors()
	{
		this.m_fillTopColor = DebugDraw.HexToColor("#f598ff");
		this.m_fillBottomColor = DebugDraw.HexToColor("#f164ff");
		this.m_outlineTopColor = DebugDraw.HexToColor("#ef4cff");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#f598ff");
		this.m_outlineColor = DebugDraw.HexToColor("#811f8a");
		this.m_textColor = "#FFFFFF";
		this.m_iconColor = Color.gray;
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x000A3F3C File Offset: 0x000A233C
	public void SetSandColors()
	{
		this.m_fillTopColor = DebugDraw.HexToColor("#F6F2E4");
		this.m_fillBottomColor = DebugDraw.HexToColor("#E9EBD9");
		this.m_outlineTopColor = DebugDraw.HexToColor("#FBF6E3");
		this.m_outlineBottomColor = DebugDraw.HexToColor("#FBF6E3");
		this.m_outlineColor = DebugDraw.HexToColor("#F6F2E4");
		this.m_textColor = "#D53228";
		this.m_iconColor = Color.gray;
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x000A3FAF File Offset: 0x000A23AF
	public void SetGlareOffset(float _y)
	{
		this.m_glareYOffset = _y;
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x000A3FB8 File Offset: 0x000A23B8
	public void SetUpperShineOffset(float _x, float _y)
	{
		this.m_topShineXOffset = _x;
		this.m_topShineYOffset = _y;
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x000A3FC8 File Offset: 0x000A23C8
	public void SetLowerShineOffset(float _x, float _y)
	{
		this.m_lowerShineXOffset = _x;
		this.m_lowerShineYOffset = _y;
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x000A3FD8 File Offset: 0x000A23D8
	public void SetMinWidth(float _width, RelativeTo _relative = RelativeTo.ScreenHeight)
	{
		this.m_hasMinWidth = true;
		this.m_minWidth = _width;
		this.m_minWidthRelativeTo = _relative;
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x000A3FF0 File Offset: 0x000A23F0
	public void SetTextWithMinWidth(string _text, float _fontSize = 0.03f, float _minWidth = 0.2f, RelativeTo _minWidthRelativeTo = RelativeTo.ScreenHeight, bool _shadow = false, RelativeTo _relativeTo = RelativeTo.ScreenShortest)
	{
		if (this.m_minWidthTextArea == null)
		{
			this.m_minWidthTextArea = new UIVerticalList(this, "textArea");
			this.m_minWidthTextArea.RemoveDrawHandler();
		}
		if (this.m_UItext != null)
		{
			this.m_UItext.Destroy();
		}
		if (_shadow)
		{
			this.m_textShadowColor = DebugDraw.ColorToHex(this.m_outlineColor);
		}
		this.m_UItext = new UIText(this.m_minWidthTextArea, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), _fontSize, _relativeTo, this.m_textColor, this.m_textShadowColor);
		if (_shadow)
		{
			this.m_UItext.SetShadowShift(new Vector2(-0.5f, -0.3f), 0.125f);
		}
		this.m_UItext.Update();
		float textWidth = this.m_UItext.m_tmc.m_textWidth;
		this.m_minWidthTextArea.SetHeight(_fontSize, _relativeTo);
		this.m_minWidthTextArea.SetWidth(Mathf.Max(textWidth / this.RelativeValueToActual(_minWidthRelativeTo), _minWidth), _minWidthRelativeTo);
		if (_text.IndexOfAny(new char[] { '\n' }) != -1)
		{
			this.SetHeight(0f, _relativeTo);
		}
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x000A4118 File Offset: 0x000A2518
	private float RelativeValueToActual(RelativeTo _relation)
	{
		switch (_relation)
		{
		case RelativeTo.ScreenWidth:
			return (float)Screen.width;
		case RelativeTo.ScreenHeight:
			return (float)Screen.height;
		case RelativeTo.ScreenShortest:
			return (float)Mathf.Min(Screen.height, Screen.width);
		case RelativeTo.ScreenLongest:
			return (float)Mathf.Max(Screen.height, Screen.width);
		default:
			Debug.LogError("only relatives to screen sizes");
			return 0f;
		}
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x000A4184 File Offset: 0x000A2584
	public void SetText(string _text, float _fontSize = 0.03f, float _textAreaWidth = 0f, RelativeTo _textAreaRelativeTo = RelativeTo.ScreenHeight, bool _shadow = false, RelativeTo _relativeTo = RelativeTo.ScreenShortest)
	{
		if (this.m_textArea == null)
		{
			this.m_textArea = new UIVerticalList(this, "textArea");
			this.m_textArea.RemoveDrawHandler();
		}
		this.m_textArea.SetWidth(_textAreaWidth, _textAreaRelativeTo);
		if (this.m_UItext != null)
		{
			this.m_UItext.Destroy();
		}
		if (_shadow)
		{
			this.m_textShadowColor = DebugDraw.ColorToHex(this.m_outlineColor);
		}
		this.m_UItext = new UIText(this.m_textArea, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), _fontSize, _relativeTo, this.m_textColor, this.m_textShadowColor);
		if (_shadow)
		{
			this.m_UItext.SetShadowShift(new Vector2(-0.5f, -0.3f), 0.125f);
		}
		if (_text.IndexOfAny(new char[] { '\n' }) != -1)
		{
			this.SetHeight(0f, _relativeTo);
		}
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x000A4270 File Offset: 0x000A2670
	public void SetFittedText(string _text, float _fontSize = 0.03f, float _textAreaWidth = 0f, RelativeTo _textAreaRelativeTo = RelativeTo.ScreenHeight, bool _shadow = false)
	{
		if (this.m_fittedTextArea == null)
		{
			this.m_fittedTextArea = new UICanvas(this, false, "textArea", null, string.Empty);
			this.m_fittedTextArea.RemoveDrawHandler();
		}
		this.m_fittedTextArea.SetWidth(_textAreaWidth, _textAreaRelativeTo);
		this.m_fittedTextArea.SetHeight(_fontSize, RelativeTo.ScreenHeight);
		if (this.m_UIFittedText != null)
		{
			this.m_UIFittedText.Destroy();
		}
		if (_shadow)
		{
			this.m_textShadowColor = DebugDraw.ColorToHex(this.m_outlineColor);
		}
		this.m_UIFittedText = new UIFittedText(this.m_fittedTextArea, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, this.m_textColor, this.m_textShadowColor);
		if (_shadow)
		{
			this.m_UIFittedText.SetShadowShift(new Vector2(-0.5f, -0.3f), 0.125f);
		}
		if (_text.IndexOfAny(new char[] { '\n' }) != -1)
		{
			this.SetHeight(0f, RelativeTo.ScreenShortest);
		}
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x000A4370 File Offset: 0x000A2770
	public void SetIcon(string _iconFrame, float _iconHeight = 0.05f, RelativeTo _iconHeightRelativeTo = RelativeTo.ScreenShortest, string _iconColor = "#FFFFFF", cpBB _margins = default(cpBB))
	{
		if (this.m_iconBase == null)
		{
			this.m_iconBase = new UICanvas(this, false, "iconBase", null, string.Empty);
			this.m_iconBase.SetSize(_iconHeight, _iconHeight, _iconHeightRelativeTo);
			this.m_iconBase.SetMargins(_margins.l, _margins.r, _margins.t, _margins.b, RelativeTo.ScreenHeight);
			this.m_iconBase.RemoveDrawHandler();
		}
		if (this.m_UIsprite != null)
		{
			this.m_UIsprite.Destroy();
		}
		this.m_UIsprite = new UIFittedSprite(this.m_iconBase, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iconFrame, null), true, true);
		this.m_UIsprite.SetHeight(_iconHeight, _iconHeightRelativeTo);
		this.m_UIsprite.SetColor(this.m_iconColor);
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x000A4444 File Offset: 0x000A2844
	public void SetIconOnly(string _iconFrame, float _iconHeight = 0.05f, RelativeTo _iconHeightRelativeTo = RelativeTo.ScreenShortest, string _iconColor = "#FFFFFF", cpBB _margins = default(cpBB))
	{
		if (this.m_iconBase == null)
		{
			this.m_iconBase = new UIHorizontalList(this, string.Empty);
			this.m_iconBase.SetHeight(_iconHeight, _iconHeightRelativeTo);
			this.m_iconBase.SetMargins(_margins.l, _margins.r, _margins.t, _margins.b, RelativeTo.ScreenHeight);
			this.m_iconBase.RemoveDrawHandler();
		}
		if (this.m_UIsprite != null)
		{
			this.m_UIsprite.Destroy();
		}
		this.m_UIsprite = new UIFittedSprite(this.m_iconBase, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iconFrame, null), true, true);
		this.m_UIsprite.SetHeight(_iconHeight, _iconHeightRelativeTo);
		this.m_UIsprite.SetColor(this.m_iconColor);
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x000A4510 File Offset: 0x000A2910
	public void SetIcon(string _iconFrame, float _iconHeight, float _iconWidth, string _iconColor = "#FFFFFF", cpBB _margins = default(cpBB), bool _fillBase = false)
	{
		if (this.m_iconBase == null)
		{
			this.m_iconBase = new UICanvas(this, false, "iconBase", null, string.Empty);
			this.m_iconBase.SetSize(_iconWidth, _iconHeight, RelativeTo.ScreenHeight);
			this.m_iconBase.SetMargins(_margins.l, _margins.r, _margins.t, _margins.b, RelativeTo.ScreenHeight);
			this.m_iconBase.RemoveDrawHandler();
		}
		if (this.m_UIsprite != null)
		{
			this.m_UIsprite.Destroy();
		}
		this.m_UIsprite = new UIFittedSprite(this.m_iconBase, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iconFrame, null), true, true);
		if (_fillBase)
		{
			this.m_UIsprite.SetHeight(1f, RelativeTo.ParentHeight);
		}
		else
		{
			this.m_UIsprite.SetHeight(_iconHeight, RelativeTo.ScreenShortest);
		}
		this.m_UIsprite.SetColor(this.m_iconColor);
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x000A4604 File Offset: 0x000A2A04
	public void SetIcon(Frame _iconFrame, float _iconHeight = 0.05f, string _iconColor = "#FFFFFF", cpBB _margins = default(cpBB))
	{
		if (this.m_iconBase == null)
		{
			this.m_iconBase = new UICanvas(this, false, "iconBase", null, string.Empty);
			this.m_iconBase.SetSize(_iconHeight, _iconHeight, RelativeTo.ScreenHeight);
			this.m_iconBase.SetMargins(_margins.l, _margins.r, _margins.t, _margins.b, RelativeTo.ScreenHeight);
			this.m_iconBase.RemoveDrawHandler();
		}
		if (this.m_UIsprite != null)
		{
			this.m_UIsprite.Destroy();
		}
		this.m_UIsprite = new UIFittedSprite(this.m_iconBase, false, string.Empty, PsState.m_uiSheet, _iconFrame, true, true);
		this.m_UIsprite.SetHeight(_iconHeight, RelativeTo.ScreenShortest);
		this.m_UIsprite.SetColor(this.m_iconColor);
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x000A46C8 File Offset: 0x000A2AC8
	public void SetAdIcon()
	{
		this.SetIcon("menu_watch_ad_badge", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", new cpBB
		{
			t = -0.02f,
			b = -0.02f,
			r = -0.06f,
			l = 0.02f
		});
		this.m_UIsprite.SetMargins(0f, 0f, -0.5f, 0.5f, RelativeTo.OwnHeight);
		this.m_UIsprite.SetHeight(1f, RelativeTo.ParentHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_UIsprite, string.Empty);
		uihorizontalList.SetDepthOffset(-5f);
		uihorizontalList.SetRogue();
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.StickerDrawHandler));
		uihorizontalList.SetMargins(0.01f, 0.01f, 0.005f, 0.005f, RelativeTo.ScreenHeight);
		UIText uitext = new UIText(uihorizontalList, false, "timeleft", PsStrings.Get(StringID.NITRO_FILL_WATCH_AD).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, "#000000", null);
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x000A47E6 File Offset: 0x000A2BE6
	public void RemoveIcon()
	{
		if (this.m_UIsprite != null)
		{
			this.m_UIsprite.Destroy();
		}
		this.m_UIsprite = null;
		if (this.m_iconBase != null)
		{
			this.m_iconBase.Destroy();
		}
		this.m_iconBase = null;
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x000A4824 File Offset: 0x000A2C24
	public void RemoveText()
	{
		if (this.m_textArea != null)
		{
			this.m_textArea.Destroy();
			this.m_textArea = null;
		}
		else if (this.m_UItext != null)
		{
			this.m_UItext.Destroy();
			this.m_UItext = null;
		}
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x000A4870 File Offset: 0x000A2C70
	public void SetCreateScale(Vector3 _scale)
	{
		this.m_scaleAtCreate = true;
		this.m_createScale = _scale;
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x000A4880 File Offset: 0x000A2C80
	public override void Update()
	{
		this.m_textShadowColor = DebugDraw.ColorToHex(this.m_outlineColor);
		if (this.m_UItext != null)
		{
			this.m_UItext.SetColor(this.m_textColor, this.m_textShadowColor);
		}
		if (this.m_UIFittedText != null)
		{
			this.m_UIFittedText.SetColor(this.m_textColor, this.m_textShadowColor);
		}
		if (this.m_upgradeTexts != null)
		{
			for (int i = 0; i < this.m_upgradeTexts.Count; i++)
			{
				this.m_upgradeTexts[i].SetColor(this.m_textColor, this.m_textShadowColor);
			}
		}
		if (this.m_UIsprite != null)
		{
			this.m_UIsprite.SetColor(this.m_iconColor);
		}
		if (this.m_hidden)
		{
			this.m_lateDraw = true;
		}
		base.Update();
		if (this.m_scaleAtCreate)
		{
			TransformS.SetScale(this.m_TC, this.m_createScale);
			this.m_scaleAtCreate = false;
		}
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x000A4984 File Offset: 0x000A2D84
	public void SetMenuButton(string _label, string _iconFrame, string _description = null, bool disabled = false)
	{
		float num = 0.045f;
		float num2 = 0.08f;
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(num2, num, RelativeTo.ScreenWidth);
		uicanvas.RemoveDrawHandler();
		if (this.m_iconBase == null)
		{
			this.m_iconBase = new UICanvas(uicanvas, false, "iconBase", null, string.Empty);
			this.m_iconBase.SetSize(num2, num, RelativeTo.ScreenWidth);
			this.m_iconBase.SetMargins(-0.1f, -0.1f, -0.5f, 0f, RelativeTo.OwnHeight);
			this.m_iconBase.RemoveDrawHandler();
		}
		if (this.m_UIsprite != null)
		{
			this.m_UIsprite.Destroy();
		}
		this.m_UIsprite = new UIFittedSprite(this.m_iconBase, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iconFrame, null), true, true);
		this.m_UIsprite.SetDepthOffset(-5f);
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetVerticalAlign(0f);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(0.6f, RelativeTo.ParentHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, _label, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#136581");
		uifittedText.SetVerticalAlign(0f);
		if (!string.IsNullOrEmpty(_description))
		{
			UIText uitext = new UIText(uicanvas, false, string.Empty, _description, PsFontManager.GetFont(PsFonts.HurmeBold), 0.25f, RelativeTo.ParentHeight, "#FFFFFF", null);
			uitext.SetVerticalAlign(-0.25f);
		}
		if (disabled)
		{
			this.SetGrayColors();
			this.m_UIsprite.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
		}
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x000A4B34 File Offset: 0x000A2F34
	public void SetNotification(string _char = "")
	{
		if (string.IsNullOrEmpty(_char))
		{
			_char = "!";
		}
		UICanvas uicanvas = new UICanvas(this, false, "notification", null, string.Empty);
		uicanvas.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.03f, -0.03f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
		uicanvas.SetRogue();
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetDepthOffset(-10f);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
		uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
		TweenC tweenC = TweenS.AddTransformTween(uicanvas2.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, _char, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x000A4C5C File Offset: 0x000A305C
	public void SetUpgradeText(string[] _texts, float[] _fontSizes, float _textAreaWidth = 0f, RelativeTo _textAreaRelativeTo = RelativeTo.ScreenHeight, bool _shadow = false)
	{
		if (this.m_textArea == null)
		{
			this.m_textArea = new UIVerticalList(this, "textArea");
			this.m_textArea.RemoveDrawHandler();
		}
		this.m_textArea.SetWidth(_textAreaWidth, _textAreaRelativeTo);
		this.m_upgradeTexts = new List<UIText>();
		if (this.m_UItext != null)
		{
			this.m_textArea.DestroyChildren();
		}
		if (_shadow)
		{
			this.m_textShadowColor = DebugDraw.ColorToHex(this.m_outlineColor);
		}
		for (int i = 0; i < _texts.Length; i++)
		{
			UIText uitext = new UIText(this.m_textArea, false, string.Empty, _texts[i], PsFontManager.GetFont(PsFonts.KGSecondChances), _fontSizes[i], RelativeTo.ScreenShortest, this.m_textColor, this.m_textShadowColor);
			if (_shadow)
			{
				uitext.SetShadowShift(new Vector2(-0.5f, -0.3f), 0.125f);
			}
			uitext.SetHorizontalAlign(0f);
			this.m_upgradeTexts.Add(uitext);
		}
		this.SetHeight(0f, RelativeTo.ScreenShortest);
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x000A4D60 File Offset: 0x000A3160
	public void SetUpgradePrice(int _coins, int _bolts, float _iconHeight)
	{
		if (this.m_upgradeCanvas != null)
		{
			this.m_upgradeCanvas.Destroy();
		}
		this.m_upgradeCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_upgradeCanvas.SetSize(_iconHeight, _iconHeight, RelativeTo.ScreenHeight);
		this.m_upgradeCanvas.SetAlign(0.55f, 1f);
		this.m_upgradeCanvas.SetMargins(0f, 0f, -0.5f, 0.5f, RelativeTo.ParentHeight);
		this.m_upgradeCanvas.SetRogue();
		this.m_upgradeCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_upgradeCanvas, "upgradePrice");
		uihorizontalList.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		if (_bolts > 0)
		{
			UIHorizontalList uihorizontalList2 = new UIHorizontalList(uihorizontalList, string.Empty);
			uihorizontalList2.SetHeight(1f, RelativeTo.ParentHeight);
			uihorizontalList2.SetSpacing(-0.5f, RelativeTo.ParentHeight);
			uihorizontalList2.SetVerticalAlign(1f);
			for (int i = 0; i < _bolts; i++)
			{
				UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_bolt_icon", null), true, true);
				uifittedSprite.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
				uifittedSprite.SetVerticalAlign(1f);
			}
		}
		UIHorizontalList uihorizontalList3 = new UIHorizontalList(uihorizontalList, string.Empty);
		uihorizontalList3.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList3.SetMargins(0.5f, 0.5f, 0f, 0f, RelativeTo.ParentHeight);
		uihorizontalList3.SetVerticalAlign(1f);
		UIText uitext = new UIText(uihorizontalList3, false, string.Empty, _coins.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.85f, RelativeTo.ParentHeight, null, null);
		uitext.SetVerticalAlign(1f);
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uihorizontalList3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null), true, true);
		uifittedSprite2.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
		uifittedSprite2.SetVerticalAlign(1f);
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x000A4F74 File Offset: 0x000A3374
	public void SetDiamondPrice(int _diamonds, float _iconHeight = 0.03f)
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.m_skipCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_skipCanvas.SetSize(_iconHeight, _iconHeight, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetAlign(1f, 1f);
		this.m_skipCanvas.SetMargins(0f, -0.045f, -0.045f, 0f, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetDepthOffset(-10f);
		this.m_skipCanvas.SetRogue();
		this.m_skipCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_skipCanvas, "SkipPrice");
		uihorizontalList.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.015f, -0.01f, -0.01f, -0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.SetDepthOffset(-15f);
		uihorizontalList.SetRogue();
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DiamondLabelBackground));
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, _diamonds.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.9f, RelativeTo.ParentHeight, "#3e3e3e", null);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x000A5104 File Offset: 0x000A3504
	public void SetRealMoneyPrice(string _amount, float _iconHeight = 0.03f)
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.m_skipCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_skipCanvas.SetSize(_iconHeight, _iconHeight, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetAlign(1f, 1f);
		this.m_skipCanvas.SetMargins(0.01f, -0.01f, -0.035f, 0.035f, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetDepthOffset(-10f);
		this.m_skipCanvas.SetRogue();
		this.m_skipCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_skipCanvas, "SkipPrice");
		uihorizontalList.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.01f, 0.01f, -0.01f, -0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.SetDepthOffset(-15f);
		uihorizontalList.SetRogue();
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DiamondLabelBackground));
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, _amount, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.9f, RelativeTo.ParentHeight, "#3e3e3e", null);
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x000A5254 File Offset: 0x000A3654
	public void SetTicketPrice(int _tickets, float _iconHeight)
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.m_skipCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_skipCanvas.SetSize(_iconHeight, _iconHeight, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetAlign(1f, 1f);
		this.m_skipCanvas.SetMargins(0f, -0.045f, -0.045f, 0f, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetDepthOffset(-10f);
		this.m_skipCanvas.SetRogue();
		this.m_skipCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_skipCanvas, "TicketPrice");
		uihorizontalList.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.015f, -0.01f, -0.01f, -0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.SetDepthOffset(-15f);
		uihorizontalList.SetRogue();
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TicketLabelBackground));
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x000A5384 File Offset: 0x000A3784
	public void SetSkipPrice(int _diamonds, float _iconHeight)
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.m_skipCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_skipCanvas.SetSize(_iconHeight * 1.2f, _iconHeight * 1.2f, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetMargins(0.01f, -0.01f, -0.035f, 0.035f, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetAlign(1f, 1f);
		this.m_skipCanvas.SetDepthOffset(-10f);
		this.m_skipCanvas.SetRogue();
		this.m_skipCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_skipCanvas, "SkipPrice");
		uihorizontalList.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.015f, -0.01f, -0.01f, -0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.SetDepthOffset(-15f);
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DiamondLabelBackground));
		if (_diamonds > 0)
		{
			this.m_priceText = new UIText(uihorizontalList, false, string.Empty, _diamonds.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.9f, RelativeTo.ParentHeight, "#3e3e3e", null);
			UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null), true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		}
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x000A5525 File Offset: 0x000A3925
	public void SetBackgroundBubble(SpeechBubbleHandlePosition _handlePosition)
	{
		this.m_backgroundBubbleHandlePosition = _handlePosition;
		this.m_drawBackgroundBubble = true;
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x000A5538 File Offset: 0x000A3938
	public void SetRentPrice(int _keys, float _iconHeight)
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.m_skipCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_skipCanvas.SetSize(_iconHeight, _iconHeight, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetMargins(2.8f, -2.8f, -1.15f, 1.15f, RelativeTo.OwnHeight);
		this.m_skipCanvas.SetDepthOffset(-10f);
		this.m_skipCanvas.SetRogue();
		this.m_skipCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_skipCanvas, "upgradePrice");
		uihorizontalList.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.005f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.KeyLabelBackground));
		if (_keys > 0)
		{
			UIText uitext = new UIText(uihorizontalList, false, string.Empty, _keys.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.9f, RelativeTo.ParentHeight, "#3e3e3e", null);
			uitext.SetDepthOffset(-5f);
			UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_key_icon", null), true, true);
			uifittedSprite.SetHeight(_iconHeight, RelativeTo.ScreenHeight);
			uifittedSprite.SetDepthOffset(-5f);
		}
		else
		{
			UIText uitext2 = new UIText(uihorizontalList, false, string.Empty, "Free!", PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.9f, RelativeTo.ParentHeight, "#3e3e3e", null);
			uitext2.SetDepthOffset(-5f);
		}
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x000A56E0 File Offset: 0x000A3AE0
	public void SetCoinPrice(int _value)
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.m_skipCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_skipCanvas.SetSize(0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.m_skipCanvas.SetMargins(2.8f, -2.8f, -1.15f, 1.15f, RelativeTo.OwnHeight);
		this.m_skipCanvas.SetDepthOffset(-10f);
		this.m_skipCanvas.SetRogue();
		this.m_skipCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_skipCanvas, string.Empty);
		uihorizontalList.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(-0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.SetAlign(1f, 1f);
		uihorizontalList.SetMargins(0.2f, 0f, 0f, 0f, RelativeTo.OwnHeight);
		uihorizontalList.RemoveDrawHandler();
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uihorizontalList, string.Empty);
		uihorizontalList2.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList2.SetMargins(0.45f, 0.6f, 0.22f, 0.22f, RelativeTo.ParentHeight);
		uihorizontalList2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CoinLabelBackground));
		uihorizontalList2.SetDepthOffset(-2f);
		UIText uitext = new UIText(uihorizontalList2, false, string.Empty, _value.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 1f, RelativeTo.ParentHeight, "#c5580f", null);
		UISprite uisprite = new UISprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_price", null), true);
		uisprite.SetSize(1.8f, 1.8f, RelativeTo.ParentHeight);
		uisprite.SetDepthOffset(-3f);
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x000A58A4 File Offset: 0x000A3CA4
	public void SetRestartAmount(int _triesLeft, int _maxTries)
	{
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.2f, RelativeTo.ScreenWidth);
		uicanvas.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uicanvas.SetRogue();
		uicanvas.SetAlign(0f, 0.5f);
		uicanvas.SetMargins(-0.2f, 0.2f, 0f, 0f, RelativeTo.ScreenWidth);
		uicanvas.SetDepthOffset(12f);
		uicanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.045f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ResourceLabelBackground));
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("item_mode_timeattack", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, _triesLeft + "/" + _maxTries, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.04f, RelativeTo.ScreenHeight, null, null);
		uitext.SetVerticalAlign(0.35f);
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x000A59FC File Offset: 0x000A3DFC
	public void SetLeagueButton(int trophies)
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.SetMargins(0.0225f, 0.0225f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetWidth(0.175f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
		uifittedSprite.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uifittedSprite.SetHorizontalAlign(0f);
		UICanvas uicanvas = new UICanvas(uifittedSprite, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.08f, -0.08f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		this.m_trophies = new UIText(uicanvas, false, string.Empty, trophies.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "#ffffff", "#7DA7C4");
		if (PsMetagameData.GetNextLeague() != null)
		{
			UIText uitext = new UIText(uiverticalList, false, string.Empty, "Next: " + PsMetagameData.GetNextLeague().m_trophyLimit + "+", PsFontManager.GetFont(PsFonts.HurmeBold), 0.02f, RelativeTo.ScreenHeight, "#8AE1FF", null);
		}
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x000A5B5C File Offset: 0x000A3F5C
	public void SetRaceButton(int _currentheat, int _purchasedRuns = 0, bool _ghostWon = false)
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.SetOrangeColors(true);
		string text = PsStrings.Get(StringID.RACE_START);
		float num = 0.2f;
		if (_currentheat > 1)
		{
			text = PsStrings.Get(StringID.RACE_AGAIN);
			num = 0.3f;
		}
		if (PsState.m_activeGameLoop is PsGameLoopRacing && _currentheat > _purchasedRuns + 5 && !_ghostWon)
		{
			text = PsStrings.Get(StringID.SHOP_TRIES_HEADER);
		}
		this.SetFittedText(text, 0.04f, num, RelativeTo.ScreenHeight, true);
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x000A5BFC File Offset: 0x000A3FFC
	public void SetNextRaceButton()
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.SetGreenColors(true);
		this.SetFittedText(PsStrings.Get(StringID.RACE_NEXT), 0.04f, 0.3f, RelativeTo.ScreenHeight, true);
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x000A5C5C File Offset: 0x000A405C
	public void SetPractiseButton()
	{
		if (this.m_skipCanvas != null)
		{
			this.m_skipCanvas.Destroy();
		}
		this.SetMargins(0.09f, 0.09f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.SetBlueColors(true);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetSpacing(-0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		new UIText(uiverticalList, false, string.Empty, "Practice", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "#ffffff", "#002140");
		new UIText(uiverticalList, false, string.Empty, "Level", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07f, RelativeTo.ScreenHeight, "#ffffff", "#002140");
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x000A5D10 File Offset: 0x000A4110
	public void CumulateTrophies(int _amount)
	{
		this.m_cumulateTrophyText = true;
		this.m_trophyCumulateAmount = _amount;
		this.m_trophyCumulateDur = 45;
		this.m_trophyCumulateCur = 0;
		TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0f, (_amount <= 0) ? 0f : 0.45f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			TweenC tweenC = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.1f, 0f, false, true);
			TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.ScaledUpEventhandler));
			this.m_scaledUp = true;
			this.Hide();
			string text = "+" + _amount;
			if (_amount < 0)
			{
				text = _amount.ToString();
			}
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_trophies, string.Empty);
			uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			UIText uitext = new UIText(uihorizontalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.04f, RelativeTo.ScreenHeight, null, null);
			uitext.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = DebugDraw.HexToColor((_amount <= 0) ? "#ff793f" : "#81f02b");
			UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true, true);
			uifittedSprite.SetSize(0.06f, 0.06f, RelativeTo.ScreenShortest);
			Vector3 vector;
			vector..ctor(0f, (float)Screen.height * 0.05f, 0f);
			if (_amount < 0)
			{
				vector..ctor(0f, (float)(-(float)Screen.height) * 0.05f, 0f);
			}
			uihorizontalList.Update();
			for (int i = 0; i < uihorizontalList.m_childs.Count; i++)
			{
				tweenC = TweenS.AddTransformTween(uihorizontalList.m_childs[i].m_TC, TweenedProperty.Alpha, TweenStyle.CubicOut, Vector3.one, Vector3.zero, 0.4f, 0.3f, true);
				if (i == 0)
				{
					TweenS.SetTweenAlphaProperties(tweenC, false, false, true, Shader.Find("Framework/FontShader"));
				}
				else
				{
					TweenS.SetTweenAlphaProperties(tweenC, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
				}
			}
			TweenC tweenC2 = TweenS.AddTransformTween(uihorizontalList.m_TC, TweenedProperty.Position, TweenStyle.CubicOut, vector, 0.75f, 0f, false);
		});
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x000A5D98 File Offset: 0x000A4198
	public void ScaledUpEventhandler(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), Vector3.one, 0.1f, 0.65f, false, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.ScaledDownEventhandler));
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x000A5DEA File Offset: 0x000A41EA
	public void ScaledDownEventhandler(TweenC _c)
	{
		this.m_scaledUp = false;
		this.Show();
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x000A5DFC File Offset: 0x000A41FC
	public void SetShopButton(string _priceText, string _label, string _iconName, bool _diamond = true, bool _coin = false, bool _bolt = false, bool _key = false)
	{
		if (this.m_shopBase != null)
		{
			this.m_shopBase.Destroy();
		}
		string text = string.Empty;
		UIDrawDelegate uidrawDelegate = new UIDrawDelegate(PsUIDrawHandlers.KeyLabelBackground);
		if (_coin)
		{
			text = "menu_resources_coin_price";
			uidrawDelegate = new UIDrawDelegate(PsUIDrawHandlers.CoinLabelBackground);
		}
		else if (_bolt)
		{
			text = "menu_resources_bolt_price";
			uidrawDelegate = new UIDrawDelegate(PsUIDrawHandlers.BoltLabelBackground);
		}
		else if (_key)
		{
			text = "menu_resources_key_price";
			uidrawDelegate = new UIDrawDelegate(PsUIDrawHandlers.KeyLabelBackground);
		}
		else if (_diamond)
		{
			text = "menu_resources_diamond_price";
			uidrawDelegate = new UIDrawDelegate(PsUIDrawHandlers.DiamondLabelBackground);
		}
		this.m_shopBase = new UIVerticalList(this, string.Empty);
		this.m_shopBase.SetWidth(0.3f, RelativeTo.ScreenHeight);
		this.m_shopBase.RemoveDrawHandler();
		this.m_shopBase.SetVerticalAlign(1f);
		UICanvas uicanvas = new UICanvas(this.m_shopBase, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.325f, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uicanvas, false, string.Empty, _label, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		UICanvas uicanvas2 = new UICanvas(this.m_shopBase, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(0.625f, RelativeTo.ParentWidth);
		uicanvas2.SetMargins(0f, 0f, -0.3f, -0.3f, RelativeTo.ParentWidth);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iconName, null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		UICanvas uicanvas3 = new UICanvas(this.m_shopBase, false, string.Empty, null, string.Empty);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(0.325f, RelativeTo.ParentWidth);
		uicanvas3.RemoveDrawHandler();
		uicanvas3.SetDepthOffset(-20f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas3, string.Empty);
		uihorizontalList.SetHeight(0.06f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.02f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDrawHandler(uidrawDelegate);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, _priceText, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 1f, RelativeTo.ParentHeight, "#3e3e3e", null);
		if (text != string.Empty)
		{
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
			uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		}
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x000A611F File Offset: 0x000A451F
	public string GetShadowColor()
	{
		if (this.m_textShadowColor != null)
		{
			return this.m_textShadowColor;
		}
		return DebugDraw.ColorToHex(this.m_outlineColor);
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x000A6143 File Offset: 0x000A4543
	public void SetSpeechHandle(SpeechBubbleHandlePosition _position, SpeechBubbleHandleType _type = SpeechBubbleHandleType.SmallToCenter)
	{
		this.m_speechBubbleHandlePosition = _position;
		this.m_speechBubbleHandleType = _type;
		this.m_speechBubbleHandler = true;
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x000A615C File Offset: 0x000A455C
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		Vector2[] array = DebugDraw.GetBezierRect(_c.m_actualWidth - this.m_cornerSize * 3f * (float)Screen.height, _c.m_actualHeight - this.m_cornerSize * 3f * (float)Screen.height, this.m_cornerSize * (float)Screen.height, 10, Vector2.zero);
		if (this.m_speechBubbleHandler)
		{
			array = DebugDraw.AddSpeechHandleToVectorArray(array, this.m_speechBubbleHandlePosition, this.m_speechBubbleHandleType);
		}
		if (array == null)
		{
			return;
		}
		float num = this.m_cornerSize * (float)Screen.height * 2f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, Vector3.forward * -1f, array, (float)Screen.width * 0.005f, this.m_outlineBottomColor, this.m_outlineTopColor, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), this.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(this.m_TC, Vector3.forward * -0.5f, ggdata, this.m_fillBottomColor, this.m_fillTopColor, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
		DebugDraw.ScaleVectorArray(array, new Vector2(0.98f, 1f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, Vector3.forward + Vector3.down * 0.001f * (float)Screen.height, array, (float)Screen.height * 0.02f, this.m_outlineColor, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), this.m_camera, Position.Center, true);
		if (this.m_drawGlare)
		{
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_glare", null);
			SpriteC spriteC = SpriteS.AddComponent(this.m_TC, frame, PsState.m_uiSheet);
			SpriteS.SetOffset(spriteC, new Vector3(0f, this.m_actualHeight * 0.5f - (float)Screen.height * 0.015f + this.m_glareYOffset * (float)Screen.height, -5f), 0f);
			SpriteS.SetDimensions(spriteC, this.m_actualWidth - num, (float)Screen.height * 0.03f);
		}
		if (this.m_drawShine)
		{
			Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
			SpriteC spriteC2 = SpriteS.AddComponent(this.m_TC, frame2, PsState.m_uiSheet);
			SpriteS.SetOffset(spriteC2, new Vector3(this.m_actualWidth * -0.5f + (float)Screen.height * 0.0175f + this.m_topShineXOffset * (float)Screen.height, this.m_actualHeight * 0.5f - (float)Screen.height * 0.015f + this.m_topShineYOffset * (float)Screen.height, -6f), 25f);
			SpriteS.SetDimensions(spriteC2, (float)Screen.height * 0.02f, (float)Screen.height * 0.01f);
			SpriteC spriteC3 = SpriteS.AddComponent(this.m_TC, frame2, PsState.m_uiSheet);
			SpriteS.SetOffset(spriteC3, new Vector3(this.m_actualWidth * 0.5f - (float)Screen.height * 0.0175f + this.m_lowerShineXOffset * (float)Screen.height, this.m_actualHeight * -0.5f + (float)Screen.height * 0.015f + this.m_lowerShineYOffset * (float)Screen.height, -6f), 205f);
			SpriteS.SetDimensions(spriteC3, (float)Screen.height * 0.015f, (float)Screen.height * 0.0085f);
			SpriteS.SetColor(spriteC3, new Color(1f, 1f, 1f, 0.6f));
		}
		SpriteS.ConvertSpritesToPrefabComponent(this.m_TC, this.m_camera, true, null);
		if (this.m_drawBackgroundBubble)
		{
			float num2 = 0.1f;
			float num3 = 0.05f;
			float num4 = _c.m_actualWidth + 0.0125f * (float)Screen.height;
			float num5 = _c.m_actualHeight + 0.0125f * (float)Screen.height;
			Vector2[] roundedRect = DebugDraw.GetRoundedRect(num4, num5, num4 * num3, 8, Vector2.zero);
			Vector2[] array2 = null;
			if (this.m_backgroundBubbleHandlePosition == SpeechBubbleHandlePosition.Right)
			{
				array2 = new Vector2[roundedRect.Length + 2];
				Array.Copy(roundedRect, 0, array2, 0, 32);
				array2[32] = new Vector2(roundedRect[0].x, roundedRect[0].y + num4 * num2);
				array2[33] = new Vector2(roundedRect[0].x + num4 * num2, roundedRect[0].y + num4 * num2 / 2f);
				array2[34] = new Vector2(roundedRect[0].x, roundedRect[0].y);
			}
			else if (this.m_backgroundBubbleHandlePosition == SpeechBubbleHandlePosition.Left)
			{
				array2 = new Vector2[roundedRect.Length + 2];
				Array.Copy(roundedRect, 0, array2, 0, 16);
				array2[16] = new Vector2(roundedRect[15].x - _c.m_actualWidth * num2, roundedRect[15].y + _c.m_actualWidth * num2 / 2f);
				array2[17] = new Vector2(roundedRect[15].x, roundedRect[15].y + _c.m_actualWidth * num2);
				Array.Copy(roundedRect, 16, array2, 18, roundedRect.Length - 16);
			}
			Color color = DebugDraw.HexToColor("#ffffff");
			Color color2 = DebugDraw.HexToColor("#000000");
			uint num6 = DebugDraw.ColorToUInt(color);
			Camera camera = CameraS.m_uiCamera;
			if (_c.m_parent != null)
			{
				camera = _c.m_parent.m_camera;
			}
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0f, (float)Screen.height * -0.0025f, 2f), array2, (float)Screen.height * 0.0125f, Color.grey * 0.5f, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, array2, (float)Screen.height * 0.0075f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array2, num6, num6, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x000A67DC File Offset: 0x000A4BDC
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		if (!this.m_customTweens && !this.m_scaledUp && !this.m_destroyed)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.Hide();
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_touchScaleTween, new TweenEventDelegate(this.EnableDraw));
			this.m_currentScale += new Vector3(0.05f, 0.05f, 0.05f);
			this.m_scaledUp = true;
		}
		base.OnTouchRollIn(_touch, _secondary);
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x000A68B8 File Offset: 0x000A4CB8
	protected override void OnTouchBegan(TLTouch _touch)
	{
		if (!this.m_customTweens && !this.m_scaledUp && !this.m_destroyed)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.Hide();
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_touchScaleTween, new TweenEventDelegate(this.EnableDraw));
			this.m_currentScale += new Vector3(0.05f, 0.05f, 0.05f);
			this.m_scaledUp = true;
		}
		base.OnTouchBegan(_touch);
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x000A6990 File Offset: 0x000A4D90
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		if (!this.m_customTweens && this.m_scaledUp && !this.m_destroyed)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.Hide();
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale - new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_touchScaleTween, new TweenEventDelegate(this.EnableDraw));
			this.m_currentScale -= new Vector3(0.05f, 0.05f, 0.05f);
			this.m_scaledUp = false;
		}
		base.OnTouchRollOut(_touch, _secondary);
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x000A6A6C File Offset: 0x000A4E6C
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		if (_inside || this.m_scaledUp)
		{
			if (_inside && (!this.m_preventHit || (this.m_preventHit && !_touch.m_dragged)))
			{
				if (!string.IsNullOrEmpty(this.m_sound))
				{
					SoundS.PlaySingleShot(this.m_sound, Vector3.zero, 1f);
				}
				if (this.m_releaseAction != null)
				{
					this.m_releaseAction.Invoke();
				}
			}
			if (!this.m_customRelease && !this.m_customTweens && this.m_scaledUp && !this.m_destroyed)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				this.Hide();
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale - new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
				TweenS.AddTweenEndEventListener(this.m_touchScaleTween, new TweenEventDelegate(this.EnableDraw));
				this.m_currentScale -= new Vector3(0.05f, 0.05f, 0.05f);
				this.m_scaledUp = false;
			}
		}
		base.OnTouchRelease(_touch, _inside);
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x000A6BC4 File Offset: 0x000A4FC4
	public void EnableDraw(TweenC _c)
	{
		TweenS.RemoveComponent(_c);
		this.m_touchScaleTween = null;
		this.Show();
		if (this.m_lateDraw && this.m_currentScale == Vector3.one)
		{
			this.d_Draw(this);
			this.m_lateDraw = false;
		}
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x000A6C18 File Offset: 0x000A5018
	public override void Step()
	{
		if (this.m_cumulateTrophyText)
		{
			int num = this.m_trophyCumulateDur - this.m_trophyCumulateCur;
			int num2 = Mathf.FloorToInt((float)this.m_trophyCumulateAmount / (float)num);
			int num3 = Convert.ToInt32(this.m_trophies.m_text);
			num3 += num2;
			this.m_trophies.SetText(num3.ToString());
			this.m_trophyCumulateAmount -= num2;
			this.m_trophyCumulateCur++;
			if (this.m_trophyCumulateCur >= this.m_trophyCumulateDur)
			{
				this.m_cumulateTrophyText = false;
			}
		}
		base.Step();
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x000A6CB5 File Offset: 0x000A50B5
	public override void Destroy()
	{
		this.m_destroyed = true;
		base.Destroy();
	}

	// Token: 0x040013FA RID: 5114
	public UIText m_UItext;

	// Token: 0x040013FB RID: 5115
	public UIFittedText m_UIFittedText;

	// Token: 0x040013FC RID: 5116
	public UIText m_priceText;

	// Token: 0x040013FD RID: 5117
	public UIFittedSprite m_UIsprite;

	// Token: 0x040013FE RID: 5118
	public UICanvas m_upgradeCanvas;

	// Token: 0x040013FF RID: 5119
	public UICanvas m_skipCanvas;

	// Token: 0x04001400 RID: 5120
	public UIVerticalList m_shopBase;

	// Token: 0x04001401 RID: 5121
	public UICanvas m_iconBase;

	// Token: 0x04001402 RID: 5122
	public List<UIText> m_upgradeTexts;

	// Token: 0x04001403 RID: 5123
	private Color m_outlineColor;

	// Token: 0x04001404 RID: 5124
	private Color m_fillTopColor;

	// Token: 0x04001405 RID: 5125
	private Color m_fillBottomColor;

	// Token: 0x04001406 RID: 5126
	private Color m_outlineTopColor;

	// Token: 0x04001407 RID: 5127
	private Color m_outlineBottomColor;

	// Token: 0x04001408 RID: 5128
	public string m_textColor;

	// Token: 0x04001409 RID: 5129
	public string m_textShadowColor;

	// Token: 0x0400140A RID: 5130
	private Color m_iconColor;

	// Token: 0x0400140B RID: 5131
	private float m_gradientSize;

	// Token: 0x0400140C RID: 5132
	private float m_gradientPos;

	// Token: 0x0400140D RID: 5133
	private float m_cornerSize;

	// Token: 0x0400140E RID: 5134
	private bool m_customRelease;

	// Token: 0x0400140F RID: 5135
	private bool m_customTweens;

	// Token: 0x04001410 RID: 5136
	public TweenC m_touchScaleTween;

	// Token: 0x04001411 RID: 5137
	public Vector3 m_currentScale;

	// Token: 0x04001412 RID: 5138
	private Vector3 m_createScale;

	// Token: 0x04001413 RID: 5139
	private bool m_scaleAtCreate;

	// Token: 0x04001414 RID: 5140
	public bool m_lateDraw;

	// Token: 0x04001415 RID: 5141
	public bool m_scaledUp;

	// Token: 0x04001416 RID: 5142
	public UIVerticalList m_textArea;

	// Token: 0x04001417 RID: 5143
	public UICanvas m_fittedTextArea;

	// Token: 0x04001418 RID: 5144
	public UICanvas m_minWidthTextArea;

	// Token: 0x04001419 RID: 5145
	private string m_sound;

	// Token: 0x0400141A RID: 5146
	private float m_glareYOffset;

	// Token: 0x0400141B RID: 5147
	private float m_topShineXOffset;

	// Token: 0x0400141C RID: 5148
	private float m_topShineYOffset;

	// Token: 0x0400141D RID: 5149
	private float m_lowerShineXOffset;

	// Token: 0x0400141E RID: 5150
	private float m_lowerShineYOffset;

	// Token: 0x0400141F RID: 5151
	public UIText m_trophies;

	// Token: 0x04001420 RID: 5152
	private int m_trophyCumulateAmount;

	// Token: 0x04001421 RID: 5153
	private int m_trophyCumulateDur;

	// Token: 0x04001422 RID: 5154
	private int m_trophyCumulateCur;

	// Token: 0x04001423 RID: 5155
	public bool m_cumulateTrophyText;

	// Token: 0x04001424 RID: 5156
	public object m_customObject;

	// Token: 0x04001425 RID: 5157
	private Action m_releaseAction;

	// Token: 0x04001426 RID: 5158
	private bool m_destroyed;

	// Token: 0x04001427 RID: 5159
	public bool m_drawGlare = true;

	// Token: 0x04001428 RID: 5160
	public bool m_drawShine = true;

	// Token: 0x04001429 RID: 5161
	private bool m_hasMinWidth;

	// Token: 0x0400142A RID: 5162
	private float m_minWidth;

	// Token: 0x0400142B RID: 5163
	private RelativeTo m_minWidthRelativeTo;

	// Token: 0x0400142C RID: 5164
	public bool m_drawBackgroundBubble;

	// Token: 0x0400142D RID: 5165
	public SpeechBubbleHandlePosition m_backgroundBubbleHandlePosition;

	// Token: 0x0400142E RID: 5166
	private SpeechBubbleHandlePosition m_speechBubbleHandlePosition;

	// Token: 0x0400142F RID: 5167
	private SpeechBubbleHandleType m_speechBubbleHandleType;

	// Token: 0x04001430 RID: 5168
	private bool m_speechBubbleHandler;
}
