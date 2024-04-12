using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

namespace CodeStage.AntiCheat.Examples
{
	// Token: 0x02000476 RID: 1142
	[AddComponentMenu("")]
	public class ActTesterGui : MonoBehaviour
	{
		// Token: 0x06001F0A RID: 7946 RVA: 0x0017DAC9 File Offset: 0x0017BEC9
		public void OnSpeedHackDetected()
		{
			this.speedHackDetected = true;
			Debug.Log("Speed hack Detected!", null);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0017DADD File Offset: 0x0017BEDD
		public void OnInjectionDetected()
		{
			this.injectionDetected = true;
			Debug.Log("Injection Detected!", null);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0017DAF1 File Offset: 0x0017BEF1
		public void OnObscuredTypeCheatingDetected()
		{
			this.obscuredTypeCheatDetected = true;
			Debug.Log("Obscured Vars Cheating Detected!", null);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0017DB05 File Offset: 0x0017BF05
		public void OnWallHackDetected()
		{
			this.wallHackCheatDetected = true;
			Debug.Log("Wall hack Detected!", null);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0017DB19 File Offset: 0x0017BF19
		private void Awake()
		{
			ObscuredPrefs.SetNewCryptoKey(this.prefsEncryptionKey);
			ObscuredPrefs.onAlterationDetected = new Action(this.SavesAlterationDetected);
			ObscuredPrefs.onPossibleForeignSavesDetected = new Action(this.ForeignSavesDetected);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0017DB48 File Offset: 0x0017BF48
		private void Start()
		{
			this.ObscuredStringExample();
			this.ObscuredIntExample();
			this.ObscuredFloatExample();
			this.ObscuredVector3Example();
			base.Invoke("RandomizeObscuredVars", Random.Range(1f, 10f));
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0017DB7C File Offset: 0x0017BF7C
		private void RandomizeObscuredVars()
		{
			this.obscuredInt.RandomizeCryptoKey();
			this.obscuredFloat.RandomizeCryptoKey();
			this.obscuredString.RandomizeCryptoKey();
			this.obscuredVector3.RandomizeCryptoKey();
			base.Invoke("RandomizeObscuredVars", Random.Range(1f, 10f));
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0017DBD0 File Offset: 0x0017BFD0
		private void ObscuredStringExample()
		{
			this.logBuilder.Length = 0;
			this.logBuilder.AppendLine("[ACTk] <b>[ ObscuredString test ]</b>");
			ObscuredString.SetNewCryptoKey("I LOVE MY GIRLz");
			string text = "the Goscurry is not a lie ;)";
			this.logBuilder.AppendLine("Original string:\n" + text);
			ObscuredString obscuredString = text;
			this.logBuilder.AppendLine("How your string is stored in memory when obscured:\n" + obscuredString.GetEncrypted());
			Debug.Log(this.logBuilder, null);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0017DC50 File Offset: 0x0017C050
		private void ObscuredIntExample()
		{
			this.logBuilder.Length = 0;
			this.logBuilder.AppendLine("[ACTk] <b>[ ObscuredInt test ]</b>");
			ObscuredInt.SetNewCryptoKey(434523);
			int num = 5;
			this.logBuilder.AppendLine("Original lives count: " + num);
			ObscuredInt obscuredInt = num;
			num = obscuredInt;
			this.logBuilder.AppendLine("How your lives count is stored in memory when obscured: " + obscuredInt.GetEncrypted());
			ObscuredInt.SetNewCryptoKey(666);
			obscuredInt -= 2;
			obscuredInt = obscuredInt + num + 10;
			obscuredInt /= 2;
			obscuredInt = ObscuredInt.op_Increment(obscuredInt);
			ObscuredInt.SetNewCryptoKey(999);
			obscuredInt = ObscuredInt.op_Increment(obscuredInt);
			obscuredInt = ObscuredInt.op_Decrement(obscuredInt);
			this.logBuilder.AppendLine(string.Concat(new object[]
			{
				"Lives count after few usual operations: ",
				obscuredInt,
				" (",
				obscuredInt.ToString("X"),
				"h)"
			}));
			Debug.Log(this.logBuilder, null);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0017DD7C File Offset: 0x0017C17C
		private void ObscuredFloatExample()
		{
			this.logBuilder.Length = 0;
			this.logBuilder.AppendLine("[ACTk] <b>[ ObscuredFloat test ]</b>");
			ObscuredFloat.SetNewCryptoKey(404);
			float num = 99.9f;
			this.logBuilder.AppendLine("Original health bar: " + num);
			ObscuredFloat obscuredFloat = num;
			this.logBuilder.AppendLine("How your health bar is stored in memory when obscured: " + obscuredFloat.GetEncrypted());
			ObscuredFloat.SetNewCryptoKey(666);
			obscuredFloat += 6f;
			obscuredFloat -= 1.5f;
			obscuredFloat = ObscuredFloat.op_Increment(obscuredFloat);
			obscuredFloat = ObscuredFloat.op_Decrement(obscuredFloat);
			obscuredFloat = ObscuredFloat.op_Decrement(obscuredFloat);
			obscuredFloat = num - obscuredFloat + 10.5f;
			this.logBuilder.AppendLine("Health bar after few usual operations: " + obscuredFloat);
			Debug.Log(this.logBuilder, null);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0017DE7C File Offset: 0x0017C27C
		private void ObscuredVector3Example()
		{
			this.logBuilder.Length = 0;
			this.logBuilder.AppendLine("[ACTk] <b>[ ObscuredVector3 test ]</b>");
			ObscuredVector3.SetNewCryptoKey(404);
			Vector3 vector;
			vector..ctor(54.1f, 64.3f, 63.2f);
			this.logBuilder.AppendLine("Original position: " + vector);
			ObscuredVector3.RawEncryptedVector3 encrypted = vector.GetEncrypted();
			this.logBuilder.AppendLine(string.Concat(new object[] { "How your position is stored in memory when obscured: (", encrypted.x, ", ", encrypted.y, ", ", encrypted.z, ")" }));
			Debug.Log(this.logBuilder, null);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0017DF61 File Offset: 0x0017C361
		private void SavesAlterationDetected()
		{
			this.savesAlterationDetected = true;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0017DF6A File Offset: 0x0017C36A
		private void ForeignSavesDetected()
		{
			this.foreignSavesDetected = true;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0017DF74 File Offset: 0x0017C374
		private void OnGUI()
		{
			GUIStyle guistyle = new GUIStyle(GUI.skin.label);
			guistyle.alignment = 1;
			GUILayout.BeginArea(new Rect(10f, 5f, (float)(Screen.width - 20), (float)(Screen.height - 10)));
			GUILayout.Label("<color=\"#0287C8\"><b>Anti-Cheat Toolkit Sandbox</b></color>", guistyle, new GUILayoutOption[0]);
			GUILayout.Label("Here you can overview common ACTk features and try to cheat something yourself.", guistyle, new GUILayoutOption[0]);
			GUILayout.Space(5f);
			this.currentTab = GUILayout.Toolbar(this.currentTab, this.tabs, new GUILayoutOption[0]);
			if (this.currentTab == 0)
			{
				GUILayout.Label("ACTk offers own collection of the secure types to let you protect your variables from <b>ANY</b> memory hacking tools (Cheat Engine, ArtMoney, GameCIH, Game Guardian, etc.).", new GUILayoutOption[0]);
				GUILayout.Space(5f);
				using (new HorizontalLayout(new GUILayoutOption[0]))
				{
					GUILayout.Label("<b>Obscured types:</b>\n<color=\"#75C4EB\">" + this.GetAllSimpleObscuredTypes() + "</color>", new GUILayoutOption[] { GUILayout.MinWidth(130f) });
					GUILayout.Space(10f);
					using (new VerticalLayout(GUI.skin.box))
					{
						GUILayout.Label("Below you can try to cheat few variables of the regular types and their obscured (secure) analogues (you may change initial values from Tester object inspector):", new GUILayoutOption[0]);
						GUILayout.Space(10f);
						using (new HorizontalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<b>string:</b> " + this.regularString, new GUILayoutOption[] { GUILayout.Width(250f) });
							if (GUILayout.Button("Add random value", new GUILayoutOption[0]))
							{
								this.regularString += (char)Random.Range(97, 122);
							}
							if (GUILayout.Button("Reset", new GUILayoutOption[0]))
							{
								this.regularString = string.Empty;
							}
						}
						using (new HorizontalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<b>ObscuredString:</b> " + this.obscuredString, new GUILayoutOption[] { GUILayout.Width(250f) });
							if (GUILayout.Button("Add random value", new GUILayoutOption[0]))
							{
								this.obscuredString += (char)Random.Range(97, 122);
							}
							if (GUILayout.Button("Reset", new GUILayoutOption[0]))
							{
								this.obscuredString = string.Empty;
							}
						}
						GUILayout.Space(10f);
						using (new HorizontalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<b>int:</b> " + this.regularInt, new GUILayoutOption[] { GUILayout.Width(250f) });
							if (GUILayout.Button("Add random value", new GUILayoutOption[0]))
							{
								this.regularInt += Random.Range(1, 100);
							}
							if (GUILayout.Button("Reset", new GUILayoutOption[0]))
							{
								this.regularInt = 0;
							}
						}
						using (new HorizontalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<b>ObscuredInt:</b> " + this.obscuredInt, new GUILayoutOption[] { GUILayout.Width(250f) });
							if (GUILayout.Button("Add random value", new GUILayoutOption[0]))
							{
								this.obscuredInt += Random.Range(1, 100);
							}
							if (GUILayout.Button("Reset", new GUILayoutOption[0]))
							{
								this.obscuredInt = 0;
							}
						}
						GUILayout.Space(10f);
						using (new HorizontalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<b>float:</b> " + this.regularFloat, new GUILayoutOption[] { GUILayout.Width(250f) });
							if (GUILayout.Button("Add random value", new GUILayoutOption[0]))
							{
								this.regularFloat += Random.Range(1f, 100f);
							}
							if (GUILayout.Button("Reset", new GUILayoutOption[0]))
							{
								this.regularFloat = 0f;
							}
						}
						using (new HorizontalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<b>ObscuredFloat:</b> " + this.obscuredFloat, new GUILayoutOption[] { GUILayout.Width(250f) });
							if (GUILayout.Button("Add random value", new GUILayoutOption[0]))
							{
								this.obscuredFloat += Random.Range(1f, 100f);
							}
							if (GUILayout.Button("Reset", new GUILayoutOption[0]))
							{
								this.obscuredFloat = 0f;
							}
						}
						GUILayout.Space(10f);
						using (new HorizontalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<b>Vector3:</b> " + this.regularVector3, new GUILayoutOption[] { GUILayout.Width(250f) });
							if (GUILayout.Button("Add random value", new GUILayoutOption[0]))
							{
								this.regularVector3 += Random.insideUnitSphere;
							}
							if (GUILayout.Button("Reset", new GUILayoutOption[0]))
							{
								this.regularVector3 = Vector3.zero;
							}
						}
						using (new HorizontalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<b>ObscuredVector3:</b> " + this.obscuredVector3, new GUILayoutOption[] { GUILayout.Width(250f) });
							if (GUILayout.Button("Add random value", new GUILayoutOption[0]))
							{
								this.obscuredVector3 += Random.insideUnitSphere;
							}
							if (GUILayout.Button("Reset", new GUILayoutOption[0]))
							{
								this.obscuredVector3 = Vector3.zero;
							}
						}
					}
				}
			}
			else if (this.currentTab == 1)
			{
				GUILayout.Label("ACTk has secure layer for the PlayerPrefs: <color=\"#75C4EB\">ObscuredPrefs</color>. It protects data from view, detects any cheating attempts, optionally locks data to the current device and supports additional data types.", new GUILayoutOption[0]);
				GUILayout.Space(5f);
				using (new HorizontalLayout(new GUILayoutOption[0]))
				{
					GUILayout.Label("<b>Supported types:</b>\n" + this.GetAllObscuredPrefsDataTypes(), new GUILayoutOption[] { GUILayout.MinWidth(130f) });
					using (new VerticalLayout(GUI.skin.box))
					{
						GUILayout.Label("Below you can try to cheat both regular PlayerPrefs and secure ObscuredPrefs:", new GUILayoutOption[0]);
						using (new VerticalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<color=\"#FF4040\"><b>PlayerPrefs:</b></color>\neasy to cheat, only 3 supported types", guistyle, new GUILayoutOption[0]);
							GUILayout.Space(5f);
							if (string.IsNullOrEmpty(this.regularPrefs))
							{
								this.LoadRegularPrefs();
							}
							using (new HorizontalLayout(new GUILayoutOption[0]))
							{
								GUILayout.Label(this.regularPrefs, new GUILayoutOption[] { GUILayout.Width(270f) });
								using (new VerticalLayout(new GUILayoutOption[0]))
								{
									using (new HorizontalLayout(new GUILayoutOption[0]))
									{
										if (GUILayout.Button("Save", new GUILayoutOption[0]))
										{
											this.SaveRegularPrefs();
										}
										if (GUILayout.Button("Load", new GUILayoutOption[0]))
										{
											this.LoadRegularPrefs();
										}
									}
									if (GUILayout.Button("Delete", new GUILayoutOption[0]))
									{
										this.DeleteRegularPrefs();
									}
								}
							}
						}
						GUILayout.Space(5f);
						using (new VerticalLayout(new GUILayoutOption[0]))
						{
							GUILayout.Label("<color=\"#02C85F\"><b>ObscuredPrefs:</b></color>\nsecure, lot of additional types and extra options", guistyle, new GUILayoutOption[0]);
							GUILayout.Space(5f);
							if (string.IsNullOrEmpty(this.obscuredPrefs))
							{
								this.LoadObscuredPrefs();
							}
							using (new HorizontalLayout(new GUILayoutOption[0]))
							{
								GUILayout.Label(this.obscuredPrefs, new GUILayoutOption[] { GUILayout.Width(270f) });
								using (new VerticalLayout(new GUILayoutOption[0]))
								{
									using (new HorizontalLayout(new GUILayoutOption[0]))
									{
										if (GUILayout.Button("Save", new GUILayoutOption[0]))
										{
											this.SaveObscuredPrefs();
										}
										if (GUILayout.Button("Load", new GUILayoutOption[0]))
										{
											this.LoadObscuredPrefs();
										}
									}
									if (GUILayout.Button("Delete", new GUILayoutOption[0]))
									{
										this.DeleteObscuredPrefs();
									}
									using (new HorizontalLayout(new GUILayoutOption[0]))
									{
										GUILayout.Label("LockToDevice level", new GUILayoutOption[0]);
										this.PlaceUrlButton("http://j.mp/1gxg1tf");
									}
									this.savesLock = GUILayout.SelectionGrid(this.savesLock, new string[]
									{
										ObscuredPrefs.DeviceLockLevel.None.ToString(),
										ObscuredPrefs.DeviceLockLevel.Soft.ToString(),
										ObscuredPrefs.DeviceLockLevel.Strict.ToString()
									}, 3, new GUILayoutOption[0]);
									ObscuredPrefs.lockToDevice = (ObscuredPrefs.DeviceLockLevel)this.savesLock;
									GUILayout.Space(5f);
									using (new HorizontalLayout(new GUILayoutOption[0]))
									{
										ObscuredPrefs.preservePlayerPrefs = GUILayout.Toggle(ObscuredPrefs.preservePlayerPrefs, "preservePlayerPrefs", new GUILayoutOption[0]);
										this.PlaceUrlButton("http://j.mp/1iBK5pz");
									}
									using (new HorizontalLayout(new GUILayoutOption[0]))
									{
										ObscuredPrefs.emergencyMode = GUILayout.Toggle(ObscuredPrefs.emergencyMode, "emergencyMode", new GUILayoutOption[0]);
										this.PlaceUrlButton("http://j.mp/1FRAL5L");
									}
									using (new HorizontalLayout(new GUILayoutOption[0]))
									{
										ObscuredPrefs.readForeignSaves = GUILayout.Toggle(ObscuredPrefs.readForeignSaves, "readForeignSaves", new GUILayoutOption[0]);
										this.PlaceUrlButton("http://j.mp/1LCdpDa");
									}
									GUILayout.Space(5f);
									GUILayout.Label(string.Concat(new object[]
									{
										"<color=\"",
										(!this.savesAlterationDetected) ? "#02C85F" : "#FF4040",
										"\">Saves modification detected: ",
										this.savesAlterationDetected,
										"</color>"
									}), new GUILayoutOption[0]);
									GUILayout.Label(string.Concat(new object[]
									{
										"<color=\"",
										(!this.foreignSavesDetected) ? "#02C85F" : "#FF4040",
										"\">Foreign saves detected: ",
										this.foreignSavesDetected,
										"</color>"
									}), new GUILayoutOption[0]);
								}
							}
						}
						GUILayout.Space(5f);
						this.PlaceUrlButton("http://docs.unity3d.com/ScriptReference/PlayerPrefs.html", "Visit docs to see where PlayerPrefs are stored", -1);
					}
				}
			}
			else
			{
				GUILayout.Label("ACTk is able to detect some types of cheating to let you take action on the cheating players. This example scene has all possible detectors and all of them are automatically start on scene start.", new GUILayoutOption[0]);
				GUILayout.Space(5f);
				using (new VerticalLayout(GUI.skin.box))
				{
					GUILayout.Label("<b>Speed Hack Detector</b>", new GUILayoutOption[0]);
					GUILayout.Label("Allows to detect Cheat Engine's speed hack (and maybe some other speed hack tools) usage.", new GUILayoutOption[0]);
					GUILayout.Label(string.Concat(new string[]
					{
						"<color=\"",
						(!this.speedHackDetected) ? "#02C85F" : "#FF4040",
						"\">Detected: ",
						this.speedHackDetected.ToString().ToLower(),
						"</color>"
					}), new GUILayoutOption[0]);
					GUILayout.Space(10f);
					GUILayout.Label("<b>Obscured Cheating Detector</b>", new GUILayoutOption[0]);
					GUILayout.Label("Detects cheating of any Obscured type (except ObscuredPrefs, it has own detection features) used in project.", new GUILayoutOption[0]);
					GUILayout.Label(string.Concat(new string[]
					{
						"<color=\"",
						(!this.obscuredTypeCheatDetected) ? "#02C85F" : "#FF4040",
						"\">Detected: ",
						this.obscuredTypeCheatDetected.ToString().ToLower(),
						"</color>"
					}), new GUILayoutOption[0]);
					GUILayout.Space(10f);
					GUILayout.Label("<b>WallHack Detector</b>", new GUILayoutOption[0]);
					GUILayout.Label("Detects common types of wall hack cheating: walking through the walls (Rigidbody and CharacterController modules), shooting through the walls (Raycast module), looking through the walls (Wireframe module).", new GUILayoutOption[0]);
					GUILayout.Label(string.Concat(new string[]
					{
						"<color=\"",
						(!this.wallHackCheatDetected) ? "#02C85F" : "#FF4040",
						"\">Detected: ",
						this.wallHackCheatDetected.ToString().ToLower(),
						"</color>"
					}), new GUILayoutOption[0]);
					GUILayout.Space(10f);
					GUILayout.Label("<b>Injection Detector</b>", new GUILayoutOption[0]);
					GUILayout.Label("Allows to detect foreign managed assemblies in your application.", new GUILayoutOption[0]);
					GUILayout.Label(string.Concat(new string[]
					{
						"<color=\"",
						(!this.injectionDetected) ? "#02C85F" : "#FF4040",
						"\">Detected: ",
						this.injectionDetected.ToString().ToLower(),
						"</color>"
					}), new GUILayoutOption[0]);
				}
			}
			GUILayout.EndArea();
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0017F004 File Offset: 0x0017D404
		private string GetAllSimpleObscuredTypes()
		{
			string text = "Can't get the list, sorry :(";
			string types = string.Empty;
			if (string.IsNullOrEmpty(this.allSimpleObscuredTypes))
			{
				IEnumerable<Type> enumerable = Enumerable.Where<Type>(Assembly.GetExecutingAssembly().GetTypes(), (Type t) => t.IsPublic && t.Namespace == "CodeStage.AntiCheat.ObscuredTypes" && t.Name != "ObscuredPrefs");
				Enumerable.ToList<Type>(enumerable).ForEach(delegate(Type t)
				{
					if (types.Length > 0)
					{
						types = types + "\n" + t.Name;
					}
					else
					{
						types += t.Name;
					}
				});
				if (!string.IsNullOrEmpty(types))
				{
					text = types;
					this.allSimpleObscuredTypes = types;
				}
			}
			else
			{
				text = this.allSimpleObscuredTypes;
			}
			return text;
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0017F0AC File Offset: 0x0017D4AC
		private string GetAllObscuredPrefsDataTypes()
		{
			return "int\nfloat\nstring\n<color=\"#75C4EB\">uint\ndouble\nlong\nbool\nbyte[]\nVector2\nVector3\nQuaternion\nColor\nRect</color>";
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0017F0B4 File Offset: 0x0017D4B4
		private void LoadRegularPrefs()
		{
			this.regularPrefs = "int: " + PlayerPrefs.GetInt("money", -1) + "\n";
			string text = this.regularPrefs;
			this.regularPrefs = string.Concat(new object[]
			{
				text,
				"float: ",
				PlayerPrefs.GetFloat("lifeBar", -1f),
				"\n"
			});
			this.regularPrefs = this.regularPrefs + "string: " + PlayerPrefs.GetString("name", "No saved PlayerPrefs!");
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0017F14E File Offset: 0x0017D54E
		private void SaveRegularPrefs()
		{
			PlayerPrefs.SetInt("money", 456);
			PlayerPrefs.SetFloat("lifeBar", 456.789f);
			PlayerPrefs.SetString("name", "Hey, there!");
			PlayerPrefs.Save();
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0017F182 File Offset: 0x0017D582
		private void DeleteRegularPrefs()
		{
			PlayerPrefs.DeleteKey("money");
			PlayerPrefs.DeleteKey("lifeBar");
			PlayerPrefs.DeleteKey("name");
			PlayerPrefs.Save();
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0017F1A8 File Offset: 0x0017D5A8
		private void LoadObscuredPrefs()
		{
			byte[] byteArray = ObscuredPrefs.GetByteArray("demoByteArray", 0, 4);
			this.obscuredPrefs = "int: " + ObscuredPrefs.GetInt("money", -1) + "\n";
			string text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"float: ",
				ObscuredPrefs.GetFloat("lifeBar", -1f),
				"\n"
			});
			this.obscuredPrefs = this.obscuredPrefs + "string: " + ObscuredPrefs.GetString("name", "No saved ObscuredPrefs!") + "\n";
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"bool: ",
				ObscuredPrefs.GetBool("gameComplete", false),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"uint: ",
				ObscuredPrefs.GetUInt("demoUint", 0U),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"long: ",
				ObscuredPrefs.GetLong("demoLong", -1L),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"double: ",
				ObscuredPrefs.GetDouble("demoDouble", -1.0),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"Vector2: ",
				ObscuredPrefs.GetVector2("demoVector2", Vector2.zero),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"Vector3: ",
				ObscuredPrefs.GetVector3("demoVector3", Vector3.zero),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"Quaternion: ",
				ObscuredPrefs.GetQuaternion("demoQuaternion", Quaternion.identity),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"Rect: ",
				ObscuredPrefs.GetRect("demoRect", new Rect(0f, 0f, 0f, 0f)),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"Color: ",
				ObscuredPrefs.GetColor("demoColor", Color.black),
				"\n"
			});
			text = this.obscuredPrefs;
			this.obscuredPrefs = string.Concat(new object[]
			{
				text,
				"byte[]: {",
				byteArray[0],
				",",
				byteArray[1],
				",",
				byteArray[2],
				",",
				byteArray[3],
				"}"
			});
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0017F534 File Offset: 0x0017D934
		private void SaveObscuredPrefs()
		{
			ObscuredPrefs.SetInt("money", 123);
			ObscuredPrefs.SetFloat("lifeBar", 123.456f);
			ObscuredPrefs.SetString("name", "Goscurry is not a lie ;)");
			ObscuredPrefs.SetBool("gameComplete", true);
			ObscuredPrefs.SetUInt("demoUint", 1234567891U);
			ObscuredPrefs.SetLong("demoLong", 1234567891234567890L);
			ObscuredPrefs.SetDouble("demoDouble", 1.234567890123456);
			ObscuredPrefs.SetVector2("demoVector2", Vector2.one);
			ObscuredPrefs.SetVector3("demoVector3", Vector3.one);
			ObscuredPrefs.SetQuaternion("demoQuaternion", Quaternion.Euler(new Vector3(10f, 20f, 30f)));
			ObscuredPrefs.SetRect("demoRect", new Rect(1.5f, 2.6f, 3.7f, 4.8f));
			ObscuredPrefs.SetColor("demoColor", Color.red);
			ObscuredPrefs.SetByteArray("demoByteArray", new byte[] { 44, 104, 43, 32 });
			ObscuredPrefs.Save();
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0017F644 File Offset: 0x0017DA44
		private void DeleteObscuredPrefs()
		{
			ObscuredPrefs.DeleteKey("money");
			ObscuredPrefs.DeleteKey("lifeBar");
			ObscuredPrefs.DeleteKey("name");
			ObscuredPrefs.DeleteKey("gameComplete");
			ObscuredPrefs.DeleteKey("demoUint");
			ObscuredPrefs.DeleteKey("demoLong");
			ObscuredPrefs.DeleteKey("demoDouble");
			ObscuredPrefs.DeleteKey("demoVector2");
			ObscuredPrefs.DeleteKey("demoVector3");
			ObscuredPrefs.DeleteKey("demoQuaternion");
			ObscuredPrefs.DeleteKey("demoRect");
			ObscuredPrefs.DeleteKey("demoColor");
			ObscuredPrefs.DeleteKey("demoByteArray");
			ObscuredPrefs.Save();
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0017F6D8 File Offset: 0x0017DAD8
		private void PlaceUrlButton(string url)
		{
			this.PlaceUrlButton(url, 30);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0017F6E3 File Offset: 0x0017DAE3
		private void PlaceUrlButton(string url, int width)
		{
			this.PlaceUrlButton(url, "?", width);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0017F6F4 File Offset: 0x0017DAF4
		private void PlaceUrlButton(string url, string buttonName, int width)
		{
			GUILayoutOption[] array = new GUILayoutOption[1];
			if (width != -1)
			{
				array[0] = GUILayout.Width((float)width);
			}
			else
			{
				array = null;
			}
			if (GUILayout.Button(buttonName, array))
			{
				Application.OpenURL(url);
			}
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0017F732 File Offset: 0x0017DB32
		private void OnApplicationQuit()
		{
			this.DeleteRegularPrefs();
			this.DeleteObscuredPrefs();
		}

		// Token: 0x040026A3 RID: 9891
		private const string RED_COLOR = "#FF4040";

		// Token: 0x040026A4 RID: 9892
		private const string GREEN_COLOR = "#02C85F";

		// Token: 0x040026A5 RID: 9893
		private const string PREFS_STRING = "name";

		// Token: 0x040026A6 RID: 9894
		private const string PREFS_INT = "money";

		// Token: 0x040026A7 RID: 9895
		private const string PREFS_FLOAT = "lifeBar";

		// Token: 0x040026A8 RID: 9896
		private const string PREFS_BOOL = "gameComplete";

		// Token: 0x040026A9 RID: 9897
		private const string PREFS_UINT = "demoUint";

		// Token: 0x040026AA RID: 9898
		private const string PREFS_LONG = "demoLong";

		// Token: 0x040026AB RID: 9899
		private const string PREFS_DOUBLE = "demoDouble";

		// Token: 0x040026AC RID: 9900
		private const string PREFS_VECTOR2 = "demoVector2";

		// Token: 0x040026AD RID: 9901
		private const string PREFS_VECTOR3 = "demoVector3";

		// Token: 0x040026AE RID: 9902
		private const string PREFS_QUATERNION = "demoQuaternion";

		// Token: 0x040026AF RID: 9903
		private const string PREFS_RECT = "demoRect";

		// Token: 0x040026B0 RID: 9904
		private const string PREFS_COLOR = "demoColor";

		// Token: 0x040026B1 RID: 9905
		private const string PREFS_BYTE_ARRAY = "demoByteArray";

		// Token: 0x040026B2 RID: 9906
		private const string API_URL_LOCK_TO_DEVICE = "http://j.mp/1gxg1tf";

		// Token: 0x040026B3 RID: 9907
		private const string API_URL_PRESERVE_PREFS = "http://j.mp/1iBK5pz";

		// Token: 0x040026B4 RID: 9908
		private const string API_URL_EMERGENCY_MODE = "http://j.mp/1FRAL5L";

		// Token: 0x040026B5 RID: 9909
		private const string API_URL_READ_FOREIGN = "http://j.mp/1LCdpDa";

		// Token: 0x040026B6 RID: 9910
		private const string API_URL_UNOBSCURED_MODE = "http://j.mp/1KVrpxi";

		// Token: 0x040026B7 RID: 9911
		private const string API_URL_PLAYER_PREFS = "http://docs.unity3d.com/ScriptReference/PlayerPrefs.html";

		// Token: 0x040026B8 RID: 9912
		[Header("Regular variables")]
		public string regularString = "I'm regular string";

		// Token: 0x040026B9 RID: 9913
		public int regularInt = 1987;

		// Token: 0x040026BA RID: 9914
		public float regularFloat = 2013.0524f;

		// Token: 0x040026BB RID: 9915
		public Vector3 regularVector3 = new Vector3(10.5f, 11.5f, 12.5f);

		// Token: 0x040026BC RID: 9916
		[Header("Obscured (secure) variables")]
		public ObscuredString obscuredString = "I'm obscured string";

		// Token: 0x040026BD RID: 9917
		public ObscuredInt obscuredInt = 1987;

		// Token: 0x040026BE RID: 9918
		public ObscuredFloat obscuredFloat = 2013.0524f;

		// Token: 0x040026BF RID: 9919
		public ObscuredVector3 obscuredVector3 = new Vector3(10.5f, 11.5f, 12.5f);

		// Token: 0x040026C0 RID: 9920
		public ObscuredBool obscuredBool = true;

		// Token: 0x040026C1 RID: 9921
		public ObscuredLong obscuredLong = 945678987654123345L;

		// Token: 0x040026C2 RID: 9922
		public ObscuredDouble obscuredDouble = 9.45678987654;

		// Token: 0x040026C3 RID: 9923
		public ObscuredVector2 obscuredVector2 = new Vector2(8.5f, 9.5f);

		// Token: 0x040026C4 RID: 9924
		[Header("Other")]
		public string prefsEncryptionKey = "change me!";

		// Token: 0x040026C5 RID: 9925
		private readonly string[] tabs = new string[] { "Variables protection", "Saves protection", "Cheating detectors" };

		// Token: 0x040026C6 RID: 9926
		private int currentTab;

		// Token: 0x040026C7 RID: 9927
		private string allSimpleObscuredTypes;

		// Token: 0x040026C8 RID: 9928
		private string regularPrefs;

		// Token: 0x040026C9 RID: 9929
		private string obscuredPrefs;

		// Token: 0x040026CA RID: 9930
		private int savesLock;

		// Token: 0x040026CB RID: 9931
		private bool savesAlterationDetected;

		// Token: 0x040026CC RID: 9932
		private bool foreignSavesDetected;

		// Token: 0x040026CD RID: 9933
		private bool injectionDetected;

		// Token: 0x040026CE RID: 9934
		private bool speedHackDetected;

		// Token: 0x040026CF RID: 9935
		private bool obscuredTypeCheatDetected;

		// Token: 0x040026D0 RID: 9936
		private bool wallHackCheatDetected;

		// Token: 0x040026D1 RID: 9937
		private readonly StringBuilder logBuilder = new StringBuilder();
	}
}
