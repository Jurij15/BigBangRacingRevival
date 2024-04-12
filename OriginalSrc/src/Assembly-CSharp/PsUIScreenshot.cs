using System;
using Server;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class PsUIScreenshot : UIComponent
{
	// Token: 0x06001223 RID: 4643 RVA: 0x000AE6D0 File Offset: 0x000ACAD0
	public PsUIScreenshot(UIComponent _parent, bool _touchable, string _tag, Vector2 _offset, PsGameLoop _minigameInfo, bool _loadImmediately = true, bool _createBorder = true, float _cornerSize = 0.045f, bool _useTutorialTexture = false)
		: base(_parent, _touchable, _tag, null, null, string.Empty)
	{
		this.m_useMinigameId = false;
		this.m_cornerSize = _cornerSize;
		this.m_offset = _offset;
		this.m_useTutorialTexture = _useTutorialTexture;
		this.SetListenCameraEvents(true);
		this.m_loop = _minigameInfo;
		if (this.m_loop != null && !this.m_loop.m_loadingMetaData)
		{
			this.m_gameId = _minigameInfo.GetGameId();
		}
		this.m_fetchingScreenshot = false;
		if (!this.m_useTutorialTexture)
		{
			this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ScreenshotMat_Material));
			this.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
		}
		else
		{
			this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ScreenshotTutorialMat_Material));
		}
		if (_loadImmediately)
		{
			this.LoadPicture();
		}
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x000AE7A6 File Offset: 0x000ACBA6
	public override void Step()
	{
		if (!this.m_hidden && this.m_handleWhenVisible)
		{
			this.LoadPicture();
			this.m_handleWhenVisible = false;
		}
		base.Step();
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x000AE7D4 File Offset: 0x000ACBD4
	public void LoadPicture()
	{
		CacheItem<byte[]> item = PsCaches.m_screenshots.GetItem(this.m_gameId);
		if (item != null)
		{
			this.HandleScreenshot(item.GetContent());
			return;
		}
		byte[] content = PsCaches.m_screenshotsDiskCache.GetContent(this.m_gameId);
		if (content != null)
		{
			PsCaches.m_screenshots.AddItem(this.m_gameId, content);
			this.HandleScreenshot(content);
			return;
		}
		if (this.m_useMinigameId)
		{
			if ((!this.m_fetchingScreenshot && !string.IsNullOrEmpty(this.m_gameId)) || this.m_forceLoad)
			{
				HttpC httpC = global::Server.Screenshot.Get(this.m_gameId, new Action<byte[]>(this.ScreenshotRequestSUCCEED), new Action<HttpC>(this.ScreenshotRequestFAILED), null);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
				this.m_fetchingScreenshot = true;
			}
			else
			{
				if (string.IsNullOrEmpty(this.m_gameId))
				{
					Debug.LogWarning("Empty m_gameId when fetching screenshot!");
				}
				this.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
			}
		}
		else if (!this.m_fetchingScreenshot && ((this.m_loop != null && this.m_loop.m_context != PsMinigameContext.Saved && this.m_loop.m_minigameMetaData.published && !string.IsNullOrEmpty(this.m_gameId)) || this.m_forceLoad))
		{
			HttpC httpC2 = global::Server.Screenshot.Get(this.m_gameId, new Action<byte[]>(this.ScreenshotRequestSUCCEED), new Action<HttpC>(this.ScreenshotRequestFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC2);
			this.m_fetchingScreenshot = true;
		}
		else
		{
			if (string.IsNullOrEmpty(this.m_gameId))
			{
				Debug.LogWarning("Empty m_gameId when fetching screenshot!");
			}
			this.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
		}
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x000AE9A5 File Offset: 0x000ACDA5
	public void RemoveScreenshot()
	{
		if (this.m_texture != null)
		{
			this.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
			Object.Destroy(this.m_texture);
			this.m_texture = null;
		}
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x000AE9E0 File Offset: 0x000ACDE0
	private void ScreenshotRequestSUCCEED(byte[] _bytes)
	{
		PsCaches.m_screenshots.AddItem(this.m_gameId, _bytes);
		PsCaches.m_screenshotsDiskCache.AddItem(this.m_gameId, _bytes);
		if (!this.m_hidden)
		{
			this.HandleScreenshot(_bytes);
		}
		else
		{
			this.m_handleWhenVisible = true;
		}
		this.m_fetchingScreenshot = false;
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x000AEA35 File Offset: 0x000ACE35
	private void ScreenshotRequestFAILED(HttpC _request)
	{
		this.m_fetchingScreenshot = false;
		Debug.Log("Screenshot FAILED", null);
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x000AEA4C File Offset: 0x000ACE4C
	protected virtual void HandleScreenshot(byte[] _bytes)
	{
		this.RemoveScreenshot();
		this.m_texture = new Texture2D(10, 10, 3, false);
		ImageConversion.LoadImage(this.m_texture, _bytes);
		if (this.m_texture.width > 255)
		{
			this.m_material.mainTexture = this.m_texture;
		}
		else
		{
			this.RemoveScreenshot();
		}
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x000AEAB0 File Offset: 0x000ACEB0
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

	// Token: 0x0600122B RID: 4651 RVA: 0x000AEB58 File Offset: 0x000ACF58
	public override void DrawHandler(UIComponent _c)
	{
		_c.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, false);
		if (this.m_texture != null)
		{
			this.m_material.mainTexture = this.m_texture;
		}
		else
		{
			this.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
		}
		Vector2[] bezierRect = DebugDraw.GetBezierRect(_c.m_actualWidth - this.m_cornerSize * 3f * (float)Screen.height, _c.m_actualHeight - this.m_cornerSize * 3f * (float)Screen.height, this.m_cornerSize * (float)Screen.height, 20, Vector2.zero);
		Vector2[] bezierRect2 = DebugDraw.GetBezierRect(_c.m_actualWidth - this.m_cornerSize * 2.5f * (float)Screen.height, _c.m_actualHeight - this.m_cornerSize * 2.5f * (float)Screen.height, this.m_cornerSize * (float)Screen.height, 20, Vector2.zero);
		UVRect uvrect = new UVRect(0f, 0.55f - _c.m_actualHeight / _c.m_actualWidth / 2f, 1f, _c.m_actualHeight / _c.m_actualWidth);
		uint num = DebugDraw.HexToUint("#0156b2");
		uint num2 = DebugDraw.HexToUint("#2e8599");
		uint num3 = DebugDraw.HexToUint("#1a64a8");
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 5f, bezierRect2, num, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera, string.Empty, uvrect);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, bezierRect2, (float)Screen.height * 0.01f, DebugDraw.UIntToColor(num), DebugDraw.UIntToColor(num2), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 1f + Vector3.up * 2.5f, bezierRect, uint.MaxValue, uint.MaxValue, this.m_material, this.m_camera, string.Empty, uvrect);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f + Vector3.up * 2.5f, bezierRect, (float)Screen.height * 0.008f, DebugDraw.HexToColor("#41acee"), DebugDraw.HexToColor("#86d9f9"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f + Vector3.up * 2.5f, bezierRect, (float)Screen.height * 0.03f, new Color(1f, 1f, 1f, 0.3f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x000AEE48 File Offset: 0x000AD248
	public static void AltDrawHandler(UIComponent _c)
	{
		PsUIScreenshot psUIScreenshot = _c as PsUIScreenshot;
		psUIScreenshot.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(psUIScreenshot.m_TC.p_entity, false);
		if (!psUIScreenshot.m_useTutorialTexture)
		{
			if (psUIScreenshot.m_texture != null)
			{
				psUIScreenshot.m_material.mainTexture = psUIScreenshot.m_texture;
			}
			else
			{
				psUIScreenshot.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
			}
		}
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(psUIScreenshot.m_actualWidth, psUIScreenshot.m_actualHeight, psUIScreenshot.m_cornerSize * (float)Screen.height, 6, Vector2.zero);
		for (int i = 0; i < roundedRect.Length; i++)
		{
			Vector2[] array = roundedRect;
			int num = i;
			array[num].x = array[num].x + roundedRect[i].y * 0.15f;
		}
		UVRect uvrect = new UVRect(0f, 0.55f - psUIScreenshot.m_actualHeight / psUIScreenshot.m_actualWidth / 2f, 1f, psUIScreenshot.m_actualHeight / psUIScreenshot.m_actualWidth);
		uint num2 = DebugDraw.HexToUint("#0156b2");
		uint num3 = DebugDraw.HexToUint("#2e8599");
		uint num4 = DebugDraw.HexToUint("#1a64a8");
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(psUIScreenshot.m_TC, Vector3.forward, roundedRect, uint.MaxValue, uint.MaxValue, psUIScreenshot.m_material, psUIScreenshot.m_camera, string.Empty, uvrect);
		PrefabS.CreatePathPrefabComponentFromVectorArray(psUIScreenshot.m_TC, Vector3.zero, roundedRect, (float)Screen.height * 0.01f, DebugDraw.HexToColor("#559bd8"), DebugDraw.HexToColor("#559bd8"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), psUIScreenshot.m_camera, Position.Center, true);
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x000AEFEC File Offset: 0x000AD3EC
	public static void BasicDrawHandler(UIComponent _c)
	{
		PsUIScreenshot psUIScreenshot = _c as PsUIScreenshot;
		psUIScreenshot.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(psUIScreenshot.m_TC.p_entity, false);
		if (!psUIScreenshot.m_useTutorialTexture)
		{
			if (psUIScreenshot.m_texture != null)
			{
				psUIScreenshot.m_material.mainTexture = psUIScreenshot.m_texture;
			}
			else
			{
				psUIScreenshot.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
			}
		}
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(psUIScreenshot.m_actualWidth, psUIScreenshot.m_actualHeight, psUIScreenshot.m_cornerSize * (float)Screen.height, 6, Vector2.zero);
		UVRect uvrect = new UVRect(0f, 0.55f - psUIScreenshot.m_actualHeight / psUIScreenshot.m_actualWidth / 2f, 1f, psUIScreenshot.m_actualHeight / psUIScreenshot.m_actualWidth);
		uint num = DebugDraw.HexToUint("#0156b2");
		uint num2 = DebugDraw.HexToUint("#2e8599");
		uint num3 = DebugDraw.HexToUint("#1a64a8");
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(psUIScreenshot.m_TC, Vector3.forward, roundedRect, uint.MaxValue, uint.MaxValue, psUIScreenshot.m_material, psUIScreenshot.m_camera, string.Empty, uvrect);
		PrefabS.CreatePathPrefabComponentFromVectorArray(psUIScreenshot.m_TC, Vector3.zero, roundedRect, (float)Screen.height * 0.01f, DebugDraw.HexToColor("#559bd8"), DebugDraw.HexToColor("#559bd8"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), psUIScreenshot.m_camera, Position.Center, true);
	}

	// Token: 0x04001552 RID: 5458
	public Texture2D m_texture;

	// Token: 0x04001553 RID: 5459
	public string m_gameId;

	// Token: 0x04001554 RID: 5460
	public PsGameLoop m_loop;

	// Token: 0x04001555 RID: 5461
	public bool m_fetchingScreenshot;

	// Token: 0x04001556 RID: 5462
	public Material m_material;

	// Token: 0x04001557 RID: 5463
	public Vector2 m_offset;

	// Token: 0x04001558 RID: 5464
	protected float m_cornerSize;

	// Token: 0x04001559 RID: 5465
	public bool m_useTutorialTexture;

	// Token: 0x0400155A RID: 5466
	private bool m_handleWhenVisible;

	// Token: 0x0400155B RID: 5467
	public bool m_forceLoad;

	// Token: 0x0400155C RID: 5468
	public bool m_useMinigameId;
}
