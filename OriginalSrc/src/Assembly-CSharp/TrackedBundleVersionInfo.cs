using System;

// Token: 0x02000517 RID: 1303
public class TrackedBundleVersionInfo
{
	// Token: 0x0600263A RID: 9786 RVA: 0x001A5923 File Offset: 0x001A3D23
	public TrackedBundleVersionInfo()
	{
		this.version = "_undefined";
		this.index = -1;
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x001A593D File Offset: 0x001A3D3D
	public TrackedBundleVersionInfo(string version, int index = -1)
	{
		this.version = version;
		this.index = index;
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x001A5953 File Offset: 0x001A3D53
	public static bool operator ==(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2)
	{
		return TrackedBundleVersionInfo.compareTo(info1, info2) == 0;
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x001A5964 File Offset: 0x001A3D64
	public static bool operator !=(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2)
	{
		return TrackedBundleVersionInfo.compareTo(info1, info2) != 0;
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x001A5978 File Offset: 0x001A3D78
	public static bool operator >(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2)
	{
		return TrackedBundleVersionInfo.compareTo(info1, info2) > 0;
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x001A5989 File Offset: 0x001A3D89
	public static bool operator <(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2)
	{
		return TrackedBundleVersionInfo.compareTo(info1, info2) < 0;
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x001A599A File Offset: 0x001A3D9A
	public static bool operator >=(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2)
	{
		return TrackedBundleVersionInfo.compareTo(info1, info2) >= 0;
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x001A59AE File Offset: 0x001A3DAE
	public static bool operator <=(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2)
	{
		return TrackedBundleVersionInfo.compareTo(info1, info2) <= 0;
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x001A59C2 File Offset: 0x001A3DC2
	public static bool operator ==(TrackedBundleVersionInfo info1, string info2)
	{
		return string.Compare(info1.version, info2) == 0;
	}

	// Token: 0x06002643 RID: 9795 RVA: 0x001A59D3 File Offset: 0x001A3DD3
	public static bool operator ==(string info1, TrackedBundleVersionInfo info2)
	{
		return string.Compare(info1, info2.version) == 0;
	}

	// Token: 0x06002644 RID: 9796 RVA: 0x001A59E4 File Offset: 0x001A3DE4
	public static bool operator !=(TrackedBundleVersionInfo info1, string info2)
	{
		return string.Compare(info1.version, info2) != 0;
	}

	// Token: 0x06002645 RID: 9797 RVA: 0x001A59F8 File Offset: 0x001A3DF8
	public static bool operator !=(string info1, TrackedBundleVersionInfo info2)
	{
		return string.Compare(info1, info2.version) != 0;
	}

	// Token: 0x06002646 RID: 9798 RVA: 0x001A5A0C File Offset: 0x001A3E0C
	public static bool operator >(TrackedBundleVersionInfo info1, string info2)
	{
		return string.Compare(info1.version, info2) > 0;
	}

	// Token: 0x06002647 RID: 9799 RVA: 0x001A5A1D File Offset: 0x001A3E1D
	public static bool operator >(string info1, TrackedBundleVersionInfo info2)
	{
		return string.Compare(info1, info2.version) > 0;
	}

	// Token: 0x06002648 RID: 9800 RVA: 0x001A5A2E File Offset: 0x001A3E2E
	public static bool operator <(TrackedBundleVersionInfo info1, string info2)
	{
		return string.Compare(info1.version, info2) < 0;
	}

	// Token: 0x06002649 RID: 9801 RVA: 0x001A5A3F File Offset: 0x001A3E3F
	public static bool operator <(string info1, TrackedBundleVersionInfo info2)
	{
		return string.Compare(info1, info2.version) < 0;
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x001A5A50 File Offset: 0x001A3E50
	public static bool operator >=(TrackedBundleVersionInfo info1, string info2)
	{
		return string.Compare(info1.version, info2) >= 0;
	}

	// Token: 0x0600264B RID: 9803 RVA: 0x001A5A64 File Offset: 0x001A3E64
	public static bool operator >=(string info1, TrackedBundleVersionInfo info2)
	{
		return string.Compare(info1, info2.version) >= 0;
	}

	// Token: 0x0600264C RID: 9804 RVA: 0x001A5A78 File Offset: 0x001A3E78
	public static bool operator <=(TrackedBundleVersionInfo info1, string info2)
	{
		return string.Compare(info1.version, info2) <= 0;
	}

	// Token: 0x0600264D RID: 9805 RVA: 0x001A5A8C File Offset: 0x001A3E8C
	public static bool operator <=(string info1, TrackedBundleVersionInfo info2)
	{
		return string.Compare(info1, info2.version) <= 0;
	}

	// Token: 0x0600264E RID: 9806 RVA: 0x001A5AA0 File Offset: 0x001A3EA0
	public static int DefaultCompareTo(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2)
	{
		if (info1 == null || info2 == null)
		{
			return ((info1 != null) ? 0 : (-1)) + ((info2 != null) ? 0 : 1);
		}
		if (info1.index < 0 && info2.index < 0)
		{
			return TrackedBundleVersionInfo.StringBasedCompareTo(info1, info2);
		}
		if (info1.index > info2.index)
		{
			return 1;
		}
		if (info1.index < info2.index)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x0600264F RID: 9807 RVA: 0x001A5B1C File Offset: 0x001A3F1C
	public static int StringBasedCompareTo(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2)
	{
		Version version = new Version(info1.version);
		Version version2 = new Version(info2.version);
		return version.CompareTo(version2);
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x001A5B48 File Offset: 0x001A3F48
	public bool AtLeast(TrackedBundleVersionInfo info2)
	{
		return TrackedBundleVersionInfo.compareTo(this, info2) >= 0;
	}

	// Token: 0x06002651 RID: 9809 RVA: 0x001A5B5C File Offset: 0x001A3F5C
	public bool Before(TrackedBundleVersionInfo info2)
	{
		return TrackedBundleVersionInfo.compareTo(this, info2) < 0;
	}

	// Token: 0x06002652 RID: 9810 RVA: 0x001A5B6D File Offset: 0x001A3F6D
	public override bool Equals(object obj2)
	{
		return obj2 != null && obj2 is TrackedBundleVersionInfo && TrackedBundleVersionInfo.compareTo(this, (TrackedBundleVersionInfo)obj2) == 0;
	}

	// Token: 0x06002653 RID: 9811 RVA: 0x001A5B96 File Offset: 0x001A3F96
	public override int GetHashCode()
	{
		return this.ToString().GetHashCode();
	}

	// Token: 0x06002654 RID: 9812 RVA: 0x001A5BA3 File Offset: 0x001A3FA3
	public override string ToString()
	{
		return string.Format("{0}-{1}", this.version, this.index);
	}

	// Token: 0x04002BD2 RID: 11218
	public const string UndefinedVersionString = "_undefined";

	// Token: 0x04002BD3 RID: 11219
	public static TrackedBundleVersionInfo.CompareTo compareTo = new TrackedBundleVersionInfo.CompareTo(TrackedBundleVersionInfo.DefaultCompareTo);

	// Token: 0x04002BD4 RID: 11220
	public string version;

	// Token: 0x04002BD5 RID: 11221
	public int index;

	// Token: 0x02000518 RID: 1304
	// (Invoke) Token: 0x06002657 RID: 9815
	public delegate int CompareTo(TrackedBundleVersionInfo info1, TrackedBundleVersionInfo info2);
}
