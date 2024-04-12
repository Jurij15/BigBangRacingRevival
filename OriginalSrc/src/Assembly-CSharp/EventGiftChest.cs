using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000F7 RID: 247
public class EventGiftChest : EventGiftComponent
{
	// Token: 0x06000575 RID: 1397 RVA: 0x00046EF4 File Offset: 0x000452F4
	public EventGiftChest(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("identifier"))
		{
			try
			{
				this.m_gachaType = (GachaType)Enum.Parse(typeof(GachaType), (string)_dict["identifier"]);
			}
			catch
			{
				Debug.LogError("parsing chest failed, string is: " + (string)_dict["identifier"]);
			}
		}
		else
		{
			Debug.LogError("Did not contain identifier");
		}
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00046F8C File Offset: 0x0004538C
	public override void Claim(Hashtable _data)
	{
		PsGachaManager.m_lastOpenedGacha = this.m_gachaType;
		PsGachaManager.m_lastGachaRewards = PsGachaManager.OpenGacha(new PsGacha(this.m_gachaType), -1, false);
		FrbMetrics.ChestOpened("event_gift");
		Hashtable updatedData = PsCustomisationManager.GetUpdatedData(PsUpgradeManager.GetUpdatedData(null));
		if (updatedData != null)
		{
			_data.Add("customisation", updatedData);
		}
		List<Dictionary<string, object>> updatedAchievements = PsAchievementManager.GetUpdatedAchievements(true);
		if (updatedAchievements != null)
		{
			_data.Add("achievements", updatedAchievements);
		}
		Dictionary<string, int> updatedEditorResources = PsMetagameManager.m_playerStats.GetUpdatedEditorResources();
		if (updatedEditorResources != null)
		{
			_data.Add("editorResources", updatedEditorResources);
		}
		base.Claim(_data);
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00047020 File Offset: 0x00045420
	public override void EndAction(Action _callback)
	{
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterOpenGacha), null, null, null, true, true, InitialPage.Center, false, false, false);
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			_callback.Invoke();
		});
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00047074 File Offset: 0x00045474
	public override string GetName()
	{
		return PsGachaManager.GetGachaNameWithChest(this.m_gachaType);
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00047084 File Offset: 0x00045484
	public override void CreateUI(UIComponent _parent)
	{
		UIComponent uicomponent = new UIComponent(_parent, false, string.Empty, null, null, string.Empty);
		uicomponent.SetWidth(1f, RelativeTo.ParentWidth);
		uicomponent.SetHeight(1f, RelativeTo.ParentHeight);
		float num = -0.25f;
		uicomponent.SetMargins(num, num, num, num, RelativeTo.ParentWidth);
		uicomponent.RemoveDrawHandler();
		PsUICenterShopAll.CreateChest(uicomponent, this.m_gachaType);
	}

	// Token: 0x04000701 RID: 1793
	public GachaType m_gachaType;
}
