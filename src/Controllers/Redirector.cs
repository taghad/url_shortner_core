using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using System.Text.RegularExpressions;

namespace src.Controllers
{
    [Route("/redirector/{*short_url}")]
    [ApiController]
    public class RedirectController : Controller 
    {
         private readonly AppDbContext _context;
        public RedirectController(AppDbContext context)
        {
        _context = context;
        }
        [HttpGet]
        public ActionResult Get(string short_url) 
        {    
            Url url = new Url();
            if(!Regex.IsMatch(short_url, @"^[a-zA-Z]+$")) 
            {
                return BadRequest();
            }
            if(short_url.Length != 8) 
            {
                return BadRequest();
            }
            if((_context.Find(url.GetType(),short_url) == null)) 
            {  
                return NotFound();
            }
            var u = (_context.urls.Find(short_url));
            
            return Redirect(u.longUrl);
        }
    }
}