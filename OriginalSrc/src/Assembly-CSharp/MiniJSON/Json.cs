using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniJSON
{
	// Token: 0x02000573 RID: 1395
	public static class Json
	{
		// Token: 0x060028A7 RID: 10407 RVA: 0x001B0657 File Offset: 0x001AEA57
		public static object Deserialize(string json)
		{
			if (json == null)
			{
				return null;
			}
			return Json.Parser.Parse(json);
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x001B0667 File Offset: 0x001AEA67
		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}

		// Token: 0x02000574 RID: 1396
		private sealed class Parser : IDisposable
		{
			// Token: 0x060028A9 RID: 10409 RVA: 0x001B066F File Offset: 0x001AEA6F
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x060028AA RID: 10410 RVA: 0x001B0683 File Offset: 0x001AEA83
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x060028AB RID: 10411 RVA: 0x001B06A4 File Offset: 0x001AEAA4
			public static object Parse(string jsonString)
			{
				object obj;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					obj = parser.ParseValue();
				}
				return obj;
			}

			// Token: 0x060028AC RID: 10412 RVA: 0x001B06E4 File Offset: 0x001AEAE4
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x060028AD RID: 10413 RVA: 0x001B06F8 File Offset: 0x001AEAF8
			private Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				this.json.Read();
				for (;;)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					switch (nextToken)
					{
					case Json.Parser.TOKEN.NONE:
						goto IL_37;
					default:
						if (nextToken != Json.Parser.TOKEN.COMMA)
						{
							string text = this.ParseString();
							if (text == null)
							{
								goto Block_2;
							}
							if (this.NextToken != Json.Parser.TOKEN.COLON)
							{
								goto Block_3;
							}
							this.json.Read();
							dictionary[text] = this.ParseValue();
						}
						break;
					case Json.Parser.TOKEN.CURLY_CLOSE:
						return dictionary;
					}
				}
				IL_37:
				return null;
				Block_2:
				return null;
				Block_3:
				return null;
			}

			// Token: 0x060028AE RID: 10414 RVA: 0x001B0784 File Offset: 0x001AEB84
			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					switch (nextToken)
					{
					case Json.Parser.TOKEN.SQUARED_CLOSE:
						flag = false;
						break;
					default:
					{
						if (nextToken == Json.Parser.TOKEN.NONE)
						{
							return null;
						}
						object obj = this.ParseByToken(nextToken);
						list.Add(obj);
						break;
					}
					case Json.Parser.TOKEN.COMMA:
						break;
					}
				}
				return list;
			}

			// Token: 0x060028AF RID: 10415 RVA: 0x001B07FC File Offset: 0x001AEBFC
			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x060028B0 RID: 10416 RVA: 0x001B0818 File Offset: 0x001AEC18
			private object ParseByToken(Json.Parser.TOKEN token)
			{
				switch (token)
				{
				case Json.Parser.TOKEN.STRING:
					return this.ParseString();
				case Json.Parser.TOKEN.NUMBER:
					return this.ParseNumber();
				case Json.Parser.TOKEN.TRUE:
					return true;
				case Json.Parser.TOKEN.FALSE:
					return false;
				case Json.Parser.TOKEN.NULL:
					return null;
				default:
					switch (token)
					{
					case Json.Parser.TOKEN.CURLY_OPEN:
						return this.ParseObject();
					case Json.Parser.TOKEN.SQUARED_OPEN:
						return this.ParseArray();
					}
					return null;
				}
			}

			// Token: 0x060028B1 RID: 10417 RVA: 0x001B0888 File Offset: 0x001AEC88
			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					if (this.json.Peek() == -1)
					{
						break;
					}
					char c = this.NextChar;
					if (c != '"')
					{
						if (c != '\\')
						{
							stringBuilder.Append(c);
						}
						else if (this.json.Peek() == -1)
						{
							flag = false;
						}
						else
						{
							c = this.NextChar;
							switch (c)
							{
							case 'r':
								stringBuilder.Append('\r');
								break;
							default:
								if (c != '"' && c != '/' && c != '\\')
								{
									if (c != 'b')
									{
										if (c != 'f')
										{
											if (c == 'n')
											{
												stringBuilder.Append('\n');
											}
										}
										else
										{
											stringBuilder.Append('\f');
										}
									}
									else
									{
										stringBuilder.Append('\b');
									}
								}
								else
								{
									stringBuilder.Append(c);
								}
								break;
							case 't':
								stringBuilder.Append('\t');
								break;
							case 'u':
							{
								char[] array = new char[4];
								for (int i = 0; i < 4; i++)
								{
									array[i] = this.NextChar;
								}
								stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
								break;
							}
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060028B2 RID: 10418 RVA: 0x001B0A08 File Offset: 0x001AEE08
			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				if (nextWord.IndexOf('.') == -1)
				{
					long num;
					long.TryParse(nextWord, ref num);
					return num;
				}
				double num2;
				double.TryParse(nextWord, ref num2);
				return num2;
			}

			// Token: 0x060028B3 RID: 10419 RVA: 0x001B0A49 File Offset: 0x001AEE49
			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(this.PeekChar))
				{
					this.json.Read();
					if (this.json.Peek() == -1)
					{
						break;
					}
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060028B4 RID: 10420 RVA: 0x001B0A82 File Offset: 0x001AEE82
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060028B5 RID: 10421 RVA: 0x001B0A94 File Offset: 0x001AEE94
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060028B6 RID: 10422 RVA: 0x001B0AA8 File Offset: 0x001AEEA8
			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!Json.Parser.IsWordBreak(this.PeekChar))
					{
						stringBuilder.Append(this.NextChar);
						if (this.json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x060028B7 RID: 10423 RVA: 0x001B0AFC File Offset: 0x001AEEFC
			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					if (this.json.Peek() == -1)
					{
						return Json.Parser.TOKEN.NONE;
					}
					char peekChar = this.PeekChar;
					switch (peekChar)
					{
					case ',':
						this.json.Read();
						return Json.Parser.TOKEN.COMMA;
					case '-':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						return Json.Parser.TOKEN.NUMBER;
					default:
						switch (peekChar)
						{
						case '[':
							return Json.Parser.TOKEN.SQUARED_OPEN;
						default:
							switch (peekChar)
							{
							case '{':
								return Json.Parser.TOKEN.CURLY_OPEN;
							default:
								if (peekChar != '"')
								{
									string nextWord = this.NextWord;
									if (nextWord != null)
									{
										if (nextWord == "false")
										{
											return Json.Parser.TOKEN.FALSE;
										}
										if (nextWord == "true")
										{
											return Json.Parser.TOKEN.TRUE;
										}
										if (nextWord == "null")
										{
											return Json.Parser.TOKEN.NULL;
										}
									}
									return Json.Parser.TOKEN.NONE;
								}
								return Json.Parser.TOKEN.STRING;
							case '}':
								this.json.Read();
								return Json.Parser.TOKEN.CURLY_CLOSE;
							}
							break;
						case ']':
							this.json.Read();
							return Json.Parser.TOKEN.SQUARED_CLOSE;
						}
						break;
					case ':':
						return Json.Parser.TOKEN.COLON;
					}
				}
			}

			// Token: 0x04002DE1 RID: 11745
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x04002DE2 RID: 11746
			private StringReader json;

			// Token: 0x02000575 RID: 1397
			private enum TOKEN
			{
				// Token: 0x04002DE4 RID: 11748
				NONE,
				// Token: 0x04002DE5 RID: 11749
				CURLY_OPEN,
				// Token: 0x04002DE6 RID: 11750
				CURLY_CLOSE,
				// Token: 0x04002DE7 RID: 11751
				SQUARED_OPEN,
				// Token: 0x04002DE8 RID: 11752
				SQUARED_CLOSE,
				// Token: 0x04002DE9 RID: 11753
				COLON,
				// Token: 0x04002DEA RID: 11754
				COMMA,
				// Token: 0x04002DEB RID: 11755
				STRING,
				// Token: 0x04002DEC RID: 11756
				NUMBER,
				// Token: 0x04002DED RID: 11757
				TRUE,
				// Token: 0x04002DEE RID: 11758
				FALSE,
				// Token: 0x04002DEF RID: 11759
				NULL
			}
		}

		// Token: 0x02000576 RID: 1398
		private sealed class Serializer
		{
			// Token: 0x060028B8 RID: 10424 RVA: 0x001B0C25 File Offset: 0x001AF025
			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			// Token: 0x060028B9 RID: 10425 RVA: 0x001B0C38 File Offset: 0x001AF038
			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			// Token: 0x060028BA RID: 10426 RVA: 0x001B0C60 File Offset: 0x001AF060
			private void SerializeValue(object value)
			{
				string text;
				IList list;
				IDictionary dictionary;
				if (value == null)
				{
					this.builder.Append("null");
				}
				else if ((text = value as string) != null)
				{
					this.SerializeString(text);
				}
				else if (value is bool)
				{
					this.builder.Append((!(bool)value) ? "false" : "true");
				}
				else if ((list = value as IList) != null)
				{
					this.SerializeArray(list);
				}
				else if ((dictionary = value as IDictionary) != null)
				{
					this.SerializeObject(dictionary);
				}
				else if (value is char)
				{
					this.SerializeString(new string((char)value, 1));
				}
				else
				{
					this.SerializeOther(value);
				}
			}

			// Token: 0x060028BB RID: 10427 RVA: 0x001B0D34 File Offset: 0x001AF134
			private void SerializeObject(IDictionary obj)
			{
				bool flag = true;
				this.builder.Append('{');
				IEnumerator enumerator = obj.Keys.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						if (!flag)
						{
							this.builder.Append(',');
						}
						this.SerializeString(obj2.ToString());
						this.builder.Append(':');
						this.SerializeValue(obj[obj2]);
						flag = false;
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
				this.builder.Append('}');
			}

			// Token: 0x060028BC RID: 10428 RVA: 0x001B0DE8 File Offset: 0x001AF1E8
			private void SerializeArray(IList anArray)
			{
				this.builder.Append('[');
				bool flag = true;
				IEnumerator enumerator = anArray.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						if (!flag)
						{
							this.builder.Append(',');
						}
						this.SerializeValue(obj);
						flag = false;
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
				this.builder.Append(']');
			}

			// Token: 0x060028BD RID: 10429 RVA: 0x001B0E78 File Offset: 0x001AF278
			private void SerializeString(string str)
			{
				this.builder.Append('"');
				char[] array = str.ToCharArray();
				foreach (char c in array)
				{
					switch (c)
					{
					case '\b':
						this.builder.Append("\\b");
						break;
					case '\t':
						this.builder.Append("\\t");
						break;
					case '\n':
						this.builder.Append("\\n");
						break;
					default:
						if (c != '"')
						{
							if (c != '\\')
							{
								int num = Convert.ToInt32(c);
								if (num >= 32 && num <= 126)
								{
									this.builder.Append(c);
								}
								else
								{
									this.builder.Append("\\u");
									this.builder.Append(num.ToString("x4"));
								}
							}
							else
							{
								this.builder.Append("\\\\");
							}
						}
						else
						{
							this.builder.Append("\\\"");
						}
						break;
					case '\f':
						this.builder.Append("\\f");
						break;
					case '\r':
						this.builder.Append("\\r");
						break;
					}
				}
				this.builder.Append('"');
			}

			// Token: 0x060028BE RID: 10430 RVA: 0x001B0FEC File Offset: 0x001AF3EC
			private void SerializeOther(object value)
			{
				if (value is float)
				{
					this.builder.Append(((float)value).ToString("R"));
				}
				else if (value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong)
				{
					this.builder.Append(value);
				}
				else if (value is double || value is decimal)
				{
					this.builder.Append(Convert.ToDouble(value).ToString("R"));
				}
				else
				{
					this.SerializeString(value.ToString());
				}
			}

			// Token: 0x04002DF0 RID: 11760
			private StringBuilder builder;
		}
	}
}
