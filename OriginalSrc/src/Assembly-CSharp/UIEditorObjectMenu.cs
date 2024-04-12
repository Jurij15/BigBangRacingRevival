using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class UIEditorObjectMenu : UIScaleToContentCanvas
{
	// Token: 0x06001A45 RID: 6725 RVA: 0x001253E0 File Offset: 0x001237E0
	public UIEditorObjectMenu(UIComponent _parent, string _tag)
		: base(_parent, _tag, true, true)
	{
		this.SetAlign(1f, 0f);
		this.SetMargins(0.02f, RelativeTo.ScreenShortest);
		this.RemoveTouchAreas();
		this.RemoveDrawHandler();
		int num = PsState.m_newEditorItemCount;
		for (int i = 0; i < 6; i++)
		{
			PsUnlockable psUnlockable = PsMetagameData.m_gameMaterials[0].m_items[i];
			for (int j = 0; j < PsState.m_newEditorItems.Length; j++)
			{
				if (PsState.m_newEditorItems[j] == psUnlockable.m_identifier)
				{
					num--;
				}
			}
		}
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "Container");
		uihorizontalList.SetSpacing(0.005f, RelativeTo.ScreenShortest);
		uihorizontalList.RemoveTouchAreas();
		uihorizontalList.RemoveDrawHandler();
		this.m_complexityMeter = new UIEditorComplexityMeter(uihorizontalList, 0.7f);
		this.m_complexityMeter.SetSize(0.06f, 0.12f, RelativeTo.ScreenShortest);
		this.m_complexityMeter.SetVerticalAlign(0.2f);
		this.m_objectMenuButton = new UIRectSpriteButton(uihorizontalList, "Add Object", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_button_object_add", null), false, false);
		this.m_objectMenuButton.SetSize(0.2f, 0.2f, RelativeTo.ScreenShortest);
		if (PsState.m_editorIsLefty)
		{
			this.m_objectMenuButton.MoveToIndexAtParentsChildList(0);
		}
		if (num > 0)
		{
			UICanvas uicanvas = new UICanvas(this.m_objectMenuButton, false, "notification", null, string.Empty);
			uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
			uicanvas.SetMargins(0.01f, -0.01f, 0.01f, -0.01f, RelativeTo.ScreenHeight);
			uicanvas.SetAlign(0f, 1f);
			uicanvas.SetDepthOffset(-10f);
			uicanvas.RemoveDrawHandler();
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
			uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
			uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
			TweenC tweenC = TweenS.AddTransformTween(uicanvas2.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
			this.m_amount = new UIFittedText(uicanvas2, false, string.Empty, num.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		}
		this.Update();
		EditorScene.m_cumulateEditorItemsDelegate = delegate(string _itemIdentifier, int _count)
		{
			if (_count > 0)
			{
				if (this.m_objectButtonTween != null)
				{
					TweenS.RemoveComponent(this.m_objectButtonTween);
					this.m_objectButtonTween = null;
				}
				this.m_objectButtonTween = TweenS.AddTransformTween(this.m_objectMenuButton.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.one * 1.1f, 0.1f, 0f, false);
				TweenS.AddTweenEndEventListener(this.m_objectButtonTween, delegate(TweenC _c)
				{
					this.m_objectButtonTween = TweenS.AddTransformTween(this.m_objectMenuButton.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one, 0.1f, 0f, false);
					TweenS.AddTweenEndEventListener(this.m_objectButtonTween, delegate(TweenC _c2)
					{
						if (this.m_objectButtonTween != null)
						{
							TweenS.RemoveComponent(this.m_objectButtonTween);
							this.m_objectButtonTween = null;
						}
					});
				});
			}
		};
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x0012567F File Offset: 0x00123A7F
	public override void Step()
	{
		base.Step();
	}

	// Token: 0x04001CCF RID: 7375
	public UIRectSpriteButton m_objectMenuButton;

	// Token: 0x04001CD0 RID: 7376
	public UIEditorComplexityMeter m_complexityMeter;

	// Token: 0x04001CD1 RID: 7377
	public UIFittedText m_amount;

	// Token: 0x04001CD2 RID: 7378
	private TweenC m_objectButtonTween;
}
