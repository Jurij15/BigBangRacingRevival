using System;
using System.Collections.Generic;

// Token: 0x02000572 RID: 1394
public class MarkovNameGenerator
{
	// Token: 0x060028A3 RID: 10403 RVA: 0x001B0250 File Offset: 0x001AE650
	public MarkovNameGenerator(IEnumerable<string> sampleNames, int order, int minLength)
	{
		if (order < 1)
		{
			order = 1;
		}
		if (minLength < 1)
		{
			minLength = 1;
		}
		this._order = order;
		this._minLength = minLength;
		foreach (string text in sampleNames)
		{
			string[] array = text.Split(new char[] { ',' });
			foreach (string text2 in array)
			{
				string text3 = text2.Trim().ToUpper();
				if (text3.Length >= order + 1)
				{
					this._samples.Add(text3);
				}
			}
		}
		foreach (string text4 in this._samples)
		{
			for (int j = 0; j < text4.Length - order; j++)
			{
				string text5 = text4.Substring(j, order);
				List<char> list;
				if (this._chains.ContainsKey(text5))
				{
					list = this._chains[text5];
				}
				else
				{
					list = new List<char>();
					this._chains[text5] = list;
				}
				list.Add(text4.get_Chars(j + order));
			}
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x060028A4 RID: 10404 RVA: 0x001B0414 File Offset: 0x001AE814
	public string NextName
	{
		get
		{
			string text = string.Empty;
			do
			{
				int num = this._rnd.Next(this._samples.Count);
				int length = this._samples[num].Length;
				text = this._samples[num].Substring(this._rnd.Next(0, this._samples[num].Length - this._order), this._order);
				while (text.Length < length)
				{
					string text2 = text.Substring(text.Length - this._order, this._order);
					char letter = this.GetLetter(text2);
					if (letter == '?')
					{
						break;
					}
					text += this.GetLetter(text2);
				}
				if (text.Contains(" "))
				{
					string[] array = text.Split(new char[] { ' ' });
					text = string.Empty;
					for (int i = 0; i < array.Length; i++)
					{
						if (!(array[i] == string.Empty))
						{
							if (array[i].Length == 1)
							{
								array[i] = array[i].ToUpper();
							}
							else
							{
								array[i] = array[i].Substring(0, 1) + array[i].Substring(1).ToLower();
							}
							if (text != string.Empty)
							{
								text += " ";
							}
							text += array[i];
						}
					}
				}
				else
				{
					text = text.Substring(0, 1) + text.Substring(1).ToLower();
				}
			}
			while (this._used.Contains(text) || text.Length < this._minLength);
			this._used.Add(text);
			return text;
		}
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x001B0601 File Offset: 0x001AEA01
	public void Reset()
	{
		this._used.Clear();
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x001B0610 File Offset: 0x001AEA10
	private char GetLetter(string token)
	{
		if (!this._chains.ContainsKey(token))
		{
			return '?';
		}
		List<char> list = this._chains[token];
		int num = this._rnd.Next(list.Count);
		return list[num];
	}

	// Token: 0x04002DDB RID: 11739
	private Dictionary<string, List<char>> _chains = new Dictionary<string, List<char>>();

	// Token: 0x04002DDC RID: 11740
	private List<string> _samples = new List<string>();

	// Token: 0x04002DDD RID: 11741
	private List<string> _used = new List<string>();

	// Token: 0x04002DDE RID: 11742
	private Random _rnd = new Random();

	// Token: 0x04002DDF RID: 11743
	private int _order;

	// Token: 0x04002DE0 RID: 11744
	private int _minLength;
}
