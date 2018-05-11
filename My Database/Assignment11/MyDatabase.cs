using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment11
{
    public class MyDatabase<T> : IMyDatabase<T>
    {
        

        public IEnumerable<T> GetData(string code, KeyValuePair<string, T> parameters)
        {
            string connectionString =
               "Data Source=(local);Initial Catalog=AdventureWorks2;"
               + "Integrated Security=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(code, connection);

                //try
                //{
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
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
                        yield return instance;

                        //int coloumnCount = dataReader.FieldCount;
                        //Type[] type = new Type[coloumnCount];
                        //for (int i = 0; i < coloumnCount; i++)
                        //    type[i] = dataReader.GetFieldType(i);

                        //var yui = new {asd = dataReader[0], };
                    }
                dataReader.Close();
                //}
                //catch (Exception)
                //{

                //    throw;
                //}

                //return null;
            }

           
        }
    }
}
