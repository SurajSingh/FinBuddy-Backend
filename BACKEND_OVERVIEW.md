# FinBuddy - Backend Infrastructure Overview & Progress

This document summarizes the backend architecture, implemented modules, and technical roadmap for the FinBuddy platform.

## 1. What is this Project?
The **FinBuddy Backend** is a high-performance, enterprise-grade API built to support the FinBuddy mobile application. It handles complex financial calculations, debt strategy modeling, and secure user data management. It is designed to be the "brain" of the financial coach, providing the computational logic required for expert strategy.

## 2. Technical Architecture & Understanding
The backend is structured using **Clean Architecture** principles to ensure loose coupling and high maintainability:

- **Core Framework**: .NET 10.0 (Latest Long-Term Support).
- **Architecture Layers**:
    - **FinCoach.Domain**: Pure business logic, core entities (User, Transaction, Goal, EMI), and enums.
    - **FinCoach.Application**: MediatR-based Commands and Queries, DTOs, and interfaces.
    - **FinCoach.Infrastructure**: EF Core implementation, SQL Server integration, Identity/JWT services, and Repositories.
    - **FinCoach.Api**: ASP.NET Core controllers, middleware, and OpenAPI/Scalar documentation.
- **Design Patterns**: Used the **Unit of Work** and **Repository Pattern** for consistent data access and **MediatR** for clean request/response handling.

## 3. Backend Functionalities Covered (Till Now)
The following infrastructure components and business modules are fully operational:

### **Core Systems**
- **Identity & Security**: Full JWT Bearer authentication system with Token generation and validation.
- **Automated Data Lifecycle**: Self-migrating database on startup and automated seeding for Countries, Currencies, and Lookups.
- **Global Settings**: Support for multi-currency and localization (Phone codes, Flags, Symbols).

### **Business Modules**
- **User Management**: Support for profile creation, onboarding status tracking, and income management.
- **Strategy Engine (Debt/EMI)**: Sophisticated tracking of Loan Principals, Interest Rates, and Tenure to calculate Debt-to-Income ratios.
- **Wealth Milestones**: Progress calculation logic for financial goals.
- **Transaction Engine**: Category-aware financial logging with support for income vs. expense tracking.

### **Developer Experience**
- **Scalar API Reference**: Premium interactive API documentation integrated at `/scalar/v1`.
- **Global Error Handling**: Centralized logging and error response structures.
- **Schema Precision**: Global enforcement of decimal precision (18, 2) for all financial data to prevent rounding errors.

## 4. Next Steps & Backend Roadmap
The backend roadmap for the next development sprint includes:

- [ ] **AI-Driven Advisory Logic**: Integrating specialized endpoints for the AI Coach to retrieve financial health snapshots.
- [ ] **Debt Elimination Strategies**: Implementing server-side calculations for Snowball vs. Avalanche repayment models.
- [ ] **Event-Driven Notifications**: Setting up background tasks for payment reminders and goal achievement alerts.
- [ ] **Infrastructure Hardening**: Transitioning from hardcoded secrets to Environment Variables/Secrets Manager for production deployment.
- [ ] **Performance Benchmarking**: Stress testing complex financial aggregations as user datasets grow.

---
*Last Updated: April 8, 2026*
