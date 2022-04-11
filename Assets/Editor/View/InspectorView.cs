using UnityEditor;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
	public new class UxmlFactory :  UxmlFactory<InspectorView, VisualElement.UxmlTraits>  { }

	private Editor _editor;
	
	public InspectorView()
	{
		
	}

	public void UpdateSelection(NodeView nodeView)
	{
		Clear();
		
		UnityEngine.Object.DestroyImmediate(_editor);
		_editor = Editor.CreateEditor(nodeView.Node);
		var container = new IMGUIContainer(() =>
		{
			if  (!_editor.target) return;
			_editor.OnInspectorGUI();
		});
		Add(container);
	}
}
