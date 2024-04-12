using System;

// Token: 0x020000B8 RID: 184
public class IceGround : Ground
{
	// Token: 0x060003C9 RID: 969 RVA: 0x00036D70 File Offset: 0x00035170
	public IceGround(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_groundItemIdentifier = "MaterialIce";
		this.m_editorWheelFrameName = "hud_button_material_ice";
		this.m_elasticity = 0.8f;
		this.m_friction = 0.3f;
		this.m_beltMaterialResourceName = "IceBeltMat_Material";
		this.m_frontMaterialResourceName = "IceFrontMat_Material";
		this.m_tireRollSound = "/InGame/Vehicles/TireRollLoop_Ice";
		this.m_drawSound = "/UI/DrawLoop_Ice";
		this.m_driveFX = "DrivingIce_GameObject";
		this.m_drawFx = "DrawingIce_GameObject";
		this.m_destructible = true;
		this.m_isFrozen = true;
	}
}
