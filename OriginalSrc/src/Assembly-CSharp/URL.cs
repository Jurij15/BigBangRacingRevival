using System;
using System.Collections;

// Token: 0x0200045E RID: 1118
public class URL
{
	// Token: 0x06001ECB RID: 7883 RVA: 0x0015A61C File Offset: 0x00158A1C
	public URL()
	{
		this.AddParameter("ts", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
	}

	// Token: 0x06001ECC RID: 7884 RVA: 0x0015A66D File Offset: 0x00158A6D
	public void AddParameter(object _key, object _value)
	{
		this.data.Add(_key, _value);
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x0015A67C File Offset: 0x00158A7C
	public string ConstructURL()
	{
		return this.host + this.ConstructURI();
	}

	// Token: 0x06001ECE RID: 7886 RVA: 0x0015A690 File Offset: 0x00158A90
	public string ConstructURI()
	{
		string text = this.command;
		int num = 0;
		IEnumerator enumerator = this.data.Keys.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text2 = (string)obj;
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					(num != 0) ? "&" : "?",
					text2,
					"=",
					this.data[text2]
				});
				num++;
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
		return text;
	}

	// Token: 0x04002227 RID: 8743
	protected Hashtable data = new Hashtable();

	// Token: 0x04002228 RID: 8744
	protected string host = string.Empty;

	// Token: 0x04002229 RID: 8745
	protected string command = string.Empty;
}
