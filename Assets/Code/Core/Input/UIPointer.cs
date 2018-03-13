using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UIPointer {

    /// <summary>
    /// The pointer info
    /// </summary>
    public PointerInfo pointer;

    /// <summary>
    /// The ray for this pointer
    /// </summary>
    public Ray ray;

    /// <summary>
    /// The raycast hit object into the 3D scene
    /// </summary>
    public RaycastHit? raycast;

    /// <summary>
    /// True if this pointer started over a UI element or anything the event system catches
    /// </summary>
    public bool overUI;
}
