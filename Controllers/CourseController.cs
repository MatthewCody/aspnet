using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        connection connection = new connection();
        // GET: CourseController
        public ActionResult Course()
        {
            DataTable dtable_course = new DataTable();
            DataTable dtable_department = new DataTable();

            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {

                string query_course = "SELECT * FROM tblcourse INNER JOIN tbldepartment ON tbldepartment.department_id=tblcourse.department_id";
                using (SqlCommand cmd = new SqlCommand(query_course))
                {

                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtable_course);
                    }
                }

                string query_department = "SELECT * FROM tbldepartment";
                using (SqlCommand cmd = new SqlCommand(query_department))
                {

                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtable_department);
                    }
                }
            }

            
            ViewBag.course = dtable_course;
            ViewBag.department = dtable_department;
                
            return View();
        }

        [HttpPost]
        public ActionResult SaveCourse(course course1)
        {
            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "INSERT INTO tblcourse (course_name, department_id) VALUES (@course_name, @department_id)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@course_name", course1.course_name);
                cmd.Parameters.AddWithValue("@department_id", course1.department_id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return RedirectToAction("Index");
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            DataTable dtable_course = new DataTable();
            DataTable dtable_department = new DataTable();

            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {

                string query_course = "SELECT * FROM tblcourse INNER JOIN tbldepartment ON tbldepartment.department_id=tblcourse.department_id WHERE tblcourse.course_id = " + id;
                using (SqlCommand cmd = new SqlCommand(query_course))
                {

                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtable_course);
                    }
                }

                string query_department = "SELECT * FROM tbldepartment";
                using (SqlCommand cmd = new SqlCommand(query_department))
                {

                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtable_department);
                    }
                }
            }


            ViewBag.course = dtable_course;
            ViewBag.department = dtable_department;

            return View("editcourse");
        }

        [HttpPost]
        public ActionResult UpdateCourse(course course1)
        {
            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "UPDATE tblcourse SET course_name = @course_name, department_id = @department_id WHERE course_id = @course_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@course_id", course1.course_id);
                cmd.Parameters.AddWithValue("@course_name", course1.course_name);
                cmd.Parameters.AddWithValue("@department_id", course1.department_id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return RedirectToAction("Index");
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
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

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
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
