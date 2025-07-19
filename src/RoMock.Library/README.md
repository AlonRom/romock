# RoMock.Library

A .NET MAUI library for mocking services and repositories with a simple and elegant approach.

## Features

- Easy-to-use mocking framework for .NET MAUI applications
- Attribute-based mock configuration
- Automatic dependency injection setup
- Support for multiple platforms (Android, iOS, macOS, Windows)

## Installation

```bash
dotnet add package RoMock.Library
```

## Quick Start

1. Add the `[MockableMethod]` attribute to your service methods
2. Register your services with the RoMock extensions
3. Use the RoMock service to configure mocks at runtime

## Usage

```csharp
// In your service
public interface IUserService
{
    [MockableMethod]
    Task<User> GetUserAsync(int id);
}

// In your MauiProgram.cs
builder.Services.AddRoMock();
builder.Services.AddScoped<IUserService, UserService>();

// Configure mocks at runtime
var roMockService = app.Services.GetRequiredService<IRoMockService>();
roMockService.SetMock<IUserService>(service => 
    service.GetUserAsync(1).Returns(new User { Id = 1, Name = "Mock User" }));
```

## License

MIT License - see LICENSE file for details. 