using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStates
{
    public static readonly Goto Goto = new Goto();
    public static readonly Target Target = new Target();
    public static readonly Idle Idle = new Idle();
}
