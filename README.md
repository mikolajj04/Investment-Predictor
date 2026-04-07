# 📈 MJ Invest Calc - Investment Predictor

A professional investment calculator built with **.NET 8 Blazor**, designed to simulate capital growth while accounting for inflation, capital gains tax (Belka tax), and historical market index returns.

🚀 **Live Application:** [mj-invest-calc.azurewebsites.net](https://mj-invest-calc.azurewebsites.net)

---

## 🛠️ Tech Stack
* **Backend/Frontend:** C# .NET 8 (Blazor Web App)
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
The project follows **Clean Architecture** principles to ensure scalability:
* **[InvestmentPredictor.Core:](./InvestmentPredictor/InvestmentPredictor.Core)** A Class Library containing all mathematical logic, financial formulas, and data models.
* **[InvestmentPredictor.WebApp:](./InvestmentPredictor/InvestmentCalculator.WebApp)** The Blazor presentation layer, handling the user interface and cloud integration.

## ⚙️ Local Setup
1. Clone the repository: `git clone https://github.com/mikolajj04/InvestmentPredictor.git`
2. Open the `.sln` file in Visual Studio 2022.
3. Ensure you have the .NET 8 SDK installed.
4. Run the `InvestmentPredictor.WebApp` project (F5).

---

## 👤 Author
**Mikołaj Jussak**
*Computer Science Student at Silesian University of Technology*
