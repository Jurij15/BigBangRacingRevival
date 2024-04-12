using System;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class PsUISwitch : UIVerticalList
{
	// Token: 0x0600122F RID: 4655 RVA: 0x000B3A74 File Offset: 0x000B1E74
	public PsUISwitch(UIComponent _parent, bool _enabled = true, float _fontSize = 0.03f, string _enabledText = "On", string _disabledText = "Off", float _textAreaWidth = 0.05f, Action<bool> _toggleHandler = null)
		: base(_parent, string.Empty)
	{
		this.CreateTouchAreas();
		this.m_enabled = _enabled;
		this.m_fontSize = _fontSize;
		this.m_enabledText = _enabledText;
		this.m_disabledText = _disabledText;
		this.m_textAreaWidth = _textAreaWidth;
		this.m_button = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_button.SetVerticalAlign(0.5f);
		this.m_button.m_TAC.m_active = false;
		this.m_button.SetText(string.Empty, _fontSize, _textAreaWidth, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.SetWidth(this.m_button.m_actualMargins.l + this.m_button.m_actualMargins.r + _textAreaWidth + 0.12f, RelativeTo.ScreenHeight);
		this.UpdateSwitchPosition();
		this.m_toggleHandler = _toggleHandler;
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x000B3B4F File Offset: 0x000B1F4F
	protected virtual void ToggleCallback(bool _value)
	{
		if (this.m_toggleHandler != null)
		{
			this.m_toggleHandler.Invoke(_value);
		}
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x000B3B68 File Offset: 0x000B1F68
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		if (_inside && !this.m_dragChanged)
		{
			this.m_enabled = !this.m_enabled;
			this.UpdateSwitchPosition();
			SoundS.PlaySingleShot("/UI/ButtonToggle", Vector3.zero, 1f);
		}
		base.OnTouchRelease(_touch, _inside);
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x000B3BB7 File Offset: 0x000B1FB7
	protected override void OnTouchBegan(TLTouch _touch)
	{
		this.m_dragStartXPosition = _touch.m_currentPosition.x;
		this.m_dragChanged = false;
		base.OnTouchBegan(_touch);
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x000B3BD8 File Offset: 0x000B1FD8
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondery)
	{
		this.m_dragStartXPosition = _touch.m_currentPosition.x;
		this.m_dragChanged = false;
		base.OnTouchRollIn(_touch, _secondery);
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x000B3BFC File Offset: 0x000B1FFC
	protected override void OnTouchMove(TLTouch _touch, bool _inside)
	{
		if (_inside)
		{
			float num = _touch.m_currentPosition.x - this.m_dragStartXPosition;
			if (num > this.m_actualWidth / 3f && !this.m_enabled)
			{
				this.m_hit = true;
				this.m_enabled = true;
				this.UpdateSwitchPosition();
				this.m_dragStartXPosition = _touch.m_currentPosition.x;
				this.m_dragChanged = true;
			}
			else if (num < -this.m_actualWidth / 3f && this.m_enabled)
			{
				this.m_hit = true;
				this.m_enabled = false;
				this.UpdateSwitchPosition();
				this.m_dragStartXPosition = _touch.m_currentPosition.x;
				this.m_dragChanged = true;
			}
		}
		base.OnTouchMove(_touch, _inside);
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x000B3CC4 File Offset: 0x000B20C4
	private void UpdateSwitchPosition()
	{
		if (this.m_enabled)
		{
			this.m_button.SetGreenColors(true);
			this.m_button.SetText(this.m_enabledText, this.m_fontSize, this.m_textAreaWidth, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
			this.m_button.SetHorizontalAlign(1f);
		}
		else
		{
			this.m_button.SetBlueColors(false);
			this.m_button.SetText(this.m_disabledText, this.m_fontSize, this.m_textAreaWidth, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
			this.m_button.SetHorizontalAlign(0f);
		}
		this.ToggleCallback(this.m_enabled);
		this.m_button.Update();
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x000B3D70 File Offset: 0x000B2170
	public override void DrawHandler(UIComponent _c)
	{
		float num = 0.006f;
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		Vector2[] bezierRect = DebugDraw.GetBezierRect(_c.m_actualWidth - num * 3f * (float)Screen.height, _c.m_actualHeight - num * 3f * (float)Screen.height, num * (float)Screen.height, 10, Vector2.one * 0.9f);
		PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, Vector3.forward * 0.1f, bezierRect, (float)Screen.width * 0.005f, DebugDraw.HexToColor("#05192f"), DebugDraw.HexToColor("#05192f"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), this.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.forward * 0.2f, bezierRect, DebugDraw.HexToUint("#156591"), DebugDraw.HexToUint("#156591"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera, string.Empty, null);
		SpriteS.ConvertSpritesToPrefabComponent(this.m_TC, this.m_camera, true, null);
	}

	// Token: 0x0400155E RID: 5470
	public bool m_enabled;

	// Token: 0x0400155F RID: 5471
	private PsUIGenericButton m_button;

	// Token: 0x04001560 RID: 5472
	private float m_fontSize;

	// Token: 0x04001561 RID: 5473
	private float m_textAreaWidth;

	// Token: 0x04001562 RID: 5474
	private string m_enabledText;

	// Token: 0x04001563 RID: 5475
	private string m_disabledText;

	// Token: 0x04001564 RID: 5476
	private float m_dragStartXPosition;

	// Token: 0x04001565 RID: 5477
	private bool m_dragChanged;

	// Token: 0x04001566 RID: 5478
	private Action<bool> m_toggleHandler;
}
