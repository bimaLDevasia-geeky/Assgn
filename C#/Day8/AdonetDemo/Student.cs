using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonetDemo
{
    public class Student
    {
        private readonly string _connectionString = "Server=localhost;Database=ADONET;User Id=sa;Password=123456789aA;TrustServerCertificate=True;";
        public void InsertData(string Name, string Class) {

            try {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {

                    conn.Open();
                    string query = "Insert into student(Name,Class) values(@name,@class)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", Name);
                        cmd.Parameters.AddWithValue("@class", Class);
                        cmd.ExecuteNonQuery();

                    }
                }
                }
        catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }



        public void GetStudentViaId(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetStudentById", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", id);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Id"]}- {reader["Name"]}- {reader["Class"]}");
                        }
                    }

                }
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }


        public void InsertIntoStudent(string Name,string Class)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("InsertAndGetLastInfo", conn))
                    {
                        cmd.Parameters.AddWithValue("@name", Name);
                        cmd.Parameters.AddWithValue("@class", Class);

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter outparam = new SqlParameter();
                        outparam.ParameterName = "@last";
                        outparam.SqlDbType = SqlDbType.Int;
                        outparam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outparam);
                        cmd.ExecuteNonQuery();

                        int newId = (int)outparam.Value;

                        Console.WriteLine($"New student inserted successfully with ID: {newId}");

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void DeleteStudent(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteStudent", conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", id);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void UpdateStudent(string name,string Class,int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateStudent", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue ("@StudentName", name);
                        cmd.Parameters.AddWithValue("@Class", Class);
                        cmd.Parameters.AddWithValue("@StudentId", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}
