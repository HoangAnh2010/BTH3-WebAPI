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
    public class PhongbansController : ApiController
    {
        private BTH3Entities db = new BTH3Entities();

        // GET: api/Phongbans
        public IQueryable<Phongban> GetPhongban()
        {
            return db.Phongban;
        }

        // GET: api/Phongbans/5
        [ResponseType(typeof(Phongban))]
        public IHttpActionResult GetPhongban(string id)
        {
            Phongban phongban = db.Phongban.Find(id);
            if (phongban == null)
            {
                return NotFound();
            }

            return Ok(phongban);
        }

        // PUT: api/Phongbans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhongban(string id, Phongban phongban)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phongban.MaPB)
            {
                return BadRequest();
            }

            db.Entry(phongban).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhongbanExists(id))
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
        [Route("api/Phongbans/PutPhongban_Proc")]
        public IHttpActionResult PutPhongban_Proc(string mapb,Phongban pb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (mapb != pb.MaPB)
            {
                return BadRequest();
            }
            return Ok(db.SuaPB(mapb, pb.TenPB));
        }
        // POST: api/Phongbans
        [ResponseType(typeof(Phongban))]
        public IHttpActionResult PostPhongban(Phongban phongban)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Phongban.Add(phongban);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PhongbanExists(phongban.MaPB))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = phongban.MaPB }, phongban);
        }
        [Route("api/Phongbans/PostPhongban_Proc")]
        public IHttpActionResult PostPhongban_Proc( Phongban pb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(db.ThemPB(pb.MaPB,pb.TenPB));
        }
        // DELETE: api/Phongbans/5
        [ResponseType(typeof(Phongban))]
        public IHttpActionResult DeletePhongban(string id)
        {
            Phongban phongban = db.Phongban.Find(id);
            if (phongban == null)
            {
                return NotFound();
            }

            db.Phongban.Remove(phongban);
            db.SaveChanges();

            return Ok(phongban);
        }
        [Route("api/Phongbans/DeletePhongban_Proc")]
        public IHttpActionResult DeletePhongban_Proc(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            db.XoaPB(id);
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

        private bool PhongbanExists(string id)
        {
            return db.Phongban.Count(e => e.MaPB == id) > 0;
        }
    }
}