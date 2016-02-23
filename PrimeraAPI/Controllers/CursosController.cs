using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PrimeraAPI.Models;

namespace PrimeraAPI.Controllers
{
    public class CursosController : ApiController
    {

        private Alumno17Entities bd;

        public CursosController()
        {
            bd = new Alumno17Entities();
        }

        public IQueryable<Curso> Get()
        {
            return bd.Curso;
        }

        [ResponseType(typeof (Curso))]
        public IHttpActionResult Get(int id)
        {
            var data = bd.Curso.Find(id);

            if (data == null)
                return NotFound();
           

            return Ok(data);
        }
    }
}
