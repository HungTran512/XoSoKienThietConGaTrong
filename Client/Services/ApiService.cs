using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Models;
using Newtonsoft.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiService(string baseUrl)
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
    }
    public async Task<LotteryEntry> SubmitEntryAsync(LotteryEntry entry)
    {
        

        var json = JsonConvert.SerializeObject(entry);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/lottery/entry", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LotteryEntry>(responseContent);
        }

        return null;
    }
    public async Task<User> RegisterUserAsync(User user)
    {
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}/user/register", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(responseContent);
        }
        return null;
    }
    public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/user/{phoneNumber}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(responseContent);
            }
            else
            {
                // Handle non-success status codes appropriately
                return null;
            }
        }
        catch (Exception ex)
        {
           System.Diagnostics.Debug.WriteLine(ex);
            throw;
        }
    }
    public async Task<List<LotteryResult>> GetLotteryResultsForTodayAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/lottery/results/today");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LotteryResult>>(responseContent);
            }
            else
            {
                // Handle non-success status codes appropriately
                return null;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            throw;
        }
    }
    public async Task<List<LotteryEntry>> GetAllLotteryEntriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/lottery/entries/all");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LotteryEntry>>(responseContent);
            }
            else
            {
                // Handle non-success status codes appropriately
                return null;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            throw;
        }
    }

}
