using System;
using System.Collections.Generic;

// Token: 0x02000291 RID: 657
public class PsUICenterPlayerProfiler : UICanvas
{
	// Token: 0x060013BF RID: 5055 RVA: 0x000C5BDC File Offset: 0x000C3FDC
	public PsUICenterPlayerProfiler(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.m_loaderEntity = EntityManager.AddEntity();
		this.m_userName = PlayerPrefsX.GetUserName();
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.m_vertList = new UIVerticalList(this, string.Empty);
		this.m_vertList.SetWidth(0.85f, RelativeTo.ScreenHeight);
		this.m_vertList.SetAlign(0.5f, 0.5f);
		this.m_vertList.SetMargins(0.03f, 0.03f, 0f, 0.02f, RelativeTo.ScreenHeight);
		this.m_vertList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.UpgradeBarsBackground));
		this.m_vertList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_vertList, string.Empty);
		uihorizontalList.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.m_userName + "'s server profile\nuserid: " + PlayerPrefsX.GetUserId(), "KGSecondChances_Font", 0.025f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uitext.RemoveDrawHandler();
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x000C5D3C File Offset: 0x000C413C
	private void InfoFetchSUCCEED(HttpC _c)
	{
		PsPlayerProfilerData psPlayerProfilerData = ClientTools.ParsePlayerProfilerData(_c);
		UIVerticalList uiverticalList = new UIVerticalList(this.m_vertList, string.Empty);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, "[Vehicle profiles]\n", "KGSecondChances_Font", 0.025f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uitext.RemoveDrawHandler();
		foreach (PsPlayerProfilerData.ProfilerVehicleData profilerVehicleData in psPlayerProfilerData.m_skills)
		{
			UIText uitext2 = new UIText(uiverticalList, false, string.Empty, string.Concat(new object[] { profilerVehicleData.m_vehicleName, " skill: ", profilerVehicleData.m_skill, " / pref: ", profilerVehicleData.m_preference }), "KGSecondChances_Font", 0.025f, RelativeTo.ScreenHeight, null, null);
			uitext2.SetHeight(0.02f, RelativeTo.ScreenHeight);
			uitext2.RemoveDrawHandler();
		}
		if (psPlayerProfilerData.m_subgenrePrefs.Count > 0)
		{
			uitext = new UIText(uiverticalList, false, string.Empty, "\n[Subgenres]\n", "KGSecondChances_Font", 0.025f, RelativeTo.ScreenHeight, null, null);
			uitext.SetHeight(0.03f, RelativeTo.ScreenHeight);
			uitext.RemoveDrawHandler();
			foreach (KeyValuePair<string, float> keyValuePair in psPlayerProfilerData.m_subgenrePrefs)
			{
				UIText uitext3 = new UIText(uiverticalList, false, string.Empty, keyValuePair.Key + ": " + keyValuePair.Value, "KGSecondChances_Font", 0.025f, RelativeTo.ScreenHeight, null, null);
				uitext3.SetHeight(0.02f, RelativeTo.ScreenHeight);
				uitext3.RemoveDrawHandler();
			}
		}
		this.Update();
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x000C5F2C File Offset: 0x000C432C
	private void InfoFetchFAILED(HttpC _c)
	{
		Debug.Log("Info fetch failed!", null);
		Debug.LogError(ServerErrors.GetNetworkError(_c.www.error));
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x000C5F4E File Offset: 0x000C434E
	public override void Destroy()
	{
		if (this.m_loaderEntity != null)
		{
			EntityManager.RemoveEntity(this.m_loaderEntity);
		}
		this.m_loaderEntity = null;
		base.Destroy();
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x000C5F73 File Offset: 0x000C4373
	public override void Step()
	{
	}

	// Token: 0x04001683 RID: 5763
	private const string m_font = "KGSecondChances_Font";

	// Token: 0x04001684 RID: 5764
	private const float m_smallTestSize = 0.03f;

	// Token: 0x04001685 RID: 5765
	public string m_userName = string.Empty;

	// Token: 0x04001686 RID: 5766
	private UIVerticalList m_vertList;

	// Token: 0x04001687 RID: 5767
	public Entity m_loaderEntity;
}
