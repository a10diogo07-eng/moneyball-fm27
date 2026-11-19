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

## 2. Exportador F8 dentro do Football Manager 27

O componente **FM27Exporter** é o plugin responsável por exportar a tabela que está aberta no jogo. Ele já vem configurado para a tecla **F8** e gera HTML, RTF e CSV.

### Instalação do exportador F8

1. Feche o Football Manager 27.
2. Copie a pasta `plugin/FM27Exporter/` para:

   `C:\Program Files (x86)\Steam\steamapps\common\Football Manager 27\BepInEx\plugins\FM27Exporter\`

3. Copie `plugin/local.fm27exporter.cfg` para:

   `C:\Program Files (x86)\Steam\steamapps\common\Football Manager 27\BepInEx\config\local.fm27exporter.cfg`

4. Abra o jogo e carregue a visão Moneyball.

### Exportar com F8

1. Abra a tabela ou visão com os jogadores.
2. Se quiser exportar somente alguns jogadores, selecione pelo menos dois registros.
3. Pressione **F8**.
4. Aguarde a mensagem de conclusão no topo da tela.
5. Os arquivos aparecerão em:

   `C:\Users\Pichau\Documents\Sports Interactive\Football Manager 27\exports`

Por padrão, o exportador cria os formatos HTML, RTF e CSV com data e hora no nome. Depois, no Moneyball, clique em **Importar arquivo** e escolha o CSV, HTML ou RTF recém-gerado.

Atalhos adicionais configurados: **Shift+F8** gera diagnóstico da interface e **Ctrl+F8** adiciona colunas de atributos à visão atual.

## 3. Plugin do painel Moneyball dentro do Football Manager 27

O plugin fica na pasta `plugin/` deste repositório e é compatível com a instalação BepInEx usada pelo FM27.

### Instalação

1. Feche o Football Manager 27.
2. Copie `plugin/MoneyballFM27.dll` para:

   `C:\Program Files (x86)\Steam\steamapps\common\Football Manager 27\BepInEx\plugins\MoneyballFM27\`

3. Copie `plugin/index.html` para a mesma pasta.
4. Abra o jogo novamente.

### Uso no jogo

Pressione **Ctrl + Shift + M** para abrir o painel Moneyball. O plugin procura o CSV mais recente exportado pelo Football Manager nas pastas de exportação do usuário e permite abrir o painel local. Esse painel é opcional; o exportador principal usa **F8**.

## 4. Executar a partir do código-fonte

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

## 5. Arquivos do projeto

- `index.html`: painel web completo.
- `main.js`: janela Electron do aplicativo desktop.
- `package.json`: configuração e comandos de build.
- `plugin/MoneyballFM27.dll`: plugin compilado para o FM27.
- `plugin/MoneyballPlugin.cs`: código-fonte do plugin.
- `plugin/index.html`: painel usado pelo plugin.
- `plugin/FM27Exporter/FM27Exporter.dll`: exportador F8 compilado.
- `plugin/local.fm27exporter.cfg`: configuração dos atalhos, formatos e pasta de saída.

## 6. Limitação do GitHub

O instalador Windows tem mais de 100 MB e não pode ser enviado como arquivo comum ao GitHub. O código e o plugin estão publicados no repositório; o instalador gerado localmente permanece em `release/`.
