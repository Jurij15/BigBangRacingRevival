using System;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class PsUILoadingAnimation : UIComponent
{
	// Token: 0x060011D0 RID: 4560 RVA: 0x000AE33C File Offset: 0x000AC73C
	public PsUILoadingAnimation(UIComponent _parent, bool _rogue)
		: base(_parent, false, string.Empty, null, null, string.Empty)
	{
		this.SetSize(0.2f, 0.2f, RelativeTo.ScreenHeight);
		this.SetAlign(0.5f, 0.5f);
		if (_rogue)
		{
			base.SetRogue();
		}
		this.RemoveDrawHandler();
		this.m_earth = new UIFittedSprite(this, false, "LoadingBar", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_loading_earth", null), true, true);
		this.m_earth.SetWidth(0.5f, RelativeTo.ParentWidth);
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x000AE3D0 File Offset: 0x000AC7D0
	public override void Step()
	{
		if (this.m_circleShape == null)
		{
			this.m_circleShape = DebugDraw.GetCircle(this.m_actualWidth * 0.2f, 36, Vector2.zero);
			this.m_signalTC1 = TransformS.AddComponent(this.m_TC.p_entity, this.m_TC.transform.position);
			this.m_signalTC2 = TransformS.AddComponent(this.m_TC.p_entity, this.m_TC.transform.position);
			this.m_signalTC3 = TransformS.AddComponent(this.m_TC.p_entity, this.m_TC.transform.position);
			TransformS.ParentComponent(this.m_signalTC1, this.m_TC);
			TransformS.ParentComponent(this.m_signalTC2, this.m_TC);
			TransformS.ParentComponent(this.m_signalTC3, this.m_TC);
			TransformS.SetPosition(this.m_signalTC1, Vector3.zero);
			TransformS.SetPosition(this.m_signalTC2, Vector3.zero);
			TransformS.SetPosition(this.m_signalTC3, Vector3.zero);
			this.m_signalPC1 = PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_signalTC1, new Vector3(0f, 0f, -10f), this.m_circleShape, this.m_actualWidth * 0.025f, Color.white, ResourceManager.GetMaterial(RESOURCE.LoadingBallEmitMat_Material), this.m_camera, Position.Center, true);
			this.m_signalPC2 = PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_signalTC2, new Vector3(0f, 0f, -10f), this.m_circleShape, this.m_actualWidth * 0.025f, Color.white, ResourceManager.GetMaterial(RESOURCE.LoadingBallEmitMat_Material), this.m_camera, Position.Center, true);
			this.m_signalPC3 = PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_signalTC3, new Vector3(0f, 0f, -10f), this.m_circleShape, this.m_actualWidth * 0.025f, Color.white, ResourceManager.GetMaterial(RESOURCE.LoadingBallEmitMat_Material), this.m_camera, Position.Center, true);
		}
		float num = (float)(Main.m_gameTicks % 120) / 120f;
		float num2 = (float)((Main.m_gameTicks + 40) % 120) / 120f;
		float num3 = (float)((Main.m_gameTicks + 80) % 120) / 120f;
		TransformS.SetScale(this.m_signalTC1, num * 0.75f + 1f);
		TransformS.SetScale(this.m_signalTC2, num2 * 0.75f + 1f);
		TransformS.SetScale(this.m_signalTC3, num3 * 0.75f + 1f);
		PrefabS.SetShaderColor(this.m_signalPC1, new Color(1f, 1f, 1f, 0.75f - num * 0.75f));
		PrefabS.SetShaderColor(this.m_signalPC2, new Color(1f, 1f, 1f, 0.75f - num2 * 0.75f));
		PrefabS.SetShaderColor(this.m_signalPC3, new Color(1f, 1f, 1f, 0.75f - num3 * 0.75f));
		base.Step();
	}

	// Token: 0x040014C7 RID: 5319
	protected Vector2[] m_circleShape;

	// Token: 0x040014C8 RID: 5320
	protected TransformC m_signalTC1;

	// Token: 0x040014C9 RID: 5321
	protected TransformC m_signalTC2;

	// Token: 0x040014CA RID: 5322
	protected TransformC m_signalTC3;

	// Token: 0x040014CB RID: 5323
	protected PrefabC m_signalPC1;

	// Token: 0x040014CC RID: 5324
	protected PrefabC m_signalPC2;

	// Token: 0x040014CD RID: 5325
	protected PrefabC m_signalPC3;

	// Token: 0x040014CE RID: 5326
	public UIFittedSprite m_earth;
}
