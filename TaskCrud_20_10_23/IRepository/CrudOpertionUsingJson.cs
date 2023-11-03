using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TaskCrud_20_10_23.DbContext;
using TaskCrud_20_10_23.Models;

// implementing the IRepoJsonCrud in UserRepository
internal class UserRepository : IRepoJsonCrud
{
    public List<Users> users;
    private List<Departments> departments;
    private readonly string departmentsDataFilePath = "Department.json";
    public IConfiguration configuration;

    public string jsonFilePath = "User_and_Department_Data.json";

    public UserRepository(IConfiguration _configuration)
    {
        // Calling the Load data method
        LoadData();
       
        configuration=_configuration;
    }
    public Users CreateUser(Users user)
    {
        //Check userId 
        if (user.UserId == 0)
            throw new ArgumentNullException(nameof(user), "Userid cannot be zero.");

        if(user==null)
            throw new ArgumentNullException(nameof(user), "User cannot be null.");
        // Check if a user with the same ID already exists
        if (users.Any(u => u.UserId == user.UserId))
        {
            throw new ArgumentException("User with the same ID already exists.");
        }

        // Check if a matching department exists
        var matchingDepartment = departments.FirstOrDefault(d => d.DepartmentId == user.DepartmentId);

        if (matchingDepartment == null)
        {
            throw new ArgumentException("Department not found for the specified DepartmentId.");
        }
        user.Password = configuration["DefultPassword"];
        users.Add(user); 
        SaveData(users);
        return user;
    }
    public List<Departments> LoadDataFromFile<Departments>(string filePath)
    {
        // Check the file is Exist or Not

        if (File.Exists(filePath))
        {
            // Reading the File data

            string jsonData = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<Departments>>(jsonData);
        }
        else
        {
            return new List<Departments>();
        }
    }


    private void LoadData()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            users = JsonConvert.DeserializeObject<List<Users>>(jsonData);
            departments = LoadDataFromFile<Departments>(departmentsDataFilePath);
        }
        else
        {
            users = new List<Users>();
        }
    }
    

    public bool DeleteUser(int userId)
    {
        var users = GetAllUsers();
        var userToRemove = users.FirstOrDefault(u => u.UserId == userId);
        if (userToRemove != null)
        {
            users.Remove(userToRemove);
            SaveData(users);
            return true;
        }
        return false;
    }

    public List<Users> GetAllUsers()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            return  JsonConvert.DeserializeObject<List<Users>>(json);
        }
        return new List<Users>();
    }

    public Users GetUserById(int userId)
    {
        var users = GetAllUsers();
        return users.FirstOrDefault(u => u.UserId == userId);

    }

    public void SaveData(List<Users> users)
    {
        string json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(jsonFilePath, json);
        
    }

    public Users UpdateUser(int id,Users updatedUser)
    {
        var users = GetAllUsers();
        var existingUser = users.FirstOrDefault(u => u.UserId == id);
        if (existingUser != null)
        {
            existingUser.Name = updatedUser.Name;
            // Update other properties as needed
            SaveData(users);
        }
        return existingUser;
    }
}