# UI.Template - Automated UI Testing Framework

A modular, extensible UI automation testing framework built with **.NET 8.0 LTS**, **Selenium**, and **NUnit**. This project is designed for robust, maintainable, and scalable automated testing of web applications using the PageObjectPattern architecture.

## ✨ Features

- **PageObjectPattern**: Clean separation of test logic and UI element locators
- **Comprehensive Logging**: Serilog-based logging with file output and detailed test run information
- **Error Artifacts**: Automatic screenshot and browser log capture on test failures
- **Selenium WebDriver**: Modern web browser automation with Chrome support
- **NUnit Testing**: Industry-standard testing framework for .NET
- **Multi-environment Configuration**: Easy switching between local, staging, and production environments
- **Robust Wait Mechanisms**: Custom wait strategies for element visibility and interaction readiness

## 📋 Documentation

- [Introduction](docs/Introduction.md) - Overview of the project structure and concepts
- [Prerequisites](docs/Prerequisites.md) - System requirements and setup checklist
- [Guidelines](docs/Guidelines.md) - Best practices and coding standards
- [Running Tests](docs/Running-Tests.md) - Instructions for running and debugging tests

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK or later ([Download .NET](https://dotnet.microsoft.com/download/dotnet/8.0))
- Visual Studio Code, Visual Studio, or any .NET-compatible IDE
- Chrome browser (for ChromeDriver compatibility)

### Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/UI.Template.git
   cd UI.Template
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Configure test environment:**
   - Copy `src/UI.Template/appsettings.json` to `src/UI.Template/appsettings.local.json`
   - Update values like `BaseUrl`, `UserName`, and `UserPassword`
   - See [Running Tests](docs/Running-Tests.md) for detailed configuration

4. **Run all tests:**
   ```bash
   dotnet test
   ```

5. **Run specific test:**
   ```bash
   dotnet test --filter "TC3" --logger "console;verbosity=detailed"
   ```

## 📊 Test Artifacts

When tests fail, the framework automatically captures:
- **Screenshots** - Visual state of the page at failure point
- **Browser Logs** - JavaScript console errors and warnings
- **Log Files** - Detailed test execution logs with timestamps

All artifacts are organized by test date/time in the `Logs/` directory and are excluded from git by default.

## 📁 Project Structure

```
src/UI.Template/
├── Components/           # UI component definitions (PageObject pattern)
├── Pages/               # Page object models for different pages
├── Tests/               # Test case implementations
├── Framework/           # Core testing framework (extensions, helpers, logging)
├── Data/                # Test data and factories
├── Models/              # Domain models and test objects
└── appsettings.json     # Configuration file (commit to repo)
```

## 🔧 Technology Stack

| Technology | Version | Purpose |
|-----------|---------|---------|
| .NET SDK | 8.0 LTS | Runtime and compilation |
| Selenium WebDriver | 4.34.0 | Web browser automation |
| NUnit | 4.3.2 | Unit testing framework |
| Serilog | 4.3.0 | Structured logging |
| HtmlAgilityPack | 1.12.2 | HTML parsing and XPath support |

## 📝 Key Test Files

- **TC3.cs** - Add Product to Admin Panel test
- **TC4.cs** - Shopping Cart and Checkout test (main flow)
- **TC4bug.cs** - Shopping Cart with known category bug workaround

## 🛠️ Development Workflow

1. Create new test class extending `BaseTest`
2. Use PageObject classes for page interactions
3. Implement test steps following arrangement (Arrange, Act, Assert)
4. Log important steps with `Logger.LogInformation()`
5. Run tests locally: `dotnet test`
6. Review artifacts in `Logs/` directory on failures

## 📈 Test Execution & Debugging

### Local Execution
```bash
# Run all tests with verbose output
dotnet test --logger "console;verbosity=detailed"

# Run tests by category
dotnet test --filter "Category=AdminTests"

# Run single test
dotnet test --filter "AddProductToAdminTest"
```

### Analyze Test Failures

1. Navigate to `Logs/[date]/` directory
2. Find the test run log file
3. Check for screenshots and source code captures
4. Review browser console logs for JavaScript errors

## 🐛 Known Issues

- **TC4bug.cs**: Demonstrates a known bug in the "Cameras" category where the Add to Cart button doesn't function correctly. A workaround is documented and tested separately in TC4.cs.

## 📚 Contributing

When adding new tests:
1. Follow PageObjectPattern for new pages/components
2. Use descriptive test names and comments
3. Ensure comprehensive error logging
4. Test locally before pushing
5. Include setup/teardown logic in BaseTest when needed

## 📄 License

[Add your license information here]

## 👥 Contact

For questions or issues, please open an issue on GitHub or contact the team.

---

**Last Updated:** March 10, 2026
**Framework Version:** .NET 8.0