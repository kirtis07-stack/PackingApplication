using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Helper
{
    public class Logger
    {
        StreamWriter wr = null;
        string FileName = null;
        private static Logger Log = null;
        private Object FileObj = null;
        public Logger()
        {
            FileName = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Log.txt";
            FileObj = new Object();
        }

        public static Logger GetLogger()
        {
            if (Log == null)
            {
                Log = new Logger();
            }

            return Log;
        }

        public void writeMessage(string Msg)
        {
            try
            {
                if (File.Exists(FileName))
                {
                    lock (FileObj)
                    {
                        wr = new StreamWriter(FileName, true);
                        wr.WriteLine(DateTime.Now + " - " + Msg);
                        wr.Close();
                    }
                }

            }
            catch
            {

            }

        }
    }
}
