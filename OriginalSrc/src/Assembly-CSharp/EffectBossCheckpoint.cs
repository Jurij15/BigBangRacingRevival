using System;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class EffectBossCheckpoint : MonoBehaviour
{
	// Token: 0x06000DB5 RID: 3509 RVA: 0x00080E2A File Offset: 0x0007F22A
	private void Awake()
	{
		this.eyeMat = this.eyeRenderer.materials[1];
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00080E4C File Offset: 0x0007F24C
	public void StopScrolling(BossCheckpointSymbol symbol)
	{
		this.enableScroll = false;
		this.eyeMat.SetTexture("_MainTex", this.symbolTexture);
		switch (symbol)
		{
		case BossCheckpointSymbol.closed:
			this.eyeMat.SetTextureOffset("_MainTex", new Vector2(0f, 0f));
			break;
		case BossCheckpointSymbol.freeze:
			this.eyeMat.SetTextureOffset("_MainTex", new Vector2(0f, 0.5f));
			break;
		case BossCheckpointSymbol.boost:
			this.eyeMat.SetTextureOffset("_MainTex", new Vector2(0.5f, 0f));
			break;
		case BossCheckpointSymbol.gold:
			this.eyeMat.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.5f));
			break;
		default:
			this.eyeMat.SetTextureOffset("_MainTex", new Vector2(0f, 0f));
			break;
		}
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00080F45 File Offset: 0x0007F345
	public void StartScrolling()
	{
		this.enableScroll = true;
		this.eyeMat.SetTexture("_MainTex", this.scrollTexture);
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00080F64 File Offset: 0x0007F364
	public void Wobble()
	{
		this.animator.SetTrigger("Wobble");
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00080F78 File Offset: 0x0007F378
	private void Update()
	{
		if (!PsState.m_activeMinigame.m_gamePaused && this.enableScroll)
		{
			this.uvScrollPos -= Main.GetDeltaTime() * -7.5f;
			if (this.uvScrollPos > 0f)
			{
				this.uvScrollPos -= 1f;
			}
			this.eyeMat.SetTextureOffset("_MainTex", new Vector2(0f, this.uvScrollPos));
		}
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x00080FF9 File Offset: 0x0007F3F9
	private void OnDestroy()
	{
		Object.Destroy(this.eyeMat);
	}

	// Token: 0x04001060 RID: 4192
	public Renderer eyeRenderer;

	// Token: 0x04001061 RID: 4193
	public Texture symbolTexture;

	// Token: 0x04001062 RID: 4194
	public Texture scrollTexture;

	// Token: 0x04001063 RID: 4195
	private bool enableScroll;

	// Token: 0x04001064 RID: 4196
	private float uvScrollPos;

	// Token: 0x04001065 RID: 4197
	private Material eyeMat;

	// Token: 0x04001066 RID: 4198
	private Animator animator;
}
