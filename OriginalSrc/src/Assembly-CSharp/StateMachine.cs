using System;
using System.Collections.Generic;

// Token: 0x02000562 RID: 1378
public class StateMachine
{
	// Token: 0x06002828 RID: 10280 RVA: 0x001AB6F1 File Offset: 0x001A9AF1
	public StateMachine(IStatedObject _parent)
	{
		this.m_parent = _parent;
	}

	// Token: 0x06002829 RID: 10281 RVA: 0x001AB70B File Offset: 0x001A9B0B
	public StateMachine()
	{
	}

	// Token: 0x0600282A RID: 10282 RVA: 0x001AB71E File Offset: 0x001A9B1E
	private void SetCurrentState(IState _s)
	{
		this.m_currentState = _s;
	}

	// Token: 0x0600282B RID: 10283 RVA: 0x001AB727 File Offset: 0x001A9B27
	private void SetPreviousState(IState _s)
	{
		this.m_previousState = _s;
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x001AB730 File Offset: 0x001A9B30
	public IState GetCurrentState()
	{
		return this.m_currentState;
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x001AB738 File Offset: 0x001A9B38
	public IState GetPreviousState()
	{
		return this.m_previousState;
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x001AB740 File Offset: 0x001A9B40
	public void Update()
	{
		if (this.m_changeToState != null)
		{
			if (this.m_currentState != null)
			{
				this.m_previousState = this.m_currentState;
				this.m_currentState.Exit();
				this.m_currentState = this.m_changeToState;
				this.m_currentState.m_stateMachine = this;
				this.m_currentState.Enter(this.m_parent);
			}
			else
			{
				this.m_currentState = this.m_changeToState;
				this.m_currentState.m_stateMachine = this;
				this.m_currentState.Enter(this.m_parent);
			}
			this.m_changeToState = null;
			if (this.m_queuedChangeStates.Count > 0)
			{
				this.m_changeToState = this.m_queuedChangeStates[0];
				this.m_queuedChangeStates.RemoveAt(0);
			}
		}
		if (this.m_currentState != null)
		{
			this.m_currentState.Execute();
		}
	}

	// Token: 0x0600282F RID: 10287 RVA: 0x001AB81D File Offset: 0x001A9C1D
	public void ChangeState(IState newState)
	{
		if (this.m_changeToState == null)
		{
			this.m_changeToState = newState;
		}
		else
		{
			this.m_queuedChangeStates.Add(newState);
		}
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x001AB842 File Offset: 0x001A9C42
	public void ClearState()
	{
		if (this.m_currentState != null)
		{
			this.m_previousState = this.m_currentState;
			this.m_currentState.Exit();
			this.m_currentState = null;
		}
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x001AB86D File Offset: 0x001A9C6D
	public void RevertToPreviousState()
	{
		if (this.m_previousState != null)
		{
			this.ChangeState(this.m_previousState);
		}
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x001AB886 File Offset: 0x001A9C86
	public void Destroy()
	{
		if (this.m_currentState != null)
		{
			this.m_currentState.Exit();
		}
	}

	// Token: 0x04002D80 RID: 11648
	public IStatedObject m_parent;

	// Token: 0x04002D81 RID: 11649
	private IState m_currentState;

	// Token: 0x04002D82 RID: 11650
	private IState m_previousState;

	// Token: 0x04002D83 RID: 11651
	private IState m_changeToState;

	// Token: 0x04002D84 RID: 11652
	private List<IState> m_queuedChangeStates = new List<IState>();
}
