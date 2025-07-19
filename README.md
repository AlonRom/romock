# RoMock

A .NET MAUI library for mocking services and repositories with a simple and elegant approach.

## 🚀 Features

- **Easy-to-use mocking framework** for .NET MAUI applications
- **Attribute-based mock configuration** with `[MockableMethod]`
- **Automatic dependency injection setup** with RoMock extensions
- **Cross-platform support** (Android, iOS, macOS, Windows)
- **Semantic versioning** with automated releases
- **CI/CD pipeline** with automated testing and publishing

## 📦 Installation

```bash
dotnet add package RoMock.Library
```

## 🛠️ Quick Start

### 1. Add the `[MockableMethod]` attribute to your service methods

```csharp
public interface IUserService
{
    [MockableMethod]
    Task<User> GetUserAsync(int id);
    
    [MockableMethod]
    Task<List<User>> GetAllUsersAsync();
}
```

### 2. Register your services with RoMock extensions

```csharp
// In your MauiProgram.cs
builder.Services.AddRoMock();
builder.Services.AddScoped<IUserService, UserService>();
```

### 3. Configure mocks at runtime

```csharp
// Get the RoMock service
var roMockService = app.Services.GetRequiredService<IRoMockService>();

// Configure mocks
roMockService.SetMock<IUserService>(service => 
    service.GetUserAsync(1).Returns(new User { Id = 1, Name = "Mock User" }));
```

## 📚 Documentation

- **[Library Documentation](src/RoMock.Library/README.md)** - Detailed usage guide
- **[CI/CD Pipeline Documentation](PIPELINE_DOCUMENTATION.md)** - Build and release process
- **[Example Application](example/RoMock.Example.App/)** - Complete working example

## 🔄 Development Workflow

This project uses **conventional commits** and **semantic versioning**:

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

## 🏗️ Building from Source

```bash
# Clone the repository
git clone https://github.com/AlonRom/romock.git
cd romock

# Install dependencies
npm install

# Build the solution
dotnet build

# Run tests
dotnet test
```

## 📄 License

MIT License - see [LICENSE](LICENSE) file for details.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Commit your changes: `git commit -m 'feat: add amazing feature'`
4. Push to the branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

## 📊 Status

[![CI/CD Pipeline](https://github.com/AlonRom/romock/workflows/CI/CD%20Pipeline/badge.svg)](https://github.com/AlonRom/romock/actions)
[![NuGet](https://img.shields.io/nuget/v/RoMock.Library.svg)](https://www.nuget.org/packages/RoMock.Library/)
