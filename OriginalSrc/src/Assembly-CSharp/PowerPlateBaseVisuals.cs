using System;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class PowerPlateBaseVisuals : MonoBehaviour
{
	// Token: 0x06000DEC RID: 3564 RVA: 0x00082B34 File Offset: 0x00080F34
	private void Awake()
	{
		this.m_localMaterial = new Material(this.m_material);
		Renderer component = this.m_lightObject.GetComponent<Renderer>();
		component.material = this.m_localMaterial;
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x00082B6A File Offset: 0x00080F6A
	private void ChangeColor(Color _color)
	{
		this.m_localMaterial.SetColor("_Color", _color);
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00082B7D File Offset: 0x00080F7D
	public void TurnOn(bool _animate, Color _color)
	{
		this.ChangeColor(_color);
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00082B86 File Offset: 0x00080F86
	public void TurnOff(bool _animate, Color _color)
	{
		this.ChangeColor(_color);
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00082B8F File Offset: 0x00080F8F
	public void Destroy()
	{
		Object.Destroy(this.m_localMaterial);
	}

	// Token: 0x040010E0 RID: 4320
	public GameObject m_lightObject;

	// Token: 0x040010E1 RID: 4321
	public Material m_material;

	// Token: 0x040010E2 RID: 4322
	private Material m_localMaterial;
}
