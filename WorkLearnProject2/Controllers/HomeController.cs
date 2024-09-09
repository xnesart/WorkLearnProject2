using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WorkLearnProject2.Controllers
{
    public class HomeController:ApiController
    {
        // public IEnumerable<int> GetValues()
        // {
        //     return Enumerable.Range(0, 10);
        // }

        [HttpGet]
        [Route("api/home/values")]
        public string Get()
        {
            return "hello";
        }
        
        [HttpPost]
        [Route("api/home/values")]
        public string Post()
        {
            return "posted succesfully";
        }   
        
        [HttpPut]
        [Route("api/home/values")]
        public string Put()
        {
            return "putted succesfully";
        }  
        
        [HttpDelete]
        [Route("api/home/values")]
        public string Delete()
        {
            return "deleted succesfully";
        }   
        
        [HttpPatch]
        [Route("api/home/values")]
        public string Patch()
        {
            return "patched succesfully";
        }
    }
}