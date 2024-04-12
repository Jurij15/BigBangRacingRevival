using System;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class PsUIEditorEdit : UICanvas, IPlayMenu
{
	// Token: 0x060018AA RID: 6314 RVA: 0x0010C3B4 File Offset: 0x0010A7B4
	public PsUIEditorEdit(Action _exitAction = null, Action _playAction = null)
		: base(null, false, string.Empty, null, string.Empty)
	{
		this.m_exitAction = _exitAction;
		this.m_playAction = _playAction;
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "MinigameMenu");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.CreateTopLeftArea(uihorizontalList);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this, "EditorMenu");
		uihorizontalList2.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList2.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList2.SetAlign(1f, 1f);
		uihorizontalList2.RemoveDrawHandler();
		this.CreateTopRightArea(uihorizontalList2);
		this.m_bottomRightArea = new UIHorizontalList(this, "BottomRight");
		this.m_bottomRightArea.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		this.m_bottomRightArea.SetAlign(1f, 0f);
		if (PsState.m_editorIsLefty)
		{
			this.m_bottomRightArea.SetAlign(0f, 0f);
		}
		this.m_bottomRightArea.RemoveDrawHandler();
		this.CreateBottomRightArea(this.m_bottomRightArea);
		this.m_bottomLeftArea = new UIHorizontalList(this, "BottomLeft");
		this.m_bottomLeftArea.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		this.m_bottomLeftArea.SetAlign(0f, 0f);
		if (PsState.m_editorIsLefty)
		{
			this.m_bottomLeftArea.SetAlign(1f, 0f);
		}
		this.m_bottomLeftArea.RemoveDrawHandler();
		this.CreateBottomLeftArea(this.m_bottomLeftArea);
		this.CreateAdminInfo();
		this.Update();
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x0010C550 File Offset: 0x0010A950
	public virtual void CreateAdminInfo()
	{
		if (!PsState.m_adminMode)
		{
			return;
		}
		UICanvas uicanvas = new UICanvas(this, false, "EditorMenu", null, string.Empty);
		uicanvas.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.4f, RelativeTo.ScreenWidth);
		uicanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupContentArea));
		uicanvas.SetMargins(0.1f, 0.1f, 0.1f, 0.1f, RelativeTo.OwnHeight);
		uicanvas.SetVerticalAlign(1f);
		this.m_adminInfo = new UITextbox(uicanvas, false, "AdminInfo", string.Empty, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.02f, RelativeTo.ScreenShortest, true, Align.Center, Align.Top, null, true, null);
		this.m_adminInfo.SetVerticalAlign(1f);
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x0010C618 File Offset: 0x0010AA18
	public virtual void CreateTopLeftArea(UIComponent _parent)
	{
		this.m_exitButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetText(PsStrings.Get(StringID.EDITOR_BUTTON_SAVE_EXIT), 0.025f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_exitButton.SetOrangeColors(true);
		this.m_freeWizardButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_freeWizardButton.SetOrangeColors(true);
		this.m_freeWizardButton.SetIcon("menu_icon_settings", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		if (ScreenCapture.isAvailable)
		{
			this.m_recordButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
			this.m_recordButton.SetOrangeColors(true);
			UISprite uisprite = new UISprite(this.m_recordButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_rec_cam", null), true);
			uisprite.SetSize(uisprite.m_frame.width / uisprite.m_frame.height * 0.05f, 0.05f, RelativeTo.ScreenHeight);
		}
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x0010C75F File Offset: 0x0010AB5F
	public virtual void CreateBottomLeftArea(UIComponent _parent)
	{
		this.m_drawMenu = new UIEditorDrawMenu(_parent, "DrawMenu");
		this.m_drawMenu.OpenDrawWindow(PsState.m_editorIsLefty);
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x0010C784 File Offset: 0x0010AB84
	public virtual void CreateTopRightArea(UIComponent _parent)
	{
		this.m_playButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_playButton.SetTextWithMinWidth(PsStrings.Get(StringID.EDITOR_BUTTON_DRIVE), 0.05f, 0.15f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_playButton.SetOrangeColors(true);
		this.m_playButton.SetSound("/UI/EditorMode_Play");
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x0010C7EF File Offset: 0x0010ABEF
	public virtual void CreateBottomRightArea(UIComponent _parent)
	{
		this.m_objectMenu = new UIEditorObjectMenu(_parent, "ObjectMenu");
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x0010C802 File Offset: 0x0010AC02
	public void ApplyLeftySettings()
	{
		if (this.m_drawMenu != null)
		{
			this.m_drawMenu.ApplyLeftySettings();
		}
		if (this.m_objectMenu != null)
		{
			this.m_objectMenu.SetHorizontalAlign(PsState.m_objectMenuButtonAlign);
			this.m_objectMenu.Update();
		}
	}

	// Token: 0x060018B1 RID: 6321 RVA: 0x0010C840 File Offset: 0x0010AC40
	public override void Step()
	{
		if (!this.m_TC.p_entity.m_active)
		{
			return;
		}
		if (this.m_drawMenu != null && !this.m_pressed && this.m_editorItemSelector == null)
		{
			this.m_drawMenu.UpdateDrawButtonActivity();
		}
		this.m_pressed = false;
		if (this.m_adminInfo != null && PsState.m_activeMinigame != null)
		{
			this.m_adminInfo.SetText("Admin Mode\nLevel Requirement: " + PsState.m_activeMinigame.m_levelRequirement);
		}
		if (this.m_exitButton != null && this.m_exitButton.m_hit)
		{
			this.m_pressed = true;
			if (ScreenCapture.isRecording)
			{
				PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUIRecordingPopup), null, null, null, true, true, InitialPage.Center, false, true, false);
				popup.SetAction("Close", new Action(popup.Destroy));
				popup.SetAction("Preview", delegate
				{
					popup.Destroy();
					this.m_recordButton.SetIcon("menu_icon_rec_start", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
					this.m_recordButton.m_parent.Update();
				});
				popup.SetAction("Discard", delegate
				{
					popup.Destroy();
					this.m_exitAction.Invoke();
				});
			}
			else
			{
				this.m_exitAction.Invoke();
			}
		}
		else if (this.m_playButton != null && this.m_playButton.m_hit)
		{
			this.m_pressed = true;
			this.m_playAction.Invoke();
		}
		else if (this.m_freeWizardButton != null && this.m_freeWizardButton.m_hit)
		{
			this.m_pressed = true;
			SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
			EditorBaseState.RemoveTransformGizmo();
			EntityManager.SetActivityOfEntitiesWithTag("UIComponent", false, true, true, true, false, false);
			if (PsState.m_freeWizardLastSelectedItemCategory == string.Empty)
			{
				PsState.m_freeWizardLastSelectedItemCategory = UIEditorSelectorNavigator.WIZARD_CATEGORIES[0];
			}
			if (PsState.m_freeWizardLastSelectedItemCategory == UIEditorSelectorNavigator.WIZARD_CATEGORIES[0])
			{
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorPlayer(EditorSelectorContext.FREE_WIZARD));
			}
			else if (PsState.m_freeWizardLastSelectedItemCategory == UIEditorSelectorNavigator.WIZARD_CATEGORIES[1])
			{
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorGameMode(EditorSelectorContext.FREE_WIZARD));
			}
			else
			{
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorArea(EditorSelectorContext.FREE_WIZARD));
			}
		}
		else if (this.m_recordButton != null && this.m_recordButton.m_hit)
		{
			this.m_pressed = true;
			if (ScreenCapture.isRecording)
			{
				ScreenCapture.Stop();
				ScreenCapture.Preview();
			}
			else
			{
				ScreenCapture.Start();
			}
		}
		this.CheckRecordButtonState();
		if (this.m_objectMenu != null && this.m_objectMenu.m_objectMenuButton.m_hit)
		{
			this.m_pressed = true;
			SoundS.PlaySingleShot("/UI/Popup", Vector3.zero, 1f);
			EditorBaseState editorBaseState = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() as EditorBaseState;
			editorBaseState.m_groundPicking = false;
			EntityManager.SetActivityOfEntitiesWithTag(new string[] { "DrawMenu", "DrawWindow", "EditorMenu", "MinigameMenu" }, false, true, true, true, true, false);
			this.m_drawMenu.m_drawWindow.m_buttonsActive = false;
			this.m_drawMenu.m_drawWindow.ResetDrawButtons(false);
			this.m_objectMenu.Destroy();
			this.m_objectMenu = null;
			PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
			this.m_editorItemSelector = new PsUIEditorItemSelector(this);
			this.m_editorItemSelector.Update();
			TweenC tweenC = TweenS.AddTransformTween(this.m_editorItemSelector.m_TC, TweenedProperty.Position, TweenStyle.Linear, this.m_editorItemSelector.m_TC.transform.position - new Vector3(0f, (float)Screen.height * 0.22f, 0f), this.m_editorItemSelector.m_TC.transform.position, 0.12f, 0f, true);
			TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
			{
				if (this.m_editorItemSelector != null)
				{
					this.m_editorItemSelector.Update();
				}
			});
			TweenS.UpdateTween(tweenC, 0f);
			this.m_editorItemSelector.m_horizontalScrollArea.AdjustCamera();
			float num = 0.7264f;
			this.m_cameraViewportTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.Linear, 1f, num, 0.12f, 0f);
			float num2 = CameraS.m_mainCamera.fieldOfView * 0.017453292f;
			float num3 = Mathf.Atan(Mathf.Tan(num2 * 0.5f) * (CameraS.m_mainCamera.pixelRect.width / CameraS.m_mainCamera.pixelRect.height)) * 2f;
			float num4 = Mathf.Atan(Mathf.Tan(num3 * 0.5f) * (CameraS.m_mainCamera.pixelRect.height * num / CameraS.m_mainCamera.pixelRect.width)) * 2f;
			float num5 = num4 * 57.29578f;
			this.m_cameraFOVTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.Linear, CameraS.m_mainCamera.fieldOfView, num5, 0.12f, 0f);
			this.m_cameraZoomTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.Linear, PsState.m_editorCameraZoom, PsState.m_editorCameraZoom * num, 0.12f, 0f);
		}
		else if (this.m_editorItemSelector != null && this.m_editorItemSelector.m_closeButton.m_hit)
		{
			this.m_pressed = true;
			SoundS.PlaySingleShot("/UI/ButtonBack", Vector3.zero, 1f);
			EditorBaseState editorBaseState2 = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() as EditorBaseState;
			editorBaseState2.m_groundPicking = true;
			TweenC tweenC2 = TweenS.AddTransformTween(this.m_editorItemSelector.m_TC, TweenedProperty.Position, TweenStyle.Linear, this.m_editorItemSelector.m_TC.transform.position, this.m_editorItemSelector.m_TC.transform.position - new Vector3(0f, (float)Screen.height * 0.22f, 0f), 0.12f, 0f, true);
			TweenS.AddTweenEndEventListener(tweenC2, delegate(TweenC _c)
			{
				this.m_editorItemSelector.Destroy();
				this.m_editorItemSelector = null;
				PsMetagameManager.HideResources();
				this.CreateBottomRightArea(this.m_bottomRightArea);
				this.m_bottomRightArea.Update();
				EntityManager.SetActivityOfEntitiesWithTag(new string[] { "DrawMenu", "DrawWindow", "EditorMenu", "MinigameMenu" }, true, true, true, true, true, false);
				this.m_drawMenu.m_drawWindow.m_buttonsActive = true;
				this.m_drawMenu.m_drawWindow.ResetDrawButtons(true);
			});
			this.m_cameraViewportTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.Linear, CameraS.m_mainCamera.rect.height, 1f, 0.12f, 0f);
			this.m_cameraFOVTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.Linear, CameraS.m_mainCamera.fieldOfView, 40f, 0.12f, 0f);
			this.m_cameraZoomTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.Linear, PsState.m_editorCameraZoom, PsState.m_editorCameraZoom * 1.376652f, 0.12f, 0f);
		}
		if (this.m_pressed)
		{
			TouchAreaS.CancelAllTouches(null);
			if (this.m_drawMenu != null && this.m_drawMenu.m_drawWindow != null)
			{
				this.m_drawMenu.m_drawWindow.m_buttonsActive = false;
			}
		}
		if (this.m_cameraViewportTween != null)
		{
			Rect rect = CameraS.m_mainCamera.rect;
			rect.height = this.m_cameraViewportTween.currentValue.x;
			rect.y = 1f - this.m_cameraViewportTween.currentValue.x;
			CameraS.m_mainCamera.rect = rect;
			EditorBaseState editorBaseState3 = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() as EditorBaseState;
			editorBaseState3.m_zoomLimitMultiplier = this.m_cameraViewportTween.currentValue.x;
			if (this.m_cameraViewportTween.currentValue == this.m_cameraViewportTween.endValue)
			{
				TweenS.RemoveComponent(this.m_cameraViewportTween);
				this.m_cameraViewportTween = null;
			}
			AutoGeometryManager.SetLayerShaderPropertiesInEditor(rect);
		}
		if (this.m_cameraFOVTween != null)
		{
			CameraS.m_mainCamera.fieldOfView = this.m_cameraFOVTween.currentValue.x;
			CameraS.m_mainCameraFov = this.m_cameraFOVTween.currentValue.x;
			if (this.m_cameraFOVTween.currentValue == this.m_cameraFOVTween.endValue)
			{
				TweenS.RemoveComponent(this.m_cameraFOVTween);
				this.m_cameraFOVTween = null;
			}
		}
		if (this.m_cameraZoomTween != null)
		{
			EditorBaseState editorBaseState4 = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() as EditorBaseState;
			editorBaseState4.SetZoomAmount(this.m_cameraZoomTween.currentValue.x);
			if (PsState.m_editorCamTarget != null)
			{
				PsState.m_editorCamTarget.interpolateSpeed = 0f;
			}
			if (this.m_cameraZoomTween.currentValue == this.m_cameraZoomTween.endValue)
			{
				TweenS.RemoveComponent(this.m_cameraZoomTween);
				this.m_cameraZoomTween = null;
			}
		}
		else if (PsState.m_editorCamTarget != null && PsState.m_editorCamTarget.interpolateSpeed == 0f)
		{
			PsState.m_editorCamTarget.interpolateSpeed = 0.33333f;
		}
		base.Step();
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x0010D150 File Offset: 0x0010B550
	private void CheckRecordButtonState()
	{
		if (this.m_recordButton == null)
		{
			return;
		}
		if (ScreenCapture.isRecording && !this.m_recordButtonRecordState)
		{
			this.m_recordButtonRecordState = true;
			this.m_recordButton.SetIcon("menu_icon_rec_stop", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_recordButton.m_parent.Update();
		}
		else if (!ScreenCapture.isRecording && this.m_recordButtonRecordState)
		{
			this.m_recordButtonRecordState = false;
			this.m_recordButton.SetIcon("menu_icon_rec_start", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_recordButton.m_parent.Update();
		}
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x0010D210 File Offset: 0x0010B610
	public override void Destroy()
	{
		Rect rect = CameraS.m_mainCamera.rect;
		rect.height = 1f;
		rect.y = 0f;
		CameraS.m_mainCamera.rect = rect;
		CameraS.m_mainCamera.fieldOfView = 40f;
		CameraS.m_mainCameraFov = 40f;
		PsState.m_editorCamTarget.interpolateSpeed = 0.33333f;
		base.Destroy();
	}

	// Token: 0x04001B4A RID: 6986
	protected PsUIGenericButton m_exitButton;

	// Token: 0x04001B4B RID: 6987
	protected PsUIGenericButton m_playButton;

	// Token: 0x04001B4C RID: 6988
	protected Action m_exitAction;

	// Token: 0x04001B4D RID: 6989
	protected Action m_playAction;

	// Token: 0x04001B4E RID: 6990
	protected UITextbox m_adminInfo;

	// Token: 0x04001B4F RID: 6991
	protected UIEditorDrawMenu m_drawMenu;

	// Token: 0x04001B50 RID: 6992
	protected UIEditorObjectMenu m_objectMenu;

	// Token: 0x04001B51 RID: 6993
	public PsUIEditorItemSelector m_editorItemSelector;

	// Token: 0x04001B52 RID: 6994
	protected PsUIGenericButton m_freeWizardButton;

	// Token: 0x04001B53 RID: 6995
	protected PsUIGenericButton m_recordButton;

	// Token: 0x04001B54 RID: 6996
	private UIHorizontalList m_bottomRightArea;

	// Token: 0x04001B55 RID: 6997
	private UIHorizontalList m_bottomLeftArea;

	// Token: 0x04001B56 RID: 6998
	private TweenC m_cameraViewportTween;

	// Token: 0x04001B57 RID: 6999
	private TweenC m_cameraFOVTween;

	// Token: 0x04001B58 RID: 7000
	private TweenC m_cameraZoomTween;

	// Token: 0x04001B59 RID: 7001
	private bool m_pressed;

	// Token: 0x04001B5A RID: 7002
	private bool m_recordButtonRecordState;
}
