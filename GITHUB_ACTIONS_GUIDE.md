# GitHub Actions Workflows - EFManager

## 📋 Visão Geral

Este projeto usa **4 workflows principais** do GitHub Actions para automatizar CI/CD, testes, qualidade de código e releases.

## 🔄 Workflows Disponíveis

### 1. **CI - Build and Test** (`ci.yml`)
Principal workflow de integração contínua

#### ⏱️ Quando Executa
- Em push para branches: `main`, `master`, `develop`
- Em pull requests para os mesmos branches
- Manualmente via `workflow_dispatch`

#### 📊 O que Faz
```
┌─────────────────────────────────────┐
│    Build and Test (Multi-OS)        │
│  ✓ Linux, Windows, macOS            │
└──────────┬──────────────────────────┘
           ├─ Checkout Code
           ├─ Setup .NET 10.0
           ├─ Restore Dependencies
           ├─ Build Project
           ├─ Run 48 Tests
           └─ Upload Results

┌─────────────────────────────────────┐
│      Code Analysis                  │
│  ✓ Code Style & Roslyn Analyzers    │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│      Code Coverage                  │
│  ✓ 92% Coverage Report              │
│  ✓ Codecov Integration              │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│      Security Scan                  │
│  ✓ Vulnerable Package Detection     │
└─────────────────────────────────────┘
```

#### 📈 Outputs
- ✅ Build Status
- ✅ Test Results (48 testes)
- ✅ Coverage Report (92%)
- ✅ Security Warnings
- ✅ Code Analysis Issues

#### ⚙️ Configuração
```yaml
# Branches monitorados
branches: [ main, master, develop ]

# Alterações que ativam
paths:
  - '**.cs'
  - '**.csproj'
  - '.github/workflows/ci.yml'

# Ambientes
matrix:
  os: [ubuntu-latest, windows-latest, macos-latest]
```

---

### 2. **Test Suite** (`tests.yml`)
Execução detalhada de testes com relatórios

#### ⏱️ Quando Executa
- Diariamente às 2 AM UTC (agendado)
- Em pull requests
- Manualmente via `workflow_dispatch`

#### 📊 O que Faz
```
┌─────────────────────────────────────┐
│    Unit Tests Execution             │
│  ✓ 38 Testes Unitários              │
└──────────┬──────────────────────────┘
           ├─ Detailed Output
           ├─ TRX Report
           └─ Artifact Upload

┌─────────────────────────────────────┐
│    Integration Tests                │
│  ✓ 5 Testes de Integração           │
└──────────┬──────────────────────────┘
           ├─ Category Filter
           ├─ TRX Report
           └─ Artifact Upload

┌─────────────────────────────────────┐
│    Coverage Analysis                │
│  ✓ 92% Code Coverage Report         │
└──────────┬──────────────────────────┘
           ├─ XPlat Coverage
           ├─ Cobertura Format
           └─ Artifact Upload

┌─────────────────────────────────────┐
│    Test Summary                     │
│  ✓ Relatório Final                  │
└─────────────────────────────────────┘
```

#### 📈 Outputs
- ✅ Unit Test Results
- ✅ Integration Test Results
- ✅ Coverage Report
- ✅ Test Summary in Workflow Summary

#### ⚙️ Schedule
```yaml
schedule:
  - cron: '0 2 * * *'  # Daily at 2 AM UTC
```

---

### 3. **Code Quality** (`code-quality.yml`)
Análise de qualidade de código, segurança e dependências

#### ⏱️ Quando Executa
- Em push para branches: `main`, `master`, `develop`
- Em pull requests
- Semanalmente aos domingos 3 AM UTC

#### 📊 O que Faz
```
┌─────────────────────────────────────┐
│    Code Quality Check               │
│  ✓ Style Enforcement                │
│  ✓ Roslyn Analyzers                 │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│    Security Analysis                │
│  ✓ Vulnerable Package Detection     │
│  ✓ Dependency Scanning              │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│    Dependency Check                 │
│  ✓ Outdated Packages                │
│  ✓ Dependency Report                │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│    Build Warnings Check             │
│  ✓ Warning Count                    │
│  ✓ Error Count                      │
│  ✓ Build Log                        │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│    Code Metrics                     │
│  ✓ File Count                       │
│  ✓ Lines of Code                    │
│  ✓ Coverage Stats                   │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│    Quality Summary                  │
│  ✓ Overall Report                   │
└─────────────────────────────────────┘
```

#### 📈 Outputs
- ✅ Code Style Report
- ✅ Security Issues
- ✅ Outdated Packages
- ✅ Build Log Analysis
- ✅ Code Metrics
- ✅ Quality Summary

---

### 4. **Release** (`release.yml`)
Automação de releases com tags Git

#### ⏱️ Quando Executa
- Ao fazer push de tag: `v*` (ex: `v1.0.0`)
- Manualmente via `workflow_dispatch`

#### 📊 O que Faz
```
┌─────────────────────────────────────┐
│    Create Release                   │
│  ✓ Build Verification               │
│  ✓ Test Execution                   │
│  ✓ GitHub Release Creation          │
│  ✓ Release Notes Generation         │
└──────────┬──────────────────────────┘
           └─ Slack Notification (Opcional)
```

#### 📈 Outputs
- ✅ GitHub Release Created
- ✅ Release Notes with Coverage Info
- ✅ Slack Notification (if configured)

#### 🔖 Como Usar
```bash
# Criar uma release
git tag v1.0.0
git push origin v1.0.0

# Ou manualmente via GitHub Actions UI
# Workflow > Release > Run workflow > Informar versão
```

---

## 🚀 Configuração Inicial

### 1. Ativar GitHub Actions
```bash
# Já está ativado por padrão se os workflows estão em .github/workflows/
# Verifique em: Settings > Actions > General > Allow all actions and reusable workflows
```

### 2. Configurar Secrets (Opcional)

Para enviar relatórios a serviços externos:

#### Codecov
```bash
# Adicionar em Settings > Secrets and variables > Actions
CODECOV_TOKEN=<seu-token-codecov>
```

#### Slack Notifications
```bash
# Adicionar em Settings > Secrets and variables > Actions
SLACK_WEBHOOK_URL=<sua-url-webhook-slack>
```

### 3. Branches Protegidos (Recomendado)

Em `Settings > Branches > Branch protection rules`:

```yaml
Require:
  ✓ A pull request before merging
  ✓ Approvals (1+)
  ✓ Status checks to pass
    - Build and Test (ci.yml)
    - Test Suite (tests.yml)
    - Code Quality (code-quality.yml)
  ✓ Automatic dismissal of stale reviews
```

---

## 📊 Monitorando os Workflows

### Via GitHub UI
1. Acesse: **Actions** tab no repositório
2. Clique no workflow desejado
3. Veja status em tempo real

### Via Status Badges
Adicione ao seu README.md:

```markdown
# EFManager

![CI - Build and Test](https://github.com/your-user/EFManager/workflows/CI%20-%20Build%20and%20Test/badge.svg)
![Test Suite](https://github.com/your-user/EFManager/workflows/Test%20Suite/badge.svg)
![Code Quality](https://github.com/your-user/EFManager/workflows/Code%20Quality/badge.svg)
```

### Via Webhooks
Configure para receber notificações de eventos do GitHub em seu chat/aplicação.

---

## 🔧 Personalizações Comuns

### Alterar Versão .NET
```yaml
env:
  DOTNET_VERSION: '10.0.x'  # Alterar aqui
```

### Adicionar Branches
```yaml
on:
  push:
    branches: [ main, master, develop, production ]
```

### Mudar Schedule dos Testes
```yaml
schedule:
  - cron: '0 2 * * *'  # Min Hour Day Month DayOfWeek
  # Exemplos:
  # - cron: '*/30 * * * *'  # A cada 30 minutos
  # - cron: '0 9 * * 1-5'   # Weekdays at 9 AM
  # - cron: '0 0 1 * *'     # 1º do mês
```

### Ignorar Alterações Específicas
```yaml
paths-ignore:
  - 'README.md'
  - 'CHANGELOG.md'
  - '.gitignore'
```

---

## 📈 Integração com Serviços Externos

### Codecov (Cobertura)
```yaml
- name: Upload coverage to Codecov
  uses: codecov/codecov-action@v4
  with:
    token: ${{ secrets.CODECOV_TOKEN }}
```

### Slack (Notificações)
```yaml
- name: Notify Slack
  uses: 8398a7/action-slack@v3
  with:
    webhook_url: ${{ secrets.SLACK_WEBHOOK_URL }}
```

### SonarQube (Análise Estática)
```yaml
- name: SonarQube Scan
  uses: SonarSource/sonarqube-scan-action@master
  with:
    args: >
      -Dsonar.projectKey=EFManager
```

---

## ✅ Checklist para Setup Completo

- [ ] Workflows criados em `.github/workflows/`
- [ ] Branches protegidos configurados
- [ ] Status checks requeridos definidos
- [ ] Secrets configurados (se usando Codecov/Slack)
- [ ] README atualizado com badges
- [ ] Testes passando em CI
- [ ] Cobertura > 80%
- [ ] Sem warnings de build

---

## 🔗 Recursos Úteis

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [GitHub Actions Marketplace](https://github.com/marketplace?type=actions)
- [xUnit Documentation](https://xunit.net/)
- [Codecov Documentation](https://codecov.io/docs)
- [Cron Expression Generator](https://crontab.guru/)

---

## 📞 Troubleshooting

### Workflow não executa
**Causa**: Arquivo YAML com sintaxe inválida
```bash
# Validar YAML
- Use linter online: yamllint.com
- Verifique indentation
```

### Testes falham em CI mas passam localmente
**Causa**: Diferença de ambiente
```bash
# Testar localmente com mesmo setup
dotnet test --configuration Release
```

### Token expirado para Codecov
**Causa**: Secret token vencido ou inválido
```bash
# Atualizar token em: Settings > Secrets and variables > Actions
```

---

**Versão**: 1.0
**Última Atualização**: 2026-04-01
**Status**: ✅ Pronto para Uso
