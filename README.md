# Core Backend

## Overview

Welcome to the `core-backend` repository.

This repository follows a **monorepo architecture** designed to centralize the application's backend services, serverless components, and shared libraries into a single maintainable and scalable solution.

The primary goal of this repository is to provide:
- Separation of concerns
- Independent deployability
- Shared standards and reusable components
- Simplified collaboration across teams
- Reduced repository fragmentation

This repository contains application source code only.  
Infrastructure, CI/CD pipelines, Step Functions orchestration, and environment provisioning are managed separately in the dedicated DevOps repository.

---

# Repository Goals

The intention behind this structure is to:

- Avoid maintaining multiple repositories with duplicated configurations and implementations
- Promote code reuse through shared libraries
- Allow applications and Lambda functions to evolve independently
- Keep business logic centralized and organized
- Support modernization and future scalability efforts

---

# Repository Structure

```text
tps-platform/
│
├── applications/
│
├── serverless/
│   ├── lambdas/
│   └── shared/
│
├── docs/
│
├── Directory.Build.props
├── Directory.Packages.props
└── README.md
```

---

# Applications

`applications/`

Contains full backend applications and APIs.

Examples:
- Customer APIs
- Admin APIs
- Internal services
- Future backend applications

Each application should remain isolated and maintain its own responsibility boundaries.

---

# Serverless

`serverless/`

Contains AWS Lambda functions and shared serverless-related components.

---

# Lambdas

`serverless/lambdas/`

Each Lambda should:
- Have a single responsibility
- Be independently deployable
- Contain minimal dependencies
- Avoid coupling with other Lambdas

Example:

```text
serverless/lambdas/process-payment/
```

---

# Shared Libraries

`serverless/shared/`

Contains reusable libraries shared across applications and Lambdas.

Examples:
- Shared contracts
- DTOs
- Logging utilities
- Common services
- Infrastructure abstractions

Recommended approach:
- Keep libraries focused and modular
- Avoid creating a large generic shared library

---

# Architecture Principles

## Separation of Concerns

Applications, Lambdas, and shared libraries should remain logically separated.

Avoid mixing:
- Infrastructure logic
- Deployment concerns
- Business domains
- Workflow orchestration

---

# Independent Deployability

Even though the repository is centralized, components should still be deployable independently.

Examples:
- A Lambda deployment should not require redeploying the entire repository
- An API update should not impact unrelated services

---

# Shared Standards

The monorepo approach helps maintain:
- Consistent package versions
- Shared coding standards
- Unified architecture practices
- Common tooling

---

# What Is NOT Managed Here

The following responsibilities are intentionally managed in the separate DevOps repository:

- Terraform
- Infrastructure provisioning
- AWS Step Functions orchestration
- CI/CD pipelines
- Environment management
- Deployment scripts
- Monitoring and observability configuration

This repository focuses exclusively on application and business logic.

---

# Development Guidelines

## Keep Components Small

Prefer:
- Small focused Lambdas
- Isolated services
- Clear ownership boundaries

Avoid:
- Large multi-purpose Lambdas
- Shared "god" libraries
- Tight coupling between applications

---

# Naming Conventions

## Lambda Names

```text
validate-order
process-payment
send-notification
```

## Shared Libraries

```text
shared-contracts
shared-logging
shared-core
```

---

# Recommended Practices

- Write unit tests for all critical business logic
- Keep handlers thin
- Place business logic into services
- Reuse shared contracts where possible
- Favor composition over duplication

---

# Getting Started

## Requirements

- .NET 10 SDK
- Amazon.Lambda.Tools
- Amazon.Lambda.Templates
- Visual Studio  / VS Code

---

# Restore Dependencies

```bash
dotnet restore
```

---

# Build Solution

```bash
dotnet build
```

---

# Run Tests

```bash
dotnet test
```

---

# Final Notes

This structure is intentionally designed to remain:
- Simple
- Scalable
- Maintainable
- Enterprise-friendly

As the platform evolves, the repository structure may continue to grow and improve. Contributions, discussions, and feedback are always welcome.
