# CursoDDD
Curso de DDD com as essenciais coisas que se deve saber antes de começar um projeto.

### Instalação

Siga os passos abaixo para funcionamento do projeto.

###### API Faker

Para simulação de um ERP de base de contratos é utilizado json-server. Para que se faça a instalação antes é necessária a instalação do NodeJs e NPM e após executar o comando abaixo em seu terminal:

``npm install -g json-server``


###### Banco de dados SQL Server

A aplicação utiliza banco de dados SQL Server e para maior facilidade é utlizado Docker. Docker funciona tanto para Windows, Linux ou Mac.

Após instalação do Docker, podemos executar o container do SQL Server com o comando abaixo:

``docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=p4ssw0rd*" -e "MSSQL_PID=Express" -p 1433:1433 --name sqlserver  -d microsoft/mssql-server-linux:latest``

Uma vez que o comando acima foi executado. Nas próximas vezes qu for utilizar o docker basta informar o comando abaixo:

``docker start sqlserver``

###### .Net Core

A aplicação está utilizando .Net Core com versão minima 2.2.
