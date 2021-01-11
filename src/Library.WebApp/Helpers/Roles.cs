using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Helpers
{
    public static class Roles
    {
        #region Areas/Identity
        // ~/Areas/Admin
        #region RolesController (Counts: 4)
        public const string RolesList = "مشاهده نقش ها";
        public const string RoleCreate = "افزودن نقش";
        public const string RoleEdit = "ویرایش نقش";
        public const string RoleDelete = "حذف نقش";
        #endregion

        // ~/Areas/Admin
        #region UsersController (Counts: 5)
        public const string UsersList = "مشاهده کاربران";
        public const string UserDetails = "مشاهده مشخصات کاربران";
        public const string UserCreate = "افزودن کاربر";
        public const string UserEdit = "ویرایش کاربر";
        public const string UserDelete = "حذف کاربر";
        #endregion

        // ~/Areas/Admin
        #region UserRolesController (Counts: 3)
        public const string UserRolesList = "مشاهده انتسابات";
        public const string UserRoleAssign = "انتساب نقش به کاربر";
        public const string UserRoleUnassign = "سلب نقش از کاربر";
        #endregion

        #region MessagesController (Counts: 3)
        public const string MessageDelete = "حذف پیام";
        public const string MessagesList = "فهرست پیام ها";
        public const string MessagesSetReadOrNot = "تنظیم پیام به عنوان خوانده شده یا نشده";
        #endregion

        // ~/Areas/Admin
        #region PersonalizationController (Counts: 1)
        public const string Personalize = "شخصی‌سازی سایت";
        #endregion
        #endregion
    }
}
