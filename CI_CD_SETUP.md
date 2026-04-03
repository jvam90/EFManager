# CI/CD Setup Guide - EFManager

## 🎯 Objetivo
Configurar o projeto para CI/CD completo com GitHub Actions, testes automatizados e proteção de branches.

## 📋 Checklist de Configuração

### Fase 1: Preparação Initial

- [x] Código em repositório Git
- [x] Testes implementados (92% cobertura)
- [x] Workflows criados em `.github/workflows/`
- [x] Documentação atualizada

### Fase 2: Configuração do Repositório

#### 2.1 Ativar GitHub Actions
```
1. Acesse: Settings > Actions > General
2. Certifique-se: "Allow all actions and reusable workflows" está marcado
3. Salve as alterações
```

#### 2.2 Configurar Secrets (Opcional)

**Para Codecov:**
```
1. Acesse: https://codecov.io/gh
2. Faça login com GitHub
3. Adicione seu repositório
4. Copie o token CODECOV_TOKEN
5. Acesse: Settings > Secrets and variables > Actions > New repository secret
6. Nome: CODECOV_TOKEN
7. Valor: <cole-o-token>
```

**Para Slack (Opcional):**
```
1. Acesse: https://api.slack.com/messaging/webhooks
2. Clique em "Create New App"
3. Choose "From scratch"
4. Nome: "EFManager CI"
5. Workspace: <selecione-seu-workspace>
6. Ative "Incoming Webhooks"
7. Clique em "Add New Webhook to Workspace"
8. Selecione canal: #deployments (ou outro)
9. Copie a URL do webhook
10. Adicione como secret: SLACK_WEBHOOK_URL
```

#### 2.3 Configurar Branches Protegidos

**Main/Master Branch:**
```
1. Acesse: Settings > Branches
2. Clique em "Add rule"
3. Branch name pattern: main (ou master)
4. Marque as opções:
   ☑ Require a pull request before merging
   ☑ Dismiss stale pull request approvals when new commits are pushed
   ☑ Require status checks to pass before merging
   ☑ Require code review approvals (1+)
   ☑ Include administrators
5. Status checks obrigatórios:
   - build-and-test
   - code-analysis
   - coverage
   - security-scan
```

**Develop Branch:**
```
1. Repetir o processo para 'develop'
2. Requerimentos um pouco menos restritivos
3. Status checks: todos menos 'security-scan' (opcional)
```

---

## 🚀 Workflows em Ação

### CI Workflow (Executa em Push/PR)

```
Trigger: Push em main/master/develop ou PR
         ↓
┌─────────────────────────────────────────┐
│ Build and Test (Multi-OS)               │
│ • Windows, Linux, macOS                 │
│ • Build + 48 Tests                      │
│ • Gera: test-results-*.trx              │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Code Analysis                           │
│ • Style checks                          │
│ • Roslyn analyzers                      │
│ • Warnings and errors                   │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Code Coverage                           │
│ • XPlat Coverage Collection             │
│ • 92% Coverage Target                   │
│ • Upload to Codecov (optional)          │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Security Scan                           │
│ • Vulnerable packages                   │
│ • Dependency analysis                   │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Test Report                             │
│ • Publicar resultados dos testes        │
│ • Comparar com commit anterior          │
└────────────┬────────────────────────────┘
             ↓
         ✅ SUCESSO
    ou
    ❌ FALHA → Bloqueia merge
```

### Test Suite Workflow (Schedule + Manual)

```
Trigger: Diariamente 2 AM UTC / Manual
         ↓
┌─────────────────────────────────────────┐
│ Unit Tests (38 testes)                  │
│ • xUnit runner                          │
│ • Detailed logging                      │
│ • TRX report                            │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Integration Tests (5 testes)            │
│ • End-to-end scenarios                  │
│ • Category filtering                    │
│ • TRX report                            │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Coverage Analysis                       │
│ • XPlat Code Coverage                   │
│ • Cobertura format                      │
│ • Artifact upload                       │
└────────────┬────────────────────────────┘
             ↓
         📊 SUMMARY
    • Total: 48 tests
    • Coverage: 92%
    • Status: ✅
```

### Code Quality Workflow (Schedule + Push/PR)

```
Trigger: Push / PR / Weekly Sunday 3 AM
         ↓
┌─────────────────────────────────────────┐
│ Code Quality                            │
│ • Style enforcement                     │
│ • Roslyn analyzers                      │
│ • TreatWarningsAsErrors: false          │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Security Analysis                       │
│ • Vulnerable packages                   │
│ • Dependency scanning                   │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Dependency Check                        │
│ • Outdated packages                     │
│ • dependency.json report                │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Build Warnings Check                    │
│ • Warning count                         │
│ • Error count                           │
│ • Build log analysis                    │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Code Metrics                            │
│ • File count                            │
│ • Lines of code                         │
│ • Coverage stats                        │
└────────────┬────────────────────────────┘
             ↓
         📋 QUALITY REPORT
    ✅ Code Style
    ✅ Security
    ✅ Dependencies
    ✅ Build
    ✅ Metrics
```

### Release Workflow (Manual + Tag Push)

```
Trigger: git tag v* / Manual input
         ↓
┌─────────────────────────────────────────┐
│ Create Release                          │
│ • Checkout with full history            │
│ • Build verification                    │
│ • Run all 48 tests                      │
│ • Extract version                       │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ GitHub Release                          │
│ • Create release on GitHub              │
│ • Add release notes                     │
│ • Include coverage info                 │
│ • Mark as latest (if not pre-release)   │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│ Notify Slack (Opcional)                 │
│ • Send notification                     │
│ • Include version & status              │
│ • Link para release                     │
└────────────┬────────────────────────────┘
             ↓
         🎉 RELEASE PUBLISHED
    • Version: vX.Y.Z
    • Tests: ✅ 48/48
    • Coverage: 92%
```

---

## 📝 Exemplo: Primeiro Merge via CI

### 1. Criar Branch Feature
```bash
git checkout -b feature/nova-funcionalidade
# ... fazer alterações ...
git add .
git commit -m "Adiciona nova funcionalidade"
git push origin feature/nova-funcionalidade
```

### 2. Criar Pull Request
```
1. GitHub > "Create Pull Request"
2. Base: main | Compare: feature/nova-funcionalidade
3. Descrever mudanças
4. Enviar PR
```

### 3. Aguardar CI
```
GitHub Actions iniciará automaticamente:
✓ Checkout code
✓ Build (Windows, Linux, macOS)
✓ Run 48 tests
✓ Code analysis
✓ Coverage check
✓ Security scan
✓ Create report

Tempo total: ~5-10 minutos
```

### 4. Resultado
```
Se tudo passar ✅:
- Botão "Merge" habilitado
- PR pode ser merged

Se falhar ❌:
- Detalhes do erro mostrados
- Botão "Merge" desabilitado
- Correções necessárias
```

### 5. Mergear
```bash
# Via GitHub UI ou CLI
gh pr merge
```

### 6. Release (Opcional)
```bash
# Tag para release
git tag v1.0.0
git push origin v1.0.0

# Workflow Release executa automaticamente
# Cria GitHub Release com notas
```

---

## 🔍 Monitorando CI/CD

### Dashboard do GitHub
```
Repositório > Actions
├─ All workflows
├─ CI - Build and Test
├─ Test Suite
├─ Code Quality
└─ Release
```

### Notificações
```
Email (padrão do GitHub):
- Falha de workflow
- Comentários em PR
- Menções

Slack (se configurado):
- Releases publicadas
- Build failures
- Status resumido
```

### Status Badge (README.md)
```markdown
## EFManager

![Build Status](https://github.com/YOUR-USER/EFManager/workflows/CI%20-%20Build%20and%20Test/badge.svg)
![Tests](https://github.com/YOUR-USER/EFManager/workflows/Test%20Suite/badge.svg)
![Code Quality](https://github.com/YOUR-USER/EFManager/workflows/Code%20Quality/badge.svg)

> Aplicação com 92% de cobertura de testes e CI/CD completo
```

---

## ⚙️ Ajustes Recomendados

### Por Ambiente

**Development**
- Testes: ✅ Completos
- Quality: ⚠️ Warnings permitidos
- Security: ⚠️ Less strict

**Staging**
- Testes: ✅ Completos
- Quality: ✅ Strict
- Security: ✅ Completo

**Production**
- Testes: ✅ Completos + Manual
- Quality: ✅ Muito strict
- Security: ✅ Muito rigoroso
- Manual Approval: ✅ Requerido

### Por Tipo de Mudança

**Bugfix**
```yaml
- [ ] Testes adicionados
- [ ] Nenhuma quebra de API
- [ ] Documentação atualizada
- [ ] Hotfix? → merge para production branch
```

**Feature**
```yaml
- [ ] Testes (cobertura >80%)
- [ ] Documentação completa
- [ ] Changelog atualizado
- [ ] Reviewed by 2+ pessoas
- [ ] Merge para develop primeiro
```

**Hotfix Production**
```yaml
- [ ] Apenas bug fixes
- [ ] Testes obrigatórios
- [ ] Immediate deployment
- [ ] Merge para main + develop
- [ ] Tag release imediatamente
```

---

## 🆘 Troubleshooting

### Workflow Não Inicia
**Causa**: Arquivo YAML inválido
```bash
# Solução: Validar YAML
yamllint .github/workflows/*.yml

# Ou verificar sintaxe manualmente
# - Indentation (2 spaces)
# - Colons after keys
# - Hyphens for lists
```

### Build Falha Apenas em CI
**Causa**: Diferença de ambiente
```bash
# Solução: Testar localmente como CI
dotnet clean
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release
```

### Testes Passam Localmente, Falham em CI
**Causa**: Arquivos temporários, cache, path absolutos
```bash
# Solução:
rm -rf bin obj  # Windows: rmdir bin obj /s
dotnet clean
dotnet test --configuration Release
```

### Status Checks Desativados
```
Settings > Branches > Branch protection rules
Verificar: "Require status checks to pass"
Selecionar todos os checks necessários
```

---

## 📚 Próximos Passos

1. **Integração com Slack** (notificações em tempo real)
2. **Codecov Integration** (rastreamento de cobertura)
3. **SonarQube** (análise de código estática)
4. **Deployment Automático** (CD para staging/production)
5. **Performance Monitoring** (relatórios de performance)

---

**Data**: 2026-04-01
**Status**: ✅ Pronto para Setup
**Próxima Revisão**: Após primeiro merge bem-sucedido
