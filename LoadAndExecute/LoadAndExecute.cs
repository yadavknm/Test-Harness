////////////////////////////////////////////////////////////////////////////////////////////////////
//  LoadAndExecute.cs - This acts as an executive by controlling the actions of the Test harness.      
//  ver 1.0                                                                
//  Language:     C#, VS 2015                                              
//  Platform:     Windows 10               
//  Application:  Project 2 - Test Harness - CSE681 - Software Modeling & Analysis  
//  Author:       Yadav Narayana Murthy, SUID: 990783888, Syracuse University                                 
////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 *   Module Operations
 *   -----------------
 *  This module contains the functions loadAndExec() to control the actions of parsing and
 *  loading tests. 
 *  The extractDlls() function is used to extract the Dlls for a particular Test within a request.
 *  The LoadingTests() performs the job of loading the extracted DLLs and to test them. 
 *  LoadAndExecute also has a reference to the Logger, which stores the logs.
 *  
 */
/*
 *   Build Process
 *   -------------
 *   - Required files:   LoadAndExecute.cs, Parser.cs, Logger.cs
 * 
 *   Maintenance History
 *   -------------------
 *   ver 1.0 : 10 October 2016
 *     - first release
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;

namespace TestHarness_proj2
{
    
    public class LoadAndExecute : MarshalByRefObject,ILoadAndExecute
    {
        private struct TestData
        {
            public string Name;
            public ITest testDriver;
        }

        private List<TestData> testDriver = new List<TestData>();

        Queue dll = new Queue();
        string result = String.Empty;

        //----< loadAndExec function >---------------------------------------
        /*
         * This loadAndExec derives from an IloadAndExecute interface.
         * 
         * This function acts as the controller for test harness. Performs the job of parsing the request, 
         * extracting the DLLs that  have to be tested, and loads them for testing. 
         * This also has a reference to the logger which stores the results of the tests that have been executed.
         */
        public void loadAndExec(String request)
        {
            Console.WriteLine("Curent Domain:" + AppDomain.CurrentDomain.FriendlyName);

            Parser parser = new Parser();
            // calling the parseRequest() function to perform the parsing of the requests
            List<Test> tests = parser.parseRequest(request);

            // extracting the DLLs of one particular test
            Queue dll = extractDLLs(tests);

            // Loading the Dlls to perform testing.
            LoadingTests(dll);

            Console.WriteLine("\n REQUIREMENT 7: The Test Harness shall store test results and logs for each of the test executions using a key that combines the test developer identity and the current date-time.");
            Console.WriteLine("================================================================");
            string[] resultData = result.Split('-');
            int rcount = 0;
            foreach (var i in tests)
            {

                string logResult = i.author + "~" + i.testName + "~" + i.timeStamp.ToString("yyyy-mm-dd hh_mm_ss") + "~" + resultData[rcount];
                rcount++;

                // Calling the LogMessage() function to log the results of the test.
                Logger.LogMessage(logResult);
            }
            Console.WriteLine("\n REQUIREMENT 6: Test logs retrieved using the getLog() function.");
            Console.WriteLine("================================================================");
            foreach (var i in tests)
            {
                // Retrieving Test Logs using getLog().
                Repository.getLog(Path.Combine(@"..\..\..\RepoFolder\", i.author + "-" + i.testName + "-" + i.timeStamp.ToString("yyyy-mm-dd hh_mm_ss") + ".xml"));
            }

        }
       

        //----< extractDlls function >---------------------------------------
        /*
         * This function extracts the Dlls of a particular test from the repository.
        */
        public Queue extractDLLs(List<Test> test1)
        {
            foreach(Test t in test1)
            {
                dll.Enqueue(t.testDriver); // extracting test drivers
                foreach(String code in t.testCode)
                {
                    dll.Enqueue(code); // extracting code to test
                }
            }
            return dll;
                    
        }


        //----< LoadingTests function >---------------------------------------
        /*
         * This function loads the extracted Dlls to perform the testing.
        */
        public void LoadingTests(Queue dll)
        {

            Console.Write("\n  Loading and executing tests");
            Console.Write("\n =============================");

            // run the tests, if loading of DLLs is successfull
            if (LoadTests(@"../../../RepoFolder/Tests/", dll))
            {
                Console.WriteLine("\n REQUIREMENT 4: Each test driver derives from an ITest interface that declares a method test() \n" +
                                    "that takes no arguments and returns the test pass status, e.g., a boolean true or false value");
                Console.WriteLine("====================================================================================================");
                run();
            }
            else
                Console.Write("\n  couldn't load tests");


            Console.Write("\n\n");

        }

        //----< load test dlls to invoke >-------------------------------

        bool LoadTests(string path, Queue dll)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.dll");

            foreach (string file in files)
            {
                if (dll.Contains(Path.GetFileName(file)))
                {

                    Console.Write("\n  loading: \"{0}\"", file);

                    try
                    {
                        Assembly assem = Assembly.LoadFrom(file);
                        Type[] types = assem.GetExportedTypes();

                        foreach (Type t in types)
                        {
                            if (t.IsClass && typeof(ITest).IsAssignableFrom(t))  // does this type derive from ITest ?
                            {
                                ITest tdr = (ITest)Activator.CreateInstance(t);   // create instance of test driver

                                // save type name and reference to created type on managed heap

                                TestData td = new TestData();
                                td.Name = t.Name;
                                td.testDriver = tdr;
                                testDriver.Add(td);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                       Console.Write("\n  {0}", ex.Message);
                    }
                }

            }
            Console.Write("\n");
            return testDriver.Count > 0;   // if we have items in list then Load succeeded
        }

        //----< run all the tests on list made in LoadTests >------------

        void run()
        {
            if (testDriver.Count == 0)
                return;
            foreach (TestData td in testDriver)  // enumerate the test list
            {
                try
                {
                    Console.Write("\n  testing {0}", td.Name);
                    td.testDriver.test();
                    result += "Pass";   // if the test was successfull, store the passed result
                    result += "-";
                    Console.WriteLine("\n  True");
                    Console.WriteLine("\nTest Succesful");

                }
                catch (Exception ex)
                {
                    result += "Fail";   // if the test was unsuccessfull, store the failed result
                    result += "-";
                    Console.Write("\n  {0}", ex.Message);
                    Console.WriteLine("\n  False");
                    Console.WriteLine("\nTest Unsuccesful");
                }
            }
        }

       
    }
#if (TEST_LOADANDEXECUTE)
    class program
    {
        static void Main(string[] args)
        {
            LoadAndExecute lae_test = new LoadAndExecute();
            XDocument xdoc_test = new XDocument();

            // provide the appropriate path for the xml request file.
            string path = "../../TestRequest.xml";
            System.IO.FileStream xml = new System.IO.FileStream(path, System.IO.FileMode.Open);

            lae_test.loadAndExec(xml.ToString());
        }
    }
#endif
}
