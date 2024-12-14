namespace UserMs.Domain.Entities
{
    public class Users : Base
    {
        public UsersType UsersType { get; private set; }
        public Users(UserId userId, UserEmail userEmail, UserPassword userPassword, UsersType usersType)
            : base(userId, userEmail, userPassword)
        {
            UsersType = usersType;
        }
        public string GetUsersTypeString()
        {
            return UsersType.ToString();
        }

        public void SetUsersType(UsersType usersType)
        {
            UsersType = usersType;
        }
    }
}