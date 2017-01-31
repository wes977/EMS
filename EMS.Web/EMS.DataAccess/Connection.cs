/*
    * Filename: Connection.cs
    *
    * Description:
    * Holds our data access layer.
    *
    * Authors:
    * Kyle Marshall
    * Kyle Kreutzer
    *  Wes Thompson
    * Colin Mills
    *
    * Date: 2016-04-21
    
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;
using EMS.Models;
using EMS.Models.Users;
using EMS.Models.Employees;
using System.Text.RegularExpressions;

namespace EMS.DataAccess
{
    public class Connection
    {
        SqlConnection connection = null;

        /// <summary>
        /// Creates a connection based on a connection string name.
        /// </summary>
        /// <param name="connStringName"> The connection string name</param>
        /// <returns></returns>
        public static Connection Create(string connStringName)
        {
            ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings[connStringName];
            Connection conn = null;
            if (connSettings != null)
            {
                conn = new Connection(connSettings.ConnectionString);
            }

            return conn;
        }


        private Connection(string connString)
        {
            connection = new SqlConnection(connString);

        }

        /// <summary>
        /// Adds a time card
        /// </summary>
        /// <param name="EmployeeID"> The employee id </param>
        /// <param name="TCDate"> The time card date </param>
        /// <param name="Hours"> The hours 1</param>
        /// <returns></returns>

        public bool AddTimecard(int EmployeeID, DateTime TCDate, decimal Hours)
        {
            bool result = true;

            // Create the command and set the connection.
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            try
            {
                cmd.CommandText = "addTimeCard";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@employeeID", SqlDbType.BigInt).Value = EmployeeID;
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = TCDate;
                cmd.Parameters.Add("@hours", SqlDbType.Decimal).Value = Hours;


                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                DbLogging.Log(e);
            }

            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }


            return result;

        }


        /// <summary>
        /// Adds a company to the database. 
        /// </summary>
        /// <param name="name"> The company name </param>
        /// <param name="street">The company's street</param>
        /// <param name="postalCode">The company postal code </param>
        /// <param name="city">The company city </param>
        /// <param name="country">The country </param>
        /// <param name="phone">The phone number </param>
        /// <param name="fax">The fax number </param>
        /// <param name="year">The year</param>
        /// <returns>A boolean </returns>
        public bool AddCompany(string name, string street, string postalCode, string city, string country, string phone, string fax, string year)
        {

            bool result = true;

            int yearParsed = 0;
            bool yearParsedResult = false;

            if(year.Length <= 4)
            {
                yearParsedResult = int.TryParse(year, out yearParsed);
            }
            

               
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "addCompany";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@street", street);
                cmd.Parameters.AddWithValue("@postalcode", postalCode);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@fax", fax);
                cmd.Parameters.AddWithValue("@year", yearParsed);

                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
            }
            catch(SqlException e)
            {
                result = false;

                DbLogging.Log(e);
            }
            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }

            return result;
        }


        /// <summary>
        /// Addds a user to the database
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="firstName">The firstname </param>
        /// <param name="lastName">The lastname</param>
        /// <param name="securityClearence"> Security Clearence </param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public bool AddUser(string userName, string firstName, string lastName, int securityClearence, string password)
        {
            bool result = true; 

            // Create the command and set the connection.
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            try
            {
                cmd.CommandText = "addUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = userName;
                cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = firstName;
                cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = lastName;
                cmd.Parameters.Add("@securityId", SqlDbType.Int).Value = securityClearence;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                result = false;
                DbLogging.Log(e);
            }

            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }

            return result;

        }

        /// <summary>
        /// Gets a list of all company names stored
        /// in the database.
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <returns>DataTable -> List of all company names</returns>
        public DataTable GetCompanyNames()
        {
            DataTable companyNames = new DataTable();

            try
            {
                /* Set to get all company names from database */
                string cmdText = "SELECT name FROM Companies ORDER BY name ASC";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                /* Get the company names */
                FillTable(cmd, companyNames);
            }
            catch (SqlException e)
            {
                DbLogging.Log(e);
                throw;
            }

            return companyNames;
        }

        /// <summary>
        /// Finds a user based on a username and a password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User FindUser(string username, string password)
        {
            User user = null;
            string cmdText = "SELECT * FROM Users WHERE username = @username";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@username", username);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string pass = reader[4].ToString();
                    if (password == pass)
                    {
                        user = new User();
                        user.Username = reader[0].ToString();
                        user.FirstName = reader[1].ToString();
                        user.LastName = reader[2].ToString();
                        user.Clearance = int.Parse(reader[3].ToString());
                        user.Password = pass;
                    }
                }
            }


            reader.Close();
            connection.Close();

            return user;
        }

        /// <summary>
        /// An entity class for SearchResults
        /// </summary>
        public class SearchResults
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// Queries the employees by name.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="level"></param>
        /// <returns></returns>

        public List<SearchResults> QueryEmployeesByName(string query, int level)
        {
            List<SearchResults> results = null;

            string cmdText = "SELECT TOP 5 employee_id as id, CONCAT(fname, ' ', lname) as name FROM Employees WHERE (fname LIKE '" + query + "%' OR lname LIKE '" + query + "%')";

            if(level == 2)
            {
                cmdText += " AND employedWithCompany = 1 AND employee_type != 'CT'";
            }

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                results = new List<SearchResults>();
                while (reader.Read())
                {
                    results.Add(new SearchResults()
                    {
                        ID = int.Parse(reader["id"].ToString()),
                        Name = reader["name"].ToString()
                    });
                }

            }

            reader.Close();
            connection.Close();

            return results;
        }

        /// <summary>
        /// Gets all the companies.
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompanies()
        {
            DataTable result = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            


            cmd.Connection = connection;
            cmd.CommandText = "getCompanies";
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Connection.Open();

            reader = cmd.ExecuteReader();

            result.Load(reader);



            return result;
        }

        /// <summary>
        /// Gets a ll the users from the database
        /// </summary>
        /// <returns>A table containing all users</returns>
        public DataTable GetAllUsers()
        {
            DataTable userTable = new DataTable("Users");
            string cmdString = " SELECT username as Username, fname as [First Name], lname as [Last Name], SecurityLevel.description as Clearance FROM Users INNER JOIN SecurityLevel ON Users.securityLevel_id = SecurityLevel.securityLevel_id";

            SqlCommand cmd = new SqlCommand(cmdString, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            // check for rows
            if (reader.HasRows)
            {
                userTable.Columns.Add("Username");
                userTable.Columns.Add("First Name");
                userTable.Columns.Add("Last Name");
                userTable.Columns.Add("Security Level");

                // fill the table
                while (reader.Read())
                {
                    DataRow row = userTable.NewRow();
                    row["Username"] = reader[0].ToString();
                    row["First Name"] = reader[1].ToString();
                    row["Last Name"] = reader[2].ToString();
                    row["Security Level"] = reader[3].ToString();
                    userTable.Rows.Add(row);
                }
            }

            reader.Close();
            connection.Close();

            return userTable;
        }

        /// <summary>
        /// Auto creates and returns the desired table by
        /// using parameters present in the command
        /// object. This method implements support for
        /// stored procedures.
        /// </summary>
        /// <param name="command">The command object holding desired command information.</param>
        /// <param name="table">The table to return data into.</param>
        /// <exception cref="SqlException"></exception>
        /// <returns>int -> The return value of store procedure</returns>
        private int FillTable(SqlCommand command, DataTable table)
        {
            SqlDataReader reader = null;    //Used for reading from database
            SqlParameter procReturn = null; //Holds return value from a stored procedure
            int retVal = 0;                 //Less than 0 if problem occurs

            try
            {
                /* Get return value if running a stored procedure */
                if (command.CommandType == CommandType.StoredProcedure)
                {
                    procReturn = command.Parameters.Add("return_value", SqlDbType.Int);
                    procReturn.Direction = ParameterDirection.ReturnValue;
                }

                /* Open connection and execute reader */
                command.Connection.Open();
                reader = command.ExecuteReader();

                /* Load data into table */
                table.Load(reader);

                /* Set retVal to what was returned from stored procedure (if applicable) */
                if (command.CommandType == CommandType.StoredProcedure)
                {
                    retVal = (int)procReturn.Value;
                }

            }
            catch (SqlException e)
            {
                DbLogging.Log(e);
                /* Rethrow */
                throw;
            }
            finally
            {
                /* Close connection and reader if open */
                if (reader != null)
                {
                    reader.Close();
                }
                command.Connection.Close();
            }

            return retVal;
        }


        /// <summary>
        /// Deletes a company based on company name.
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public bool DeleteCompany(string companyName)
        {

            bool result = true;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            try
            {
                cmd.CommandText = "DeleteCompany";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", companyName);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            catch (SqlException e)
            {
                DbLogging.Log(e);    
                result = false;
            }

            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }

            return result;
        }

        /// <summary>
        /// Deletes the user based on username.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool DeleteUser(string userName)
        {
            
            bool result = true;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            try
            {
                cmd.CommandText = "removeUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", userName);
                
                connection.Open();
                cmd.ExecuteNonQuery();
            }

            catch (SqlException e)
            {
                DbLogging.Log(e);
                result = false;
            }

            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }

            return result;
        }

        /// <summary>
        /// Gets a single employee based on id.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns></returns>
        public Employee GetEmployee(int id)
        {
            Employee e = null;
            string cmdText = "GetEmployee";
            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter()
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@id",
                Value = id
            });

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.HasRows)
            {
                if (reader.Read())
                {
                    string type = reader["employee_type"].ToString();
                    switch (type)
                    {
                        case "FT":
                            e = FillFullTimeEmployee(reader);
                            break;
                        case "PT":
                            e = FillPartTimeEmployee(reader);
                            break;
                        case "SN":
                            e = FillSeasonalEmployee(reader);
                            break;
                        case "CT":
                            e = FillContractEmployee(reader);
                            break;
                    }
                }

            }

            reader.Close();
            connection.Close();
            if (e != null)
            {
                e.CompanyName = GetEmployeeCompanies(e.ID);
            }

            return e;
        }

        /// <summary>
        /// Fills a base employee 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="reader"></param>
        private void FillBaseEmployee(Employee e, SqlDataReader reader)
        {
            DateTime dob;
            e.FirstName = reader["fname"].ToString();
            e.LastName = reader["lname"].ToString();
            e.SocialInsuranceNumber = reader["sin"].ToString();

            if(DateTime.TryParse(reader["dateOfBirth"].ToString(), out dob))
            {
                e.DateOfBirth = dob;
            }
            else
            {
                e.DateOfBirth = DateTime.MinValue;
            }

            e.EmployedWithCompany = reader["employedWithCompany"].ToString();
            e.ID = int.Parse(reader["employee_id"].ToString());
            e.ReasonForLeaving = reader["reasonForLeaving"].ToString();

        }

        /// <summary>
        /// Fills in full time employee
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private FulltimeEmployee FillFullTimeEmployee(SqlDataReader reader)
        {
            FulltimeEmployee ft = EmployeeFactory.CreateFullTimeEmployee();
            float salary = 0;
            DateTime doh;
            DateTime dot;
            FillBaseEmployee(ft, reader);

            if(DateTime.TryParse(reader["dateOfHire"].ToString(), out doh))
            {
                ft.DateOfHire = doh;
            }
            else
            {
                ft.DateOfHire = DateTime.MinValue;
            }

            if(DateTime.TryParse(reader["dateOfTermination"].ToString(), out dot))
            {
                ft.DateOfTermination = dot;
            }
            else
            {
                ft.DateOfTermination = DateTime.MinValue;
            }

            if(float.TryParse(reader["salary_pay"].ToString(), out salary))
            {
                ft.Salary = salary;
            }
            else
            {
                ft.Salary = null;
            }


            return ft;
        }

        /// <summary>
        /// Fills full time employee
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private ParttimeEmployee FillPartTimeEmployee(SqlDataReader reader)
        {
            float hourlyRate = 0;
            DateTime doh;
            DateTime dot;
            ParttimeEmployee pt = EmployeeFactory.CreatePartTimeEmployee();
            FillBaseEmployee(pt, reader);

            if (DateTime.TryParse(reader["dateOfHire"].ToString(), out doh))
            {
                pt.DateOfHire = doh;
            }
            else
            {
                pt.DateOfHire = DateTime.MinValue;
            }

            if (DateTime.TryParse(reader["dateOfTermination"].ToString(), out dot))
            {
                pt.DateOfTermination = dot;
            }
            else
            {
                pt.DateOfTermination = DateTime.MinValue;
            }

            if (float.TryParse(reader["hourly_rate"].ToString(), out hourlyRate))
            {
                pt.HourlyRate = hourlyRate;
            }
            else
            {
                pt.HourlyRate = null;
            }


            return pt;
        }

        /// <summary>
        /// Fill seasonal employee
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>

        private SeasonalEmployee FillSeasonalEmployee(SqlDataReader reader)
        {
            float piecePay = 0;
            SeasonalEmployee sn = EmployeeFactory.CreateSeasonalEmployee();
            FillBaseEmployee(sn, reader);

            sn.Season = reader["season"].ToString();
            sn.SeasonYear = reader["season_year"].ToString();
            
            if(float.TryParse(reader["piece_pay"].ToString(), out piecePay))
            {
                sn.PiecePay = piecePay;
            }
            else
            {
                sn.PiecePay = null;
            }

            return sn;
        }

        /// <summary>
        /// Fill contract employee 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private ContractEmployee FillContractEmployee(SqlDataReader reader)
        {
            DateTime csd;
            DateTime ced;
            float fca;
            ContractEmployee ct = EmployeeFactory.CreateContractEmployee();
            FillBaseEmployee(ct, reader);

            if(DateTime.TryParse(reader["contractStartDate"].ToString(), out csd))
            {
                ct.ContractStartDate = csd;
            }
            else
            {
                ct.ContractStartDate = DateTime.MinValue;
            }

            if (DateTime.TryParse(reader["contractStopDate"].ToString(), out ced))
            {
                ct.ContractStopDate = ced;
            }
            else
            {
                ct.ContractStopDate = DateTime.MinValue;
            }

            if (float.TryParse(reader["fixedContractAmount"].ToString(), out fca))
            {
                ct.FixedContractAmount = fca;
            }
            else
            {
                ct.FixedContractAmount = null;
            }

            return ct;
        }


        /// <summary>
        /// Gets employee companies.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetEmployeeCompanies(int id)
        {
            List<string> companies = new List<string>();
            string cmdText = "SELECT name FROM Companies" +
                              " INNER JOIN EmployeeCompanies" +
                              " ON Companies.company_id=EmployeeCompanies.company_id" +
                              " INNER JOIN Employees" +
                              " ON EmployeeCompanies.employee_id=Employees.employee_id" +
                              " WHERE Employees.employee_id=@id";

            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    companies.Add(reader["name"].ToString());
                }
            }

            reader.Close();
            connection.Close();

            return companies;
        } 

        /// <summary>
        /// Invalid employee count 
        /// </summary>
        /// <returns></returns>
        public int InvalidEmployeeCount()
        {
            int result = 0;
            string cmdText = "SELECT COUNT(isValid) FROM Employees WHERE isValid = 0";
            SqlCommand cmd = new SqlCommand(cmdText, connection);

            connection.Open();
            result = int.Parse(cmd.ExecuteScalar().ToString());
            connection.Close();

            return result;
        }


        /// <summary>
        /// Search parameters
        /// </summary>
        public enum SearchParameters
        {
            All,
            FirstName,
            LastName,
            SIN,
            Invalid
        }

        /// <summary>
        /// Query Employees
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<Employee> QueryEmployees(string query, SearchParameters param)
        {
            List<Employee> employeeResults = new List<Employee>();
            List<int> employeeIds = new List<int>();

            string cmdText = "SELECT employee_id FROM Employees WHERE ";

            switch (param)
            {
                case SearchParameters.FirstName:
                    cmdText += "fname LIKE '" + query + "%'";
                    break;
                case SearchParameters.LastName:
                    cmdText += "lname LIKE '" + query + "%'";
                    break;
                case SearchParameters.SIN:
                    cmdText += "lname LIKE '" + query + "%'";
                    break;
                case SearchParameters.All:
                    cmdText += "CONCAT(fname, ' ', lname) LIKE '%" + query + "%' OR sin LIKE '[" + query + "]%'";
                    break;
                case SearchParameters.Invalid:
                    cmdText += "isValid = 0";
                    break;
            }

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    employeeIds.Add(int.Parse(reader["employee_id"].ToString()));
                }
            }

            reader.Close();
            connection.Close();

            if (employeeIds.Count > 0)
            {
                foreach (int id in employeeIds)
                {
                    employeeResults.Add(GetEmployee(id));
                }
            }

            return employeeResults;
        }


        /// <summary>
        /// Inserts an employee of either seasonal, fulltime, part
        /// </summary>
        /// <param name="firstName">Employee first name</param>
        /// <param name="lastName">Employee last name</param>
        /// <param name="sin">Employee sin or business number</param>
        /// <param name="pay">Pay amount of the employee</param>
        /// <param name="companyName">Name of the company employee works for</param>
        /// <param name="dateOfBirth">Employee date of birth</param>
        /// <param name="dateOfHire">Employee date of hire</param>
        /// <param name="dateOfTermination">Employee date of termination</param>
        /// <param name="reasonForLeaving">Employee reason for leaving</param>
        /// <param name="employeeType">Type of employee</param>
        /// <param name="season">The season the employee is working (if seasonal)</param>
        /// <param name="seasonYear">The year the employee is working (if seasonal)</param>
        /// <param name="Clearance">If admin or general user is inserting</param>
        public void CreateEmployee(string firstName, string lastName, string sin, string pay, string companyName,
            string dateOfBirth, string dateOfHire, string dateOfTermination, string reasonForLeaving, string employeeType,
            string season, string seasonYear, string Clearance)
        {
            SqlCommand cmd = new SqlCommand("", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                /* Remove spaces from sin */
                sin = Regex.Replace(sin, "[\\s]", "");

                /* Add all common parameters */
                cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = firstName;
                cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = lastName;
                cmd.Parameters.Add("@sin", SqlDbType.NChar).Value = sin;
                cmd.Parameters.Add("@employedWithCompany", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@companyName", SqlDbType.NVarChar).Value = companyName;
                cmd.Parameters.Add("@dateOfBirth", SqlDbType.Date).Value = dateOfBirth;

                /* Add specific parameters */
                if (employeeType != "seasonal")
                {
                    if (!string.IsNullOrEmpty(dateOfTermination) && Clearance == "1")
                    {
                        cmd.Parameters.Add("@reasonForLeaving", SqlDbType.NVarChar).Value = reasonForLeaving;
                        cmd.Parameters.Add("@dateOfTermination", SqlDbType.Date).Value = dateOfTermination;
                    }
                    else
                    {
                        cmd.Parameters.Add("@reasonForLeaving", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@dateOfTermination", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add("@dateOfHire", SqlDbType.Date).Value = dateOfHire;
                }
                else
                {
                    cmd.Parameters.Add("@season", SqlDbType.NVarChar).Value = season;
                    cmd.Parameters.Add("@seasonYear", SqlDbType.NChar).Value = seasonYear;
                    cmd.Parameters.Add("@reasonForLeaving", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                /* Set to valid if admin, false otherwise */
                if (Clearance == "1")
                {
                    cmd.Parameters.Add("@isValid", SqlDbType.Bit).Value = true;
                    cmd.Parameters.Add("@pay", SqlDbType.Money).Value = pay;
                }
                else
                {
                    cmd.Parameters.Add("@isValid", SqlDbType.Bit).Value = false;
                    cmd.Parameters.Add("@pay", SqlDbType.Money).Value = DBNull.Value;
                }

                /* Call appropriate method */
                switch (employeeType)
                {
                    case "fulltime":
                        cmd.CommandText = "CreateFullTime";
                        break;

                    case "parttime":
                        cmd.CommandText = "CreatePartTime";
                        break;

                    case "contract":
                        cmd.CommandText = "CreateContract";
                        break;

                    case "seasonal":
                        cmd.CommandText = "CreateSeasonal";
                        break;
                }

                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                DbLogging.Log(ex);
                throw;
            }
            finally
            {
                connection.Close();
            }
	
        }

        /// <summary>
        /// Gets the reports. 
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="funcName"></param>
        /// <param name="companyName"></param>
        /// <param name="employeeType"></param>
        /// <param name="Week"></param>
        /// <returns></returns>
        public bool GetReports(DataTable Table, string funcName, string companyName, string employeeType = "", string Week = "")
        {
            bool result = true;

            // Create the command and set the connection.
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;    //Used for reading from database
            cmd.Connection = connection;

            try
            {
                cmd.CommandText = funcName;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@companyName", SqlDbType.NVarChar).Value = companyName;
                if (employeeType != "")
                {
                    cmd.Parameters.Add("@employeeType", SqlDbType.NVarChar).Value = employeeType;
                }
                if (Week != "") // Not all them need a week so don't add parametr if dont need to 
                {
                    cmd.Parameters.Add("@StartDay", SqlDbType.Date).Value = Convert.ToDateTime(Week);
                }
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();

                /* Load data into table */
                Table.Load(reader);

            }
            catch (SqlException ex)
            {
                DbLogging.Log(ex);
            }

            finally
            {
                connection.Close();
                cmd.Parameters.Clear();
            }
            return result;
        }


        /// <summary>
        /// Gets the date of incorporation.
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public string GetDateOfIncorporation(string companyName)
        {
            SqlDataReader reader = null;
            string incorporationYear = "";
            string cmdStr = "SELECT DISTINCT yearOfIncorporation FROM companies WHERE name=@companyName";

            try
            {
                SqlCommand command = new SqlCommand(cmdStr, connection);
                command.Parameters.Add("@companyName", SqlDbType.NVarChar).Value = companyName;

                /* Open connection and get data */
                connection.Open();
                reader = command.ExecuteReader();

                /* Get the year of incorporation from reader */
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        incorporationYear = reader["yearOfIncorporation"].ToString();
                    }
                }

                
            }
            catch (SqlException ex)
            {
                DbLogging.Log(ex);
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();
            }
            return incorporationYear;
        }
    }
}





