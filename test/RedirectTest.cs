using System;
using Xunit;
using RA;
using System.Text.RegularExpressions;

namespace url_shortner_core.Test
{
    public class RedirectTest
    {

        [Fact]
        public void TestFoundOrNot()
        {
            
            new RestAssured()
              .Given()
                .Name("Not Found ")
                .Header("content-type", "application/json")
                .Header("Accept-Encoding", "gzip, deflate, br")
              .When()
                
                .Get("http://localhost:5000/redirector/ZkPRHbDL")
                .Then()
                .TestStatus("redirect checker", r => r == 404)
                .AssertAll();

              // new RestAssured()
              // .Given()
              //   .Name("Found")
              //   .Header("content-type", "application/json")
              //   .Header("Accept-Encoding", "gzip, deflate, br")
              // .When()
                
              //   .Get("http://localhost:5000/redirector/EwmsBzbP")
              //   .Then()
              //   .TestStatus("Found checker", r => r != 404)
              //   .AssertAll();
        }

        [Fact]
        public void TestAlphabeticalShort()
        { 
            new RestAssured()
              .Given()
                .Name("Is not Alphabetical")
                .Header("content-type", "application/json")
              .When()
                .Get("http://localhost:5000/redirector/ZkPRH678")
                .Then()
                .TestStatus("Not Alphabetical", r => r == 400)
                .AssertAll();

            // new RestAssured()
            //   .Given()
            //     .Name("Is Alphabetical")
            //     .Header("content-type", "application/json")
            //     .Header("Accept-Encoding", "gzip, deflate, br")
            //   .When()
            //     .Get("http://localhost:5000/redirector/eVmocHyw")
            //     .Then()
            //     .TestStatus("Is Alphabetical", r => r != 404)
            //     .AssertAll();
        }

        [Fact]
        public void TestJust8Char()
        {
            new RestAssured()
              .Given()
                .Name("Less than 8 chars")
                .Header("content-type", "application/json")
                .Header("Accept-Encoding", "gzip, deflate, br")
              .When()
                .Get("http://localhost:5000/redirector/ZkPRHb")
                .Then()
                .TestStatus("Less than 8 chars", r => r == 400)
                .AssertAll();

            new RestAssured()
              .Given()
                .Name("More than 8 chars")
                .Header("content-type", "application/json")
                .Header("Accept-Encoding", "gzip, deflate, br")
              .When()
                .Get("http://localhost:5000/redirector/ZkPRHbDLghgfg")
                .Then()
                .TestStatus("More than 8 chars", r => r == 400)
                .AssertAll();

            // new RestAssured()
            //   .Given()
            //     .Name("equal 8 chars")
            //     .Header("content-type", "application/json")
            //   .When()
            //     .Get("http://localhost:5000/redirector/JAWefNMp")
            //     .Then()
            //     .TestStatus("Equal 8 chars", r => r != 404)
            //     .AssertAll();
        }
    }
}