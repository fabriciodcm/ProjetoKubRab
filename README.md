# Projeto Kubernetes + RabbitMQ

Projeto de estudo e desenvolvimento de uma aplicação distribuída, formada por serviços .NET, interfaces web e infraestrutura baseada em containers com foco principal no aprendizado de Kubernetes e RabbitMQ.

## Sobre o projeto

A solução reúne diferentes componentes responsáveis pela coleta, disponibilização, exibição e teste de dados de produtos:

- **ProjectKubRab.Worker**: Worker Service responsável por coletar, por meio de raspagem, os dados dos produtos informados via args na página gerada em ProjectKubRab.ProductsWebApp. Os produtos são inseridos na Fila do RabbitMQ.
- **ProjectKubRab.API**: Monolito que processa a Queue de Produtos e insere os registros no MongoDB. Ele deverá conter uma API REST de Produtos persistidos no MongoDB. Futuramente poderá ser dividido em microserviços (conforme apontado como uma boa prática: https://martinfowler.com/bliki/MonolithFirst.html).
- **ProjectKubRab.ProductsWebApp**: Página de produtos gerada em server-side.
- **ProjectKubRab.ReactProductsWebApp**: Página de produtos gerada em client-side desenvolvida com React, TypeScript e Vite (atualmente não está sendo utilizada).
- **ProjectKubRab.Test**: projeto de testes automatizados com xUnit.
- **MongoDB**: Banco de dados utilizado pela aplicação e executado por meio do Docker Compose.
- **ProductsPage**: Página de produtos gerada em client-side desenvolvida com HTML e JS (atualmente não está sendo utilizada).
- **RabbitMQ**: Message Broker de comunicação assíncrona entre os serviços.
- **Docker Compose**: Responsável pela criação do ambiente local e pela execução dos serviços em containers.

Como evolução, o projeto terá suporte ao **Kubernetes** para implantação e orquestração dos containers.

## ESSENTIALS TO-DO LIST


- [x] **PRONTO** — Página Web contendo produtos. A cada requisição, o valor dos produtos sofre uma variação aleatória de -7% a 7% do valor base do produto. Foi implementada em 3 versões, HTML E JS, REACT, .NET MVC.
- [x] **PRONTO** — Extração dos dados do produto desejado utilizando HtmlAgilityPack no Worker. Foi utilizado o .NET MVC para simplificar o projeto, pois ele já traz a página renderizada do backend. Para utilizar as versões contendo JS seria necessário utilizar um Headless Browser como o Selenium ou Playwright, pois o HtmlAgilityPack não executa o JS para renderizar a página com produtos. 
- [x] **PRONTO** — O Worker adiciona o produto extraido na fila do RabbitMQ(Direct).
- [x] **PRONTO** — O API lê o produto extraído da fila do RabbitMQ(Direct). Caso já exista um registro do produto na data da leitura, a API atualiza esse registro; caso contrário, insere um novo. 
- [x] **PRONTO** — Usar variáveis de ambiente nas aplicações a fim de poder depurar localmente ou via Docker Compose sem precisar alterar as cadeias de conexão(ex: url da página de produtos local localhost:8585 e via Docker Compose dotnetproductspage:8080). 
- [ ] **PENDENTE** — Adicionar a API no Kubernetes com duas instâncias rodando. Adicionar atraso no processamento de filas de Produtos para visualizar o Load Balancer roteando requisições nos logs (e, se necessário, adicionar mais produtos para processamento). 


## IMPROVEMENTS TO-DO LIST 

- [ ] **PENDENTE** — API REST de Produtos.
- [ ] **PENDENTE** — Melhorar a legibilidade do Producer e Consumer da fila de Produtos usando o Exchange Direct.
- [ ] **PENDENTE** — Extração dos dados do produto desejado utilizando Selenium ou Playwright.
- [ ] **PENDENTE** — Script de criação e população de registros de produtos no MongoDB.
- [ ] **PENDENTE** — Na leitura do produto da fila do RabbitMQ(Direct), verificar se é o menor preço registrado ou se o preço está abaixo de 5% da primeira leitura de produto. Caso um dos requisitos descritos seja atendido, adicionar um exchange do tipo topic (promo.notify.*) ao RabbitMQ para serviços de notificação.
- [ ] **PENDENTE** — Criar abstrações para diferentes serviços de notificação, acompanhadas de implementações simuladas que consumam as mensagens do RabbitMQ sem processá-las efetivamente (ex.: promo.notify.email, promo.notify.notificationhub, promo.notify.sms etc).


## MATERIAL DE APOIO

**GERAL**

- Leandro Costa - https://www.udemy.com/course/restful-apis-do-0-a-nuvem-com-aspnet-core-e-docker

**DOCKER**

- Diolinux (Dionatan Simioni) - https://www.youtube.com/watch?v=ntbpIfS44Gw
- Diolinux (Dionatan Simioni) - https://www.youtube.com/watch?v=Y6kz884AoME
- Fernanda Kipper | Dev - https://www.youtube.com/watch?v=D_ha0g9yS2E
- Milan Jovanovic - https://www.youtube.com/watch?v=svfxvsGfLlU
- Stefan Schranz - https://stefansch.medium.com/understanding-the-visual-studio-docker-compose-integration-3e19c55bb757

**RABBITMQ**

- Full Cycle (Wesley Willians) - https://www.youtube.com/watch?v=2YWHtbZJ0QI
- Milan Jovanovic - https://www.youtube.com/watch?v=sN5YpfOpCHA
- Milan Jovanovic - https://www.youtube.com/watch?v=daaiAjZnOm4
- Kevin Patrick Boylan - https://blog.devops.dev/using-rabbitmq-with-net-core-web-api-and-worker-services-15330c53cfb0

**MONGODB**

- Luis Felipe (LuisDev) - https://www.youtube.com/watch?v=6wvRpDl-lvQ

**KUBERNETES**

- Conteúdo a ser incluído após a implementação do Kubernetes