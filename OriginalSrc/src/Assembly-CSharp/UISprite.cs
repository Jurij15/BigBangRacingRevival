using System;
using UnityEngine;

// Token: 0x020005A6 RID: 1446
public class UISprite : UICanvas
{
	// Token: 0x060029EC RID: 10732 RVA: 0x000C9790 File Offset: 0x000C7B90
	public UISprite(UIComponent _parent, bool _touchable, string _tag, Material _textureMaterial, bool _convertToPrefab = true)
		: base(_parent, _touchable, _tag, null, string.Empty)
	{
		this.m_textureMaterial = _textureMaterial;
		this.m_convertToPrefab = _convertToPrefab;
		this.m_spriteTC = TransformS.AddComponent(this.m_TC.p_entity, "Sprite");
		TransformS.ParentComponent(this.m_spriteTC, this.m_TC, Vector3.zero);
		this.m_color = Color.gray;
	}

	// Token: 0x060029ED RID: 10733 RVA: 0x000C97F8 File Offset: 0x000C7BF8
	public UISprite(UIComponent _parent, bool _touchable, string _tag, SpriteSheet _spriteSheet, Frame _frame, bool _convertToPrefab = true)
		: base(_parent, _touchable, _tag, null, string.Empty)
	{
		this.m_spriteSheet = _spriteSheet;
		this.m_frame = _frame;
		this.m_convertToPrefab = _convertToPrefab;
		this.m_spriteTC = TransformS.AddComponent(this.m_TC.p_entity, "Sprite");
		TransformS.ParentComponent(this.m_spriteTC, this.m_TC, Vector3.zero);
		this.m_color = Color.gray;
	}

	// Token: 0x060029EE RID: 10734 RVA: 0x000C9868 File Offset: 0x000C7C68
	public void SetColor(Color _color)
	{
		this.m_color = _color;
	}

	// Token: 0x060029EF RID: 10735 RVA: 0x000C9874 File Offset: 0x000C7C74
	public override void DrawHandler(UIComponent _c)
	{
		if (this.m_textureMaterial != null)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = (float)this.m_textureMaterial.mainTexture.width;
			float num4 = (float)this.m_textureMaterial.mainTexture.height;
			if (num4 > 0f)
			{
				num2 = this.m_actualHeight / num4;
			}
			if (num3 > 0f)
			{
				num = this.m_actualWidth / num3;
			}
			TransformS.ParentComponent(this.m_spriteTC, this.m_TC, Vector3.zero);
			if (num2 > 0f && num > 0f)
			{
				if (this.m_rect != null)
				{
					PrefabS.RemoveComponent(this.m_rect, true);
				}
				Vector2[] rect = DebugDraw.GetRect(num3 * num, num4 * num2, Vector2.zero);
				UVRect uvrect = new UVRect(0f, 0f, 1f, 1f);
				this.m_rect = PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_spriteTC, Vector3.zero, rect, DebugDraw.ColorToUInt(this.m_color), DebugDraw.ColorToUInt(this.m_color), this.m_textureMaterial, this.m_camera, string.Empty, uvrect);
				if (this.m_overrideShader != null)
				{
					this.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = this.m_overrideShader;
				}
			}
		}
		else if (this.m_frame != null && this.m_spriteSheet != null)
		{
			float num5 = 0f;
			float num6 = 0f;
			if (this.m_frame.height > 0f)
			{
				num6 = this.m_actualHeight / this.m_frame.height;
			}
			if (this.m_frame.width > 0f)
			{
				num5 = this.m_actualWidth / this.m_frame.width;
			}
			TransformS.ParentComponent(this.m_spriteTC, this.m_TC, Vector3.zero);
			if (num6 > 0f && num5 > 0f)
			{
				if (this.m_rect != null)
				{
					PrefabS.RemoveComponent(this.m_rect, true);
				}
				Vector2[] rect2 = DebugDraw.GetRect(this.m_frame.width * num5, this.m_frame.height * num6, Vector2.zero);
				float num7 = (float)this.m_spriteSheet.m_material.mainTexture.width;
				float num8 = (float)this.m_spriteSheet.m_material.mainTexture.height;
				UVRect uvrect2 = new UVRect(this.m_frame.x / num7, 1f - this.m_frame.y / num8 - this.m_frame.height / num8, this.m_frame.width / num7, this.m_frame.height / num8);
				if (this.m_frame.flipX)
				{
					uvrect2.left += uvrect2.width;
					uvrect2.width *= -1f;
				}
				if (this.m_frame.flipY)
				{
					uvrect2.bottom += uvrect2.height;
					uvrect2.height *= -1f;
				}
				this.m_rect = PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_spriteTC, Vector3.zero, rect2, DebugDraw.ColorToUInt(this.m_color), DebugDraw.ColorToUInt(this.m_color), this.m_spriteSheet.m_material, this.m_camera, string.Empty, uvrect2);
				if (this.m_overrideShader != null)
				{
					this.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = this.m_overrideShader;
				}
			}
		}
	}

	// Token: 0x060029F0 RID: 10736 RVA: 0x000C9C1F File Offset: 0x000C801F
	public void SetOverrideShader(Shader _overrideShader)
	{
		this.m_overrideShader = _overrideShader;
	}

	// Token: 0x04002F18 RID: 12056
	public SpriteSheet m_spriteSheet;

	// Token: 0x04002F19 RID: 12057
	public Frame m_frame;

	// Token: 0x04002F1A RID: 12058
	public Material m_textureMaterial;

	// Token: 0x04002F1B RID: 12059
	public TransformC m_spriteTC;

	// Token: 0x04002F1C RID: 12060
	public bool m_adjustWidthToFitSprite;

	// Token: 0x04002F1D RID: 12061
	public bool m_convertToPrefab;

	// Token: 0x04002F1E RID: 12062
	private float m_clipLeft;

	// Token: 0x04002F1F RID: 12063
	private float m_clipRight;

	// Token: 0x04002F20 RID: 12064
	private float m_clipBottom;

	// Token: 0x04002F21 RID: 12065
	private float m_clipTop;

	// Token: 0x04002F22 RID: 12066
	private bool m_clip;

	// Token: 0x04002F23 RID: 12067
	public Shader m_overrideShader;

	// Token: 0x04002F24 RID: 12068
	public PrefabC m_rect;

	// Token: 0x04002F25 RID: 12069
	private Color m_color;
}
