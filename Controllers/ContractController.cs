using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MVCWEF.Models;

namespace MVCWEF.Controllers
{
    public class ContractController : Controller
    {
        string connectionString = @"Data Source = (local)\sqle2012; Initial Catalog = licdb; Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblContract = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM licdb.Contract", sqlCon);
                sqlDa.Fill(dtblContract);
            }
            return View(dtblContract);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ContractModel());
        }

        //
        // POST: /Contract/Create
        [HttpPost]
        public ActionResult Create(ContractModel ContractModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "EXEC licdb.AddEdit_Contract (@p_Name,@p_Address,@p_ID,@p_Country,@p_SDate,@p_Gen,@p_Age, @p_dob, @p_flag )";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@p_ID", 0);
                sqlCmd.Parameters.AddWithValue("@p_Name", ContractModel.CustomerName);
                sqlCmd.Parameters.AddWithValue("@p_Address", ContractModel.Address);
                sqlCmd.Parameters.AddWithValue("@p_Country", ContractModel.Country);
                sqlCmd.Parameters.AddWithValue("@p_SDate", ContractModel.SaleDate);
                sqlCmd.Parameters.AddWithValue("@p_Gen", ContractModel.Gender);
                sqlCmd.Parameters.AddWithValue("@p_dob", ContractModel.Dateofbirth);
                sqlCmd.Parameters.AddWithValue("@p_flag", "ADD");
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Contract/Edit/5
        [HttpPost]
        public ActionResult Edit(ContractModel ContractModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "EXEC licdb.AddEdit_Contract (@p_Name,@p_Address,@p_ID,@p_Country,@p_SDate,@p_Gen,@p_Age, @p_dob, @p_flag )";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@p_Name", ContractModel.CustomerName);
                sqlCmd.Parameters.AddWithValue("@p_Address", ContractModel.Address);
                sqlCmd.Parameters.AddWithValue("@p_ID", ContractModel.ContractID);
                sqlCmd.Parameters.AddWithValue("@p_Country", ContractModel.Country);
                sqlCmd.Parameters.AddWithValue("@p_SDate", ContractModel.SaleDate);
                sqlCmd.Parameters.AddWithValue("@p_Gen", ContractModel.Gender);
                sqlCmd.Parameters.AddWithValue("@p_dob", ContractModel.Dateofbirth);
                sqlCmd.Parameters.AddWithValue("@p_flag", "MODIFY");
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /ContractID/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Contract WHERE ContractID = @ContractID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ContractID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Contract/Edit/5 // Modified alone
        public ActionResult Edit(int id)
        {
            ContractModel ContractModel = new ContractModel();
            DataTable dtblContract = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM licdb.Contract Where ContractID = @ContractID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ContractID", id);
                sqlDa.Fill(dtblContract);
            }
            if (dtblContract.Rows.Count == 1)
            {
                ContractModel.ContractID = Convert.ToInt32(dtblContract.Rows[0][0].ToString());
                ContractModel.CustomerName = dtblContract.Rows[0][1].ToString();
                ContractModel.Address = dtblContract.Rows[0][2].ToString();
                ContractModel.Country = dtblContract.Rows[0][3].ToString();
                ContractModel.SaleDate = dtblContract.Rows[0][4].ToString();
                ContractModel.Name = dtblContract.Rows[0][5].ToString();
                ContractModel.Gender = dtblContract.Rows[0][6].ToString();
                ContractModel.Dateofbirth = dtblContract.Rows[0][7].ToString();
                return View(ContractModel);
            }
            else
                return RedirectToAction("Index");
        }

 
   }
}
