using ElizerBot.Adapter;

namespace ChatBot
{
    public static class UserExtension
    {
        public static string GetFullName(this UserAdapter user)
        {
            var names = string.Join(" ", new []
                { 
                    user.FirstName,
                    user.LastName
                }.Where(e => !string.IsNullOrWhiteSpace(e))
            );
            if (string.IsNullOrEmpty(names))
                return user.Id.ToString();
            else
                return $"{user.Id} ({names})";
        }
    }
}
