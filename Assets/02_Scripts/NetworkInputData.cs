using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON1 = 0 * 01;
    public const byte MOUSEBUTTON2 = 0 * 02;

    public byte buttons;
    public Vector3 direction;
}
