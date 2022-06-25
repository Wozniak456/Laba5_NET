class Client
{
    static void Main(string[] args)
    {
        //it has money, password is right, the limit is not exceeded, ATM has NOT this amount of money
        Operation receiver = new Operation(true, true, false, false);

        OperationHandler hasATMMoneyHandler = new HasATMMoneyHandler();
        OperationHandler isValidPasswordHandler = new PasswordValidHandler();
        OperationHandler isLimitExceededHandler = new MoreThanLimitHandler();
        OperationHandler hasATMThisMoneyAmountHandler = new HasATMThisAmountHandler();

        hasATMMoneyHandler.Successor = isValidPasswordHandler;
        isValidPasswordHandler.Successor = isLimitExceededHandler;
        isLimitExceededHandler.Successor = hasATMThisMoneyAmountHandler;

        try
        {
            hasATMMoneyHandler.Handle(receiver);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

class Operation
{
    public bool HasAnAtmMoney { get; set; }
    public bool InPasswordValid { get; set; }
    public bool IsItMoreThenLimit { get; set; }
    public bool HasATMThisAmount { get; set; }
    public Operation(bool bt, bool mt, bool ppt, bool a)
    {
        HasAnAtmMoney = bt;
        InPasswordValid = mt;
        IsItMoreThenLimit = ppt;
        HasATMThisAmount = a;
    }
}
abstract class OperationHandler
{
    public OperationHandler Successor { get; set; }
    public abstract void Handle(Operation receiver);
}

class HasATMMoneyHandler : OperationHandler
{
    public override void Handle(Operation receiver)
    {
        if (receiver.HasAnAtmMoney == false)
            throw new Exception("Банкомат заблоковано");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

class PasswordValidHandler : OperationHandler
{
    public override void Handle(Operation receiver)
    {
        if (receiver.InPasswordValid == false)
            throw new Exception("Пароль неправильний.");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}
class MoreThanLimitHandler : OperationHandler
{
    public override void Handle(Operation receiver)
    {
        if (receiver.IsItMoreThenLimit == true)
            throw new Exception("Сума перевищує лiмiт картки.");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}
class HasATMThisAmountHandler : OperationHandler
{
    public override void Handle(Operation receiver)
    {
        if (receiver.HasATMThisAmount == false)
            throw new Exception("У банкоматi немає цiєi суми.");
        else
            throw new Exception("Грошi успiшно знято.");
    }
}