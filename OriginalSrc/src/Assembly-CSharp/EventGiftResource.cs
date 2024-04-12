using System;
using System.Collections.Generic;

// Token: 0x020000FA RID: 250
public abstract class EventGiftResource : EventGiftComponent
{
	// Token: 0x06000585 RID: 1413 RVA: 0x00047671 File Offset: 0x00045A71
	protected void ParseAmount(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("amount"))
		{
			this.m_amount = Convert.ToInt32(_dict["amount"]);
		}
		else
		{
			Debug.LogError("Did not contain amount");
		}
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x000476A8 File Offset: 0x00045AA8
	public static EventGiftResource GetResourceObject(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("identifier"))
		{
			string text = (string)_dict["identifier"];
			if (text == "coins")
			{
				return new EventGiftResourceCoin(_dict);
			}
			if (text == "gems")
			{
				return new EventGiftResourceGem(_dict);
			}
			if (text == "nitros")
			{
				return new EventGiftResourceNitro(_dict);
			}
			Debug.LogError("Did not find: " + text);
		}
		else
		{
			Debug.LogError("Did not have identifier");
		}
		return null;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x0004773C File Offset: 0x00045B3C
	public override void CreateUI(UIComponent _parent)
	{
		UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.GetPicture(), null), true, true);
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00047773 File Offset: 0x00045B73
	public virtual string GetPicture()
	{
		return string.Empty;
	}

	// Token: 0x04000709 RID: 1801
	public int m_amount;
}
