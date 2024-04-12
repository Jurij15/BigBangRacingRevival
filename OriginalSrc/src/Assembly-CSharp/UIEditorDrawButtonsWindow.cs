using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038D RID: 909
public class UIEditorDrawButtonsWindow : UICanvas
{
	// Token: 0x06001A2D RID: 6701 RVA: 0x00122D10 File Offset: 0x00121110
	public UIEditorDrawButtonsWindow(UIScrollableCanvas _parent, string _tag)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.SetWidth(0.5f, RelativeTo.ParentHeight);
		this.SetHeight(0.7f, RelativeTo.ParentHeight);
		this.SetAlign(PsState.m_drawButtonWindowPosition, 0f);
		this.SetDepthOffset(20f);
		this.RemoveDrawHandler();
		this.m_materialBGs = new List<PrefabC>();
		this.m_addAreaContainer = new UIComponent(this, false, "DrawButtonAdd", null, null, string.Empty);
		this.m_addAreaContainer.RemoveDrawHandler();
		this.m_addAreaContainer.SetSize(0.23f, 0.23f, RelativeTo.ScreenShortest);
		if (!PsState.m_editorIsLefty)
		{
			this.m_addAreaContainer.SetAlign(0f, 1f);
		}
		else
		{
			this.m_addAreaContainer.SetAlign(1f, 1f);
		}
		this.m_addArea = new UIComponent(this.m_addAreaContainer, false, "button", null, null, string.Empty);
		this.m_addArea.RemoveDrawHandler();
		this.m_addArea.SetSize(0.2f, 0.2f, RelativeTo.ScreenShortest);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("editor_draw_sensory_button_draw", null);
		if (PsState.m_editorIsLefty)
		{
			frame.flipX = true;
		}
		this.m_drawAddButton = new UIFittedSprite(this.m_addArea, true, "rim", PsState.m_uiSheet, frame, true, true);
		this.m_subAreaContainer = new UIComponent(this, false, "DrawButtonSub", null, null, string.Empty);
		this.m_subAreaContainer.RemoveDrawHandler();
		this.m_subAreaContainer.SetSize(0.23f, 0.23f, RelativeTo.ScreenShortest);
		if (!PsState.m_editorIsLefty)
		{
			this.m_subAreaContainer.SetAlign(0.25f, 0.55f);
		}
		else
		{
			this.m_subAreaContainer.SetAlign(0.75f, 0.55f);
		}
		this.m_subArea = new UIComponent(this.m_subAreaContainer, false, "button", null, null, string.Empty);
		this.m_subArea.RemoveDrawHandler();
		this.m_subArea.SetSize(0.2f, 0.2f, RelativeTo.ScreenShortest);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("editor_draw_sensory_button_erase", null);
		if (PsState.m_editorIsLefty)
		{
			frame2.flipX = true;
		}
		this.m_drawSubButton = new UIFittedSprite(this.m_subArea, true, "rim", PsState.m_uiSheet, frame2, true, true);
		this.Update();
		this.RegenerateDrawButtonBackgrounds();
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x00122F8C File Offset: 0x0012138C
	private PrefabC CreateMaterialPrefab(TransformC _tc, Vector3 _offset, Camera _camera)
	{
		float num = 0.2f * (float)Mathf.Min(Screen.height, Screen.width);
		Vector2[] array = new Vector2[]
		{
			new Vector2(0.275f * num, 0.9f * num),
			new Vector2(0.725f * num, 0.9f * num),
			new Vector2(0.925f * num, 0.5f * num),
			new Vector2(0.8f * num, 0.125f * num),
			new Vector2(0.5f * num, 0.05f * num),
			new Vector2(0.2f * num, 0.2f * num),
			new Vector2(0.075f * num, 0.6f * num)
		};
		if (PsState.m_editorIsLefty)
		{
			Vector2[] array2 = new Vector2[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Vector2[] array3 = array;
				int num2 = i;
				array3[num2].x = array3[num2].x * -1f;
				array2[array.Length - i - 1] = array[i];
			}
			array2.CopyTo(array, 0);
		}
		uint num3 = DebugDraw.ColorToUInt(Color.white);
		Material material;
		if (PsState.m_drawLayer == 0)
		{
			material = ResourceManager.GetMaterial("SandButtonMat_Material");
		}
		else if (PsState.m_drawLayer == 1)
		{
			material = ResourceManager.GetMaterial("IceButtonMat_Material");
		}
		else if (PsState.m_drawLayer == 2)
		{
			material = ResourceManager.GetMaterial("MudButtonMat_Material");
		}
		else if (PsState.m_drawLayer == 3)
		{
			material = ResourceManager.GetMaterial("MetalButtonMat_Material");
		}
		else if (PsState.m_drawLayer == 4)
		{
			material = ResourceManager.GetMaterial("DangerButtonMat_Material");
		}
		else if (PsState.m_drawLayer == 5)
		{
			material = ResourceManager.GetMaterial("LavaButtonMat_Material");
		}
		else if (PsState.m_drawLayer == 6)
		{
			material = ResourceManager.GetMaterial("ConcreteButtonMat_Material");
		}
		else
		{
			material = ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material);
		}
		UVRect uvrect = new UVRect(0f, 0f, 1f, 1f);
		return PrefabS.CreateFlatPrefabComponentsFromVectorArray(_tc, _offset, array, num3, num3, material, _camera, string.Empty, uvrect);
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x00123208 File Offset: 0x00121608
	protected void MoveHandler(TouchAreaC _touchArea, int _touchCount, TLTouch[] _touches, TouchAreaPhase[] _touchPhases, bool[] _touchIsSecondary)
	{
		if (_touchPhases[0] == TouchAreaPhase.Began)
		{
			this.m_dragStartPosOffset = this.m_TC.transform.position - _touches[0].m_currentPosition;
			this.m_dragging = true;
		}
		else if (this.m_dragging && (_touchPhases[0] == TouchAreaPhase.MoveIn || _touchPhases[0] == TouchAreaPhase.MoveOut))
		{
			TransformS.SetPosition(this.m_TC, _touches[0].m_currentPosition + this.m_dragStartPosOffset);
		}
		else if (_touchPhases[0] == TouchAreaPhase.DragEnd)
		{
			this.m_dragging = true;
		}
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x001232AC File Offset: 0x001216AC
	public void RegenerateDrawButtonBackgrounds()
	{
		while (this.m_materialBGs.Count > 0)
		{
			int num = this.m_materialBGs.Count - 1;
			PrefabS.RemoveComponent(this.m_materialBGs[num], true);
			this.m_materialBGs.RemoveAt(num);
		}
		if (PsState.m_editorIsLefty)
		{
			if (this.m_addPressed || (!this.m_addPressed && !this.m_subPressed))
			{
				this.m_materialBGs.Add(this.CreateMaterialPrefab(this.m_addArea.m_TC, new Vector3(this.m_addArea.m_actualWidth * 0.5f, this.m_addArea.m_actualHeight * -0.5f, 2f), this.m_addArea.m_camera));
			}
			if (this.m_subPressed || (!this.m_addPressed && !this.m_subPressed))
			{
				this.m_materialBGs.Add(this.CreateMaterialPrefab(this.m_subArea.m_TC, new Vector3(this.m_subArea.m_actualWidth * 0.5f, this.m_subArea.m_actualHeight * -0.5f, 2f), this.m_subArea.m_camera));
			}
		}
		else
		{
			if (this.m_addPressed || (!this.m_addPressed && !this.m_subPressed))
			{
				this.m_materialBGs.Add(this.CreateMaterialPrefab(this.m_addArea.m_TC, new Vector3(this.m_addArea.m_actualWidth * -0.5f, this.m_addArea.m_actualHeight * -0.5f, 2f), this.m_addArea.m_camera));
			}
			if (this.m_subPressed || (!this.m_addPressed && !this.m_subPressed))
			{
				this.m_materialBGs.Add(this.CreateMaterialPrefab(this.m_subArea.m_TC, new Vector3(this.m_subArea.m_actualWidth * -0.5f, this.m_subArea.m_actualHeight * -0.5f, 2f), this.m_subArea.m_camera));
			}
		}
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x001234DC File Offset: 0x001218DC
	public void SetDrawButtonsActivity(bool _active)
	{
		if (_active != this.m_buttonsActive)
		{
			EntityManager.SetActivityOfEntity(this.m_addArea.m_TC.p_entity, _active, true, true, true, true, true);
			EntityManager.SetActivityOfEntity(this.m_subArea.m_TC.p_entity, _active, true, true, true, true, true);
			this.m_buttonsActive = _active;
		}
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x00123534 File Offset: 0x00121934
	public void ResetDrawButtons(bool _showUI = false)
	{
		this.m_addPressed = false;
		this.m_subPressed = false;
		this.m_addReleased = true;
		this.m_subReleased = true;
		this.m_drawAddButton.m_began = false;
		this.m_drawAddButton.m_end = false;
		this.m_drawAddButton.m_hit = false;
		this.m_drawSubButton.m_began = false;
		this.m_drawSubButton.m_end = false;
		this.m_drawSubButton.m_hit = false;
		PsState.m_addDown = false;
		PsState.m_subDown = false;
		if (this.m_drawSubHighlight != null)
		{
			this.m_drawSubHighlight.Destroy();
		}
		if (this.m_drawAddHighlight != null)
		{
			this.m_drawAddHighlight.Destroy();
		}
		this.m_drawAddHighlight = null;
		this.m_drawSubHighlight = null;
		if (_showUI)
		{
			EntityManager.SetActivityOfEntitiesWithTag(new string[] { "DrawMenu", "MinigameMenu", "EditorMenu", "ObjectMenu", "DrawModeButton", "DrawButtonSub", "DrawButtonAdd" }, true, true, true, true, true, false);
		}
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x0012363C File Offset: 0x00121A3C
	public override void Step()
	{
		if (!this.m_buttonsActive)
		{
			return;
		}
		this.m_drawToggle = (this.m_subToggle = false);
		if (this.m_subReleased && (this.m_drawAddButton.m_began || Input.GetKeyDown(304)) && !this.m_drawToggle && !this.m_addPressed && this.m_addReleased)
		{
			this.touchBegan = Main.m_EPOCHSeconds;
			if (!Input.GetKeyDown(304))
			{
				this.m_drawToggle = true;
			}
			EditorBaseState.RemoveTransformGizmo();
			if (PsState.m_currentTool == EditorTool.None)
			{
				PsState.m_currentTool = EditorTool.Paint;
			}
			PsState.m_addDown = true;
			this.m_addPressed = true;
			this.m_addReleased = false;
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("editor_draw_sensory_button_bg", null);
			if (PsState.m_editorIsLefty)
			{
				frame.flipX = true;
			}
			if (this.m_drawAddHighlight != null)
			{
				this.m_drawAddHighlight.Destroy();
			}
			this.m_drawAddHighlight = new UIFittedSprite(this.m_addAreaContainer, true, "highlight", PsState.m_uiSheet, frame, true, true);
			this.m_drawAddHighlight.SetDepthOffset(5f);
			this.Update();
			EntityManager.SetActivityOfEntitiesWithTag(new string[] { "DrawMenu", "MinigameMenu", "EditorMenu", "ObjectMenu", "DrawModeButton", "DrawButtonSub" }, false, true, true, true, true, false);
			if (this.m_hintText != null)
			{
				this.m_hintText.Destroy();
			}
			this.m_hintText = new UIText(null, false, string.Empty, PsStrings.Get(StringID.EDITOR_HELP_TEXT_DRAW), PsFontManager.GetFont(PsFonts.HurmeRegular), 0.04f, RelativeTo.ScreenShortest, null, null);
			this.m_hintText.SetAlign(0.5f, 1f);
			this.m_hintText.SetMargins(0.02f, RelativeTo.ScreenShortest);
			this.m_hintText.Update();
			SoundS.PlaySingleShot("/UI/ButtonDraw", Vector3.zero, 1f);
		}
		else if ((this.m_addPressed && (this.m_drawAddButton.m_end || Input.GetKeyUp(304)) && !this.m_drawToggle) || (this.m_drawToggle && this.m_drawAddButton.m_began))
		{
			this.m_drawToggle = false;
			if (PsState.m_currentTool == EditorTool.Paint)
			{
				PsState.m_currentTool = EditorTool.None;
			}
			PsState.m_addDown = false;
			this.m_addReleased = true;
			this.m_addPressed = false;
			this.m_drawAddHighlight.Destroy();
			this.m_drawAddHighlight = null;
			this.Update();
			EntityManager.SetActivityOfEntitiesWithTag(new string[] { "DrawMenu", "MinigameMenu", "EditorMenu", "ObjectMenu", "DrawModeButton", "DrawButtonSub" }, true, true, true, true, true, false);
			if (this.m_hintText != null)
			{
				this.m_hintText.Destroy();
				this.m_hintText = null;
			}
		}
		else if (this.m_addReleased && (this.m_drawSubButton.m_began || Input.GetKeyDown(308)) && !this.m_subToggle && !this.m_subPressed && this.m_subReleased)
		{
			this.touchBegan = Main.m_EPOCHSeconds;
			if (!Input.GetKeyDown(308))
			{
				this.m_subToggle = true;
			}
			EditorBaseState.RemoveTransformGizmo();
			if (PsState.m_currentTool == EditorTool.None)
			{
				PsState.m_currentTool = EditorTool.Paint;
			}
			PsState.m_subDown = true;
			this.m_subPressed = true;
			this.m_subReleased = false;
			Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("editor_draw_sensory_button_bg", null);
			if (PsState.m_editorIsLefty)
			{
				frame2.flipX = true;
			}
			if (this.m_drawSubHighlight != null)
			{
				this.m_drawSubHighlight.Destroy();
			}
			this.m_drawSubHighlight = new UIFittedSprite(this.m_subAreaContainer, true, "highlight", PsState.m_uiSheet, frame2, true, true);
			this.m_drawSubHighlight.SetDepthOffset(5f);
			this.Update();
			EntityManager.SetActivityOfEntitiesWithTag(new string[] { "DrawMenu", "MinigameMenu", "EditorMenu", "ObjectMenu", "DrawModeButton", "DrawButtonAdd" }, false, true, true, true, true, false);
			if (this.m_hintText != null)
			{
				this.m_hintText.Destroy();
			}
			this.m_hintText = new UIText(null, false, string.Empty, PsStrings.Get(StringID.EDITOR_HELP_TEXT_ERASE), PsFontManager.GetFont(PsFonts.HurmeRegular), 0.04f, RelativeTo.ScreenShortest, null, null);
			this.m_hintText.SetAlign(0.5f, 1f);
			this.m_hintText.SetMargins(0.02f, RelativeTo.ScreenShortest);
			this.m_hintText.Update();
			SoundS.PlaySingleShot("/UI/ButtonErase", Vector3.zero, 1f);
		}
		else if ((this.m_subPressed && (this.m_drawSubButton.m_end || Input.GetKeyUp(308)) && !this.m_subToggle) || (this.m_subToggle && this.m_drawSubButton.m_began))
		{
			this.m_subToggle = false;
			if (PsState.m_currentTool == EditorTool.Paint)
			{
				PsState.m_currentTool = EditorTool.None;
			}
			PsState.m_subDown = false;
			this.m_subPressed = false;
			this.m_subReleased = true;
			this.m_drawSubHighlight.Destroy();
			this.m_drawSubHighlight = null;
			this.Update();
			EntityManager.SetActivityOfEntitiesWithTag(new string[] { "DrawMenu", "MinigameMenu", "EditorMenu", "ObjectMenu", "DrawModeButton", "DrawButtonAdd" }, true, true, true, true, true, false);
			if (this.m_hintText != null)
			{
				this.m_hintText.Destroy();
				this.m_hintText = null;
			}
		}
		base.Step();
	}

	// Token: 0x04001C97 RID: 7319
	private UIFittedSprite m_drawAddButton;

	// Token: 0x04001C98 RID: 7320
	private UIFittedSprite m_drawSubButton;

	// Token: 0x04001C99 RID: 7321
	private UIFittedSprite m_drawAddHighlight;

	// Token: 0x04001C9A RID: 7322
	private UIFittedSprite m_drawSubHighlight;

	// Token: 0x04001C9B RID: 7323
	private UIComponent m_addAreaContainer;

	// Token: 0x04001C9C RID: 7324
	private UIComponent m_subAreaContainer;

	// Token: 0x04001C9D RID: 7325
	private List<PrefabC> m_materialBGs;

	// Token: 0x04001C9E RID: 7326
	private UIText m_hintText;

	// Token: 0x04001C9F RID: 7327
	private Vector3 m_dragStartPosOffset;

	// Token: 0x04001CA0 RID: 7328
	private bool m_dragging;

	// Token: 0x04001CA1 RID: 7329
	private UIComponent m_addArea;

	// Token: 0x04001CA2 RID: 7330
	private UIComponent m_subArea;

	// Token: 0x04001CA3 RID: 7331
	public bool m_buttonsActive = true;

	// Token: 0x04001CA4 RID: 7332
	public bool m_toggleMode;

	// Token: 0x04001CA5 RID: 7333
	public float m_toggleTreshold = 0.225f;

	// Token: 0x04001CA6 RID: 7334
	private bool m_addPressed;

	// Token: 0x04001CA7 RID: 7335
	private bool m_subPressed;

	// Token: 0x04001CA8 RID: 7336
	private bool m_addReleased = true;

	// Token: 0x04001CA9 RID: 7337
	private bool m_subReleased = true;

	// Token: 0x04001CAA RID: 7338
	private bool m_drawToggle;

	// Token: 0x04001CAB RID: 7339
	private bool m_subToggle;

	// Token: 0x04001CAC RID: 7340
	private double touchBegan;
}
