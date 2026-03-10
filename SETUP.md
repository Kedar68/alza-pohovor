# Complete Setup Guide for UI.Template

This guide provides step-by-step instructions to set up and run the UI.Template automated testing framework from scratch.

## Table of Contents

1. [System Prerequisites](#system-prerequisites)
2. [Clone and Initial Setup](#clone-and-initial-setup)
3. [Environment Configuration](#environment-configuration)
4. [Building the Project](#building-the-project)
5. [Running Tests](#running-tests)
6. [Troubleshooting](#troubleshooting)
7. [CI/CD Integration](#cicd-integration)

---

## System Prerequisites

### Required Software

1. **.NET 8.0 SDK LTS** (or later)
   - Download from: https://dotnet.microsoft.com/download/dotnet/8.0
   - Verify installation: `dotnet --version`

2. **Chrome Browser**
   - Download from: https://www.google.com/chrome/
   - Required for Selenium WebDriver automation
   - ChromeDriver (v139+) is included via NuGet package

3. **Git** (for version control)
   - Download from: https://git-scm.com/
   - Verify: `git --version`

4. **IDE (Optional but recommended)**
   - Visual Studio Code
   - Visual Studio Community/Professional
   - JetBrains Rider

### Verify Prerequisites

```powershell
# Check .NET SDK
dotnet --version
# Expected output: 8.0.x or higher

# Check Git
git --version
# Expected output: git version 2.x.x or higher
```

---

## Clone and Initial Setup

### Step 1: Clone the Repository

```bash
git clone https://github.com/yourusername/UI.Template.git
cd UI.Template
```

### Step 2: Verify Initial Structure

```bash
# Verify key directories exist
dir src/
dir docs/
dir src/UI.Template/Tests/
```

### Step 3: Restore Dependencies

```bash
dotnet restore
# This downloads all NuGet packages
# You should see: "Restore completed in..." message
```

---

## Environment Configuration

### Step 1: Create Local Configuration File

Located in: `src/UI.Template/`

```bash
# Copy the existing configuration template
copy src/UI.Template/appsettings.json src/UI.Template/appsettings.local.json
```

**Important:** `appsettings.local.json` is included in `.gitignore` to keep sensitive data out of version control.

### Step 2: Update Configuration Values

Edit `src/UI.Template/appsettings.local.json`:

```json
{
  "WebConfiguration": {
    "BaseUrl": "https://your-app-url.com",
    "UserName": "your-test-username",
    "UserPassword": "your-test-password",
    "BrowserType": "Chrome",
    "Headless": false,
    "ImplicitWaitSeconds": 10,
    "PageLoadTimeoutSeconds": 30
  },
  "TestConfiguration": {
    "LogsPath": "../../../Logs"
  },
  "Serilog": {
    "MinimumLevel": "Verbose",
    "WriteTo:File": {
      "Name": "File",
      "Args": {
        "path": "../../../Logs/.log",
        "rollingInterval": "Day",
        "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] [{TestMethod}] {Message:lj}{NewLine}{Exception}"
      }
    },
    "WriteTo:NUnit": {
      "Name": "NUnitOutput",
      "Args": {
        "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] [{TestMethod}] {Message:lj}{NewLine}{Exception}",
        "restrictedToMinimumLevel": "Verbose"
      }
    }
  }
}
```

### Configuration Parameters Explained

| Parameter | Purpose | Example |
|-----------|---------|---------|
| `BaseUrl` | Application URL to test | `https://demo.eshop.com` |
| `UserName` | Test user login | `testuser@example.com` |
| `UserPassword` | Test user password | `SecurePassword123` |
| `BrowserType` | Browser to automate | `Chrome`, `Firefox` |
| `Headless` | Run without GUI | `false` for visible browser |
| `ImplicitWaitSeconds` | Default element wait time | `10` seconds |
| `LogsPath` | Artifact storage location | `../../../Logs` |

---

## Building the Project

### Step 1: Build the Solution

```bash
dotnet build
```

**Expected output:**
```
Build succeeded. 0 Warning(s)
```

### Step 2: Verify Build

```bash
# Check if DLL files were created
dir src/UI.Template/bin/Debug/net8.0/
```

---

## Running Tests

### Basic Test Execution

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run with XML report
dotnet test --logger "nunit;LogFileName=TestResults-{date}.xml"
```

### Filter Tests

```bash
# Run specific test by name
dotnet test --filter "AddProductToAdminTest"

# Run tests matching pattern
dotnet test --filter "TC*"

# Run tests in specific category
dotnet test --filter "Category=Smoke"
```

### Example Test Runs

```bash
# Run TC3 test
dotnet test --filter "TC3"

# Run TC4 (main checkout flow)
dotnet test --filter "TC4" --logger "console;verbosity=detailed"

# Run all tests with custom timeout (important for slow networks)
dotnet test --configuration Debug --verbosity normal --logger:console
```

---

## Analyzing Test Results

### Log Files Location

Logs are organized by date in: `Logs/[YYYY-MM-DD]/`

```
Logs/
├── 2026-03-08/
│   ├── test_run_20_06_00.log
│   └── screenshots/
│       └── error_20_06_00.png
├── 2026-03-10/
│   ├── test_run_20_18_52.log
│   └── browser_logs/
│       └── error_20_18_52.log
```

### On Test Failure, Framework Captures:

1. **Screenshots** - `screenshot_[testname].png`
2. **Page Source** - `page_source_[testname].html`
3. **Browser Console Logs** - `browser_logs_[testname].log`
4. **Test Execution Log** - `test_run_[timestamp].log`

### Example Error Analysis

```bash
# Navigate to latest logs
cd Logs
dir /O:D /T:W  # Sort by date, newest first

# View test log
type 2026-03-10\test_run_20_18_52.log

# View screenshot
start 2026-03-10\screenshots\error.png
```

---

## Troubleshooting

### Issue: "Chrome is not installed"

```
Error: chromedriver version mismatch or Chrome not found
```

**Solution:**
1. Install Chrome from https://google.com/chrome
2. Verify Chrome is in PATH
3. Reinstall ChromeDriver package:
   ```bash
   dotnet package remove Selenium.WebDriver.ChromeDriver
   dotnet restore
   ```

### Issue: "appsettings.local.json not found"

```
Error: Could not load file 'appsettings.local.json'
```

**Solution:**
```bash
# Create the file
copy src/UI.Template/appsettings.json src/UI.Template/appsettings.local.json

# Update values with valid credentials
```

### Issue: "Timeout waiting for element"

**Causes:**
- Element selector is incorrect
- Application is slow
- Network connectivity issues

**Debug Steps:**
1. Check logs in `Logs/[date]/` directory
2. Review screenshot of failure point
3. Verify `ImplicitWaitSeconds` in configuration
4. Check browser console logs for JavaScript errors

### Issue: "Tests pass locally but fail in CI/CD"

**Common Causes:**
- Different environment configuration
- Missing environment variables
- Browser configuration differences (headless vs headed)

**Solution:**
```bash
# Run tests in headless mode to match CI/CD
# Update appsettings.local.json:
# "Headless": true

dotnet test
```

---

## CI/CD Integration

### GitHub Actions Example

Create `.github/workflows/tests.yml`:

```yaml
name: UI Tests

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
  test:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore
    
    - name: Copy configuration
      run: cp src/UI.Template/appsettings.json src/UI.Template/appsettings.local.json
    
    - name: Run tests
      run: dotnet test --no-build --logger "trx" --collect:"XPlat Code Coverage"
    
    - name: Upload test results
      if: always()
      uses: actions/upload-artifact@v3
      with:
        name: test-results
        path: TestResults/
```

---

## Git Workflow

### Initial Repository Setup

```bash
# Initialize repository (if not cloned)
git init
git add .
git commit -m "Initial commit: UI.Template framework"
git branch -M main
git remote add origin https://github.com/yourusername/UI.Template.git
git push -u origin main
```

### Daily Workflow

```bash
# Create feature branch
git checkout -b feature/add-new-tests

# Make changes and test
dotnet test

# Commit changes
git add .
git commit -m "Add new test cases for checkout flow"

# Push and create pull request
git push origin feature/add-new-tests
```

### Files NOT Committed (Ignored by Git)

- `appsettings.local.json` - Contains credentials
- `Logs/` - Test artifacts and logs
- `TestResults/` - NUnit test result files
- `bin/`, `obj/` - Build output
- `.vs/`, `.vscode/settings.json` - IDE settings

---

## Additional Resources

- [NUnit Documentation](https://docs.nunit.org/)
- [Selenium WebDriver Documentation](https://www.selenium.dev/documentation/)
- [Serilog Documentation](https://serilog.net/)
- [.NET 8.0 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)

---

## Next Steps

1. ✅ Clone repository
2. ✅ Setup .NET SDK
3. ✅ Configure appsettings.local.json
4. ✅ Run `dotnet restore`
5. ✅ Run `dotnet test` to verify setup
6. 📖 Read [Guidelines](docs/Guidelines.md) for development standards
7. 🧪 Create new tests following PageObjectPattern
8. 📤 Push changes to GitHub

---

**Setup Guide Last Updated:** March 10, 2026
**Compatible With:** .NET 8.0 LTS and later
