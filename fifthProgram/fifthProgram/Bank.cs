// Some of the code for:
//      Account.cs
//      Transaction.cs
//      SavingAccount.cs
//      CheckingAccount.cs
//      and Customer.cs
// is taken or based off of the code shown by the teacher (Wei Gong) in class, March 8th, 2018

using System;
using System.Collections.Generic;
using fifthProgram.Entities;

namespace fifthProgram
{
    class Bank
    {
        static List<Customer> accounts = new List<Customer>();
        static void Main(string[] args)
        {
            // Name
            string name = "";
            do
            {
                Console.Write("Enter customer name: ");
                name = Console.ReadLine();
                if (name != "")
                {
                    // Initial deposit
                    double initialDeposit = 0;
                    do
                    {
                        Console.Write("Enter " + name + "'s Initial Deposit Amount: ");
                        try
                        {
                            initialDeposit = double.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.Write("\nYou did not enter a number!\n\n");
                        }

                    } while (initialDeposit == 0);

                    Console.Write("\n");

                    // Add account
                    Customer customer = new Customer(name);
                    accounts.Add(customer);
                    Transaction tran = new Transaction(initialDeposit, TransactionType.DEPOSIT);
                    customer.Saving.Deposit(tran);
                }
                else
                {
                    if (accounts.Count < 1)
                    {
                        name = " ";
                        Console.WriteLine("\nYou did not enter a name!\n");
                    }
                }
            } while (name != "");

            // Select a customer
            Console.WriteLine("\nSelect one of the following customers: ");

            // List accounts
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine(i + ". Customer " + accounts[i].Name + ", current status " + accounts[i].Status);
            }

            // Select a customer + error handling
            int n = 0;
            int numOfAccounts = accounts.Count - 1;
            do
            {
                Console.WriteLine("\nEnter your selection 0 to " + numOfAccounts + ": ");
                try
                {
                    n = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("You did not enter a number!");
                }
            } while (n > numOfAccounts || n < 0);

            // Selected account
            Console.WriteLine("\nWelcome " + accounts[n].Name + "! You are currently our " + accounts[n].Status + " customer.");

            // Activities
            int selection = 0;
            do
            {
                Console.WriteLine("\nSelect one of the following activities: \n");
                Console.WriteLine("1. Deposit ...");
                Console.WriteLine("2. Withdraw ...");
                Console.WriteLine("3. Transfer ...");
                Console.WriteLine("4. Balance Enquiry ...");
                Console.WriteLine("5. Account Activity Enquiry ...");
                Console.WriteLine("6. Exit\n");
                Console.Write("Enter your selection (1 to 6): ");
                try
                {
                    selection = int.Parse(Console.ReadLine());
                    if (selection > 6 || selection == 0)
                    {
                        Console.WriteLine("\nThe number must be between 1 and 6.");
                    }
                }
                catch
                {
                    Console.WriteLine("\nYou did not enter a number!\n");
                }

                // Deposit
                if (selection == 1)
                {

                    // Choice
                    int accountType = SelectAccount();

                    // Amount
                    double amount = ValidateAmount();

                    if (accountType == 1)
                    {
                        Transaction tran = new Transaction(amount, TransactionType.DEPOSIT);
                        accounts[n].Checking.Deposit(tran);
                        Console.WriteLine("\nDeposit complete, account current balance: $" + accounts[n].Checking.Balance + "\n");
                    }
                    if (accountType == 2)
                    {
                        Transaction tran = new Transaction(amount, TransactionType.DEPOSIT);
                        accounts[n].Saving.Deposit(tran);
                        Console.WriteLine("\nDeposit complete, account current balance: $" + accounts[n].Saving.Balance + "\n");
                    }

                    // Withdraw
                }
                else if (selection == 2)
                {
                    // Choice
                    int accountType = SelectAccount();

                    // Amount
                    double amount = ValidateAmount();

                    if (accountType == 1)
                    {
                        Transaction tran = new Transaction(amount, TransactionType.WITHDRAW);
                        TransactionResult result = accounts[n].Checking.Withdraw(tran);
                        if (result != TransactionResult.SUCCESS)
                        {
                            Console.WriteLine("\nWithdraw canceled: " + result);
                        }
                        else
                        {
                            Console.WriteLine("\nWithdraw complete, account current balance: $" + accounts[n].Checking.Balance);
                        }
                    }
                    if (accountType == 2)
                    {
                        Transaction tran = new Transaction(amount, TransactionType.WITHDRAW);
                        TransactionResult result = accounts[n].Saving.Withdraw(tran);
                        if (result != TransactionResult.SUCCESS)
                        {
                            Console.WriteLine("\nWithdraw canceled: " + result + "\n");
                        }
                        else
                        {
                            Console.WriteLine("\nWithdraw complete, account current balance: $" + accounts[n].Saving.Balance + "\n");
                        }
                    }
                    
                // Transfer
                }
                else if (selection == 3)
                {
                    int from = 0;
                    int to = 0;
                    do
                    {
                        // Choice from
                        Console.WriteLine("\nFrom: ");
                        from = SelectAccount();

                        // Choice To
                        Console.WriteLine("\nTo: ");
                        to = SelectAccount();

                        if (from == to)
                        {
                            Console.WriteLine("You cannot transfer from and to the same account!");
                        }
                    } while (from == to);

                    // Amount
                    double fromAmount = ValidateAmount();

                    // Checkings to savings
                    if (from == 1 && to == 2)
                    {
                        // Transfer out checkings
                        Transaction tran = new Transaction(fromAmount, TransactionType.TRANSFER_OUT);
                        TransactionResult result = accounts[n].Checking.Withdraw(tran);

                        // Transfer in savings
                        if (result == TransactionResult.SUCCESS)
                        {
                            Transaction that = new Transaction(fromAmount, TransactionType.TRANSFER_IN);
                            accounts[n].Saving.Deposit(that);
                            Console.WriteLine("\nTransfer complete, account current balance: $" + accounts[n].Saving.Balance + "\n");
                        }
                        else
                        {
                            Console.WriteLine("\nWithdraw canceled: " + result + "\n");
                        }

                        // Savings to checkings
                    }
                    else if (from == 2 && to == 1)
                    {
                        // Transfer out savings
                        Transaction tran = new Transaction(fromAmount, TransactionType.TRANSFER_OUT);
                        TransactionResult result = accounts[n].Saving.Withdraw(tran);

                        // Transfer in checkings
                        if (result == TransactionResult.SUCCESS)
                        {
                            Transaction that = new Transaction(fromAmount, TransactionType.TRANSFER_IN);
                            accounts[n].Checking.Deposit(that);
                            Console.WriteLine("\nTransfer complete, account current balance: $" + accounts[n].Checking.Balance + "\n");
                        }
                        else
                        {
                            Console.WriteLine("\nWithdraw canceled: " + result + "\n");
                        }
                    }
                
                // Balance enquiry
                }
                else if (selection == 4)
                {
                    Console.WriteLine("\nAccount\t\t\tBalance");
                    Console.WriteLine("-------\t\t\t-------");
                    Console.WriteLine("Checking\t\t" + accounts[n].Checking.Balance);
                    Console.WriteLine("Saving\t\t\t" + accounts[n].Saving.Balance + "\n");


                // Account activity enquiry
                }
                else if (selection == 5)
                {

                    // Checking
                    Console.WriteLine("\nChecking Account:\n");
                    Console.Write("Amount\t\tDate\t\tActivity");
                    Console.Write("\n------\t\t----\t\t--------\n");
                    
                    for (int t = 0; t < accounts[n].Checking.TransactionHistories.Count; t++)
                    {
                        Console.Write(accounts[n].Checking.TransactionHistories[t].Amount + "\t\t");
                        Console.Write(accounts[n].Checking.TransactionHistories[t].TransactionDate.ToString("dd/MM/yyyy") + "\t");
                        Console.Write(accounts[n].Checking.TransactionHistories[t].Type + "\t\t\n");
                    }

                    // Saving
                    Console.WriteLine("\nSaving Account:\n");
                    Console.Write("Amount\t\tDate\t\tActivity");
                    Console.Write("\n------\t\t----\t\t--------\n");
                    for (int t = 0; t < accounts[n].Saving.TransactionHistories.Count; t++)
                    {
                        Console.Write(accounts[n].Saving.TransactionHistories[t].Amount + "\t\t");
                        Console.Write(accounts[n].Saving.TransactionHistories[t].TransactionDate.ToString("dd/MM/yyyy") + "\t");
                        Console.Write(accounts[n].Saving.TransactionHistories[t].Type + "\t\t\n");
                    }
                }
            } while (selection != 6);
        }

        // Select Account Function
        public static int SelectAccount()
        {
            int accountType = 0;
            do
            {
                Console.Write("\nSelect account (1 - Checking Account, 2 - Saving Account): ");
                try
                {
                    accountType = int.Parse(Console.ReadLine());
                    if (accountType != 1 && accountType != 2)
                    {
                        Console.WriteLine("\nThe number must be 1 or 2! \n");
                    }
                }
                catch
                {
                    Console.WriteLine("\nYou did not enter a number! \n");
                }
            } while (accountType != 1 && accountType != 2);
            return accountType;
        }

        // Validate Amount Function
        public static double ValidateAmount()
        {
            double amount = 0;
            Console.Write("\nEnter amount: ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("\nPlease enter a number: ");
            }
            return amount;
        }
    }
}