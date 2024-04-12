using System;

// Token: 0x020000B7 RID: 183
public class DangerousGround : Ground
{
	// Token: 0x060003C8 RID: 968 RVA: 0x00036CF4 File Offset: 0x000350F4
	public DangerousGround(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_groundItemIdentifier = "MaterialDanger";
		this.m_editorWheelFrameName = "hud_button_material_danger";
		this.m_elasticity = 0.1f;
		this.m_friction = 0.8f;
		this.m_rectBrush = true;
		this.m_beltMaterialResourceName = "DangerBeltMat_Material";
		this.m_frontMaterialResourceName = "DangerFrontMat_Material";
		this.m_isElectrified = true;
		this.m_drawSound = "/UI/DrawLoop_Danger";
		this.m_drawFx = "DrawingDanger_GameObject";
	}
}
