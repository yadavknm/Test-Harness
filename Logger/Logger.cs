/////////////////////////////////////////////////////////////////////////////////////////
//  Logger.cs -   Used to log results of the Tests.               
//  ver 1.0                                                                
//  Language:     C#, VS 2015                                              
//  Platform:     Windows 10               
//  Application:  Project 2 - Test Harness - CSE681 - Software Modeling & Analysis  
//  Author:       Yadav Narayana Murthy, SUID: 990783888, Syracuse University                                 
//////////////////////////////////////////////////////////////////////////////////////////
/*
 *   Module Operations
 *   -----------------
 *   This module performs the logging of the test results.
 * 
 */
/*
 *   Build Process
 *   -------------
 *   - Required files:   Logger.cs, LoadAndExecute.cs
 * 
 *   Maintenance History
 *   -------------------
 *   ver 1.0 : 10 October 2016
 *     - first release
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestHarness_proj2
{
    public class Logger
    {
        //----< LogMessage function >---------------------------------------
        /*
        *   This function logs the test results using the XmlWriter class.
        */
        public static void LogMessage(string result)
        {
            string[] log = result.Split('~');
            string path = @"..\..\..\RepoFolder\";
            string fileName = log[0] + '-' + log[1] + '-' + log[2] + ".xml";
            Console.WriteLine(path + fileName);
            using (XmlWriter writer = XmlWriter.Create(Path.Combine(path, fileName)))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("Log");

                writer.WriteElementString("Author", log[0]);
                writer.WriteElementString("TestName", log[1]);
                writer.WriteElementString("TimeStamp", log[2]);
                writer.WriteElementString("TestResult", log[3]);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

    }

#if (TEST_LOGGER)
    class program
    {
        static void Main(string[] args)
        {
            Logger.LogMessage("Clinton~TrialTest~2016-10-10 04_10_10~true");
        }
    }
#endif

}
