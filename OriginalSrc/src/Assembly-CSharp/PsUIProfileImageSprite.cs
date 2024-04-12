using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class PsUIProfileImageSprite
{
	// Token: 0x0600120D RID: 4621 RVA: 0x000B2E20 File Offset: 0x000B1220
	public PsUIProfileImageSprite(string _facebookId, Camera _camera, TransformC _parent, string _defaultHatIdentifier = null, Vector3 _offset = default(Vector3), Shader _shader = null)
	{
		this.m_facebookId = _facebookId;
		this.m_camera = _camera;
		this.m_shader = _shader;
		this.m_TC = TransformS.AddComponent(_parent.p_entity, "ProfileImagePrefab");
		TransformS.ParentComponent(this.m_TC, _parent, _offset);
		this.m_hatIdentifier = _defaultHatIdentifier;
		this.LoadPicture();
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x000B2ED0 File Offset: 0x000B12D0
	public void LoadPicture()
	{
		if (this.m_facebookId != null)
		{
			Texture2D content = PsCaches.m_profileImages.GetContent(this.m_facebookId);
			if (content != null)
			{
				this.DisplayPicture(content);
			}
			else if (!this.m_fetchingImage)
			{
				HttpC picture = FacebookManager.GetPicture(this.m_facebookId, new Action<Texture2D>(this.PictureDownloadOk), new Action<HttpC>(this.PictureDownloadFailed), this.m_largePicture);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, picture);
				this.m_fetchingImage = true;
			}
		}
		else if (string.IsNullOrEmpty(this.m_hatIdentifier))
		{
			this.DisplayPicture(ResourceManager.GetTexture(RESOURCE.AnonymousProfile_Texture2D) as Texture2D);
		}
		else
		{
			this.DisplayPicture(PsState.m_uiSheet.m_atlas.GetFrame("menu_profilepic_" + this.m_hatIdentifier, null));
		}
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x000B2FB3 File Offset: 0x000B13B3
	private void PictureDownloadFailed(HttpC _c)
	{
		this.DisplayPicture(ResourceManager.GetTexture(RESOURCE.AnonymousProfile_Texture2D) as Texture2D);
		this.m_fetchingImage = false;
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x000B2FD1 File Offset: 0x000B13D1
	private void PictureDownloadOk(Texture2D _texture)
	{
		PsCaches.m_profileImages.AddItem(this.m_facebookId, _texture);
		this.DisplayPicture(_texture);
		this.m_fetchingImage = false;
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x000B2FF4 File Offset: 0x000B13F4
	private void DisplayPicture(Texture2D _texture)
	{
		if (_texture != this.m_texture)
		{
			this.m_texture = _texture;
			if (this.m_TC != null)
			{
				if (this.m_material != null)
				{
					Object.Destroy(this.m_material);
				}
				this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ProfileMat_Material));
				this.m_material.mainTexture = this.m_texture;
				float width = this.m_width;
				float num = this.m_widthRatio * this.m_width;
				Vector2[] array = DebugDraw.GetRoundedRect(width, num, num * this.m_borderRadius, 4, Vector2.zero);
				this.m_prefabs = new List<PrefabC>();
				this.m_prefabs.Add(PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.forward * -0.1f, array, DebugDraw.HexToUint("ffffff"), DebugDraw.HexToUint("ffffff"), this.m_material, this.m_camera, "Profile", UVRect.Normal()));
				array = DebugDraw.GetRoundedRect(width + num * 0.025f, num + num * 0.025f, num * this.m_borderRadius, 4, Vector2.zero);
				Material material = new Material(ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material));
				if (this.m_shader != null)
				{
					this.m_material.shader = this.m_shader;
					material.shader = this.m_shader;
				}
				this.m_prefabs.Add(PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, Vector3.forward * -0.15f, array, this.m_actualHeight * this.m_borderWidth, DebugDraw.HexToColor(this.m_borderColor), material, this.m_camera, Position.Center, true));
				if (this.m_shadow)
				{
					Color color;
					color..ctor(0f, 0f, 0f, 0.3f);
					this.m_prefabs.Add(PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, new Vector3(this.m_actualHeight * 0.015f, -this.m_actualHeight * 0.015f, 0f), array, this.m_actualHeight * 0.18f, color, material, this.m_camera, Position.Center, true));
				}
				if (!this.m_TC.p_entity.m_visible)
				{
					PrefabS.SetVisibilityByTransformComponent(this.m_TC, false, false, false);
				}
			}
		}
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x000B3234 File Offset: 0x000B1634
	private void DisplayPicture(Frame _frame)
	{
		if (this.m_TC != null)
		{
			if (this.m_material != null)
			{
				Object.Destroy(this.m_material);
			}
			this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ProfileMat_Material));
			this.m_material.mainTexture = PsState.m_uiSheet.m_material.mainTexture;
			if (this.m_shader != null)
			{
				this.m_material.shader = this.m_shader;
			}
			float actualWidth = this.m_actualWidth;
			float actualHeight = this.m_actualHeight;
			Vector2[] array = DebugDraw.GetRoundedRect(actualWidth, actualHeight, actualHeight * this.m_borderRadius, 4, Vector2.zero);
			float num = (float)this.m_material.mainTexture.width;
			float num2 = (float)this.m_material.mainTexture.height;
			UVRect uvrect = new UVRect(_frame.x / num, 1f - _frame.y / num2 - _frame.height / num2, _frame.width / num, _frame.height / num2);
			this.m_prefabs = new List<PrefabC>();
			this.m_prefabs.Add(PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.forward * -0.1f, array, DebugDraw.HexToUint("ffffff"), DebugDraw.HexToUint("ffffff"), this.m_material, this.m_camera, "Profile", uvrect));
			array = DebugDraw.GetRoundedRect(actualWidth + actualHeight * 0.025f, actualHeight + actualHeight * 0.025f, actualHeight * this.m_borderRadius, 4, Vector2.zero);
			Material material = new Material(ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material));
			if (this.m_shader != null)
			{
				this.m_material.shader = this.m_shader;
				material.shader = this.m_shader;
			}
			this.m_prefabs.Add(PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, new Vector3(0f, 0f, 0.01f), array, this.m_actualHeight * this.m_borderWidth, DebugDraw.HexToColor(this.m_borderColor), material, this.m_camera, Position.Center, true));
			if (this.m_shadow)
			{
				Color color;
				color..ctor(0f, 0f, 0f, 0.3f);
				this.m_prefabs.Add(PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, new Vector3(this.m_actualHeight * 0.015f, -this.m_actualHeight * 0.015f, 0f), array, this.m_actualHeight * 0.18f, color, material, this.m_camera, Position.Center, true));
			}
			if (!this.m_TC.p_entity.m_visible)
			{
				PrefabS.SetVisibilityByTransformComponent(this.m_TC, false, false, false);
			}
		}
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x000B34E6 File Offset: 0x000B18E6
	public void Destroy()
	{
	}

	// Token: 0x04001532 RID: 5426
	private string m_facebookId;

	// Token: 0x04001533 RID: 5427
	private bool m_fetchingImage;

	// Token: 0x04001534 RID: 5428
	private Camera m_camera;

	// Token: 0x04001535 RID: 5429
	private Texture2D m_texture;

	// Token: 0x04001536 RID: 5430
	private List<PrefabC> m_prefabs;

	// Token: 0x04001537 RID: 5431
	private Material m_material;

	// Token: 0x04001538 RID: 5432
	private float m_borderWidth = 0.1f;

	// Token: 0x04001539 RID: 5433
	private float m_borderRadius = 0.06f;

	// Token: 0x0400153A RID: 5434
	private string m_borderColor = "fff9e6";

	// Token: 0x0400153B RID: 5435
	private float m_actualHeight = 1f;

	// Token: 0x0400153C RID: 5436
	private float m_actualWidth = 1f;

	// Token: 0x0400153D RID: 5437
	private float m_width = 1f;

	// Token: 0x0400153E RID: 5438
	private float m_widthRatio = 1f;

	// Token: 0x0400153F RID: 5439
	private bool m_largePicture = true;

	// Token: 0x04001540 RID: 5440
	private bool m_shadow;

	// Token: 0x04001541 RID: 5441
	private string m_hatIdentifier;

	// Token: 0x04001542 RID: 5442
	private Shader m_shader;

	// Token: 0x04001543 RID: 5443
	public TransformC m_TC;
}
