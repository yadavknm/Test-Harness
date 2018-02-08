/////////////////////////////////////////////////////////////////////
// CodeToTest2.cs - define code to be tested                       //
//                                                                 //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2016 //
// ver 2.0 - Yadav Narayana Murthy, SUID: 990783888                //
/////////////////////////////////////////////////////////////////////
/*
 *   Build Process
 *   -------------
 *   - Required files:   TestDriver2.cs, CodeToTest2.cs
 * 
 *   Maintenance History
 *   -------------------
 *   ver 1.0 : 22 October 2013
 *     - first release
 *   ver 2.0 : 11 October 2016
 *      - by Yadav Narayana Murthy, SUID: 990783888, Syracuse University
 *      - modifications to add() function to suit the Project 2.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness_proj2
{
    public class CodeToTest2
    {
        public bool add(int num1, int num2)
        {
            Console.Write("\n  Test 2 being tested..\n");
            //Console.WriteLine("testing exception in test 1");
            int a = num1;
            int b = num2;
            Console.WriteLine("  Adding {0} and {1}", a, b);
            Console.Write("  Result:");
            Console.Write(a + b);

            return true;
        }

#if (TEST_CODE2)
        static void Main(string[] args)
        {
            CodeToTest2 ctt = new CodeToTest2();
            ctt.add(5,5);
            Console.Write("\n\n");
        }
#endif

    }
}
