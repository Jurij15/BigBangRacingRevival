using System;
using System.Collections.Generic;

// Token: 0x020001F2 RID: 498
public static class GizmoManager
{
	// Token: 0x06000EC9 RID: 3785 RVA: 0x0008A408 File Offset: 0x00088808
	public static void Update()
	{
		if (GizmoManager.m_multiGizmo != null)
		{
			GizmoManager.m_multiGizmo.Destroy();
			GizmoManager.m_multiGizmo = null;
		}
		while (GizmoManager.m_parentGizmos.Count > 0)
		{
			int num = GizmoManager.m_parentGizmos.Count - 1;
			GizmoManager.m_parentGizmos[num].Destroy();
			GizmoManager.m_parentGizmos.RemoveAt(num);
		}
		while (GizmoManager.m_childGizmos.Count > 0)
		{
			int num2 = GizmoManager.m_childGizmos.Count - 1;
			GizmoManager.m_childGizmos[num2].Destroy();
			GizmoManager.m_childGizmos.RemoveAt(num2);
		}
		GizmoManager.RemoveVisualGizmos();
		GizmoManager.m_parents.Clear();
		GizmoManager.m_childs.Clear();
		if (GizmoManager.m_selection.Count == 0)
		{
			return;
		}
		for (int i = 0; i < GizmoManager.m_selection.Count; i++)
		{
			GraphElement graphElement = GizmoManager.m_selection[i];
			if (graphElement.m_elementType == GraphElementType.Node)
			{
				GraphNode graphNode = graphElement as GraphNode;
				if (graphNode.m_childElements.Count > 0 || graphNode.m_parentElement.m_elementType == GraphElementType.Graph)
				{
					GizmoManager.m_parents.Add(graphNode);
				}
				else if (graphNode.m_parentElement.m_elementType == GraphElementType.Node)
				{
					GizmoManager.m_childs.Add(graphNode);
				}
			}
			else if (graphElement.m_elementType == GraphElementType.Connection)
			{
				GizmoManager.m_parents.Add(graphElement);
			}
		}
		if (GizmoManager.m_parents.Count > 1)
		{
			GizmoManager.m_multiGizmo = new MultiGizmo(GizmoManager.m_selection);
		}
		else
		{
			for (int j = 0; j < GizmoManager.m_parents.Count; j++)
			{
				List<GraphElement> list = new List<GraphElement>();
				list.Add(GizmoManager.m_parents[j]);
				if (GizmoManager.m_parents[j].m_elementType == GraphElementType.Node)
				{
					if ((GizmoManager.m_parents[j] as GraphNode).m_nodeType == GraphNodeType.Normal)
					{
						GizmoManager.m_parentGizmos.Add(new ParentGizmo(list));
					}
				}
				else if (GizmoManager.m_parents[j].m_elementType == GraphElementType.Connection)
				{
					GizmoManager.m_parentGizmos.Add(new ParentGizmo(list));
				}
			}
			for (int k = 0; k < GizmoManager.m_childs.Count; k++)
			{
				List<GraphElement> list2 = new List<GraphElement>();
				list2.Add(GizmoManager.m_childs[k]);
				if ((GizmoManager.m_childs[k] as GraphNode).m_nodeType == GraphNodeType.Child)
				{
					GizmoManager.m_childGizmos.Add(new ChildGizmo(list2));
				}
				else if ((GizmoManager.m_childs[k] as GraphNode).m_nodeType == GraphNodeType.IndependentChild)
				{
					GizmoManager.m_childGizmos.Add(new IndependentChildGizmo(list2));
				}
			}
		}
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0008A6E0 File Offset: 0x00088AE0
	public static List<GraphElement> GetParents(List<GraphElement> _graphElements)
	{
		List<GraphElement> list = new List<GraphElement>();
		for (int i = 0; i < _graphElements.Count; i++)
		{
			if (_graphElements[i].m_parentElement.m_elementType == GraphElementType.Node)
			{
				if (!list.Contains(_graphElements[i].m_parentElement))
				{
					list.Add(_graphElements[i].m_parentElement);
				}
			}
			else if (!list.Contains(_graphElements[i]))
			{
				list.Add(_graphElements[i]);
			}
		}
		return list;
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0008A770 File Offset: 0x00088B70
	public static void AddToSelection(List<GraphElement> _graphElements)
	{
		for (int i = 0; i < _graphElements.Count; i++)
		{
			GizmoManager.AddToSelection(_graphElements[i]);
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0008A7A0 File Offset: 0x00088BA0
	public static void AddToSelection(GraphElement _graphElement)
	{
		if (!_graphElement.m_isSelectable)
		{
			return;
		}
		if (_graphElement.m_parentElement.m_elementType == GraphElementType.Node)
		{
			GizmoManager.AddToSelection(_graphElement.m_parentElement);
		}
		else
		{
			if (!GizmoManager.m_selection.Contains(_graphElement))
			{
				GizmoManager.m_selection.Add(_graphElement);
				_graphElement.Select(true);
			}
			if (_graphElement.m_elementType == GraphElementType.Node)
			{
				GraphNode graphNode = _graphElement as GraphNode;
				for (int i = 0; i < graphNode.m_childElements.Count; i++)
				{
					if (!GizmoManager.m_selection.Contains(graphNode.m_childElements[i]))
					{
						GizmoManager.m_selection.Add(graphNode.m_childElements[i]);
						graphNode.m_childElements[i].Select(true);
					}
				}
			}
		}
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0008A870 File Offset: 0x00088C70
	public static void RemoveFromSelection(GraphElement _graphElement)
	{
		if (!_graphElement.m_isSelectable)
		{
			return;
		}
		if (_graphElement.m_parentElement.m_elementType == GraphElementType.Node)
		{
			GizmoManager.RemoveFromSelection(_graphElement.m_parentElement);
		}
		else
		{
			if (_graphElement.m_elementType == GraphElementType.Node)
			{
				GraphNode graphNode = _graphElement as GraphNode;
				for (int i = 0; i < graphNode.m_childElements.Count; i++)
				{
					if (graphNode.m_childElements[i].m_selected)
					{
						GizmoManager.m_selection.Remove(graphNode.m_childElements[i]);
						graphNode.m_childElements[i].Select(false);
					}
				}
			}
			if (_graphElement.m_selected)
			{
				GizmoManager.m_selection.Remove(_graphElement);
				_graphElement.Select(false);
			}
		}
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x0008A936 File Offset: 0x00088D36
	public static void SetSelection(List<GraphElement> _newSelection)
	{
		GizmoManager.ClearSelection();
		GizmoManager.AddToSelection(_newSelection);
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x0008A943 File Offset: 0x00088D43
	public static List<GraphElement> GetSelection()
	{
		return GizmoManager.m_selection;
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x0008A94C File Offset: 0x00088D4C
	public static void ClearSelection()
	{
		for (int i = 0; i < GizmoManager.m_selection.Count; i++)
		{
			GizmoManager.m_selection[i].Select(false);
		}
		GizmoManager.m_selection.Clear();
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0008A990 File Offset: 0x00088D90
	public static void UpdatePosition()
	{
		if (GizmoManager.m_multiGizmo != null)
		{
			GizmoManager.m_multiGizmo.UpdatePosition();
		}
		for (int i = 0; i < GizmoManager.m_parentGizmos.Count; i++)
		{
			GizmoManager.m_parentGizmos[i].UpdatePosition();
		}
		for (int j = 0; j < GizmoManager.m_childGizmos.Count; j++)
		{
			GizmoManager.m_childGizmos[j].UpdatePosition();
		}
		for (int k = 0; k < GizmoManager.m_visualGizmos.Count; k++)
		{
			GizmoManager.m_visualGizmos[k].UpdatePosition();
		}
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0008AA34 File Offset: 0x00088E34
	public static void RemoveVisualGizmos()
	{
		while (GizmoManager.m_visualGizmos.Count > 0)
		{
			int num = GizmoManager.m_visualGizmos.Count - 1;
			GizmoManager.m_visualGizmos[num].Destroy();
			GizmoManager.m_visualGizmos.RemoveAt(num);
		}
	}

	// Token: 0x040011B9 RID: 4537
	public static List<GraphElement> m_selection = new List<GraphElement>();

	// Token: 0x040011BA RID: 4538
	public static List<GraphElement> m_parents = new List<GraphElement>();

	// Token: 0x040011BB RID: 4539
	public static List<GraphElement> m_childs = new List<GraphElement>();

	// Token: 0x040011BC RID: 4540
	public static MultiGizmo m_multiGizmo;

	// Token: 0x040011BD RID: 4541
	public static List<ParentGizmo> m_parentGizmos = new List<ParentGizmo>();

	// Token: 0x040011BE RID: 4542
	public static List<BasicGizmo> m_childGizmos = new List<BasicGizmo>();

	// Token: 0x040011BF RID: 4543
	public static List<BasicGizmo> m_visualGizmos = new List<BasicGizmo>();
}
