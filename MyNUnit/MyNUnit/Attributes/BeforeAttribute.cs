﻿namespace MyAttributes;

using System;

/// <summary>
/// Method with this label will invoke before each test
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class BeforeAttribute : MyTestAttribute
{
}