#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace BreakInfinity
{
    [CustomPropertyDrawer(typeof(BigDouble))]
    public class BigDoubleDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            const float gapWidth = 18;
            var mantissaRect = new Rect(position.x, position.y, (position.width - gapWidth) * 0.66f, position.height);
            var gapRect = new Rect(mantissaRect.x + mantissaRect.width, position.y, gapWidth, position.height);
            var exponentRect = new Rect(gapRect.x + gapRect.width, position.y, (position.width - gapWidth) * 0.33f, position.height);

            var mantissaProperty = property.FindPropertyRelative("mantissa");
            var exponentProperty = property.FindPropertyRelative("exponent");
            
            EditorGUI.BeginChangeCheck();
            var mantissa = EditorGUI.DoubleField(mantissaRect, mantissaProperty.doubleValue, new GUIStyle(EditorStyles.numberField)
            {
                alignment = TextAnchor.MiddleRight
            });

            GUIStyle _s = new GUIStyle(EditorStyles.label);
            _s.normal.textColor = Color.white;
            _s.alignment = TextAnchor.MiddleCenter;

            EditorGUI.LabelField(gapRect, "E", _s);
            var exponent = EditorGUI.LongField(exponentRect, exponentProperty.longValue);

            if (EditorGUI.EndChangeCheck())
            {
                var normalized = BigDouble.Normalize(mantissa, exponent);
                mantissaProperty.doubleValue = normalized.Mantissa;
                exponentProperty.longValue = normalized.Exponent;
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
#endif