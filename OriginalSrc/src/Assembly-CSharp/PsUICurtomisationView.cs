using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public abstract class PsUICurtomisationView : UICanvas
{
	// Token: 0x060004DC RID: 1244 RVA: 0x0003CB85 File Offset: 0x0003AF85
	public PsUICurtomisationView(UIComponent _parent, string _tag)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.RemoveDrawHandler();
	}

	// Token: 0x060004DD RID: 1245
	public abstract void UpdateItems();

	// Token: 0x060004DE RID: 1246 RVA: 0x0003CBB4 File Offset: 0x0003AFB4
	public override void Step()
	{
		if (this.m_selectionChanged && this.m_itemChangedAction != null)
		{
			this.m_itemChangedAction.Invoke(this.m_selectedItem);
			this.m_selectionChanged = false;
		}
		base.Step();
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0003CBEC File Offset: 0x0003AFEC
	public virtual void HideUI(Action _callback)
	{
		if (this.m_tween != null)
		{
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = null;
		}
		this.m_tween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Position, TweenStyle.Linear, this.m_TC.transform.localPosition, new Vector3(this.m_actualWidth, this.m_TC.transform.localPosition.y, this.m_TC.transform.localPosition.z), 0.2f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_tween, delegate(TweenC _c)
		{
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = null;
			if (_callback != null)
			{
				_callback.Invoke();
			}
		});
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0003CCAC File Offset: 0x0003B0AC
	public virtual void ShowUI()
	{
		if (this.m_tween != null)
		{
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = null;
		}
		this.m_tween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Position, TweenStyle.Linear, new Vector3(this.m_actualWidth, this.m_TC.transform.localPosition.y, this.m_TC.transform.localPosition.z), this.m_TC.transform.localPosition, 0.2f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_tween, delegate(TweenC _c)
		{
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = null;
		});
		TweenS.UpdateTween(this.m_tween, 0f);
	}

	// Token: 0x04000634 RID: 1588
	public string m_selectedItem;

	// Token: 0x04000635 RID: 1589
	public bool m_selectionChanged;

	// Token: 0x04000636 RID: 1590
	public Action<string> m_itemChangedAction;

	// Token: 0x04000637 RID: 1591
	private TweenC m_tween;
}
