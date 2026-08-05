# Projeto Kubernetes + RabbitMQ

Projeto de estudo e desenvolvimento de uma aplicação distribuída, formada por serviços .NET, interfaces web e infraestrutura baseada em containers com foco principal no aprendizado de Kubernetes e RabbitMQ.

## Sobre o projeto

A solução reúne diferentes componentes responsáveis pela coleta, disponibilização, exibição e teste de dados de produtos:

- **ProjectKubRab.API**: API desenvolvida com ASP.NET Core para disponibilizar os dados da aplicação.
- **ProjectKubRab.Worker**: Worker Service responsável pelo processamento e pela coleta de informações.
- **ProjectKubRab.ProductsWebApp**: aplicação web desenvolvida com ASP.NET Core.
- **ProjectKubRab.ReactProductsWebApp**: interface web desenvolvida com React, TypeScript e Vite.
- **ProjectKubRab.Test**: projeto de testes automatizados com xUnit.
- **MongoDB**: banco de dados utilizado pela aplicação e executado por meio do Docker Compose.
- **Docker Compose**: responsável pela criação do ambiente local e pela execução dos serviços em containers.

Como evolução, o projeto terá o **RabbitMQ** para comunicação assíncrona entre os serviços e suporte ao **Kubernetes** para implantação e orquestração dos containers.

## ESSENTIALS TO DO LIST


- [x] **PRONTO** — Página Web contendo produtos(foi feita em 3 versão, HTML E JS, REACT, .NET MVC).
- [x] **PRONTO** — Extração dos dados do produto desejado utilizando HtmlAgilityPack no Worker. Foi utilizado o .NET MVC para simplificar o projeto pois ele já trás a página renderizado do backend. Para utilizar as versões contendo JS seria necessário utilizar um Headless Browser como o Selenium ou Playwright pois o HtmlAgilityPack não executa o JS para renderizar a página com produtos. 
- [ ] **PENDENTE** — O Worker adicionar o produto extraido na fila do RabbitMQ(Direct).
- [ ] **PENDENTE** — O API ler o produto extraido da fila do RabbitMQ(Direct), caso o produto já exista para o dia da leitura atualizar o registro do dia, se não inserir um novo. 


## IMPROVEMENTS TO DO LIST 


- [ ] **PENDENTE** — Extração dos dados do produto desejado utilizando Selenium ou Playwright.
- [ ] **PENDENTE** — Script de criação e população de registros de produtos no MongoDB.
- [ ] **PENDENTE** — Na leitura do produto da fila do RabbitMQ(Direct), verificar se é o menor preço registrado ou se o preço está abaixo de 5% da primeira leitura de produto, adicionar um Topic (promo.notify.*) no RabbitMQ para serviços de notificação.
- [ ] **PENDENTE** — Cria diferentes abstrações de serviços de notificação que leem a notificação do RabbitMQ (ex: promo.notify.email, promo.notify.notificationhub, promo.notify.sms etc).
