using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace ReplacementMIS.common
{
    class MySqlobj
    {
        private MySqlConnection conn;
        private MySqlCommand com;
        private bool _alreadyDispose = false;

        #region 构造与柝构
        public MySqlobj()
        {
            try
            {
                conn = new MySqlConnection(ConfigurationManager.AppSettings["mysqlconn"]);
                conn.Open();
                com = new MySqlCommand();
                com.Connection = conn;
            }
            catch (Exception ee)
            {
                //   throw new Exception("连接数据库出错");
            }
        }


        ~MySqlobj()
        {
            Dispose();
        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (_alreadyDispose) return;
            if (isDisposing)
            {
                // TODO: 此处释放受控资源    
                if (com != null)
                {

                    com.Dispose();
                }
                if (conn != null)
                {
                    try
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                    catch (Exception ee)
                    {
                    }
                    finally
                    {
                        conn = null;
                    }
                }
            }
            // TODO: 此处释放非受控资源。设置被处理过标记    
            _alreadyDispose = true;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region 获取DataSet
        public DataSet GetDataSet(string sqlString)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, conn);
                da.Fill(ds);
            }
            catch (Exception ee)
            {
                throw new Exception("SQL:" + sqlString + "<br />" + ee.Message.ToString());
            }
            return ds;
        }
        #endregion

        #region 执行插入或删除操作
        public void ExecuteNonQuery(string sqlString)
        {
            int ret = 0;
            com.CommandText = sqlString;
            com.CommandType = CommandType.Text;
            try
            {
                ret = com.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                throw new Exception("SQL:" + sqlString + "<br />" + ee.Message.ToString());
            }
            finally
            {
                com.Dispose();
            }
        }
        #endregion   

    }
}
