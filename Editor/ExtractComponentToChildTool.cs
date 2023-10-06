
// Copied from https://www.youtube.com/watch?v=qDoevls1wmI 
// Warped Imagination tutorials

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor; 

using UnityEditorInternal; 


public static class ExtractComponentToChildTool
{
    [MenuItem("CONTEXT/Component/Extract", priority = 504)]
    public static void ExtractMenuOption(MenuCommand command) {
        Component sourceComponent = command.context as Component; 

        int undoGroupIndex = Undo.GetCurrentGroup(); 
        Undo.IncrementCurrentGroup(); 

        GameObject gameObject = new (sourceComponent.GetType().Name);
            gameObject.transform.SetParent(sourceComponent.transform); 
            gameObject.transform.localScale = Vector3.one; 
            gameObject.transform.localPosition = Vector3.zero; 
            gameObject.transform.localRotation = Quaternion.identity; 


        Undo.RegisterCreatedObjectUndo(gameObject, "Created child object");

        if(!ComponentUtility.CopyComponent(sourceComponent) ||
            !ComponentUtility.PasteComponentAsNew(gameObject)) {
            Debug.LogError("Cannot Extract Component", sourceComponent.gameObject);
           
            Undo.CollapseUndoOperations(undoGroupIndex); 
            Undo.PerformUndo(); 
            return; 
        } 

        Undo.DestroyObjectImmediate(sourceComponent); 

        Undo.CollapseUndoOperations(undoGroupIndex); 
    }

    [MenuItem("CONTEXT/Component/Unextract", priority = 505)]
    public static void UnextractMenuOption(MenuCommand command) {
        Component sourceComponent = command.context as Component; 

        if (sourceComponent.transform.parent == null) {
            Debug.LogError("No parent to unextract to"); 
            return; 
        }

        int undoGroupIndex = Undo.GetCurrentGroup(); 
        Undo.IncrementCurrentGroup(); 

        if(!ComponentUtility.CopyComponent(sourceComponent) ||
            !ComponentUtility.PasteComponentAsNew(sourceComponent.transform.parent.gameObject)) {
            Debug.LogError("Cannot Unextract Component", sourceComponent.gameObject);
           
            Undo.CollapseUndoOperations(undoGroupIndex); 
            Undo.PerformUndo(); 
            return; 
        } 

        Undo.DestroyObjectImmediate(sourceComponent);         
        Undo.CollapseUndoOperations(undoGroupIndex); 


    }

}
