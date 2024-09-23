# Valid.Test
Teste técnico Valid

Os dados de entrada para publicação foram feitos via Api.

O banco de dados foi feito em SQL Server e contém as configurações de acesso dentro do AppSettings. Ele esta sendo criado via FluentMigrator, não sendo necessário script para criação das tabelas.

As configurações do RabbitMQ estão dentro do AppSettings.

Na raíz do projeto existem os arquivos: 
- 'script.sh' - para testes em massa, que faz o cURL com 10 mocks de dados, ele irá solicitar o caminho do host e de uma imagem local para que possa ser feito o envio;
- 'curl_mocks_protocolos.txt' - contém os cURLs caso deseja importar via Postman (nacessário adicionar o token);
- Testes.postman_collection.json - collection do postman para auxiliar nas chamadas;

## Gerar token
```
curl --location 'https://localhost:7076/auth' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Email": "admin@admin",
    "Password": "123456"
}'
```
