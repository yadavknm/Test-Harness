Steps to run the Test Harness 
_____________________________

1) Place the XML requests in the following directory TestHarness_Project2/XMLRequestsFolder.
2) The XML template has to match the XMLRequest already present in the above directory.
3) Each test driver derives from an ITest interface that declares a method test() that takes no arguments and 
   returns the test pass status, e.g., a boolean true or false value.
4) The logs can be found in the following directory TestHarness_Project2/RepoFolder.
5) compile.bat and run.bat files are placed in the parent directory of this project.