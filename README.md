Documentação Api De autenticação Fisioterapia
Authentication API

Esta API é usada para gerenciar usuários, pacientes, fisioterapeutas, coordenadores e administradores.

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

json
Copiar código
{
  "login": "user@example.com",
  "password": "senha123"
}
Neste exemplo:

O valor da propriedade "login" é "user@example.com", que representa o e-mail do usuário.
O valor da propriedade "password" é "senha123", que representa a senha do usuário.
É importante que os dados fornecidos no JSON de login sejam precisos e correspondam às credenciais válidas de um usuário registrado no sistema.
