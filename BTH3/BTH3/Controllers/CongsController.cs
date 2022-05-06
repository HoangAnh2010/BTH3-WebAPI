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
    public class CongsController : ApiController
    {
        private BTH3Entities db = new BTH3Entities();

        // GET: api/Congs
        public IQueryable<Cong> GetCong()
        {
            return db.Cong;
        }

        // GET: api/Congs/5
        [ResponseType(typeof(Cong))]
        public IHttpActionResult GetCong(string id)
        {
            Cong cong = db.Cong.Find(id);
            if (cong == null)
            {
                return NotFound();
            }

            return Ok(cong);
        }

        // PUT: api/Congs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCong(string id, Cong cong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cong.MaCT)
            {
                return BadRequest();
            }

            db.Entry(cong).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongExists(id))
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
        [Route("api/Congs/PutCong_Proc")]
        public IHttpActionResult PutCong_Proc(string mact, string manv, Cong cong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (mact != cong.MaCT|| manv != cong.MaNV)
            {
                return BadRequest();
            }
            return Ok(db.SuaCong(mact, manv, cong.SLNgayCong));
        }
        // POST: api/Congs
        [ResponseType(typeof(Cong))]
        public IHttpActionResult PostCong(Cong cong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cong.Add(cong);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CongExists(cong.MaCT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cong.MaCT }, cong);
        }
        [Route("api/Congs/PostCong_Proc")]
        public IHttpActionResult PostCong_Proc(Cong cong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(db.ThemCong(cong.MaCT, cong.MaNV, cong.SLNgayCong));
        }
        // DELETE: api/Congs/5
        [ResponseType(typeof(Cong))]
        public IHttpActionResult DeleteCong(string id)
        {
            Cong cong = db.Cong.Find(id);
            if (cong == null)
            {
                return NotFound();
            }

            db.Cong.Remove(cong);
            db.SaveChanges();

            return Ok(cong);
        }
        [Route("api/Congs/DeleteCong_Proc")]
        public IHttpActionResult DeleteCong_Proc(string mact, string manv)
        {
            if (mact == null || manv==null)
            {
                return NotFound();
            }
            db.XoaCong(mact, manv);
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

        private bool CongExists(string id)
        {
            return db.Cong.Count(e => e.MaCT == id) > 0;
        }
    }
}