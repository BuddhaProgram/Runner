using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemControls : BaseInstance<SystemControls> {
    // Getter function of zero
    public float Zero { get; } = 0f;
    // One point float value
    public float One { get; } = 1f;
    // Minimum float value available
    public float Min { get; } = 0.1f;
    // Universal Time
    public float UTime { get; set; } = 1f;
}
