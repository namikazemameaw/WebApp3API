using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp3API.Models;

namespace WebApp3API.Controllers
{
    public class AdminApproveController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<AdminApproveResponseModel> Get()
        {
            IEnumerable<AdminApproveResponseModel> Response = new List<AdminApproveResponseModel>();
     



            MySql.Data.MySqlClient.MySqlConnection conn;
            MySql.Data.MySqlClient.MySqlCommand cmd;

            String queryStr;
            

            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);


            conn.Open();
            queryStr = ("SELECT firstname, lastname, email, username,password FROM webappdemo.userregistration where approve = 0 ;");
            cmd = new MySqlCommand(queryStr, conn);
            MySqlDataReader dtReader = cmd.ExecuteReader();
            if (dtReader.HasRows)
            {
                List<AdminApproveResponseModel> list = new List<AdminApproveResponseModel>();

                while (dtReader.Read())
                {
                    var type = typeof(AdminApproveResponseModel);
                    AdminApproveResponseModel obj = (AdminApproveResponseModel)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    { 
                        var propType = prop.PropertyType;
                        prop.SetValue(obj, Convert.ChangeType(dtReader[prop.Name].ToString(),propType));

                    }
                    list.Add(obj);
                    
                }
                return list;

            }
           // conn.Close();




            return Response ;


        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            
            return "value";
        }

        // POST api/<controller>
        public ResponseModel Post([FromBody] AdminApproveRequstModel requst)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                if (requst.approve == 1)
                {
                    MySql.Data.MySqlClient.MySqlConnection conn;
                    MySql.Data.MySqlClient.MySqlCommand cmd;
                    string updateStr;
                    string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();
                    conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
                    conn.Open();

                    updateStr = "";

                    updateStr = "UPDATE webappdemo.userregistration SET approve ='" + requst.approve + "'  WHERE username = '" + requst.username + "' ";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(updateStr, conn);

                    cmd.ExecuteReader();

                    conn.Close();
                    responseModel.status = true;
                }
                else if (requst.disapprove == 2)
                {
                    MySql.Data.MySqlClient.MySqlConnection conn;
                    MySql.Data.MySqlClient.MySqlCommand cmd;
                    string updateStr;
                    string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();
                    conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
                    conn.Open();

                    updateStr = "";

                    updateStr = "UPDATE webappdemo.userregistration SET approve ='" + requst.disapprove + "'  WHERE username = '" + requst.username + "' ";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(updateStr, conn);

                    cmd.ExecuteReader();

                    conn.Close();
                    responseModel.status = true;
                }

            }

            catch (Exception ex)
            {
                responseModel.status = false;

            }

            return responseModel;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}