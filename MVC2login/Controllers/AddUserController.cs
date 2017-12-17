using MVC2login.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC2login.Controllers
{
    public class AddUserController : Controller
    {
        string ConnctionString = @"Data Source= DESKTOP-DR8C3OD\SQLSERVER;Initial Catalog=Product;User ID=DevLogin ;Password=ABC#@BL16";
        // GET: AddUser
        public ActionResult Index()
        {
            DataTable dtnlProdct = new DataTable();

            using (SqlConnection Con = new SqlConnection(ConnctionString))
            {
                string query = "SELECT * FROM STUDENT";
                Con.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter(query, Con);
                sqlData.Fill(dtnlProdct);


            }
            return View(dtnlProdct);
        }

        public ActionResult Insert()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult Insert(User user)
        {
            using (SqlConnection con = new SqlConnection(ConnctionString))
            {
                con.Open();
                string sqlQuery = "INSERT INTO STUDENT (Name,Pwd,Tele,Town) Values(@Name,@pwd,@Tele,@town)";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@pwd", user.Pwd);
                cmd.Parameters.AddWithValue("@Tele", user.Tele);
                cmd.Parameters.AddWithValue("@town", user.Town);
                cmd.ExecuteNonQuery();





            }

            return RedirectToAction("Index", "AddUser");
        }

        public ActionResult Update(int id)
        {
            DataTable dataset = new DataTable();
            string query = "SELECT * FROM STUDENT WHERE StudentId=@StudentId";
            using (SqlConnection con = new SqlConnection(ConnctionString))
            {
                con.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                sqlData.SelectCommand.Parameters.AddWithValue("@StudentId", id);
                sqlData.Fill(dataset);
                if (dataset.Rows.Count == 1)
                {
                    User userdata = new User();
                    userdata.StudentId = dataset.Rows[0][0].ToString();
                    userdata.Name = dataset.Rows[0][1].ToString();
                    userdata.Pwd = dataset.Rows[0][2].ToString();
                    userdata.Tele = dataset.Rows[0][3].ToString();
                    userdata.Town = dataset.Rows[0][4].ToString();

                    return View(userdata);
                }
                else
                {
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Update(User Data)
        {

            string querry = "UPDATE Student SET Name=@Name ,Pwd=@Pwd,Tele=@Tele,Town=@Town WHERE StudentId=@ID";
            using (SqlConnection con = new SqlConnection(ConnctionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(querry, con);
                cmd.Parameters.AddWithValue("@Name", Data.Name);
                cmd.Parameters.AddWithValue("Pwd", Data.Pwd);
                cmd.Parameters.AddWithValue("@Tele", Data.Tele);
                cmd.Parameters.AddWithValue("@Town", Data.Town);
                cmd.Parameters.AddWithValue("@ID", Data.StudentId);

                cmd.ExecuteNonQuery();

                con.Close();
                return RedirectToAction("Index", "AddUser");

            }


        }
        public ActionResult Delete(int id)
        {
            string DelQuery = "DELETE FROM Student WHERE StudentId=@ID";
            using (SqlConnection con = new SqlConnection(ConnctionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(DelQuery, con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
                con.Close();

            }

            return RedirectToAction("Index", "AddUser");

        }
    }
}