using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdonetDemo
{
    public class Employee
    {
        private readonly string _connectionString = "Server=localhost;Database=ADONET;User Id=sa;Password=123456789aA;TrustServerCertificate=True;";

        public void CreateTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "CREATE TABLE  Employee(Id int IDENTITY(1,1) PRIMARY KEY , Name varchar(50), Salary decimal(7,2));";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table created ");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Creating Table"+ex.Message);
            }

        }

        public void Insert(string name,double salary)
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection(_connectionString))
                {
                    

                    string query = "Insert into Employee(Name,Salary) values(@Name,@salary)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@salary", salary);

                        connection.Open();

                        int num = cmd.ExecuteNonQuery();
                        Console.WriteLine($"{num} row(s) changed");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void ReadAllData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * from Employee ;",conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Id"]} - {reader["Name"]}- {reader["Salary"]}");
                        }
                        reader.Close();
                    }
                }

            }
            catch(Exception e) {
                Console.WriteLine($"{e.Message}");
            }   
        }

        public void UpdateById(string name, double salary, int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Employee SET Name = @Name, Salary = @Salary WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Salary", salary);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        


        public void DeleteById(int id)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Employee WHERE Id = @Id";

                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void DisplayCount()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "Select count(*) as Count from Employee";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object value = cmd.ExecuteScalar();

                    Console.WriteLine($"{ value }");
                }



            }
        }


        public void DisconnectedRead()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "Select * from Employee";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds,"Employee");

                    foreach( DataRow row in ds.Tables["Employee"].Rows)
                    {
                        Console.WriteLine($"{row["Name"]}- {row["Salary"]}");
                    }
                    ds.Clear();
                }
            }
            catch(Exception e) {
                Console.WriteLine( e.Message);
            }
        }

        public void UpdateViaDisconnected(int id,string name,double salary)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "Select * from Employee";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Employee");
                    foreach (DataRow row in ds.Tables["Employee"].Rows)
                    {
                        if (row["Id"].Equals(id) )
                        {
                            row["Name"] = name;
                            row["Salary"] = salary;
                        }
                    }

                    da.Update(ds.Tables["Employee"]);
                    
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message);
            }
        }
    }
}
