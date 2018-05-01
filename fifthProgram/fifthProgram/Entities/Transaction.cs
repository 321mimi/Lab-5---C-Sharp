using System;

namespace fifthProgram.Entities
{
    class Transaction
    {
        // Properties
        public double Amount { get; }
        public TransactionType Type { get; }
        public DateTime TransactionDate { get; }

        // Constructor
        public Transaction(double amount, TransactionType type)
        {
            if (amount <= 0) throw new Exception("Invalid");
            Amount = amount;
            Type = type;
            TransactionDate = DateTime.Now;
        }
    }
}
