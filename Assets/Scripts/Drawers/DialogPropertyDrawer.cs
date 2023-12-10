using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Dialog))]
[CanEditMultipleObjects]
public class DialogPropertyDrawer : PropertyDrawer
{
    float baseSpace = EditorGUIUtility.standardVerticalSpacing * 2 + EditorGUIUtility.singleLineHeight * 3;
    float spacePerElementItemList = 60;
    float spacePerElementGiftablePokemonList = 260;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var titleFieldPosition = new Rect(position);
        titleFieldPosition.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.BeginProperty(titleFieldPosition, label, property.FindPropertyRelative("dialogTitle"));
        EditorGUI.PropertyField(titleFieldPosition, property.FindPropertyRelative("dialogTitle"));
        EditorGUI.EndProperty();

        Rect textFieldPosition = new Rect(position);
        textFieldPosition.height = EditorGUIUtility.singleLineHeight * 4;
        textFieldPosition.y = titleFieldPosition.yMax + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.BeginProperty(textFieldPosition, label, property.FindPropertyRelative("Text"));
        EditorGUI.PropertyField(textFieldPosition, property.FindPropertyRelative("Text"));
        EditorGUI.EndProperty();

        var isSpecialToggleFieldPosition = new Rect(position);
        isSpecialToggleFieldPosition.height = EditorGUIUtility.singleLineHeight;
        isSpecialToggleFieldPosition.y = textFieldPosition.yMax + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.BeginProperty(isSpecialToggleFieldPosition, label, property.FindPropertyRelative("isSpecial"));
        EditorGUI.PropertyField(isSpecialToggleFieldPosition, property.FindPropertyRelative("isSpecial"));
        EditorGUI.EndProperty();

        if (property.FindPropertyRelative("isSpecial").boolValue == true)
        {
            var giftsItemToggleFieldPosition = new Rect(position);
            giftsItemToggleFieldPosition.height = EditorGUIUtility.singleLineHeight;
            giftsItemToggleFieldPosition.y = isSpecialToggleFieldPosition.yMax + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.BeginProperty(giftsItemToggleFieldPosition, label, property.FindPropertyRelative("giftsItems"));
            EditorGUI.PropertyField(giftsItemToggleFieldPosition, property.FindPropertyRelative("giftsItems"));
            EditorGUI.EndProperty();

            float itemFieldMaxY = giftsItemToggleFieldPosition.yMax;
            if (property.FindPropertyRelative("giftsItems").boolValue == true)
            {
                //Show item configs
                var itemDictionaryFieldPosition = new Rect(position);
                itemDictionaryFieldPosition.height = EditorGUIUtility.singleLineHeight;
                itemDictionaryFieldPosition.y = giftsItemToggleFieldPosition.yMax + EditorGUIUtility.standardVerticalSpacing;
                SerializedProperty itemList = property.FindPropertyRelative("giftableItems");
                EditorGUI.BeginProperty(itemDictionaryFieldPosition, label, itemList);
                EditorGUI.PropertyField(itemDictionaryFieldPosition, itemList);
                EditorGUI.EndProperty();
                if (itemList.isExpanded == false)
                {
                    itemFieldMaxY = itemDictionaryFieldPosition.yMax;
                }
                else
                {
                    if (itemList.arraySize == 0)
                    {
                        itemFieldMaxY += baseSpace;
                    }
                    else
                    {
                        float spacing = spacePerElementItemList * itemList.arraySize + EditorGUIUtility.singleLineHeight * 2;
                        itemFieldMaxY += spacing;
                    }
                }


            }

            var giftsPokemonToggleFieldPosition = new Rect(position);
            giftsPokemonToggleFieldPosition.height = EditorGUIUtility.singleLineHeight;
            giftsPokemonToggleFieldPosition.y = itemFieldMaxY + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.BeginProperty(giftsPokemonToggleFieldPosition, label, property.FindPropertyRelative("giftsPokemon"));
            EditorGUI.PropertyField(giftsPokemonToggleFieldPosition, property.FindPropertyRelative("giftsPokemon"));
            EditorGUI.EndProperty();

            float pokemonFieldMaxY = giftsPokemonToggleFieldPosition.yMax;
            if (property.FindPropertyRelative("giftsPokemon").boolValue == true)
            {
                //Show pokemon configs
                var itemDictionaryFieldPosition = new Rect(position);
                itemDictionaryFieldPosition.height = EditorGUIUtility.singleLineHeight;
                itemDictionaryFieldPosition.y = giftsPokemonToggleFieldPosition.yMax + EditorGUIUtility.standardVerticalSpacing;
                SerializedProperty itemList = property.FindPropertyRelative("giftablePokemon");
                EditorGUI.BeginProperty(itemDictionaryFieldPosition, label, itemList);
                EditorGUI.PropertyField(itemDictionaryFieldPosition, itemList);
                EditorGUI.EndProperty();
                if (itemList.isExpanded == false)
                {
                    pokemonFieldMaxY = itemDictionaryFieldPosition.yMax;
                }
                else
                {
                    if (itemList.arraySize == 0)
                    {
                        pokemonFieldMaxY += baseSpace;
                    }
                    else
                    {
                        float spacing = spacePerElementGiftablePokemonList * itemList.arraySize + EditorGUIUtility.singleLineHeight * 2;
                        pokemonFieldMaxY += spacing;
                    }
                }
            }
        }
    }

    //per indi
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLines = 3 + 4;
        float propertyHeight = 0;
        //giftable items
        if (property.FindPropertyRelative("isSpecial").boolValue == true)
        {
            totalLines += 2;
            SerializedProperty GiftableItemsArrayProperty = property.FindPropertyRelative("giftableItems");
            if (property.FindPropertyRelative("giftsItems").boolValue == true)
            {
                totalLines++;
                if (GiftableItemsArrayProperty.isExpanded == true)
                {
                    if (GiftableItemsArrayProperty.arraySize == 0)
                    {
                        propertyHeight += baseSpace / 1.5f;
                    }
                    else
                    {
                        float spacing = spacePerElementItemList * GiftableItemsArrayProperty.arraySize + EditorGUIUtility.singleLineHeight * 1;
                        propertyHeight += spacing;
                    }
                }
            }

            //giftable pokemon
            SerializedProperty GiftablePokemonArrayProperty = property.FindPropertyRelative("giftablePokemon");
            if (property.FindPropertyRelative("giftsPokemon").boolValue == true)
            {
                totalLines++;
                if (GiftablePokemonArrayProperty.isExpanded == true)
                {
                    if (GiftablePokemonArrayProperty.arraySize == 0)
                    {
                        propertyHeight += baseSpace / 1.5f;
                    }
                    else
                    {
                        float spacing = spacePerElementGiftablePokemonList * GiftablePokemonArrayProperty.arraySize + EditorGUIUtility.singleLineHeight * 1;
                        propertyHeight += spacing;
                    }
                }
            }
        }

        propertyHeight += EditorGUIUtility.singleLineHeight * totalLines + EditorGUIUtility.standardVerticalSpacing * (totalLines - 1);
        return propertyHeight;
    }

}
