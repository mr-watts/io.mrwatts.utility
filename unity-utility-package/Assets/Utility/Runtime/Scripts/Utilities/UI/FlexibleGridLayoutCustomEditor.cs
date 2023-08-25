#if UNITY_EDITOR
using UnityEditor;

namespace MrWatts.Internal.Utilities
{
    [CustomEditor(typeof(FlexibleGridLayout), true)]
    [CanEditMultipleObjects]
    public sealed class FlexibleGridLayoutCustomEditor : Editor
    {
        internal SerializedProperty m_Padding;
        internal SerializedProperty m_Spacing;
        internal SerializedProperty m_StartCorner;
        internal SerializedProperty m_StartAxis;
        internal SerializedProperty m_ChildAlignment;
        internal SerializedProperty _columnCount;
        internal SerializedProperty _rowCount;
        internal SerializedProperty _fixedColumnCellSize;
        internal SerializedProperty _columnCellSize;
        internal SerializedProperty _fixedRowCellSize;
        internal SerializedProperty _rowCellSize;

        private void OnEnable()
        {
            m_Padding = serializedObject.FindProperty("m_Padding");
            m_Spacing = serializedObject.FindProperty("m_Spacing");
            m_StartCorner = serializedObject.FindProperty("m_StartCorner");
            m_StartAxis = serializedObject.FindProperty("m_StartAxis");
            m_ChildAlignment = serializedObject.FindProperty("m_ChildAlignment");
            _columnCount = serializedObject.FindProperty("_columnCount");
            _rowCount = serializedObject.FindProperty("_rowCount");

            _fixedRowCellSize = serializedObject.FindProperty("_fixedRowCellSize");
            _rowCellSize = serializedObject.FindProperty("_rowCellSize");
            _fixedColumnCellSize = serializedObject.FindProperty("_fixedColumnCellSize");
            _columnCellSize = serializedObject.FindProperty("_columnCellSize");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Padding, true);
            EditorGUILayout.PropertyField(m_Spacing, true);
            EditorGUILayout.PropertyField(m_StartCorner, true);
            EditorGUILayout.PropertyField(m_StartAxis, true);
            EditorGUILayout.PropertyField(m_ChildAlignment, true);

            EditorGUILayout.PropertyField(_columnCount, true);
            EditorGUILayout.PropertyField(_rowCount, true);

            EditorGUILayout.PropertyField(_fixedRowCellSize, true);
            EditorGUILayout.PropertyField(_rowCellSize, true);
            EditorGUILayout.PropertyField(_fixedColumnCellSize, true);
            EditorGUILayout.PropertyField(_columnCellSize, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif