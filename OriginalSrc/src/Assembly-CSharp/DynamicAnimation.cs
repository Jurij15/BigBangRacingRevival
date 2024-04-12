using System;
using UnityEngine;

// Token: 0x020004BA RID: 1210
public class DynamicAnimation : MonoBehaviour
{
	// Token: 0x0600228A RID: 8842 RVA: 0x0018FD0C File Offset: 0x0018E10C
	private void Start()
	{
		AnimationClip animationClip = new AnimationClip();
		animationClip.wrapMode = 2;
		base.GetComponent<Animation>().AddClip(animationClip, "test");
		base.GetComponent<Animation>().Play("test");
		Quaternion quaternion = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		this.xcurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, quaternion.x),
			new Keyframe(1f, quaternion.x)
		});
		this.ycurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, quaternion.y),
			new Keyframe(1f, quaternion.y)
		});
		this.zcurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, quaternion.z),
			new Keyframe(1f, quaternion.z)
		});
		this.wcurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, quaternion.w),
			new Keyframe(1f, quaternion.w)
		});
	}

	// Token: 0x0600228B RID: 8843 RVA: 0x0018FE90 File Offset: 0x0018E290
	private void Update()
	{
		Quaternion quaternion = Quaternion.Euler(new Vector3(0f, 180f, (float)Random.Range(-180, 180)));
		this.xcurve.MoveKey(0, new Keyframe(0f, quaternion.x));
		this.xcurve.MoveKey(1, new Keyframe(1f, quaternion.x));
		this.ycurve.MoveKey(0, new Keyframe(0f, quaternion.y));
		this.ycurve.MoveKey(1, new Keyframe(1f, quaternion.y));
		this.zcurve.MoveKey(0, new Keyframe(0f, quaternion.z));
		this.zcurve.MoveKey(1, new Keyframe(1f, quaternion.z));
		this.wcurve.MoveKey(0, new Keyframe(0f, quaternion.w));
		this.wcurve.MoveKey(1, new Keyframe(1f, quaternion.w));
		AnimationClip clip = base.GetComponent<Animation>().GetClip("test");
		clip.SetCurve(string.Empty, typeof(Transform), "localRotation.x", this.xcurve);
		clip.SetCurve(string.Empty, typeof(Transform), "localRotation.y", this.ycurve);
		clip.SetCurve(string.Empty, typeof(Transform), "localRotation.z", this.zcurve);
		clip.SetCurve(string.Empty, typeof(Transform), "localRotation.w", this.wcurve);
	}

	// Token: 0x040028A2 RID: 10402
	private AnimationCurve xcurve;

	// Token: 0x040028A3 RID: 10403
	private AnimationCurve ycurve;

	// Token: 0x040028A4 RID: 10404
	private AnimationCurve zcurve;

	// Token: 0x040028A5 RID: 10405
	private AnimationCurve wcurve;
}
