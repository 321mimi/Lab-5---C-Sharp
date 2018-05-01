namespace fifthProgram.Entities
{
    class SavingAccount : Account
    {
        // Properties
        public static double PremierAmount = 2000.0;
        public static double WithdrawPenaltyAmount = 10.0;

        // Constructor
        public SavingAccount(Customer customer) : base(customer) { }

        // Withdraw
        public override TransactionResult Withdraw(Transaction transaction)
        {
            // Regular customer withdraws
            if (transaction.Type == TransactionType.WITHDRAW && Owner.Status != CustomerStatus.PREMIER)
            {
                // Balance and penalty is smaller than amount
                if ((Balance - WithdrawPenaltyAmount) < transaction.Amount)
                {
                    return TransactionResult.INSUFFICIENT_FUND;
                }
                // Withdraw penalty
                Transaction penalty = new Transaction(WithdrawPenaltyAmount, TransactionType.PENALTY);
                this.Withdraw(penalty);
            }
            // Premier customer withdraws or transfers out
            if ((transaction.Type == TransactionType.WITHDRAW || transaction.Type == TransactionType.TRANSFER_OUT) && Owner.Status != CustomerStatus.REGULAR)
            {
                // New balance is smaller than 2000
                if ((Balance - transaction.Amount) < PremierAmount)
                {
                    Owner.Status = CustomerStatus.REGULAR;
                }
            }
            return base.Withdraw(transaction);
        }

        // Deposit
        public override TransactionResult Deposit(Transaction transaction)
        {
            // New balance is bigger or equal to 2000
            if (Balance + transaction.Amount >= PremierAmount)
            {
                Owner.Status = CustomerStatus.PREMIER;
            }
            return base.Deposit(transaction);
        }
    }
}
