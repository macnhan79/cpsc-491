namespace PhoHa7.Library.Interface
{
    public interface IPermission
    {
        bool PermissionView();
        bool PermissionAdd();
        bool PermissionUpdate();
        bool PermissionDelete();
        bool PermissionPrint();
        bool PermissionWord();
        bool PermissionExcel();
        bool PermissionApproved();
        bool PermissionReport();

        int PermissionAll();
        void loadPermission(string pFormCode, int pUserID);

    }
}
