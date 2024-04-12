using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class EventGiftTimedUpgradeItemCardDiscount : EventGiftTimed
{
	// Token: 0x060005A8 RID: 1448 RVA: 0x00047D4C File Offset: 0x0004614C
	public EventGiftTimedUpgradeItemCardDiscount(Dictionary<string, object> _dict)
	{
		this.m_timedType = EventGiftTimedType.upgradeItem90discount;
		if (_dict.ContainsKey("amount"))
		{
			int num = Convert.ToInt32(_dict["amount"]);
			num = Mathf.Min(100, Mathf.Max(0, num));
			if (num != 1)
			{
				this.m_discount = (float)num / 100f;
			}
		}
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00047DB8 File Offset: 0x000461B8
	public override void EndAction(Action _callback)
	{
		PsMainMenuState.ChangeToShopState(delegate
		{
			PsMainMenuState.ExitToMainMenuState();
			_callback.Invoke();
		});
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00047DE4 File Offset: 0x000461E4
	public float GetPriceMultiplier()
	{
		return 1f - this.m_discount;
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00047E00 File Offset: 0x00046200
	public string GetDiscountTag()
	{
		return "-" + this.m_discount * 100f + "%";
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x00047E2F File Offset: 0x0004622F
	public override string GetName()
	{
		return PsStrings.Get(StringID.GIFT_TYPE_DISCOUNT_DAY);
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00047E3C File Offset: 0x0004623C
	public override void CreateUI(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, this.GetDiscountTag(), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FEA700", "#cc350c");
	}

	// Token: 0x04000711 RID: 1809
	private float m_discount = 0.9f;
}
