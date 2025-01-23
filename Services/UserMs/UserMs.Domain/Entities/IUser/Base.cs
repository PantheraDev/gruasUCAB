namespace UserMs.Domain.Entities
{
    public class Base
    {
        public UserId UserId { get; private set; } // Make it private to enforce immutability
        public UserEmail UserEmail { get; private set; }
        public UserPassword UserPassword { get; private set; }
        public UserDelete? UserDelete { get; private set; }
        public UserDepartament UserDepartament { get; private set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        protected Base(UserId userId, UserEmail userEmail, UserPassword userPassword, UserDepartament userDepartament)
        {
            UserId = userId;
            UserEmail = userEmail;
            UserPassword = userPassword;
            UserDepartament = userDepartament;
        }

        public void SetUserEmail(UserEmail userEmail)
        {
            UserEmail = userEmail;
        }

        public void SetUserPassword(UserPassword userPassword)
        {
            UserPassword = userPassword;
        }

        public void SetUserDelete(UserDelete userDelete)
        {
            UserDelete = userDelete;
        }

        
        public void SetUserDepartament(UserDepartament userDepartament)
        {
            UserDepartament = userDepartament;
        }
    }
}