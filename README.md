# 📖 Search in Tanach (Hebrew Bible Search Engine)

A comprehensive **Hebrew Bible search system** built with **C# ASP.NET Core Web API** and **HTML/JavaScript frontend** for searching words and names throughout the entire Tanach (Hebrew Bible).

## 🎯 What This System Does

This is a **complete Hebrew Bible search engine** that allows users to:

- **Search for any word** throughout the entire Tanach with precise location details
- **Find verses containing names** with highlighted matches
- **Get detailed references** including book, chapter, verse, and Torah portion
- **Cache recent searches** for improved performance
- **Navigate with Hebrew numbering** system (Gematria)

## 🏗️ System Architecture

**Full-Stack Hebrew Text Processing System:**

```txt
📁 Frontend (HTML/JS):
├── 🏠 HomePage.html      # Main navigation
├── 🔍 SearchWord.html    # Word search interface
└── 👤 SearchName.html    # Name search interface

📁 Backend (C# Web API):
├── 🎯 TanachController   # API endpoints
├── 💼 BibleBll          # Business logic
├── 📊 BibleDal          # Data access layer
└── 📍 Location          # Result model

📁 Data Storage:
├── 📚 Tanach/           # 39 JSON files (all books)
├── 📄 Data.json         # Metadata & recent searches
├── 📝 psukim.json       # Verse database
└── 📜 tanach.txt        # Plain text version
```

## 🚀 Key Features

### 🔍 Advanced Word Search

- **Exact word matching** with context awareness
- **Detailed location data**: Book, Torah portion, chapter, verse
- **Highlighted results** showing word in context
- **Performance caching** for frequently searched terms
- **Hebrew Gematria numbering** for authentic references

### 👤 Name-Based Search

- **Personal name lookup** in biblical texts
- **Pattern matching** for names with prefixes/suffixes
- **Acrostic search** using first and last letters
- **Highlighted name display** within verses

### 📚 Complete Tanach Database

- **All 39 books** of the Hebrew Bible included
- **Torah portions** (Parsha) integration
- **Structured JSON format** for efficient searching
- **Hebrew text processing** with proper encoding

### ⚡ Smart Caching System

- **Recent search storage** for instant results
- **Automatic cache updates** when new searches performed
- **JSON-based persistence** for search history

## 🛠️ Technology Stack

### Backend (C# ASP.NET Core)

- **Web API** with RESTful endpoints
- **Newtonsoft.Json** for JSON processing
- **CORS enabled** for cross-origin requests
- **Swagger documentation** for API testing

### Frontend (HTML/JavaScript)

- **Vanilla JavaScript** with Axios for HTTP requests
- **Responsive Hebrew UI** with RTL support
- **Dynamic result rendering** with highlighting
- **CSS styling** for Hebrew text display

### Data Processing

- **Hebrew text encoding** (UTF-8)
- **Gematria conversion** (Hebrew numerals)
- **JSON data structures** for efficient storage
- **Text parsing algorithms** for accurate matching

## 📦 Installation & Setup

### Prerequisites

- .NET 6.0 or higher
- Web browser with JavaScript support

### Backend Setup

```bash
cd Search-in-Tanach
dotnet restore
dotnet run
```

### Frontend Access

```txt
Open HomePage.html in web browser
API runs on: https://localhost:7231
```

### API Endpoints

```txt
GET /api/Tanach/GetWord?word={word}
GET /api/Tanach/GetPasukName?name={name}
```

## 📊 Data Structure

### Location Object

```json
{
  "WordFound": "searched word",
  "AllPasuk": "complete verse text",
  "Sefer": "book name in Hebrew",
  "Paracha": "Torah portion name",
  "Perek": "chapter in Hebrew numerals",
  "Pasuk": "verse in Hebrew numerals"
}
```

### Supported Books

**Torah (5 books)**: Genesis, Exodus, Leviticus, Numbers, Deuteronomy
**Prophets (21 books)**: Joshua, Judges, Samuel, Kings, Isaiah, Jeremiah, Ezekiel, and 12 Minor Prophets
**Writings (13 books)**: Psalms, Proverbs, Job, Song of Songs, Ruth, Lamentations, Ecclesiastes, Esther, Daniel, Ezra, Nehemiah, Chronicles

## 🎯 Search Capabilities

### Word Search Features

- **Whole word matching** (not partial)
- **Context-aware results** showing surrounding text
- **Multiple occurrence handling** across all books
- **Performance optimization** through caching

### Name Search Features

- **Exact name matching** in various contexts
- **Acrostic pattern search** using first/last letters
- **Flexible matching** with common Hebrew prefixes/suffixes

## 💡 Perfect For

- **Hebrew Bible Study** - Comprehensive text analysis
- **Religious Research** - Academic and personal study
- **Hebrew Language Learning** - Text processing examples
- **Web API Development** - RESTful service patterns
- **JSON Data Processing** - Large dataset management

## 🔧 Technical Highlights

- **Hebrew Text Processing** with proper encoding
- **Gematria Number System** for authentic Hebrew references
- **Efficient JSON Parsing** for large biblical datasets
- **Caching Mechanism** for improved search performance
- **RESTful API Design** with proper HTTP methods
- **Cross-Origin Support** for web integration
- **Responsive Hebrew UI** with RTL text support
- **Pattern Matching Algorithms** for flexible name search

---

*A professional Hebrew Bible search engine demonstrating advanced text processing, Hebrew language support, and efficient data management for religious and academic research.*
