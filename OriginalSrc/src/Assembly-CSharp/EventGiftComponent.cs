using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000F4 RID: 244
public abstract class EventGiftComponent
{
	// Token: 0x06000565 RID: 1381 RVA: 0x00046BD8 File Offset: 0x00044FD8
	public virtual void Claim(Hashtable _data)
	{
		List<string> list = new List<string>();
		_data.Add("update", ClientTools.GeneratePlayerSetData(new Hashtable(), ref list));
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00046C02 File Offset: 0x00045002
	public virtual void EndAction(Action _callback)
	{
		_callback.Invoke();
	}

	// Token: 0x06000567 RID: 1383
	public abstract void CreateUI(UIComponent _parent);

	// Token: 0x06000568 RID: 1384
	public abstract string GetName();

	// Token: 0x040006FE RID: 1790
	public int texture = -1;
}
