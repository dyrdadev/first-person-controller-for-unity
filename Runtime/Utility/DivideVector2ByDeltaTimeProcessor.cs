using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DyrdaDev.FirstPersonController
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class DivideVector2ByDeltaTimeProcessor : InputProcessor<Vector2>
    {
#if UNITY_EDITOR
        static DivideVector2ByDeltaTimeProcessor()
        {
            Initialize();
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            InputSystem.RegisterProcessor<DivideVector2ByDeltaTimeProcessor>();
        }


        /// <summary>
        /// Divides the value by the delta time. This makes the frame rate dependent input frame rate independent.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        public override Vector2 Process(Vector2 value, InputControl control)
        {
            return value / Time.deltaTime;
        }
    }
}