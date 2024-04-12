using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class PsUIResourceProgressBar : UICanvas
{
	// Token: 0x060004C0 RID: 1216 RVA: 0x0003DDB8 File Offset: 0x0003C1B8
	public PsUIResourceProgressBar(UIComponent _parent, int _currentValue, int _fullValue, string _tag = "cardfront", bool _shopMode = false, string _replaceText = null)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.m_currentValue = _currentValue;
		this.m_fullValue = _fullValue;
		this.m_full = this.m_currentValue >= this.m_fullValue;
		this.SetDrawHandler(new UIDrawDelegate(this.DrawHandler));
		string text = this.m_currentValue + "/" + this.m_fullValue;
		string text2 = "#ffffff";
		if (_replaceText != null)
		{
			text = _replaceText;
			this.noCountText = true;
		}
		else if (this.m_currentValue >= this.m_fullValue)
		{
			if (!_shopMode)
			{
				if (_fullValue == 1)
				{
					text = PsStrings.Get(StringID.INSTALL_ITEM);
				}
				else
				{
					text = PsStrings.Get(StringID.LEVEL_UP_ITEM);
				}
			}
			text2 = "#055701";
		}
		UIComponent uicomponent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetWidth(0.9f, RelativeTo.ParentWidth);
		this.m_countText = new UIFittedText(uicomponent, false, _tag, text, PsFontManager.GetFont(PsFonts.HurmeBold), true, text2, null);
		this.m_countText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		this.m_countText.SetDepthOffset(-6f);
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0003DEF8 File Offset: 0x0003C2F8
	public void SetValues(int _currentValue, int _fullValue)
	{
		this.m_currentValue = _currentValue;
		this.m_fullValue = _fullValue;
		this.m_full = this.m_currentValue >= this.m_fullValue;
		if (this.m_countText != null && !this.noCountText)
		{
			if (this.m_currentValue >= this.m_fullValue)
			{
				this.m_countText.SetColor("#055701", null);
			}
			else
			{
				this.m_countText.SetColor("#ffffff", null);
			}
			this.m_countText.SetText(this.m_currentValue + "/" + this.m_fullValue);
		}
		if (this.m_parent != null)
		{
			this.m_parent.Update();
		}
		else
		{
			this.Update();
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0003DFC4 File Offset: 0x0003C3C4
	public void Increase(int _count)
	{
		this.m_currentValue += _count;
		this.m_full = this.m_currentValue >= this.m_fullValue;
		if (!this.noCountText)
		{
			if (this.m_currentValue >= this.m_fullValue)
			{
				this.m_countText.SetColor("#055701", null);
			}
			else
			{
				this.m_countText.SetColor("#ffffff", null);
			}
			this.m_countText.SetText(this.m_currentValue + "/" + this.m_fullValue);
		}
		this.Update();
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0003E06C File Offset: 0x0003C46C
	private new void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_upgrade_bar_background", null);
		float num = frame.width / 3f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x + num * 2f, frame.y, num, frame.height);
		float num2 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame2.width / frame2.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		float num5 = Mathf.Min((float)this.m_currentValue / (float)this.m_fullValue, 1f);
		string text;
		if (num5 == 1f)
		{
			text = "menu_garage_upgrade_bar_filler_full";
		}
		else
		{
			text = "menu_garage_upgrade_bar_filler";
		}
		Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame(text, null);
		float num6 = _c.m_actualWidth * num5;
		float num7 = -_c.m_actualWidth / 2f;
		float num8 = frame5.width / 3f;
		float num9 = _c.m_actualHeight * (num8 / frame5.height);
		float num10 = Mathf.Min(num6 / num9, 1f);
		Frame frame6 = new Frame(frame5.x, frame5.y, num8 * num10, frame5.height);
		float num11 = _c.m_actualHeight * (frame6.width / frame6.height);
		SpriteC spriteC4 = SpriteS.AddComponent(_c.m_TC, frame6, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC4, num11, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC4, new Vector3(num7 + num11 / 2f, 0f, -0.2f), 0f);
		if (num10 == 1f)
		{
			float num12 = Mathf.Min((num6 - num9) / (_c.m_actualWidth - num9 * 2f), 1f);
			Frame frame7 = new Frame(frame5.x + num8, frame5.y, num8, frame5.height);
			float num13 = (_c.m_actualWidth - num9 * 2f) * num12;
			SpriteC spriteC5 = SpriteS.AddComponent(_c.m_TC, frame7, PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC5, num13, _c.m_actualHeight);
			SpriteS.SetOffset(spriteC5, new Vector3(num7 + num11 + num13 / 2f, 0f, -0.2f), 0f);
			if (num12 == 1f)
			{
				float num14 = Mathf.Min((num6 - (_c.m_actualWidth - num9)) / num9, 1f);
				if (num14 > 0f)
				{
					Frame frame8 = new Frame(frame5.x + num8 * 2f, frame5.y, num8 * num14, frame5.height);
					float num15 = _c.m_actualHeight * (frame8.width / frame8.height);
					SpriteC spriteC6 = SpriteS.AddComponent(_c.m_TC, frame8, PsState.m_uiSheet);
					SpriteS.SetDimensions(spriteC6, num15, _c.m_actualHeight);
					SpriteS.SetOffset(spriteC6, new Vector3(num7 + num11 + num13 + num15 / 2f, 0f, -0.2f), 0f);
				}
			}
		}
		Frame frame9 = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_upgrade_bar_frame", null);
		float num16 = frame9.width / 3f;
		Frame frame10 = new Frame(frame9.x, frame9.y, num16, frame9.height);
		Frame frame11 = new Frame(frame9.x + num16, frame9.y, num16, frame9.height);
		Frame frame12 = new Frame(frame9.x + num16 * 2f, frame9.y, num16, frame9.height);
		float num17 = _c.m_actualHeight * (frame10.width / frame10.height);
		SpriteC spriteC7 = SpriteS.AddComponent(_c.m_TC, frame10, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC7, _c.m_actualHeight * (frame10.width / frame10.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC7, new Vector3(-(_c.m_actualWidth - num17) / 2f, 0f, -0.4f), 0f);
		float num18 = _c.m_actualHeight * (frame10.width / frame10.height);
		SpriteC spriteC8 = SpriteS.AddComponent(_c.m_TC, frame12, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC8, num18, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC8, new Vector3((_c.m_actualWidth - num18) / 2f, 0f, -0.4f), 0f);
		float num19 = _c.m_actualWidth - num17 - num18;
		SpriteC spriteC9 = SpriteS.AddComponent(_c.m_TC, frame11, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC9, num19, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC9, new Vector3(0f, 0f, -0.4f), 0f);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0003E668 File Offset: 0x0003CA68
	public void CreatorRankDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_creatorrank_bar_background", null);
		float num = frame.width / 3f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x + num * 2f, frame.y, num, frame.height);
		float num2 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame2.width / frame2.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		float num5 = Mathf.Min((float)this.m_currentValue / (float)this.m_fullValue, 1f);
		string text = "menu_creatorrank_bar_filler";
		Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame(text, null);
		float num6 = _c.m_actualWidth * num5;
		float num7 = -_c.m_actualWidth / 2f;
		float num8 = frame5.width / 3f;
		float num9 = _c.m_actualHeight * (num8 / frame5.height);
		float num10 = Mathf.Min(num6 / num9, 1f);
		Frame frame6 = new Frame(frame5.x, frame5.y, num8 * num10, frame5.height);
		float num11 = _c.m_actualHeight * (frame6.width / frame6.height);
		SpriteC spriteC4 = SpriteS.AddComponent(_c.m_TC, frame6, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC4, num11, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC4, new Vector3(num7 + num11 / 2f, 0f, -0.2f), 0f);
		if (num10 == 1f)
		{
			float num12 = Mathf.Min((num6 - num9) / (_c.m_actualWidth - num9 * 2f), 1f);
			Frame frame7 = new Frame(frame5.x + num8, frame5.y, num8, frame5.height);
			float num13 = (_c.m_actualWidth - num9 * 2f) * num12;
			SpriteC spriteC5 = SpriteS.AddComponent(_c.m_TC, frame7, PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC5, num13, _c.m_actualHeight);
			SpriteS.SetOffset(spriteC5, new Vector3(num7 + num11 + num13 / 2f, 0f, -0.2f), 0f);
			if (num12 == 1f)
			{
				float num14 = Mathf.Min((num6 - (_c.m_actualWidth - num9)) / num9, 1f);
				if (num14 > 0f)
				{
					Frame frame8 = new Frame(frame5.x + num8 * 2f, frame5.y, num8 * num14, frame5.height);
					float num15 = _c.m_actualHeight * (frame8.width / frame8.height);
					SpriteC spriteC6 = SpriteS.AddComponent(_c.m_TC, frame8, PsState.m_uiSheet);
					SpriteS.SetDimensions(spriteC6, num15, _c.m_actualHeight);
					SpriteS.SetOffset(spriteC6, new Vector3(num7 + num11 + num13 + num15 / 2f, 0f, -0.2f), 0f);
				}
			}
		}
		Frame frame9 = PsState.m_uiSheet.m_atlas.GetFrame("menu_creatorrank_bar_frame", null);
		float num16 = frame9.width / 3f;
		Frame frame10 = new Frame(frame9.x, frame9.y, num16, frame9.height);
		Frame frame11 = new Frame(frame9.x + num16, frame9.y, num16, frame9.height);
		Frame frame12 = new Frame(frame9.x + num16 * 2f, frame9.y, num16, frame9.height);
		float num17 = _c.m_actualHeight * (frame10.width / frame10.height);
		SpriteC spriteC7 = SpriteS.AddComponent(_c.m_TC, frame10, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC7, _c.m_actualHeight * (frame10.width / frame10.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC7, new Vector3(-(_c.m_actualWidth - num17) / 2f, 0f, -0.4f), 0f);
		float num18 = _c.m_actualHeight * (frame10.width / frame10.height);
		SpriteC spriteC8 = SpriteS.AddComponent(_c.m_TC, frame12, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC8, num18, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC8, new Vector3((_c.m_actualWidth - num18) / 2f, 0f, -0.4f), 0f);
		float num19 = _c.m_actualWidth - num17 - num18;
		SpriteC spriteC9 = SpriteS.AddComponent(_c.m_TC, frame11, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC9, num19, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC9, new Vector3(0f, 0f, -0.4f), 0f);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x0400061E RID: 1566
	public UIFittedText m_countText;

	// Token: 0x0400061F RID: 1567
	public bool m_full;

	// Token: 0x04000620 RID: 1568
	private int m_fullValue;

	// Token: 0x04000621 RID: 1569
	private int m_currentValue;

	// Token: 0x04000622 RID: 1570
	private bool noCountText;
}
