using System.Collections.Generic;

namespace fifthProgram.Entities
{
    class Account
    {
        // Properties
        public Customer Owner { get; }
        public double Balance { get; protected set; }
        public List<Transaction> TransactionHistories { get; }
        
        // Constructor
        public Account(Customer customer)
        {
            Owner = customer;
            Balance = 0.0;
            TransactionHistories = new List<Transaction>();
        }
        
        // Deposit
        public virtual TransactionResult Deposit(Transaction tran)
        {
            Balance += tran.Amount;
            TransactionHistories.Add(tran);
            return TransactionResult.SUCCESS;

        }

        // Withdraw
        public virtual TransactionResult Withdraw(Transaction tran)
        {
            // Balance is smaller than amount
            if (Balance < tran.Amount)
            {
                return TransactionResult.INSUFFICIENT_FUND;
            }
            Balance -= tran.Amount;
            TransactionHistories.Add(tran);
            return TransactionResult.SUCCESS;
        }
    }
}
