using System;
using UnityEngine;

// Token: 0x020004F7 RID: 1271
public class StoredRenderTexture
{
	// Token: 0x0600248E RID: 9358 RVA: 0x00193458 File Offset: 0x00191858
	public StoredRenderTexture()
	{
	}

	// Token: 0x0600248F RID: 9359 RVA: 0x00193460 File Offset: 0x00191860
	public StoredRenderTexture(RenderTexture _renderTextureToStore)
	{
		this.StoreRenderTexture(_renderTextureToStore);
	}

	// Token: 0x06002490 RID: 9360 RVA: 0x00193470 File Offset: 0x00191870
	public void StoreRenderTexture(RenderTexture _renderTexture)
	{
		RenderTexture active = RenderTexture.active;
		if (this.m_storedRT != null)
		{
			Object.Destroy(this.m_storedRT);
		}
		this.m_storedRT = new Texture2D(_renderTexture.width, _renderTexture.height, 3, false, true);
		this.m_storedRT.wrapMode = 1;
		RenderTexture.active = _renderTexture;
		this.m_storedRT.ReadPixels(new Rect(0f, 0f, (float)_renderTexture.width, (float)_renderTexture.height), 0, 0);
		this.m_storedRT.Apply();
		RenderTexture.active = active;
	}

	// Token: 0x06002491 RID: 9361 RVA: 0x00193506 File Offset: 0x00191906
	public void ShowStoredRenderTexture()
	{
		CameraS.EnableRenderTextureView();
		CameraS.m_renderTextureViewMaterial.mainTexture = this.m_storedRT;
		CameraS.m_renderTextureViewMaterial.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06002492 RID: 9362 RVA: 0x00193540 File Offset: 0x00191940
	public void FadeOutRenderTexture(float _duration)
	{
		this.m_fadingOut = true;
		this.m_fadeTimer = 0f;
		this.m_fadeDuration = _duration;
		this.ShowStoredRenderTexture();
		CameraS.m_renderTextureViewMaterial.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06002493 RID: 9363 RVA: 0x00193590 File Offset: 0x00191990
	public void FadeInRenderTexture(float _duration)
	{
		this.m_fadingIn = true;
		this.m_fadeTimer = 0f;
		this.m_fadeDuration = _duration;
		this.ShowStoredRenderTexture();
		CameraS.m_renderTextureViewMaterial.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x06002494 RID: 9364 RVA: 0x001935E0 File Offset: 0x001919E0
	public void Update()
	{
		if (this.m_fadingOut || this.m_fadingIn)
		{
			this.m_fadeTimer += Main.m_gameDeltaTime;
			if (this.m_fadeTimer >= this.m_fadeDuration)
			{
				if (this.m_fadingOut)
				{
					CameraS.DisableRenderTextureView();
				}
				this.m_fadingOut = false;
				this.m_fadingIn = false;
			}
			else
			{
				float num = this.m_fadeTimer / this.m_fadeDuration;
				Color color = CameraS.m_renderTextureViewMaterial.color;
				if (this.m_fadingOut)
				{
					CameraS.m_renderTextureViewMaterial.color = new Color(1f, 1f, 1f, 1f - num);
				}
				else if (this.m_fadingIn)
				{
					CameraS.m_renderTextureViewMaterial.color = new Color(1f, 1f, 1f, num);
				}
			}
		}
	}

	// Token: 0x06002495 RID: 9365 RVA: 0x001936C1 File Offset: 0x00191AC1
	public void Destroy()
	{
		if (this.m_storedRT != null)
		{
			Object.Destroy(this.m_storedRT);
			this.m_storedRT = null;
		}
	}

	// Token: 0x04002A69 RID: 10857
	public Texture2D m_storedRT;

	// Token: 0x04002A6A RID: 10858
	private bool m_fadingOut;

	// Token: 0x04002A6B RID: 10859
	private bool m_fadingIn;

	// Token: 0x04002A6C RID: 10860
	private float m_fadeTimer;

	// Token: 0x04002A6D RID: 10861
	private float m_fadeDuration;
}
