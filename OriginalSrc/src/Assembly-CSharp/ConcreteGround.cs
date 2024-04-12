using System;

// Token: 0x020000B6 RID: 182
public class ConcreteGround : Ground
{
	// Token: 0x060003C7 RID: 967 RVA: 0x00036C68 File Offset: 0x00035068
	public ConcreteGround(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_groundItemIdentifier = "MaterialConcrete";
		this.m_editorWheelFrameName = "hud_button_material_concrete";
		this.m_elasticity = 0.5f;
		this.m_friction = 0.7f;
		this.m_beltMaterialResourceName = "ConcreteBeltMat_Material";
		this.m_frontMaterialResourceName = "ConcreteFrontMat_Material";
		this.m_tireRollSound = "/InGame/Vehicles/TireRollLoop_Sand";
		this.m_drawSound = "/UI/DrawLoop_Sand";
		this.m_driveFX = "DrivingSand_GameObject";
		this.m_drawFx = "DrawingSand_GameObject";
		this.m_destructible = false;
	}
}
