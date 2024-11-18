using redAcademy_ATM_OF_EXCELLENCE;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Services;
using System.Text.RegularExpressions;
using System.Threading;

namespace redAcademy_ATM_OF_EXCELLENCE
{
    public class Program
    {
        static void Main(string[] args)
        {
           
            Console.Title = "R.A ATM Of Excellence";//changes console name
            Methods.IntroLoadingScreen();// loading intro
            Menu1();//calls the menu method
           
        }

        public static void Menu1()
        {
            try
            {
                Methods.indent("WELCOME TO REDACADEMY'S ATM OF EXCELLENCE!");
                Console.WriteLine("Hey There! Choose to create a new profile or log in if you already have one.(type number only)\n\n1. Create New Profile\n\n2. Log In\n\n3. Exit Program");
                string menuOption = Console.ReadLine();

                if (menuOption == "1")
                {
                    Console.Clear();//clears the program
                    Methods.SignUp();
                }
                else if (menuOption == "2")
                {
                    Console.Clear();//clears the program
                    Methods.LogIn();
                }
                else if (menuOption == "3")
                {
                    Environment.Exit(0); // exits the program
                }
                else//input validation
                {
                    Console.WriteLine("Incorrect option! Try again");
                    Console.Clear();
                    Menu1();
                }

            }
            catch (Exception)//handles exceptions
            {
                Console.WriteLine("Incorrect option! Try again");
                Menu1();
            }
        }

        public static void Menu2()
        {
            Console.Clear();
            Methods.indent($"WELCOME {PublicVariables.usernameAns.ToUpper()}, TO REDACADEMY'S ATM OF EXCELLENCE");
            Validation.DisplayAccountNumber(); //displays accountNumber
            Validation.displayAccountType(); //Displayes accountType

            Methods.indent(DateTime.Now.ToString());//Displays Time
            Console.WriteLine("Please choose an option below (number only)\n\n1. Check Balance\n\n2. Withdraw\n\n3. Deposit\n\n4. Transfer Money\n\n5. View Transactions\n\n6. Log Out Of Account");
            string ans = Console.ReadLine();
           
            if (ans == "1")
            {
                PublicVariables.finalBalanceString = Validation.getBalance(); // Gets balance
                if (PublicVariables.finalBalanceString != null)
                {
                    Console.WriteLine($"Your balance for this account {PublicVariables.accountNumber} is: {PublicVariables.finalBalanceString}");//displays balance
                    Console.WriteLine("Press anywhere to continue...");
                    Console.ReadKey();
                    Menu2();
                }
                else // No balance found
                {
                    Console.WriteLine ("Balance not found");
                    Console.WriteLine("Press anywhere to continue...");
                    Console.ReadKey();
                    Menu2();
                }
            }
            else if (ans == "2")
            {
                while (true) //withdrawing
                {
                    try
                    {
                        Console.WriteLine("How much do you want to withdraw?");
                        string amountMoney = Console.ReadLine();

                        if (Regex.IsMatch(amountMoney, @"^[0-9]+$") || Regex.IsMatch(amountMoney, @"^-?\d*\.?\d+$")) //makes sure that is only numbers
                        {
                            if (Convert.ToDouble(amountMoney) > 0 && Convert.ToDouble(amountMoney) <= 3000) // must be over 0 and less or equal to 3000
                            {
                                PublicVariables.finalBalanceString = Validation.getBalance();
                                PublicVariables.finalBalance = Convert.ToDouble(PublicVariables.finalBalanceString); // converting string to double

                                if (Convert.ToDouble(amountMoney) > PublicVariables.finalBalance) // cant withdraw more than you have
                                {
                                    Console.WriteLine("You have insufficient funds");
                                    Console.WriteLine("Press any key to return to menu...");
                                    Console.ReadKey();
                                    Menu2();

                                }
                                else
                                {

                                    PublicVariables.finalBalance = PublicVariables.finalBalance - Convert.ToDouble(amountMoney);
                                    //updating account balance
                                    string query = $"UPDATE accounts SET balance = {PublicVariables.finalBalance} WHERE accountNumber = @accountNumber;";
                                    //inserting into transactions
                                    string query2 = $"INSERT INTO transactions (accountNumber,transactionType,amount, transactionDateTime)VALUES('{PublicVariables.accountNumber}','withdraw','{Convert.ToDouble(amountMoney)}','{DateTime.Now}')";

                                    //establishing connection
                                    using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
                                    {
                                        SqlCommand cmd = new SqlCommand(query, connection);

                                        connection.Open();
                                        cmd.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumber);
                                        cmd.ExecuteNonQuery();
                                        connection.Close();

                                        SqlCommand cmd1 = new SqlCommand(query2, connection);
                                        connection.Open();
                                        cmd1.ExecuteNonQuery();
                                        connection.Close();
                                        Methods.ShowLoadingScreen();
                                        Console.WriteLine("Thank you!");
                                        Console.WriteLine("Press any key to return to menu...");
                                        Console.ReadKey();
                                        Menu2();


                                    }
                                }

                            }
                            else if (Convert.ToDouble(amountMoney) < 0) //cannot withdraw negative
                            {
                                Console.WriteLine("Cannot withdraw negative numbers");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                            else if (Convert.ToDouble(amountMoney) > 3000) //cant withdraw more than 3000
                            {
                                Console.WriteLine("You can only withdraw less or equal than 3000");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                            else if (Convert.ToDouble(amountMoney) == 0) //cant withdraw 0
                            {
                                Console.WriteLine("You cannot withdraw 0 money");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                        }  
                    }
                    catch(Exception)
                    {

                    }
                    Console.WriteLine("Enter valid options!");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    Menu2();
                }
            }
            else if (ans == "3")
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("How much do you want to deposit?");
                        string amountMoney = Console.ReadLine();

                        if (Regex.IsMatch(amountMoney, @"^[0-9]+$") || Regex.IsMatch(amountMoney, @"^-?\d*\.?\d+$"))
                        {
                            if (Convert.ToDouble(amountMoney) > 0 && Convert.ToDouble(amountMoney) <= 3000)
                            {

                                PublicVariables.finalBalanceString = Validation.getBalance();
                                PublicVariables.finalBalance = Convert.ToDouble(PublicVariables.finalBalanceString);
                                PublicVariables.finalBalance = PublicVariables.finalBalance + Convert.ToDouble(amountMoney);
                                string query = $"UPDATE accounts SET balance = {PublicVariables.finalBalance} WHERE accountNumber = @accountNumber;";
                                string query2 = $"INSERT INTO transactions (accountNumber,transactionType,amount, transactionDateTime)VALUES('{PublicVariables.accountNumber}','deposit','{Convert.ToDouble(amountMoney)}','{DateTime.Now}')";

                                using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
                                {
                                    SqlCommand cmd = new SqlCommand(query, connection);
                                    connection.Open();
                                    cmd.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumber);
                                    cmd.ExecuteNonQuery();
                                    connection.Close();

                                    SqlCommand cmd1 = new SqlCommand(query2, connection);
                                    connection.Open();
                                    cmd1.ExecuteNonQuery();
                                    connection.Close();
                                    Methods.ShowLoadingScreen();
                                    Console.WriteLine("Thank you!");
                                    Console.WriteLine("Press any key to return to menu...");
                                    Console.ReadKey();
                                    Menu2();
                                }
                            }
                            else if (Convert.ToDouble(amountMoney) < 0)
                            {
                                Console.WriteLine("Cannot deposit negative numbers");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                            else if (Convert.ToDouble(amountMoney) > 3000)
                            {
                                Console.WriteLine("You can only deposit less or equal than 3000");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                            else if (Convert.ToDouble(amountMoney) == 0)
                            {
                                Console.WriteLine("You cannot deposit 0 money");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }                 
                    Console.WriteLine("Enter valid options!");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    Menu2();
                }
            }
            else if (ans == "4")
            {
                while (true)
                {
                    try
                    {
                        PublicVariables.finalBalanceString = Validation.getBalance();//gets user's balance
                        PublicVariables.finalBalance = Convert.ToDouble(PublicVariables.finalBalanceString);
                        Console.WriteLine("Please enter transfer amount");
                        string amount = Console.ReadLine();

                        if (Regex.IsMatch(amount, @"[0-9]"))
                        {
                            if (Convert.ToDouble(amount) > 0 && Convert.ToDouble(amount) <= 3000 && Convert.ToDouble(amount) <= PublicVariables.finalBalance)
                            {
                                while (true)
                                {
                                    Console.Write("Please enter accountnumber: ");
                                    PublicVariables.accountNumberForTransfer = Console.ReadLine();

                                    if (PublicVariables.accountNumberForTransfer != null)
                                    {
                                        // get the receipiant's accountnumber
                                        string query = "SELECT accountNumber FROM accounts WHERE accountNumber = @accountNumber";

                                        using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
                                        {

                                            SqlCommand command = new SqlCommand(query, connection);
                                            command.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumberForTransfer);


                                            connection.Open();
                                            object result = command.ExecuteScalar();
                                            connection.Close();

                                        }

                                        if (PublicVariables.accountNumberForTransfer == PublicVariables.accountNumber)
                                        {
                                            Console.WriteLine("You cant deposit to the same account!");
                                            Console.WriteLine("Press any key to return to menu...");
                                            Console.ReadKey();
                                            Menu2();
                                        }

                                        if (!String.IsNullOrEmpty(PublicVariables.accountNumberForTransfer))
                                        {

                                            string acc2 = Validation.getBalanceForTransfer(); //gets the balance
                                            PublicVariables.transferbalance = Convert.ToDouble(acc2);
                                            PublicVariables.transferbalance = PublicVariables.transferbalance + Convert.ToDouble(amount); //adding amount to the receiver's account 
                                            PublicVariables.finalBalanceString = Validation.getBalance();
                                            PublicVariables.finalBalance = Convert.ToDouble(PublicVariables.finalBalanceString);
                                            PublicVariables.finalBalance = PublicVariables.finalBalance - Convert.ToDouble(amount);

                                            string query5 = $"UPDATE accounts SET balance = {PublicVariables.finalBalance} WHERE accountNumber ='{PublicVariables.accountNumber}' ;";
                                            string query2 = $"UPDATE accounts SET balance = {PublicVariables.transferbalance} WHERE accountNumber ='{PublicVariables.accountNumberForTransfer}' ;";
                                            string query3 = $"INSERT INTO transactions (accountNumber,transactionType,amount, transactionDateTime)VALUES('{PublicVariables.accountNumber}','transfer','{Convert.ToDouble(amount)}','{DateTime.Now}')";

                                            using (SqlConnection connection = new SqlConnection(PublicVariables.stringConn))
                                            {
                                                SqlCommand cmd = new SqlCommand(query2, connection);

                                                connection.Open();
                                                cmd.Parameters.AddWithValue("@accountNumber", acc2);
                                                cmd.ExecuteNonQuery();
                                                connection.Close();

                                                SqlCommand cmd1 = new SqlCommand(query3, connection);
                                                connection.Open();
                                                cmd1.ExecuteNonQuery();
                                                connection.Close();

                                                SqlCommand cmd2 = new SqlCommand(query5, connection);
                                                connection.Open();
                                                cmd2.ExecuteNonQuery();
                                                connection.Close();
                                                Methods.ShowLoadingScreen();
                                                Console.WriteLine("Thank you!");
                                                Console.WriteLine("Press any key to return to menu...");
                                                Console.ReadKey();
                                                Menu2();
                                            }
                                        }
                                        else
                                        {
                                            Methods.indent("Username does not have an associated account number.");
                                            Console.WriteLine("Press any key to return to menu...");
                                            Console.ReadKey();
                                            Menu2();
                                        }
                                    }
                                    Console.WriteLine("Please enter valid account number");
                                }
                            }
                            else if (Convert.ToDouble(amount) < 0)
                            {
                                Console.WriteLine("Cannot transfer negative numbers");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                            else if (Convert.ToDouble(amount) > 3000)
                            {
                                Console.WriteLine("You can only transfer less or equal than 3000");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                            else if (Convert.ToDouble(amount) == 0)
                            {
                                Console.WriteLine("You cannot transfer 0 money");
                                Console.WriteLine("Press any key to return to menu...");
                                Console.ReadKey();
                                Menu2();
                            }
                        }
                      
                    }
                    catch (Exception)
                    {

                    }
                    Console.WriteLine("Please enter valid amount or accountNumber!");
                    Console.WriteLine("Press Any Key To Continue!...");
                    Console.ReadKey();
                    Menu2();
                }
            }
            else if (ans == "5")
            {
                while (true)
                {
                    try
                    {
                        //Displays transactions
                        string query = "SELECT transactionalID,accountNumber,TransactionType,amount,transactionDateTime FROM TRANSACTIONS WHERE accountNumber = @accountNumber";

                        using (SqlConnection conn = new SqlConnection(PublicVariables.stringConn))
                        {
                            try
                            {
                                conn.Open();

                                using (SqlCommand command = new SqlCommand(query, conn))
                                {
                                    command.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumber);

                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        Console.WriteLine("Transaction ID    ||    Type    ||     Balance    ||      Time    ");

                                        if (reader.HasRows) // if transactions do exist
                                        {
                                            while (reader.Read()) //reads the data
                                            {

                                                var tID = reader.GetInt32(0);
                                                var accountNumbe = reader.GetString(1);
                                                var tType = reader.GetString(2);
                                                var balance = reader.GetDouble(3);
                                                var time = reader.GetDateTime(4);

                                                Console.WriteLine($"{tID}                   {tType}        R{balance}           {time}");
                                            }
                                            Console.WriteLine("Press Any Key To Continue!...");
                                            Console.ReadKey();
                                            Menu2();
                                        }
                                        else
                                        {
                                            Console.WriteLine("No transactions found");
                                            Console.WriteLine("Press Any Key To Continue!...");
                                            Console.ReadKey();
                                            Menu2();
                                        }


                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("Press Any Key To Continue!...");
                                Console.ReadKey();
                                Menu2();
                            }



                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error");
                        Console.WriteLine("Press Any Key To Continue!...");
                        Console.ReadKey();
                        Menu2();
                    }




                }
            }
            else if(ans == "6") 
            {
                    Console.Clear();
                    Methods.NewAccount();   
            }
            else
            {
                Console.Clear();
                Console.WriteLine("****PLEASE ENTER VALID CHOICE****");
                Menu2();  
            }
        }
  
    }
}