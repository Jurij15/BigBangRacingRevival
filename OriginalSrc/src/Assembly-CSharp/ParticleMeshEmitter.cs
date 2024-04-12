using System;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class ParticleMeshEmitter : MonoBehaviour
{
	// Token: 0x06000CF1 RID: 3313 RVA: 0x0007CCAC File Offset: 0x0007B0AC
	public void Initialize(GameObject effectTargetObj)
	{
		this.emitterObject = effectTargetObj;
		this.emitterMesh = effectTargetObj.GetComponent<MeshFilter>().mesh;
		this.emitterSystem = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0007CCD2 File Offset: 0x0007B0D2
	public void Play()
	{
		this.emit = true;
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0007CCDB File Offset: 0x0007B0DB
	public void Stop()
	{
		this.emit = false;
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0007CCE4 File Offset: 0x0007B0E4
	public void ParticleUpdate(bool updateMesh)
	{
		if (this.emit)
		{
			if (this.emissionTimer >= 1f / this.emitterSystem.emissionRate)
			{
				if (updateMesh)
				{
					this.UpdateEmitterMesh();
				}
				this.EmitParticle();
				this.emissionTimer = Main.m_gameDeltaTime;
			}
			else
			{
				this.emissionTimer += Main.m_gameDeltaTime;
			}
		}
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0007CD4C File Offset: 0x0007B14C
	private void UpdateEmitterMesh()
	{
		this.vertices = this.emitterMesh.vertices;
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0007CD60 File Offset: 0x0007B160
	private void EmitParticle()
	{
		Vector3 vector = this.emitterObject.transform.TransformPoint(this.vertices[Random.Range(0, this.vertices.Length)]);
		base.GetComponent<ParticleSystem>().Emit(vector, new Vector3(0f, 0f, 0f), this.emitterSystem.startSize, this.emitterSystem.startLifetime, this.emitterSystem.startColor);
	}

	// Token: 0x04000E39 RID: 3641
	public Mesh emitterMesh;

	// Token: 0x04000E3A RID: 3642
	private GameObject emitterObject;

	// Token: 0x04000E3B RID: 3643
	private ParticleSystem emitterSystem;

	// Token: 0x04000E3C RID: 3644
	private Vector3[] vertices;

	// Token: 0x04000E3D RID: 3645
	private bool emit;

	// Token: 0x04000E3E RID: 3646
	private float emissionTimer;
}
