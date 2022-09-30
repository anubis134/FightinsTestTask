using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class Opponent
{
    [SerializeField]
    internal Fighter OpponentFighter { get; set; }
    [SerializeField]
    internal bool IsExist => OpponentFighter != null;
}
