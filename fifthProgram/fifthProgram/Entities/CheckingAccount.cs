namespace fifthProgram.Entities
{
    class CheckingAccount : Account
    {
        // Properties
        public static double MaxWithdrawAmount = 300.0;

        // Constructor
        public CheckingAccount(Customer customer) : base(customer) { }

        // Withdraw
        public override TransactionResult Withdraw(Transaction transaction)
        {
            // Regular customer
            if (transaction.Type == TransactionType.WITHDRAW && Owner.Status != CustomerStatus.PREMIER)
            {
                // Withdraws more than 300
                if (transaction.Amount > MaxWithdrawAmount)
                {
                    return TransactionResult.EXCEED_MAX_WITHDRAW_AMOUNT;
                }
            }
            return base.Withdraw(transaction);
        }
    }
}
