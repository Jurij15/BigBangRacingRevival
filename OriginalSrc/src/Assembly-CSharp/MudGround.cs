using System;

// Token: 0x020000BB RID: 187
public class MudGround : Ground
{
	// Token: 0x060003CC RID: 972 RVA: 0x00036F08 File Offset: 0x00035308
	public MudGround(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_groundItemIdentifier = "MaterialMud";
		this.m_editorWheelFrameName = "hud_button_material_mud";
		this.m_elasticity = 0.1f;
		this.m_friction = 0.4f;
		this.m_segmentRadius = 8f;
		this.m_beltMaterialResourceName = "MudBeltMat_Material";
		this.m_frontMaterialResourceName = "MudFrontMat_Material";
		this.m_destructible = true;
		this.m_skiddable = true;
		this.m_depth = 150f;
		this.m_zOffset = 10f;
		this.m_tireRollSound = "/InGame/Vehicles/TireRollLoop_Mud";
		this.m_drawSound = "/UI/DrawLoop_Mud";
		this.m_driveFX = "MudSplatter_GameObject";
		this.m_drawFx = "DrawingMud_GameObject";
	}
}
