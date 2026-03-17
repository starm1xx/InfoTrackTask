# Solicitor Search App (.NET Core + Blazor)

## Overview

This application demonstrates a full-stack .NET solution that scrapes solicitor data from:

https://www.solicitors.com/conveyancing.html

It allows users to search solicitors by location (including multiple and custom locations), view structured results, and analyze the quality of the collected data.

---

## Architecture

The solution follows a layered architecture:

* InfoTrack.Api → ASP.NET Core Web API (scraping and data processing)
* InfoTrack.Client → Blazor WebAssembly UI
* InfoTrack.Shared → Shared models (DTOs)

This structure provides:

* Clear separation of concerns
* Reusability
* Maintainability

---

## Features

### Data Scraping

* Custom HTML parsing using HttpClient and Regex
* No third-party scraping libraries
* Extracted fields:

  * Name
  * Location
  * Phone
  * Address
  * Website
  * Contact (enquiry link)
  * Profile URL
* Handles invalid locations safely without breaking execution

---

### API

Endpoint:

GET /api/solicitors?locations=London,Manchester,...

* Supports multiple locations
* Accepts custom user input
* Skips invalid locations gracefully
* Returns structured JSON
* Stores results in an in-memory database
* Prevents duplicate entries

---

### Data Storage

* Entity Framework Core
* In-memory database (UseInMemoryDatabase)
* Lightweight and suitable for demonstration purposes

---

### UI (Blazor)

* Input supports both typing and selection
* Dropdown suggestions based on predefined cities
* Multiple location selection using tag-style UI
* Allows combining predefined and custom locations
* Clean and responsive layout

---

### Results Table

* Paginated data
* Adjustable page size
* Sorting by:

  * Name
  * Location
* Clickable fields:

  * Name → profile page
  * Website → external site
  * Contact → enquiry form

---

### Insights

* Total results count
* Grouping by location
* Data quality metrics:

  * Missing phones
  * Missing emails
  * Missing websites
  * Missing addresses
  * Complete records
* Duplicate detection

---

### UX Improvements

* Loading indicator
* Error handling for invalid or empty searches
* Pagination controls
* Dynamic dropdown suggestions
* Multi-location selection
* Consistent fallback values ("N/A")
* Disabled actions during loading

---

## Running the Application

### Run both API and Client together (recommended)

1. Open the solution in Visual Studio
2. Right-click the solution → Set Startup Projects
3. Select Multiple startup projects
4. Configure:

   * InfoTrack.Api → Start
   * InfoTrack.Client → Start
   * InfoTrack.Shared → None
5. Click Apply
6. Run the solution (F5)

---

### Application Flow

* API starts (e.g. https://localhost:7277)
* Blazor UI launches in browser
* UI sends requests to API
* Data is scraped, stored in memory, and displayed

---

### Notes

* Ensure both projects use HTTPS
* Update API URL in Client if the port changes
* Initial load may take a few seconds due to scraping
* No external database is required

---

### CLI Alternative

Run projects manually:

dotnet run --project InfoTrack.Api
dotnet run --project InfoTrack.Client

---

## Key Design Decisions

### No Third-Party Scraping Libraries

Implemented parsing manually using Regex to demonstrate understanding of HTML structure and extraction logic.

---

### Shared Models

The InfoTrack.Shared project avoids duplication and ensures consistency between API and UI.

---

### In-Memory Database

Chosen for simplicity and quick setup while still demonstrating use of Entity Framework Core.

---

### Defensive Backend Handling

Invalid locations (e.g. non-existent pages) are handled gracefully without breaking the application.

---

### Flexible Input Design

Users can:

* Select predefined locations
* Enter custom locations
* Combine both in a single query

---

### Separation of Concerns

* API handles scraping and data processing
* UI handles presentation and interaction

---

## Possible Improvements

* Server-side pagination
* Caching layer to reduce repeated scraping
* Replace Regex with a structured HTML parser
* Add unit and integration tests
* Add visual charts for insights
* Improve loading experience (skeleton UI)

---

## Author

Developed as part of a technical assessment to demonstrate:

* .NET Web API development
* Blazor WebAssembly UI
* Data scraping and transformation
* Clean architecture
* User-focused design
