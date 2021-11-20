﻿using System.Collections.Concurrent;
using MyNUnit;

namespace Test3
{
    public class Test3
    {
        /// <summary>
        /// String to be sure in order of invokation
        /// </summary>
        public static bool[] array = new bool[5];

        /// <summary>
        /// Adds one symbol to the test string
        /// </summary>
        [MyTest]
        public void MainMethod()
        {
            array[0] = true;
        }

        /// <summary>
        /// Testing Before mark
        /// </summary>
        [Before]
        public void BeforeMethod()
        {
            array[1] = true;
        }

        
        /// <summary>
        /// Second Before Method to chack that we run all of them if there are some
        /// </summary>
        [Before]
        public void BeforeMethod2()
        {
            array[2] = true;
        }

        /// <summary>
        /// After mark tested
        /// </summary>
        [After]
        public void AfterMethod()
        {
            array[3] = true;
        }
        
        /// <summary>
        /// Second After Method to chack that we run all of them if there are some
        /// </summary>
        [After]
        public void AfterMethod2()
        {
            array[4] = true;
        }
    }
}
