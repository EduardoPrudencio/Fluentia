# Projeto de Síntese de Fala (Speech Synthesis)

Este projeto demonstra como carregar configurações a partir de um arquivo JSON, ler e processar linhas de um arquivo de texto e, em seguida, sintetizar e salvar áudios em formato WAV usando a biblioteca **System.Speech** do .NET.

## Sumário
1. [Visão Geral](#visão-geral)  
2. [Requisitos](#requisitos)  
3. [Configuração](#configuração)  
4. [Como Executar](#como-executar)  
5. [Estrutura do Projeto](#estrutura-do-projeto)  
6. [Exemplos](#exemplos)  
7. [Créditos](#créditos)

---

## Visão Geral

A aplicação realiza os seguintes passos:

1. **Carrega o arquivo de configuração** (`config.json`) usando `Microsoft.Extensions.Configuration`.  
2. **Lê um arquivo de texto** cujo caminho e nome são definidos no `config.json`.  
3. **Filtra as linhas vazias** e seleciona somente as linhas ímpares (1ª, 3ª, 5ª etc.).  
4. **Remove prefixos e caracteres específicos** (por exemplo, `"Gerente: "`, `"Programador: "`, `"?"`, etc.).  
5. **Concatena todo o texto limpo** em uma única string e gera um arquivo de áudio completo (`FullText.wav`).  
6. **Gera arquivos de áudio separados** para cada linha, usando a mesma voz selecionada.

---

## Requisitos

- **.NET 6.0** (ou superior) instalado.  
- **Pacotes NuGet** (já listados no `.csproj`, mas os principais são):  
  - [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration)  
  - [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json)  
  - [Microsoft.Extensions.Configuration.Binder](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Binder)  
- **Windows** (a biblioteca `System.Speech` é compatível apenas com Windows).  

> **Observação**: Se você estiver usando um **template** de Console Application mais recente, talvez seja preciso instalar os pacotes de configuração manualmente:
> ```bash
> dotnet add package Microsoft.Extensions.Configuration
> dotnet add package Microsoft.Extensions.Configuration.Json
> dotnet add package Microsoft.Extensions.Configuration.Binder
> ```

---

## Configuração

O arquivo `config.json` deve estar na pasta raiz do projeto ou no mesmo local de execução da aplicação. Ele é usado para definir:

- **Path**: O diretório onde o arquivo de texto e os áudios serão lidos/salvos.  
- **File**: O nome do arquivo de texto a ser lido.

Exemplo de `config.json`:

```json
{
  "path": "C:\\Users\\Fulano\\Documentos\\Textos\\",
  "file": "FullText.txt"
}
```

## Como Executar

1. **Clone** este repositório ou faça o download do código-fonte.  
2. Verifique se você tem instalado o **.NET 6.0** ou superior (use `dotnet --version` para checar).  
3. Na pasta do projeto, restaure dependências (caso necessário) com:
   ```bash
   dotnet restore

Compile e execute o projeto:
bash
Copiar
dotnet run
A aplicação lerá o config.json, encontrará o arquivo definido em "path" + "file", processará as linhas e gerará áudios.

Program.cs: Contém o Main e orquestra o carregamento das configurações, a leitura do arquivo e a geração dos áudios.
ProgramConfig.cs: Classe que representa as propriedades lidas do config.json.
config.json: Arquivo de configuração com o caminho e o arquivo a ser processado.

Exemplos
1. Exemplo de arquivo de texto

> ```
> Gerente: Olá, bom dia!
> Programador: Bom dia!
> Gerente: Como está o progresso do projeto?
> Programador: Ainda estou analisando...
> ```
>
> Após o processamento, o programa pode gerar:

FullText.wav com todo o texto concatenado (sem prefixos).
Arquivos individuais como Olá, bom dia.wav, Bom dia.wav, etc.
2. Ajuste de voz
No Program.cs, há um trecho para selecionar a voz:

> ``` bash
> synthesizer.SelectVoice("Microsoft Zira Desktop");
> ```

Se quiser usar outra voz instalada no seu Windows, basta alterar para o nome correspondente.

## Créditos
System.Speech: Biblioteca utilizada para síntese de fala no Windows.
Microsoft.Extensions.Configuration: Infraestrutura para ler e gerenciar configurações de forma flexível.
Qualquer dúvida ou sugestão, fique à vontade para abrir uma issue ou enviar um pull request.

Bom desenvolvimento!

