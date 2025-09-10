#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorEventManagementApp.Models;
using Microsoft.JSInterop;

namespace BlazorEventManagementApp.Data
{
    public class EventService
    {
        private readonly IJSRuntime _js;
        private const string StorageKey = "events";
        private readonly JsonSerializerOptions _json = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        // Lista en memoria
        private List<Event> _events = new();

        public EventService(IJSRuntime js)
        {
            _js = js;
        }

        /// <summary>
        /// Cargar desde localStorage. Si no hay datos, sembrar con 1000 items.
        /// Llamar una vez al inicio de la app (Program.cs).
        /// </summary>
        public async Task InitializeAsync()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrWhiteSpace(json))
            {
                var loaded = JsonSerializer.Deserialize<List<Event>>(json, _json) ?? new();
                _events = loaded;
            }
            else
            {
                // seed inicial si no hay nada en localStorage
                _events = Enumerable.Range(1, 1000).Select(i => new Event
                {
                    Id = i,
                    EventName = $"Event {i}",
                    Date = DateTime.Today.AddDays(i),
                    Location = i % 2 == 0 ? "Managua" : "Online"
                }).ToList();

                await SaveAsync();
            }
        }

        private async Task SaveAsync()
        {
            var json = JsonSerializer.Serialize(_events, _json);
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }

        public Task<List<Event>> GetEventsAsync() => Task.FromResult(_events.ToList());

        public Task<Event?> GetEventByIdAsync(int id)
            => Task.FromResult(_events.FirstOrDefault(e => e.Id == id));

        public async Task AddAsync(Event e)
        {
            // Asignar Id incremental
            e.Id = _events.Count == 0 ? 1 : _events.Max(x => x.Id) + 1;

            _events.Add(e);
            await SaveAsync();
        }

        public Task<int> GetEventsCountAsync()
            => Task.FromResult(_events.Count);

        public Task<List<Event>> GetEventsAsync(int startIndex, int count)
        {
            if (startIndex < 0) startIndex = 0;
            if (startIndex > _events.Count) startIndex = _events.Count;

            var take = Math.Max(0, Math.Min(count, _events.Count - startIndex));
            var slice = _events.Skip(startIndex).Take(take).ToList();
            return Task.FromResult(slice);
        }

        public async Task ClearAsync()
        {
            _events.Clear();
            await _js.InvokeVoidAsync("localStorage.removeItem", StorageKey);
        }

    }
}
