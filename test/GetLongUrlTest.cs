using System;
using Xunit;
using RA;

namespace url_shortner_core
{
    public class GetLongUrlTest
    {
        [Fact]
        public void TestShortUrlLength()
        {
         var body = new {
                LongUrl = "http://google.com"
            };    
            new RestAssured()
             .Given()
                .Name("Length Checker")
                .Header("Content-Type", "application/json")
                .Body(body)
            .When()
                .Post("http://localhost:5000/get_long_url")
            .Then()
                .TestBody("Length Test", u => ((string)u.shortUrl).Length == 8)
                .Assert("Length Test");
                
        }
    }
}
