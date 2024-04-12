using System;
using UnityEngine;

// Token: 0x020004D7 RID: 1239
public class TweenC : BasicComponent
{
	// Token: 0x060022F6 RID: 8950 RVA: 0x001917FB File Offset: 0x0018FBFB
	public TweenC()
		: base(ComponentType.Tween)
	{
	}

	// Token: 0x060022F7 RID: 8951 RVA: 0x00191808 File Offset: 0x0018FC08
	public override void Reset()
	{
		base.Reset();
		this.p_TC = null;
		this.duration = 0f;
		this.currentRepeat = 0;
		this.repeats = 0;
		this.delay = 0f;
		this.mirrored = false;
		this.removeEntityAtFinish = false;
		this.removeComponentAtFinish = false;
		this.hasFinished = false;
		this.curved = false;
		this.alphaSprite = true;
		this.alphaPrefabs = false;
		this.alphaText = false;
		this.alphaShader = null;
		this.customObject = null;
		this.tweenEndEvent = null;
		this.tweenStartEvent = null;
		this.hasStarted = false;
	}

	// Token: 0x040029BA RID: 10682
	public TransformC p_TC;

	// Token: 0x040029BB RID: 10683
	public TweenedProperty tweenedProperty;

	// Token: 0x040029BC RID: 10684
	public bool mirrored;

	// Token: 0x040029BD RID: 10685
	public int currentRepeat;

	// Token: 0x040029BE RID: 10686
	public int repeats;

	// Token: 0x040029BF RID: 10687
	public TweenStyle currentTweenStyle;

	// Token: 0x040029C0 RID: 10688
	public TweenStyle mirroredTweenStyle;

	// Token: 0x040029C1 RID: 10689
	public Vector3 startValue;

	// Token: 0x040029C2 RID: 10690
	public Vector3 endValue;

	// Token: 0x040029C3 RID: 10691
	public Vector3 currentValue;

	// Token: 0x040029C4 RID: 10692
	public Vector3 control0;

	// Token: 0x040029C5 RID: 10693
	public Vector3 control1;

	// Token: 0x040029C6 RID: 10694
	public float delay;

	// Token: 0x040029C7 RID: 10695
	public float duration;

	// Token: 0x040029C8 RID: 10696
	public float startTime;

	// Token: 0x040029C9 RID: 10697
	public float currentTime;

	// Token: 0x040029CA RID: 10698
	public bool useUnscaledDeltaTime;

	// Token: 0x040029CB RID: 10699
	public TweenEventDelegate tweenEndEvent;

	// Token: 0x040029CC RID: 10700
	public TweenEventDelegate tweenStartEvent;

	// Token: 0x040029CD RID: 10701
	public bool removeEntityAtFinish;

	// Token: 0x040029CE RID: 10702
	public bool removeComponentAtFinish;

	// Token: 0x040029CF RID: 10703
	public bool hasFinished;

	// Token: 0x040029D0 RID: 10704
	public bool hasStarted;

	// Token: 0x040029D1 RID: 10705
	public bool curved;

	// Token: 0x040029D2 RID: 10706
	public bool alphaSprite;

	// Token: 0x040029D3 RID: 10707
	public bool alphaPrefabs;

	// Token: 0x040029D4 RID: 10708
	public bool alphaText;

	// Token: 0x040029D5 RID: 10709
	public Shader alphaShader;

	// Token: 0x040029D6 RID: 10710
	public object customObject;
}
