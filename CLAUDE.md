# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is the `core-backend` monorepo — a centralized .NET solution for backend application logic, AWS Lambda functions, and shared libraries for the TPS platform. **This repository contains application and business logic only.** Infrastructure, CI/CD pipelines, Step Functions orchestration, Terraform, and environment provisioning are managed in a separate DevOps repository.

## Target Repository Structure

```text
tps-platform/
├── applications/         # Full backend APIs and services (customer, admin, internal)
├── serverless/
│   ├── lambdas/          # One directory per Lambda function (kebab-case names)
│   └── shared/           # Focused shared libraries (contracts, logging, core, etc.)
├── docs/
├── Directory.Build.props
├── Directory.Packages.props
└── README.md
```

> The current layout (`src/Lambdas/`, `src/Shared/`) is transitional and being migrated to the structure above.

## Commands

```bash
# Restore, build, test
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release --verbosity normal

# Single project
dotnet build "serverless/lambdas/<lambda-name>/<LambdaName>.csproj" --configuration Release
dotnet test "tests/<TestProject>/<TestProject>.csproj" --configuration Release --verbosity normal

# Package a Lambda (requires Amazon.Lambda.Tools)
dotnet lambda package --project-location "serverless/lambdas/<lambda-name>" --configuration Release --framework net10.0 --output-package artifact.zip
```

**Requirements:** .NET 10 SDK, `Amazon.Lambda.Tools`, `Amazon.Lambda.Templates`.

## Architecture Rules

**Lambda functions**
- Single responsibility, independently deployable, minimal dependencies.
- Handlers must be thin — delegate all business logic to services.
- No coupling between Lambdas.
- Named in kebab-case: `process-payment`, `validate-order`, `send-notification`.

**Shared libraries** (`serverless/shared/`)
- Keep libraries small and focused — avoid a large generic "god" library.
- Prefer purpose-scoped libs: `shared-contracts`, `shared-logging`, `shared-core`.
- Shared contracts and DTOs live here, not inside individual Lambda projects.

**Applications** (`applications/`)
- Each application owns its responsibility boundary and remains isolated from others.

## Project Configuration (.csproj)

Lambda projects require these properties for the AWS packaging tool to work correctly:

```xml
<AWSProjectType>Lambda</AWSProjectType>
<GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
<CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
<PublishReadyToRun>true</PublishReadyToRun>  <!-- reduces cold start -->
```

`Directory.Build.props` and `Directory.Packages.props` at the repo root centralize SDK versions and package versions across all projects.

## What Is NOT in This Repository

- Terraform / infrastructure provisioning
- CI/CD pipeline definitions
- AWS Step Functions orchestration
- Deployment scripts
- Environment and monitoring configuration

All of the above live in the separate DevOps repository.
