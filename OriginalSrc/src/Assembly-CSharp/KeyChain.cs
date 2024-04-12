using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public static class KeyChain
{
	// Token: 0x06000316 RID: 790 RVA: 0x0002FD7D File Offset: 0x0002E17D
	public static void SaveString(string name, string value)
	{
		if (Application.platform == 8 && !IOSKeyChain.SetData("WhatOnEarth", name, value))
		{
			Debug.LogError("Saving string to keychain failed!");
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0002FDA8 File Offset: 0x0002E1A8
	public static string GetString(string key)
	{
		string text = string.Empty;
		if (Application.platform == 8)
		{
			text = IOSKeyChain.GetData("WhatOnEarth", key);
		}
		return text;
	}

	// Token: 0x040003F1 RID: 1009
	private const string SERVICE_NAME = "WhatOnEarth";
}
