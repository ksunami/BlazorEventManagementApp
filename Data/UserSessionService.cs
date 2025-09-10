using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using BlazorEventManagementApp.Models;

namespace BlazorEventManagementApp.Data
{
    public class UserSessionService
    {
        private const string StorageKey = "app.user.session";
        private readonly IJSRuntime _js;
        private readonly EventService _eventService;

        public event Action? OnChanged;

        public bool IsAuthenticated => Identity is not null;
        public UserIdentity? Identity { get; private set; }
        public DateTimeOffset? LastActivityUtc { get; private set; }
        public string? LastPage { get; private set; }

        public UserSessionService(IJSRuntime js, EventService eventService) 
        {
            _js = js;
            _eventService = eventService;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var payload = JsonSerializer.Deserialize<SessionPayload>(json);
                    if (payload is not null)
                    {
                        Identity = payload.Identity;
                        LastActivityUtc = payload.LastActivityUtc;
                        LastPage = payload.LastPage;
                    }
                }
            }
            catch { /* ignore */ }
            OnChanged?.Invoke();
        }

        public async Task SignInAsync(UserIdentity identity)
        {
            Identity = identity;
            Touch();
            await PersistAsync();
            OnChanged?.Invoke();
        }

        public async Task SignOutAsync()
        {
            Identity = null;
            LastActivityUtc = null;
            LastPage = null;
            await _js.InvokeVoidAsync("localStorage.removeItem", StorageKey);
            await _eventService.ClearAsync();
            OnChanged?.Invoke();
        }

        public async Task TrackNavigationAsync(string uri)
        {
            LastPage = uri;
            Touch();
            await PersistAsync();
            OnChanged?.Invoke();
        }

        public void Touch()
        {
            LastActivityUtc = DateTimeOffset.UtcNow;
        }

        private async Task PersistAsync()
        {
            var payload = new SessionPayload
            {
                Identity = Identity,
                LastActivityUtc = LastActivityUtc,
                LastPage = LastPage
            };
            var json = JsonSerializer.Serialize(payload);
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }

        private class SessionPayload
        {
            public UserIdentity? Identity { get; set; }
            public DateTimeOffset? LastActivityUtc { get; set; }
            public string? LastPage { get; set; }
        }
    }
}
