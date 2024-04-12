using System;
using UnityEngine;

// Token: 0x02000254 RID: 596
public class PsUIProfileImage : UIComponent
{
	// Token: 0x06001201 RID: 4609 RVA: 0x000B23FC File Offset: 0x000B07FC
	public PsUIProfileImage(UIComponent _parent, bool _touchable, string _tag, string _facebookId, string _gameCenterId, int _rank = -1, bool _loadImmediately = true, bool _shadow = false, bool _largePicture = false, float _borderWidth = 0.1f, float _borderRadius = 0.06f, string _borderColor = "fff9e6", string _defaultHatIdentifier = null, bool _playerProfile = false, bool _border = true)
		: base(_parent, _touchable, _tag, null, null, string.Empty)
	{
		this.m_border = _border;
		this.m_playerProfile = _playerProfile;
		this.m_facebookId = _facebookId;
		this.m_fetchingImage = false;
		this.m_prefabTC = TransformS.AddComponent(this.m_TC.p_entity, "PrefabTC");
		TransformS.ParentComponent(this.m_prefabTC, this.m_TC, Vector3.zero);
		this.m_shadow = _shadow;
		this.m_largePicture = _largePicture;
		this.m_borderWidth = _borderWidth;
		this.m_borderRadius = _borderRadius;
		this.m_borderColor = _borderColor;
		this.m_hatIdentifier = _defaultHatIdentifier;
		this.SetMargins(-0.1f, RelativeTo.OwnHeight);
		if (_rank != -1)
		{
			UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas.SetAlign(0f, 1f);
			uicanvas.SetSize(0.275f, 0.275f, RelativeTo.ParentHeight);
			uicanvas.SetDrawHandler(new UIDrawDelegate(this.RankDrawHandler));
			string text = (PsState.m_versusRankCap + 1 - _rank).ToString();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeRegular), true, "000000", null);
			uifittedText.SetSize(1f, 1f, RelativeTo.ParentHeight);
		}
		if (this.m_material == null)
		{
			this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ProfileMat_Material));
		}
		if (_loadImmediately)
		{
			this.LoadPicture();
		}
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x000B256C File Offset: 0x000B096C
	private void PictureDownloadFailed(HttpC _c)
	{
		if (string.IsNullOrEmpty(this.m_hatIdentifier))
		{
			this.DisplayPicture(PsState.m_uiSheet.m_atlas.GetFrame("menu_profilepic_MotocrossHelmet", null));
		}
		else
		{
			this.DisplayPicture(PsState.m_uiSheet.m_atlas.GetFrame("menu_profilepic_" + this.m_hatIdentifier, null));
		}
		this.m_fetchingImage = false;
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x000B25D8 File Offset: 0x000B09D8
	private void PictureDownloadOk(Texture2D _texture)
	{
		string text = this.m_facebookId;
		if (this.m_largePicture)
		{
			text += "_large";
		}
		PsCaches.m_profileImages.AddItem(text, _texture);
		this.DisplayPicture(_texture);
		this.m_fetchingImage = false;
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x000B2620 File Offset: 0x000B0A20
	public void LoadPicture()
	{
		if (!string.IsNullOrEmpty(this.m_facebookId))
		{
			string text = this.m_facebookId;
			if (this.m_largePicture)
			{
				text += "_large";
			}
			Texture2D content = PsCaches.m_profileImages.GetContent(text);
			if (content != null)
			{
				this.DisplayPicture(content);
			}
			else if (!this.m_fetchingImage)
			{
				HttpC picture = FacebookManager.GetPicture(this.m_facebookId, new Action<Texture2D>(this.PictureDownloadOk), new Action<HttpC>(this.PictureDownloadFailed), this.m_largePicture);
				picture.ignoreInQueue = true;
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, picture);
				this.m_fetchingImage = true;
			}
		}
		else if (this.m_playerProfile)
		{
			this.DisplayPicture(ResourceManager.GetTexture(RESOURCE.AnonymousProfile_Texture2D) as Texture2D);
		}
		else if (string.IsNullOrEmpty(this.m_hatIdentifier))
		{
			this.DisplayPicture(PsState.m_uiSheet.m_atlas.GetFrame("menu_profilepic_MotocrossHelmet", null));
		}
		else
		{
			this.DisplayPicture(PsState.m_uiSheet.m_atlas.GetFrame("menu_profilepic_" + this.m_hatIdentifier, null));
		}
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x000B2753 File Offset: 0x000B0B53
	private void DisplayPicture(Texture2D _texture)
	{
		this.m_texture = _texture;
		if (this.m_material != null)
		{
			this.m_material.mainTexture = this.m_texture;
		}
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x000B2780 File Offset: 0x000B0B80
	private void DisplayPicture(Frame _frame)
	{
		this.m_frame = _frame;
		if (this.m_material != null)
		{
			this.m_material.mainTexture = PsState.m_uiSheet.m_material.mainTexture;
		}
		if (this.m_pic != null && this.m_material != null)
		{
			Vector2[] uv = this.m_pic.p_mesh.uv;
			for (int i = 0; i < uv.Length; i++)
			{
				float num = (float)this.m_material.mainTexture.width;
				float num2 = (float)this.m_material.mainTexture.height;
				uv[i].x = uv[i].x * (this.m_frame.width / num) + this.m_frame.x / num;
				uv[i].y = uv[i].y * (this.m_frame.height / num2) + (1f - this.m_frame.y / num2 - this.m_frame.height / num2);
			}
			this.m_pic.p_mesh.uv = uv;
		}
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x000B28B1 File Offset: 0x000B0CB1
	public override void RemoveDrawHandler()
	{
		Debug.LogError("Dont use this on profileimages");
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x000B28C0 File Offset: 0x000B0CC0
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_prefabTC.p_entity, false);
		this.m_prefabTC.transform.localScale = Vector3.one;
		if (this.m_frame != null)
		{
			this.m_material.mainTexture = PsState.m_uiSheet.m_material.mainTexture;
		}
		if (this.m_texture != null)
		{
			this.m_material.mainTexture = this.m_texture;
		}
		float actualWidth = this.m_actualWidth;
		float actualHeight = this.m_actualHeight;
		Vector2[] array = DebugDraw.GetRoundedRect(actualWidth, actualHeight, actualHeight * this.m_borderRadius, 4, Vector2.zero);
		UVRect uvrect = UVRect.Normal();
		if (this.m_frame != null)
		{
			float num = (float)this.m_material.mainTexture.width;
			float num2 = (float)this.m_material.mainTexture.height;
			uvrect = new UVRect(this.m_frame.x / num, 1f - this.m_frame.y / num2 - this.m_frame.height / num2, this.m_frame.width / num, this.m_frame.height / num2);
		}
		this.m_pic = PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_prefabTC, Vector3.forward * -0.1f, array, DebugDraw.HexToUint("ffffff"), DebugDraw.HexToUint("ffffff"), this.m_material, this.m_camera, "Profile", uvrect);
		if (this.m_border)
		{
			array = DebugDraw.GetRoundedRect(actualWidth + actualHeight * 0.025f, actualHeight + actualHeight * 0.025f, actualHeight * this.m_borderRadius, 4, Vector2.zero);
			PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_prefabTC, new Vector3(0f, 0f, -1f), array, this.m_actualHeight * this.m_borderWidth, DebugDraw.HexToColor(this.m_borderColor), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), this.m_camera, Position.Center, true);
		}
		if (this.m_shadow)
		{
			Color color;
			color..ctor(0f, 0f, 0f, 0.3f);
			PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_prefabTC, new Vector3(this.m_actualHeight * 0.015f, -this.m_actualHeight * 0.015f, -1f), array, this.m_actualHeight * 0.18f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), this.m_camera, Position.Center, true);
		}
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x000B2B21 File Offset: 0x000B0F21
	public override void Destroy()
	{
		this.m_texture = null;
		if (this.m_material != null)
		{
			Object.Destroy(this.m_material);
		}
		this.m_material = null;
		base.Destroy();
		this.m_prefabTC = null;
		this.m_pic = null;
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x000B2B64 File Offset: 0x000B0F64
	public void RankDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] circle = DebugDraw.GetCircle(_c.m_actualHeight * 0.5f, 16, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, circle, DebugDraw.HexToUint("fff9e6"), DebugDraw.HexToUint("fff9e6"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera, "Rank", UVRect.Normal());
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.1f, circle, _c.m_actualHeight * 0.3f, DebugDraw.HexToColor("fff9e6"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), this.m_camera, Position.Center, true);
	}

	// Token: 0x04001521 RID: 5409
	public TransformC m_prefabTC;

	// Token: 0x04001522 RID: 5410
	public Texture2D m_texture;

	// Token: 0x04001523 RID: 5411
	private PrefabC m_pic;

	// Token: 0x04001524 RID: 5412
	public string m_facebookId;

	// Token: 0x04001525 RID: 5413
	public bool m_fetchingImage;

	// Token: 0x04001526 RID: 5414
	public Material m_material;

	// Token: 0x04001527 RID: 5415
	public bool m_shadow;

	// Token: 0x04001528 RID: 5416
	public bool m_largePicture;

	// Token: 0x04001529 RID: 5417
	public float m_borderWidth;

	// Token: 0x0400152A RID: 5418
	public float m_borderRadius;

	// Token: 0x0400152B RID: 5419
	public string m_borderColor;

	// Token: 0x0400152C RID: 5420
	public string m_hatIdentifier;

	// Token: 0x0400152D RID: 5421
	public Frame m_frame;

	// Token: 0x0400152E RID: 5422
	public bool m_playerProfile;

	// Token: 0x0400152F RID: 5423
	public bool m_border;
}
