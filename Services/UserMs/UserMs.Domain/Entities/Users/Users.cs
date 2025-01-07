namespace UserMs.Domain.Entities
{
    public class Users : Base
    {
        public UsersType UsersType { get; private set; }
        public Users(UserId userId, UserEmail userEmail, UserPassword userPassword,UserDepartament userDepartament, UsersType usersType)
            : base(userId, userEmail, userPassword, userDepartament)
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