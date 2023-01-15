using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp3API.Models;

namespace WebApp3API.Controllers
{
    public class WebApp3Controller : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<controller>
        public ResponseModel Post([FromBody]  RequstModel requst)
        {
            ResponseModel responseModel = new ResponseModel();
            int approve = 0;
            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn1;
                MySql.Data.MySqlClient.MySqlCommand cmd1;
                MySql.Data.MySqlClient.MySqlConnection conn;
                MySql.Data.MySqlClient.MySqlCommand cmd;

                string selectStr;
                string queryStr;
                
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();
                conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
                conn1 = new MySql.Data.MySqlClient.MySqlConnection(connString);
                conn.Open();
                conn1.Open();
                queryStr = "";
                selectStr = ("SELECT username FROM webappdemo.userregistration where (username ='" + requst.username + "') ");
                cmd1 = new MySqlCommand(selectStr, conn1);
                MySqlDataReader dtReader = cmd1.ExecuteReader();

                if (dtReader.Read())
                {
                    responseModel.status = false;

                }
                else
                {

                queryStr = "INSERT INTO webappdemo.userregistration (firstname, middlename, lastname, email, phonenumber, username, password, approve)" +
                    "VALUES('" + requst.firstname + "','" + requst.middlename + "','" + requst.lastname + "','" +
                    requst.email + "','" + requst.phonenumber + "','" + requst.username + "','" + requst.password + "','" + approve + "')";

                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, conn);

                cmd.ExecuteReader();

                
                responseModel.status = true;
                }
                conn.Close();

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