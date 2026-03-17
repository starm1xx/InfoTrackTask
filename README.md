\# 🏛️ Solicitor Search App (.NET Core + Blazor)



\## 📌 Overview



This application demonstrates a full-stack .NET solution that scrapes solicitor data from:



👉 https://www.solicitors.com/conveyancing.html



It allows users to search solicitors by location, view structured results, and gain insights into the data.



\---



\## 🧱 Architecture



The solution follows a clean, layered architecture:



\- \*\*InfoTrack.Api\*\* → ASP.NET Core Web API (scraping + data processing)

\- \*\*InfoTrack.Client\*\* → Blazor WebAssembly UI

\- \*\*InfoTrack.Shared\*\* → Shared models (DTOs)



This separation ensures:

\- Reusability

\- Maintainability

\- Clear contract between client and server



\---



\## ⚙️ Features



\### 🔍 Data Scraping

\- Custom HTML parsing using `HttpClient` and `Regex`

\- No third-party scraping libraries (as required)

\- Extracted fields:

&#x20; - Name

&#x20; - Location

&#x20; - Phone

&#x20; - Address

&#x20; - Website

&#x20; - Contact (enquiry link)

&#x20; - Profile URL



\---



\### 🌐 API



Endpoint:



GET /api/solicitors?locations=London,Birmingham,...



\- Supports multiple locations

\- Returns structured JSON data

\- Saves results into an in-memory database



\---



\### 💾 Data Storage



\- Uses Entity Framework Core

\- In-memory database (`UseInMemoryDatabase`)

\- Prevents duplicate entries



\---



\### 🖥️ UI (Blazor)



\- Dropdown for selecting locations

\- Default load for London

\- Results displayed in a table

\- Clickable:

&#x20; - Firm name → profile page

&#x20; - Website → external site

&#x20; - Contact → enquiry form



\---



\### 📊 Insights



\- Total results count

\- Results grouped by location

\- Data quality metrics:

&#x20; - Missing phones

&#x20; - Missing websites

&#x20; - Missing addresses

\- Duplicate detection



\---



\### ⚡ UX Improvements



\- Loading indicator

\- Error handling

\- Sorting (Name \& Location)

\- Visual sort indicators (↑ ↓)

\- “N/A” fallback for missing data

\- Disabled button during loading

\- Clickable UI elements



\---



🚀 Running the Application

This solution contains three projects:

InfoTrack.Api – ASP.NET Core Web API (data scraping + in-memory database)

InfoTrack.Client – Blazor Web UI

InfoTrack.Shared – shared models between API and Client

▶️ Run both API and Client together (Recommended)

The solution is configured to run both projects at the same time using Visual Studio.

Steps:

Open the solution in Visual Studio

Right-click the solution → click Set Startup Projects

Select:

Multiple startup projects

Set the following configuration:

InfoTrack.Api      → Start
InfoTrack.Client   → Start
InfoTrack.Shared   → None

Click Apply

Press F5 or click Run

🌐 What happens after running

The API starts (e.g. https://localhost:7277)

The Blazor UI starts in the browser

The UI automatically sends requests to the API

Data is scraped, stored in memory, and displayed in the table

⚠️ Notes

Ensure both projects are using HTTPS

If API port changes, update the API URL in the Client

First load may take a few seconds due to scraping

No external database is required (uses in-memory DB)

🛠 Alternative: Run via CLI

If you prefer running manually:

dotnet run --project InfoTrack.Api
dotnet run --project InfoTrack.Client


\### 3. Use the App



\- Select location from dropdown

\- Click \*\*Search\*\*

\- View results and insights



\---



\## 🧠 Key Design Decisions



\### 1. No Third-Party Scraping Libraries

Implemented parsing using `Regex` to demonstrate understanding of HTML structure and data extraction.



\---



\### 2. Shared Models Project

Used `InfoTrack.Shared` to:

\- Avoid duplication

\- Ensure consistency between API and UI



\---



\### 3. In-Memory Database

Chosen for simplicity and fast setup while still demonstrating EF Core usage.



\---



\### 4. UI Data Normalization

Handled missing values at the UI layer (`N/A`) to keep domain models clean.



\---



\### 5. Clean Separation of Concerns

\- API handles scraping and data processing

\- UI handles presentation and user interaction



\---



\## 📈 Possible Improvements



\- Add pagination

\- Add filtering/search within results

\- Use a real database (SQL Server / PostgreSQL)

\- Replace regex with HTML parser for robustness

\- Add caching layer

\- Add unit tests



\---



\## 🧑‍💻 Author



Developed as part of a technical assessment to demonstrate:



\- .NET Web API development

\- Blazor WebAssembly UI

\- Data scraping and transformation

\- Clean architecture and UX thinking

