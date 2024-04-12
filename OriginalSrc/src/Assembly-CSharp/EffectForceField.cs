using System;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class EffectForceField : MonoBehaviour
{
	// Token: 0x06000DD6 RID: 3542 RVA: 0x00082074 File Offset: 0x00080474
	public void Initialize(ForceFieldColor effectColor, bool startEnabled)
	{
		this.curColor = this.EnumToColor(effectColor);
		this.curFadedColor = new Color32(this.curColor.r, this.curColor.g, this.curColor.b, 40);
		this.forceFieldTintedMat = new Material(this.forceFieldObj.GetComponent<Renderer>().material);
		this.forceFieldTintedMat.SetColor("_ColorTint", this.curColor);
		this.forceFieldObj.GetComponent<Renderer>().sharedMaterial = this.forceFieldTintedMat;
		this.lightButtonTintedMat = new Material(this.lightButtonObj.GetComponent<Renderer>().material);
		this.lightButtonTintedMat.SetColor("_Color", this.curColor);
		this.lightButtonObj.GetComponent<Renderer>().sharedMaterial = this.lightButtonTintedMat;
		this.lightTintedMat = new Material(this.lightObj.GetComponent<Renderer>().material);
		this.lightTintedMat.SetColor("_Color", this.curColor);
		this.lightObj.GetComponent<Renderer>().sharedMaterial = this.lightTintedMat;
		this.forceFieldBeamTintedMat = new Material(this.forceFieldBeamMat);
		this.forceFieldBeamTintedMat.SetColor("_ColorTint", this.curColor);
		this.GenerateForceField();
		this.AdjustHolderRotations();
		this.ToggleForceField(startEnabled);
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x000821E0 File Offset: 0x000805E0
	public void ToggleForceField(bool _on)
	{
		if (!_on)
		{
			this.lightObj.SetActive(false);
			this.lightBackgroundObj.SetActive(true);
			this.forceFieldBeamTintedMat.SetColor("_ColorTint", this.curFadedColor);
			this.forceFieldTintedMat.SetColor("_ColorTint", this.curFadedColor);
		}
		else
		{
			this.lightObj.SetActive(true);
			this.lightBackgroundObj.SetActive(false);
			this.forceFieldBeamTintedMat.SetColor("_ColorTint", this.curColor);
			this.forceFieldTintedMat.SetColor("_ColorTint", this.curColor);
		}
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x00082294 File Offset: 0x00080694
	private void AdjustForceField()
	{
		if (this.leftObjTrans != null && this.rightObjTrans != null)
		{
			float num = Vector3.Distance(this.leftObjTrans.position, this.rightObjTrans.position);
			this.forceFieldBar.transform.localScale = new Vector3(1f, 1f, num * 0.01f);
			this.UpdateLineRendererPositions();
		}
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0008230C File Offset: 0x0008070C
	private void GenerateForceField()
	{
		this.frontLine = new GameObject
		{
			name = "lineObjectFront",
			transform = 
			{
				parent = base.transform,
				localPosition = Vector3.zero
			},
			layer = base.gameObject.layer
		}.AddComponent<LineRenderer>();
		this.frontLine.SetVertexCount(2);
		this.frontLine.SetWidth(20f, 20f);
		this.frontLine.sharedMaterial = this.forceFieldBeamTintedMat;
		this.backLine = new GameObject
		{
			name = "lineObjectBack",
			transform = 
			{
				parent = base.transform,
				localPosition = Vector3.zero
			},
			layer = base.gameObject.layer
		}.AddComponent<LineRenderer>();
		this.backLine.SetVertexCount(2);
		this.backLine.SetWidth(20f, 20f);
		this.backLine.sharedMaterial = this.forceFieldBeamTintedMat;
		this.UpdateLineRendererPositions();
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x00082424 File Offset: 0x00080824
	private void UpdateLineRendererPositions()
	{
		this.frontLine.SetPosition(0, new Vector3(this.leftObjTrans.position.x, this.leftObjTrans.position.y, this.leftObjTrans.position.z - this.beamDistance));
		this.frontLine.SetPosition(1, new Vector3(this.rightObjTrans.position.x, this.rightObjTrans.position.y, this.rightObjTrans.position.z - this.beamDistance));
		this.backLine.SetPosition(0, new Vector3(this.leftObjTrans.position.x, this.leftObjTrans.position.y, this.leftObjTrans.position.z - 10f));
		this.backLine.SetPosition(1, new Vector3(this.rightObjTrans.position.x, this.rightObjTrans.position.y, this.rightObjTrans.position.z - 10f));
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0008257C File Offset: 0x0008097C
	private void AdjustHolderRotations()
	{
		Vector3 vector = Vector3.Cross(-Vector3.forward, this.leftObjTrans.position - this.rightObjTrans.position);
		this.leftObjTrans.LookAt(this.rightObjTrans, vector);
		this.rightObjTrans.LookAt(this.leftObjTrans, vector);
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x000825D8 File Offset: 0x000809D8
	private void FlashEdges()
	{
		if (this.flashTimer <= 0f)
		{
			this.flashTimer = Random.Range(0.01f, 0.02f);
			float num = Random.Range(16f, 20f);
			float num2 = Random.Range(16f, 20f);
			this.frontLine.SetWidth(num, num2);
			this.backLine.SetWidth(num2, num);
		}
		else
		{
			this.flashTimer -= Time.deltaTime;
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x0008265C File Offset: 0x00080A5C
	private Color32 EnumToColor(ForceFieldColor color)
	{
		Color32 color2;
		switch (color)
		{
		case ForceFieldColor.Purple:
			color2 = this.purple;
			break;
		case ForceFieldColor.Blue:
			color2 = this.blue;
			break;
		case ForceFieldColor.Green:
			color2 = this.green;
			break;
		case ForceFieldColor.Yellow:
			color2 = this.yellow;
			break;
		case ForceFieldColor.Orange:
			color2 = this.orange;
			break;
		default:
			color2 = this.blue;
			break;
		}
		return color2;
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x000826D4 File Offset: 0x00080AD4
	public void Update()
	{
		if (this.leftObjTrans.hasChanged || this.rightObjTrans.hasChanged)
		{
			this.AdjustForceField();
			this.AdjustHolderRotations();
			Transform transform = this.leftObjTrans;
			bool flag = false;
			this.rightObjTrans.hasChanged = flag;
			transform.hasChanged = flag;
		}
		this.FlashEdges();
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0008272D File Offset: 0x00080B2D
	private void OnDestroy()
	{
		Object.Destroy(this.forceFieldBeamTintedMat);
		Object.Destroy(this.forceFieldTintedMat);
		Object.Destroy(this.lightButtonTintedMat);
		Object.Destroy(this.lightTintedMat);
	}

	// Token: 0x040010B9 RID: 4281
	public Transform leftObjTrans;

	// Token: 0x040010BA RID: 4282
	public Transform rightObjTrans;

	// Token: 0x040010BB RID: 4283
	public GameObject forceFieldObj;

	// Token: 0x040010BC RID: 4284
	public GameObject forceFieldBar;

	// Token: 0x040010BD RID: 4285
	public GameObject lightObj;

	// Token: 0x040010BE RID: 4286
	public GameObject lightBackgroundObj;

	// Token: 0x040010BF RID: 4287
	public GameObject lightButtonObj;

	// Token: 0x040010C0 RID: 4288
	public Material forceFieldBeamMat;

	// Token: 0x040010C1 RID: 4289
	private Material forceFieldTintedMat;

	// Token: 0x040010C2 RID: 4290
	private Material forceFieldBeamTintedMat;

	// Token: 0x040010C3 RID: 4291
	private Material lightButtonTintedMat;

	// Token: 0x040010C4 RID: 4292
	private Material lightTintedMat;

	// Token: 0x040010C5 RID: 4293
	public Color32 purple;

	// Token: 0x040010C6 RID: 4294
	public Color32 blue;

	// Token: 0x040010C7 RID: 4295
	public Color32 green;

	// Token: 0x040010C8 RID: 4296
	public Color32 yellow;

	// Token: 0x040010C9 RID: 4297
	public Color32 orange;

	// Token: 0x040010CA RID: 4298
	private Color32 curColor;

	// Token: 0x040010CB RID: 4299
	private Color32 curFadedColor;

	// Token: 0x040010CC RID: 4300
	private float beamDistance = 200f;

	// Token: 0x040010CD RID: 4301
	private float sideLineOffset = 2.5f;

	// Token: 0x040010CE RID: 4302
	private LineRenderer frontLine;

	// Token: 0x040010CF RID: 4303
	private LineRenderer backLine;

	// Token: 0x040010D0 RID: 4304
	private float flashTimer;
}
