﻿/////////////////////////////////////////////////////////////////////
// TestDriver1.cs - define a test to run                           //
//                                                                 //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2016 //
// ver 2.0 - Yadav Narayana Murthy, SUID: 990783888                //
/////////////////////////////////////////////////////////////////////
/*
*   Test driver needs to know the types and their interfaces
*   used by the code it will test.  It doesn't need to know
*   anything about the test harness.
*/
/*
 *   Build Process
 *   -------------
 *   - Required files:   TestDriver1.cs, CodeToTest1.cs
 * 
 *   Maintenance History
 *   -------------------
 *   ver 1.0 : 22 October 2013
 *     - first release
 *   ver 2.0 : 11 October 2016
 *      - by Yadav Narayana Murthy, SUID: 990783888, Syracuse University
 *      - modifications to test() function to suit the Project 2.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness_proj2
{
    
    public class TestDriver1 : ITest
    {
        private CodeToTest1 code;  // will be compiled into separate DLL

        //----< Testdriver constructor >---------------------------------
        /*
        *  For production code the test driver may need the tested code
        *  to provide a creational function.
        */
        public TestDriver1()
        {
            code = new CodeToTest1();
        }
        //----< factory function >---------------------------------------
        /*
        *   This can't be used by any code that doesn't know the name
        *   of this class.  That means the TestHarness will need to
        *   use reflection - ugh!
        *
        *   The language gives us this problem because it won't
        *   allow a static method in an interface or abstract class.
        */
        public static ITest create()
        {
            return new TestDriver1();
        }
        //----< test method is where all the testing gets done >---------

        public bool test()
        {
       
            int num1 = 10;
            int num2 = 0;
            return code.divide(num1, num2); 
        }


        //----< test stub - not run in test harness >--------------------
#if (TEST_TD1)
        static void Main(string[] args)
        {
            Console.Write("\n  Local test:\n");

            ITest test = TestDriver1.create();

            if (test.test() == true)
                Console.Write("\n  test passed");
            else
                Console.Write("\n  test failed");
            Console.Write("\n\n");
        }
#endif
    }
}
