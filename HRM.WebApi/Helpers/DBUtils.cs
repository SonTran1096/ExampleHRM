using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HRM.WebApi.Helpers
{
    public class DBUtils
    {
        public static string connectionStr = "Data Source=LVIE1MIS08077;Initial Catalog=dbHrmExample;user id=sa;password=abc@123456;MultipleActiveResultSets=True;App=EntityFramework";

        public static DataSet ExecDataSetSP(string pSPName, List<SqlParameter> pParams)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand(pSPName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                //add parameters
                if (pParams != null)
                {
                    foreach (SqlParameter param in pParams)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                try
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Open();
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Dispose();
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        protected static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        if (objProperty.PropertyType.Name == "Guid")
                        {
                            value = new Guid((byte[])row[columnname]).ToString();
                        }
                        else
                        {
                            value = row[columnname].ToString();
                        }
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                if (objProperty.PropertyType.Name == "Guid")
                                {
                                    value = new Guid((byte[])row[columnname]).ToString().Replace("$", "").Replace(",", "");
                                    objProperty.SetValue(obj, new Guid((byte[])row[columnname]), null);
                                }
                                else
                                {
                                    value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                    objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                                }
                            }
                            else
                            {
                                if (objProperty.PropertyType.Name == "Guid")
                                {
                                    value = new Guid((byte[])row[columnname]).ToString().Replace("%", "");
                                    objProperty.SetValue(obj, new Guid((byte[])row[columnname]), null);
                                }
                                else
                                {
                                    value = row[columnname].ToString().Replace("%", "");
                                    objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                                }
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }


        public static T ExecuteSP<T>(string pSPName, List<SqlParameter> pParameters) where T : new()
        {
            try
            {
                DataTable datatable = ExecDataSetSP(pSPName, pParameters).Tables[0];
                datatable = datatable ?? new DataTable();

                T Temp = new T();
                try
                {
                    List<string> columnsNames = new List<string>();
                    foreach (DataColumn DataColumn in datatable.Columns)
                        columnsNames.Add(DataColumn.ColumnName);
                    Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames)).FirstOrDefault();
                    return Temp;
                }
                catch
                {
                    return Temp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> ExecuteSPList<T>(string pSPName, List<SqlParameter> pParameters) where T : new()
        {
            try
            {
                DataTable datatable = ExecDataSetSP(pSPName, pParameters).Tables[0];
                datatable = datatable ?? new DataTable();

                List<T> Temp = new List<T>();
                try
                {
                    List<string> columnsNames = new List<string>();
                    foreach (DataColumn DataColumn in datatable.Columns)
                        columnsNames.Add(DataColumn.ColumnName);
                    Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                    return Temp;
                }
                catch
                {
                    return Temp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int ExecQuery(string pQueryString, List<SqlParameter> pParams)
        {
            int result = 0;
            try
            {
                SqlConnection conn = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand(pQueryString, conn);
                cmd.CommandType = CommandType.Text;
                //add parameters
                if (pParams != null)
                {
                    foreach (SqlParameter param in pParams)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                try
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Open();
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static object ExecSP(string pSPName, List<SqlParameter> pParams)
        {
            object result = 0;
            try
            {
                SqlConnection conn = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand(pSPName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //add parameters
                SqlParameter returnParam = new SqlParameter();
                if (pParams != null)
                {
                    foreach (SqlParameter param in pParams)
                    {
                        cmd.Parameters.Add(param);

                        if (param.Direction == ParameterDirection.ReturnValue)
                        {
                            returnParam = param;
                        }
                    }
                }

                try
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    result = returnParam.Value == null ? -1000 : returnParam.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
