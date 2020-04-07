using Framework;
using Framework.Attributes;
using System;
using System.Threading.Tasks;

namespace DBFramework
{
    class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [ProcedureName("SP_CreateUser")]
    public class SP_CreateUser : StoredProcedure 
    {
        [InParameter] public string Email;
        [InParameter] public string Name;
        [InParameter] public int Age;
    }

    [ProcedureName("SP_GetUsers")]
    public class SP_GetUsers : StoredProcedure
    {
    }

    [ProcedureName("SP_GetUserById")]
    public class SP_GetUserById : StoredProcedure
    {
        [InParameter] public int Id;
    }

    [ProcedureName("SP_DeleteUser")]
    public class SP_DeleteUser : StoredProcedure
    {
        [InParameter] public int Id;
    }

    [ProcedureName("SP_OutParameterTest")]
    public class SP_OutParameterTest : StoredProcedure
    {
        [OutParameter] public int SomeNumber;
    }

    [ProcedureName("SP_ReturnValueTest")]
    public class SP_ReturnValueTest : StoredProcedure
    {
        [ReturnValue] public int ReturnValue;
    }

    class Program
    {
        public static string connection = "Server=DESKTOP-1VMQJVE;Database=testdb;Trusted_Connection=True;MultipleActiveResultSets=true;Application Name=testdb;";

        static async Task Main(string[] args)
        {
            await CreateUserTest(new User { Name = "User1", Age = 20, Email = "user1@gmail.com" });
            await CreateUserTest(new User { Name = "User2", Age = 15, Email = "user2@gmail.com" });
            await CreateUserTest(new User { Name = "User3", Age = 27, Email = "user3@gmail.com" });
            await GetUsersTest();
            await GetUserByIdTest(1);
            await DeleteUserTest(2);
            await GetUsersTest();
            await OutParameterTest();
            await ReturnValueTest();

            Console.ReadLine();
        }

        public static async Task GetUsersTest()
        {
            Console.WriteLine("GetUsersTest");

            StoredProcedureExecutor executor = new StoredProcedureExecutor(connection);
            var users = await executor.ExecuteWithListResponseAsync<User>(new SP_GetUsers());

            Array.ForEach(users.ToArray(), 
                user => Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Email: {user.Email}, Age: {user.Age}"));

            Console.WriteLine(Environment.NewLine);
        }

        public static async Task GetUserByIdTest(int userId)
        {
            Console.WriteLine($"GetUserByIdTest. User Id {userId}");

            StoredProcedureExecutor executor = new StoredProcedureExecutor(connection);
            var user = await executor.ExecuteWithObjectResponseAsync<User>(new SP_GetUserById { Id = userId });
            Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Email: {user.Email}, Age: {user.Age}");

            Console.WriteLine(Environment.NewLine);
        }

        public static async Task CreateUserTest(User user)
        {
            Console.WriteLine("CreateUserTest");

            StoredProcedureExecutor executor = new StoredProcedureExecutor(connection);

            var rowsAffected = await executor.ExecuteWithReturnValueResponseAsync(new SP_CreateUser 
            { 
                Age = user.Age, 
                Email = user.Email, 
                Name = user.Name 
            });

            Console.WriteLine($"Rows affected: {rowsAffected}.");

            Console.WriteLine(Environment.NewLine);
        }

        public static async Task DeleteUserTest(int userId)
        {
            Console.WriteLine($"DeleteUserTest. User Id {userId}");

            StoredProcedureExecutor executor = new StoredProcedureExecutor(connection);
            var rowsAffected = await executor.ExecuteWithReturnValueResponseAsync(new SP_DeleteUser { Id = userId });
            Console.WriteLine($"Rows affected: {rowsAffected}.");

            Console.WriteLine(Environment.NewLine);
        }

        public static async Task OutParameterTest()
        {
            Console.WriteLine("OutParameterTest");

            StoredProcedureExecutor executor = new StoredProcedureExecutor(connection);
            var storedProcedure = new SP_OutParameterTest();
            await executor.ExecuteWithReturnValueResponseAsync(storedProcedure);
            Console.WriteLine($"Out parameter(must be '7'): {storedProcedure.SomeNumber}.");

            Console.WriteLine(Environment.NewLine);
        }

        public static async Task ReturnValueTest()
        {
            Console.WriteLine("ReturnValueTest");

            StoredProcedureExecutor executor = new StoredProcedureExecutor(connection);
            var storedProcedure = new SP_ReturnValueTest();
            await executor.ExecuteWithReturnValueResponseAsync(storedProcedure);
            Console.WriteLine($"Out parameter(must be '8'): {storedProcedure.ReturnValue}.");

            Console.WriteLine(Environment.NewLine);
        }
    }
}
