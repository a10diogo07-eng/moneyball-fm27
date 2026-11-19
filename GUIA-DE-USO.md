# Guia de uso — Moneyball FM27

## 1. Aplicativo desktop

O aplicativo Windows é a versão instalável do painel. Ele permite importar planilhas do Football Manager nos formatos CSV, HTML e RTF, combinar filtros por qualquer coluna e exportar a seleção em CSV, HTML ou RTF.

### Abrir

Execute o instalador local `release/Moneyball-FM27-1.0.0-x64.exe`. Depois da instalação, abra o atalho “Moneyball FM27” no menu Iniciar ou na área de trabalho.

### Fluxo recomendado

1. No Football Manager, exporte a visão/planilha com os atributos desejados.
2. No Moneyball, clique em **Importar arquivo** e selecione o CSV, HTML ou RTF.
3. Use a busca, posição, grupo, minutos e os filtros por coluna.
4. Clique em **Novo jogador** para cadastrar dados manualmente quando necessário.
5. Exporte a seleção em **HTML**, **RTF** ou **CSV**.

Os dados ficam salvos localmente no computador. O botão “Carregar exemplo” adiciona jogadores fictícios para testar o painel.

## 2. Plugin dentro do Football Manager 27

O plugin fica na pasta `plugin/` deste repositório e é compatível com a instalação BepInEx usada pelo FM27.

### Instalação

1. Feche o Football Manager 27.
2. Copie `plugin/MoneyballFM27.dll` para:

   `C:\Program Files (x86)\Steam\steamapps\common\Football Manager 27\BepInEx\plugins\MoneyballFM27\`

3. Copie `plugin/index.html` para a mesma pasta.
4. Abra o jogo novamente.

### Uso no jogo

Pressione **Ctrl + Shift + M** para abrir o painel Moneyball. O plugin procura o CSV mais recente exportado pelo Football Manager nas pastas de exportação do usuário e permite abrir o painel local.

## 3. Executar a partir do código-fonte

Na pasta do projeto:

```powershell
npm install
npm start
```

Para gerar novamente os instaladores Windows:

```powershell
npm run dist
```

O resultado é criado na pasta `release/`.

## 4. Arquivos do projeto

- `index.html`: painel web completo.
- `main.js`: janela Electron do aplicativo desktop.
- `package.json`: configuração e comandos de build.
- `plugin/MoneyballFM27.dll`: plugin compilado para o FM27.
- `plugin/MoneyballPlugin.cs`: código-fonte do plugin.
- `plugin/index.html`: painel usado pelo plugin.

## 5. Limitação do GitHub

O instalador Windows tem mais de 100 MB e não pode ser enviado como arquivo comum ao GitHub. O código e o plugin estão publicados no repositório; o instalador gerado localmente permanece em `release/`.
