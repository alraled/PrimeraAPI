using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PrimeraAPI.Models;

namespace PrimeraAPI.Controllers
{
    public class AlumnoController : ApiController
    {


        private Alumno17Entities db;

        public AlumnoController()
        {
            db = new Alumno17Entities();
        }


        public IQueryable<Alumno> Get()
        {
            return db.Alumno;
        }

        [ResponseType(typeof (Alumno))]

        public IHttpActionResult Get(String id)
        {
            var a = db.Alumno.Find(dni);
            if (a == null)
                return NotFound();
            return Ok(a);
        }

        public ICollection<Alumno> GetByCurso(int idCurso)
        {
            var c = db.Curso.Include("Alumno").FirstOrDefault(o => o.IdCurso == idCurso);
            if (c == null)
                return null;
            return c.Alumno;
         }


        public ICollection<Alumno> GetByProfe(int idProfesor)
        {
            var c = db.ProfesorCurso.Where(o => o.IdProfesor == idProfesor).Select(o=> o.IdCurso);
            var al = db.Curso.Where(o => c.Contains(o.IdCurso)).Select(o => o.Alumno);
            var l=new List<Alumno>();
            foreach (var a in al)
            {
             
                l.AddRange(a);   
            }

            return l;

        }


        [ResponseType(typeof (Alumno))]
        public IHttpActionResult Post(Alumno alumno)
        {
            
            db.Alumno.Add(alumno);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return BadRequest("Error en el alta");
            }

            return Created("ApiAlumnos", alumno);


        }

        [ResponseType(typeof(Alumno))]
        public IHttpActionResult Put(Alumno al)
        {

            db.Entry(al).State=EntityState.Modified;           // OPCION SIMPLE //
            
            //var alu = db.Alumno.Find(al.DNI);
            //if (alu == null)
            //    return (NotFound());                         // OPCION COMPLEJA //

            //alu.Nombre = al.Nombre;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok(alu);



        }










    }
}
