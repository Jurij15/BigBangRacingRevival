using System;
using UnityEngine;

// Token: 0x0200044C RID: 1100
public class SplashLoad : MonoBehaviour
{
	// Token: 0x06001E88 RID: 7816 RVA: 0x0015854C File Offset: 0x0015694C
	private void Start()
	{
		PsMetrics.SplashScreenStarted();
		EveryplayManager.SetEnabled(PsState.m_everyplayEnabled);
		EveryplayManager.Initialize();
		Color color = this.m_fadeMaterial.color;
		color.a = 0f;
		this.m_fadeMaterial.color = color;
		this.m_triggered = false;
		this.loadOperation = Application.LoadLevelAsync("MainScene");
		this.loadOperation.allowSceneActivation = false;
	}

	// Token: 0x06001E89 RID: 7817 RVA: 0x001585B4 File Offset: 0x001569B4
	private void Update()
	{
		this.m_timer += Time.deltaTime;
		if (this.m_timer >= 3.5f)
		{
			if (this.m_fadeTimer <= 0f)
			{
				this.m_fadeTimer = 0f;
				if (!this.m_triggered)
				{
					PsMetrics.SplashScreenFinished();
					this.loadOperation.allowSceneActivation = true;
					this.m_triggered = true;
				}
			}
			float num = (0.5f - this.m_fadeTimer) / 0.5f;
			Color color = this.m_fadeMaterial.color;
			color.a = num;
			this.m_fadeMaterial.color = color;
			this.m_fadeTimer -= Time.deltaTime;
			this.m_timer = 3.5f;
		}
	}

	// Token: 0x040021B8 RID: 8632
	public Material m_fadeMaterial;

	// Token: 0x040021B9 RID: 8633
	private float m_timer;

	// Token: 0x040021BA RID: 8634
	private float m_fadeTimer = 0.5f;

	// Token: 0x040021BB RID: 8635
	private bool m_triggered;

	// Token: 0x040021BC RID: 8636
	private AsyncOperation loadOperation;
}
