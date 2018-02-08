/////////////////////////////////////////////////////////////////////////////////////////
//  ITest.cs -   Defines all the interfaces for the TestHarness             
//  ver 1.0                                                                
//  Language:     C#, VS 2015                                              
//  Platform:     Windows 10               
//  Application:  Project 2 - Test Harness - CSE681 - Software Modeling & Analysis  
//  Author:       Yadav Narayana Murthy, SUID: 990783888, Syracuse University                                 
//////////////////////////////////////////////////////////////////////////////////////////
/*
 *   Module Operations
 *   -----------------
 *   This is the Interface documentation.
 *   
 *   ITest - Each test driver derives from an ITest interface that declares a 
 *   method test() that takes no arguments and returns the test pass status, 
 *   e.g., a boolean true or false value
 *  
 *   ILoadAndExecute - The loadAndExecute derives from ILoadAndExecute interface 
 *   which provides loadAndExec() that takes a string argument and returns void.
 *   
 */
/*
 *   Build Process
 *   -------------
 *   - Required files:   ITest.cs
 * 
 *   Maintenance History
 *   -------------------
 *   ver 1.0 : 10 October 2016
 *     - first release
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestHarness_proj2
{
    public interface ITest
    {
        bool test();
    }
    public interface ILoadAndExecute
    {
        void loadAndExec(String request);
    }
}
