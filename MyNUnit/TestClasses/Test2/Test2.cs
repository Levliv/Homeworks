﻿namespace Test2;

using MyAttributes;

public class Test2
{
    public static bool[] checker { get; set; } = { false, false, false };

    public static void BeforeClassMethod()
    {
        checker[0] = true;
    }

    [MyTest]
    public static void MyTestMethod()
    {
        checker[1] = true;
    }

    public static void AfterClassMethod()
    {
        checker[2] = true;
    }
}