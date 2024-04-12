using System;
using UnityEngine;

// Token: 0x02000585 RID: 1413
public class UI3DRenderTextureCanvas : UISprite
{
	// Token: 0x06002919 RID: 10521 RVA: 0x000C9C28 File Offset: 0x000C8028
	public UI3DRenderTextureCanvas(UIComponent _parent, string _tag, string _shaderPath = null, bool _touchable = false)
		: base(_parent, _touchable, _tag, null, new Frame(0f, 0f, 0f, 0f), true)
	{
		this.m_shaderPath = _shaderPath;
		if (string.IsNullOrEmpty(this.m_shaderPath))
		{
			this.m_shaderPath = "Framework/VertexColorUnlitDoubleRenderTexture";
		}
		this.m_3DEntity = EntityManager.AddEntity(new string[] { "UIComponent", _tag });
		this.m_3DCameraPivot = TransformS.AddComponent(this.m_3DEntity, "UI 3D Camera pivot");
		TransformS.ParentComponent(this.m_3DCameraPivot, this.m_TC, Vector3.zero);
		this.m_3DCamera = CameraS.AddCamera("UI 3D Camera", false, 3);
		this.m_3DCamera.clearFlags = 3;
		this.m_3DCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
		this.m_3DCamera.transform.SetParent(this.m_3DCameraPivot.transform);
		this.m_3DCamera.transform.localPosition = Vector3.zero;
		this.m_3DCameraPivot.transform.gameObject.layer = this.m_3DCamera.gameObject.layer;
		this.m_3DContent = TransformS.AddComponent(this.m_3DEntity, "Content");
		TransformS.ParentComponent(this.m_3DContent, this.m_TC, Vector3.zero);
		PsState.m_activeRenderTextures.Add(this);
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x000C9D9E File Offset: 0x000C819E
	public override void RemoveDrawHandler()
	{
		Debug.LogError("Do not call this method.");
	}

	// Token: 0x0600291B RID: 10523 RVA: 0x000C9DAC File Offset: 0x000C81AC
	public override void DrawHandler(UIComponent _c)
	{
		this.ReleaseAndDestroyRenderTexture();
		this.m_renderTexture = new RenderTexture((int)_c.m_actualWidth, (int)_c.m_actualHeight, 24, 0);
		if (_c.m_actualWidth <= 0f || _c.m_actualHeight <= 0f)
		{
			return;
		}
		this.m_3DCamera.targetTexture = this.m_renderTexture;
		if (this.m_material == null)
		{
			this.m_material = new Material(Shader.Find(this.m_shaderPath));
		}
		this.m_material.mainTexture = this.m_3DCamera.targetTexture;
		if (this.m_spriteSheet == null)
		{
			this.m_spriteSheet = SpriteS.AddSpriteSheet(this.m_camera, this.m_material, 1f);
		}
		this.m_frame = new Frame(0f, 0f, (float)this.m_3DCamera.targetTexture.width, (float)this.m_3DCamera.targetTexture.height);
		base.DrawHandler(_c);
	}

	// Token: 0x0600291C RID: 10524 RVA: 0x000C9EB0 File Offset: 0x000C82B0
	public virtual PrefabC AddGameObject(GameObject _go, Vector3 _position, Vector3 _rotation = default(Vector3))
	{
		PrefabC prefabC = PrefabS.AddComponent(this.m_3DContent, _position, _go, _go.name, true, true);
		PrefabS.SetCamera(prefabC, this.m_3DCamera);
		prefabC.p_gameObject.transform.rotation = Quaternion.Euler(_rotation);
		return prefabC;
	}

	// Token: 0x0600291D RID: 10525 RVA: 0x000C9EF8 File Offset: 0x000C82F8
	public void ReleaseAndDestroyRenderTexture()
	{
		this.m_3DCamera.enabled = false;
		this.m_3DCamera.targetTexture = null;
		if (this.m_material != null)
		{
			this.m_material.mainTexture = null;
		}
		if (this.m_renderTexture != null)
		{
			Object.Destroy(this.m_renderTexture);
			this.m_renderTexture = null;
		}
		this.m_3DCamera.enabled = true;
	}

	// Token: 0x0600291E RID: 10526 RVA: 0x000C9F6C File Offset: 0x000C836C
	public void RecreateRenderTexture()
	{
		this.m_renderTexture = new RenderTexture((int)this.m_actualWidth, (int)this.m_actualHeight, 24, 0);
		this.m_3DCamera.targetTexture = this.m_renderTexture;
		this.m_material.mainTexture = this.m_3DCamera.targetTexture;
	}

	// Token: 0x0600291F RID: 10527 RVA: 0x000C9FBC File Offset: 0x000C83BC
	public override void Update()
	{
		this.m_3DCamera.transform.localPosition = new Vector3(0f, 0f, this.m_3DCameraOffset);
		base.Update();
	}

	// Token: 0x06002920 RID: 10528 RVA: 0x000C9FEC File Offset: 0x000C83EC
	public override void Step()
	{
		if (this.m_renderTexture != null && !this.m_renderTexture.IsCreated())
		{
			this.m_renderTexture.Create();
			this.m_3DCamera.targetTexture = this.m_renderTexture;
		}
		if (!this.m_TC.m_active && this.m_3DCamera.enabled)
		{
			this.m_3DCamera.enabled = false;
		}
		else if (this.m_TC.m_active && !this.m_3DCamera.enabled)
		{
			this.m_3DCamera.enabled = true;
		}
		if (this.m_renderTexture != null)
		{
			Graphics.SetRenderTarget(this.m_renderTexture);
			GL.Clear(true, true, new Color(0f, 0f, 0f, 0f));
			Graphics.SetRenderTarget(null);
		}
		base.Step();
	}

	// Token: 0x06002921 RID: 10529 RVA: 0x000CA0DC File Offset: 0x000C84DC
	public override void Destroy()
	{
		if (this.m_material != null)
		{
			Object.Destroy(this.m_material);
			this.m_material = null;
		}
		if (this.m_spriteSheet != null)
		{
			SpriteS.RemoveSpriteSheet(this.m_spriteSheet);
			this.m_spriteSheet = null;
		}
		if (this.m_renderTexture != null)
		{
			Object.Destroy(this.m_renderTexture);
			this.m_renderTexture = null;
		}
		if (this.m_3DCamera != null)
		{
			CameraS.RemoveCamera(this.m_3DCamera);
			this.m_3DCamera = null;
		}
		if (this.m_3DEntity != null)
		{
			EntityManager.RemoveEntity(this.m_3DEntity);
			this.m_3DEntity = null;
		}
		PsState.m_activeRenderTextures.Remove(this);
		base.Destroy();
	}

	// Token: 0x04002E1F RID: 11807
	private Entity m_3DEntity;

	// Token: 0x04002E20 RID: 11808
	public TransformC m_3DContent;

	// Token: 0x04002E21 RID: 11809
	public Camera m_3DCamera;

	// Token: 0x04002E22 RID: 11810
	public float m_3DCameraOffset = -500f;

	// Token: 0x04002E23 RID: 11811
	public TransformC m_3DCameraPivot;

	// Token: 0x04002E24 RID: 11812
	public Rect m_cameraAddRect;

	// Token: 0x04002E25 RID: 11813
	public Material m_material;

	// Token: 0x04002E26 RID: 11814
	private RenderTexture m_renderTexture;

	// Token: 0x04002E27 RID: 11815
	private int m_renderTextureWidth;

	// Token: 0x04002E28 RID: 11816
	private int m_renderTextureHeight;

	// Token: 0x04002E29 RID: 11817
	private string m_shaderPath;
}
