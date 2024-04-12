using System;
using Server;
using UnityEngine;

// Token: 0x02000242 RID: 578
public class PsUIFittedUrlPicture : UICanvas
{
	// Token: 0x06001189 RID: 4489 RVA: 0x000AA2BC File Offset: 0x000A86BC
	public PsUIFittedUrlPicture(UIComponent _parent, bool _touchable, string _tag, string _url)
		: base(_parent, _touchable, _tag, null, string.Empty)
	{
		this.m_url = _url;
		this.m_key = _url;
		string key = this.m_key;
		for (int i = 0; i < key.Length; i++)
		{
			char c = key.get_Chars(i);
			if ((c < '0' || c > '9') && (c < 'A' || c > 'Z') && (c < 'a' || c > 'z') && c != '.' && c != '_')
			{
				this.m_key = this.m_key.Replace(c, '_');
			}
		}
		this.LoadPicture();
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x000AA368 File Offset: 0x000A8768
	private void LoadPicture()
	{
		this.m_loading = new PsUILoadingAnimation(this, false);
		CacheItem<byte[]> item = PsCaches.m_urlPictures.GetItem(this.m_key);
		if (item != null)
		{
			this.HandlePicture(item.GetContent());
			return;
		}
		byte[] content = PsCaches.m_urlPicturesDiskCache.GetContent(this.m_key);
		if (content != null)
		{
			PsCaches.m_urlPictures.AddItem(this.m_key, content);
			this.HandlePicture(content);
			return;
		}
		if (!this.m_fetchingPicture)
		{
			HttpC httpC = Request.PreloaderGet(this.m_url, "pictureUrl", new Action<HttpC>(this.Success), new Action<HttpC>(this.Failure), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			this.m_fetchingPicture = true;
		}
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x000AA42C File Offset: 0x000A882C
	private void Success(HttpC _httpc)
	{
		byte[] bytes = _httpc.www.bytes;
		PsCaches.m_urlPictures.AddItem(this.m_key, bytes);
		PsCaches.m_urlPicturesDiskCache.AddItem(this.m_key, bytes);
		if (!this.m_hidden)
		{
			this.HandlePicture(bytes);
		}
		else
		{
			this.m_handleWhenVisible = true;
		}
		this.m_fetchingPicture = false;
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x000AA48D File Offset: 0x000A888D
	private void Failure(HttpC _httpc)
	{
		if (this.m_loading != null)
		{
			this.m_loading.Destroy();
			this.m_loading = null;
		}
		this.m_fetchingPicture = false;
		Debug.LogError("Didn't load the picture.");
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x000AA4C0 File Offset: 0x000A88C0
	protected virtual void HandlePicture(byte[] _bytes)
	{
		if (this.m_loading != null)
		{
			this.m_loading.Destroy();
			this.m_loading = null;
		}
		this.RemovePicture();
		this.m_texture = new Texture2D(10, 10, 3, false);
		ImageConversion.LoadImage(this.m_texture, _bytes);
		if (this.m_texture.width > 255)
		{
			this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ScreenshotMat_Material));
			this.m_material.mainTexture = this.m_texture;
			this.Update();
		}
		else
		{
			this.RemovePicture();
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000AA55C File Offset: 0x000A895C
	private void RemovePicture()
	{
		if (this.m_texture != null && this.m_material != null)
		{
			Object.Destroy(this.m_material);
			Object.Destroy(this.m_texture);
			this.m_texture = null;
		}
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x000AA5A8 File Offset: 0x000A89A8
	public override void Update()
	{
		if (!this.m_hidden && this.m_material != null)
		{
			this.SetWidth(1f, RelativeTo.ParentWidth);
			this.SetHeight(1f, RelativeTo.ParentHeight);
			this.CalculateReferenceSizes();
			this.UpdateSize();
			this.UpdateMargins();
			float num = 0f;
			float num2 = 0f;
			if (this.m_material.mainTexture.height > 0)
			{
				num2 = this.m_actualHeight / (float)this.m_material.mainTexture.height;
			}
			if (this.m_material.mainTexture.width > 0)
			{
				num = this.m_actualWidth / (float)this.m_material.mainTexture.width;
			}
			float num3 = Mathf.Min(num2, num);
			float num4 = (float)this.m_material.mainTexture.height / (float)this.m_material.mainTexture.width;
			if (this.m_parent.m_actualHeight != 0f && this.m_parent.m_actualWidth != 0f)
			{
				if (num2 < num)
				{
					this.SetWidth(this.m_parent.m_actualHeight / num4 / this.m_parent.m_actualWidth, RelativeTo.ParentWidth);
				}
				else
				{
					this.SetHeight(this.m_actualWidth * num4 / this.m_actualHeight, RelativeTo.ParentHeight);
				}
			}
		}
		base.Update();
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x000AA704 File Offset: 0x000A8B04
	public override void Destroy()
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		if (this.m_material != null)
		{
			if (this.m_material.mainTexture != null && this.m_material.mainTexture.GetInstanceID() < 0)
			{
				Object.Destroy(this.m_material.mainTexture);
			}
			Object.Destroy(this.m_material);
		}
		if (this.m_texture != null)
		{
			Object.Destroy(this.m_texture);
		}
		this.m_texture = null;
		this.m_material = null;
		base.Destroy();
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x000AA7AA File Offset: 0x000A8BAA
	public override void Step()
	{
		if (!this.m_hidden && this.m_handleWhenVisible)
		{
			this.LoadPicture();
			this.m_handleWhenVisible = false;
		}
		base.Step();
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x000AA7D8 File Offset: 0x000A8BD8
	public override void DrawHandler(UIComponent _c)
	{
		_c.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, false);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		UVRect uvrect = new UVRect(0f, 0f, 1f, 1f);
		uint num = DebugDraw.HexToUint("#1a64a8");
		if (this.m_material != null)
		{
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 1f + Vector3.up * 2.5f, rect, uint.MaxValue, uint.MaxValue, this.m_material, this.m_camera, string.Empty, uvrect);
		}
	}

	// Token: 0x0400147F RID: 5247
	public string m_url;

	// Token: 0x04001480 RID: 5248
	public string m_key;

	// Token: 0x04001481 RID: 5249
	public Texture2D m_texture;

	// Token: 0x04001482 RID: 5250
	public Material m_material;

	// Token: 0x04001483 RID: 5251
	public bool m_fetchingPicture;

	// Token: 0x04001484 RID: 5252
	public bool m_handleWhenVisible;

	// Token: 0x04001485 RID: 5253
	private PsUILoadingAnimation m_loading;
}
