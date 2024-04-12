using System;

// Token: 0x020000BC RID: 188
public class SandGround : Ground
{
	// Token: 0x060003CD RID: 973 RVA: 0x00036FBC File Offset: 0x000353BC
	public SandGround(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_groundItemIdentifier = "MaterialSand";
		this.m_editorWheelFrameName = "hud_button_material_dirt";
		this.m_elasticity = 0.5f;
		this.m_friction = 0.7f;
		this.m_beltMaterialResourceName = "SandBeltMat_Material";
		this.m_frontMaterialResourceName = "SandFrontMat_Material";
		this.m_groundPropStorageName = "SandGroundPropStorage_GameObject";
		this.m_tireRollSound = "/InGame/Vehicles/TireRollLoop_Sand";
		this.m_drawSound = "/UI/DrawLoop_Sand";
		this.m_driveFX = "DrivingSand_GameObject";
		this.m_drawFx = "DrawingSand_GameObject";
		this.m_destructible = true;
	}
}
