using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class EffectGemMeteorStormSmallMeteor : MonoBehaviour
{
	// Token: 0x06000DBF RID: 3519 RVA: 0x00081417 File Offset: 0x0007F817
	private void Start()
	{
		this.trailMat = this.meteorTrailObject.GetComponent<Renderer>().material;
		this.smokeMat = this.meteorSmokeObject.GetComponent<Renderer>().material;
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00081448 File Offset: 0x0007F848
	private void Update()
	{
		if (PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_gamePaused)
		{
			return;
		}
		float deltaTime = Main.GetDeltaTime();
		this.trailAlphaOffset += deltaTime * this.meteorTrailSpeed;
		this.smokeAlphaOffset += deltaTime * this.meteorSmokeSpeed;
		if (this.trailAlphaOffset > 1f)
		{
			this.trailAlphaOffset -= 1f;
		}
		if (this.smokeAlphaOffset > 1f)
		{
			this.smokeAlphaOffset -= 1f;
		}
		this.trailMat.SetTextureOffset("_SideAlpha", new Vector2(0f, this.trailAlphaOffset));
		this.smokeMat.SetTextureOffset("_SideAlpha", new Vector2(0f, this.smokeAlphaOffset));
		if (base.transform.position.y < 100f + this.meteorExplosionYOffset && !this.explosionToggled)
		{
			this.explosionToggled = true;
			this.meteorExplosion.transform.position = base.transform.position;
			this.meteorExplosion.Play();
		}
	}

	// Token: 0x0400107B RID: 4219
	public GameObject meteorTrailObject;

	// Token: 0x0400107C RID: 4220
	public GameObject meteorSmokeObject;

	// Token: 0x0400107D RID: 4221
	public float meteorTrailSpeed = 1f;

	// Token: 0x0400107E RID: 4222
	public float meteorSmokeSpeed = 1f;

	// Token: 0x0400107F RID: 4223
	public float meteorExplosionYOffset = -2176f;

	// Token: 0x04001080 RID: 4224
	private float trailAlphaOffset;

	// Token: 0x04001081 RID: 4225
	private float smokeAlphaOffset;

	// Token: 0x04001082 RID: 4226
	private Material trailMat;

	// Token: 0x04001083 RID: 4227
	private Material smokeMat;

	// Token: 0x04001084 RID: 4228
	public ParticleSystem meteorExplosion;

	// Token: 0x04001085 RID: 4229
	public bool explosionToggled;
}
