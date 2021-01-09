using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using DataMiddleware.Windows;
//using Digihail.DAD3.DataAdapter.DataAdapters;
//using Digihail.DAD3.DataAdapter.RamDataManager;
//using Digihail.DAD3.Models.DataAdapter;
using MySql.Data.MySqlClient;

namespace DataMiddleware.Utils
{
    public class DataHelper : IDisposable
    {
        //public static MysqlDataAdapter Adapter = new MysqlDataAdapter("10.1.0.61", "3306", "cv_police_beijing", "root", "frontfree");

        public static string ConnectionString = "Data Source=localhost;Database=cv_police_beijing;User Id=root;Password=frontfree;Port=3306;Charset=utf8";

        /// <summary>
        /// 获取数据表中全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTableData(string sqlString)
        {
            var dataTable = new DataTable();
            try
            {
                //AdapterDataTable data = Adapter.GetDataByQueryString(sqlString);
                //dataTable = AdapterAndSystemDataTableConvertHelper.GetSystemDataTableByAdapterDataTable(data);
                return dataTable;
            }
            catch (Exception ex)
            {
                MainWindow.WriteLog("执行SQL语句：" + sqlString + " 时出错！");
            }

            return dataTable;
        }

        /// <summary>
        /// 将数据插入到数据表中
        /// </summary>
        private static void InsertDataToTable(DataTable dataTable, string filePath)
        {
            if (dataTable == null || dataTable.Rows == null || dataTable.Rows.Count <= 0)
            {
                return;
            }
            try
            {
                List<string> columnNameList = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlBulkLoader bulk = new MySqlBulkLoader(connection);
                    bulk.FieldTerminator = ",";
                    bulk.FieldQuotationCharacter = '"';
                    bulk.EscapeCharacter = '"';
                    bulk.LineTerminator = "\r\n";
                    bulk.FileName = filePath;
                    bulk.NumberOfLinesToSkip = 0;
                    bulk.TableName = dataTable.TableName;

                    bulk.Columns.AddRange(columnNameList.ToArray());
                    bulk.Load();

                }
                MainWindow.WriteLog(dataTable.TableName + "--导入数据库成功!");
            }
            catch (Exception)
            {
                MainWindow.WriteLog(dataTable.TableName + "--导入数据库失败!");
            }
        }

        /// <summary>
        /// 将DataTable中数据写入到CSV文件中 同时写入到数据库中
        /// </summary>
        /// <param name="dataTable">提供保存数据的DataTable</param>
        public static void SaveCsv(DataTable dataTable)
        {
            try
            {
                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", Guid.NewGuid() + ".csv");
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                string data = "";

                //写出列名称
                //for (int i = 0; i < dataTable.Columns.Count; i++)
                //{
                //    data += dataTable.Columns[i].ColumnName.ToString();
                //    if (i < dataTable.Columns.Count - 1)
                //    {
                //        data += ",";
                //    }
                //}
                //sw.WriteLine(data);

                //写出各行数据
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    data = "";
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        data += dataTable.Rows[i][j].ToString();
                        if (j < dataTable.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }

                sw.Close();
                fs.Close();
                MainWindow.WriteLog(dataTable.TableName + "写入csv文件成功!--" + Path.GetFileName(fileName) + ".csv");

                InsertDataToTable(dataTable, fileName);
            }
            catch (Exception)
            {
                MainWindow.WriteLog(dataTable.TableName + "写入csv文件失败!--");
            }
        }

        /// <summary>
        /// 彻底清空数据库表中的数据
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        public static void ClearAllData(string tableName)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    string deleteString = String.Format("truncate table `{0}` ", tableName);
                    MySqlCommand deleteCommand = new MySqlCommand(deleteString, connection);
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MainWindow.WriteLog("清空库表" + tableName + "时出错！");
            }
        }

        /// <summary>
        ///     执行无返回值SQL语句
        /// </summary>
        public void ExacuteNoResult(string sqlString)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    var updateCommand = new MySqlCommand(sqlString, conn);
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Dispose()
        {

        }
    }
}
