# Investment-Predictor

A professional .NET 8 console application designed to forecast long-term investment growth. This tool uses historical market data and compound interest logic to provide realistic financial projections.

## 🚀 Key Features

* **Compound Interest Engine:** Precise calculations using the `decimal` type to ensure financial accuracy.
* **Monthly Capitalization:** Realistic simulation of monthly contributions and interest accrual.
* **Input Validation:** Robust error handling using `TryParse` patterns to ensure system stability.
* **Separation of Concerns:** Clean architecture with logic decoupled from the User Interface.

## 🚀 Update v0.4: Web API Prototype (ASP.NET Core)

The project has been refactored from a CLI tool into a **Web API prototype**, demonstrating a modern tiered architecture. This is a work-in-progress version that will be further expanded with new features.

### Highlights:
* **Clean Architecture**: Core logic is isolated in a shared `.Core` library for multi-platform support.
* **Dependency Injection**: Implements `IInvestmentCalculator` for decoupled and testable code.
* **Interactive API**: Integrated **Swagger/OpenAPI** for real-time endpoint testing via JSON.

