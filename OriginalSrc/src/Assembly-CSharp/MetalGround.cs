using System;

// Token: 0x020000BA RID: 186
public class MetalGround : Ground
{
	// Token: 0x060003CB RID: 971 RVA: 0x00036E80 File Offset: 0x00035280
	public MetalGround(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_groundItemIdentifier = "MaterialMetal";
		this.m_editorWheelFrameName = "hud_button_material_metal";
		this.m_elasticity = 0.8f;
		this.m_friction = 0.85f;
		this.m_rectBrush = true;
		this.m_marchHard = false;
		this.m_smoothingAngle = 0f;
		this.m_beltMaterialResourceName = "MetalBeltMat_Material";
		this.m_frontMaterialResourceName = "MetalFrontMat_Material";
		this.m_tireRollSound = "/InGame/Vehicles/TireRollLoop_Metal";
		this.m_drawSound = "/UI/DrawLoop_Metal";
	}
}
