using System;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class PsEditorItemCard : UIRectSpriteButton
{
	// Token: 0x06000EAF RID: 3759 RVA: 0x0008854C File Offset: 0x0008694C
	public PsEditorItemCard(UIComponent _parent, Action<PsEditorItem> _action, string _name, string _description, Color _color, PsEditorItem _itemMetaData, bool _forceOverrideShader = false)
		: base(_parent, _name, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_editor_item_card", null), true, false)
	{
		this.m_action = _action;
		this.m_itemMetaDate = _itemMetaData;
		_color = Color.Lerp(_color, Color.black, 0.5f);
		this.locked = false;
		if (_itemMetaData != null && !PsState.m_adminMode && !PsState.m_unlockEditorItems)
		{
			this.locked = !_itemMetaData.m_unlocked;
		}
		if (this.locked || _forceOverrideShader)
		{
			base.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
		}
		this.SetSize(this.m_frame.width / this.m_frame.height, 1f, RelativeTo.ParentHeight);
		this.SetMargins(0.05f, RelativeTo.OwnHeight);
		this.RemoveDrawHandler();
		this.m_TAC.m_letTouchesThrough = true;
		if (_itemMetaData != null && _itemMetaData.m_iconImage != null && !_itemMetaData.m_iconImage.Equals(string.Empty))
		{
			UIFittedSprite uifittedSprite = new UIFittedSprite(this, false, "Unit", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_itemMetaData.m_iconImage, null), true, true);
			if (this.locked)
			{
				uifittedSprite.m_overrideShader = Shader.Find("WOE/Editor/ItemCardIconLocked");
			}
			uifittedSprite.m_rogue = true;
			uifittedSprite.SetDepthOffset(-15f);
			uifittedSprite.SetVerticalAlign(1.05f);
			uifittedSprite.SetWidth(0.6f, RelativeTo.ParentWidth);
		}
		UIVerticalList uiverticalList = new UIVerticalList(this, "textContainer");
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.55f, 0f, RelativeTo.ParentHeight);
		if (!this.locked)
		{
			_name = string.Concat(new string[]
			{
				"<color=#",
				ToolBox.ColorToHex(PsColors.menuLimeGreen),
				"> ",
				_name,
				" </color>"
			});
		}
		else
		{
			_name = "LOCKED";
		}
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, _name, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		if (!this.locked && _description != null)
		{
			new UITextbox(uiverticalList, false, string.Empty, _description, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.02f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		}
		if (PsState.m_newEditorItemCount > 0)
		{
			bool flag = false;
			for (int i = 0; i < PsState.m_newEditorItems.Length; i++)
			{
				if (PsState.m_newEditorItems[i] == _itemMetaData.m_identifier)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				UICanvas uicanvas = new UICanvas(this, false, "notification", null, string.Empty);
				uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
				uicanvas.SetMargins(-0.005f, 0.005f, -0.005f, 0.005f, RelativeTo.ScreenHeight);
				uicanvas.SetAlign(0f, 1f);
				uicanvas.SetDepthOffset(-25f);
				uicanvas.RemoveDrawHandler();
				UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
				uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
				uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
				uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
				TweenC tweenC = TweenS.AddTransformTween(uicanvas2.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
				TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
				UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, "!", PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			}
		}
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x00088909 File Offset: 0x00086D09
	public override void Step()
	{
		if (this.m_hit && this.m_action != null)
		{
			this.m_action.Invoke(this.m_itemMetaDate);
		}
		base.Step();
	}

	// Token: 0x040011A1 RID: 4513
	public bool locked;

	// Token: 0x040011A2 RID: 4514
	public Action<PsEditorItem> m_action;

	// Token: 0x040011A3 RID: 4515
	public PsEditorItem m_itemMetaDate;
}
