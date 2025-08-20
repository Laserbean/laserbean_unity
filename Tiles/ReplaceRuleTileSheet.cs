using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR


[ExecuteInEditMode]

/*
https://forum.unity.com/threads/rule-tile-copy.1182511/
-Just duplicate a a Rule Tile in which you want to replace it's texture.
-Stick this script on a blank object in the scene
-Select the "Rule Tile To Update"
-Select the new texture in "Replace With"
-Check the "Replace" box & it will complete the tedious work automatically.
-Even though this method should do it automatically, uou have to change the default sprite. If you don't all changes
*/


public class ReplaceRuleTileSheet : MonoBehaviour
{
    #region Variables
    
        public RuleTile ruleTileToUpdate;
        public Texture replaceWith; // choose the default sprite & it will replace all of the sprites with the new sheet

        [Header("Tap to Replace")]
        public bool replace = false;
    
    #endregion

    #region Update
    
        private void Update()
        {
        
            if (replace) ReplaceRT();
        
        }
        
    #endregion

    #region Methods
        
        public void ReplaceRT()
        {
            List<RuleTile.TilingRule> trList = ruleTileToUpdate.m_TilingRules; // Load all Tiling Rules from the RuleTile

            // Get the sprites associated with this texture
                string path = AssetDatabase.GetAssetPath(replaceWith); // Get the path of the new texture
                    path = path.Replace("Assets/Resources/",""); // Remove "Resources" path
                    path = path.Replace(".png",""); // Remove file extension
                Sprite[] replaceSpriteList = Resources.LoadAll<Sprite>(path); // Load all sprites from the texture's path

            // Get the sprite number of the default sprite
                int dsn = int.Parse(ruleTileToUpdate.m_DefaultSprite.name.Replace(ruleTileToUpdate.m_DefaultSprite.texture.name + "_", "")); // Default sprite number (remove the name & parse)

            // Get the name & replace with the numbered tile of the "replaceWith" sprite's name. Set the Replaced Default tile to the same number as the original tilesheet
                foreach (RuleTile.TilingRule tr in trList)
                {
                    
                    // Get the sprite number
                        int sn = int.Parse(tr.m_Sprites[0].name.Replace(tr.m_Sprites[0].texture.name + "_", "")); // Sprite Number (remove the name & parse)

                    // Replace with the new sprite
                        tr.m_Sprites[0] = replaceSpriteList[sn]; 

                    // If default sprite, update default
                        if (dsn == sn) ruleTileToUpdate.m_DefaultSprite = replaceSpriteList[sn];

                }

            replace = false;
        }
        
    #endregion
}


#endif