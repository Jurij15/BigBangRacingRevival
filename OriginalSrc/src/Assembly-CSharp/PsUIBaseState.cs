using System;
using System.Collections;

// Token: 0x02000321 RID: 801
public class PsUIBaseState : BasicState
{
	// Token: 0x0600178B RID: 6027 RVA: 0x000C4A0B File Offset: 0x000C2E0B
	public PsUIBaseState(Type _main, Type _top = null, Type _left = null, Type _right = null, bool _addToOpenPopups = false, InitialPage _page = InitialPage.Center)
	{
		this.m_mainType = _main;
		this.m_leftType = _left;
		this.m_rightType = _right;
		this.m_topType = _top;
		this.m_page = _page;
		this.m_addToOpenPopups = _addToOpenPopups;
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000C4A4B File Offset: 0x000C2E4B
	public override void Enter(IStatedObject _parent)
	{
		EntityManager.SetActivityOfEntitiesWithTag("PlanetUI", false, false, true, true, false, false);
		this.CreateUI();
		if (this.m_createDelegate != null)
		{
			this.m_createDelegate.Invoke();
		}
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x000C4A7A File Offset: 0x000C2E7A
	public void SetCreateDelegate(Action _action)
	{
		this.m_createDelegate = _action;
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x000C4A84 File Offset: 0x000C2E84
	public virtual void CreateUI()
	{
		this.m_baseCanvas = new PsUIBasePopup(this.m_mainType, this.m_topType, this.m_leftType, this.m_rightType, true, this.m_addToOpenPopups, this.m_page, false, false, true);
		if (this.m_queuedActions.Count > 0)
		{
			IEnumerator enumerator = this.m_queuedActions.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					string text = (string)obj;
					this.SetAction(text, this.m_queuedActions[text] as Action);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000C4B44 File Offset: 0x000C2F44
	public void SetAction(string _key, Action _action)
	{
		if (this.m_baseCanvas != null)
		{
			this.m_baseCanvas.SetAction(_key, _action);
		}
		else
		{
			Debug.LogWarning("Base UI is null! Setting action to queue to be set up after UI creation");
			if (!this.m_queuedActions.ContainsKey(_key))
			{
				this.m_queuedActions.Add(_key, _action);
			}
			else
			{
				Debug.LogWarning("DUPLICATE KEY! Replacing action set to key: " + _key);
				this.m_queuedActions[_key] = _action;
			}
		}
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000C4BB8 File Offset: 0x000C2FB8
	public void BringToFront()
	{
		if (this.m_baseCanvas != null)
		{
			this.m_baseCanvas.BringToFront();
		}
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x000C4BD0 File Offset: 0x000C2FD0
	public void CallAction(string _key)
	{
		if (this.m_baseCanvas != null)
		{
			this.m_baseCanvas.CallAction(_key);
		}
		else
		{
			Debug.LogWarning("Base UI is null!");
		}
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x000C4BF9 File Offset: 0x000C2FF9
	public void DestroyCanvas()
	{
		if (this.m_baseCanvas != null)
		{
			this.m_baseCanvas.Destroy();
			this.m_baseCanvas = null;
		}
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x000C4C18 File Offset: 0x000C3018
	public override void Exit()
	{
		if (this.m_createDelegate != null)
		{
			this.m_createDelegate = null;
		}
		this.DestroyCanvas();
		EntityManager.SetActivityOfEntitiesWithTag("PlanetUI", true, true, true, true, false, false);
	}

	// Token: 0x04001A60 RID: 6752
	public PsUIBasePopup m_baseCanvas;

	// Token: 0x04001A61 RID: 6753
	private Type m_mainType;

	// Token: 0x04001A62 RID: 6754
	private Type m_leftType;

	// Token: 0x04001A63 RID: 6755
	private Type m_rightType;

	// Token: 0x04001A64 RID: 6756
	private Type m_topType;

	// Token: 0x04001A65 RID: 6757
	private InitialPage m_page;

	// Token: 0x04001A66 RID: 6758
	private bool m_addToOpenPopups;

	// Token: 0x04001A67 RID: 6759
	private Hashtable m_queuedActions = new Hashtable();

	// Token: 0x04001A68 RID: 6760
	private Action m_createDelegate;
}
