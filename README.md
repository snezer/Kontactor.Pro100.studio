<p align="center">
    <h1 align="center">CRM/ERP КОНТАКТОР</h1>
    <br>
</p>

<h4>Реализованная функциональность</h4>
<ul>
    <li>Полнофункциональный бэк с видимыми эндпойнтами в swagger</li>
    <li>DAL на MongoDB, который позволил очень быстро прототипировать</li>
    <li>Сервис геометрии - отдельный сервис для работы с растровыми картами, выделение регионов и объектов</li>
	<li>UI-клиент на vue.js</li>
	<li>Аутентификация, работа с заявками, интерфейсы УК, клиента, чата, договорной работы</li>
</ul> 
<h4>Особенность проекта в следующем:</h4>
<ul>
 	<li>Киллерфича-1;</li>
 	<li>Киллерфича-2;</li>
 	<li>Киллерфича-3;</li>  
 </ul>

<h4>Основной стек технологий:</h4>
<ul>
	<li>HTML, CSS, JavaScript.</li>
	<li>Vue, Vuex, Vuetify.</li>
	<li>LESS, SASS, PostCSS.</li>
	<li>Yarn, Webpack, Babel.</li>
	<li>Git, GitHub.</li>
	<li>Mongodb</li>
	<li>.NET Core 3.1, Prometheus, Dapper, Log4Net</li>
	<li>ASP Core, Swashbuckle/Swagger </li>
	<li>Docker, Docker-Compose</li>  
 </ul>

 <h4>Демо</h4>
<p>Демо сервиса доступно по адресу: https://01fe1d0d97fb.ngrok.io/api/v1/ </p>
<p>Демо UI системы по адресу: ??? </p>
<p>Реквизиты администратора (работника УК): логин: admin пароль: admin</p>
<p>Реквизиты пользователя (ИП): логин: ivanov пароль: ivanov</p>

СРЕДА ЗАПУСКА
------------
1) развертывание сервиса производилось на Windows-машине, однако может происходить и на linux-like средах, поддерживаемых .NET Core
2) требуется установленный паке Docker и docker-compose для автоматизации развёртывания проекта;
3) Разворачиваем mongo:
###
version: "3.7"
services:
  mongodb:
    container_name: mongo-dev
    image: mongo
    environment:
      - MONGO_INITDB_ROOT_USERNAME=admin
      - MONGO_INITDB_DATABASE=auth
      - MONGO_INITDB_ROOT_PASSWORD=pass
    ports:
      - '27017:27017'
  mongo-express:
    container_name: mongo-express
    image: mongo-express
    depends_on:
      - mongodb
    environment:
      - ME_CONFIG_MONGODB_ADMINUSERNAME=admin
      - ME_CONFIG_MONGODB_ADMINPASSWORD=pass
      - ME_CONFIG_MONGODB_SERVER=mongo-dev
      - ME_CONFIG_BASICAUTH_USERNAME=admin
      - ME_CONFIG_BASICAUTH_PASSWORD=admin
    ports:
      - '8081:8081'

4) Простой ci процесс на bat:
###
sc stop kontaktormn
:loop
sc query kontaktormn | find "STOPPED"
if errorlevel 1 (
  timeout 1
  goto loop
)
taskkill /F /IM KONTAKTOR.Main.Service.exe

cd C:\Users\fofin\Documents\Work_2020\2021\hack\full-sources
git pull
cd C:\Users\fofin\Documents\Work_2020\2021\hack\full-sources\backend\kontaktor-network\kontaktor-network.Service\
dotnet publish -c Debug -o C:\Users\fofin\Documents\Work_2020\2021\hack\published\kontaktor-network

sc start kontaktormn

5) Имеет смысл зарегистрировать сервис через sc
### 
sc create kontaktormn "C:\Users\fofin\Documents\Work_2020\2021\hack\full-sources\backend\kontaktor-network\kontaktor-network.Service\KONTAKTOR.Main.Service.exe"
