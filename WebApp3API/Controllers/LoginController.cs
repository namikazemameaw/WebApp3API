using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp3API.Models;
using MySql.Data.MySqlClient;
using System.Runtime.Remoting.Messaging;
using System.Data.SqlClient;
using WebGrease;
using NLog;
using System.Configuration;
using System.Collections.Specialized;

namespace WebApp3API.Controllers
{
    public class LoginController : ApiController
    {
        private static Logger logger = NLog.LogManager.GetLogger("myAppLoggerrule");  
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
        public LoginResponseModel Post([FromBody] LoginRequstModel requst)
        {
            logger.Info("Start Login");

            NameValueCollection myKeys = ConfigurationManager.AppSettings;
            LoginResponseModel LoginResponseModel = new LoginResponseModel();
            string check;
            //string username ;
            //string password;
            string firstname = "null";
            string lastname = "null";
            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn;
                MySql.Data.MySqlClient.MySqlCommand cmd;

                String queryStr;

                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();
                conn = new MySql.Data.MySqlClient.MySqlConnection(connString);


                conn.Open();
                queryStr = ("SELECT username,password,firstname, lastname FROM webappdemo.userregistration where (username ='" + requst.username + "') and (password ='" + requst.password +"')");
                cmd = new MySqlCommand(queryStr, conn);
                MySqlDataReader dtReader = cmd.ExecuteReader();

                if (dtReader.Read())
                {
                    logger.Info("Login success!");
                    check = myKeys["AAA"]; 
                    firstname = dtReader["firstname"].ToString();
                    lastname = dtReader["lastname"].ToString();

                }

                else {
                    check = myKeys["BBB"];
                    logger.Info("Login fail");
                }

                //username = dtReader["username"].ToString();
                //password = dtReader["password"].ToString();

                //if (dtReader.HasRows)
                //{
                //    while (dtReader.Read())
                //    {
                //        username = dtReader["username"].ToString();
                //        password = dtReader["password"].ToString();



                //        if (requst.username == username && requst.password == password)
                //        {
                //            logger.Info("Exit login controller. Login success!");
                //            check = "54525545";
                //            firstname = dtReader["firstname"].ToString();
                //            lastname = dtReader["lastname"].ToString();

                //            break;
                //        }

                //         check = "4552524F52";

                //    }
                //    if (check == "4552524F52")
                //    {
                //        logger.Info("Login Fail 4552524F52");
                //    }





                //}
                conn.Close();
                LoginResponseModel.status = check;
                LoginResponseModel.firstname = firstname;
                LoginResponseModel.lastname = lastname; 

            }
            catch (Exception ex)
            {
                logger.Error("Login Error" + ex.Message);
                check = myKeys["CCC"];
                LoginResponseModel.status = check;
                

            }
            logger.Info("Login Finish");
            return LoginResponseModel;


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