using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class Screenshot : MonoBehaviour
{
	// Token: 0x06000BAE RID: 2990 RVA: 0x00073EE0 File Offset: 0x000722E0
	public void Initialize(int _width, int _height)
	{
		RenderTextureFormat renderTextureFormat = 4;
		if (SystemInfo.graphicsDeviceType == 16)
		{
			renderTextureFormat = 7;
		}
		this.m_texture = new RenderTexture(_width, _height, 24, renderTextureFormat);
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00073F10 File Offset: 0x00072310
	private void Update()
	{
		if (this.m_takeShot)
		{
			this.m_takeShot = false;
			RenderTexture targetTexture = this.m_camera.targetTexture;
			Vector3 position = this.m_camera.transform.position;
			this.m_camera.targetTexture = this.m_texture;
			this.m_camera.transform.position = this.m_shotCamPos;
			this.m_camera.aspect = 1f;
			this.m_camera.Render();
			this.m_camera.targetTexture = targetTexture;
			this.m_camera.transform.position = position;
			this.m_camera.ResetAspect();
			this.m_hasShot = true;
			if (this.m_tex2D != null)
			{
				Object.Destroy(this.m_tex2D);
			}
			this.m_tex2D = null;
		}
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x00073FE0 File Offset: 0x000723E0
	private void OnApplicationPause(bool _pauseStatus)
	{
		if (!_pauseStatus)
		{
			if (this.m_tex2D != null)
			{
				Graphics.Blit(this.m_tex2D, this.m_texture);
			}
		}
		else if (this.m_texture != null && this.m_tex2D == null)
		{
			this.GetScreenshotTex2D();
		}
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x00074043 File Offset: 0x00072443
	public void TakeScreenshot(Camera _cam, Vector3 _shotCamPos)
	{
		this.m_takeShot = true;
		this.m_camera = _cam;
		this.m_shotCamPos = _shotCamPos;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0007405C File Offset: 0x0007245C
	public byte[] GetScreenshotJPGBytes()
	{
		if (this.m_texture != null)
		{
			Texture2D screenshotTex2D = this.GetScreenshotTex2D();
			byte[] array = ImageConversion.EncodeToJPG(screenshotTex2D, 90);
			Object.DestroyImmediate(screenshotTex2D);
			return array;
		}
		return null;
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00074094 File Offset: 0x00072494
	public Texture2D GetScreenshotTex2D()
	{
		if (this.m_texture != null && this.m_tex2D == null)
		{
			this.m_tex2D = new Texture2D(this.m_texture.width, this.m_texture.height, 3, false, true);
			this.m_tex2D.wrapMode = 1;
			RenderTexture.active = this.m_texture;
			this.m_tex2D.ReadPixels(new Rect(0f, 0f, (float)this.m_texture.width, (float)this.m_texture.height), 0, 0);
			this.m_tex2D.Apply();
			RenderTexture.active = null;
			return this.m_tex2D;
		}
		if (this.m_tex2D != null)
		{
			return this.m_tex2D;
		}
		return null;
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x00074163 File Offset: 0x00072563
	public void OnDestroy()
	{
		if (this.m_texture != null)
		{
			Object.DestroyImmediate(this.m_texture);
			this.m_texture = null;
		}
		this.m_camera = null;
	}

	// Token: 0x04000A52 RID: 2642
	public bool m_hasShot;

	// Token: 0x04000A53 RID: 2643
	private bool m_takeShot;

	// Token: 0x04000A54 RID: 2644
	private RenderTexture m_texture;

	// Token: 0x04000A55 RID: 2645
	private Camera m_camera;

	// Token: 0x04000A56 RID: 2646
	private Vector3 m_shotCamPos;

	// Token: 0x04000A57 RID: 2647
	private Texture2D m_tex2D;
}
