using System;
using UnityEngine;

// Token: 0x02000577 RID: 1399
public class NoodlePermissionGranter : MonoBehaviour
{
	// Token: 0x060028C0 RID: 10432 RVA: 0x001B10E0 File Offset: 0x001AF4E0
	private static string GetPermissionCode(NoodlePermissionGranter.AndroidPermission _permission)
	{
		return "android.permission." + _permission.ToString();
	}

	// Token: 0x060028C1 RID: 10433 RVA: 0x001B10F9 File Offset: 0x001AF4F9
	public static void GrantPermission(NoodlePermissionGranter.AndroidPermission _permission)
	{
		if (!NoodlePermissionGranter.initialized)
		{
			NoodlePermissionGranter.initialize();
		}
		NoodlePermissionGranter.noodlePermissionGranterClass.CallStatic("grantPermission", new object[]
		{
			NoodlePermissionGranter.activity,
			(int)_permission
		});
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x001B1130 File Offset: 0x001AF530
	public static bool ShouldShowRequestPermissionRationale(NoodlePermissionGranter.AndroidPermission _permission)
	{
		if (!NoodlePermissionGranter.initialized)
		{
			NoodlePermissionGranter.initialize();
		}
		bool flag = NoodlePermissionGranter.activity.Call<bool>("shouldShowRequestPermissionRationale", new object[] { NoodlePermissionGranter.GetPermissionCode(_permission) });
		Debug.LogError(string.Concat(new object[]
		{
			"Should show requestpermissionrationale ",
			_permission.ToString(),
			": ",
			flag
		}));
		return flag;
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x001B11A8 File Offset: 0x001AF5A8
	public static bool HasPermission(NoodlePermissionGranter.AndroidPermission _permission)
	{
		if (!NoodlePermissionGranter.initialized)
		{
			NoodlePermissionGranter.initialize();
		}
		string permissionCode = NoodlePermissionGranter.GetPermissionCode(_permission);
		bool flag = NoodlePermissionGranter.noodlePermissionGranterClass.CallStatic<bool>("askIfPermissionIsEnabled", new object[]
		{
			NoodlePermissionGranter.activity,
			permissionCode
		});
		Debug.LogError("permission received for: " + permissionCode + ": " + flag.ToString());
		return flag;
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x001B1210 File Offset: 0x001AF610
	public static int getSDKInt()
	{
		int @static;
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION"))
		{
			@static = androidJavaClass.GetStatic<int>("SDK_INT");
		}
		return @static;
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x001B1258 File Offset: 0x001AF658
	public void Awake()
	{
		NoodlePermissionGranter.instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
		if (base.name != "PermissionGranter")
		{
			base.name = "PermissionGranter";
		}
	}

	// Token: 0x060028C6 RID: 10438 RVA: 0x001B128C File Offset: 0x001AF68C
	private static void initialize()
	{
		if (NoodlePermissionGranter.instance == null)
		{
			GameObject gameObject = new GameObject();
			NoodlePermissionGranter.instance = gameObject.AddComponent<NoodlePermissionGranter>();
			gameObject.name = "PermissionGranter";
		}
		NoodlePermissionGranter.noodlePermissionGranterClass = new AndroidJavaClass("com.traplight.permissiongranter.TraplightPermissionGranter");
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		NoodlePermissionGranter.activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		NoodlePermissionGranter.initialized = true;
	}

	// Token: 0x060028C7 RID: 10439 RVA: 0x001B12F8 File Offset: 0x001AF6F8
	private void permissionRequestCallbackInternal(string message)
	{
		bool flag = message == "PERMISSION_GRANTED";
		if (NoodlePermissionGranter.PermissionRequestCallback != null)
		{
			NoodlePermissionGranter.PermissionRequestCallback.Invoke(flag);
		}
	}

	// Token: 0x04002DF1 RID: 11761
	public static Action<bool> PermissionRequestCallback;

	// Token: 0x04002DF2 RID: 11762
	private static NoodlePermissionGranter instance;

	// Token: 0x04002DF3 RID: 11763
	private static bool initialized;

	// Token: 0x04002DF4 RID: 11764
	private static AndroidJavaClass noodlePermissionGranterClass;

	// Token: 0x04002DF5 RID: 11765
	private static AndroidJavaObject activity;

	// Token: 0x04002DF6 RID: 11766
	private const string WRITE_EXTERNAL_STORAGE = "WRITE_EXTERNAL_STORAGE";

	// Token: 0x04002DF7 RID: 11767
	private const string PERMISSION_GRANTED = "PERMISSION_GRANTED";

	// Token: 0x04002DF8 RID: 11768
	private const string PERMISSION_DENIED = "PERMISSION_DENIED";

	// Token: 0x04002DF9 RID: 11769
	private const string NOODLE_PERMISSION_GRANTER = "PermissionGranter";

	// Token: 0x02000578 RID: 1400
	public enum AndroidPermission
	{
		// Token: 0x04002DFB RID: 11771
		WRITE_EXTERNAL_STORAGE
	}
}
