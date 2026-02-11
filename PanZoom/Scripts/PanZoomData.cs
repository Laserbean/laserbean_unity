using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Dossamer.PanZoom
{
    [Serializable]
    public struct AxisData
    {
        public Axis key;
        public Quaternion orientation;
        public bool IsOverriden;
        public bool IsEnabled;
        public bool IsSecondaryInputEnabled;
        public bool DoesSecondaryNeedTrigger;
        public string secondaryInputTrigger;
        public string customInputMethod;
        public string customSecondaryInputMethod;
        public InputMethod inputMethod;
        public InputMethod secondaryInputMethod;
    }

    [Serializable]
    public enum Axis
    {
        RIGHT,
        UP,
        FORWARD
    }

    [Serializable]
    public enum InputMethod
    {
        PointerHorizontal,    // aka mouse, touch
        PointerVertical,
        HorizontalAxis,       // gamepad, keyboard
        VerticalAxis,
        Scrollwheel, // mouse-specific
        CustomAxis, // user-specified
        PinchZoom,
        Null
    }

    [Serializable]
    public enum MouseButton
    {
        Left = 0,
        Right = 1,
        Middle = 2
    }
}
