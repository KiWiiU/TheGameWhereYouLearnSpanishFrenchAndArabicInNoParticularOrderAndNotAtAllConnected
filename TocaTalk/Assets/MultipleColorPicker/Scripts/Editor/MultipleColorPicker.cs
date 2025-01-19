using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

namespace MultipleColorPicker
{
    public class MultipleColorPicker
    {
        enum ColorComponent { R, G, B, A, H, S, V }
        enum ColorSpace { RGBByte, RGBFloat, HSV }
        static readonly string[] ColorSpaceLabels = { "RGB 0-255", "RGB 0-1.0", "HSV" };
        static readonly int[] ColorSpaceValues = { 0, 1, 2 };

        public Color Color
        {
            set
            {
                Color.RGBToHSV(value, out hue, out saturation, out brightness);
                alpha = value.a;
            }
        }

        public Color OriginalColor
        {
            set { originalColor = value; }
        }

        public Action<Color> OnColorChanged
        {
            set { onColorChanged = value; }
        }

        GUIContent currentColorContent;
        GUIContent originalColorContent;
        GUIStyle currentColorStyle;
        GUIStyle originalColorStyle;

        GUIContent colorCircleContent;
        GUIStyle colorCircleStyle;
        static readonly int colorCircleHint = 100;
        Texture2D colorCirclePointerTexture;

        GUIContent colorRectContent;
        GUIStyle colorRectStyle;
        static readonly int colorRectHint = 200;
        Texture2D colorRectPointerTexture;

        GUIContent[] colorSliderContents;
        GUIStyle[] colorSliderStyles;
        static readonly int[] colorSliderHint = { 300, 301, 302, 303 };
        Texture2D colorSliderOutlineTexture;
        Texture2D colorSliderCheckerTexture;
        Texture2D colorSliderPointerTexture;

        Color originalColor;
        float hue;
        float saturation;
        float brightness;
        float alpha;
        Action<Color> onColorChanged;
        ColorSpace colorSpace = ColorSpace.RGBByte;

        public MultipleColorPicker()
        {
            string[] guids = AssetDatabase.FindAssets("MultipleColorPicker");
            string rootFolder = AssetDatabase.GUIDToAssetPath(guids[0]);

            {
                Color[] pixels = new Color[38 * 24];
                for (int i = 0; i < pixels.Length; ++i)
                {
                    pixels[i] = Color.white;
                }
                Texture2D currentColorImage = new Texture2D(38, 24);
                currentColorImage.SetPixels(pixels);
                currentColorImage.Apply();
                currentColorContent = new GUIContent(currentColorImage);
                currentColorContent.tooltip = "The new color.";

                Texture2D originalColorImage = new Texture2D(38, 24);
                originalColorImage.SetPixels(pixels);
                originalColorImage.Apply();
                originalColorContent = new GUIContent(originalColorImage);
                originalColorContent.tooltip = "The original color. Click this swatch to reset the color picker to this value.";
            }

            {
                currentColorStyle = new GUIStyle(GUIStyle.none);
                currentColorStyle.margin = new RectOffset(0, 10, 0, 0);
                currentColorStyle.normal.background = AssetDatabase.LoadAssetAtPath<Texture2D>(rootFolder + "/Textures/current_color.png");

                originalColorStyle = new GUIStyle(GUIStyle.none);
                originalColorStyle.normal.background = AssetDatabase.LoadAssetAtPath<Texture2D>(rootFolder + "/Textures/original_color.png");

                colorCircleContent = new GUIContent(AssetDatabase.LoadAssetAtPath<Texture2D>(rootFolder + "/Textures/color_circle.png"));
                colorCircleStyle = new GUIStyle(GUIStyle.none);
                colorCircleStyle.margin = new RectOffset(8, 8, 0, 0);
                colorCirclePointerTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(rootFolder + "/Textures/color_circle_pointer.png");

                colorRectContent = new GUIContent();
                colorRectContent.image = new Texture2D(106, 106);
                colorRectStyle = new GUIStyle(GUIStyle.none);
                colorRectStyle.margin = new RectOffset(63, 0, 55, 0);
                colorRectPointerTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(rootFolder + "/Textures/color_rect_pointer.png");
            }

            {
                Vector2Int sliderSize = new Vector2Int(147, 18);
                int numSliders = 4;
                colorSliderContents = new GUIContent[numSliders];
                colorSliderStyles = new GUIStyle[numSliders];
                for (int i = 0; i < numSliders; ++i)
                {
                    colorSliderContents[i] = new GUIContent();
                    colorSliderContents[i].image = new Texture2D(sliderSize.x - 2, sliderSize.y - 2);
                    colorSliderStyles[i] = new GUIStyle(GUIStyle.none);
                }
                colorSliderCheckerTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(rootFolder + "/Textures/color_slider_checker.png");
                colorSliderPointerTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(rootFolder + "/Textures/color_slider_pointer.png");

                colorSliderOutlineTexture = new Texture2D(sliderSize.x, sliderSize.y);
                Color[] pixels = colorSliderOutlineTexture.GetPixels();
                for (int i = 0; i < pixels.Length; ++i)
                {
                    pixels[i] = new Color(0f, 0f, 0f, 0.8f);
                }
                colorSliderOutlineTexture.SetPixels(pixels);
                colorSliderOutlineTexture.Apply();
            }
        }

        public void OnGUI(Event ev)
        {
            Color prevColor = Color.HSVToRGB(hue, saturation, brightness);
            prevColor.a = alpha;

            onGUISelectedColor(ev);
            GUILayout.Space(10f);
            onGUIColorCircle(ev);
            GUILayout.Space(10f);
            onGUIColorSliders(ev);
            GUILayout.Space(3f);
            onGUIHexField(ev);

            Color newColor = Color.HSVToRGB(hue, saturation, brightness);
            newColor.a = alpha;
            if (newColor != prevColor)
            {
                onColorChanged(newColor);
            }
        }

        void onGUISelectedColor(Event ev)
        {
            Color backgroundColor = GUI.backgroundColor;
            Color contentColor = GUI.contentColor;

            Rect rect = GUILayoutUtility.GetRect(currentColorContent, currentColorStyle);

            // current color
            Vector2 size = currentColorStyle.CalcSize(currentColorContent);
            rect.x = rect.width - size.x;
            rect.width = size.x;
            int controlId = GUIUtility.GetControlID(FocusType.Passive);
            if (ev.type == EventType.Repaint)
            {
                Color color = Color.HSVToRGB(hue, saturation, brightness);
                color.a = alpha;
                GUI.backgroundColor = color.a == 1.0 ? Color.clear : Color.white;
                GUI.contentColor = color;
                currentColorStyle.Draw(rect, currentColorContent, controlId);
            }

            // original color
            size = originalColorStyle.CalcSize(originalColorContent);
            rect.x -= size.x;
            rect.width = size.x;
            GUI.backgroundColor = originalColor.a == 1.0 ? Color.clear : Color.white;
            GUI.contentColor = originalColor;
            if (GUI.Button(rect, originalColorContent, originalColorStyle))
            {
                Color.RGBToHSV(originalColor, out hue, out saturation, out brightness);
                alpha = originalColor.a;
            }

            GUI.backgroundColor = backgroundColor;
            GUI.contentColor = contentColor;
        }

        void onGUIColorCircle(Event ev)
        {
            // color circle
            int controlId = GUIUtility.GetControlID(colorCircleHint, FocusType.Passive);
            Rect rect = GUILayoutUtility.GetRect(colorCircleContent, colorCircleStyle);

            if (ev.type == EventType.Repaint)
            {
                colorCircleStyle.Draw(rect, colorCircleContent, controlId);

                Rect pointerRect = new Rect();
                float angle = hue * 360f * Mathf.Deg2Rad;
                pointerRect.position = rect.center + new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle)).normalized * (colorCircleContent.image.width / 2 - 12);
                pointerRect.x -= colorCirclePointerTexture.width / 2;
                pointerRect.y -= colorCirclePointerTexture.height / 2;
                pointerRect.width = colorCirclePointerTexture.width;
                pointerRect.height = colorCirclePointerTexture.height;
                GUI.DrawTexture(pointerRect, colorCirclePointerTexture);
            }

            switch (ev.type)
            {
                case EventType.MouseDown:
                    float inside = colorCircleContent.image.width / 2 - colorCirclePointerTexture.width;
                    float outside = inside + colorCirclePointerTexture.width;
                    float distance = Vector2.Distance(rect.center, ev.mousePosition);
                    if (inside <= distance && distance <= outside)
                    {
                        float angle = Vector2.Angle(Vector2.right, ev.mousePosition - rect.center);
                        if (ev.mousePosition.y > rect.center.y)
                        {
                            angle = 180 + 180 - angle;
                        }
                        hue = angle / 360f;
                        GUIUtility.hotControl = controlId;
                        GUIUtility.keyboardControl = 0;
                        ev.Use();
                    }
                    break;

                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == controlId)
                    {
                        float angle = Vector2.Angle(Vector2.right, ev.mousePosition - rect.center);
                        if (ev.mousePosition.y > rect.center.y)
                        {
                            angle = 180 + 180 - angle;
                        }
                        hue = angle / 360f;
                        ev.Use();
                    }
                    break;

                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlId)
                    {
                        GUIUtility.hotControl = 0;
                        ev.Use();
                    }
                    break;
            }

            // color rect
            Texture2D colorRectTexture = (Texture2D)colorRectContent.image;
            int width = colorRectTexture.width;
            int height = colorRectTexture.height;
            controlId = GUIUtility.GetControlID(colorRectHint, FocusType.Passive);
            rect.x = rect.x - colorCircleStyle.margin.left + colorRectStyle.margin.left;
            rect.y = rect.y + colorRectStyle.margin.top;
            rect.size = new Vector2(width, height);

            if (ev.type == EventType.Repaint)
            {
                setPixelsColorRect(colorRectTexture, hue);
                colorRectStyle.Draw(rect, colorRectContent, controlId);

                Rect pointerRect = new Rect();
                pointerRect.position = rect.position + new Vector2(saturation * rect.width, (1 - brightness) * rect.height);
                pointerRect.x -= colorRectPointerTexture.width / 2;
                pointerRect.y -= colorRectPointerTexture.height / 2;
                pointerRect.width = colorRectPointerTexture.width;
                pointerRect.height = colorRectPointerTexture.height;
                GUI.DrawTexture(pointerRect, colorRectPointerTexture);
            }

            switch (ev.type)
            {
                case EventType.MouseDown:
                    if (rect.Contains(ev.mousePosition))
                    {
                        saturation = (ev.mousePosition.x - rect.x) / (float)rect.width;
                        brightness = 1 - ((ev.mousePosition.y - rect.y) / (float)rect.height);
                        GUIUtility.hotControl = controlId;
                        GUIUtility.keyboardControl = 0;
                        ev.Use();
                    }
                    break;

                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == controlId)
                    {
                        saturation = Mathf.Clamp01((ev.mousePosition.x - rect.x) / (float)rect.width);
                        brightness = Mathf.Clamp01(1 - ((ev.mousePosition.y - rect.y) / (float)rect.height));
                        ev.Use();
                    }
                    break;

                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlId)
                    {
                        GUIUtility.hotControl = 0;
                        ev.Use();
                    }
                    break;
            }
        }

        void onGUIColorSliders(Event ev)
        {
            // popup
            float width = EditorGUILayout.GetControlRect(true, 1f, EditorStyles.numberField).width;
            EditorGUIUtility.labelWidth = width - 90f;
            EditorGUIUtility.fieldWidth = 65f;
            colorSpace = (ColorSpace)EditorGUILayout.IntPopup(" ", (int)colorSpace, ColorSpaceLabels, ColorSpaceValues, GUILayout.ExpandWidth(false));

            GUILayout.Space(6f);

            // sliders
            switch (colorSpace)
            {
                case ColorSpace.RGBByte:
                case ColorSpace.RGBFloat:
                    colorSlider(ev, "R", colorSpace, 0, ColorComponent.R);
                    GUILayout.Space(6f);
                    colorSlider(ev, "G", colorSpace, 1, ColorComponent.G);
                    GUILayout.Space(6f);
                    colorSlider(ev, "B", colorSpace, 2, ColorComponent.B);
                    GUILayout.Space(6f);
                    colorSlider(ev, "A", colorSpace, 3, ColorComponent.A);
                    break;

                case ColorSpace.HSV:
                    colorSlider(ev, "H", colorSpace, 0, ColorComponent.H);
                    GUILayout.Space(6f);
                    colorSlider(ev, "S", colorSpace, 1, ColorComponent.S);
                    GUILayout.Space(6f);
                    colorSlider(ev, "V", colorSpace, 2, ColorComponent.V);
                    GUILayout.Space(6f);
                    colorSlider(ev, "A", colorSpace, 3, ColorComponent.A);
                    break;
            }
        }

        void onGUIHexField(Event ev)
        {
            float width = EditorGUILayout.GetControlRect(true, 1f, EditorStyles.numberField).width;
            EditorGUIUtility.labelWidth = width - 100f;
            EditorGUIUtility.fieldWidth = 57f;
            Color color = Color.HSVToRGB(hue, saturation, brightness);
            string htmlString = ColorUtility.ToHtmlStringRGB(color);
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(10f);
                EditorGUI.BeginChangeCheck();
                htmlString = EditorGUILayout.TextField("Hexadecimal", htmlString, GUILayout.ExpandWidth(false));
                if (EditorGUI.EndChangeCheck())
                {
                    htmlString = "#" + htmlString;
                    Color outColor;
                    if (ColorUtility.TryParseHtmlString(htmlString, out outColor))
                    {
                        Color.RGBToHSV(outColor, out hue, out saturation, out brightness);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        void colorSlider(Event ev, string label, ColorSpace space, int index, ColorComponent component)
        {
            Rect sliderRect = new Rect();
            Color color = Color.HSVToRGB(hue, saturation, brightness);
            int controlId = GUIUtility.GetControlID(colorSliderHint[index], FocusType.Passive);

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(10f);

                // label
                GUIStyle labelStyle = new GUIStyle(GUIStyle.none);
                labelStyle.normal.textColor = Color.white;
                EditorGUILayout.LabelField(label, labelStyle, GUILayout.Width(12));

                // slider
                sliderRect = GUILayoutUtility.GetRect(colorSliderOutlineTexture.width, colorSliderOutlineTexture.height, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                if (ev.type == EventType.Repaint)
                {
                    GUI.DrawTexture(sliderRect, colorSliderOutlineTexture);

                    switch (component)
                    {
                        case ColorComponent.R: setPixelsSliderRGB((Texture2D)colorSliderContents[index].image, component, hue, saturation, brightness); break;
                        case ColorComponent.G: setPixelsSliderRGB((Texture2D)colorSliderContents[index].image, component, hue, saturation, brightness); break;
                        case ColorComponent.B: setPixelsSliderRGB((Texture2D)colorSliderContents[index].image, component, hue, saturation, brightness); break;
                        case ColorComponent.A: setPixelsSliderA((Texture2D)colorSliderContents[index].image, hue, saturation, brightness); break;
                        case ColorComponent.H: setPixelsSliderH((Texture2D)colorSliderContents[index].image); break;
                        case ColorComponent.S: setPixelsSliderS((Texture2D)colorSliderContents[index].image, hue, brightness); break;
                        case ColorComponent.V: setPixelsSliderV((Texture2D)colorSliderContents[index].image, hue, saturation); break;
                    }

                    if (component == ColorComponent.A)
                    {
                        GUI.DrawTexture(
                            new Rect(sliderRect.x + 1, sliderRect.y + 1, sliderRect.width - 2, sliderRect.height - 2),
                            colorSliderCheckerTexture
                        );
                    }
                    colorSliderStyles[index].Draw(
                        new Rect(sliderRect.x + 1, sliderRect.y + 1, sliderRect.width - 2, sliderRect.height - 2),
                        colorSliderContents[index],
                        controlId
                    );
                }

                Rect pointerRect = new Rect();
                pointerRect.size = new Vector2(colorSliderPointerTexture.width, colorSliderPointerTexture.height);
                color.a = alpha;
                switch (component)
                {
                    case ColorComponent.R: pointerRect.position = new Vector2(color.r * (sliderRect.width - pointerRect.size.x - 2) + sliderRect.x + 1, sliderRect.y + 1); break;
                    case ColorComponent.G: pointerRect.position = new Vector2(color.g * (sliderRect.width - pointerRect.size.x - 2) + sliderRect.x + 1, sliderRect.y + 1); break;
                    case ColorComponent.B: pointerRect.position = new Vector2(color.b * (sliderRect.width - pointerRect.size.x - 2) + sliderRect.x + 1, sliderRect.y + 1); break;
                    case ColorComponent.A: pointerRect.position = new Vector2(color.a * (sliderRect.width - pointerRect.size.x - 2) + sliderRect.x + 1, sliderRect.y + 1); break;
                    case ColorComponent.H: pointerRect.position = new Vector2(hue * (sliderRect.width - pointerRect.size.x - 2) + sliderRect.x + 1, sliderRect.y + 1); break;
                    case ColorComponent.S: pointerRect.position = new Vector2(saturation * (sliderRect.width - pointerRect.size.x - 2) + sliderRect.x + 1, sliderRect.y + 1); break;
                    case ColorComponent.V: pointerRect.position = new Vector2(brightness * (sliderRect.width - pointerRect.size.x - 2) + sliderRect.x + 1, sliderRect.y + 1); break;
                }
                GUI.DrawTexture(pointerRect, colorSliderPointerTexture);

                GUILayout.Space(10f);

                // value
                EditorGUI.BeginChangeCheck();
                {
                    Rect valueRect = new Rect();
                    valueRect.position = new Vector2(sliderRect.position.x + sliderRect.size.x + 6, sliderRect.position.y);
                    valueRect.size = new Vector2(44, sliderRect.size.y);
                    if (space == ColorSpace.RGBByte || space == ColorSpace.HSV)
                    {
                        switch (component)
                        {
                            case ColorComponent.R: color.r = EditorGUI.IntField(valueRect, Mathf.RoundToInt(color.r * 255)) / 255f; break;
                            case ColorComponent.G: color.g = EditorGUI.IntField(valueRect, Mathf.RoundToInt(color.g * 255)) / 255f; break;
                            case ColorComponent.B: color.b = EditorGUI.IntField(valueRect, Mathf.RoundToInt(color.b * 255)) / 255f; break;
                            case ColorComponent.A: color.a = EditorGUI.IntField(valueRect, Mathf.RoundToInt(color.a * 255)) / 255f; break;
                            case ColorComponent.H: hue = EditorGUI.IntField(valueRect, Mathf.RoundToInt(hue * 360)) / 360f; break;
                            case ColorComponent.S: saturation = EditorGUI.IntField(valueRect, Mathf.RoundToInt(saturation * 100)) / 100f; break;
                            case ColorComponent.V: brightness = EditorGUI.IntField(valueRect, Mathf.RoundToInt(brightness * 100)) / 100f; break;
                        }
                    }
                    else if (space == ColorSpace.RGBFloat)
                    {
                        switch (component)
                        {
                            case ColorComponent.R: color.r = EditorGUI.FloatField(valueRect, (int)(color.r * 1000) / 1000f); break;
                            case ColorComponent.G: color.g = EditorGUI.FloatField(valueRect, (int)(color.g * 1000) / 1000f); break;
                            case ColorComponent.B: color.b = EditorGUI.FloatField(valueRect, (int)(color.b * 1000) / 1000f); break;
                            case ColorComponent.A: color.a = EditorGUI.FloatField(valueRect, color.a); break;
                        }
                    }
                    if (EditorGUI.EndChangeCheck())
                    {
                        Color.RGBToHSV(new Color(color.r, color.g, color.b, color.a), out hue, out saturation, out brightness);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            switch (ev.type)
            {
                case EventType.MouseDown:
                    if (sliderRect.Contains(ev.mousePosition))
                    {
                        switch (component)
                        {
                            case ColorComponent.R:
                                float r = (ev.mousePosition.x - sliderRect.x) / sliderRect.width;
                                r = (int)(r * 1000) / 1000f;
                                Color.RGBToHSV(new Color(r, color.g, color.b, color.a), out hue, out saturation, out brightness);
                                break;
                            case ColorComponent.G:
                                float g = (ev.mousePosition.x - sliderRect.x) / sliderRect.width;
                                g = (int)(g * 1000) / 1000f;
                                Color.RGBToHSV(new Color(color.r, g, color.b, color.a), out hue, out saturation, out brightness);
                                break;
                            case ColorComponent.B:
                                float b = (ev.mousePosition.x - sliderRect.x) / sliderRect.width;
                                b = (int)(b * 1000) / 1000f;
                                Color.RGBToHSV(new Color(color.r, color.g, b, color.a), out hue, out saturation, out brightness);
                                break;
                            case ColorComponent.A:
                                alpha = (ev.mousePosition.x - sliderRect.x) / sliderRect.width;
                                break;
                            case ColorComponent.H:
                                hue = (ev.mousePosition.x - sliderRect.x) / sliderRect.width;
                                break;
                            case ColorComponent.S:
                                saturation = (ev.mousePosition.x - sliderRect.x) / sliderRect.width;
                                break;
                            case ColorComponent.V:
                                brightness = (ev.mousePosition.x - sliderRect.x) / sliderRect.width;
                                break;
                        }
                        GUIUtility.hotControl = controlId;
                        GUIUtility.keyboardControl = 0;
                        ev.Use();
                    }
                    break;

                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == controlId)
                    {
                        switch (component)
                        {
                            case ColorComponent.R:
                                float r = Mathf.Clamp01((ev.mousePosition.x - sliderRect.x) / sliderRect.width);
                                r = (int)(r * 1000) / 1000f;
                                Color.RGBToHSV(new Color(r, color.g, color.b, color.a), out hue, out saturation, out brightness);
                                break;
                            case ColorComponent.G:
                                float g = Mathf.Clamp01((ev.mousePosition.x - sliderRect.x) / sliderRect.width);
                                g = (int)(g * 1000) / 1000f;
                                Color.RGBToHSV(new Color(color.r, g, color.b, color.a), out hue, out saturation, out brightness);
                                break;
                            case ColorComponent.B:
                                float b = Mathf.Clamp01((ev.mousePosition.x - sliderRect.x) / sliderRect.width);
                                b = (int)(b * 1000) / 1000f;
                                Color.RGBToHSV(new Color(color.r, color.g, b, color.a), out hue, out saturation, out brightness);
                                break;
                            case ColorComponent.A:
                                alpha = Mathf.Clamp01((ev.mousePosition.x - sliderRect.x) / sliderRect.width);
                                break;
                            case ColorComponent.H:
                                hue = Mathf.Clamp01((ev.mousePosition.x - sliderRect.x) / sliderRect.width);
                                break;
                            case ColorComponent.S:
                                saturation = Mathf.Clamp01((ev.mousePosition.x - sliderRect.x) / sliderRect.width);
                                break;
                            case ColorComponent.V:
                                brightness = Mathf.Clamp01((ev.mousePosition.x - sliderRect.x) / sliderRect.width);
                                break;
                        }
                        ev.Use();
                    }
                    break;

                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlId)
                    {
                        GUIUtility.hotControl = 0;
                        ev.Use();
                    }
                    break;
            }
        }

        void setPixelsColorRect(Texture2D texture, float h)
        {
            Color[] pixels = texture.GetPixels();
            for (int y = 0; y < texture.height; ++y)
            {
                float v = y / (float)texture.height;
                for (int x = 0; x < texture.width; ++x)
                {
                    float s = x / (float)texture.width;
                    pixels[y * texture.height + x] = Color.HSVToRGB(h, s, v);
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }

        void setPixelsSliderRGB(Texture2D texture, ColorComponent component, float h, float s, float v)
        {
            int[] index = { 0, 1, 2, 3 };
            Color[] pixels = texture.GetPixels();
            for (int x = 0; x < texture.width; ++x)
            {
                Color color = Color.HSVToRGB(h, s, v);
                color.a = 1.0f;
                color[index[(int)component]] = x / (float)texture.width;
                for (int y = 0; y < texture.height; ++y)
                {
                    pixels[y * texture.width + x] = color;
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }

        void setPixelsSliderA(Texture2D texture, float h, float s, float v)
        {
            Color color = Color.HSVToRGB(h, s, v);
            Color[] pixels = texture.GetPixels();
            for (int x = 0; x < texture.width; ++x)
            {
                color.a = x / (float)texture.width;
                for (int y = 0; y < texture.height; ++y)
                {
                    pixels[y * texture.width + x] = color;
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }

        void setPixelsSliderH(Texture2D texture)
        {
            Color[] pixels = texture.GetPixels();
            for (int x = 0; x < texture.width; ++x)
            {
                Color color = getHueColor(x / (float)texture.width);
                for (int y = 0; y < texture.height; ++y)
                {
                    pixels[y * texture.width + x] = color;
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }

        void setPixelsSliderS(Texture2D texture, float h, float v)
        {
            if (v <= 0.2f)
            {
                v = 0.2f;
            }
            Color[] pixels = texture.GetPixels();
            for (int x = 0; x < texture.width; ++x)
            {
                Color color = Color.HSVToRGB(h, x / (float)texture.width, v);
                color.a = 1.0f;
                for (int y = 0; y < texture.height; ++y)
                {
                    pixels[y * texture.width + x] = color;
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }

        void setPixelsSliderV(Texture2D texture, float h, float s)
        {
            Color[] pixels = texture.GetPixels();
            for (int x = 0; x < texture.width; ++x)
            {
                Color color = Color.HSVToRGB(h, s, x / (float)texture.width);
                color.a = 1.0f;
                for (int y = 0; y < texture.height; ++y)
                {
                    pixels[y * texture.width + x] = color;
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }

        public static Color getHueColor(float value)
        {
            Color color = new Color();
            color.a = 1.0f;

            if (value < (1.0f / 6.0f))
            {
                color.r = 1.0f;
                color.g = Mathf.Lerp(0.0f, 1.0f, (value - (0.0f / 6.0f)) / (1.0f / 6.0f));
                color.b = 0.0f;
            }
            else
            if (value < (2.0f / 6.0f))
            {
                color.r = Mathf.Lerp(1.0f, 0.0f, (value - (1.0f / 6.0f)) / (1.0f / 6.0f));
                color.g = 1.0f;
                color.b = 0.0f;
            }
            else
            if (value < (3.0f / 6.0f))
            {
                color.r = 0.0f;
                color.g = 1.0f;
                color.b = Mathf.Lerp(0.0f, 1.0f, (value - (2.0f / 6.0f)) / (1.0f / 6.0f));
            }
            else
            if (value < (4.0f / 6.0f))
            {
                color.r = 0.0f;
                color.g = Mathf.Lerp(1.0f, 0.0f, (value - (3.0f / 6.0f)) / (1.0f / 6.0f));
                color.b = 1.0f;
            }
            else
            if (value < (5.0f / 6.0f))
            {
                color.r = Mathf.Lerp(0.0f, 1.0f, (value - (4.0f / 6.0f)) / (1.0f / 6.0f));
                color.g = 0.0f;
                color.b = 1.0f;
            }
            else
            {
                color.r = 1.0f;
                color.g = 0.0f;
                color.b = Mathf.Lerp(1.0f, 0.0f, (value - (5.0f / 6.0f)) / (1.0f / 6.0f));
            }

            return color;
        }
    }
}
