namespace App;

class Trade
{
    public string Items;
    public string Sender;
    public string Receiver;
    public Tradestatus Status;

    public Trade(string items, string sender, string receiver, Tradestatus status)
    {
        Items = items;
        Sender = sender;
        Receiver = receiver;
        Status = status;
    }
}