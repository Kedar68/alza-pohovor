# GitHub Handover Guide

This document outlines how to properly share and maintain the UI.Template automated testing framework on GitHub.

## Table of Contents

1. [Project Overview](#project-overview)
2. [Repository Structure](#repository-structure)
3. [Initial Repository Setup](#initial-repository-setup)
4. [Pushing to GitHub](#pushing-to-github)
5. [Branch Strategy](#branch-strategy)
6. [Collaboration Guidelines](#collaboration-guidelines)
7. [Issues and Pull Requests](#issues-and-pull-requests)

---

## Project Overview

**UI.Template** is a comprehensive UI automation testing framework for .NET 8.0 applications using:
- **Selenium WebDriver** for browser automation
- **NUnit** for test execution
- **PageObjectPattern** for maintainable test code
- **Serilog** for comprehensive logging
- **ChromeDriver** for Chrome browser automation

### Key Features
✅ Automatic screenshot capture on test failures
✅ Comprehensive logging with timestamps and error details
✅ Browser console log capture
✅ Page source HTML capture for debugging
✅ Multi-environment configuration support
✅ Reusable PageObject components

---

## Repository Structure

```
UI.Template/
├── .github/              # GitHub-specific files (workflows, templates)
├── docs/                 # Documentation
│   ├── Guidelines.md
│   ├── Introduction.md
│   ├── Prerequisites.md
│   └── Running-Tests.md
├── src/
│   └── UI.Template/      # Main test project
│       ├── Components/   # PageObject components
│       ├── Pages/        # Page object models
│       ├── Tests/        # Test cases (TC3, TC4, TC4bug, etc.)
│       ├── Framework/    # Core testing framework
│       ├── Data/         # Test data
│       ├── Models/       # Domain objects
│       ├── appsettings.json    # Configuration template
│       └── UI.Template.csproj
├── README.md             # Project overview
├── SETUP.md              # Detailed setup instructions
├── UI.Template.sln       # Visual Studio solution
├── .gitignore            # Git ignore patterns
├── .gitattributes        # Git attributes
└── .editorconfig         # Code style configuration
```

---

## Initial Repository Setup

### Step 1: Clean Up Before Committing

```bash
# Ensure no local artifacts are staged
cd c:\Users\honza\Downloads\TestTemplate

# Remove any local logs/artifacts
Remove-Item Logs -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item TestResults -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item src\UI.Template\bin -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item src\UI.Template\obj -Recurse -Force -ErrorAction SilentlyContinue

# Verify git ignore is working
git status
# Should NOT show Logs/ or TestResults/ folders
```

### Step 2: Configure Git Identity (Local Machine)

```bash
# Configure git for this repository
git config user.name "Your Name"
git config user.email "your.email@example.com"

# Verify configuration
git config --list | Select-String "user"
```

### Step 3: Stage and Commit Initial Files

```bash
# Stage all files (respecting .gitignore)
git add .

# Verify staged files (should NOT include Logs/ or TestResults/)
git status

# Create initial commit
git commit -m "Initial commit: UI.Template automated testing framework

- PageObject pattern implementation with Selenium WebDriver
- Comprehensive logging and error artifact capture
- NUnit test framework with base test class
- Configuration management for multiple environments
- Example test cases demonstrating framework usage (TC3, TC4)
- Complete setup and development documentation"
```

---

## Pushing to GitHub

### Step 1: Create Repository on GitHub

1. Go to https://github.com/new
2. Enter Repository name: `UI.Template`
3. Description: `Automated UI testing framework for .NET 8.0 using Selenium and NUnit`
4. Choose visibility: **Public** (for sharing with team) or **Private**
5. Do NOT initialize with README, .gitignore, or license (we have these)
6. Click "Create repository"

### Step 2: Add Remote and Push

```bash
# Add remote repository
git remote add origin https://github.com/yourusername/UI.Template.git

# Verify remote
git remote -v
# Expected output:
# origin  https://github.com/yourusername/UI.Template.git (fetch)
# origin  https://github.com/yourusername/UI.Template.git (push)

# Rename branch to main (if needed)
git branch -M main

# Push to GitHub
git push -u origin main
# You may be prompted for GitHub credentials or SSH key
```

### Step 3: Add Default Branch Protection (Optional but Recommended)

1. Go to Repository Settings → Branches
2. Add rule for `main` branch
3. Enable "Require pull request reviews"
4. Enable "Require status checks to pass"
5. Enable "Require branches to be up to date before merging"

---

## Branch Strategy

### Main Branches

| Branch | Purpose | Created From |
|--------|---------|--------------|
| `main` | Production-ready releases | pull requests |
| `develop` | Integration branch for features | from `main` |

### Feature Branches

```bash
# Create feature branch
git checkout -b feature/add-login-tests

# Make changes and test locally
dotnet test

# Stage and commit
git add .
git commit -m "Add login page tests with error handling"

# Push feature branch
git push origin feature/add-login-tests

# Create Pull Request on GitHub UI
```

### Bugfix Branches

```bash
# Create hotfix branch
git checkout -b bugfix/fix-screenshot-capture

# Fix the issue and test
dotnet test

# Push and create PR
git push origin bugfix/fix-screenshot-capture
```

---

## Collaboration Guidelines

### Commit Message Format

Follow conventional commit format:

```
<type>(<scope>): <subject>

<body>

<footer>
```

**Types:**
- `feat` - New feature or test case
- `fix` - Bug fix
- `docs` - Documentation update
- `refactor` - Code restructuring
- `chore` - Build, dependencies, configuration
- `test` - Test improvements

**Examples:**

```bash
# Good commit messages
git commit -m "feat(tests): add checkout flow test

- Implements TC4 test case for complete checkout scenario
- Validates shipping info, payment method, and order total
- Includes error handling for payment failures"

git commit -m "fix(framework): handle page load timeouts gracefully

- Increase implicit wait for slow network conditions
- Add retry logic for element visibility checks
- Log performance metrics"

git commit -m "docs: update setup guide with troubleshooting section"
```

### Code Review Checklist

Before creating a Pull Request:

- [ ] Code follows project guidelines (see [Guidelines.md](docs/Guidelines.md))
- [ ] Tests pass: `dotnet test`
- [ ] No sensitive data in commits (credentials, API keys)
- [ ] Commit messages are clear and descriptive
- [ ] Documentation is updated if new features added
- [ ] `appsettings.local.json` is NOT committed

---

## Issues and Pull Requests

### Creating Issues

Use GitHub Issues to track:
- **Bug Reports** - Unexpected framework behavior
- **Feature Requests** - New test cases or framework improvements
- **Documentation** - Missing or unclear documentation

**Issue Template:**

```markdown
## Description
Brief description of the issue

## Steps to Reproduce (for bugs)
1. Run test: `dotnet test --filter "TestName"`
2. Observe the error in: `Logs/[date]/test_run.log`

## Expected Behavior
What should happen

## Actual Behavior
What actually happens

## Environment
- Windows 10 / 11 / Linux
- .NET version: 8.0.x
- Chrome version: x.x.x

## Additional Context
Screenshots, logs, or other relevant information
```

### Creating Pull Requests

1. **Before pushing:**
   ```bash
   dotnet test
   dotnet build
   ```

2. **Create descriptive PR:**
   - Clear title: "Add new admin page tests"
   - Detailed description of changes
   - Link related issues: "Closes #42"
   - Request reviewers

3. **PR Description Template:**

```markdown
## Description
What changes does this PR introduce?

## Type of Change
- [ ] New test cases
- [ ] Framework enhancement
- [ ] Bug fix
- [ ] Documentation update

## Related Issues
Closes #123

## Testing
How have you tested these changes?
```

---

## Common Git Operations

### Sync with Remote

```bash
# Fetch latest changes
git fetch origin

# Update current branch
git pull origin develop

# Create local branch from remote
git checkout -b feature/new-feature origin/feature/new-feature
```

### Undo Changes

```bash
# Discard changes in working directory
git checkout -- src/UI.Template/Tests/NewTest.cs

# Unstage file
git reset HEAD src/UI.Template/Tests/NewTest.cs

# Undo last commit (keep changes)
git reset --soft HEAD~1

# Undo last commit (lose changes)
git reset --hard HEAD~1
```

### View History

```bash
# View commit history
git log --oneline -10

# View changes in current branch
git diff origin/main..HEAD

# View file history
git log --oneline -- src/UI.Template/Tests/TC3.cs
```

---

## Continuous Integration/Deployment

### GitHub Actions (Optional Setup)

Create `.github/workflows/test.yml` for automated testing:

```yaml
name: Run Automated Tests

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
  test:
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v3
    - uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    - run: dotnet restore
    - run: dotnet build
    - run: dotnet test --logger trx
    - name: Upload test results
      if: always()
      uses: actions/upload-artifact@v3
      with:
        name: test-results
        path: TestResults/
```

---

## Troubleshooting GitHub Operations

### Issue: "Permission denied (publickey)"

**Solution:**
```bash
# Generate SSH key
ssh-keygen -t ed25519 -C "your_email@example.com"

# Add to GitHub Settings → SSH Keys
# Copy content of ~/.ssh/id_ed25519.pub
```

### Issue: ".gitignore not working"

**Solution:**
```bash
# Remove cached files
git rm -r --cached .
git add .
git commit -m "Fix: update gitignore and remove cached files"
```

### Issue: "Accidentally committed sensitive data"

**Solution:**
```bash
# Remove file from history
git filter-branch --tree-filter 'rm -f appsettings.local.json' HEAD

# Or use BFG tool for large files
# Then update GitHub credentials if API keys were exposed
```

---

## Next Steps for Team

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/UI.Template.git
   cd UI.Template
   ```

2. **Follow SETUP.md:**
   ```bash
   # Create local configuration
   copy src/UI.Template/appsettings.json src/UI.Template/appsettings.local.json
   
   # Update credentials
   # Run tests
   dotnet test
   ```

3. **Create Feature Branch:**
   ```bash
   git checkout -b feature/your-feature-name
   ```

4. **Make Changes and Push:**
   ```bash
   dotnet test
   git add .
   git commit -m "feat: your feature description"
   git push origin feature/your-feature-name
   ```

5. **Create Pull Request:**
   - Go to GitHub repository
   - Click "Compare & pull request"
   - Follow PR template
   - Request reviewers

---

## Resources

- [GitHub Docs](https://docs.github.com/)
- [Git Cheat Sheet](https://github.github.com/training-kit/downloads/github-git-cheat-sheet.pdf)
- [Conventional Commits](https://www.conventionalcommits.org/)
- [How to Write Good Commit Messages](https://tbaggery.com/writing-good-commit-messages.html)

---

**GitHub Handover Guide**
Last Updated: March 10, 2026
Framework: UI.Template with .NET 8.0 LTS
