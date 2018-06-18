# SimpleWebService

1. Enlist the project using git clone https://github.com/shenred176/SimpleWebService.git command.
2. Open project in Visual Studio 2017, set Starting Project to "SimpleWebService" and hit F5 (Run)
3. After browser is launched, use application like PostMan or Fiddler to test Rest API calls.
   You may test with the following urls:
   http://localhost:8080/Get/  it should return "OK 200" with text "Gideon"
   http://localhost:8080/Put/  it should return "Bad Request 404"
   http://localhost:8080/Put?key=key1&value=value1   it should return "Accepted 202"
   http://localhost:8080/Get?key=key1   it should return "OK 200" with text "value1"
4. To run unit test, open UnitTest1.cs in SimpleWebService.Tests, hit Ctrl+R,T to run all tests.
   
