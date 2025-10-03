namespace App;

class Trade
{
    public string SenderItems;
    public string ReceiverItems;
    public string Sender;
    public string Receiver;
    public Tradestatus Status;

    public Trade(string senderitems, string receiveritems, string sender, string receiver, Tradestatus status)
    {
        SenderItems = senderitems;
        ReceiverItems = receiveritems;
        Sender = sender;
        Receiver = receiver;
        Status = status;
    }
}