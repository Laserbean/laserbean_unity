

using UnityEngine;
using UnityEditor;


public class SpritePreviewAttribute : PropertyAttribute
{
    public int xval;
    public int yval;

    public SpritePreviewAttribute(int xxx = 0, int yyy = 0)
    {
        this.xval = xxx;
        this.yval = yyy;
    }
}

[CustomPropertyDrawer(typeof(SpritePreviewAttribute))]
public class SpritePreviewDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(position, property, label, true);
        position.y += lineHeight; 

        // Check if the property is a reference to a Sprite.
        if (property.propertyType == SerializedPropertyType.ObjectReference &&
            property.objectReferenceValue is Sprite)
        {
            Sprite sprite = property.objectReferenceValue as Sprite;

            var spritePreviewAttribute = (attribute as SpritePreviewAttribute); 


            // Use the width and height values from the attribute to determine the size.
            int xval = spritePreviewAttribute.xval;
            int yval = spritePreviewAttribute.yval;

            if (sprite != null) {
                if (xval == 0 && yval != 0) {
                    float scale = yval/(sprite.rect.yMax - sprite.rect.yMin); 
                    xval = (int)((sprite.rect.xMax - sprite.rect.xMin) *scale);
                }

                if (xval == 0) 
                    xval = (int)(sprite.rect.xMax - sprite.rect.xMin);
                if (yval == 0) 
                    yval = (int)(sprite.rect.yMax - sprite.rect.yMin);            
                
                Rect previewRect = new Rect(position.x, position.y, xval, yval);

                float curinspectorwidth = EditorGUIUtility.currentViewWidth;

                // float xOffset = (previewRect.width - sprite.rect.width) * 0.5f;
                // float yOffset = (previewRect.height - sprite.rect.height) * 0.5f;

                // previewRect.x += xOffset;
                // previewRect.y += yOffset;

                previewRect.x += (curinspectorwidth/2) - (xval/2); 

                EditorGUI.LabelField(previewRect, " ");

                // Calculate the portion of the texture to display based on the sprite's rect.
                Rect textureRect = sprite.rect;
                float textureWidth = sprite.texture.width;
                float textureHeight = sprite.texture.height;

                textureRect.x /= textureWidth;
                textureRect.y /= textureHeight;
                textureRect.width /= textureWidth;
                textureRect.height /= textureHeight;

                // Draw the sliced part of the sprite.
                previewRect.y += EditorGUIUtility.singleLineHeight;
                GUI.DrawTextureWithTexCoords(previewRect, sprite.texture, textureRect);
            


                
                // EditorGUI.DrawPreviewTexture(previewRect, sprite.texture, null, ScaleMode.ScaleToFit);
            }
        }
        else
        {
            // If the property isn't a Sprite reference, display an error message.
            EditorGUI.HelpBox(position, "This attribute can only be used with Sprite references.", MessageType.Error);
        }

        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float totalheight = 0f; 

        totalheight += lineHeight*3; 

        float height = (attribute as SpritePreviewAttribute).yval; 


        if (height == 0) {
            Sprite sprite = property.objectReferenceValue as Sprite;
            height = sprite.rect.height; 
        }

        totalheight += height;
        
        

        return totalheight;
    }
}