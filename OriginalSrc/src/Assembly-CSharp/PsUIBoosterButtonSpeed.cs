using System;

// Token: 0x02000219 RID: 537
public class PsUIBoosterButtonSpeed : PsUIBoosterButton
{
	// Token: 0x06000F90 RID: 3984 RVA: 0x0009227C File Offset: 0x0009067C
	public PsUIBoosterButtonSpeed(UIComponent _parent, float _width = 0.345f)
		: base(_parent, false)
	{
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x00092286 File Offset: 0x00090686
	public override void CreateBooster(Vehicle _vehicle, bool _freeBooster)
	{
		_vehicle.m_booster = new SpeedBooster(_freeBooster);
	}
}
