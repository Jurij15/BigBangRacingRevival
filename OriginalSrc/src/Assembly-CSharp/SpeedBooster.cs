using System;

// Token: 0x0200002A RID: 42
public class SpeedBooster : Booster
{
	// Token: 0x06000120 RID: 288 RVA: 0x0000CEA0 File Offset: 0x0000B2A0
	public SpeedBooster(bool _free)
		: base(_free)
	{
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000CEA9 File Offset: 0x0000B2A9
	public override void Use(Vehicle _vehicle)
	{
		if (_vehicle is ISpeedBoost && !this.m_used && Booster.IsBoosterAvailable())
		{
			(_vehicle as ISpeedBoost).TrickBoost(2, true);
			base.Use(_vehicle);
		}
	}
}
