using System;
using UnityEngine;

// Token: 0x020004BF RID: 1215
public class LastAnimatorStateInfo
{
	// Token: 0x060022A0 RID: 8864 RVA: 0x0019076C File Offset: 0x0018EB6C
	public LastAnimatorStateInfo(Animator _animator)
	{
		this.animatorGameobjectName = _animator.gameObject.name;
		this.states = new AnimatorStateInfo[_animator.layerCount];
		this.parameters = new AnimatorControllerParameter[_animator.parameterCount];
		this.parameterValues = new object[this.parameters.Length];
		for (int i = 0; i < this.states.Length; i++)
		{
			this.states[i] = _animator.GetCurrentAnimatorStateInfo(i);
		}
		for (int j = 0; j < this.parameters.Length; j++)
		{
			this.parameters[j] = _animator.parameters[j];
			if (this.parameters[j].type == 4)
			{
				this.parameterValues[j] = _animator.GetBool(this.parameters[j].name);
			}
			else if (this.parameters[j].type == 1)
			{
				this.parameterValues[j] = _animator.GetFloat(this.parameters[j].name);
			}
			else if (this.parameters[j].type == 3)
			{
				this.parameterValues[j] = _animator.GetInteger(this.parameters[j].name);
			}
			else if (this.parameters[j].type == 9)
			{
				this.parameterValues[j] = _animator.GetBool(this.parameters[j].name);
			}
		}
	}

	// Token: 0x060022A1 RID: 8865 RVA: 0x001908FC File Offset: 0x0018ECFC
	public void ApplyLastState(Animator _animator)
	{
		for (int i = 0; i < this.states.Length; i++)
		{
			_animator.Play(this.states[i].fullPathHash);
		}
		for (int j = 0; j < this.parameters.Length; j++)
		{
			if (this.parameters[j].type == 4)
			{
				_animator.SetBool(this.parameters[j].name, (bool)this.parameterValues[j]);
			}
			else if (this.parameters[j].type == 1)
			{
				_animator.SetFloat(this.parameters[j].name, (float)this.parameterValues[j]);
			}
			else if (this.parameters[j].type == 3)
			{
				_animator.SetInteger(this.parameters[j].name, (int)this.parameterValues[j]);
			}
			else if (this.parameters[j].type == 9 && (bool)this.parameterValues[j])
			{
				_animator.SetTrigger(this.parameters[j].name);
			}
		}
	}

	// Token: 0x040028BA RID: 10426
	public string animatorGameobjectName;

	// Token: 0x040028BB RID: 10427
	private AnimatorControllerParameter[] parameters;

	// Token: 0x040028BC RID: 10428
	private object[] parameterValues;

	// Token: 0x040028BD RID: 10429
	private AnimatorStateInfo[] states;
}
