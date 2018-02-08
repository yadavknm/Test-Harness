/////////////////////////////////////////////////////////////////////////////////////////
//  Repository.cs -  Used for Retrieving the log files               
//  ver 1.0                                                                
//  Language:     C#, VS 2015                                              
//  Platform:     Windows 10               
//  Application:  Project 2 - Test Harness - CSE681 - Software Modeling & Analysis  
//  Author:       Yadav Narayana Murthy, SUID: 990783888, Syracuse University                                 
//////////////////////////////////////////////////////////////////////////////////////////
/*
 *   Module Operations
 *   -----------------
 *   This module performs the retrieving of the test logs.
 * 
 */
/*
 *   Build Process
 *   -------------
 *   - Required files:   Repository.cs
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
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness_proj2
{
    public class Repository
    {
        public class Log
        {
            public string TestName { get; set; }
            public string Author { get; set; }
            public string TimeStamp { get; set; }
            public String TestResult { get; set; }
        }

        //----< parseLog function >---------------------------------------
        /*
        *   This function parses the log file which is stored in the form an XML
        */
        public static List<Log> parseLog(System.IO.Stream xml)
        {
            XDocument doc_ = new XDocument();
            doc_ = XDocument.Load(xml);
            List<Log> LogList_ = new List<Log>();

            Log log = new Log();

            log.Author = doc_.Descendants("Author").First().Value;
            log.TimeStamp = doc_.Descendants("TimeStamp").First().Value;
            log.TestName = doc_.Descendants("TestName").First().Value;
            log.TestResult = doc_.Descendants("TestResult").First().Value;

            LogList_.Add(log);

            return LogList_;
        }

        //----< getLog function >---------------------------------------
        /*
        *   This function is used to display the log contents.
        */
        public static void getLog(string filename)
        {
            string logFile = filename;

            System.IO.FileStream xml = new System.IO.FileStream(logFile, System.IO.FileMode.Open);
            List<Log> logList = parseLog(xml);

            foreach (Log log in logList)
            {
                Console.WriteLine("Author: {0}", log.Author);
                Console.WriteLine("Test Name: {0}", log.TestName);
                Console.WriteLine("TimeStamp: {0}", log.TimeStamp);
                Console.WriteLine("Test Status: {0}\n", log.TestResult);
            }
        }
    }

#if (TEST_REPO)
    class program
    {
        public static void Main(string[] args)
        {
            Repository.getLog(@"..\..\..\RepoFolder\Clinton~TrialTest~2016-10-10 04_10_10.xml");
        }
    }
#endif
}
