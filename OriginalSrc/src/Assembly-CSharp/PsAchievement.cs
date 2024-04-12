using System;
using System.Collections.Generic;

// Token: 0x020000ED RID: 237
public class PsAchievement : Achievement
{
	// Token: 0x06000539 RID: 1337 RVA: 0x00045552 File Offset: 0x00043952
	public PsAchievement(string _identifier, string _name = null, string _description = null, int _target = 1, int _baseValue = 0)
		: base(_identifier, _name, _description, _target, _baseValue)
	{
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00045561 File Offset: 0x00043961
	public override void Complete()
	{
		base.Complete();
		PsMetagameManager.m_playerStats.SetDirty(true);
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00045574 File Offset: 0x00043974
	private void ShowNotification()
	{
		List<PsCustomisationItem> customizationItemsByAchievement = PsCustomisationManager.GetCharacterCustomisationData().GetCustomizationItemsByAchievement(this.m_identifier);
		for (int i = 0; i < customizationItemsByAchievement.Count; i++)
		{
			bool flag = true;
			for (int j = 0; j < customizationItemsByAchievement[i].m_requiredAchievements.Count; j++)
			{
				if (!PsAchievementManager.IsCompleted(customizationItemsByAchievement[i].m_requiredAchievements[j]))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				new PsAchievementNotification("Achievement unlocked: " + this.m_name, this.m_description, customizationItemsByAchievement[i].m_iconName);
			}
		}
	}
}
