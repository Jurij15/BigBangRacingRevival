using System;
using UnityEngine;

// Token: 0x02000732 RID: 1842
[AddComponentMenu("GoogleVR/Audio/FmodGvrAudioRoom")]
public class FmodGvrAudioRoom : MonoBehaviour
{
	// Token: 0x0600354A RID: 13642 RVA: 0x001CFCAC File Offset: 0x001CE0AC
	private void OnEnable()
	{
		FmodGvrAudio.UpdateAudioRoom(this, FmodGvrAudio.IsListenerInsideRoom(this));
	}

	// Token: 0x0600354B RID: 13643 RVA: 0x001CFCBA File Offset: 0x001CE0BA
	private void OnDisable()
	{
		FmodGvrAudio.UpdateAudioRoom(this, false);
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x001CFCC3 File Offset: 0x001CE0C3
	private void Update()
	{
		FmodGvrAudio.UpdateAudioRoom(this, FmodGvrAudio.IsListenerInsideRoom(this));
	}

	// Token: 0x0600354D RID: 13645 RVA: 0x001CFCD1 File Offset: 0x001CE0D1
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireCube(Vector3.zero, this.size);
	}

	// Token: 0x04003368 RID: 13160
	public FmodGvrAudioRoom.SurfaceMaterial leftWall = FmodGvrAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x04003369 RID: 13161
	public FmodGvrAudioRoom.SurfaceMaterial rightWall = FmodGvrAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x0400336A RID: 13162
	public FmodGvrAudioRoom.SurfaceMaterial floor = FmodGvrAudioRoom.SurfaceMaterial.ParquetOnConcrete;

	// Token: 0x0400336B RID: 13163
	public FmodGvrAudioRoom.SurfaceMaterial ceiling = FmodGvrAudioRoom.SurfaceMaterial.PlasterRough;

	// Token: 0x0400336C RID: 13164
	public FmodGvrAudioRoom.SurfaceMaterial backWall = FmodGvrAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x0400336D RID: 13165
	public FmodGvrAudioRoom.SurfaceMaterial frontWall = FmodGvrAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x0400336E RID: 13166
	public float reflectivity = 1f;

	// Token: 0x0400336F RID: 13167
	public float reverbGainDb;

	// Token: 0x04003370 RID: 13168
	public float reverbBrightness;

	// Token: 0x04003371 RID: 13169
	public float reverbTime = 1f;

	// Token: 0x04003372 RID: 13170
	public Vector3 size = Vector3.one;

	// Token: 0x02000733 RID: 1843
	public enum SurfaceMaterial
	{
		// Token: 0x04003374 RID: 13172
		Transparent,
		// Token: 0x04003375 RID: 13173
		AcousticCeilingTiles,
		// Token: 0x04003376 RID: 13174
		BrickBare,
		// Token: 0x04003377 RID: 13175
		BrickPainted,
		// Token: 0x04003378 RID: 13176
		ConcreteBlockCoarse,
		// Token: 0x04003379 RID: 13177
		ConcreteBlockPainted,
		// Token: 0x0400337A RID: 13178
		CurtainHeavy,
		// Token: 0x0400337B RID: 13179
		FiberglassInsulation,
		// Token: 0x0400337C RID: 13180
		GlassThin,
		// Token: 0x0400337D RID: 13181
		GlassThick,
		// Token: 0x0400337E RID: 13182
		Grass,
		// Token: 0x0400337F RID: 13183
		LinoleumOnConcrete,
		// Token: 0x04003380 RID: 13184
		Marble,
		// Token: 0x04003381 RID: 13185
		Metal,
		// Token: 0x04003382 RID: 13186
		ParquetOnConcrete,
		// Token: 0x04003383 RID: 13187
		PlasterRough,
		// Token: 0x04003384 RID: 13188
		PlasterSmooth,
		// Token: 0x04003385 RID: 13189
		PlywoodPanel,
		// Token: 0x04003386 RID: 13190
		PolishedConcreteOrTile,
		// Token: 0x04003387 RID: 13191
		Sheetrock,
		// Token: 0x04003388 RID: 13192
		WaterOrIceSurface,
		// Token: 0x04003389 RID: 13193
		WoodCeiling,
		// Token: 0x0400338A RID: 13194
		WoodPanel
	}
}
