using System;
using System.Collections.Generic;

// Token: 0x02000411 RID: 1041
public class PsPlayerProfilerData
{
	// Token: 0x04001FD1 RID: 8145
	public int m_itemLevel;

	// Token: 0x04001FD2 RID: 8146
	public List<PsPlayerProfilerData.ProfilerVehicleData> m_skills = new List<PsPlayerProfilerData.ProfilerVehicleData>();

	// Token: 0x04001FD3 RID: 8147
	public Dictionary<string, float> m_subgenrePrefs = new Dictionary<string, float>();

	// Token: 0x02000412 RID: 1042
	public class ProfilerVehicleData
	{
		// Token: 0x06001C91 RID: 7313 RVA: 0x0014189D File Offset: 0x0013FC9D
		public ProfilerVehicleData(string _vehicleName, int _skill, int _preference)
		{
			this.m_vehicleName = _vehicleName;
			this.m_skill = _skill;
			this.m_preference = _preference;
		}

		// Token: 0x04001FD4 RID: 8148
		public string m_vehicleName;

		// Token: 0x04001FD5 RID: 8149
		public int m_skill;

		// Token: 0x04001FD6 RID: 8150
		public int m_preference;
	}
}
