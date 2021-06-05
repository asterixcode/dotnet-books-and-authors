using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AuthorBlazor.Models;

namespace AuthorBlazor.Services
{
    public class AuthorService
    {
        private readonly HttpClient _httpClient;
        
        public AuthorService(HttpClient client)
        {
            _httpClient = client;
        }
        
        // GET METHODS
        public async Task<IList<Book>> GetAllBooksAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/books");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            
            string responseContent = await response.Content.ReadAsStringAsync();
            
            List<Book> allBooks = JsonSerializer.Deserialize<List<Book>>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return allBooks;
        }
        
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/authors");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            
            string responseContent = await response.Content.ReadAsStringAsync();
            
            List<Author> allAuthors = JsonSerializer.Deserialize<List<Author>>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return allAuthors;
        }
        
        
        // POST METHODS
        public async Task AddNewAuthorAsync(Author newAuthor)
        {
            var jsonAuthor = new StringContent(
                JsonSerializer.Serialize(newAuthor, typeof(Author), new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await _httpClient.PostAsync("/authors", jsonAuthor);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task AddNewBookAsync(Book newBook, int authorId)
        {
            var jsonBook = new StringContent(
                JsonSerializer.Serialize(newBook, typeof(Book), new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await _httpClient.PostAsync($"/books/{authorId}", jsonBook);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
            }
        }

        
        // DELETE METHODS
        public async Task DeleteBookAsync(int isbn)
        { 
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/books/{isbn}");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");        }
    }
}