using ColoursAPi.Db;
using ColoursAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColoursAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ColorDbContext _context;

        public ValuesController(ColorDbContext colorsDbContext)
        {
            _context = colorsDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Color>> GetColorItems() {
            return _context.Colors.ToList();
        }

        public ActionResult<IEnumerable<string>> GetColors() {
            return new string[] { "Testing1Color", "Testing2Color" };
        }
    }
}
