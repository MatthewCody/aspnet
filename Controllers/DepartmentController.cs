using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        connection connection = new connection();
        // GET: DepartmentController
        public ActionResult Department()
        {
            DataTable dtable_department = new DataTable();

            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "SELECT * FROM tbldepartment";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtable_department);
                    }
                }
            }

            ViewBag.department = dtable_department;

            return View();
        }

        [HttpPost]
        public ActionResult SaveDepartment(department department1)
        {
            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "INSERT INTO tbldepartment (department_name) VALUES (@department_name)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@department_name", department1.department_name);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return RedirectToAction("Index");
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            DataTable dtable_department = new DataTable();

            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "SELECT * FROM tbldepartment WHERE department_id = " + id;
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtable_department);
                    }
                }
            }

            ViewBag.department = dtable_department;
            return View("editdepartment");
        }

        [HttpPost]
        public ActionResult UpdateDepartment(department department1)
        {
            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "UPDATE tbldepartment SET department_name = @department_name WHERE department_id = @department_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@department_id", department1.department_id);
                cmd.Parameters.AddWithValue("@department_name", department1.department_name);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return RedirectToAction("Index");
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
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

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentController/Edit/5
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

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connection._sqlConnection()))
            {
                string query = "DELETE FROM tbldepartment WHERE department_id = " + id;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return RedirectToAction("Index");
        }

        // POST: DepartmentController/Delete/5
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
