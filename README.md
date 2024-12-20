# WebCrawler

Este projeto é um Web Crawler desenvolvido em .NET 8 para extrair informações de proxies disponíveis no site [ProxyServers.pro](https://proxyservers.pro). Ele utiliza a biblioteca `HtmlAgilityPack` para realizar o parsing de páginas HTML, além de `Newtonsoft.Json` para manipulação de dados no formato JSON.

## Funcionalidades

- Raspagem de páginas HTML para extrair informações de proxies, como:
  - Endereço IP
  - Porta
  - País
  - Protocolo
- Armazenamento de logs de execução em um banco de dados SQLite.
- Geração de arquivos JSON com os proxies extraídos.
- Salvamento das páginas HTML processadas.

## Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** .NET 8
- **Bibliotecas:**
  - [HtmlAgilityPack](https://html-agility-pack.net/) para parsing de HTML.
  - [Newtonsoft.Json](https://www.newtonsoft.com/json) para manipulação de JSON.
  - SQLite para armazenamento de logs de execução.

## Estrutura do Projeto

- `Models/` - Contém as classes de modelo, como `ProxyInfo` e `ExecutionLog`.
- `Services/` - Implementações de serviços para raspagem de dados (`ProxyScraper`) e manipulação de banco de dados (`DatabaseLogger`).
- `Program.cs` - Ponto de entrada principal do projeto.
- `Result.json` - Arquivo gerado com os proxies extraídos (ignorado no controle de versão).
- `executionLogs.db` - Banco de dados SQLite para armazenar logs de execução (ignorado no controle de versão).

## Pré-requisitos

Certifique-se de ter os seguintes itens instalados:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQLite (opcional, apenas para acessar os logs gerados)

## Configuração

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-usuario/webcrawler.git
   cd webcrawler
   
2. Restaure os pacotes do projeto:
dotnet restore

3. Compile o projeto:
dotnet build

4. Execute o programa:
dotnet run

Como Funciona
O crawler começa na URL base configurada no código e percorre as páginas de proxies.
Para cada página, ele:
Extrai os dados de cada proxy válido.
Salva o HTML da página para análise posterior.
Ao final da execução:
Os proxies extraídos são salvos em um arquivo JSON.
Um log de execução é salvo no banco de dados SQLite.

Exemplo de Uso

A saída do programa no console exibe informações como:

Proxies encontrados
Erros durante o processamento
Resumo da execução
Os arquivos gerados (Result.json, páginas HTML) são salvos na pasta de trabalho.

Contribuindo

Contribuições são bem-vindas! Siga os passos abaixo:

Faça um fork do projeto.
Crie uma nova branch com sua feature: git checkout -b minha-feature.
Faça commit das alterações: git commit -m 'Adicionei minha feature'.
Envie para o repositório remoto: git push origin minha-feature.
Abra um Pull Request.

Licença
Este projeto é licenciado sob a MIT License.

Autor: Hélio Galdino
Contato: heliogad@hotmail.com
