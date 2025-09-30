namespace App;

class Trade
{
    public string Items;
    public string Sender;
    public string Receiver;
    public string Status;

    public Trade(string items, string sender, string receiver, string status)
    {
        Items = items;
        Sender = sender;
        Receiver = receiver;
        Status = status;
    }
}