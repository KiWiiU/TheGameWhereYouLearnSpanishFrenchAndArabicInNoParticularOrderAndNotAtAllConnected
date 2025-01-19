using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MultipleColorPicker
{
    public class MultipleColorPickerWindow : EditorWindow
    {
        public class PropertyInfo
        {
            public int InstanceID { get; set; }
            public string TargetName { get; set; }
            public SerializedProperty Property { get; set; }

            public PropertyInfo(int instanceID, string targetName, SerializedProperty property)
            {
                InstanceID = instanceID;
                TargetName = targetName;
                Property = property;
            }
        }

        public class PropertyContainer
        {
            public int PopupIndex { get; set; }
            List<PropertyInfo> propertyInfo = new List<PropertyInfo>();

            public void AddProperty(int instanceID, string targetName, SerializedProperty property)
            {
                propertyInfo.Add(new PropertyInfo(instanceID, targetName, property));
            }

            public void ClearProperty()
            {
                propertyInfo.Clear();
            }

            public PropertyInfo GetProperty()
            {
                return GetProperty(PopupIndex);
            }

            public PropertyInfo GetProperty(int index)
            {
                return propertyInfo[index];
            }

            public int GetPropertyCount()
            {
                return propertyInfo.Count();
            }

            public List<PropertyInfo> GetPropertyList()
            {
                return propertyInfo;
            }
        }

        static List<MultipleColorPickerWindow> windows = new List<MultipleColorPickerWindow>();

        PropertyContainer propertyContainer = new PropertyContainer();
        MultipleColorPicker colorPicker;
        bool isLock = false;

        [MenuItem("Tools/Multiple Color Picker")]
        static void Open()
        {
            MultipleColorPickerWindow window = CreateInstance<MultipleColorPickerWindow>();
            Undo.undoRedoPerformed += () =>
            {
                foreach (MultipleColorPickerWindow w in windows)
                {
                    w.Repaint();
                }
            };
            window.ShowUtility();
        }

        void Awake()
        {
            titleContent = new GUIContent("Color");
            minSize = maxSize = new Vector2(232f, 480f);
            wantsMouseMove = true;

            updateProperty(propertyContainer, Selection.gameObjects);
            if (propertyContainer.GetPropertyCount() > 0)
            {
                colorPicker = new MultipleColorPicker();
                colorPicker.Color = propertyContainer.GetProperty().Property.colorValue;
                colorPicker.OriginalColor = propertyContainer.GetProperty().Property.colorValue;
                colorPicker.OnColorChanged = color => applyColorProperty(propertyContainer.GetProperty().Property, color);
            }
            windows.Add(this);
        }

        void OnDestroy()
        {
            windows.Remove(this);
        }

        void OnGUI()
        {
            Event ev = Event.current;

            GUILayout.Space(10f);

            // Lock
            {
                int lastpopupIndex = propertyContainer.PopupIndex;
                int lastInstanceID = 0;
                if (propertyContainer.GetPropertyCount() > 0)
                {
                    lastInstanceID = propertyContainer.GetProperty().InstanceID;
                }

                EditorGUIUtility.labelWidth = 50f;
                EditorGUI.BeginChangeCheck();
                isLock = EditorGUILayout.Toggle("Lock", isLock);
                if (EditorGUI.EndChangeCheck())
                {
                    if (!isLock)
                    {
                        updateProperty(propertyContainer, Selection.gameObjects);
                        if (propertyContainer.GetPropertyCount() > 0)
                        {
                            if (propertyContainer.GetProperty().InstanceID == lastInstanceID)
                            {
                                propertyContainer.PopupIndex = lastpopupIndex;
                            }
                            else
                            {
                                colorPicker.OriginalColor = propertyContainer.GetProperty().Property.colorValue;
                            }
                            colorPicker.Color = propertyContainer.GetProperty().Property.colorValue;
                        }
                        Repaint();
                    }
                }
            }

            // Target
            EditorGUI.BeginDisabledGroup(propertyContainer.GetPropertyCount() == 0);
            {
                EditorGUIUtility.labelWidth = 50f;
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                propertyContainer.PopupIndex = EditorGUILayout.Popup("Target", propertyContainer.PopupIndex, propertyContainer.GetPropertyList().Select(p => p.TargetName).ToArray(), GUILayout.Width(220f));
                if (EditorGUI.EndChangeCheck())
                {
                    colorPicker.Color = propertyContainer.GetProperty().Property.colorValue;
                    colorPicker.OriginalColor = propertyContainer.GetProperty().Property.colorValue;
                    Repaint();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.EndDisabledGroup();

            GUILayout.Space(10f);

            if (propertyContainer.GetPropertyCount() > 0 && colorPicker != null)
            {
                colorPicker.OnGUI(ev);
            }

            if (ev.commandName == "UndoRedoPerformed")
            {
                if (updatePropertyIfNeeded())
                {
                    colorPicker.Color = propertyContainer.GetProperty().Property.colorValue;
                    Repaint();
                }
            }
        }

        void OnSelectionChange()
        {
            if (!isLock)
            {
                updateProperty(propertyContainer, Selection.gameObjects);
                if (propertyContainer.GetPropertyCount() > 0)
                {
                    if (colorPicker == null)
                    {
                        colorPicker = new MultipleColorPicker();
                        colorPicker.OnColorChanged = color => applyColorProperty(propertyContainer.GetProperty().Property, color);
                    }
                    colorPicker.Color = propertyContainer.GetProperty().Property.colorValue;
                    colorPicker.OriginalColor = propertyContainer.GetProperty().Property.colorValue;
                }
                Repaint();
            }
        }

        void OnFocus()
        {
            if (updatePropertyIfNeeded())
            {
                colorPicker.Color = propertyContainer.GetProperty().Property.colorValue;
                Repaint();
            }
        }

        bool isPropertyEqual(PropertyContainer a, PropertyContainer b, int index)
        {
            bool result = false;

            List<PropertyInfo> aList = a.GetPropertyList();
            List<PropertyInfo> bList = b.GetPropertyList();
            if (aList.Count() == bList.Count())
            {
                if (aList.Count() > 0)
                {
                    result = aList[index].Property.colorValue == bList[index].Property.colorValue;
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }

        bool updatePropertyIfNeeded()
        {
            bool result = false;
            int popupIndex = propertyContainer.PopupIndex;

            bool componentRemoved = propertyContainer.GetPropertyList().Any(p => p.Property.serializedObject.targetObject == null);
            bool componentAdded = false;
            bool propertyChanged = false;
            {
                PropertyContainer newContainer = new PropertyContainer();
                updateProperty(newContainer, Selection.gameObjects);
                if (newContainer.GetPropertyCount() > propertyContainer.GetPropertyCount())
                {
                    componentAdded = true;
                }
                else if (newContainer.GetPropertyCount() == propertyContainer.GetPropertyCount())
                {
                    propertyChanged = !isPropertyEqual(newContainer, propertyContainer, popupIndex);
                }
            }

            if (componentRemoved || componentAdded || propertyChanged)
            {
                updateProperty(propertyContainer, Selection.gameObjects);
                if (propertyChanged)
                {
                    propertyContainer.PopupIndex = popupIndex;
                }
                result = true;
            }

            return result;
        }

        void updateProperty(PropertyContainer propertyContainer, GameObject[] gameObjects)
        {
            propertyContainer.PopupIndex = 0;
            propertyContainer.ClearProperty();

            foreach (GameObject go in gameObjects)
            {
                Component[] components = go.GetComponents<Component>();
                foreach (Component component in components)
                {
                    SerializedObject so = new SerializedObject(component);
                    SerializedProperty sp = so.GetIterator();
                    addProperty(propertyContainer, sp, go, component.GetType().Name);
                }
            }
        }

        void addProperty(PropertyContainer propertyContainer, SerializedProperty sp, GameObject go, string componentName)
        {
            List<string> displayNameList = new List<string>();
            int depth = -1;

            while (sp.NextVisible(true))
            {
                if (sp.depth > depth)
                {
                    displayNameList.Add(sp.displayName);
                }
                else if (sp.depth == depth)
                {
                    displayNameList.RemoveAt(displayNameList.Count - 1);
                    displayNameList.Add(sp.displayName);
                }
                else if (sp.depth < depth)
                {
                    int removeCount = depth - sp.depth + 1;
                    displayNameList.RemoveRange(displayNameList.Count - removeCount, removeCount);
                    displayNameList.Add(sp.displayName);
                }
                depth = sp.depth;

                if (sp.propertyType == SerializedPropertyType.Color)
                {
                    string targetName = go.name + " : " + componentName;
                    foreach (string displayName in displayNameList)
                    {
                        targetName = string.Format("{0} -> {1}", targetName, displayName);
                    }
                    propertyContainer.AddProperty(go.GetInstanceID(), targetName, sp.Copy());
                }
            }
        }

        void applyColorProperty(SerializedProperty property, Color color)
        {
            property.serializedObject.Update();
            property.colorValue = color;
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
