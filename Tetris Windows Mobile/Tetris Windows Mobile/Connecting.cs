using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace Tetris_Windows_Mobile
{
    public class Constants
    {

        public static string connectionString = "Data Source=" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\TopScore.sdf; Password = 0912403";
        public static string dbFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\TopScore.sdf";
        public static string createSql = @"Create Table TopScore(Rank nvarchar(5) PRIMARY KEY , Score int NULL)";
    }

    class Connecting
    {
        private SqlCeConnection con = null;

        public Connecting(ListView l)
        {
            if (!System.IO.File.Exists(Constants.dbFilePath))
            {
                SqlCeEngine eng = new SqlCeEngine(Constants.connectionString);

                eng.CreateDatabase();

                createTable();
            }
            readTable(l);
        }

        protected void openConnection()
        {
            con = new SqlCeConnection(Constants.connectionString);

            con.Open();
        }

        protected void closeConnection()
        {

            if (con != null)
            {
                con.Dispose();
            }
        }

        public void createTable()
        {
            openConnection();

            try
            {
                using (SqlCeCommand cmdCreate = new SqlCeCommand(Constants.createSql, con))
                {
                    cmdCreate.CommandType = System.Data.CommandType.Text;

                    cmdCreate.ExecuteNonQuery();
                }
            }
            catch (SqlCeException scee)
            {
                for (int curExNdx = 0; curExNdx < scee.Errors.Count; ++curExNdx)
                {
                    MessageBox.Show("Error:" + scee.Errors[curExNdx].ToString() + "\n");
                }
            }

            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Dispose();
                }
            }

            for (int i = 1; i < 11; i++)
                insertNewScore(i.ToString() + ".", 0);
        }

        public void readTable(ListView l)
        {
            openConnection();

            SqlCeCommand cmdGetInfo = null;
            SqlCeDataReader drInfo = null;

            try
            {
                cmdGetInfo = new SqlCeCommand("SELECT * FROM TopScore", con);
                cmdGetInfo.CommandType = CommandType.Text;
                drInfo = cmdGetInfo.ExecuteReader(CommandBehavior.Default);

                while (drInfo.Read())
                {
                    ListViewItem score = new ListViewItem(drInfo.GetString(0));
                    score.SubItems.Add(drInfo.GetInt32(1).ToString());
                    l.Items.Add(score);
                }
            }

            catch (SqlCeException scee)
            {
                for (int curExNdx = 0; curExNdx < scee.Errors.Count; ++curExNdx)
                {
                    System.Windows.Forms.MessageBox.Show("Error:" + scee.Errors[curExNdx].ToString() + "\n");
                }
            }

            finally
            {
                if (cmdGetInfo != null)
                    cmdGetInfo.Dispose();

                if (drInfo != null)
                    drInfo.Close();
            }
            closeConnection();
        }

        public void insertNewScore(string iD, int score)
        {
            openConnection();
            SqlCeCommand cmdInsert = new SqlCeCommand("INSERT INTO TopScore(Rank, Score) " +
                                                        "VALUES ('" + iD + "', " + score + ")", con);;
            try
            {
                cmdInsert = new SqlCeCommand("INSERT INTO TopScore(Rank, Score) " +
                                                        "VALUES ('" + iD + "', " + score + ")", con);
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.ExecuteNonQuery();
            }

            catch (SqlCeException scee)
            {
                for (int curNdx = 0; curNdx < scee.Errors.Count; ++curNdx)
                {
                    MessageBox.Show("Error:" + scee.Errors[curNdx].ToString() + "\n");
                }
            }

            finally
            {
                if (cmdInsert != null)
                    cmdInsert.Dispose();
            }
            closeConnection();
        }

        public void editScore(string iD, int score)
        {
            openConnection();

            try
            {
                using (SqlCeCommand cmdUpdate = new SqlCeCommand("Update TopScore set Score = " + score + " where ID = '" + iD + "'", con))
                {
                    cmdUpdate.CommandType = System.Data.CommandType.Text;

                    try
                    {
                        cmdUpdate.ExecuteNonQuery();
                    }

                    catch (Exception e)
                    {
                        throw (e);
                    }
                }
            }

            finally
            {
                closeConnection();
            }
        }

        public int[] getScores()
        {
            int[] temp = new int[10];
            int i = 0;
            openConnection();

            SqlCeCommand cmdGetInfo = null;
            SqlCeDataReader drInfo = null;

            try
            {
                cmdGetInfo = new SqlCeCommand("SELECT * FROM TopScore", con);
                cmdGetInfo.CommandType = CommandType.Text;
                drInfo = cmdGetInfo.ExecuteReader(CommandBehavior.Default);

                while (drInfo.Read())
                {
                    temp[i++] = drInfo.GetInt32(1);
                }
            }

            catch (SqlCeException scee)
            {
                for (int curExNdx = 0; curExNdx < scee.Errors.Count; ++curExNdx)
                {
                    System.Windows.Forms.MessageBox.Show("Error:" + scee.Errors[curExNdx].ToString() + "\n");
                }
            }

            finally
            {
                if (cmdGetInfo != null)
                    cmdGetInfo.Dispose();

                if (drInfo != null)
                    drInfo.Close();
            }
            closeConnection();
            return temp;
        }

        public int updateScore(int score)
        {

            int[] temp = new int[10];
            int rank = 10;
            temp = getScores();

            for (int i = 0; i < 10; i++)
                if (score == temp[i])
                {
                    rank = 10;
                    i = 10;
                }
                else
                    if (score > temp[i])
                    {
                        rank = i;
                        i = 10;
                    }
            if (rank < 10)
            {

                for (int i = 9; i > rank; i--)
                    temp[i] = temp[i - 1];

                temp[rank] = score;
                for(int j = rank; j < 10; j++)
                    editScore(j.ToString() + ".", temp[j]);
            }
            return rank;
        }
    }
}
