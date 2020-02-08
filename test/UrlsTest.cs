using System;
using Xunit;
using RA;
using System.Text.RegularExpressions;

namespace url_shortner_core.Test
{
    public class GetLongUrlTest
    {

        [Fact]
        public void TestShortUrlLength()
        {
            var body = new 
            {
                LongUrl = "http://google.com"
            };    
            new RestAssured()
             .Given()
                .Name("Length Checker")
                .Header("Content-Type", "application/json")
                .Body(body)
            .When()
                .Post("http://localhost:5000/urls")
            .Then()
                .TestBody("Length Test", u => ((string)u.shortUrl).Length == 8)
                .TestStatus("Status code Test", s => s == 200)
                .AssertAll();                
        }

        [Fact]
        public void TestOnlyAlphabetsUsedInShort() 
        {
            var body = new 
            {
                LongUrl = "http://google.com"
            };    
            new RestAssured()
             .Given()
                .Name("Alphabets Checker")
                .Header("Content-Type", "application/json")
                .Body(body)
            .When()
                .Post("http://localhost:5000/urls")
            .Then()
                .TestBody("Alphabet Test", u => Regex.IsMatch((string)u.shortUrl, @"^[a-zA-Z]+$"))
                .TestStatus("Status code Test", s => s == 200)
                .AssertAll();                
        }

        [Fact]
        public void TestValidating() 
        {
            //good request
            var goodBody = new 
            {
                LongUrl = "http://google.com"
            };
            new RestAssured()
             .Given()
                .Name("Alphabets Checker")
                .Header("Content-Type", "application/json")
                .Body(goodBody)
            .When()
                .Post("http://localhost:5000/urls")
            .Then()
                .TestStatus("Status code Test", s => s == 200)
                .AssertAll();  
            //bad requests
            var badBody1 = new 
            {
                LongUrl = "google"
            };    
            new RestAssured()
             .Given()
                .Name("Alphabets Checker")
                .Header("Content-Type", "application/json")
                .Body(badBody1)
            .When()
                .Post("http://localhost:5000/urls")
            .Then()
                .TestStatus("Status code Test", s => s == 400)
                .AssertAll();      
            var badBody2 = new {
                LongUrl = "google"
            };
            new RestAssured()
             .Given()
                .Name("Alphabets Checker")
                .Header("Content-Type", "application/json")
                .Body(badBody2)
            .When()
                .Post("http://localhost:5000/urls")
            .Then()
                .TestStatus("Status code Test", s => s == 400)
                .AssertAll();  
            var badBody3 = new 
            {
                LongUrl = "سلام سلام"
            };
            new RestAssured()
             .Given()
                .Name("Alphabets Checker")
                .Header("Content-Type", "application/json")
                .Body(badBody3)
            .When()
                .Post("http://localhost:5000/urls")
            .Then()
                .TestStatus("Status code Test", s => s == 400)
                .AssertAll();              
        } 
        [Fact]
        public void TestSupportUniCode() 
        {
            var body = new 
            {
                LongUrl = "http://google.com/سلام سلام تربچه"
            };    
            new RestAssured()
             .Given()
                .Name("Utf8 support Checker")
                .Header("Content-Type", "application/json")
                .Body(body)
            .When()
                .Post("http://localhost:5000/urls")
            .Then()
                .TestStatus("Utf8 support test", s => s == 200)
                .AssertAll();                
        }
        [Fact]
        public void TestEmptyBody() 
        {  
            new RestAssured()
             .Given()
                .Name("Utf8 support Checker")
                .Header("Content-Type", "application/json")
                .Body("{}")
            .When()
                .Post("http://localhost:5000/urls")
            .Then()
                .TestStatus("Empty body", s => s == 400)
                .AssertAll();                
        }
    }
}
