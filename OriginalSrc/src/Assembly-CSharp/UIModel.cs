using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020005B1 RID: 1457
public class UIModel
{
	// Token: 0x06002A89 RID: 10889 RVA: 0x001BA1F8 File Offset: 0x001B85F8
	public UIModel(object _object, Action<string, object, object> _valueChangeDelegate = null)
	{
		this.m_object = _object;
		this.m_valueChangeDelegate = _valueChangeDelegate;
		this.m_objectType = _object.GetType();
		this.m_objectFields = new Dictionary<string, FieldInfo>();
		this.m_boundComponents = new Dictionary<string, List<UIComponent>>();
		BindingFlags bindingFlags = 28;
		foreach (FieldInfo fieldInfo in this.m_objectType.GetFields(bindingFlags))
		{
			if (!this.m_objectFields.ContainsKey(fieldInfo.Name))
			{
				this.m_objectFields.Add(fieldInfo.Name, fieldInfo);
			}
			else
			{
				Debug.LogError("UIModel trying to add an existing field, may cause crash or unwanted behavior. Check your variable names!");
			}
			if (!this.m_boundComponents.ContainsKey(fieldInfo.Name))
			{
				this.m_boundComponents.Add(fieldInfo.Name, new List<UIComponent>());
			}
			else
			{
				Debug.LogError("UIModel trying to add an existing field, may cause crash or unwanted behavior. Check your variable names!");
			}
		}
	}

	// Token: 0x06002A8A RID: 10890 RVA: 0x001BA2D8 File Offset: 0x001B86D8
	public FieldInfo GetFieldInfo(string _fieldName)
	{
		FieldInfo fieldInfo;
		if (this.m_objectFields.TryGetValue(_fieldName, ref fieldInfo))
		{
			return fieldInfo;
		}
		return null;
	}

	// Token: 0x06002A8B RID: 10891 RVA: 0x001BA2FC File Offset: 0x001B86FC
	public object GetValue(UIComponent _uiComponent, bool _registerAsBound = true)
	{
		FieldInfo fieldInfo;
		if (this.m_objectFields.TryGetValue(_uiComponent.m_fieldName, ref fieldInfo))
		{
			List<UIComponent> list;
			this.m_boundComponents.TryGetValue(_uiComponent.m_fieldName, ref list);
			if (_registerAsBound && !list.Contains(_uiComponent))
			{
				list.Add(_uiComponent);
			}
			return fieldInfo.GetValue(this.m_object);
		}
		Debug.LogError(string.Concat(new object[] { "No such property: ", this.m_objectType, ".", _uiComponent.m_fieldName }));
		return null;
	}

	// Token: 0x06002A8C RID: 10892 RVA: 0x001BA390 File Offset: 0x001B8790
	public object GetValue(UIComponent _uiComponent, out Type _fieldType, bool _registerAsBound = true)
	{
		FieldInfo fieldInfo;
		if (this.m_objectFields.TryGetValue(_uiComponent.m_fieldName, ref fieldInfo))
		{
			List<UIComponent> list;
			this.m_boundComponents.TryGetValue(_uiComponent.m_fieldName, ref list);
			if (_registerAsBound && !list.Contains(_uiComponent))
			{
				list.Add(_uiComponent);
			}
			_fieldType = fieldInfo.FieldType;
			return fieldInfo.GetValue(this.m_object);
		}
		_fieldType = null;
		Debug.LogError(string.Concat(new object[] { "No such property: ", this.m_objectType, ".", _uiComponent.m_fieldName }));
		return null;
	}

	// Token: 0x06002A8D RID: 10893 RVA: 0x001BA430 File Offset: 0x001B8830
	public object GetValue(string _fieldName, out Type _fieldType)
	{
		FieldInfo fieldInfo;
		if (this.m_objectFields.TryGetValue(_fieldName, ref fieldInfo))
		{
			List<UIComponent> list;
			this.m_boundComponents.TryGetValue(_fieldName, ref list);
			_fieldType = fieldInfo.FieldType;
			return fieldInfo.GetValue(this.m_object);
		}
		_fieldType = null;
		Debug.LogError(string.Concat(new object[] { "No such property: ", this.m_objectType, ".", _fieldName }));
		return null;
	}

	// Token: 0x06002A8E RID: 10894 RVA: 0x001BA4A8 File Offset: 0x001B88A8
	public object GetValue(string _fieldName)
	{
		FieldInfo fieldInfo;
		if (this.m_objectFields.TryGetValue(_fieldName, ref fieldInfo))
		{
			List<UIComponent> list;
			this.m_boundComponents.TryGetValue(_fieldName, ref list);
			return fieldInfo.GetValue(this.m_object);
		}
		Debug.LogError(string.Concat(new object[] { "No such property: ", this.m_objectType, ".", _fieldName }));
		return null;
	}

	// Token: 0x06002A8F RID: 10895 RVA: 0x001BA514 File Offset: 0x001B8914
	public void SetValue(object _newValue, UIComponent _uiComponent)
	{
		FieldInfo fieldInfo;
		if (this.m_objectFields.TryGetValue(_uiComponent.m_fieldName, ref fieldInfo))
		{
			object value = this.GetValue(_uiComponent, true);
			fieldInfo.SetValue(this.m_object, _newValue);
			List<UIComponent> list;
			this.m_boundComponents.TryGetValue(_uiComponent.m_fieldName, ref list);
			for (int i = 0; i < list.Count; i++)
			{
				UIComponent uicomponent = list[i];
				if (uicomponent != _uiComponent)
				{
					uicomponent.OnValueChange(_newValue, value);
				}
				if (this.m_valueChangeDelegate != null)
				{
					this.m_valueChangeDelegate.Invoke(_uiComponent.m_fieldName, _newValue, value);
				}
			}
		}
		else
		{
			Debug.LogError(string.Concat(new object[] { "No such property: ", this.m_objectType, ".", _uiComponent.m_fieldName }));
		}
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x001BA5EC File Offset: 0x001B89EC
	public void SetValue(object _newValue, string _fieldName)
	{
		FieldInfo fieldInfo;
		if (this.m_objectFields.TryGetValue(_fieldName, ref fieldInfo))
		{
			object value = this.GetValue(_fieldName);
			fieldInfo.SetValue(this.m_object, _newValue);
			List<UIComponent> list;
			this.m_boundComponents.TryGetValue(_fieldName, ref list);
			for (int i = 0; i < list.Count; i++)
			{
				UIComponent uicomponent = list[i];
				uicomponent.OnValueChange(_newValue, value);
				if (this.m_valueChangeDelegate != null)
				{
					this.m_valueChangeDelegate.Invoke(_fieldName, _newValue, value);
				}
			}
		}
		else
		{
			Debug.LogError(string.Concat(new object[] { "No such property: ", this.m_objectType, ".", _fieldName }));
		}
	}

	// Token: 0x06002A91 RID: 10897 RVA: 0x001BA6A4 File Offset: 0x001B8AA4
	public void RemoveBinding(UIComponent _uiComponent)
	{
		List<UIComponent> list;
		this.m_boundComponents.TryGetValue(_uiComponent.m_fieldName, ref list);
		list.Remove(_uiComponent);
	}

	// Token: 0x04002FC0 RID: 12224
	private object m_object;

	// Token: 0x04002FC1 RID: 12225
	private Type m_objectType;

	// Token: 0x04002FC2 RID: 12226
	private Dictionary<string, FieldInfo> m_objectFields;

	// Token: 0x04002FC3 RID: 12227
	private Dictionary<string, List<UIComponent>> m_boundComponents;

	// Token: 0x04002FC4 RID: 12228
	private Action<string, object, object> m_valueChangeDelegate;
}
