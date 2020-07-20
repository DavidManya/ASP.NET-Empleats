using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using Empleats.Lib.DAL;
using Empleats.Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmpleatsASPNET.Controllers
{
    [Route("api/Empleat")]
    [ApiController]
    public class EmpleatsController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public EmpleatsController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/<EmpleatsController>
        [HttpGet]
        //public IEnumerable<string> Get()
        public async Task<ActionResult<IEnumerable<Empleat>>> GetEmpleat()
        {
            var repo = Entity.DepCon.Resolve<IRepository<Empleat>>();
            return await repo.QueryAll().ToListAsync();
        }

        // GET api/<EmpleatsController>/5
        [HttpGet("{id}")]
        //public string Get(int id)
        public async Task<ActionResult<Empleat>> GetEmpleat(Guid id)
        {
            var repo = Entity.DepCon.Resolve<IRepository<Empleat>>();

            var empleat = await repo.QueryAll().FirstOrDefaultAsync(s => s.Id == id);

            if (empleat == null)
            {
                return NotFound();
            }

            return empleat;

        }

        // POST api/<EmpleatsController>
        [HttpPost]
        //public void Post([FromBody] string value)
        public async Task<SaveValidation<Empleat>> PostEmpleat(Empleat empleat)
        {
            return await Task.Run(() =>
            {
                return empleat.Save();
            });
        }

        // PUT api/<EmpleatsController>/5
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        public async Task<SaveValidation<Empleat>> PutEmpleat(Guid id, Empleat empleat)
        {
            return await Task.Run(() =>
            {
                return empleat.Save();
            });
        }

        // DELETE api/<EmpleatsController>/5
        [HttpDelete("{id}")]
        //public void Delete(int id)
        public async Task<DeleteValidation<Empleat>> Deleteempleat(Guid id)
        {
            var repo = Entity.DepCon.Resolve<IRepository<Empleat>>();

            var empleat = await repo.QueryAll().FirstOrDefaultAsync(s => s.Id == id);


            return await Task.Run(() =>
            {

                return empleat.Delete();
            });
        }
    }
}
