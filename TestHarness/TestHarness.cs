/////////////////////////////////////////////////////////////////////////////////////////
//  TestHarness.cs - The TestHarness performs the job of dequeuing the request. 
//                   For each request, a new child app domain is created.                
//  ver 1.0                                                                
//  Language:     C#, VS 2015                                              
//  Platform:     Windows 10               
//  Application:  Project 2 - Test Harness - CSE681 - Software Modeling & Analysis  
//  Author:       Yadav Narayana Murthy, SUID: 990783888, Syracuse University                                 
//////////////////////////////////////////////////////////////////////////////////////////
/*
 *   Module Operations
 *   -----------------
 *  This module contains the functions processMessages() which dequeues the request.
 *  Once the request has been dequeued, createChildAppDomain() creates a new child app domain
 *  for each request.
 * 
 */
/*
 *   Build Process
 *   -------------
 *   - Required files:   Testharness.cs, BlockingQueue.cs, LoadAndExecute.cs
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
using System.Runtime.Remoting;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TestHarness_proj2
{

    public class TestHarness : MarshalByRefObject
    {
        public BlockingQueue<string> inQ;

        //----< constructor >--------------------------------------------
        public TestHarness()
        {
            inQ = new BlockingQueue<string>();
        }

        //----< processMessages function >---------------------------------------
        /*
         * The processMessages() function dequeues the XML requests and displays it to the user.
         * It also extracts the author name from the request, to be used for naming the 
         * child app domain.
        */
        public void processMessages()
        {
            Console.WriteLine("Inside TestHarness processing function. Dequeuing XML requests here. ");
            int count = inQ.size();

            // iterating through each request, to create a child app domain per request.
            for (int i=0; i<count ; i++)
            {
                Console.WriteLine("Size of the queue: "+inQ.size());
                XDocument doc = new XDocument();
                doc = XDocument.Load(inQ.deQ());

                // dequeuing the requests from the queue and displaying the xml request.
                Console.WriteLine("Deque successfull !");
                Console.WriteLine("\n REQUIREMENT 2: Test Harness accepts one or more test requests, each in the form of an XML file");
                Console.WriteLine("===============================================================================================");
                Console.WriteLine("Displaying the XML request.");
                Console.WriteLine(doc);
                string childADName = doc.Descendants("author").First().Value;

                // calling the function to create a child app domain for this particular request.
                createChildAppDomain(childADName,doc);
            }
            
        }

        //----< createChildAppDomain function >---------------------------------------
        /*
         * This function creates a child app domain for ecah request by the client.
         * Loads the assembly in to the child app domain to perform testing within the child domain,
         * and then unloads the assembly once the testing is completed.
        */
        public bool createChildAppDomain(string childName, XDocument doc)
        {
            Console.WriteLine("\n REQUIREMENT 5: Creating a ChildAppDomain for each test request.");
            Console.WriteLine("-----------------------------------------------------------------------");
            // Create application domain setup information for new AppDomain

            AppDomainSetup domaininfo = new AppDomainSetup();
            domaininfo.ApplicationBase
              = "file:///" + System.Environment.CurrentDirectory;  // defines search path for assemblies

            //Create evidence for the new AppDomain from evidence of current

            Evidence adevidence = AppDomain.CurrentDomain.Evidence;

            // Create Child AppDomain

            AppDomain ad
              = AppDomain.CreateDomain("CHILDAD_"+childName, adevidence, domaininfo);
            Console.WriteLine("Child app domain created : "+ ad.FriendlyName + "\n\n");
            
            // Loading the assembly
            ad.Load("LoadAndExecute");
            Object ob = ad.CreateInstanceAndUnwrap("LoadAndExecute", "TestHarness_proj2.LoadAndExecute");
            
            //using the IloadAndExecute interface to call the function in the assembly.
            ILoadAndExecute iLoad = (ILoadAndExecute)ob;
            iLoad.loadAndExec(doc.ToString());

            // Unloading the assembly after testing is completed
            AppDomain.Unload(ad);
            Console.WriteLine("\nAfter unloading, the current domain is the parent domain:\n" + AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("\nREQUIREMENT 9: The functional requirements have been met. They are indicated "
                + "with uppercase REQUIREMENT display message where applicable.");
            Console.WriteLine("===================================================================================================");
            return true;
        }     
    }

#if (TEST_TESTHARNESS)
    class program
    {
        static void Main(string[] args)
        {
            TestHarness th_test = new TestHarness();
            XDocument xdoc_test = new XDocument();
            BlockingQueue<string> q_test = new BlockingQueue<string>();
            th_test.processMessages();
            th_test.createChildAppDomain("CHILD_Domain", xdoc_test);        
        }
    }
#endif

}
