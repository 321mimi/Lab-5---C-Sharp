namespace fifthProgram.Entities
{
    class Customer
    {
        // Properties
        public string Name { get; }
        public CustomerStatus Status { get; set; }
        public CheckingAccount Checking;
        public SavingAccount Saving;

        // Constructor
        public Customer(string name)
        {
            Name = name;
            Checking = new CheckingAccount(this);
            Saving = new SavingAccount(this);    
            Status = CustomerStatus.REGULAR;
        }
    }
}