using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BTH3.Models;

namespace BTH3.Controllers
{
    public class CongtrinhsController : ApiController
    {
        private BTH3Entities db = new BTH3Entities();

        // GET: api/Congtrinhs
        public IQueryable<Congtrinh> GetCongtrinh()
        {
            return db.Congtrinh;
        }

        // GET: api/Congtrinhs/5
        [ResponseType(typeof(Congtrinh))]
        public IHttpActionResult GetCongtrinh(string id)
        {
            Congtrinh congtrinh = db.Congtrinh.Find(id);
            if (congtrinh == null)
            {
                return NotFound();
            }

            return Ok(congtrinh);
        }

        // PUT: api/Congtrinhs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCongtrinh(string id, Congtrinh congtrinh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != congtrinh.MaCT)
            {
                return BadRequest();
            }

            db.Entry(congtrinh).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongtrinhExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Congtrinhs/PutCongtrinh_Proc")]
        public IHttpActionResult PutCongtrinh_Proc(string mact, Congtrinh ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (mact != ct.MaCT)
            {
                return BadRequest();
            }

            return Ok(db.SuaCT(mact, ct.TenCT,ct.DiaDiem,ct.NGAYCAPGP,ct.NGAYKC));
        }
        // POST: api/Congtrinhs
        [ResponseType(typeof(Congtrinh))]
        public IHttpActionResult PostCongtrinh(Congtrinh congtrinh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Congtrinh.Add(congtrinh);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CongtrinhExists(congtrinh.MaCT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = congtrinh.MaCT }, congtrinh);
        }
        [Route("api/Congtrinhs/PostCongtrinh_Proc")]
        public IHttpActionResult PostCongtrinh_Proc( Congtrinh ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(db.ThemCongTrinh(ct.MaCT, ct.TenCT, ct.DiaDiem, ct.NGAYCAPGP, ct.NGAYKC));
        }
        // DELETE: api/Congtrinhs/5
        [ResponseType(typeof(Congtrinh))]
        public IHttpActionResult DeleteCongtrinh(string id)
        {
            Congtrinh congtrinh = db.Congtrinh.Find(id);
            if (congtrinh == null)
            {
                return NotFound();
            }

            db.Congtrinh.Remove(congtrinh);
            db.SaveChanges();

            return Ok(congtrinh);
        }
        [Route("api/Congtrinhs/DeleteCongtrinh_Proc")]
        public IHttpActionResult DeleteCongtrinh_Proc(string mact)
        {
            if (mact == null)
            {
                return NotFound();
            }
            db.XoaCT(mact);
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CongtrinhExists(string id)
        {
            return db.Congtrinh.Count(e => e.MaCT == id) > 0;
        }
    }
}