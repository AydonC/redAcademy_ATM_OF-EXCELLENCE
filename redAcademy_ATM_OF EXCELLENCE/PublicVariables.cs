using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace redAcademy_ATM_OF_EXCELLENCE
{
    public class PublicVariables
    {
        //Global variables are being declared
        public static string stringConn = @"Data Source=DESKTOP-9HB8HRN;Initial Catalog=Atm;Integrated Security=True;Encrypt=False";
        public static SqlConnection sqlConn = new SqlConnection(stringConn);
        public static string accountNumber;
        public static string usernameAns;
        public static string pinAns;
        public static string pinAns2;
        public static int accountNum2;
        public static string accountType;
        public static string fName;
        public static string sName;
        public static string accountPassword;
        public static double finalBalance;
        public static string finalBalanceString;
        public static double transferbalance;
        public static string accountNumberForTransfer;
        public static int counter;
        public static string teacherName;
        public static string maidenName;
        
    }
}
