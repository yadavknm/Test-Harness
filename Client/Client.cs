/////////////////////////////////////////////////////////////////////////////////////////
//  Client.cs -   Main starting point for the Test Harness, where the requests are sent               
//  ver 1.0                                                                
//  Language:     C#, VS 2015                                              
//  Platform:     Windows 10               
//  Application:  Project 2 - Test Harness - CSE681 - Software Modeling & Analysis  
//  Author:       Yadav Narayana Murthy, SUID: 990783888, Syracuse University                                 
//////////////////////////////////////////////////////////////////////////////////////////
/*
 *   Module Operations
 *   -----------------
 *   This module performs the work of sending the requests into the repository directory, 
 *   then loads the requests into the blocking queue, and enters into the TestHarness package for
 *   processing of the requests
 * 
 *   NOTE:
 *   The sendFiles function is optional as all the requests will be placed in the 
 *   Repository Directory manually. sendFiles() can be used when the client does not 
 *   place the requests in the repository directory, and places them in any random directory
 * 
 */
/*
 *   Build Process
 *   -------------
 *   - Required files:   Client.cs, Testharness.cs, Blockingqueue.cs
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

namespace TestHarness_proj2
{
    class Client
    {
        //----< constructor >--------------------------------------------
        Client(TestHarness th)
        {

        }

        //----< sendFiles function >---------------------------------------
        /*
        *  All the requests are sent to RepositoryDirectory if needed.
        *  Ideally the TestHarness_Project2/XMLRequestsFolder is the destination from 
        *  where the XML requests are used for testing. 
        */
        bool sendFiles(string source, string dest)
        {
            Console.WriteLine("Sending XML Requests to the Repository Directory...");
            if (System.IO.Directory.Exists(source))
            {
                // Extract only the .xml files
                string[] XMLRequests = System.IO.Directory.GetFiles(source, "*.xml", SearchOption.AllDirectories);

                // Create Repository Directory if it does not exist
                if (!System.IO.Directory.Exists(dest))
                {
                    System.IO.Directory.CreateDirectory(dest);
                }

                // Copy the files and overwrite destination files if they already exist
                foreach (string req in XMLRequests)
                {
                    string fileName = System.IO.Path.GetFileName(req);
                    string destFile = System.IO.Path.Combine(dest, fileName);
                    System.IO.File.Copy(req, destFile, true);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }

            Console.WriteLine("XML Requests copied to the Repository Directory successfully ! \n");
            Console.WriteLine("====================================================================");
            return true;
        }

        //----< loadQueue function >---------------------------------------
        /*
        *   This function enqueues the test request into the blocking queue. 
        */
        bool loadQueue(string path, BlockingQueue<string> queue)
        {
            Console.WriteLine("\n REQUIREMENT 3: Enqueue Test Requests and executing them serially in dequeued order");
            Console.WriteLine("===================================================================================");
            Console.WriteLine("Loading Queue with test requests...");
            string[] XMLRequests = System.IO.Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
            foreach (string item in XMLRequests)
            {
                queue.enQ(item);
                Console.WriteLine(item);
            }
            Console.WriteLine("Loading of requests successfull ! \n");
            return true;
        }

        //----<startTesting function >---------------------------------------
        /*
        *   This function strats the process of testing by entering 
        *   into the TestHarness Package 
        */
        bool stratTesting(TestHarness th)
        {
            Console.WriteLine("Entering Test Harness for processing...");
            th.processMessages();
            return true;
        }

        //----<Main function >---------------------------------------
        /*
        *   The Main function acts as the starting point for the project.
        *   This calls all the functions within the Client package to accept the 
        *   requests and enqueue them in the blocking queue.
        */
        public static void Main(string[] args)
        {
            try
            {
                // creating the necessary objects required.
                TestHarness th_ = new TestHarness();
                Client client = new Client(th_);
                BlockingQueue<string> queue = th_.inQ;

                Console.WriteLine("/////////////////////////////////////////////////////////////////");
                Console.WriteLine("            CSE681 - Software Modeling & Analysis                ");
                Console.WriteLine("               Project 2 - Test Harness                          ");
                Console.WriteLine("          Yadav Narayana Murthy - SUID: 990783888                ");
                Console.WriteLine("//////////////////////////////////////////////////////////////////\n");

                Console.WriteLine("\n REQUIREMENT 1: Implemented in C# using the facilities of the .Net Framework Class Library and Visual Studio 2015");
                Console.WriteLine("=================================================================================================================\n");

                Console.WriteLine(@"Location of XML Requests: TestHarness_Project2\XMLRequestsFolder");
                Console.WriteLine(@"Path for XML Requests Directory: ..\..\..\XMLRequestsFolder");

                string sourcePath = @"..\..\..\XMLRequestsFolder";

                // Calling the Client functions for loading the queue, and to start processing of the requets 
                // by entering into the TestHarness package.
                client.loadQueue(sourcePath, queue);
                client.stratTesting(th_);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message {0}", ex.Message);
            }
        }
    }
}
