using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample
{
    //-- forras: https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
    #region snippet_prod
    //-- NuGet: Microsoft.AspNet.WebApi.Client csomag is kell a 
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
    #endregion

    class Program
    {
        #region snippet_HttpClient
        static HttpClient client = new HttpClient();
        #endregion

        static void ShowUser(User user)
        {
            Console.WriteLine($"Name: {user.username}\tPassword: " + $"{user.password}\tRole: {user.role}");
        }

        #region snippet_CreateUserAsync
        static async Task<Uri> CreateUserAsync(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("login", user);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        #endregion

        #region snippet_GetUserAsync
        static async Task<User> GetUserAsync(string path)
        {
            User user = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }
        #endregion

        #region snippet_UpdateUserAsync
        //static async Task<User> UpdateUserAsync(User user)
        //{
        //    HttpResponseMessage response = await client.PutAsJsonAsync($"users/{user.id}", user);
        //    response.EnsureSuccessStatusCode();

        //    // Deserialize the updated user from the response body.
        //    user = await response.Content.ReadAsAsync<User>();
        //    return user;
        //}
        #endregion

        #region snippet_DeleteUserAsync
        //static async Task<HttpStatusCode> DeleteUserAsync(string id)
        //{
        //    HttpResponseMessage response = await client.DeleteAsync(
        //        $"users/{id}");
        //    return response.StatusCode;
        //}
        #endregion

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        #region snippet_run
        #region snippet5
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:3000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion

            try
            {
                // Create a new user
                User user = new User
                {
                    id = null,
                    username = "Admin2",
                    password = "123",
                    role = "admin"
                };

                var url = await CreateUserAsync(user);
                Console.WriteLine($"Created at {url}");

                // Get the user
                user = await GetUserAsync(url.PathAndQuery);
                ShowUser(user);

                // Update the user
                Console.WriteLine("Updating password...");
                user.password = "1234";
                //await UpdateUserAsync(user);

                // Get the updated user
                user = await GetUserAsync(url.PathAndQuery);
                ShowUser(user);

                // Delete the user
                //var statusCode = await DeleteUserAsync(user.id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        #endregion
    }
}
