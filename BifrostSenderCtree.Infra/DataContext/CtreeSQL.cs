using System;
using System.Data;
using BifrostSenderCtree.Domain.Interfaces.Infrastructure;
using CanaaCore.Utils;
using Ctree.Data.SqlClient;

namespace BifrostSenderCtree.Infra.DataContext
{
    public class CtreeSQL : IDisposable, IDatabase
    {
        public static CtreeSqlConnection Connection { get; set; }

        public IDbCommand Command { get; set; }

        private void Connect()
        {
            try
            {
                // initialize connection object
                Connection = new CtreeSqlConnection();
                Connection.ConnectionString = Envs.GetTypedEnvVariable<string>("DATABASE_CONNECTION_STRING",
                    "UID=promax;PWD=itax#2021;Database=ctreeSQL_GEO_D2;Server=172.22.6.83;Service=6570;");

                // initialize command object
                Command = new CtreeSqlCommand();
                Command.CommandType = System.Data.CommandType.Text;
                Command.Connection = Connection;
                Command.Transaction = null;

                // connect to server
                Console.WriteLine("\tLogon to server...");
                Connection.Open();
            }
            catch (CtreeSqlException e)
            {
                HandleException(e);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        public void Dispose()
        {
            Connection.Close();
        }

        public CtreeSQL()
        {
            Connect();
        }
        
        private void HandleException(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            ErrorExit();
        }

        private void HandleException(CtreeSqlException e)
        {
            int TABLE_ALREADY_EXISTS = -20041;
            int INDEX_ALREADY_EXISTS = -20028;

            if (e.ErrorNumber == TABLE_ALREADY_EXISTS ||
                e.ErrorNumber == INDEX_ALREADY_EXISTS)
                return;
            else
            {
                Console.WriteLine("Error: " + e.ErrorNumber + " - " + e.ErrorMessage);
                if (e.ErrorNumber == -30096 || e.ErrorNumber == -20212)
                    Console.WriteLine("Perhaps your c-tree server is not running?");
            }

            ErrorExit();
        }

        private void ErrorExit()
        {
            Console.WriteLine("*** Execution aborted *** \nPress <ENTER> key to exit...");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}