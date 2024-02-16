using Microsoft.AspNetCore.Mvc;
using ppedv.AdventureApp.Model.Contracts;
using ppedv.AdventureApp.Model.DbModel;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ppedv.AdventureApp.UI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository repo;

        public EmployeesController(IRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return repo.Query<Employee>().ToList();
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return repo.GetById<Employee>(id);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] Employee value)
        {
            repo.Add(value);
            repo.SaveAll();
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee value)
        {
            repo.Update(value);
            repo.SaveAll();
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.Delete(Get(id));
            repo.SaveAll();
        }
    }
}
