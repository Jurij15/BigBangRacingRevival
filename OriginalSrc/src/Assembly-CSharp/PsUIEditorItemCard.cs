using System;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class PsUIEditorItemCard : UIRectSpriteButton
{
	// Token: 0x06001A11 RID: 6673 RVA: 0x00120A08 File Offset: 0x0011EE08
	public PsUIEditorItemCard(UIComponent _parent, PsEditorItem _editorItem, bool _sizeRelativeToParentHeight = true, bool _locked = false)
		: base(_parent, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_editor_item_card", null), true, true)
	{
		this.m_editorItem = _editorItem;
		this.m_TAC.m_letTouchesThrough = true;
		this.m_locked = _locked;
		if (_locked)
		{
			this.DisableTouchAreas(true);
		}
		if (_sizeRelativeToParentHeight)
		{
			this.SetSize(this.m_frame.width / this.m_frame.height, 1f, RelativeTo.ParentHeight);
		}
		else
		{
			this.SetSize(1f, this.m_frame.height / this.m_frame.width, RelativeTo.ParentWidth);
		}
		this.SetMargins(0.04f, 0.04f, 0.04f, 0.11f, RelativeTo.OwnHeight);
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.72f, RelativeTo.ParentHeight);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.RemoveDrawHandler();
		string text = _editorItem.m_iconImage;
		float num = 1f;
		float num2 = 0f;
		if (this.m_locked)
		{
			text = "menu_level_lock";
			num = 0.6f;
			num2 = 0.5f;
		}
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		uifittedSprite.SetHeight(num, RelativeTo.ParentHeight);
		uifittedSprite.SetVerticalAlign(num2);
		int num3 = PsMetagameManager.m_playerStats.GetEditorResourceCount(_editorItem.m_identifier);
		num3 += EditorScene.GetReservedResourceCount(_editorItem.m_identifier);
		string text2 = "cccccc";
		if (num3 > 0 && !this.m_locked)
		{
			text2 = "a1ee55";
		}
		UITextbox uitextbox = new UITextbox(this, false, string.Empty, PsStrings.Get(_editorItem.m_name), PsFontManager.GetFont(PsFonts.HurmeBold), 0.1f, RelativeTo.ParentHeight, false, Align.Center, Align.Middle, text2, true, null);
		uitextbox.SetMaxRows(2);
		uitextbox.UseDotsWhenWrapping(true);
		uitextbox.SetWidth(0.85f, RelativeTo.ParentWidth);
		if (!this.m_locked)
		{
			uitextbox.SetVerticalAlign(0.2f);
		}
		else
		{
			uitextbox.SetVerticalAlign(0f);
		}
		if (!this.m_locked)
		{
			if (num3 > 0)
			{
				UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
				uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
				uicanvas2.SetHeight(0.22f, RelativeTo.ParentHeight);
				uicanvas2.SetVerticalAlign(0f);
				uicanvas2.RemoveDrawHandler();
				uicanvas2.SetMargins(0f, 0f, 0.04f, -0.11f, RelativeTo.ParentHeight);
				string text3 = ((num3 <= 0) ? string.Empty : ("x" + num3));
				this.m_countText = new UIFittedText(uicanvas2, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeBold), false, "ffffff", "000000");
			}
			else if (_editorItem.m_currency != PsCurrency.None && _editorItem.m_currency != PsCurrency.Real)
			{
				base.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
				UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
				uihorizontalList.SetVerticalAlign(0f);
				uihorizontalList.SetHeight(0.16f, RelativeTo.ParentHeight);
				uihorizontalList.RemoveDrawHandler();
				uihorizontalList.SetSpacing(0.03f, RelativeTo.ParentHeight);
				UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, _editorItem.m_price.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), false, "ffffff", "000000");
				string text4 = string.Empty;
				PsCurrency currency = _editorItem.m_currency;
				if (currency != PsCurrency.Coin)
				{
					if (currency == PsCurrency.Gem)
					{
						text4 = "menu_resources_diamond_icon";
					}
				}
				else
				{
					text4 = "menu_resources_coin_icon";
				}
				UIFittedSprite uifittedSprite2 = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text4, null), true, true);
			}
		}
		else
		{
			base.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
		}
	}

	// Token: 0x06001A12 RID: 6674 RVA: 0x00120DF8 File Offset: 0x0011F1F8
	public void SetItemCount(int _itemCount)
	{
		if (this.m_countText != null)
		{
			if (_itemCount <= 0)
			{
				this.m_countText.SetText(string.Empty);
			}
			else
			{
				this.m_countText.SetText("x" + _itemCount);
			}
		}
	}

	// Token: 0x06001A13 RID: 6675 RVA: 0x00120E48 File Offset: 0x0011F248
	public void UpdateCard()
	{
		this.DestroyChildren();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.72f, RelativeTo.ParentHeight);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.RemoveDrawHandler();
		string text = this.m_editorItem.m_iconImage;
		float num = 1f;
		float num2 = 0f;
		if (this.m_locked)
		{
			text = "menu_level_lock";
			num = 0.6f;
			num2 = 0.5f;
		}
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		uifittedSprite.SetHeight(num, RelativeTo.ParentHeight);
		uifittedSprite.SetVerticalAlign(num2);
		int num3 = PsMetagameManager.m_playerStats.GetEditorResourceCount(this.m_editorItem.m_identifier);
		num3 += EditorScene.GetReservedResourceCount(this.m_editorItem.m_identifier);
		string text2 = "cccccc";
		if (num3 > 0 && !this.m_locked)
		{
			text2 = "a1ee55";
		}
		UITextbox uitextbox = new UITextbox(this, false, string.Empty, PsStrings.Get(this.m_editorItem.m_name), PsFontManager.GetFont(PsFonts.HurmeBold), 0.1f, RelativeTo.ParentHeight, false, Align.Center, Align.Middle, text2, true, null);
		uitextbox.SetMaxRows(2);
		uitextbox.UseDotsWhenWrapping(true);
		uitextbox.SetWidth(0.85f, RelativeTo.ParentWidth);
		if (!this.m_locked)
		{
			uitextbox.SetVerticalAlign(0.2f);
		}
		else
		{
			uitextbox.SetVerticalAlign(0f);
		}
		if (!this.m_locked)
		{
			this.EnableTouchAreas(true);
			if (num3 > 0)
			{
				base.SetOverrideShader(Shader.Find("Framework/VertexColorUnlitDouble"));
				UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
				uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
				uicanvas2.SetHeight(0.22f, RelativeTo.ParentHeight);
				uicanvas2.SetVerticalAlign(0f);
				uicanvas2.RemoveDrawHandler();
				uicanvas2.SetMargins(0f, 0f, 0.04f, -0.11f, RelativeTo.ParentHeight);
				string text3 = ((num3 <= 0) ? string.Empty : ("x" + num3));
				this.m_countText = new UIFittedText(uicanvas2, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeBold), false, "ffffff", "000000");
			}
			else if (this.m_editorItem.m_currency != PsCurrency.None && this.m_editorItem.m_currency != PsCurrency.Real)
			{
				base.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
				UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
				uihorizontalList.SetVerticalAlign(0f);
				uihorizontalList.SetHeight(0.16f, RelativeTo.ParentHeight);
				uihorizontalList.RemoveDrawHandler();
				uihorizontalList.SetSpacing(0.03f, RelativeTo.ParentHeight);
				UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, this.m_editorItem.m_price.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), false, "ffffff", "000000");
				string text4 = string.Empty;
				PsCurrency currency = this.m_editorItem.m_currency;
				if (currency != PsCurrency.Coin)
				{
					if (currency == PsCurrency.Gem)
					{
						text4 = "menu_resources_diamond_icon";
					}
				}
				else
				{
					text4 = "menu_resources_coin_icon";
				}
				UIFittedSprite uifittedSprite2 = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text4, null), true, true);
			}
		}
		else
		{
			base.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
			this.DisableTouchAreas(true);
		}
		this.Update();
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x001211CC File Offset: 0x0011F5CC
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		base.TouchHandler(_touchArea, _touchPhase, _touchIsSecondary, _touchCount, _touches);
		if (_touchIsSecondary)
		{
			return;
		}
		if (_touchPhase == TouchAreaPhase.Began)
		{
			this.m_detectingDrag = true;
		}
		else if (_touchPhase == TouchAreaPhase.ReleaseIn && !_touchArea.m_wasDragged)
		{
			this.m_detectingDrag = false;
			Vector3 vector = CameraS.m_mainCamera.transform.position;
			vector += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
			this.PlaceUnit(vector);
		}
		else if (_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut)
		{
			this.m_detectingDrag = false;
			if (this.m_dragging)
			{
				this.m_dragging = false;
				this.m_fallBackTimer = 0.2f;
			}
		}
		else if (this.m_detectingDrag)
		{
			if ((_touches[0].m_currentPosition - _touches[0].m_startPosition).y / (float)Screen.height > 0.035f)
			{
				this.m_detectingDrag = false;
				this.m_dragging = true;
				this.m_originalY = this.m_TC.transform.localPosition.y;
			}
		}
		else if (this.m_dragging)
		{
			float num = Mathf.Max(this.m_originalY, this.m_TC.transform.localPosition.y + _touches[0].m_deltaPosition.y);
			TransformS.SetPosition(this.m_TC, new Vector3(this.m_TC.transform.localPosition.x, num, this.m_TC.transform.localPosition.z));
			if (num == this.m_originalY)
			{
				this.m_fallBackTimer = 0f;
				this.m_dragging = false;
				this.m_detectingDrag = true;
			}
			else if (num >= this.m_originalY + this.m_actualHeight)
			{
				this.m_dragging = false;
				this.m_fallBackTimer = 0.2f;
				if (this.PlaceUnit(TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, _touches[0].m_currentPosition, 0f)))
				{
					BasicGizmo basicGizmo;
					if (GizmoManager.m_parentGizmos != null && GizmoManager.m_parentGizmos.Count > 0)
					{
						basicGizmo = GizmoManager.m_parentGizmos[0];
					}
					else
					{
						GizmoManager.m_multiGizmo = new MultiGizmo(GizmoManager.m_selection);
						basicGizmo = GizmoManager.m_multiGizmo;
					}
					if (basicGizmo.m_TAC != null)
					{
						TouchAreaS.SwitchTouchToAnotherTouchArea(_touches[0], basicGizmo.m_TAC);
						basicGizmo.m_TAC.d_TouchEventDelegate(basicGizmo.m_TAC, _touches[0].m_primaryPhase, false, 1, new TLTouch[] { _touches[0] });
						Vector3 vector2 = BasicGizmo.LimitInsideDome(basicGizmo.m_worldTC.transform.position);
						Vector3 vector3 = basicGizmo.m_worldTC.transform.position - vector2;
						basicGizmo.MoveGizmo(vector2);
						for (int i = 0; i < GizmoManager.m_childGizmos.Count; i++)
						{
							bool flag = true;
							for (int j = 0; j < GizmoManager.m_childGizmos[i].m_graphElements.Count; j++)
							{
								GraphNode graphNode = GizmoManager.m_childGizmos[i].m_graphElements[j] as GraphNode;
								if (!graphNode.m_parentElement.m_moveChilds)
								{
									flag = false;
								}
							}
							if (flag)
							{
								Vector3 vector4 = GizmoManager.m_childGizmos[i].m_worldTC.transform.position - vector3;
								GizmoManager.m_childGizmos[i].MoveGizmo(vector4);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x00121588 File Offset: 0x0011F988
	public override void Step()
	{
		if (this.m_fallBackTimer != 0f)
		{
			this.m_fallBackTimer = Mathf.Max(0f, this.m_fallBackTimer - Time.deltaTime);
			float num = 1f - this.m_fallBackTimer / 0.2f;
			TransformS.SetPosition(this.m_TC, new Vector3(this.m_TC.transform.localPosition.x, Mathf.Lerp(this.m_TC.transform.localPosition.y, this.m_originalY, num), this.m_TC.transform.localPosition.z));
		}
		base.Step();
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x00121640 File Offset: 0x0011FA40
	private bool PlaceUnit(Vector3 _position)
	{
		if (PsMetagameData.CanPlaceUnit(this.m_editorItem.m_identifier))
		{
			if (PsMetagameManager.m_playerStats.GetEditorResourceCount(this.m_editorItem.m_identifier) + EditorScene.GetReservedResourceCount(this.m_editorItem.m_identifier) > 0)
			{
				this.CreateNewObject(_position);
				return true;
			}
			if (this.m_editorItem.m_currency != PsCurrency.None && this.m_editorItem.m_currency != PsCurrency.Real)
			{
				SoundS.PlaySingleShot("/UI/UpgradeSelect", Vector2.zero, 1f);
				EditorBaseState editorBaseState = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() as EditorBaseState;
				editorBaseState.ShowEditorItemPurchasePopup(this.m_editorItem, delegate
				{
					EditorScene.m_cumulateEditorItemsDelegate(this.m_editorItem.m_identifier, 1);
				});
			}
		}
		return false;
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x00121708 File Offset: 0x0011FB08
	public void CreateNewObject(Vector3 _position)
	{
		_position.z = 0f;
		Vector3 zero = Vector3.zero;
		Vector3 one = Vector3.one;
		object[] array = new object[]
		{
			GraphNodeType.Normal,
			Type.GetType(this.m_editorItem.m_identifier),
			this.m_editorItem.m_identifier,
			_position,
			zero,
			one
		};
		GraphElement graphElement = (GraphElement)Activator.CreateInstance(Type.GetType(this.m_editorItem.m_graphNodeClassName), array);
		LevelManager.m_currentLevel.m_currentLayer.AddElement(graphElement);
		graphElement.Assemble();
		if (graphElement != null)
		{
			EditorBaseState.CreateTransformGizmo(graphElement);
		}
		SoundS.PlaySingleShot("/UI/ItemCreate", Vector3.zero, 1f);
		if (PsState.UsingEditorResources())
		{
			EditorScene.CumulateReservedResources(this.m_editorItem.m_identifier, -1);
		}
		LevelManager.m_currentLevel.ItemChanged();
		if (graphElement.m_assembledClasses != null && graphElement.m_assembledClasses.Count > 0 && graphElement.m_assembledClasses[0] is CCPlatform && PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_playerNode != null && PsState.m_editorCamTarget != null)
		{
			Vector3 position = PsState.m_activeMinigame.m_playerNode.m_position;
			PsState.m_editorCameraPos = position;
			PsState.m_editorCamTarget.targetTC.transform.position = position;
		}
	}

	// Token: 0x04001C76 RID: 7286
	public PsEditorItem m_editorItem;

	// Token: 0x04001C77 RID: 7287
	private UIFittedText m_countText;

	// Token: 0x04001C78 RID: 7288
	public bool m_locked;

	// Token: 0x04001C79 RID: 7289
	private bool m_detectingDrag;

	// Token: 0x04001C7A RID: 7290
	private bool m_dragging;

	// Token: 0x04001C7B RID: 7291
	private float m_originalY;

	// Token: 0x04001C7C RID: 7292
	private float m_fallBackTimer;
}
