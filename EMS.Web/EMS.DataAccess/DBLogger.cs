
/*
    * Filename: DBLogger.cs
    *
    * Description:
    * Holds the code behind for the logging class.
    *
    * Authors:
    * Kyle Marshall
    * Kyle Kreutzer
    *  Wes Thompson
    * Colin Mills
    *
    * Date: 2016-04-21
*/
    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace EMS
{
    
    /// <summary>
    /// Contains methods for logging text to a database
    /// log file.
    /// </summary>
    static class DbLogging
    {
        /// <summary>
        /// Logs the desired message to the log file.
        /// </summary>
        /// <param name="logString">The string to be logged to the file.</param>
        public static void Log(string logString)
        {
            /* Open streamWriter and log desired text */
using (StreamWriter writer = new StreamWriter("DBLog.txt", true))
            {
                writer.WriteLine(logString);
            }
        }

        /// <summary>
        /// Logs the message and the stack trace of an exception.
        /// </summary>
        /// <param name="ex">The exception to be logged.</param>
        public static void Log(Exception ex)
        {
            /* Open streamWriter and log stack trace and message */
            using (StreamWriter writer = new StreamWriter("DBLog.txt", true))
            {
                writer.WriteLine(ex.StackTrace);
            }
        }

    }
}