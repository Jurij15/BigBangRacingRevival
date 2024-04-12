using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000320 RID: 800
public class PsUIBasePopup : UICanvas
{
	// Token: 0x0600177E RID: 6014 RVA: 0x000FFA18 File Offset: 0x000FDE18
	public PsUIBasePopup(Type _main, Type _top = null, Type _left = null, Type _right = null, bool _hideUIComponents = true, bool _addPopup = true, InitialPage _page = InitialPage.Center, bool _pauseUIComponents = false, bool _background = false, bool _state = false)
		: base(null, false, "UIBase", null, string.Empty)
	{
		this.m_state = _state;
		this.m_hideUIComponents = _hideUIComponents;
		this.m_pauseUICOmponents = _pauseUIComponents;
		this.m_main = _main;
		this.m_top = _top;
		this.m_left = _left;
		this.m_right = _right;
		this.m_page = _page;
		this.m_guid = Guid.NewGuid().ToString();
		this.Initialize(this.m_main, this.m_top, this.m_left, this.m_right, _addPopup, _background);
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x000FFABC File Offset: 0x000FDEBC
	private void Initialize(Type _main, Type _top, Type _left = null, Type _right = null, bool _addPopup = true, bool _background = false)
	{
		if (this.m_hideUIComponents || this.m_pauseUICOmponents)
		{
			List<Entity> list = EntityManager.SetActivityOfEntitiesWithTag("UIComponent", false, this.m_hideUIComponents, true, true, false, false);
			foreach (Entity entity in list)
			{
				EntityManager.AddTagForEntity(entity, this.m_guid);
			}
			PsState.m_UIComponentsHidden = true;
		}
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetDepthOffset(20f);
		if (_background)
		{
			this.SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		}
		else
		{
			this.RemoveDrawHandler();
		}
		this.m_utilityEntity = EntityManager.AddEntity();
		this.m_utilityEntity.m_persistent = true;
		int num = 1;
		if (_left != null)
		{
			num++;
		}
		if (_right != null)
		{
			num++;
		}
		this.m_scrollableCanvas = new UIScrollableSnappingCanvas(this, "ScrollableCanvas", num);
		this.m_scrollableCanvas.RemoveDrawHandler();
		this.m_scrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_scrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_scrollableCanvas.SetVerticalAlign(0f);
		this.m_scrollableCanvas.m_maxScrollInertialX = 200f * (float.Parse(Screen.width.ToString()) / 1024f);
		if (num == 1)
		{
			this.m_scrollableCanvas.m_maxScrollInertialX = 0f;
		}
		this.m_scrollableCanvas.m_maxScrollInertialY = 0f;
		float num2 = 0f;
		if (this.m_page == InitialPage.Center)
		{
			if (_left != null && _right == null)
			{
				this.m_scrollableCanvas.SetScrollPosition(1f, 0f);
				this.m_scrollableCanvas.m_currentPageX = 1;
				num2 = 1f;
			}
			else if (_left == null && _right != null)
			{
				this.m_scrollableCanvas.SetScrollPosition(0f, 0f);
				this.m_scrollableCanvas.m_currentPageX = 0;
			}
			else
			{
				this.m_scrollableCanvas.SetScrollPosition(0.5f, 0f);
				num2 = 0.5f;
				this.m_scrollableCanvas.m_currentPageX = 1;
			}
		}
		else if (this.m_page == InitialPage.Left)
		{
			num2 = 0f;
			this.m_scrollableCanvas.SetScrollPosition(0f, 0f);
			this.m_scrollableCanvas.m_currentPageX = 0;
		}
		else if (this.m_page == InitialPage.Right)
		{
			num2 = 1f;
			this.m_scrollableCanvas.SetScrollPosition(1f, 0f);
			if (_left == null)
			{
				this.m_scrollableCanvas.m_currentPageX = 1;
			}
			else
			{
				this.m_scrollableCanvas.m_currentPageX = 2;
			}
		}
		this.m_scrollableCanvasContent = new UIHorizontalList(this.m_scrollableCanvas, "hlist");
		this.m_scrollableCanvasContent.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_scrollableCanvasContent.SetHorizontalAlign(num2);
		this.m_scrollableCanvasContent.RemoveDrawHandler();
		object[] array = new object[] { this.m_scrollableCanvasContent };
		if (_left != null)
		{
			this.m_leftContent = Activator.CreateInstance(_left, array) as UIComponent;
		}
		if (_main != null)
		{
			this.m_mainContent = Activator.CreateInstance(_main, array) as UIComponent;
		}
		if (_right != null)
		{
			this.m_rightContent = Activator.CreateInstance(_right, array) as UIComponent;
		}
		if (_top != null)
		{
			this.m_overlayCamera = CameraS.AddCamera("OverlayCamera", true, 3);
			array = new object[] { this };
			this.m_topContent = Activator.CreateInstance(_top, array) as UIComponent;
			this.m_topContent.SetCamera(this.m_overlayCamera, true, false);
		}
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
		if (this.m_topContent != null)
		{
			this.m_topContent.UpdateUniqueCamera();
		}
		if (_addPopup)
		{
			PsState.m_openPopups.Add(this);
		}
		this.Update();
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x000FFEF0 File Offset: 0x000FE2F0
	public override void Step()
	{
		if (this.m_pageButtonLeft != null && this.m_pageButtonLeft.m_hit)
		{
			this.m_scrollableCanvas.m_changePage = -1;
		}
		else if (this.m_pageButtonRight != null && this.m_pageButtonRight.m_hit)
		{
			this.m_scrollableCanvas.m_changePage = 1;
		}
		base.Step();
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x000FFF58 File Offset: 0x000FE358
	public void CreatePageButtonLeft(string _text)
	{
		if (this.m_overlayCamera == null)
		{
			this.m_overlayCamera = CameraS.AddCamera("OverlayCamera", true, 3);
		}
		this.m_pageButtonLeft = new UICanvas(null, true, "PageButtonLeft", null, string.Empty);
		this.m_pageButtonLeft.SetSize(0.15f, 0.1f, RelativeTo.ScreenHeight);
		this.m_pageButtonLeft.SetAlign(0.05f, 0.5f);
		UIFittedText uifittedText = new UIFittedText(this.m_pageButtonLeft, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.m_shadowDistance = 0.05f;
		this.m_pageButtonLeft.SetCamera(this.m_overlayCamera, true, false);
		this.m_pageButtonLeft.Update();
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x00100018 File Offset: 0x000FE418
	public void CreatePageButtonRight(string _text)
	{
		if (this.m_overlayCamera == null)
		{
			this.m_overlayCamera = CameraS.AddCamera("OverlayCamera", true, 3);
		}
		this.m_pageButtonRight = new UICanvas(null, true, "PageButtonRight", null, string.Empty);
		this.m_pageButtonRight.SetSize(0.15f, 0.1f, RelativeTo.ScreenHeight);
		this.m_pageButtonRight.SetAlign(0.95f, 0.5f);
		UIFittedText uifittedText = new UIFittedText(this.m_pageButtonRight, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.m_shadowDistance = 0.05f;
		this.m_pageButtonRight.SetCamera(this.m_overlayCamera, true, false);
		this.m_pageButtonRight.Update();
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x001000D8 File Offset: 0x000FE4D8
	public void RemovePageButtons()
	{
		if (this.m_pageButtonLeft != null)
		{
			this.m_pageButtonLeft.Destroy();
			this.m_pageButtonLeft = null;
		}
		if (this.m_pageButtonRight != null)
		{
			this.m_pageButtonRight.Destroy();
			this.m_pageButtonRight = null;
		}
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x00100114 File Offset: 0x000FE514
	public void SetAction(string _key, Action _action)
	{
		if (!this.m_actions.ContainsKey(_key))
		{
			this.m_actions.Add(_key, _action);
		}
		else
		{
			Debug.LogWarning("OVERRIDING EXISTING ACTION, KEY: " + _key);
			this.m_actions[_key] = _action;
		}
	}

	// Token: 0x06001785 RID: 6021 RVA: 0x00100161 File Offset: 0x000FE561
	public void RemoveAction(string _key)
	{
		if (this.m_actions.ContainsKey(_key))
		{
			this.m_actions.Remove(_key);
		}
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x00100180 File Offset: 0x000FE580
	public void BringToFront()
	{
		if (this.m_scrollableCanvasContent != null)
		{
			CameraS.BringToFront(this.m_scrollableCanvasContent.m_camera, true);
		}
		if (this.m_topContent != null)
		{
			CameraS.BringToFront(this.m_topContent.m_camera, true);
		}
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x001001BC File Offset: 0x000FE5BC
	public bool CallAction(string _key)
	{
		if (this.m_actions.ContainsKey(_key))
		{
			Action action = this.m_actions[_key] as Action;
			if (action != null)
			{
				action.Invoke();
			}
			return true;
		}
		Debug.LogWarning("KEY NOT FOUND: " + _key);
		return false;
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x0010020B File Offset: 0x000FE60B
	public void UnhideUIOnDestroy(bool _flag)
	{
		this.m_hideUIComponents = _flag;
		this.m_pauseUICOmponents = _flag;
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x0010021C File Offset: 0x000FE61C
	public override void Destroy()
	{
		this.RemovePageButtons();
		if (this.m_hideUIComponents || this.m_pauseUICOmponents)
		{
			EntityManager.SetActivityOfEntitiesWithTag(this.m_guid, true, this.m_hideUIComponents, true, true, false, false);
			EntityManager.RemoveTagFromAllEntities(this.m_guid);
		}
		if (this.m_overlayCamera != null)
		{
			CameraS.RemoveCamera(this.m_overlayCamera);
		}
		EntityManager.RemoveEntity(this.m_utilityEntity);
		this.m_utilityEntity = null;
		PsState.m_openPopups.Remove(this);
		base.Destroy();
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x001002A8 File Offset: 0x000FE6A8
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_scrollableCanvas.m_camera);
	}

	// Token: 0x04001A4C RID: 6732
	public Entity m_utilityEntity;

	// Token: 0x04001A4D RID: 6733
	private bool m_hideUIComponents;

	// Token: 0x04001A4E RID: 6734
	private bool m_pauseUICOmponents;

	// Token: 0x04001A4F RID: 6735
	public UIComponent m_mainContent;

	// Token: 0x04001A50 RID: 6736
	private UIComponent m_leftContent;

	// Token: 0x04001A51 RID: 6737
	private UIComponent m_rightContent;

	// Token: 0x04001A52 RID: 6738
	public UIComponent m_topContent;

	// Token: 0x04001A53 RID: 6739
	public UIComponent m_pageButtonLeft;

	// Token: 0x04001A54 RID: 6740
	public UIComponent m_pageButtonRight;

	// Token: 0x04001A55 RID: 6741
	private Type m_main;

	// Token: 0x04001A56 RID: 6742
	private Type m_top;

	// Token: 0x04001A57 RID: 6743
	private Type m_left;

	// Token: 0x04001A58 RID: 6744
	private Type m_right;

	// Token: 0x04001A59 RID: 6745
	private Hashtable m_actions = new Hashtable();

	// Token: 0x04001A5A RID: 6746
	public UIScrollableSnappingCanvas m_scrollableCanvas;

	// Token: 0x04001A5B RID: 6747
	private UIHorizontalList m_scrollableCanvasContent;

	// Token: 0x04001A5C RID: 6748
	public Camera m_overlayCamera;

	// Token: 0x04001A5D RID: 6749
	private InitialPage m_page;

	// Token: 0x04001A5E RID: 6750
	public string m_guid;

	// Token: 0x04001A5F RID: 6751
	public bool m_state;
}
