# RoMock CI/CD Pipeline Documentation

This document explains the complete CI/CD pipeline setup for the RoMock MAUI library, including all files and their purposes.

## 📁 Pipeline Files Overview

### 1. `.github/workflows/ci-cd.yml` - Main Pipeline
**Purpose**: The core GitHub Actions workflow that orchestrates the entire CI/CD process.

**Key Features**:
- **4 Jobs** that run in parallel and sequence
- **Cross-platform builds** (Windows, macOS, Linux)
- **Automatic semantic versioning** based on commit messages
- **NuGet package publishing** to NuGet.org
- **Conditional job execution** based on semantic-release output

**Jobs Explained**:

#### Job 1: `build-and-test`
- **Purpose**: Builds the solution and runs all unit tests
- **Triggers**: Every push to main branch
- **Steps**:
  1. Checkout code with full history
  2. Setup .NET 8.0 environment
  3. Restore NuGet packages (library and tests only)
  4. Build solution in Release mode
  5. Run tests

#### Job 2: `semantic-version`
- **Purpose**: Analyzes commit messages and determines next version
- **Triggers**: Only on main branch after successful build-and-test
- **Outputs**: 
  - `new_release_published`: Boolean indicating if a new version was created
  - `new_release_version`: The new version number (if created)
- **Steps**:
  1. Checkout with full history (required for semantic analysis)
  2. Setup Node.js for semantic-release
  3. Install npm dependencies
  4. Run semantic-release to analyze commits

#### Job 3: `build-package`
- **Purpose**: Creates NuGet package with new version
- **Triggers**: Only when semantic-release creates a new version
- **Conditions**: `github.ref == 'refs/heads/main' && needs.semantic-version.outputs.new_release_published == 'true'`
- **Steps**:
  1. Extract version from semantic-release output
  2. Update version in `.csproj` file
  3. Build NuGet package
  4. Upload package as artifact

#### Job 4: `publish-nuget`
- **Purpose**: Publishes package to NuGet.org
- **Triggers**: Only when a new version is created
- **Conditions**: `github.ref == 'refs/heads/main' && needs.semantic-version.outputs.new_release_published == 'true'`
- **Steps**:
  1. Download package artifact
  2. Publish to NuGet.org using API key

### 2. `.releaserc.json` - Semantic Release Configuration
**Purpose**: Configures how semantic-release analyzes commits and creates releases.

**Plugins Explained**:

#### `@semantic-release/commit-analyzer`
- **What it does**: Reads commit messages and determines version bump
- **Version rules**:
  - `feat:` → Minor version (1.0.0 → 1.1.0)
  - `fix:` → Patch version (1.0.0 → 1.0.1)
  - `feat!:` or `BREAKING CHANGE:` → Major version (1.0.0 → 2.0.0)
  - `docs:`, `style:`, `refactor:`, `perf:`, `test:`, `chore:` → No version bump

#### `@semantic-release/release-notes-generator`
- **What it does**: Creates readable release notes from commit messages
- **Output**: Groups commits by type and creates a summary

#### `@semantic-release/changelog`
- **What it does**: Creates and updates `CHANGELOG.md` file
- **Purpose**: Maintains complete history of all releases

#### `@semantic-release/npm`
- **What it does**: Handles npm package operations
- **Configuration**: `npmPublish: false` (we publish to NuGet, not npm)

#### `@semantic-release/github`
- **What it does**: Creates GitHub releases and uploads assets
- **Assets**: Uploads NuGet package as release asset

### 3. `package.json` - Node.js Dependencies
**Purpose**: Defines npm dependencies required for semantic-release.

**Dependencies**:
- `semantic-release`: Core package that orchestrates the release process
- `@semantic-release/commit-analyzer`: Analyzes commit messages
- `@semantic-release/release-notes-generator`: Generates release notes
- `@semantic-release/changelog`: Manages changelog file
- `@semantic-release/github`: Creates GitHub releases
- `@semantic-release/npm`: Handles npm operations (disabled)

**Scripts**:
- `release`: Runs semantic-release locally for testing

## 🔄 How the Pipeline Works

### 1. Development Workflow
```
Feature Branch → Commit with conventional format → Push → PR → Merge to main
```

### 2. Release Process
```
Main Branch → Build & Test → Semantic Analysis → Version Bump → Package Build → NuGet Publish → GitHub Release
```

### 3. Conditional Execution
- **Build & Test**: Always runs on main branch pushes
- **Semantic Versioning**: Always runs after successful build & test
- **Package Build**: Only runs when semantic-release creates a new version
- **NuGet Publish**: Only runs when a new version is created

### 4. Commit Message Examples
```bash
# New feature (minor version bump)
git commit -m "feat: add support for async method mocking"

# Bug fix (patch version bump)
git commit -m "fix: resolve dependency injection issue"

# Breaking change (major version bump)
git commit -m "feat!: change API interface"

# Documentation (no version bump)
git commit -m "docs: update README with examples"
```

## 🛠️ Setup Requirements

### 1. GitHub Repository Settings
- **Actions**: Enable GitHub Actions
- **Branch Protection**: Require PR reviews and status checks
- **Secrets**: Add `NUGET_API_KEY` and `PAT_TOKEN` for publishing

### 2. Required Secrets
- `NUGET_API_KEY`: Your NuGet API key for publishing packages
- `PAT_TOKEN`: Personal Access Token with repo permissions for semantic-release

### 3. Local Setup
```bash
# Install dependencies
npm install

# Test semantic-release locally
npm run release -- --dry-run

# The pipeline will work automatically when you push to main
```

## 📊 Pipeline Benefits

### ✅ **Automated Versioning**
- No manual version management
- Semantic versioning based on commit types
- Automatic changelog generation
- Conditional job execution to avoid unnecessary builds

### ✅ **Quality Assurance**
- Automated testing on every commit
- Cross-platform compatibility checks
- Build verification before release
- Only builds packages when new versions are created

### ✅ **Streamlined Publishing**
- Automatic NuGet package creation
- One-click publishing to NuGet.org
- GitHub releases with assets
- Efficient resource usage with conditional execution

### ✅ **Developer Experience**
- Clear commit message conventions
- Automated release process
- Comprehensive documentation
- Local testing capabilities

## 🚀 Getting Started

1. **Install dependencies**: `npm install`
2. **Add required secrets** to GitHub repository settings
3. **Test locally**: `npm run release -- --dry-run`
4. **Make your first commit** with conventional format:
   ```bash
   git commit -m "feat: initial release setup"
   git push origin main
   ```
5. **Monitor the pipeline** in GitHub Actions tab
6. **Check the release** in GitHub Releases tab

The pipeline will automatically handle everything else! 