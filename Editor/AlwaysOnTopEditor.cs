#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Komutil.AlwaysOnTop.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class AlwaysOnTopEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            EditorApplication.delayCall += DelayComponentReorder;
        }

        private void OnDisable()
        {
            EditorApplication.delayCall -= DelayComponentReorder;
        }

        private void DelayComponentReorder()
        {
            EditorApplication.delayCall -= DelayComponentReorder;
            
            if (target is IAlwaysOnTop alwaysOnTopComponent)
            {
                var components = (alwaysOnTopComponent as MonoBehaviour).GetComponents<Component>();

                if (components.Length > 1 && components[1].GetType() != alwaysOnTopComponent.GetType())
                    for (int i = 0; i < components.Length - 1; i++)
                        UnityEditorInternal.ComponentUtility.MoveComponentUp(alwaysOnTopComponent as Component);
            }
        }
    }
}
#endif