/////////////////////////////////////////////////////////////////////
// CodeToTest1.cs - define code to be tested                       //
//                                                                 //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2016 //
// ver 2.0 - Yadav Narayana Murthy, SUID: 990783888                //
/////////////////////////////////////////////////////////////////////
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
 *      - modifications to divide() function to suit the Project 2.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness_proj2
{
    public class CodeToTest1
    {
        public bool divide(int num1, int num2)
        {
            Console.Write("\n  Test 1 being tested..\n");
            int a = num1;
            int b = num2;
            Console.WriteLine("  Dividing {0} by {1}", a, b);
                        
            Console.WriteLine(a/b);

            return true;
        }

#if (TEST_CODE1)
        static void Main(string[] args)
        {
            CodeToTest1 ctt = new CodeToTest1();
            ctt.divide(2, 1);
            Console.Write("\n\n");
        }
#endif
    }
}
