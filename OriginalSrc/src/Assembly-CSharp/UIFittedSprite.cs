using System;
using UnityEngine;

// Token: 0x020005A5 RID: 1445
public class UIFittedSprite : UICanvas
{
	// Token: 0x060029E5 RID: 10725 RVA: 0x00040FE4 File Offset: 0x0003F3E4
	public UIFittedSprite(UIComponent _parent, bool _touchable, string _tag, SpriteSheet _spriteSheet, Frame _frame, bool _convertToPrefab = true, bool _adjustWidthToFitSprite = true)
		: base(_parent, _touchable, _tag, null, string.Empty)
	{
		this.m_scaleOnTouch = true;
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.SetMargins(0f, RelativeTo.ScreenHeight);
		this.m_spriteSheet = _spriteSheet;
		this.m_frame = _frame;
		this.m_convertToPrefab = _convertToPrefab;
		this.m_adjustWidthToFitSprite = _adjustWidthToFitSprite;
		this.m_spriteTC = TransformS.AddComponent(this.m_TC.p_entity, "Sprite");
		TransformS.ParentComponent(this.m_spriteTC, this.m_TC, Vector3.zero);
		this.m_color = Color.gray;
	}

	// Token: 0x060029E6 RID: 10726 RVA: 0x00041087 File Offset: 0x0003F487
	public void SetColor(Color _color)
	{
		this.m_color = _color;
		if (this.m_overrideMaterial != null)
		{
			this.m_overrideMaterial.color = this.m_color;
		}
	}

	// Token: 0x060029E7 RID: 10727 RVA: 0x000410B4 File Offset: 0x0003F4B4
	public override void Update()
	{
		if (!this.m_hidden)
		{
			this.CalculateReferenceSizes();
			this.UpdateSize();
			this.UpdateMargins();
			float num = 0f;
			float num2 = 0f;
			if (this.m_frame.height > 0f)
			{
				num2 = this.m_actualHeight / this.m_frame.height;
			}
			if (this.m_frame.width > 0f)
			{
				num = this.m_actualWidth / this.m_frame.width;
			}
			float num3 = Mathf.Min(num2, num);
			float num4 = this.m_frame.height / this.m_frame.width;
			if (num2 < num)
			{
				this.SetWidth(this.m_actualHeight / num4 / (float)Screen.width, RelativeTo.ScreenWidth);
			}
			else
			{
				this.SetHeight(this.m_actualWidth * num4 / (float)Screen.height, RelativeTo.ScreenHeight);
			}
			this.CalculateReferenceSizes();
			this.UpdateSize();
			this.UpdateAlign();
			if (this.m_convertToPrefab)
			{
			}
			if (this.m_rect != null)
			{
				PrefabS.RemoveComponent(this.m_rect, true);
			}
			Vector2[] rect = DebugDraw.GetRect(this.m_frame.width * num3, this.m_frame.height * num3, Vector2.zero);
			float num5 = (float)this.m_spriteSheet.m_material.mainTexture.width;
			float num6 = (float)this.m_spriteSheet.m_material.mainTexture.height;
			UVRect uvrect = new UVRect(this.m_frame.x / num5, 1f - this.m_frame.y / num6 - this.m_frame.height / num6, this.m_frame.width / num5, this.m_frame.height / num6);
			if (this.m_frame.flipX)
			{
				uvrect.left += uvrect.width;
				uvrect.width *= -1f;
			}
			if (this.m_frame.flipY)
			{
				uvrect.bottom += uvrect.height;
				uvrect.height *= -1f;
			}
			this.m_rect = PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_spriteTC, Vector3.zero, rect, DebugDraw.ColorToUInt(this.m_color), DebugDraw.ColorToUInt(this.m_color), (!(this.m_overrideMaterial != null)) ? this.m_spriteSheet.m_material : this.m_overrideMaterial, this.m_camera, string.Empty, uvrect);
			if (!this.m_hidden && this.d_Draw != null)
			{
				this.d_Draw(this);
			}
			this.UpdateUniqueCamera();
			this.UpdateChildren();
			this.ArrangeContents();
			if (this.m_parent == null)
			{
				this.UpdateSpecial();
			}
		}
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x00041387 File Offset: 0x0003F787
	public override void Destroy()
	{
		this.m_overrideShader = null;
		base.Destroy();
	}

	// Token: 0x060029E9 RID: 10729 RVA: 0x00041396 File Offset: 0x0003F796
	public void SetFrame(Frame _frame)
	{
		this.m_frame = _frame;
	}

	// Token: 0x060029EA RID: 10730 RVA: 0x000413A0 File Offset: 0x0003F7A0
	public void SetOverrideShader(Shader _overrideShader)
	{
		if (this.m_overrideMaterial != null)
		{
			Object.Destroy(this.m_overrideMaterial);
			this.m_overrideMaterial = null;
		}
		if (_overrideShader == null)
		{
			return;
		}
		this.m_overrideMaterial = new Material(PsState.m_uiSheet.m_material);
		this.m_overrideMaterial.shader = _overrideShader;
		if (this.m_rect != null)
		{
			this.m_rect.p_gameObject.GetComponent<Renderer>().material = this.m_overrideMaterial;
		}
	}

	// Token: 0x060029EB RID: 10731 RVA: 0x00041424 File Offset: 0x0003F824
	public override void DrawHandler(UIComponent _c)
	{
	}

	// Token: 0x04002F0D RID: 12045
	public SpriteSheet m_spriteSheet;

	// Token: 0x04002F0E RID: 12046
	public Frame m_frame;

	// Token: 0x04002F0F RID: 12047
	public TransformC m_spriteTC;

	// Token: 0x04002F10 RID: 12048
	public bool m_adjustWidthToFitSprite;

	// Token: 0x04002F11 RID: 12049
	public bool m_convertToPrefab;

	// Token: 0x04002F12 RID: 12050
	public bool m_scaleOnTouch;

	// Token: 0x04002F13 RID: 12051
	public TweenC m_touchScaleTween;

	// Token: 0x04002F14 RID: 12052
	public Shader m_overrideShader;

	// Token: 0x04002F15 RID: 12053
	private Color m_color;

	// Token: 0x04002F16 RID: 12054
	public Material m_overrideMaterial;

	// Token: 0x04002F17 RID: 12055
	public PrefabC m_rect;
}
