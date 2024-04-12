using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class Controller
{
	// Token: 0x06000331 RID: 817 RVA: 0x0003029B File Offset: 0x0002E69B
	public Controller()
	{
		Controller.m_controllerDisabled = false;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x000302AC File Offset: 0x0002E6AC
	public void AddButton(string _name, UIComponent _component, ControllerButtonType _type, KeyCode _keyCode = 0)
	{
		Controller.ControllerButton controllerButton = default(Controller.ControllerButton);
		controllerButton.component = _component;
		controllerButton.type = _type;
		controllerButton.dir = Vector2.zero;
		controllerButton.keyCode = _keyCode;
		Controller.m_buttons.Add(_name, controllerButton);
	}

	// Token: 0x06000333 RID: 819 RVA: 0x000302F7 File Offset: 0x0002E6F7
	public virtual void DisableController()
	{
		Controller.m_controllerDisabled = true;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x000302FF File Offset: 0x0002E6FF
	public virtual void EnableController()
	{
		Controller.m_controllerDisabled = false;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00030307 File Offset: 0x0002E707
	public static void RemoveButton(string _name)
	{
		Controller.m_buttons.Remove(_name);
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00030314 File Offset: 0x0002E714
	public static void RemoveAllButtons()
	{
		Controller.m_buttons.Clear();
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00030320 File Offset: 0x0002E720
	public static bool GetAnyButton()
	{
		if (Controller.m_controllerDisabled)
		{
			return false;
		}
		IDictionaryEnumerator enumerator = Controller.m_buttons.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				Controller.ControllerButton controllerButton = (Controller.ControllerButton)Controller.m_buttons[dictionaryEntry.Key];
				if (controllerButton.component.m_isDown || Input.GetKey(controllerButton.keyCode))
				{
					return true;
				}
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
		return false;
	}

	// Token: 0x06000338 RID: 824 RVA: 0x000303D0 File Offset: 0x0002E7D0
	public static bool GetAnyButton(List<string> _ignoreList)
	{
		if (Controller.m_controllerDisabled)
		{
			return false;
		}
		IDictionaryEnumerator enumerator = Controller.m_buttons.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				Controller.ControllerButton controllerButton = (Controller.ControllerButton)Controller.m_buttons[dictionaryEntry.Key];
				if ((controllerButton.component.m_isDown || Input.GetKey(controllerButton.keyCode)) && !_ignoreList.Contains((string)dictionaryEntry.Key))
				{
					return true;
				}
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
		return false;
	}

	// Token: 0x06000339 RID: 825 RVA: 0x00030498 File Offset: 0x0002E898
	public static ControllerButtonState GetButtonState(string _name)
	{
		if (!Controller.m_buttons.ContainsKey(_name) || Controller.m_controllerDisabled)
		{
			return ControllerButtonState.OFF;
		}
		Controller.ControllerButton controllerButton = (Controller.ControllerButton)Controller.m_buttons[_name];
		if (controllerButton.component.m_isDown || Input.GetKey(controllerButton.keyCode))
		{
			return ControllerButtonState.ON;
		}
		return ControllerButtonState.OFF;
	}

	// Token: 0x0600033A RID: 826 RVA: 0x000304F8 File Offset: 0x0002E8F8
	public static UIComponent GetButton(string _name)
	{
		if (!Controller.m_buttons.ContainsKey(_name) || Controller.m_controllerDisabled)
		{
			return null;
		}
		return ((Controller.ControllerButton)Controller.m_buttons[_name]).component;
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0003053C File Offset: 0x0002E93C
	public static Vector2 GetControllerJoystickDir(string _name)
	{
		if (!Controller.m_buttons.ContainsKey(_name))
		{
			return Vector2.zero;
		}
		Controller.ControllerButton controllerButton = (Controller.ControllerButton)Controller.m_buttons[_name];
		if (controllerButton.type == ControllerButtonType.JOYSTICK)
		{
			return controllerButton.dir;
		}
		return Vector2.zero;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0003058C File Offset: 0x0002E98C
	public virtual void Open()
	{
		this.m_baseTouch = new UICanvas(null, true, string.Empty, null, string.Empty);
		this.m_baseTouch.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_baseTouch.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_baseTouch.SetDepthOffset(100f);
		this.m_baseTouch.RemoveDrawHandler();
		this.m_baseTouch.m_TAC.m_letTouchesThrough = true;
		TouchAreaS.SwitchAllTouchesToTouchArea(this.m_baseTouch.m_TAC);
		this.m_baseTouch.Update();
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0003061C File Offset: 0x0002EA1C
	public virtual void Close()
	{
		if (this.m_baseTouch != null)
		{
			this.m_baseTouch.Destroy();
		}
		this.m_baseTouch = null;
		IDictionaryEnumerator enumerator = Controller.m_buttons.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				UIComponent component = ((Controller.ControllerButton)Controller.m_buttons[dictionaryEntry.Key]).component;
				List<TLTouch> touchesOnTAC = TouchAreaS.GetTouchesOnTAC(component.m_TAC);
				for (int i = 0; i < touchesOnTAC.Count; i++)
				{
					touchesOnTAC[i].Reset();
				}
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

	// Token: 0x0600033E RID: 830 RVA: 0x000306F0 File Offset: 0x0002EAF0
	public virtual void DisabletouchAreas()
	{
	}

	// Token: 0x04000429 RID: 1065
	private static Hashtable m_buttons = new Hashtable();

	// Token: 0x0400042A RID: 1066
	public PsUIBoosterButton m_boostButton;

	// Token: 0x0400042B RID: 1067
	protected bool m_open;

	// Token: 0x0400042C RID: 1068
	public static bool m_controllerDisabled;

	// Token: 0x0400042D RID: 1069
	private UICanvas m_baseTouch;

	// Token: 0x0200009C RID: 156
	private struct ControllerButton
	{
		// Token: 0x0400042E RID: 1070
		public ControllerButtonType type;

		// Token: 0x0400042F RID: 1071
		public UIComponent component;

		// Token: 0x04000430 RID: 1072
		public Vector2 dir;

		// Token: 0x04000431 RID: 1073
		public KeyCode keyCode;
	}
}
