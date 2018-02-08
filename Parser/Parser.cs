/////////////////////////////////////////////////////////////////////////////////////////
//  Parser.cs -  Performs the parsing of the test requests               
//  ver 1.0                                                                
//  Language:     C#, VS 2015                                              
//  Platform:     Windows 10               
//  Application:  Project 2 - Test Harness - CSE681 - Software Modeling & Analysis  
//  Author:       Yadav Narayana Murthy, SUID: 990783888, Syracuse University                                 
//////////////////////////////////////////////////////////////////////////////////////////
/*
 *   Module Operations
 *   -----------------
 *   This module performs the work of parsing the test request.
 * 
 */
/*
 *   Build Process
 *   -------------
 *   - Required files:   Parser.cs
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
    public class Test
    {
        public string testName { get; set; }
        public string author { get; set; }
        public DateTime timeStamp { get; set; }
        public String testDriver { get; set; }
        public List<string> testCode { get; set; }

        //----< show function >---------------------------------------
        /*
        *   This function is used to display the parsed request. 
        */
        public void show()
        {
            Console.Write("\n  {0,-12} : {1}", "test name", testName);
            Console.Write("\n  {0,12} : {1}", "author", author);
            Console.Write("\n  {0,12} : {1}", "time stamp", timeStamp);
            Console.Write("\n  {0,12} : {1}", "test driver", testDriver);
            foreach (string library in testCode)
            {
                Console.Write("\n  {0,12} : {1}", "library", library);
            }
        }
    }
   
    public class Parser
    {
        private List<Test> testList_;
        //----< constructor >--------------------------------------------
        public Parser()
        {
            testList_ = new List<Test>();
        }

        //----< parseRequest function >---------------------------------------
        /*
        *   This function performs the parsing of requests. Adds the result to
        *   a list of Test. The Test class has been defined.
        */
        public List<Test> parseRequest(String  req)
        {
            XDocument doc_ = XDocument.Parse(req);

            if (doc_ == null)
                return null;
            
            string author = doc_.Descendants("author").First().Value;
            Test test = null;

            XElement[] xtests = doc_.Descendants("test").ToArray();
            int numTests = xtests.Count();

            for (int i = 0; i < numTests; ++i)
            {
                test = new Test();
                test.testCode = new List<string>();
                test.author = author;
                test.timeStamp = DateTime.Now;
                test.testName = xtests[i].Attribute("name").Value;
                test.testDriver = xtests[i].Element("testDriver").Value;
                IEnumerable<XElement> xtestCode = xtests[i].Elements("library");
                foreach (var xlibrary in xtestCode)
                {
                    test.testCode.Add(xlibrary.Value);
                }
                testList_.Add(test);

            }
            return testList_;            
        }
    }
#if (TEST_PARSER)
    class program
    {
        static void Main(string[] args)
        {
            Parser parser_test = new Parser();
            List<Test> testList_test = new List<Test>();
            Test test = new Test();
            try
            {
                // provide the path for XML request.
                string path = "../../TestRequest.xml";
                System.IO.FileStream xml = new System.IO.FileStream(path, System.IO.FileMode.Open);
                parser_test.parseRequest(xml.ToString());
                foreach (Test test1 in testList_test)
                {
                    test1.show();
                }
            }
            catch (Exception ex)
            {
                Console.Write("\n\n  {0}", ex.Message);
            }
        }
    }
#endif
}
