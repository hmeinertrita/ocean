﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    public abstract void StartTransition();
    public abstract void EndTransition();
}
