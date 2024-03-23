namespace Dto
{

    public class UserStatus
    {
        // Indicates whether the provided username exists or not
        public bool UserNAmeStatus { get; set; }

        // Indicates whether to load the OTP page or the password page after user enters their Email/PhoneNumber/Username
        public bool IsOtp { get; set; }

        // Indicates whether the email/phonenumber exist
        public bool IsOtpAvailable { get; set; }

        // Indicates whether the password of the user exists in the Identity table
        public bool IsPasswordAvailable { get; set; }

    }
}
