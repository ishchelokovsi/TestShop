using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
public class SelectImplementationDrawer : PropertyDrawer
{
    #region Nested types

    private struct PropertyData
    {
        #region Fields

        public bool isOpen;
        public int implementationTypeIndex;

        #endregion
    }

    #endregion

    #region Fields

    private const float menuHeight = 18;
    private const float openPadding = 10;
    private readonly Dictionary<string, PropertyData> saveData = new();
    private List<Type> _implementations;

    #endregion

    #region Methods

    private static List<Type> GetImplementations(Type interfaceType)
    {
        var resTypes = new List<Type>();
        resTypes.Add(null);
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
        resTypes.AddRange(
            types.Where(p => interfaceType.IsAssignableFrom(p)
                             && !p.IsAbstract
                             && !p.IsSubclassOf(typeof(Object))
                             && Attribute.GetCustomAttribute(p, typeof(SerializableAttribute)) != null)
        );
        return resTypes;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var castedAttribure = attribute as SelectImplementationAttribute;
        EditorGUI.BeginProperty(position, label, property);
        //Menu 
        if (IsOpen(property))
        {
            position.y += openPadding;
            var popupWidth = position.width * (0.75f + 0.125f);
            var buttonWidth = position.width - popupWidth;
            {
                if (_implementations == null)
                {
                    _implementations = GetImplementations(castedAttribure.FieldType);
                }

                if (GUI.Button(new Rect(position.x + popupWidth, position.y, buttonWidth, menuHeight), "Set"))
                {
                    var item = _implementations[GetImplementationTypeIndex(property)];
                    if (item == null)
                    {
                        property.managedReferenceValue = null;
                    }
                    else
                    {
                        property.managedReferenceValue =
                            Activator.CreateInstance(_implementations[GetImplementationTypeIndex(property)]);
                    }

                    SetImplementationTypeIndex(property, 0);
                    SetAsClose(property);
                }
            }
            {
                var _implementationTypeIndex = EditorGUI.Popup(new Rect(position.x, position.y, popupWidth, menuHeight),
                    $"Implementation ({_implementations.Count - 1})",
                    GetImplementationTypeIndex(property),
                    _implementations.Select(impl =>
                        impl == null ? "null" : GetTypeNameLabel(impl.FullName.Replace('+', '/'))).ToArray());
                SetImplementationTypeIndex(property, _implementationTypeIndex);
            }
            position.y += menuHeight;
        }

        {
            const float openButtonWidth = 15;
            // Property
            {
                var typeName = "";
                if (string.IsNullOrEmpty(property.managedReferenceFullTypename))
                {
                    typeName = "null";
                }
                else
                {
                    var spaceIndex = property.managedReferenceFullTypename.IndexOf(" ") + 1;
                    if (spaceIndex != 0)
                    {
                        typeName = GetTypeNameLabel(property.managedReferenceFullTypename.Substring(spaceIndex,
                            property.managedReferenceFullTypename.Length - spaceIndex));
                    }
                    else
                    {
                        typeName = GetTypeNameLabel(property.managedReferenceFullTypename);
                    }
                }

                EditorGUI.PropertyField(
                    new Rect(position.x, position.y, position.width - openButtonWidth,
                        EditorGUI.GetPropertyHeight(property)), property,
                    new GUIContent($"{property.displayName} ({typeName})"), true);
            }
            // Open menu button
            {
                const float openButtonHeight = 18;
                var isOpend =
                    GUI.Toggle(
                        new Rect(position.x + position.width - openButtonWidth, position.y, openButtonWidth,
                            openButtonHeight), IsOpen(property), "i", "Button");
                SetAsOpenCloseState(property, isOpend);
            }
        }
        EditorGUI.EndProperty();

        string GetTypeNameLabel(string type)
        {
            if (!castedAttribure.ShowNamespace)
            {
                var index = type.LastIndexOf('.');
                if (index != -1)
                {
                    type = type.Substring(index + 1);
                }
            }

            if (!castedAttribure.ShowParentClass)
            {
                var index = type.LastIndexOf('/');
                if (index != -1)
                {
                    type = type.Substring(index + 1);
                }
            }

            return type;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = EditorGUI.GetPropertyHeight(property);
        if (IsOpen(property))
        {
            height += menuHeight + openPadding * 2;
        }

        return height;
    }

    private PropertyData GetProppertyData(SerializedProperty prop)
    {
        if (!saveData.ContainsKey(prop.propertyPath))
        {
            saveData[prop.propertyPath] = new PropertyData();
        }

        return saveData[prop.propertyPath];
    }

    private bool IsOpen(SerializedProperty prop)
    {
        return GetProppertyData(prop).isOpen;
    }

    private void SetAsClose(SerializedProperty prop)
    {
        SetAsOpenCloseState(prop, false);
    }

    private void SetAsOpenCloseState(SerializedProperty prop, bool isOpen)
    {
        var data = GetProppertyData(prop);
        data.isOpen = isOpen;
        saveData[prop.propertyPath] = data;
    }

    private int GetImplementationTypeIndex(SerializedProperty prop)
    {
        return GetProppertyData(prop).implementationTypeIndex;
    }

    private void SetImplementationTypeIndex(SerializedProperty prop, int index)
    {
        var data = GetProppertyData(prop);
        data.implementationTypeIndex = index;
        saveData[prop.propertyPath] = data;
    }

    #endregion
}