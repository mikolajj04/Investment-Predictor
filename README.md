# MJ Invest Calc - Investment Predictor
![.NET Version](https://img.shields.io/badge/.NET-8.0-512bd4?style=flat&logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green?style=flat)
![Azure](https://img.shields.io/badge/Azure-Deployed-0089D6?style=flat&logo=microsoftazure)
![Status](https://img.shields.io/badge/Status-Active-success)

A professional investment calculator built with **.NET 8 Blazor**, designed to simulate capital growth while accounting for capital gains tax (Belka tax) and historical market index returns.

 **Live Application:** [mj-invest-calc.azurewebsites.net](https://mj-invest-calc.azurewebsites.net)

---

## 🛠️ Tech Stack
* **Framework:** C# .NET 8 (Blazor Web App)
* **Render Mode:** Interactive Server (Real-time UI updates via SignalR)
* **UI/UX:** Bootstrap 5 (Responsive Design / Mobile-First)
* **Charting:** ApexCharts.Blazor
* **Cloud/Hosting:** Microsoft Azure (App Service)
* **Architecture:** Clean Architecture (Separation of Concerns)

## Key Features
- [x] **Compound Interest Engine:** Real-time calculation of investment future value.
- [x] **Tax Logic:** Automated 19% capital gains tax deduction on profits.
- [x] **Modern UI:** Fully responsive "Drawer" style navigation menu for both Mobile and Desktop.
- [x] **Interactive Visualization:** Dynamic line charts showing capital growth over time.
- [ ] **AI Investment News (Upcoming):** Market insights aggregator powered by AI (OpenAI API).

## 🏗️ Project Structure
The project follows **Clean Architecture** principles to ensure scalability.
* **[InvestmentPredictor.Core:](./InvestmentPredictor/InvestmentPredictor.Core)** A Class Library containing all mathematical logic, financial formulas, and data models.
* **[InvestmentPredictor.WebApp:](./InvestmentPredictor/InvestmentCalculator.WebApp)** The Blazor presentation layer, handling the user interface and cloud integration.
* **[InvestmentPredictor.Console (Legacy/Testing):](./InvestmentPredictor/InvestmentPredictor.Console)** The initial version of the project. Currently used as a sandbox for rapid testing of new financial algorithms.
* **[InvestmentPredictor.api (Experimental):](InvestmentPredictor/InvestmentPredictor.api)** A REST API layer designed to potentially serve data to other clients (e.g., mobile apps) in the future.

## 📊 Calculation Methodology
* **Historical Data Benchmarking:** When selecting a market index (e.g., S&P 500), the system utilizes a 20-year historical average return (where applicable). This provides a smoothed, long-term perspective on market performance, filtering out short-term volatility.
* **Postnumerando Model:** The calculation engine follows the Postnumerando (Ordinary Annuity) convention.
    * Monthly subsidies are added at the end of each period.
    * Interest is compounded on the existing balance before the new monthly contribution is added.
    * This approach provides a more conservative and realistic growth simulation compared to the prenumerando (start-of-period) model.

## ⚙️ Local Setup
1. Clone the repository: `git clone https://github.com/mikolajj04/InvestmentPredictor.git`
2. Open the `.sln` file in Visual Studio 2022.
3. Ensure you have the .NET 8 SDK installed.
4. Run the `InvestmentPredictor.WebApp` project (F5).

---

## Author
**Mikołaj Jussak**
*Computer Science Student at Silesian University of Technology*
## ⚖️ License
Distributed under the **MIT License**. See [`LICENSE`](./LICENSE) for more information.
