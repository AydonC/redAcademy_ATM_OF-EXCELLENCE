using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

using redAcademy_ATM_OF_EXCELLENCE;



namespace redAcademy_ATM_OF_EXCELLENCE
{
    public class Methods
    {
        public static void SignUp()
        {

            while (true) //invalid options will run the code again
            {
                try
                {
                    string fname = "Please Enter first Name: ";
                    Console.Write("\n \n" + fname.PadLeft(fname.Length + 40)); //moves the string to the middle
                    PublicVariables.fName = Console.ReadLine();
                    if (Regex.IsMatch(PublicVariables.fName, @"^[a-zA-Z]+$") && !string.IsNullOrEmpty(PublicVariables.fName)) //using regular expressions to make sure the block only accepts letters and no empty spaces 
                    {
                        break;
                    }

                }
                catch (Exception)
                {

                }

                indent("Enter Valid Name!");

            }


            while (true)
            {
                try
                {
                    string sname = "Please Enter Last Name: ";
                    Console.Write("\n \n" + sname.PadLeft(sname.Length + 40));
                    PublicVariables.sName = Console.ReadLine();
                    if (Regex.IsMatch(PublicVariables.sName, @"^[a-zA-Z]+$") && !string.IsNullOrEmpty(PublicVariables.sName))
                    {
                        break;
                    }

                }
                catch (Exception)
                {

                }
                indent("Enter Valid Surname!");

            }


            while (true)
            {
                try
                {
                    string username = "Please Create Username: ";
                    Console.Write("\n \n" + username.PadLeft(username.Length + 40));
                    PublicVariables.usernameAns = Console.ReadLine();
                    bool isTaken = Validation.UsernameDatabaseCheck(); // checks if userName is taken

                    if (isTaken)
                    {
                        indent("Username is already taken.");


                    }
                    else if (!isTaken && !string.IsNullOrEmpty(PublicVariables.usernameAns) && PublicVariables.usernameAns.Any(char.IsLetter) && PublicVariables.usernameAns.Length >=3) //userName is not taken
                    {
                        indent($"Welcome {PublicVariables.usernameAns}");
                        break;
                    }
                }
                catch (Exception)
                {
                    
                }
                indent("Username must be more or equal to 3 letters and must contain letters!");
            }
            while (true)
            {
                try
                {
                    string tname = "Please enter surname of your first grade teacher: ";
                    Console.Write("\n \n" + tname.PadLeft(tname.Length + 40));
                    PublicVariables.teacherName = Console.ReadLine();
                    if (Regex.IsMatch(PublicVariables.teacherName, @"^[a-zA-Z]+$") && !string.IsNullOrEmpty(PublicVariables.teacherName))
                    {
                        break;
                    }

                }
                catch (Exception)
                {

                }
                indent("Enter Valid Surname!");

            }
            while (true)
            {
                try
                {
                    string maidenName = "Please enter your mother's maiden name: ";
                    Console.Write("\n \n" + maidenName.PadLeft(maidenName.Length + 40));
                    PublicVariables.maidenName = Console.ReadLine();
                    if (Regex.IsMatch(PublicVariables.maidenName, @"^[a-zA-Z]+$") && !string.IsNullOrEmpty(PublicVariables.maidenName))
                    {
                        break;
                    }

                }
                catch (Exception)
                {

                }
                indent("Enter Valid name!");

            }
            while (true)
            {
                try
                {
                    string pin = "Please Enter Desired 4 Number Pin : ";
                    Console.Write("\n \n" + pin.PadLeft(pin.Length + 40));
                    string pinTemp = "";
                    ConsoleKeyInfo key;
                    //Hides the pin from user
                    do
                    {
                        key = Console.ReadKey(true);
                        if (key.Key != ConsoleKey.Backspace)
                        {
                            pinTemp += key.KeyChar;
                            Console.Write("*");
                        }
                        else
                        {
                            if (pinTemp.Length > 0)
                            {
                                pinTemp = pinTemp.Substring(0, pinTemp.Length - 1);
                                Console.Write("\b \b");
                            }
                        }
                    } while (key.Key != ConsoleKey.Enter);

                    PublicVariables.pinAns = pinTemp.Trim(); //sets global variable to the temporary pin


                    if (PublicVariables.pinAns.Length == 4 && Regex.IsMatch(PublicVariables.pinAns, @"^[0-9]+$")) //validating pin
                    {
                        while (true)
                        {
                            try
                            {
                                string pin2 = "Please re-enter same pin: "; //extra security
                                Console.Write("\n \n" + pin2.PadLeft(pin2.Length + 40));
                                string pinTemp2 = "";
                                ConsoleKeyInfo key1;
                                do
                                {
                                    key1 = Console.ReadKey(true);
                                    if (key1.Key != ConsoleKey.Backspace)
                                    {
                                        pinTemp2 += key1.KeyChar;
                                        Console.Write("*");
                                    }
                                    else
                                    {
                                        if (pinTemp2.Length > 0)
                                        {
                                            pinTemp2 = pinTemp2.Substring(0, pinTemp2.Length - 1);
                                            Console.Write("\b \b");
                                        }
                                    }
                                } while (key1.Key != ConsoleKey.Enter);

                                PublicVariables.pinAns2 = pinTemp2.Trim();
                                while (PublicVariables.pinAns2 != PublicVariables.pinAns) //pins is not matching
                                {
                                    string pinwa = "Pins does not match!,Try again! : ";
                                    Console.Write("\n \n" + pinwa.PadLeft(pinwa.Length + 40));
                                    string pinTemp3 = "";
                                    ConsoleKeyInfo key2;
                                    do
                                    {
                                        key2 = Console.ReadKey(true);
                                        if (key2.Key != ConsoleKey.Backspace)
                                        {
                                            pinTemp3 += key2.KeyChar;
                                            Console.Write("*");
                                        }
                                        else
                                        {
                                            if (pinTemp3.Length > 0)
                                            {
                                                pinTemp3 = pinTemp3.Substring(0, pinTemp3.Length - 1);
                                                Console.Write("\b \b");
                                            }

                                        }
                                    } while (key2.Key != ConsoleKey.Enter);

                                    PublicVariables.pinAns2 = pinTemp3.Trim();

                                }
                                break;

                            }
                            catch (Exception)
                            {

                            }
                            Console.WriteLine(" ");
                            indent("Enter valid digits!");
                        }



                        Console.Clear();
                        Console.WriteLine("\n\n\n");
                        int pinAns2I = (int)Convert.ToInt64(PublicVariables.pinAns2);


                        PublicVariables.sqlConn.Open();
                        SqlCommand cmd = PublicVariables.sqlConn.CreateCommand();
                        cmd.CommandText = $@"INSERT INTO users (fName,sName,userName,encryptedPIN,teachername,maidenName)VALUES('{PublicVariables.fName}','{PublicVariables.sName}','{PublicVariables.usernameAns}','{pinAns2I}','{PublicVariables.teacherName}','{PublicVariables.maidenName}')";
                        //inserting into the database 
                        cmd.ExecuteNonQuery();
                        PublicVariables.sqlConn.Close();
                        indent($"Thank you! Your Profile has been successfully created!");
                        indent($"Full Name: {PublicVariables.fName} {PublicVariables.sName}");
                        indent($"Username:{PublicVariables.usernameAns}");
                        indent("Press Any Key To Continue!...");
                        Console.ReadKey();
                        Console.Clear();
                        NewAccount();


                    }
                }
                catch (Exception)
                {

                }
                Console.WriteLine("");
                indent("Pin must be 4 digits!");

            }
        }

        public static void LogIn()
        {
            indent("WELCOME TO REDACADEMY'S ATM OF EXCELLENCE!");
            indent("  Please Log In");

            while (true)
            {
                try
                {
                    string username = "Please Enter Username: ";
                    Console.Write("\n \n" + username.PadLeft(username.Length + 40));
                    PublicVariables.usernameAns = Console.ReadLine();
                    bool isTaken = Validation.UsernameDatabaseCheck();

                    if (isTaken)
                    {
                        indent($"Welcome {PublicVariables.usernameAns}");
                        break;

                    }
                    else
                    {
                        Console.Clear();
                        while (true)
                        {
                            try
                            {
                                string valid = "****User does not exist, try again, or do you want to create a new account?(Y/N)?****\n";
                                Console.Write("\n \n" + valid.PadLeft(valid.Length + 15));
                                string validAns = Console.ReadLine();
                                if (!string.IsNullOrEmpty(validAns))
                                {
                                    validAns = validAns.ToLower();
                                    if (validAns == "y")
                                    {
                                        Console.Clear();
                                        Program.Menu1();
                                    }
                                    else if (validAns == "n")
                                    {
                                        Console.Clear();
                                        LogIn();
                                    }
                                }

                            }
                            catch (Exception)
                            {

                            }

                            indent("Enter valid option!");

                        }

                    }
                }
                catch (Exception)
                {
                    indent("Enter valid username!");
                }
            }

            int counter = 0;

            while (counter < 3)
            {
                try
                {
                    string password = "Please Enter PIN Number: ";
                    Console.Write("\n \n" + password.PadLeft(password.Length + 40));

                    string pinTemp2 = "";
                    ConsoleKeyInfo key1;
                    do
                    {
                        key1 = Console.ReadKey(true);
                        if (key1.Key != ConsoleKey.Backspace)
                        {
                            pinTemp2 += key1.KeyChar;
                            Console.Write("*");
                        }
                        else
                        {
                            if (pinTemp2.Length > 0)
                            {
                                pinTemp2 = pinTemp2.Substring(0, pinTemp2.Length - 1);
                                Console.Write("\b \b");
                            }
                        }
                    } while (key1.Key != ConsoleKey.Enter);

                    PublicVariables.pinAns2 = pinTemp2.Trim();

                    bool isTaken = Validation.PinDatabaseCheck();

                    if (isTaken)
                    {
                        indent("Pin is correct!.");
                        NewAccount();

                    }
                    else
                    {
                        Console.WriteLine(" ");

                        counter++;
                        if (counter == 3)
                        {
                            while (true)
                            {
                                Console.WriteLine("Forgot Password?\n Press Y to change or N to go back");
                                string forgot = Console.ReadLine();
                                if (!string.IsNullOrEmpty(forgot))
                                {
                                    forgot = forgot.ToUpper();
                                    if (forgot == "Y")
                                    {
                                        securityQuestions();
                                        while (true)
                                        {
                                            try
                                            {


                                                string password1 = "Please Enter New PIN Number: ";
                                                Console.Write("\n \n" + password1.PadLeft(password1.Length + 40));

                                                string pinTemp21 = "";
                                                ConsoleKeyInfo key11;
                                                do
                                                {
                                                    key11 = Console.ReadKey(true);
                                                    if (key11.Key != ConsoleKey.Backspace)
                                                    {
                                                        pinTemp21 += key11.KeyChar;
                                                        Console.Write("*");
                                                    }
                                                    else
                                                    {
                                                        if (pinTemp21.Length > 0)
                                                        {
                                                            pinTemp21 = pinTemp21.Substring(0, pinTemp21.Length - 1);
                                                            Console.Write("\b \b");
                                                        }
                                                    }
                                                } while (key11.Key != ConsoleKey.Enter);

                                                PublicVariables.pinAns2 = pinTemp21.Trim();

                                                if (PublicVariables.pinAns2.Length == 4 && Regex.IsMatch(PublicVariables.pinAns2, @"^[0-9]+$"))
                                                {
                                                    string query = $"UPDATE users SET encryptedPin = '{PublicVariables.pinAns2}' WHERE  userName = @userName";

                                                    using (SqlConnection conn = new SqlConnection(PublicVariables.stringConn))
                                                    {
                                                        conn.Open();
                                                        SqlCommand cmd = new SqlCommand(query, conn);

                                                        cmd.Parameters.AddWithValue("@userName", PublicVariables.usernameAns);
                                                        cmd.ExecuteNonQuery();
                                                        conn.Close();
                                                    }
                                                    Console.WriteLine();
                                                    Console.WriteLine("Your password has been changed successfully!");
                                                    Console.WriteLine("Press any key to continue...");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                    LogIn();
                                                }
                                            }
                                            catch(Exception)
                                            {

                                            }
                                            Console.WriteLine("Pin must be 4 digits!");

                                        }
                                    }
                                    else if (forgot == "N")
                                    {
                                        Console.Clear();
                                        LogIn();
                                    }
                                }
                                Console.WriteLine(" ");
                                indent("Your pin is not correct, please try again");
                            }

                        }
                    }


                }
                catch (Exception)
                {

                }
                Console.WriteLine(" ");
                indent("Your pin is not correct, please try again");
            }



        }

        public static void ShowLoadingScreen()
        {
            Console.Write("Processing your request, please wait :) ");
            int loadingTime = 3000;
            int interval = 500;
            int totalIntervals = loadingTime / interval;

            for (int i = 0; i < totalIntervals; i++)
            {
                Console.Write(".");
                Thread.Sleep(interval);
            }

            Console.WriteLine();
            Console.WriteLine("Success!");
            Thread.Sleep(2000);

        }

        public static void IntroLoadingScreen()
        {
            string intro = "Launching The ATM Of Excellence ";
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n" + intro.PadLeft(intro.Length + 40));
            int loadingTime = 3000;
            int interval = 500;
            int totalIntervals = loadingTime / interval;

            for (int i = 0; i < totalIntervals; i++)
            {
                Console.Write(".");
                Thread.Sleep(interval);
            }

            Console.WriteLine();
            string success = "Success!";
            Console.WriteLine(success.PadLeft(success.Length + 55));
            Thread.Sleep(2000);
            Console.Clear();

        }

        public static void AccountType(string accountType)
        {
            while (true)
            {
                try
                {
                    string pass1 = "Provide Pin for new account: ";
                    Console.Write("\n \n" + pass1.PadLeft(pass1.Length + 30));
                    string pinTemp3 = "";
                    ConsoleKeyInfo key2;
                    do
                    {
                        key2 = Console.ReadKey(true);
                        if (key2.Key != ConsoleKey.Backspace)
                        {
                            pinTemp3 += key2.KeyChar;
                            Console.Write("*");
                        }
                        else
                        {
                            if (pinTemp3.Length > 0)
                            {
                                pinTemp3 = pinTemp3.Substring(0, pinTemp3.Length - 1);
                                Console.Write("\b \b");
                            }

                        }
                    } while (key2.Key != ConsoleKey.Enter);


                    PublicVariables.accountPassword = pinTemp3.Trim();

                    if (PublicVariables.accountPassword.Length == 4 && Regex.IsMatch(PublicVariables.accountPassword, @"^[0-9]+$"))
                    {
                        break;
                    }
                }
                catch (Exception)
                {

                } 
                
                Console.WriteLine(" ");
                indent("Pin must be 4 digits!");
            }



            PublicVariables.accountNum2 = (int)Convert.ToInt64(accountNumberGenerator());
            SqlCommand cmd = PublicVariables.sqlConn.CreateCommand();
            PublicVariables.sqlConn.Open();
            cmd.CommandText = $@"INSERT INTO accounts(accountNumber,userName,balance,accountType,accountPin)VALUES('{PublicVariables.accountNumber}','{PublicVariables.usernameAns}','0.00','{accountType}','{PublicVariables.accountPassword}')";
            cmd.ExecuteNonQuery();
            PublicVariables.sqlConn.Close();
            Console.Clear();
            indent($"\n\n\n");
            Console.WriteLine("\t\t\t\t\tThank you! Your account has been successfully created!");
            indent($"Username:{PublicVariables.usernameAns}");
            indent($"Account Number: {PublicVariables.accountNum2}");
            indent($"Account Type: {accountType}");
            indent("Press Any Key To Continue!...");
            Console.ReadKey();
            Program.Menu2();
            
        }

        public static void NewAccount()
        {
            Console.Clear();
            indent($"WELCOME {PublicVariables.usernameAns.ToUpper()},TO REDACADEMY'S ATM OF EXCELLENCE");
            while (true)
            {
                Console.WriteLine("Select a number...\n\n1.Create new Transaction Account\n\n2.Log into a Transaction account\n\n3.View Accounts\n\n4.Log Out");
                string ans = Console.ReadLine();
                if (ans == "1")
                {
                    while (true)
                    {
                        try
                        {
                            string type = "1.Cheque or 2.Savings(Only Provide Number) : ";
                            Console.Write("\n \n" + type.PadLeft(type.Length + 30));
                            string typeAns = Console.ReadLine();

                            if (typeAns == "1")
                            {
                                Methods.AccountType("Cheque");
                            }
                            else if (typeAns == "2")
                            {
                                Methods.AccountType("Savings");
                            }
                        }
                        catch (Exception)
                        {

                        }
                        Console.WriteLine(" ");
                        indent("Please Provide Correct Option!");
                    }
                }
                else if (ans == "2")
                {
                    while (true)
                    {
                        try
                        {
                            string type = "Please enter account number: ";
                            Console.Write("\n \n" + type.PadLeft(type.Length + 30));
                            PublicVariables.accountNumber = Console.ReadLine();
                            string accountNum = Validation.GetAccountNumber();

                            if (PublicVariables.accountNumber != null)
                            {
                                if (accountNum == PublicVariables.accountNumber)
                                {
                                    indent($"Account Number for {PublicVariables.usernameAns}: {PublicVariables.accountNumber}");
                                    int counter = 0;
                                    while (true)
                                    {
                                        try
                                        {


                                            string pass = "Please enter pin: ";
                                            Console.Write("\n \n" + pass.PadLeft(pass.Length + 30));
                                            string pinTemp3 = "";
                                            ConsoleKeyInfo key2;
                                            do
                                            {
                                                key2 = Console.ReadKey(true);
                                                if (key2.Key != ConsoleKey.Backspace)
                                                {
                                                    pinTemp3 += key2.KeyChar;
                                                    Console.Write("*");
                                                }
                                                else
                                                {
                                                    if (pinTemp3.Length > 0)
                                                    {
                                                        pinTemp3 = pinTemp3.Substring(0, pinTemp3.Length - 1);
                                                        Console.Write("\b \b");
                                                    }

                                                }
                                            } while (key2.Key != ConsoleKey.Enter);

                                            PublicVariables.accountPassword = pinTemp3.Trim();

                                            bool isTaken = Validation.CheckPinForLogin();

                                            if (isTaken)
                                            {
                                                indent("Pin is correct!.");
                                                Program.Menu2();
                                            }
                                            else
                                            {
                                                Console.WriteLine(" ");
                                                indent("Your pin is not correct, please try again");
                                                counter++;
                                                if (counter == 3)
                                                {
                                                    while (true)
                                                    {
                                                        Console.WriteLine("Forgot Password?\n Press Y to change or N to go back");
                                                        string forgot = Console.ReadLine();
                                                        if (!string.IsNullOrEmpty(forgot))
                                                        {
                                                            forgot = forgot.ToUpper();
                                                            if (forgot == "Y")
                                                            {
                                                                securityQuestions();
                                                                while (true)
                                                                {
                                                                    try
                                                                    {
                                                                        string password1 = "Please Enter New PIN Number: ";
                                                                        Console.Write("\n \n" + password1.PadLeft(password1.Length + 40));

                                                                        string pinTemp21 = "";
                                                                        ConsoleKeyInfo key11;
                                                                        do
                                                                        {
                                                                            key11 = Console.ReadKey(true);
                                                                            if (key11.Key != ConsoleKey.Backspace)
                                                                            {
                                                                                pinTemp21 += key11.KeyChar;
                                                                                Console.Write("*");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (pinTemp21.Length > 0)
                                                                                {
                                                                                    pinTemp21 = pinTemp21.Substring(0, pinTemp21.Length - 1);
                                                                                    Console.Write("\b \b");
                                                                                }
                                                                            }
                                                                        } while (key11.Key != ConsoleKey.Enter);

                                                                        PublicVariables.accountPassword = pinTemp21.Trim();

                                                                        if (PublicVariables.accountPassword.Length == 4 && Regex.IsMatch(PublicVariables.accountPassword, @"^[0-9]+$"))
                                                                        {
                                                                            string query = $"UPDATE accounts SET accountPin = '{PublicVariables.accountPassword}' WHERE  accountNumber = @accountNumber";
                                                                            using (SqlConnection conn = new SqlConnection(PublicVariables.stringConn))
                                                                            {
                                                                                conn.Open();
                                                                                SqlCommand cmd = new SqlCommand(query, conn);

                                                                                cmd.Parameters.AddWithValue("@accountNumber", PublicVariables.accountNumber);
                                                                                cmd.ExecuteNonQuery();
                                                                                conn.Close();
                                                                            }
                                                                            Console.WriteLine();
                                                                            Console.WriteLine("Your password has been changed successfully!");
                                                                            Console.WriteLine("Press any key to continue...");
                                                                            Console.ReadKey();
                                                                            Console.Clear();
                                                                            NewAccount();
                                                                        }
                                                                    }
                                                                    catch (Exception)
                                                                    {

                                                                    }
                                                                    Console.WriteLine(" ");
                                                                    indent("Pin must be 4 digits!");
                                                                }
                                                            }
                                                            else if (forgot == "N")
                                                            {
                                                                Console.Clear();
                                                                NewAccount();
                                                            }
                                                        }
                                                        Console.WriteLine(" ");
                                                        indent("Enter Valid Option!");
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine(" ");
                                            indent("Your pin is not correct, please try again");
                                        }  
                                    }
                                }
                                else
                                {
                                    indent("Username does not have an associated account number.Try again");
                                    indent("Press Any Key To Continue!...");
                                    Console.ReadKey();
                                    NewAccount();
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                        indent("Please provide correct options");
                        indent("Press Any Key To Continue!...");
                        Console.ReadKey();
                        NewAccount();
                    }
                }
                else if (ans == "3")
                {
                    //displays accounts
                    string query = $"Select accountNumber,userName,balance,accountType from Accounts where userName = '{PublicVariables.usernameAns}'";
                    using (SqlConnection conn = new SqlConnection(PublicVariables.stringConn))
                    {
                        Console.WriteLine("Account Number || User Name || Balance || Account Type "); // alligning headings
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) // if accounts do exist
                        {
                            while (reader.Read())
                            {
                                string accountNumb = reader.GetString(0);
                                string userName = reader.GetString(1);
                                double balance = reader.GetDouble(2);
                                string accountType = reader.GetString(3);

                                Console.WriteLine($"{accountNumb},        {userName},      R{balance},        {accountType}"); //displays information
                            }
                            Console.WriteLine("Press anywhere to continue...");
                            Console.ReadKey();
                            NewAccount();
                        }
                        else //no account found
                        {
                            Console.WriteLine("No accounts found");
                            Console.WriteLine("Press anywhere to continue...");
                            Console.ReadKey();
                            NewAccount();
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
                else if(ans == "4")
                {
                    Console.Clear();   
                    Program.Menu1();
                }
                indent("Please Provide Correct Option!");
            }
        }

        public static void indent(string message)//moves string to middle
        {
            Console.WriteLine(message.PadLeft(message.Length + 40));
        }

        public static string accountNumberGenerator()//Generates random accountnumber
        {
            Random random = new Random();
            PublicVariables.accountNumber = "";

            for (int i = 1; i < 10; i++)
            {
                PublicVariables.accountNumber += random.Next(1, 9);
            }
            return PublicVariables.accountNumber;
        }

        static void securityQuestions()
        {
            Console.Clear();
            int countdown = 2;
            int count = 0;
            while (count < 3)
            {
                Console.Write("Enter Surname of your first grade teacher: ");
                string tName = Console.ReadLine();

                if (!string.IsNullOrEmpty(tName))
                {
                    tName = tName.ToLower();
                    string ans = Validation.getTeacherName().ToLower();
                    if (tName == ans)
                    {
                        Console.WriteLine("Correct!");
                        break;
                    }
                }

                count++;
                Console.WriteLine($"You have {countdown--} attempts left!");

            }
            if (count == 3)
            {
                int countdownS = 10;

                while (countdownS > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Atm is locked and security has been alerted.\nProgram will return to main menu after cooldown.");
                    Console.WriteLine($"Time left: {countdownS} seconds");
                    Thread.Sleep(1000); // Sleep for 1 second
                    countdownS--;

                }
                Console.WriteLine("Redirecting to Main Menu...");
                Thread.Sleep(3000);
                Console.Clear();
                Program.Menu1();
            }
            countdown = 2;
            count = 0;
            while (count < 3)
            {
                Console.Write("Enter your mother's maiden name: ");
                string mName = Console.ReadLine();

                if (!string.IsNullOrEmpty(mName))
                {
                    mName = mName.ToLower();
                    string ans = Validation.getMaidenName().ToLower();
                    if (mName == ans)
                    {
                        Console.WriteLine("Correct!");
                        break;
                    }
                }

                count++;
                Console.WriteLine($"You have {countdown--} attempts left!");

            }
            if (count == 3)
            {
                int countdownS = 10;

                while (countdownS > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Atm is locked and security has been alerted.\nProgram will return to main menu after cooldown.");
                    Console.WriteLine($"Time left: {countdownS} seconds");
                    Thread.Sleep(1000); // Sleep for 1 second
                    countdownS--;

                }
                Console.WriteLine("Redirecting to Main Menu...");
                Thread.Sleep(3000);
                Console.Clear();
                Program.Menu1();
            }
        }

    }
}
