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
    public class NhanViensController : ApiController
    {
        private BTH3Entities db = new BTH3Entities();

        // GET: api/NhanViens
        public IQueryable<NhanVien> GetNhanVien()
        {
            return db.NhanVien;
        }

        // GET: api/NhanViens/5
        [ResponseType(typeof(NhanVien))]
        [Route("api/NhanViens/GetNhanVienTheoMa/{id}")]
        public IHttpActionResult GetNhanVienTheoMa(string id)
        {
            //List<NhanVien> nv = new List<NhanVien>();
            //nv=db.F_NhanVienTheoMa(id);
            return Ok(db.F_NhanVienTheoMa(id));
        }
        [Route("api/NhanViens/GetNhanVienTheoTen/{ten}")]
        public IHttpActionResult GetNhanVienTheoTen(string ten)
        {
            
            return Ok(db.F_NhanVienTheoTen(ten));
        }
        [Route("api/NhanViens/GetNhanVienTheoDC/{dc}")]
        public IHttpActionResult GetNhanVienTheoDC(string dc)
        {
            
            return Ok(db.F_NhanVienTheoDC(dc));
        }
        [Route("api/NhanViens/GetNhanVienTheoDoTuoi/{tuoimin,tuoimax}")]
        public IHttpActionResult GetNhanVienTheoDoTuoi(int tuoimin, int tuoimax)
        {
            
            return Ok(db.f_nvtheotuoi(tuoimin, tuoimax));
        }
        [Route("api/NhanViens/GetSLNhanVienTheoGT/{gt}")]
        public IHttpActionResult GetSLNhanVienTheoGT(string gt)
        {
            
            return Ok(db.F_SoNhanVienTheoGT(gt));
        }
        // PUT: api/NhanViens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNhanVien(string id, NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhanVien.MaNV)
            {
                return BadRequest();
            }

            db.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
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
        [Route("api/NhanViens/PutNV_Proc")]

        public IHttpActionResult PutNV_Proc(string id, NhanVien nv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nv.MaNV)
            {
                return BadRequest();
            }
            return Ok(db.SuaNV(id, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.DiaChi, nv.MaPB));
            
        }

        // POST: api/NhanViens
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult PostNhanVien(NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NhanVien.Add(nhanVien);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NhanVienExists(nhanVien.MaNV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nhanVien.MaNV }, nhanVien);
        }
        [Route("api/NhanViens/PostNV_Proc")]

        public IHttpActionResult PostNV_Proc(NhanVien nv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.ThemNV(nv.MaNV, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.DiaChi, nv.MaPB);
            }
            catch (DbUpdateException)
            {
                if (NhanVienExists(nv.MaNV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(nv);
        }
        // DELETE: api/NhanViens/5
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult DeleteNhanVien(string id)
        {
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            db.NhanVien.Remove(nhanVien);
            db.SaveChanges();

            return Ok(nhanVien);
        }
        [Route("api/NhanViens/DeleteNV_Proc")]

        public IHttpActionResult DeleteNV_Proc(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            db.XoaNV(id);
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

        private bool NhanVienExists(string id)
        {
            return db.NhanVien.Count(e => e.MaNV == id) > 0;
        }
    }
}