using System;
using UnityEngine;

public class SelectImplementationAttribute : PropertyAttribute
{
    #region Fields

    public Type FieldType;
    public bool ShowNamespace;
    public bool ShowParentClass;

    #endregion

    #region Class lifecycle

    public SelectImplementationAttribute(Type fieldType, bool showNamespace = false, bool showParentClass = false)
    {
        FieldType = fieldType;
        ShowNamespace = showNamespace;
        ShowParentClass = showParentClass;
    }

    #endregion
}