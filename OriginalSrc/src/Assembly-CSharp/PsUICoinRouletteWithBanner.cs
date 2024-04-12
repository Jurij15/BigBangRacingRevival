using System;
using Prime31;
using UnityEngine;

// Token: 0x020003D7 RID: 983
public class PsUICoinRouletteWithBanner : UIVerticalList
{
	// Token: 0x06001BCD RID: 7117 RVA: 0x0013583C File Offset: 0x00133C3C
	public PsUICoinRouletteWithBanner(UIComponent _parent)
		: base(_parent, string.Empty)
	{
		this.RemoveDrawHandler();
		IAPProduct iapproductById = PsIAPManager.GetIAPProductById("coin_booster");
		if (iapproductById != null)
		{
			this.SetVerticalAlign(1f);
			this.SetMargins(0f, 0f, 0.03f, 0f, RelativeTo.ScreenHeight);
			this.SetSpacing(0.03f, RelativeTo.ScreenHeight);
			UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
			uiverticalList.SetWidth(0.60719997f, RelativeTo.ScreenHeight);
			uiverticalList.RemoveDrawHandler();
			this.coinBoosterBanner = new PsUINonconsumableBanner(uiverticalList, "coin_booster", iapproductById.price);
			TweenS.AddTransformTween(this.coinBoosterBanner.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		PsUICenterRouletteCoin psUICenterRouletteCoin = new PsUICenterRouletteCoin(this);
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x0013590D File Offset: 0x00133D0D
	public override void Step()
	{
		if (this.coinBoosterBanner != null && this.coinBoosterBanner.IsHit())
		{
			PsPurchaseHelper.PurchaseIAP("coin_booster", null, delegate
			{
				this.coinBoosterBanner.Destroy();
				this.coinBoosterBanner = null;
			});
		}
		base.Step();
	}

	// Token: 0x04001E17 RID: 7703
	private PsUINonconsumableBanner coinBoosterBanner;
}
