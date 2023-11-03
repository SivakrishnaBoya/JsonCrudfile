using TaskCrud_20_10_23.Models;

namespace TaskCrud_20_10_23.DbContext
{
    public interface IRepoJsonCrud
    {
        public List<Users> GetAllUsers();
        public Users GetUserById(int userId);
        public Users CreateUser(Users user);
        public Users UpdateUser(int id,Users updatedUser);
        public bool DeleteUser(int userId);
        public void SaveData(List<Users> users);

    }
}
