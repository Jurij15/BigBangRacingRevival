using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class PsUIEditorItemSelector : UICanvas
{
	// Token: 0x06001A1D RID: 6685 RVA: 0x00121BEC File Offset: 0x0011FFEC
	public PsUIEditorItemSelector(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		int num = ((!PsState.m_adminMode) ? (PsMetagameData.m_units.Count - 1) : PsMetagameData.m_units.Count);
		this.m_tabs = new UISprite[num];
		if (PsUIEditorItemSelector.m_tabPositions == null || PsUIEditorItemSelector.m_tabPositions.Length != num)
		{
			PsUIEditorItemSelector.m_tabPositions = new float[num];
		}
		if (PsUIEditorItemSelector.m_activeTabIndex >= num)
		{
			PsUIEditorItemSelector.m_activeTabIndex = 0;
		}
		this.m_tabNames = new string[num];
		for (int i = 0; i < num; i++)
		{
			this.m_tabNames[i] = PsMetagameData.m_units[i].m_name;
		}
		this.m_activeFrame = PsState.m_uiSheet.m_atlas.GetFrame("menu_editor_tab_active", null);
		this.m_activeFrame.x += 1f;
		this.m_activeFrame.width -= 2f;
		this.m_deactiveFrame = PsState.m_uiSheet.m_atlas.GetFrame("menu_editor_tab_deactive", null);
		this.m_deactiveFrame.x += 1f;
		this.m_deactiveFrame.width -= 2f;
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(0.38f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0f);
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHeight(0.28f, RelativeTo.ParentHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		float num2 = 0f;
		float num3 = 0f;
		for (int j = 0; j < num; j++)
		{
			bool flag = j == PsUIEditorItemSelector.m_activeTabIndex;
			Frame frame = ((!flag) ? this.m_deactiveFrame : this.m_activeFrame);
			UISprite uisprite = new UISprite(uihorizontalList, true, string.Empty, PsState.m_uiSheet, frame, true);
			uisprite.SetHeight(1f, RelativeTo.ParentHeight);
			uisprite.SetWidth(uisprite.m_frame.width / uisprite.m_frame.height, RelativeTo.OwnHeight);
			this.CreateTabText(uisprite, this.m_tabNames[j], flag);
			this.m_tabs[j] = uisprite;
			num2 = frame.height;
			num3 += uisprite.m_frame.width / uisprite.m_frame.height;
		}
		num3 = (1f - num3 / ((float)Screen.width / (0.1064f * (float)Screen.height))) * 0.5f;
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_editor_tab_divider", null);
		frame2.x += 1f;
		frame2.width -= 2f;
		UISprite uisprite2 = new UISprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, frame2, true);
		uisprite2.SetWidth(num3, RelativeTo.ParentWidth);
		uisprite2.SetHeight(uisprite2.m_frame.height / num2, RelativeTo.ParentHeight);
		uisprite2.SetVerticalAlign(0f);
		uisprite2.MoveToIndexAtParentsChildList(0);
		UISprite uisprite3 = new UISprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, frame2, true);
		uisprite3.SetWidth(num3, RelativeTo.ParentWidth);
		uisprite3.SetHeight(uisprite3.m_frame.height / num2, RelativeTo.ParentHeight);
		uisprite3.SetVerticalAlign(0f);
		UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetRogue();
		uicanvas.SetAlign((!PsState.m_editorIsLefty) ? 1f : 0f, 0f);
		uicanvas.SetSize(0.2f, 0.2f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0f, 0.02f, 0.032f, -0.032f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		this.m_closeButton = new UIRectSpriteButton(uicanvas, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_button_object_close", null), false, false);
		this.m_closeButton.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_closeButton.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_closeButton.SetHorizontalAlign(1f);
		this.m_closeButton.SetDepthOffset(-2f);
		this.m_contentArea = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_contentArea.SetHeight(0.72f, RelativeTo.ParentHeight);
		this.m_contentArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_contentArea.SetVerticalAlign(0f);
		this.m_contentArea.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.EditorItemSelectorDrawhandler));
		this.m_horizontalScrollArea = new UIScrollableCanvas(this.m_contentArea, string.Empty);
		this.m_horizontalScrollArea.m_maxScrollInertialX = 200f * ((float)Screen.width / 1024f);
		this.m_horizontalScrollArea.m_maxScrollInertialY = 0f;
		this.m_horizontalScrollArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_horizontalScrollArea.SetHeight(2f, RelativeTo.ParentHeight);
		this.m_horizontalScrollArea.SetVerticalAlign(0f);
		this.m_horizontalScrollArea.SetMargins(0f, 0f, 1f, 0f, RelativeTo.ParentHeight);
		this.m_horizontalScrollArea.RemoveDrawHandler();
		this.m_horizontalScrollArea.m_currentScrollX = PsUIEditorItemSelector.m_tabPositions[PsUIEditorItemSelector.m_activeTabIndex];
		this.m_content = new UIHorizontalList(this.m_horizontalScrollArea, string.Empty);
		this.m_content.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_content.SetHorizontalAlign(0f);
		this.m_content.SetMargins(0.02f, 0.02f, 0.05f, 0.03f, RelativeTo.OwnHeight);
		this.m_content.RemoveDrawHandler();
		this.CreateContent();
		EditorScene.m_cumulateEditorItemsDelegate = delegate(string _itemIdentifier, int _count)
		{
			for (int k = 0; k < this.m_editorItemCards.Count; k++)
			{
				if (this.m_editorItemCards[k].m_editorItem.m_identifier == _itemIdentifier)
				{
					this.m_editorItemCards[k].UpdateCard();
					if (_count > 0)
					{
						TransformC transformC = this.m_editorItemCards[k].m_TC;
						TweenC tweenC = TweenS.AddTransformTween(transformC, TweenedProperty.Position, TweenStyle.Linear, transformC.transform.localPosition + new Vector3(0f, -10f, 0f), 0.1f, 0f, true);
						TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
						{
							TweenS.AddTransformTween(transformC, TweenedProperty.Position, TweenStyle.BackOut, transformC.transform.localPosition + new Vector3(0f, 10f, 0f), 0.1f, 0f, true);
						});
					}
				}
			}
			if (_count > 0)
			{
				if (this.m_closeButtonTween != null)
				{
					TweenS.RemoveComponent(this.m_closeButtonTween);
					this.m_closeButtonTween = null;
				}
				this.m_closeButtonTween = TweenS.AddTransformTween(this.m_closeButton.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.one * 1.1f, 0.1f, 0f, false);
				TweenS.AddTweenEndEventListener(this.m_closeButtonTween, delegate(TweenC _c)
				{
					this.m_closeButtonTween = TweenS.AddTransformTween(this.m_closeButton.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one, 0.1f, 0f, false);
					TweenS.AddTweenEndEventListener(this.m_closeButtonTween, delegate(TweenC _c2)
					{
						if (this.m_closeButtonTween != null)
						{
							TweenS.RemoveComponent(this.m_closeButtonTween);
							this.m_closeButtonTween = null;
						}
					});
				});
			}
		};
	}

	// Token: 0x06001A1E RID: 6686 RVA: 0x001221D0 File Offset: 0x001205D0
	public override void Update()
	{
		base.Update();
		PsState.m_editorCameraExtraBorder.b = this.m_actualHeight / 2f;
		this.ResizeTAC();
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x001221F4 File Offset: 0x001205F4
	public void UpdateCards()
	{
		for (int i = 0; i < this.m_editorItemCards.Count; i++)
		{
			this.m_editorItemCards[i].UpdateCard();
		}
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x00122230 File Offset: 0x00120630
	private void ResizeTAC()
	{
		if (this.m_horizontalScrollArea != null)
		{
			TouchAreaS.ResizeRectCollider(this.m_horizontalScrollArea.m_TAC, this.m_horizontalScrollArea.m_actualWidth, this.m_horizontalScrollArea.m_actualHeight - this.m_horizontalScrollArea.m_actualMargins.t, new Vector2(0f, -this.m_horizontalScrollArea.m_actualMargins.t * 0.5f));
		}
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x001222A0 File Offset: 0x001206A0
	private void CreateTabText(UIComponent _parent, string _text, bool _active)
	{
		UIText uitext = new UIText(_parent, false, string.Empty, PsStrings.Get(_text), PsFontManager.GetFont(PsFonts.HurmeBold), (!_active) ? 0.032f : 0.039f, RelativeTo.ScreenHeight, (!_active) ? "8fd0e5" : "ffffff", "0000aa");
		uitext.SetVerticalAlign(0.62f);
	}

	// Token: 0x06001A22 RID: 6690 RVA: 0x00122304 File Offset: 0x00120704
	public override void Step()
	{
		for (int i = 0; i < this.m_tabs.Length; i++)
		{
			if (i != PsUIEditorItemSelector.m_activeTabIndex && this.m_tabs[i] != null && this.m_tabs[i].m_hit)
			{
				SoundS.PlaySingleShot("/UI/SwitchTab", Vector3.zero, 1f);
				this.ChangeTab(i);
				break;
			}
		}
		if (this.m_shopButton != null && this.m_shopButton.m_hit)
		{
			CameraS.CreateBlur(null);
			EditorBaseState editorBaseState = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() as EditorBaseState;
			editorBaseState.OpenShopPopup(PsCurrency.None);
		}
		base.Step();
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x001223BC File Offset: 0x001207BC
	private void ChangeTab(int _index)
	{
		PsUIEditorItemSelector.m_tabPositions[PsUIEditorItemSelector.m_activeTabIndex] = this.m_horizontalScrollArea.m_currentScrollX;
		this.m_tabs[PsUIEditorItemSelector.m_activeTabIndex].m_frame = this.m_deactiveFrame;
		this.m_tabs[PsUIEditorItemSelector.m_activeTabIndex].DestroyChildren();
		this.CreateTabText(this.m_tabs[PsUIEditorItemSelector.m_activeTabIndex], this.m_tabNames[PsUIEditorItemSelector.m_activeTabIndex], false);
		this.m_tabs[PsUIEditorItemSelector.m_activeTabIndex].Update();
		this.m_tabs[_index].m_frame = this.m_activeFrame;
		this.m_tabs[_index].DestroyChildren();
		this.CreateTabText(this.m_tabs[_index], this.m_tabNames[_index], true);
		this.m_tabs[_index].Update();
		PsUIEditorItemSelector.m_activeTabIndex = _index;
		this.m_content.DestroyChildren();
		this.CreateContent();
		this.m_horizontalScrollArea.SetScrollPosition(PsUIEditorItemSelector.m_tabPositions[PsUIEditorItemSelector.m_activeTabIndex], 0f);
		this.m_horizontalScrollArea.Update();
		this.ResizeTAC();
	}

	// Token: 0x06001A24 RID: 6692 RVA: 0x001224C0 File Offset: 0x001208C0
	private void CreateContent()
	{
		int num = PsMetagameManager.m_playerStats.likesEarned + PsMetagameManager.m_playerStats.megaLikesEarned * PsState.m_superLikeVisualMultiplier;
		int creatorRank = PlayerPrefsX.GetClientConfig().creatorRank3;
		int num2 = creatorRank - num;
		if (this.m_tint != null)
		{
			this.m_tint.Destroy();
			this.m_tint = null;
		}
		UIVerticalList uiverticalList = new UIVerticalList(this.m_content, string.Empty);
		uiverticalList.SetSpacing(0.08f, RelativeTo.ParentHeight);
		uiverticalList.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ParentHeight);
		uihorizontalList.SetMargins(0.18f, 0.18f, 0f, 0f, RelativeTo.ParentHeight);
		uihorizontalList.RemoveDrawHandler();
		UISprite uisprite = new UISprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true);
		uisprite.SetSize(uisprite.m_frame.width / uisprite.m_frame.height * 0.3f, 0.3f, RelativeTo.ParentHeight);
		UITextbox uitextbox = new UITextbox(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.EDITOR_ITEM_INFO_TEXT), PsFontManager.GetFont(PsFonts.HurmeBold), 0.11f, RelativeTo.ParentHeight, false, Align.Left, Align.Top, "8fd0e5", true, null);
		uitextbox.SetWidth(1f, RelativeTo.ParentHeight);
		this.m_shopButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_shopButton.SetIcon("menu_icon_chest", 0.11f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_shopButton.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_shopButton.SetDepthOffset(-2f);
		this.m_shopButton.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.m_shopButton.SetVerticalAlign(0.55f);
		UIVerticalList uiverticalList2 = new UIVerticalList(this.m_shopButton, string.Empty);
		uiverticalList2.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.BUTTON_GET_MORE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
		UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.EDITOR_BUTTON_GET_MORE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, null, null);
		if (PsUIEditorItemSelector.m_activeTabIndex == 3 && num2 > 0 && !PsState.m_adminMode)
		{
			this.m_shopButton.SetGrayColors();
			this.m_shopButton.DisableTouchAreas(true);
		}
		else
		{
			this.m_shopButton.SetBlueColors(true);
			this.m_shopButton.EnableTouchAreas(true);
		}
		if (this.m_editorItemCards != null)
		{
			this.m_editorItemCards.Clear();
		}
		else
		{
			this.m_editorItemCards = new List<PsUIEditorItemCard>();
		}
		int count = PsMetagameData.m_units[PsUIEditorItemSelector.m_activeTabIndex].m_items.Count;
		if (PsUIEditorItemSelector.m_activeTabIndex == 3 && num2 > 0 && !PsState.m_adminMode)
		{
			this.m_tint = new UICanvas(this.m_horizontalScrollArea, false, string.Empty, null, string.Empty);
			this.m_tint.SetRogue();
			this.m_tint.SetHeight(0.98f, RelativeTo.ParentHeight);
			this.m_tint.SetVerticalAlign(0f);
			this.m_tint.SetDepthOffset(-35f);
			this.m_tint.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TransparentBackground));
			this.m_horizontalScrollArea.FreezeToCamera(this.m_tint);
			string text = PsStrings.Get(StringID.EDITOR_UNLOCK_ENFORCER_INFO);
			text = text.Replace("%1", num2.ToString());
			UITextbox uitextbox2 = new UITextbox(this.m_tint, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), 0.185f, RelativeTo.ParentHeight, false, Align.Center, Align.Top, null, true, null);
			uitextbox2.SetMaxRows(4);
			uitextbox2.SetWidth(0.5f, RelativeTo.ParentWidth);
			for (int i = 0; i < count; i++)
			{
				PsEditorItem psEditorItem = PsMetagameData.m_units[PsUIEditorItemSelector.m_activeTabIndex].m_items[i] as PsEditorItem;
				this.m_editorItemCards.Add(new PsUIEditorItemCard(this.m_content, psEditorItem, true, true));
			}
		}
		else
		{
			for (int j = 0; j < count; j++)
			{
				PsEditorItem psEditorItem2 = PsMetagameData.m_units[PsUIEditorItemSelector.m_activeTabIndex].m_items[j] as PsEditorItem;
				this.m_editorItemCards.Add(new PsUIEditorItemCard(this.m_content, psEditorItem2, true, false));
			}
		}
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x0012294E File Offset: 0x00120D4E
	public override void Destroy()
	{
		PsUIEditorItemSelector.m_tabPositions[PsUIEditorItemSelector.m_activeTabIndex] = this.m_horizontalScrollArea.m_currentScrollX;
		PsState.m_editorCameraExtraBorder.b = 0f;
		EditorScene.m_cumulateEditorItemsDelegate = null;
		base.Destroy();
	}

	// Token: 0x04001C7E RID: 7294
	public const float HEIGHT = 0.38f;

	// Token: 0x04001C7F RID: 7295
	public const float TAB_AREA_HEIGHT = 0.28f;

	// Token: 0x04001C80 RID: 7296
	private const string ACTIVE_TAB_COLOR = "ffffff";

	// Token: 0x04001C81 RID: 7297
	private const string DEACTIVE_TAB_COLOR = "8fd0e5";

	// Token: 0x04001C82 RID: 7298
	private const float ACTIVE_TAB_SIZE = 0.039f;

	// Token: 0x04001C83 RID: 7299
	private const float DEACTIVE_TAB_SIZE = 0.032f;

	// Token: 0x04001C84 RID: 7300
	public static int m_activeTabIndex;

	// Token: 0x04001C85 RID: 7301
	public static float[] m_tabPositions;

	// Token: 0x04001C86 RID: 7302
	private UISprite[] m_tabs;

	// Token: 0x04001C87 RID: 7303
	private string[] m_tabNames;

	// Token: 0x04001C88 RID: 7304
	private UICanvas m_contentArea;

	// Token: 0x04001C89 RID: 7305
	private UIHorizontalList m_content;

	// Token: 0x04001C8A RID: 7306
	public UIScrollableCanvas m_horizontalScrollArea;

	// Token: 0x04001C8B RID: 7307
	private List<PsUIEditorItemCard> m_editorItemCards;

	// Token: 0x04001C8C RID: 7308
	private Frame m_activeFrame;

	// Token: 0x04001C8D RID: 7309
	private Frame m_deactiveFrame;

	// Token: 0x04001C8E RID: 7310
	public UIRectSpriteButton m_closeButton;

	// Token: 0x04001C8F RID: 7311
	private TweenC m_closeButtonTween;

	// Token: 0x04001C90 RID: 7312
	private PsUIGenericButton m_shopButton;

	// Token: 0x04001C91 RID: 7313
	private UICanvas m_tint;
}
