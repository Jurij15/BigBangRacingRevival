using System;

// Token: 0x020000B9 RID: 185
public class LavaGround : Ground
{
	// Token: 0x060003CA RID: 970 RVA: 0x00036E00 File Offset: 0x00035200
	public LavaGround(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_groundItemIdentifier = "MaterialLava";
		this.m_editorWheelFrameName = "hud_button_material_lava";
		this.m_elasticity = 0f;
		this.m_friction = 2f;
		this.m_segmentRadius = 8f;
		this.m_beltMaterialResourceName = "LavaBeltMat_Material";
		this.m_frontMaterialResourceName = "LavaFrontMat_Material";
		this.m_isBurning = true;
		this.m_drawSound = "/UI/DrawLoop_Danger";
		this.m_drawFx = "DrawingDanger_GameObject";
	}
}
