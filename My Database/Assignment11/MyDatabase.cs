using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment11
{
    public class MyDatabase<T> : IMyDatabase<T>
    {
        

        public IEnumerable<T> GetData(string code, KeyValuePair<string, object>[] parameters)
        {
            string connectionString =
               "Data Source=(local);Initial Catalog=AdventureWorks2;"
               + "Integrated Security=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader dataReader;

                string filepath = @"C:\Users\User\Source\Repos\Data-Access-Layer\My Database\query.txt";
                string[] lines = File.ReadAllLines(filepath);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 1; i < lines.Length; i++)
                {
                    stringBuilder.Append(lines[i] + " ");
                }
               
                connection.Open();
                SqlCommand command = new SqlCommand(stringBuilder.ToString(), connection);
                
                if (lines[0].Equals("Procedure"))
                {
                    
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (KeyValuePair<string, object> item in parameters)
                    {
                        var arg = command.Parameters.AddWithValue(item.Key, item.Value);
                        arg.Direction = ParameterDirection.Input;
                    }
                    dataReader = command.ExecuteReader();
                }
                else 
                {
                    dataReader = command.ExecuteReader();
                }

                List<T> result = new List<T>();

                try
                {

                    while (dataReader.Read())
                    {

                        var columns = Enumerable.Range(0, dataReader.FieldCount).Select(dataReader.GetName).ToList();

                        T instance = (T)Activator.CreateInstance<T>();

                        Type _type = instance.GetType();

                        PropertyInfo[] props = _type.GetProperties();

                        foreach (var item in columns)
                        {
                            for (int i = 0; i < props.Length; i++)
                            {
                                if (item.Equals(props[i].Name))
                                {
                                    props[i].SetValue(instance, dataReader[i]);
                                }
                            }
                        }
                        result.Add(instance);
                    }

                 
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Cannot open a connection without specifying a data source or server.orThe connection is already open.");
                    return null;
                }
                finally
                {
                    dataReader.Close();
                }

                return result;
            }
        }
    }
}
