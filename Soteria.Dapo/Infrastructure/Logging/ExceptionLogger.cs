using Soteria.DataComponents.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using context = System.Web.HttpContext;

namespace Soteria.DataComponents.Infrastructure
{
    public static class ExceptionLogger
    {

        public static string connectionString = ConfigHelper.GetDefaultConnectionString();

        public static async void LogExceptionAsync(Exception ex, int userID, int sourceApp, ExceptionSeverity exType)
        {
            if (ex == null)
                return;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("[dbo].[LogException]", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SourceAppID", sourceApp);
                    cmd.Parameters.AddWithValue("@LogMessage", ex.Message);
                    cmd.Parameters.AddWithValue("@StackTrace", (ex.StackTrace != null) ? ex.StackTrace : string.Empty);
                    cmd.Parameters.AddWithValue("@CreatedBy", userID.ToString());
                    cmd.Parameters.AddWithValue("@Severity", exType);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static void LogException(Exception ex, int userID, int sourceApp, ExceptionSeverity exType)
        {
            if (ex == null)
                return;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("[dbo].[LogException]", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SourceAppID", sourceApp);
                    cmd.Parameters.AddWithValue("@LogMessage", ex.Message);
                    cmd.Parameters.AddWithValue("@StackTrace", ex.StackTrace);
                    cmd.Parameters.AddWithValue("@CreatedBy", userID.ToString());
                    cmd.Parameters.AddWithValue("@Severity", exType);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void SaveLogFile(Exception exception)
        {
            if (exception != null)
            {
                var line = string.Format("{0} {1}", Environment.NewLine, Environment.NewLine);
                try
                {
                    string filepath = context.Current.Server.MapPath("~/ErrorLogs/");  //Text File Path
                    if (!Directory.Exists(filepath))
                        Directory.CreateDirectory(filepath);

                    filepath = string.Format("{0} {1} {2}", filepath, DateTime.Today.ToString("yyyy-MM-dd"), ".txt");

                    if (!File.Exists(filepath))
                        File.Create(filepath).Dispose();

                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        string error = new StringBuilder("")
                                        .Append(string.Format("{0} {1}", context.Current.Request.Url.ToString(), line))
                                        .Append(string.Format("{0} {1}", exception.GetType().Name.ToString(), line))
                                        .Append(string.Format("{0} {1}", exception.Message, line))
                                        .Append(string.Format("{0} {1}", line, exception.StackTrace))
                                        .ToString();
                        string message = string.Format("-----------Exception Details on {0} -----------------", DateTime.Now.ToString());
                        sw.WriteLine(message);
                        sw.WriteLine(line);
                        sw.WriteLine(error);
                        sw.WriteLine(line);
                        sw.WriteLine("--------------------------------*End*------------------------------------------");
                        sw.WriteLine(line);
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
