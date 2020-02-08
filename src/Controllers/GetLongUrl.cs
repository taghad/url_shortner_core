 using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using System.Net;
using System;


namespace src.Controllers
{

    [Route("get_long_url")]
    [ApiController]
    public class ValuesController : Controller 
    {
        private readonly AppDbContext _context;
        public ValuesController(AppDbContext context)
        {
        _context = context;
        }
        [HttpPost]
        public ActionResult Post([FromBody]Url url)
        {
            if(!ValidateUrl(url.LongUrl)) {
                return BadRequest();
            }
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            while(true) 
            {
                url.ShortUrl = "";
                for (int i = 0; i < 8; i++)
                {
                    url.ShortUrl += chars[random.Next(chars.Length)];
                }
                if((_context.Find(url.GetType(),url.ShortUrl) == null)) 
                {
                    break;
                }
            }

            _context.urls.Add(url);
            _context.SaveChanges();
            return Ok(url);
        }
        private bool ValidateUrl(string url)
        {
            Uri validatedUri;
            if (Uri.TryCreate(url, UriKind.Absolute, out validatedUri)) 
            {
                return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }
    }

}