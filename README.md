# Valid.Test
Teste técnico Valid

Os dados de entrada para publicação foram feitos via Api.

O banco de dados foi feito em SQL Server e contém as configurações de acesso dentro do AppSettings. Ele esta sendo criado via FluentMigrator, não sendo necessário script para criação das tabelas.

As configurações do RabbitMQ estão dentro do AppSettings.

Para testes em massa, existe um script na raíz do projeto chamado 'script.sh' (em bash) que faz o cURL com 10 mocks de dados, ele irá solicitar o caminho do host e de uma imagem local para que possa ser feito o envio.
Também dentro da raíz do projeto tem um arquivo chamado 'curl_mocks_protocolos.txt', onde contém os cURLs caso deseja importar via Postman.
