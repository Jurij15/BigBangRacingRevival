using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using MiniJSON;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class Achievement
{
	// Token: 0x060021AC RID: 8620 RVA: 0x0004516C File Offset: 0x0004356C
	public Achievement(string _identifier, string _name = null, string _description = null, int _target = 1, int _baseValue = 0)
	{
		this.m_identifier = _identifier;
		this.m_name = _name;
		this.m_description = _description;
		this.m_value = _baseValue;
		this.m_target = _target;
		this.m_percentCompleted = 0;
		this.CheckFlags();
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x000451C0 File Offset: 0x000435C0
	public Achievement(Dictionary<string, object> _dictionary)
	{
		this.FromDictionary(_dictionary);
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x060021AE RID: 8622 RVA: 0x000451CF File Offset: 0x000435CF
	// (set) Token: 0x060021AF RID: 8623 RVA: 0x000451D7 File Offset: 0x000435D7
	public string id
	{
		get
		{
			return this.m_identifier;
		}
		set
		{
			this.m_identifier = value;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000451E0 File Offset: 0x000435E0
	// (set) Token: 0x060021B1 RID: 8625 RVA: 0x000451E8 File Offset: 0x000435E8
	public string humanReadableName
	{
		get
		{
			return this.m_name;
		}
		set
		{
			this.m_name = value;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000451F1 File Offset: 0x000435F1
	// (set) Token: 0x060021B3 RID: 8627 RVA: 0x000451F9 File Offset: 0x000435F9
	public string humanReadableDescription
	{
		get
		{
			return this.m_description;
		}
		set
		{
			this.m_description = value;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060021B4 RID: 8628 RVA: 0x00045202 File Offset: 0x00043602
	public bool completed
	{
		get
		{
			this.CheckFlags();
			return this.m_completed;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060021B5 RID: 8629 RVA: 0x00045210 File Offset: 0x00043610
	public bool hidden
	{
		get
		{
			this.CheckFlags();
			return this.m_hidden;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060021B6 RID: 8630 RVA: 0x0004521E File Offset: 0x0004361E
	// (set) Token: 0x060021B7 RID: 8631 RVA: 0x0004522B File Offset: 0x0004362B
	public int percentCompleted
	{
		get
		{
			return this.m_percentCompleted;
		}
		set
		{
			this.m_percentCompleted = Math.Min(value, 100);
			this.CheckFlags();
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060021B8 RID: 8632 RVA: 0x00045246 File Offset: 0x00043646
	public int targetValue
	{
		get
		{
			return this.m_target;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060021B9 RID: 8633 RVA: 0x00045253 File Offset: 0x00043653
	public int currentValue
	{
		get
		{
			return this.m_value;
		}
	}

	// Token: 0x060021BA RID: 8634 RVA: 0x00045260 File Offset: 0x00043660
	protected void CheckFlags()
	{
		if (this.m_percentCompleted >= 0)
		{
			this.m_hidden = false;
		}
		else
		{
			this.m_hidden = true;
		}
		if (this.m_percentCompleted == 100)
		{
			this.m_completed = true;
		}
		else
		{
			this.m_completed = false;
		}
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x000452B6 File Offset: 0x000436B6
	public virtual void Refresh()
	{
		this.m_percentCompleted = Mathf.FloorToInt((float)this.m_value / (float)this.m_target * 100f);
		this.CheckFlags();
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x000452F0 File Offset: 0x000436F0
	public virtual void IncrementProgress(int _value)
	{
		if (!this.completed)
		{
			this.m_value += _value;
			this.m_percentCompleted = Mathf.FloorToInt((float)this.m_value / (float)this.m_target * 100f);
			this.CheckFlags();
			if (this.m_percentCompleted >= 100)
			{
				this.Complete();
			}
			this.ReportProgress();
		}
		else
		{
			this.Complete();
		}
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x0004537E File Offset: 0x0004377E
	public void ReportProgress()
	{
		GooglePlayManager.ReportProgress(this.id, (float)this.m_percentCompleted);
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x00045397 File Offset: 0x00043797
	public void UpdateProgress(int _progressValue)
	{
		this.m_value = _progressValue;
		this.Refresh();
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x000453AB File Offset: 0x000437AB
	public virtual void Complete()
	{
		this.m_value = this.m_target;
		this.m_percentCompleted = 100;
		this.m_hidden = false;
		this.m_completed = true;
		this.ReportProgress();
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x000453DA File Offset: 0x000437DA
	public string ToJson()
	{
		return Json.Serialize(this.ToDict());
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x000453E8 File Offset: 0x000437E8
	public Dictionary<string, object> ToDict()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("id", this.m_identifier);
		dictionary.Add("name", this.m_name);
		dictionary.Add("description", this.m_description);
		dictionary.Add("value", this.m_value);
		dictionary.Add("target", this.m_target);
		return dictionary;
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x00045468 File Offset: 0x00043868
	private void FromDictionary(Dictionary<string, object> _dictionary)
	{
		if (!_dictionary.ContainsKey("id") && !_dictionary.ContainsKey("name") && !_dictionary.ContainsKey("description") && !_dictionary.ContainsKey("value") && !_dictionary.ContainsKey("target"))
		{
			Debug.LogError("Invalid dictionary");
		}
		else
		{
			this.m_identifier = (string)_dictionary["id"];
			this.m_name = (string)_dictionary["name"];
			this.m_description = (string)_dictionary["description"];
			this.m_value = Convert.ToInt32(_dictionary["value"]);
			this.m_target = Convert.ToInt32(_dictionary["target"]);
			this.CheckFlags();
		}
	}

	// Token: 0x040027ED RID: 10221
	protected string m_identifier;

	// Token: 0x040027EE RID: 10222
	protected string m_name;

	// Token: 0x040027EF RID: 10223
	protected string m_description;

	// Token: 0x040027F0 RID: 10224
	private bool m_completed;

	// Token: 0x040027F1 RID: 10225
	private bool m_hidden;

	// Token: 0x040027F2 RID: 10226
	private ObscuredInt m_percentCompleted;

	// Token: 0x040027F3 RID: 10227
	protected ObscuredInt m_value;

	// Token: 0x040027F4 RID: 10228
	protected ObscuredInt m_target;
}
