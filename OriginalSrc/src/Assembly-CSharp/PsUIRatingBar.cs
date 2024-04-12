using System;
using UnityEngine;

// Token: 0x020003A5 RID: 933
public class PsUIRatingBar : UIComponent
{
	// Token: 0x06001AAD RID: 6829 RVA: 0x00129118 File Offset: 0x00127518
	public PsUIRatingBar(UIComponent _parent, int _positive, int _negative)
		: base(_parent, false, string.Empty, null, null, string.Empty)
	{
		this.m_positive = ((_positive >= 0) ? _positive : 0);
		this.m_negative = ((_negative >= 0) ? _negative : 0);
		this.SetMargins(0.25f, 0.25f, 0f, 0f, RelativeTo.OwnHeight);
		this.m_likes = 0.5f;
		if (this.m_positive + this.m_negative != 0)
		{
			this.m_likes = (float)this.m_positive / (float)(this.m_positive + this.m_negative);
		}
		this.m_positiveText = new UIText(this, false, "Positive", this.m_positive.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.675f, RelativeTo.ParentHeight, "#FFFFFF", null);
		this.m_positiveText.SetHorizontalAlign(0f);
		this.m_negativeText = new UIText(this, false, "Negative", this.m_negative.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.675f, RelativeTo.ParentHeight, "#FFFFFF", null);
		this.m_negativeText.SetHorizontalAlign(1f);
		this.SetDesc();
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x00129246 File Offset: 0x00127646
	private float GetRatio()
	{
		return (float)this.m_positive / (float)(this.m_positive + this.m_negative);
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x00129260 File Offset: 0x00127660
	public override void Step()
	{
		if (this.m_hasTargetValue && this.m_tween != null)
		{
			float num = (this.m_tween.currentTime - this.m_tween.startTime) / this.m_tween.duration;
			if (this.m_tween.hasFinished)
			{
				TweenS.RemoveComponent(this.m_tween);
				this.m_hasTargetValue = false;
				this.m_tween = null;
			}
			else
			{
				this.m_likes = this.m_tween.currentValue.x;
			}
			int num2 = (int)Mathf.Round((float)this.m_positiveStart + (float)(this.m_positive - this.m_positiveStart) * num);
			int num3 = (int)Mathf.Round((float)this.m_negativeStart + (float)(this.m_negative - this.m_negativeStart) * num);
			if (this.m_positiveText.m_text != num2.ToString())
			{
				this.m_positiveText.SetText(num2.ToString());
			}
			if (this.m_negativeText.m_text != num3.ToString())
			{
				this.m_negativeText.SetText(num3.ToString());
			}
			this.d_Draw(this);
		}
		base.Step();
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x001293AF File Offset: 0x001277AF
	public void ThumbsDown()
	{
		this.GiveRating(-1);
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x001293B8 File Offset: 0x001277B8
	public void ThumbsDownDouble()
	{
		this.GiveRating(-2);
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x001293C2 File Offset: 0x001277C2
	public void ThumbsUp()
	{
		this.GiveRating(1);
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x001293CB File Offset: 0x001277CB
	public void ThumbsUpDouble()
	{
		this.GiveRating(2);
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x001293D4 File Offset: 0x001277D4
	public void SuperLike()
	{
		this.GiveRating(10);
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x001293E0 File Offset: 0x001277E0
	private void GiveRating(int _value)
	{
		if (_value == 0)
		{
			Debug.LogError("Error, cant give zero");
		}
		else
		{
			this.m_positiveStart = this.m_positive;
			this.m_negativeStart = this.m_negative;
			if (_value > 0)
			{
				this.m_positive += _value;
			}
			else
			{
				this.m_negative -= _value;
			}
			this.m_targetValue = this.GetRatio();
			this.m_hasTargetValue = true;
			this.m_startvalue = this.m_likes;
			float num = 0.0125f;
			if (Mathf.Abs(this.m_targetValue - this.m_startvalue) < num)
			{
				this.m_highlightWidth = this.m_startvalue + ((this.m_targetValue - this.m_startvalue >= 0f) ? (1f * num) : (-1f * num));
			}
			else
			{
				this.m_highlightWidth = this.m_targetValue;
			}
			this.m_tween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.ExpoInOut, this.m_likes, this.m_targetValue, 0.5f, 0f);
		}
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x001294F8 File Offset: 0x001278F8
	public void SetDesc()
	{
		float num = 0.5f;
		UIComponent uicomponent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent.SetDepthOffset(5f);
		uicomponent.SetMargins(0f, 0f, -0.5f - num / 2f, 0.5f + num / 2f, RelativeTo.ParentHeight);
		uicomponent.SetHeight(1f, RelativeTo.ParentHeight);
		uicomponent.SetWidth(1f, RelativeTo.ParentWidth);
		UIComponent uicomponent2 = new UIComponent(uicomponent, false, string.Empty, null, null, string.Empty);
		uicomponent2.SetHorizontalAlign(0.1f);
		uicomponent2.SetHeight(num + 0.15f, RelativeTo.ParentHeight);
		uicomponent2.SetWidth(0.4f, RelativeTo.ParentWidth);
		uicomponent2.SetMargins(0.15f, 0.15f, 0.15f, 0.3f, RelativeTo.OwnHeight);
		uicomponent2.SetDrawHandler(new UIDrawDelegate(this.DescDrawHandler));
		string text = PsStrings.Get(StringID.LIKE_A_METER);
		UIFittedText uifittedText = new UIFittedText(uicomponent2, false, "desc", text, PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x001295F8 File Offset: 0x001279F8
	public void DescDrawHandler(UIComponent _c)
	{
		Camera camera = this.m_camera;
		if (this.m_parent != null)
		{
			camera = this.m_parent.m_camera;
		}
		float num = 0.2f;
		float num2 = _c.m_actualHeight * 0.125f;
		float num3 = (_c.m_actualHeight - num2) / _c.m_actualHeight;
		float num4 = (_c.m_actualWidth - num2) / _c.m_actualWidth;
		int num5 = 8;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, num * _c.m_actualHeight, num5, Vector2.zero);
		uint num6 = DebugDraw.HexToUint("#3D3D33");
		uint num7 = DebugDraw.HexToUint("#3D3D33");
		Vector3 vector;
		vector..ctor(0f, _c.m_actualHeight * num3 / 4f + _c.m_actualHeight * 0.02f, 0f);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 3f, roundedRect, num7, num6, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x001296F8 File Offset: 0x00127AF8
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		Camera camera = this.m_camera;
		if (this.m_parent != null)
		{
			camera = this.m_parent.m_camera;
		}
		float num = 0.2f;
		float num2 = _c.m_actualHeight * 0.125f;
		float num3 = (_c.m_actualHeight - num2) / _c.m_actualHeight;
		float num4 = (_c.m_actualWidth - num2) / _c.m_actualWidth;
		int num5 = 8;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth + num2 / 2f, _c.m_actualHeight + num2 / 2f, num * _c.m_actualHeight, num5, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, num * _c.m_actualHeight, num5, Vector2.zero);
		float num6 = _c.m_actualWidth * this.m_likes;
		Vector2[] roundedRect3 = DebugDraw.GetRoundedRect(num6, _c.m_actualHeight, num * _c.m_actualHeight, num5, Vector2.zero);
		if (_c.m_actualWidth > num6)
		{
			int num7 = num5 * 3;
			for (int i = num7; i < num5 + num7; i++)
			{
				roundedRect3[i] = new Vector2(num6 / 2f, _c.m_actualHeight / 2f);
			}
			num7 = 0;
			for (int j = num7; j < num5 + num7; j++)
			{
				roundedRect3[j] = new Vector2(num6 / 2f, -_c.m_actualHeight / 2f);
			}
		}
		if (this.m_tween != null)
		{
			float num8 = (this.m_tween.currentTime - this.m_tween.startTime) / this.m_tween.duration;
			float num9 = this.m_startvalue - this.m_highlightWidth;
			Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth * Mathf.Abs(num9), _c.m_actualHeight, Vector2.zero);
			float num10 = this.m_startvalue - this.m_targetValue;
			float num11 = num10 - num9;
			num11 *= _c.m_actualWidth;
			Vector3 vector;
			vector..ctor(-_c.m_actualWidth / 2f + this.m_startvalue * _c.m_actualWidth - _c.m_actualWidth * num9 / 2f - num11, 0f, 0f);
			Color white = Color.white;
			white.a = 1f - num8;
			uint num12 = DebugDraw.ColorToUInt(white);
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -2f + vector, rect, num12, num12, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		}
		Vector2[] roundedRect4 = DebugDraw.GetRoundedRect(_c.m_actualWidth * num4 * 0.97f, _c.m_actualHeight * num3 / 2f, num * _c.m_actualHeight, num5, Vector2.zero);
		Color white2 = Color.white;
		Color white3 = Color.white;
		white3.a = 0.05f;
		white2.a = 0.3f;
		uint num13 = DebugDraw.ColorToUInt(white3);
		uint num14 = DebugDraw.ColorToUInt(white2);
		Vector3 vector2;
		vector2..ctor(0f, _c.m_actualHeight * num3 / 4f + _c.m_actualHeight * 0.02f, 0f);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -1f + vector2, roundedRect4, num14, num13, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		num13 = DebugDraw.HexToUint("#3D3D33");
		num14 = DebugDraw.HexToUint("#3D3D33");
		Color color = DebugDraw.HexToColor("#3D3D33");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -3f, roundedRect, num2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		num13 = DebugDraw.HexToUint("#C33C1C");
		num14 = DebugDraw.HexToUint("#C33C1C");
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect2, num14, num13, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		num13 = DebugDraw.HexToUint("#61C400");
		num14 = DebugDraw.HexToUint("#61C400");
		vector2..ctor(-_c.m_actualWidth / 2f + num6 / 2f, 0f, 0f);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 1f + vector2, roundedRect3, num14, num13, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		SpriteS.ConvertSpritesToPrefabComponent(this.m_TC, camera, true, null);
	}

	// Token: 0x04001D1F RID: 7455
	private int m_positive;

	// Token: 0x04001D20 RID: 7456
	private int m_positiveStart;

	// Token: 0x04001D21 RID: 7457
	private int m_negative;

	// Token: 0x04001D22 RID: 7458
	private int m_negativeStart;

	// Token: 0x04001D23 RID: 7459
	private UIText m_positiveText;

	// Token: 0x04001D24 RID: 7460
	private UIText m_negativeText;

	// Token: 0x04001D25 RID: 7461
	private float m_likes;

	// Token: 0x04001D26 RID: 7462
	private bool m_hasTargetValue;

	// Token: 0x04001D27 RID: 7463
	private float m_targetValue;

	// Token: 0x04001D28 RID: 7464
	private float m_startvalue;

	// Token: 0x04001D29 RID: 7465
	private float m_highlightWidth;

	// Token: 0x04001D2A RID: 7466
	private TweenC m_tween;
}
