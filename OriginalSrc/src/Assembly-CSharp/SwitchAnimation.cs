using System;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class SwitchAnimation : MonoBehaviour
{
	// Token: 0x06000DF8 RID: 3576 RVA: 0x00082C28 File Offset: 0x00081028
	private void Awake()
	{
		this.m_outsideRingMaterial = new Material(this.m_lightmaterial);
		Renderer renderer = this.m_outsideRingObject.GetComponent<Renderer>();
		renderer.material = this.m_outsideRingMaterial;
		this.m_handleMaterial = new Material(this.m_lightmaterial);
		renderer = this.m_handleObject.GetComponent<Renderer>();
		renderer.material = this.m_handleMaterial;
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x00082C87 File Offset: 0x00081087
	public void SetHandleColor(Color _color)
	{
		this.m_handleMaterial.SetColor("_Color", _color);
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x00082C9A File Offset: 0x0008109A
	private void ChangeColor(Color _color)
	{
		this.m_outsideRingMaterial.SetColor("_Color", _color);
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x00082CAD File Offset: 0x000810AD
	public void TurnOn(bool _animate, Color _color)
	{
		if (_animate)
		{
			this.m_animator.Play("SwitchOn");
		}
		else
		{
			this.m_animator.Play("SwitchOnIdle");
		}
		this.ChangeColor(_color);
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x00082CE1 File Offset: 0x000810E1
	public void TurnOff(bool _animate, Color _color)
	{
		if (_animate)
		{
			this.m_animator.Play("SwitchOff");
		}
		else
		{
			this.m_animator.Play("SwitchOffIdle");
		}
		this.ChangeColor(_color);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x00082D15 File Offset: 0x00081115
	public void Destroy()
	{
		Object.Destroy(this.m_outsideRingMaterial);
		Object.Destroy(this.m_handleMaterial);
	}

	// Token: 0x040010E5 RID: 4325
	public Material m_lightmaterial;

	// Token: 0x040010E6 RID: 4326
	public Animator m_animator;

	// Token: 0x040010E7 RID: 4327
	public GameObject m_handleObject;

	// Token: 0x040010E8 RID: 4328
	public GameObject m_outsideRingObject;

	// Token: 0x040010E9 RID: 4329
	private string m_onColor = "f793ff";

	// Token: 0x040010EA RID: 4330
	private string m_offColor = "9b5da0";

	// Token: 0x040010EB RID: 4331
	private Material m_outsideRingMaterial;

	// Token: 0x040010EC RID: 4332
	private Material m_handleMaterial;
}
