using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class PowerLineGraphVisual : GraphConnectionVisual
{
	// Token: 0x060000F1 RID: 241 RVA: 0x00008986 File Offset: 0x00006D86
	public PowerLineGraphVisual(TransformC _parent, Vector3 _start, Vector3 _end)
		: base(_parent, _start, _end)
	{
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00008994 File Offset: 0x00006D94
	public override void CreateConnectionVisual(Vector3 _start, Vector3 _end)
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.PowerLinePrefab_GameObject);
		this.m_prefab = PrefabS.AddComponent(this.m_TC, Vector3.zero, gameObject);
		this.m_visualscript = this.m_prefab.p_gameObject.GetComponent<VisualsPowerLine>();
		this.m_visualscript.ChangeColor(Color.grey);
		this.m_startTransform = this.m_prefab.p_gameObject.transform.Find("Start");
		this.m_startTransform.localPosition = _start - this.m_TC.transform.position + Vector3.forward * 70f;
		this.m_endTransform = this.m_prefab.p_gameObject.transform.Find("End");
		this.m_endTransform.position = _end + Vector3.forward * 70f;
		this.m_visualscript.UpdateLine();
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00008A89 File Offset: 0x00006E89
	public override void SetEndPosition(Vector3 _endPos)
	{
		this.m_endTransform.position = _endPos + Vector3.forward * 70f;
		this.m_visualscript.UpdateLine();
	}

	// Token: 0x040000AF RID: 175
	private PrefabC m_prefab;

	// Token: 0x040000B0 RID: 176
	private VisualsPowerLine m_visualscript;

	// Token: 0x040000B1 RID: 177
	private Transform m_startTransform;

	// Token: 0x040000B2 RID: 178
	private Transform m_endTransform;
}
