using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace redAcademy_ATM_OF_EXCELLENCE
{
    public class Validation
    {
        public static bool UsernameDatabaseCheck()//checks if username is valid
        {
            string query = "SELECT COUNT(*) FROM Users WHERE userName = @userName COLLATE SQL_Latin1_General_CP1_CS_AS";

            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userName", PublicVariables.usernameAns);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0; //if higher then true
            }
        }

        public static bool PinDatabaseCheck()//checks if pin is correct for account
        {
            string query = "SELECT COUNT(*) FROM users WHERE userName = @userName AND encryptedPIN = @encryptedPIN";

            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@encryptedPIN", PublicVariables.pinAns2);

                connection.Open();
                command.Parameters.AddWithValue("@Username", PublicVariables.usernameAns);
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        public static void DisplayAccountNumber()//Displays account
        {

            PublicVariables.accountNumber = GetAccountNumber(); //gets accountNumber from method

            if (!string.IsNullOrEmpty(PublicVariables.accountNumber))
            {
                Methods.indent($"Account Number for {PublicVariables.usernameAns}: {PublicVariables.accountNumber}");

            }
            else
            {
                Methods.indent("Username does not have an associated account number.");
            }
        }

        public static void ShowAccountNumberForLogIn()// Shows accountNumber for log in
        {
            PublicVariables.accountNumber = GetAccountNumber();

            if (!string.IsNullOrEmpty(PublicVariables.accountNumber))
            {
                Methods.indent($"Welcome {PublicVariables.accountNumber}!");

            }
            else
            {
                Methods.indent("Username not found or does not have an associated account number.");
                Methods.indent("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                Methods.NewAccount();

            }
        }

        public static void displayAccountType()//displays account type
        {
            string accountType = getAccountType(); //Cheque or Savings

            if (!string.IsNullOrEmpty(accountType))
            {
                Methods.indent($"AccountType for {PublicVariables.accountNumber}: {accountType}");//Displays accountType
            }
            else
            {
                Methods.indent("Account Type not available");
            }
        }

        public static bool CheckPinForLogin()//validates pin for login
        {
            string query = "SELECT COUNT(*) FROM accounts WHERE accountNumber = @accountNumber AND accountPin = @accountPin";

            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountPin", PublicVariables.accountPassword);

                connection.Open();
                command.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumber);
                object count = command.ExecuteScalar();

                return (int)count > 0;
            }
        }

        public static string getBalanceForTransfer()//get balance for the account you want to transfer to
        {
            string query = "Select balance from accounts where accountNumber = @accountNumber";
            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumberForTransfer);
                object result = command.ExecuteScalar();
                connection.Close();
                return result != null ? result.ToString() : null;
            }
        }

        public static string getAccountType()//Gets account Type for cheque or savings
        {
            string query = "SELECT accountType FROM accounts WHERE accountNumber = @accountNumber";

            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumber);


                connection.Open();
                object result = command.ExecuteScalar();

                return result != null ? result.ToString() : null;
            }
        }

        public static string getBalance()//Gets balance
        {
            string query = "Select balance from accounts where accountNumber = @accountNumber";
            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumber);
                object result = command.ExecuteScalar();
                connection.Close();
                return result != null ? result.ToString() : null;
            }
        }

        public static string GetAccountNumber()//gets account Number
        {
            string query = "SELECT accountNumber FROM accounts WHERE userName = @userName and accountNumber = @accountNumber";

            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userName", PublicVariables.usernameAns);
                command.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumber);

                connection.Open();
                object result = command.ExecuteScalar();

                return result != null ? result.ToString() : null;
            }
        }

        public static string getTeacherName()//gets account Number
        {
            string query = "SELECT teachername FROM users WHERE userName = @userName";

            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userName", PublicVariables.usernameAns);

                connection.Open();
                object result = command.ExecuteScalar();

                return result != null ? result.ToString() : null;
            }
        }

        public static string getMaidenName()//gets account Number
        {
            string query = "SELECT maidenName FROM users WHERE userName = @userName";

            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userName", PublicVariables.usernameAns);

                connection.Open();
                object result = command.ExecuteScalar();

                return result != null ? result.ToString() : null;
            }
        }



    }
}
