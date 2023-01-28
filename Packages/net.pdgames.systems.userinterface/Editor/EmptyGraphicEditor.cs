using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace PDGames.UserInterface
{
    [CanEditMultipleObjects, CustomEditor(typeof(EmptyGraphic), false)]
    public sealed class EmptyGraphicEditor : GraphicEditor
    {
        public override void OnInspectorGUI()
        {
            base.serializedObject.Update();
            EditorGUILayout.PropertyField(base.m_Script, new GUILayoutOption[0]);
            base.RaycastControlsGUI();
            base.serializedObject.ApplyModifiedProperties();
        }
    }
}