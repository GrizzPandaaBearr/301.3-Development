using _301._3_Development.models;
using _301._3_Development.Scripts.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _301._3_Development.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(string baseUrl)
        {
            // note to self, this is unsecure as hell, CHANGE THIS
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            _http = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task<bool> UpdateAppointmentStatus(int appointmentId, string status)
        {
            var data = new { Status = status };
            var result = await SessionManager.Instance.Api.PutAsync<object>($"Appointments/{appointmentId}/status", data);

            return result != null;
        }
        public async Task<bool> CancelAppointment(int appointmentId)
        {
            var result = await SessionManager.Instance.Api.PutAsync<object>($"Appointments/{appointmentId}/cancel", null);

            return result != null;
        }
        public void SetToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            MessageBox.Show(url);
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<T?> PutAsync<T>(string url, object data)
        {
            MessageBox.Show(url);

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PutAsync(url, content);

            string raw = await response.Content.ReadAsStringAsync();
            MessageBox.Show($"RAW PUT RESPONSE:\n{raw}");

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        public async Task<T?> PostAsync<T>(string url, object data)
        {
            MessageBox.Show(url);
            var json = System.Text.Json.JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(url, content);
            string raw = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<T>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
