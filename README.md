# 📌 Blazor Event Management App

A **Blazor WebAssembly** project built as part of the course:  
👉 [Blazor for Front-End Development](https://www.coursera.org/learn/blazor-for-front-end-development)

This app demonstrates **modern front-end practices** with Blazor, including:

- 🔐 User registration with validation
- 📅 Event registration and storage
- 🗂️ State management with session tracking
- 💾 Local storage persistence
- 🌐 Responsive Bootstrap-based UI

---

## 🚀 Features

- **Registration Form** with validation & secure handling
- **Event Management**: create, list, and view events
- **Session Tracking**: last login, last visited page, sign-in/out
- **Virtualized Event Listing** for performance with large datasets
- **Responsive UI** styled with Bootstrap 5
- **Toast Notifications** for user feedback
- **LocalStorage Integration** for persistent events

---

## 🛠️ Tech Stack

- [.NET 8](https://dotnet.microsoft.com/)
- [Blazor WebAssembly](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- [Bootstrap 5](https://getbootstrap.com/)
- [LocalStorage](https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage)

---

## 📂 Project Structure

```plaintext
BlazorEventManagementApp/
│── Data/               # Event service, state handling
│── Models/             # Event & User models with validation
│── Pages/              # Razor components (Events, Register, EventCard, etc.)
│── Shared/             # Layouts & shared components
│── Services/           # Session service, Toast notifications
│── wwwroot/            # Static files, CSS, JS, Bootstrap
│── Program.cs          # Blazor setup & DI
```

---

## ▶️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/ksunami/BlazorEventManagementApp.git
cd BlazorEventManagementApp
```

### 2. Run the app

```bash
dotnet run
```

Then open [http://localhost:5000](http://localhost:5000) 🚀

---

## 🖼️ Screenshots

### 🔐 Register Page
![Welcome Page](https://github.com/ksunami/blazor-event-management-app/blob/main/docs/welcome.png)

![Register Page](https://github.com/ksunami/blazor-event-management-app/blob/main/docs/register.png)

### 📅 Events Page
![Events Page](https://github.com/ksunami/blazor-event-management-app/blob/main/docs/events.png)

### 📝 Event Card
![Event Card](https://github.com/ksunami/blazor-event-management-app/blob/main/docs/eventcard.png)


---

## 📧 Contact

For inquiries or collaboration:  
**Email:** ksunami_dr@yahoo.es

---

## 📚 Credits

Developed as part of the course:  
👉 [Blazor for Front-End Development](https://www.coursera.org/learn/blazor-for-front-end-development)
