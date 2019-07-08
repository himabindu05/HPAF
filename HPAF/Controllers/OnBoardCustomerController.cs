using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace iDMS.Controllers
{
    public class OnBoardCustomerController : Controller
    {
        // GET: OnBoardCustomer
        public ActionResult CustomerDetails()
        {
            return View();
        }
        public ActionResult GuaranteerDetails(string CustomerID = null)
        {
            ViewBag.CustomerID = CustomerID;
            return View();
        }

        public string InsertCustomerData(string json_elementsCustomer)
        {
            string k = InsertCustomerData_db(json_elementsCustomer);
            return k;
        }

        public int InsertGuaranteerData(string json_elementsGuaranteer,string CustomerID)
        {
            int k = InsertGuaranterData_db(json_elementsGuaranteer,CustomerID);
            return k;
        }

        public int InsertVehicleData(string jsonvehicle, string CustomerID)
        {
            int k = InsertVehicleData_db(jsonvehicle, CustomerID);
            return k;
        }
        public ActionResult VehicleDetails(string CustomerID = null)
        {
            ViewBag.CustomerID = CustomerID;
            return View();
        }
        public ActionResult SchemeDetails(string CustomerID = null)
        {
            ViewBag.CustomerID = CustomerID;

            return View();
        }

        public int InsertOnBoardCustomerDetails(string jsoncustomer, string jsonguaranter, string jsonvehicle, string jsonscheme)
        {
            int k = InsertOnBoardCustomerDetails_db(jsoncustomer, jsonguaranter, jsonvehicle, jsonscheme);
            return k;
        }



        //DBdata
        SqlConnection con = null;
        string connSTR = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlCommand cmd = null;
        SqlDataAdapter sda = null;


        public string InsertCustomerData_db(string json_elementsCustomer)
        {
            DataTable dt = new DataTable();
            try
            {
                using (con = new SqlConnection(connSTR))
                {
                    using (cmd = new SqlCommand("InsertCustomerDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@jsoncustomer", json_elementsCustomer);
                        cmd.Parameters.Add("@ReturnVal", SqlDbType.VarChar,20);
                        cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;
                        con.Open();
                       int result = cmd.ExecuteNonQuery();
                        string message = (string)cmd.Parameters["@ReturnVal"].Value;
                        con.Close();
                        return message;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.Log(ex.Message.ToString(), "Exception on Retrieving BrandList.");
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public int InsertGuaranterData_db(string json_elementsGuaranteer, string CustomerID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (con = new SqlConnection(connSTR))
                {
                    using (cmd = new SqlCommand("InsertGuaranteerDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@jsonguaranter", json_elementsGuaranteer);
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                        // cmd.Parameters.Add("@ReturnVal", SqlDbType.VarChar, 20);
                        //cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        
                        con.Close();
                         return result;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.Log(ex.Message.ToString(), "Exception on Retrieving BrandList.");
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int InsertVehicleData_db(string jsonvehicle, string CustomerID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (con = new SqlConnection(connSTR))
                {
                    using (cmd = new SqlCommand("InsertVehicleDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@jsonvehicle", jsonvehicle);
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                        
                        con.Open();
                        int result = cmd.ExecuteNonQuery();

                        con.Close();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.Log(ex.Message.ToString(), "Exception on Retrieving BrandList.");
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int InsertOnBoardCustomerDetails_db(string jsoncustomer, string jsonguaranter, string jsonvehicle, string jsonscheme)
        {
            DataTable dt = new DataTable();
            try
            {
                using (con = new SqlConnection(connSTR))
                {
                    using (cmd = new SqlCommand("InsertOnBoardCustomerDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@jsoncustomer", jsoncustomer);
                        cmd.Parameters.AddWithValue("@jsonguaranter", jsonguaranter);
                        cmd.Parameters.AddWithValue("@jsonvehicle", jsonvehicle);
                        cmd.Parameters.AddWithValue("@jsonscheme", jsonscheme);
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                         con.Close();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.Log(ex.Message.ToString(), "Exception on Retrieving BrandList.");
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
    }
}



