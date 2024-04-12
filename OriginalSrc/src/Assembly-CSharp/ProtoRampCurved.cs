using System;

// Token: 0x02000075 RID: 117
public class ProtoRampCurved : Unit
{
	// Token: 0x06000258 RID: 600 RVA: 0x0001E846 File Offset: 0x0001CC46
	public ProtoRampCurved(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0001E850 File Offset: 0x0001CC50
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}
}
