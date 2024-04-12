using System;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class PowerPlateVisuals : MonoBehaviour
{
	// Token: 0x06000DF2 RID: 3570 RVA: 0x00082BA4 File Offset: 0x00080FA4
	private void Awake()
	{
		this.m_localMaterial = new Material(this.m_material);
		Renderer component = base.GetComponent<Renderer>();
		component.material = this.m_localMaterial;
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x00082BD5 File Offset: 0x00080FD5
	private void ChangeColor(Color _color)
	{
		this.m_localMaterial.SetColor("_Color", _color);
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x00082BE8 File Offset: 0x00080FE8
	public void TurnOn(bool _animate, Color _color)
	{
		this.ChangeColor(_color);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x00082BF1 File Offset: 0x00080FF1
	public void TurnOff(bool _animate, Color _color)
	{
		this.ChangeColor(_color);
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x00082BFA File Offset: 0x00080FFA
	public void Destroy()
	{
		Object.Destroy(this.m_localMaterial);
	}

	// Token: 0x040010E3 RID: 4323
	public Material m_material;

	// Token: 0x040010E4 RID: 4324
	private Material m_localMaterial;
}
