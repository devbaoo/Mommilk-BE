namespace Domain.Constants
{
    public class AppErrors
    {
        public const string INVALID_CERTIFICATE = "Account username or password is incorrect";
        public const string INVALID_USER_UNACTIVE = "User is inactive";
        public const string NO_CHANGE = "No change in request provided";
        public const string INVALID_DATE = "Invalid date";

        // User
        public const string USER_NOT_EXIST = "User does not exist";
        public const string USERNAME_EXIST = "Username exists";
        public const string WRONG_PASSWORD = "Wrong password";
        public const string SAME_PASSOWRD = "Old and new passwords are the same";
        public const string SAME_STATUS = "No change in status";
        public const string INVALID_STATUS = "Invalid status";

        // Product
        public const string INVALID_QUANTITY = "Invalid quantity";
        public const string PRODUCT_QUANTITY_NOT_ENOUGH = "Not enough product in inventory";

        // Query
        public const string CREATE_FAIL = "Create failed";
        public const string UPDATE_FAIL = "Update failed";
        public const string RECORD_NOT_FOUND = "Record not found";

        // Order
        public const string INVALID_PAYMENT_METHOD = "Invalid payment";
        public const string ORDER_NOT_CONFIRMED = "Order is not confirmed";
        public const string INVALID_ORDER_STATUS = "Invalid order status";
        // Voucher
        public const string VOUCHER_NOT_ENOUGH = "Voucher is out of use";
        public const string VOUCHER_NOT_EXIST = "Voucher does not exist";
        public const string VOUCHER_DUPLICATE = "Voucher code duplicate";
        public const string VOUCHER_NO_VALUE = "Voucher has no value";

        // Product Line
        public const string PRODUCT_INSTOCK_NOT_ENOUGH = "Product instock is not enough";

        //Feedback
        public const string NO_COMPLETED_ORDER = "Customer has not purchased the product";
        public const string FEEDBACK_ALREADY_EXISTS = "Customer has already given a feedback";
        public const string INVALID_STAR_RATING = "Invalid ratings (0 < Star < 5)";

    }
}
