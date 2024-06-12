Documentação Api De autenticação Fisioterapia
Authentication API

Esta API é usada para gerenciar usuários, pacientes, fisioterapeutas, coordenadores e administradores.

![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/dffeb9f1-00f1-4d28-8004-56ab0a1b8275)


Endpoints Disponíveis:

/Usuario

GET: Lista todos os usuários.
POST: Cria um novo usuário.
PUT: Atualiza um usuário existente.
DELETE: Deleta um usuário pelo ID.
/Paciente

GET: Lista todos os pacientes.
POST: Cria um novo paciente.
PUT: Atualiza um paciente existente.
DELETE: Deleta um paciente pelo ID.
/Fisioterapeuta

GET: Lista todos os fisioterapeutas.
POST: Cria um novo fisioterapeuta.
PUT: Atualiza um fisioterapeuta existente.
DELETE: Deleta um fisioterapeuta pelo ID.
/Coordenador

GET: Lista todos os coordenadores.
POST: Cria um novo coordenador.
PUT: Atualiza um coordenador existente.
DELETE: Deleta um coordenador pelo ID.
/Admin

GET: Lista todos os administradores.
POST: Cria um novo administrador.
PUT: Atualiza um administrador existente.
DELETE: Deleta um administrador pelo ID.
/Auth/login

POST: Realiza o login de um usuário.
Autorização:

Alguns endpoints requerem autorização com base em políticas específicas, como AdminPolicy, FisioterapeutaPolicy, PacientePolicy e AdminOrCoordenadorPolicy.
Respostas de Erro:

400: Dados inválidos fornecidos na solicitação.
404: Recurso não encontrado.
500: Erro interno do servidor.
Formato dos Dados:

Os dados são enviados e recebidos no formato JSON.

Formato do JSON de Login:

Para fazer uma solicitação de login para a rota /Auth/login, o corpo da solicitação HTTP deve ser um objeto JSON contendo as seguintes propriedades:

login (string): O e-mail ou nome de usuário do usuário que está tentando fazer login.
password (string): A senha correspondente ao login do usuário.
Exemplo de JSON de Login:

{
  "login": "user@example.com",
  "password": "senha123"
}

Neste exemplo:

O valor da propriedade "login" é "user@example.com", que representa o e-mail do usuário.
O valor da propriedade "password" é "senha123", que representa a senha do usuário.
É importante que os dados fornecidos no JSON de login sejam precisos e correspondam às credenciais válidas de um usuário registrado no sistema.

Resposta esperada:
![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/2991e332-617d-4b7d-8c4d-c331d3b844a6)

Endpoint: Alteração de Senha
Método HTTP: POST
URL: /change-password
Descrição
Este endpoint permite que um usuário autenticado altere sua senha. O usuário deve fornecer a senha atual e a nova senha. O endpoint verifica a validade da senha atual e, se for válida, substitui-a pela nova senha.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token do usuário autenticado (quando aplicável)
Parâmetros
Query Parameter
userId (int): O ID do usuário cuja senha está sendo alterada. Este parametro deve ser enviado no corpo da requisição.
Corpo da Requisição (application/json)

{
  "oldPassword": "SenhaAtual123",
  "newPassword": "NovaSenha456"
}

Controlador: Usuario
Descrição
O UsuarioController gerencia operações CRUD (Criar, Ler, Atualizar e Excluir) para usuários no sistema. Além disso, ele manipula a alteração de senha para usuários autenticados.

Endpoint: Obter Todos os Usuários
Método HTTP: GET
URL: /usuario
Descrição
Obtém uma lista de todos os usuários registrados no sistema. Apenas administradores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminPolicy"
Respostas
200 OK

Corpo: Um array de objetos UsuarioDto.
404 Not Found

Corpo: "Usuarios not found"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Obter Usuário por ID
Método HTTP: GET
URL: /usuario/{id}
Descrição
Obtém os detalhes de um usuário específico com base no ID fornecido. Apenas administradores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminPolicy"
Parâmetros
Path Parameter
id (int): O ID do usuário a ser recuperado.
Respostas
200 OK

Corpo: Um objeto UsuarioDto com os detalhes do usuário.
404 Not Found

Corpo: "Usuario not found"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Criar Novo Usuário
Método HTTP: POST
URL: /usuario
Descrição
Cria um novo usuário no sistema. Este endpoint está disponível para todos, incluindo usuários anônimos.

{
  "idUser": 0,
  "username": "user@example.com",
  "password": "string",
  "tipoUsuario": 1
}

Será criado um usuario com a role corresponente ao tipo do usuario
Resposta esperada:
![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/04c9c188-5863-4932-b5d1-3e30f6a02328)

Respostas
201 Created

Corpo: O objeto UsuarioDto criado.
500 Internal Server Error

Corpo: "Internal server error"

Endpoint: Atualizar Usuário
Método HTTP: PUT
URL: /usuario
Descrição
Atualiza os detalhes de um usuário existente. Apenas administradores podem acessar este endpoint.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token com política "AdminPolicy"
Corpo da Requisição (application/json)
Um objeto JSON representando o usuário a ser atualizado.

json  de exemplo

{
  "idUser": 10,
  "username": "exemplo@example.com",
  "password": "string",
  "tipoUsuario": 1
}

Resposta esperada
![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/31f93193-285c-4db9-b9b9-c06ce8a7d165)

Endpoint: Excluir Usuário
Método HTTP: DELETE
URL: /usuario/{id}
Descrição
Exclui um usuário existente do sistema com base no ID fornecido. Apenas administradores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminPolicy"
Parâmetros
Path Parameter
id (int): O ID do usuário a ser excluído.
Respostas
200 OK

Corpo: O objeto UsuarioDto excluído.
404 Not Found

Corpo: "Usuario not found"
500 Internal Server Error

Corpo: "Internal server error"

Controlador: Paciente
Descrição
O PacienteController gerencia operações CRUD (Criar, Ler, Atualizar e Excluir) para pacientes no sistema. Os endpoints são protegidos por políticas de autorização que garantem acesso apropriado baseado nos papéis dos usuários.

Endpoint: Obter Todos os Pacientes
Método HTTP: GET
URL: /paciente
Descrição
Obtém uma lista de todos os pacientes registrados no sistema. Apenas fisioterapeutas podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "FisioterapeutaPolicy"
Respostas
200 OK

Corpo: Um array de objetos PacienteDto com detalhes dos pacientes.
404 Not Found

Corpo: "Pacientes not found"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Obter Paciente por ID
Método HTTP: GET
URL: /paciente/{id}
Descrição
Obtém os detalhes de um paciente específico com base no ID fornecido. Apenas fisioterapeutas podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "FisioterapeutaPolicy"
Parâmetros
Path Parameter
id (int): O ID do paciente a ser recuperado.
Respostas
200 OK

Corpo: Um objeto PacienteDto com os detalhes do paciente.
404 Not Found

Corpo: "Pacientes not found"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Criar Novo Paciente
Método HTTP: POST
URL: /paciente
Descrição
Cria um novo paciente no sistema. Este endpoint está disponível para todos, incluindo usuários anônimos. Além disso, cria um novo usuário vinculado ao paciente.

Cabeçalhos HTTP
Content-Type: application/json
Corpo da Requisição (application/json)
Um objeto JSON representando o novo paciente a ser criado.

Exemplo de json
{
  "idPaciente": 0,
  "nomePaciente": "string",
  "cpf": "string",
  "uf": "string",
  "endereco": "string",
  "numeroCasa": "string",
  "dataDeNascimento": "2024-06-12T19:02:34.906Z",
  "sexo": "m",
  "proficao": "string",
  "diagnosticoClinico": "string",
  "diagnosticoFisio": "string",
  "emailPaciente": "TestePaciente@example.com",
  "tipoUsuario": 4
}

![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/410100fa-e460-41f6-b9b0-d6e5bd510154)

Endpoint: Atualizar Paciente
Método HTTP: PUT
URL: /paciente
Descrição
Atualiza os detalhes de um paciente existente. Apenas pacientes autenticados podem acessar este endpoint.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token com política "PacientePolicy"
Corpo da Requisição (application/json)
Um objeto JSON representando o paciente a ser atualizado.

![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/2a96c172-06bc-4e53-9a9e-2f074869642e)

Endpoint: Excluir Paciente
Método HTTP: DELETE
URL: /paciente/{id}
Descrição
Exclui um paciente existente do sistema com base no ID fornecido. Apenas administradores ou coordenadores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Parâmetros
Path Parameter
id (int): O ID do paciente a ser excluído.
Respostas
200 OK

Corpo: O objeto PacienteDto excluído.
404 Not Found

Corpo: "Paciente not found"
500 Internal Server Error

Corpo: "Internal server error"

Controlador: Fisioterapeuta
Descrição
O FisioterapeutaController gerencia operações CRUD (Criar, Ler, Atualizar e Excluir) para fisioterapeutas no sistema. Os endpoints são protegidos por políticas de autorização que garantem acesso apropriado baseado nos papéis dos usuários.

Endpoint: Obter Todos os Fisioterapeutas
Método HTTP: GET
URL: /fisioterapeuta
Descrição
Obtém uma lista de todos os fisioterapeutas registrados no sistema. Apenas administradores ou coordenadores podem acessar este endpoint.
404 Not Found

Corpo: "Fisioterapeutas not found"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Obter Fisioterapeuta por ID
Método HTTP: GET
URL: /fisioterapeuta/{id}
Descrição
Obtém os detalhes de um fisioterapeuta específico com base no ID fornecido. Apenas administradores ou coordenadores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Parâmetros
Path Parameter
id (int): O ID do fisioterapeuta a ser recuperado.
Respostas
200 OK

Corpo: Um objeto FisioterapeutaDto com os detalhes do fisioterapeuta.
Exemplo:
{
  "idFisio": 1,
  "nomeFisio": "Jane Doe",
  "emailFisio": "jane@example.com",
  "especialidade": "Ortopedia"
}
404 Not Found

Corpo: "Fisioterapeuta not found"
500 Internal Server Error

Corpo: "Internal server error"

Endpoint: Criar Novo Fisioterapeuta
Método HTTP: POST
URL: /fisioterapeuta
Descrição
Cria um novo fisioterapeuta no sistema. Apenas administradores ou coordenadores podem acessar este endpoint. Além disso, cria um novo usuário vinculado ao fisioterapeuta.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Corpo da Requisição (application/json)
Um objeto JSON representando o novo fisioterapeuta a ser criado.
exemplo de json:
{
  "idFisio": 0,
  "nomeFisio": "string",
  "emailFisio": "fisioteste@example.com",
  "matricula": 0,
  "semestreFisio": "string",
  "tipoUsuario": 3
}
![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/50045733-fc18-4607-a77c-4f40c8a1962a)

Endpoint: Atualizar Fisioterapeuta
Método HTTP: PUT
URL: /fisioterapeuta
Descrição
Atualiza os detalhes de um fisioterapeuta existente. Apenas administradores ou coordenadores podem acessar este endpoint.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Corpo da Requisição (application/json)
Um objeto JSON representando o fisioterapeuta a ser atualizado.

exemplo de json:
{
  "idFisio": 12,
  "nomeFisio": "string",
  "emailFisio": "fisioteste@example.com",
  "matricula": 0,
  "semestreFisio": "string",
  "tipoUsuario": 3,
  "usuario": null
}

![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/83b09fa0-e038-42f2-a77e-83341d428db5)


Aqui está a documentação para os endpoints do controlador FisioterapeutaController. Esta documentação detalha os métodos disponíveis para gerenciamento de fisioterapeutas, incluindo as operações de criação, leitura, atualização e exclusão, com base no código fornecido.

Controlador: Fisioterapeuta
Descrição
O FisioterapeutaController gerencia operações CRUD (Criar, Ler, Atualizar e Excluir) para fisioterapeutas no sistema. Os endpoints são protegidos por políticas de autorização que garantem acesso apropriado baseado nos papéis dos usuários.

Endpoint: Obter Todos os Fisioterapeutas
Método HTTP: GET
URL: /fisioterapeuta
Descrição
Obtém uma lista de todos os fisioterapeutas registrados no sistema. Apenas administradores ou coordenadores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Respostas
200 OK

Corpo: Um array de objetos FisioterapeutaDto com detalhes dos fisioterapeutas.
Exemplo:
json
Copiar código
[
  {
    "idFisio": 1,
    "nomeFisio": "Jane Doe",
    "emailFisio": "jane@example.com",
    "especialidade": "Ortopedia"
  },
  {
    "idFisio": 2,
    "nomeFisio": "John Smith",
    "emailFisio": "john@example.com",
    "especialidade": "Neurologia"
  }
]
404 Not Found

Corpo: "Fisioterapeutas not found"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Obter Fisioterapeuta por ID
Método HTTP: GET
URL: /fisioterapeuta/{id}
Descrição
Obtém os detalhes de um fisioterapeuta específico com base no ID fornecido. Apenas administradores ou coordenadores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Parâmetros
Path Parameter
id (int): O ID do fisioterapeuta a ser recuperado.
Respostas
200 OK

Corpo: Um objeto FisioterapeutaDto com os detalhes do fisioterapeuta.
Exemplo:
json
Copiar código
{
  "idFisio": 1,
  "nomeFisio": "Jane Doe",
  "emailFisio": "jane@example.com",
  "especialidade": "Ortopedia"
}
404 Not Found

Corpo: "Fisioterapeuta not found"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Criar Novo Fisioterapeuta
Método HTTP: POST
URL: /fisioterapeuta
Descrição
Cria um novo fisioterapeuta no sistema. Apenas administradores ou coordenadores podem acessar este endpoint. Além disso, cria um novo usuário vinculado ao fisioterapeuta.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Corpo da Requisição (application/json)
Um objeto JSON representando o novo fisioterapeuta a ser criado.

Campo	Tipo	Obrigatório	Descrição
nomeFisio	string	Sim	Nome do fisioterapeuta
emailFisio	string	Sim	Email do fisioterapeuta
especialidade	string	Sim	Especialidade
Exemplo de Corpo da Requisição
json
Copiar código
{
  "nomeFisio": "Jane Doe",
  "emailFisio": "jane@example.com",
  "especialidade": "Ortopedia"
}
Respostas
200 OK

Corpo: Um objeto JSON com detalhes do fisioterapeuta criado e do usuário vinculado.
Exemplo:
json
Copiar código
{
  "Fisioterapeuta": {
    "idFisio": 1,
    "nomeFisio": "Jane Doe",
    "emailFisio": "jane@example.com",
    "especialidade": "Ortopedia"
  },
  "Usuario": {
    "IdUser": 1,
    "Username": "jane@example.com",
    "TipoUsuario": 3
  }
}
400 Bad Request

Corpo: "Dados inválidos"
500 Internal Server Error

Corpo: "Erro interno do servidor"
Endpoint: Atualizar Fisioterapeuta
Método HTTP: PUT
URL: /fisioterapeuta
Descrição
Atualiza os detalhes de um fisioterapeuta existente. Apenas administradores ou coordenadores podem acessar este endpoint.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Corpo da Requisição (application/json)
Um objeto JSON representando o fisioterapeuta a ser atualizado.

Campo	Tipo	Obrigatório	Descrição
idFisio	int	Sim	ID do fisioterapeuta
nomeFisio	string	Sim	Nome do fisioterapeuta
emailFisio	string	Sim	Email do fisioterapeuta
especialidade	string	Sim	Especialidade
Exemplo de Corpo da Requisição
json
Copiar código
{
  "idFisio": 1,
  "nomeFisio": "Jane Doe",
  "emailFisio": "jane@example.com",
  "especialidade": "Ortopedia"
}
Respostas
200 OK

Corpo: O objeto FisioterapeutaDto atualizado.
400 Bad Request

Corpo: "Data invalid"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Excluir Fisioterapeuta
Método HTTP: DELETE
URL: /fisioterapeuta/{id}
Descrição
Exclui um fisioterapeuta existente do sistema com base no ID fornecido. Apenas administradores ou coordenadores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminOrCoordenadorPolicy"
Parâmetros
Path Parameter
id (int): O ID do fisioterapeuta a ser excluído.
Respostas
200 OK

Corpo: O objeto FisioterapeutaDto excluído.
Exemplo:
json
{
  "idFisio": 1,
  "nomeFisio": "Jane Doe",
  "emailFisio": "jane@example.com",
  "especialidade": "Ortopedia"
}
404 Not Found

Corpo: "Fisioterapeuta not found"
500 Internal Server Error

Corpo: "Internal server error"

Controlador: Coordenador
Descrição
O CoordenadorController gerencia operações CRUD (Criar, Ler, Atualizar e Excluir) para coordenadores no sistema. Todos os endpoints são protegidos por políticas de autorização que garantem acesso apropriado com base nos papéis dos usuários.

Endpoint: Obter Todos os Coordenadores
Método HTTP: GET
URL: /coordenador
Descrição
Obtém uma lista de todos os coordenadores registrados no sistema. Apenas administradores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminPolicy"
Respostas
200 OK

Corpo: Um array de objetos CoordenadorDto com detalhes dos coordenadores.
Exemplo:
json
[
  {
    "idCoordenador": 1,
    "nomeCoordenador": "Alice",
    "emailCoordenador": "alice@example.com",
    "departamento": "Fisioterapia"
  },
  {
    "idCoordenador": 2,
    "nomeCoordenador": "Bob",
    "emailCoordenador": "bob@example.com",
    "departamento": "Ortopedia"
  }
]
404 Not Found

Corpo: "Coordenadores not found"
500 Internal Server Error

Corpo: "Internal server error"

Endpoint: Obter Coordenador por ID
Método HTTP: GET
URL: /coordenador/{id}
Descrição
Obtém os detalhes de um coordenador específico com base no ID fornecido. Apenas administradores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminPolicy"
Parâmetros
Path Parameter
id (int): O ID do coordenador a ser recuperado.
Respostas
200 OK

Corpo: Um objeto CoordenadorDto com os detalhes do coordenador.
Exemplo:
json
{
  "idCoordenador": 1,
  "nomeCoordenador": "Alice",
  "emailCoordenador": "alice@example.com",
  "departamento": "Fisioterapia"
}
404 Not Found

Corpo: "Coordenador not found"
500 Internal Server Error

Corpo: "Internal server error"
Endpoint: Criar Novo Coordenador
Método HTTP: POST
URL: /coordenador
Descrição
Cria um novo coordenador no sistema. Apenas administradores podem acessar este endpoint. Além disso, cria um novo usuário vinculado ao coordenador.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token com política "AdminPolicy"
Corpo da Requisição (application/json)
Um objeto JSON representando o novo coordenador a ser criado.

{
  "idCoordenador": 0,
  "nomeCoordenador": "string",
  "emailCoordenador": "coordteste@example.com",
  "tipoUsuario": 2
}

![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/671f36ad-1cfd-4b84-b780-fb58898731a1)

Endpoint: Atualizar Coordenador
Método HTTP: PUT
URL: /coordenador
Descrição
Atualiza os detalhes de um coordenador existente. Apenas administradores podem acessar este endpoint.

Cabeçalhos HTTP
Content-Type: application/json
Authorization: Bearer token com política "AdminPolicy"
Corpo da Requisição (application/json)
Um objeto JSON representando o coordenador a ser atualizado.

![image](https://github.com/GeanVitorM/Authetication.Server/assets/166526691/30e438e0-5523-4835-a5ea-2885736ca6c5)
Endpoint: Excluir Coordenador
Método HTTP: DELETE
URL: /coordenador/{id}
Descrição
Exclui um coordenador existente do sistema com base no ID fornecido. Apenas administradores podem acessar este endpoint.

Cabeçalhos HTTP
Authorization: Bearer token com política "AdminPolicy"
Parâmetros
Path Parameter
id (int): O ID do coordenador a ser excluído.
Respostas
200 OK

Corpo: O objeto CoordenadorDto excluído.
Exemplo:
json
{
  "idCoordenador": 1,
  "nomeCoordenador": "Alice",
  "emailCoordenador": "alice@example.com",
  "departamento": "Fisioterapia"
}
404 Not Found

Corpo: "Coordenador not found"
500 Internal Server Error

Corpo: "Internal server error"

