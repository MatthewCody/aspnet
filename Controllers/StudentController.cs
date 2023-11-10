using Microsoft.AspNetCore.Mvc;
//using System.Configuration;
using System.Dynamic;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Drawing;

namespace WebApplication1.Controllers
{

    public class StudentController : Controller
    {
        private IConfiguration Configuration;
        connection connection = new connection();

        public StudentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        // GET: StudentController
        public IActionResult Student()
        {
            DataTable dtable_student = new DataTable();
            DataTable dtable_course = new DataTable();
            Random r = new Random();

            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "SELECT * FROM tblstudent INNER JOIN tblcourse ON tblcourse.course_id=tblstudent.course_id";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtable_student);
                    }
                }

                string query_course = "SELECT * FROM tblcourse";
                using (SqlCommand cmd = new SqlCommand(query_course))
                {

                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtable_course);
                    }
                }
            }

            int num = r.Next();

            ViewBag.student = dtable_student;
            ViewBag.course = dtable_course;
            ViewBag.num = num;

            return View();
        }

        [HttpPost]
        public ActionResult SaveStudent(student student1)
        {

            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "INSERT INTO tblstudent (studentID, name, course_id) VALUES (@StudentId, @name, @course_id)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", student1.StudentId);
                cmd.Parameters.AddWithValue("@name", student1.name);
                cmd.Parameters.AddWithValue("@course_id", student1.course_id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return RedirectToAction("Index");
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] student student1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //string resp = this.SaveStudent(student1);
                    //TempData["msg"] = resp;
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
