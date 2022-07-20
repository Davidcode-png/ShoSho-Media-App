using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("apt/[controller]")]
    public class TestsController: ControllerBase
    {
        [HttpGet]
        public List<int> Test()
        {
            int[] tests = { 1, 2, 3 };
            return tests.ToList();
        }
    }
}
