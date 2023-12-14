namespace PhoHa7.Library.Enum
{
    public enum EnumFormStatus
    {
        View = 1,
        Add = 2,
        Modify = 4,
        Delete = 8,
        Print = 16,
        Word = 32,
        Excel = 64,
        Approved = 128,
        Report = 256

    }

    public enum EnumErrorType
    {
        Information,
        Error,
        Warning,
        Confirm
    }

    public enum EnumStatusAccess
    {
        Public,
        Register,
        Special
    }

    public enum EnumRefType
    {
        None,
        Inward,
        Outward,
        Sales
    }

    public enum EnumPriceType
    {
        SalePrice = 1,
        RetailsPrice,
        AgentPrice
    }

    public enum EnumOrdersStatus
    {
        None,
        OrderOutOfStock,//orders có hàng tạm hết
        Order,//orders
        Pending,//orders đang trạng thái chờ
        Deposit,//orders đã đặt cọc
        Paid//orders đã thanh toán và lấy hàng
    }

    public enum EnumFormCode
    {
        System,
        //System
        FrmLogin,
        FrmBackup,
        FrmRestore,
        FrmParameter,
        FrmErrorSystem,
        FrmHistorySystem,
        FrmChangePassword,
        FrmPermissionUser,
        FrmPermissionGroup,


        Category,
        Enity,
        FrmAttendance,
        FrmCategory,
        FrmCustomer,
        FrmEmployee,
        FrmEmployeeAdmin,
        FrmManagerOrder,
        FrmPayroll,
        Report,
        Sales,
        FrmMachineManagement,
        FrmDictionary,
        FrmObject,
        FrmTabItems,
        FrmError
    }

    public enum PaymentType
    {
        Cash,Credit,Check,GiftCard,PrePaid
    }



}
