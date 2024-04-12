using System;

// Token: 0x02000440 RID: 1088
public static class ServerErrors
{
	// Token: 0x06001E14 RID: 7700 RVA: 0x00155F80 File Offset: 0x00154380
	public static string GetNetworkError(string _wwwError)
	{
		Debug.LogError("Network error: " + _wwwError);
		if (!string.IsNullOrEmpty(_wwwError) && _wwwError.StartsWith("503"))
		{
			return ServerErrors.MAINTENANCE_BREAK;
		}
		if (!string.IsNullOrEmpty(_wwwError) && _wwwError.StartsWith("500"))
		{
			return ServerErrors.FATAL_ERROR;
		}
		return ServerErrors.NETWORK_ERROR;
	}

	// Token: 0x04002165 RID: 8549
	private static string NETWORK_ERROR = PsStrings.Get(StringID.NETWORK_ERROR);

	// Token: 0x04002166 RID: 8550
	private static string MAINTENANCE_BREAK = PsStrings.Get(StringID.MAINTENANCE_BREAK);

	// Token: 0x04002167 RID: 8551
	public static string FATAL_ERROR = PsStrings.Get(StringID.FATAL_ERROR);
}
