namespace EventSourcingPoc.Messages.Orders
{
    public class Address
    {
        public string Value { get; }
        public Address(string value)
        {
            this.Value = value;
        }
    }
}
