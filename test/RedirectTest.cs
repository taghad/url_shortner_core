using System;
using Xunit;
using RA;
using System.Text.RegularExpressions;

namespace url_shortner_core.Test
{
    public class RedirectTest
    {

        [Fact]
        public void TestRedirection()
        {
            
            new RestAssured()
              .Given()
                .Name("Not Found ")
                .Header("content-type", "application/json")
                .Header("Accept-Encoding", "gzip, deflate, br")
              .When()
                
                .Get("http://localhost:5000/redirect/ZkPRHbDL")
                .Then()
                .TestStatus("redirect checker", r => r == 404)
                .AssertAll();
        }

        [Fact]
        public void TestAlphabeticalShort()
        { 
            new RestAssured()
              .Given()
                .Name("Is not Alphabetical")
                .Header("content-type", "application/json")
              .When()
                .Get("http://localhost:5000/redirect/ZkPRH678")
                .Then()
                .TestStatus("Not Alphabetical", r => r == 400)
                .AssertAll();

            new RestAssured()
              .Given()
                .Name("Is Alphabetical")
                .Header("content-type", "application/json")
              .When()
                .Get("http://localhost:5000/redirect/EwmsBzbP")
                .Then()
                .TestStatus("Is Alphabetical", r => r == 302)
                .AssertAll();
        }

        
    }

}