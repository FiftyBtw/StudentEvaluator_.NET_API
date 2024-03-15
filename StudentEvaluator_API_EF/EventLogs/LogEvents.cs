namespace EventLogs;

public class LogEvents
{
    protected LogEvents() { }
    
    public const int GenerateItems = 1000;
    public const int ListItems     = 1001;
    public const int GetItem       = 1002;
    public const int InsertItem    = 1003;
    public const int UpdateItem    = 1004;
    public const int DeleteItem    = 1005;
    
    public const int GetItems      = 2000;
    public const int GetItemsNotFound = 2001;
    public const int GetItemsNoContent = 2002;

    public const int TestItem      = 3000;

    public const int GetItemNotFound    = 4000;
    public const int UpdateItemNotFound = 4001;
    public const int DeleteItemNotFound = 4002;
    public const int GetItemBadRequest  = 4003;
    public const int UpdateItemBadRequest = 4004;
    public const int DeleteItemBadRequest = 4005;
    public const int InsertItemBadRequest = 4006;
}