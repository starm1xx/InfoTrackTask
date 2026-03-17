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



\## 🚀 How to Run



\### 1. Run API



Set `InfoTrack.Api` as startup project and run:



https://localhost:7277



\---



\### 2. Run Client



Set `InfoTrack.Client` as startup project and run (port may vary):



https://localhost:xxxx



\---



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

