<h2>CRM-ERP Контрактор<h2>
<h4>Реализованная функциональность</h4>
<ul>
    <li>Полнофункциональный бэк с видимыми эндпойнтами в swagger<li>
    <li>DAL на MongoDB, который позволил очень быстро прототипировать<li>
    <li>Сервис геометрии - отдельный сервис для работы с растровыми картами, выделение регионов и объектов<li>
    <li>UI-клиент на vue.js<li>
    <li>Регистрация авторизация пользователей;</li>
    <li>Функционал рисования планов помещений, в том числе поверх подгруженного изображения;</li>
    <li>Автоматическая генерация договоров на оренду на основе введенных регистрационных данных;</li>
    <li>Подача заявок на краткосрочную и долгосрочную оренду помещений;</li>
    <li>Подача показаний приборов учета в управляющую компанию;</li>
    <li>Генерация платежных документов по полученным показаниям приборов учета;</li>
    <li>Добавление событий в календарь мероприятий;<li>
    <li>Просмотр товаров и услуг оказываемых арендатором;<li>
    <li>Обмен мгновенными сообщениями;<li>
    <li>Управление персоналом арендатора;<li>
    <li>Просмотр событий в ленте арендатора;<li>
    <li>Промотр карты помещения и информации о помещении;<li>
    <li>Позиционирование пользователя приложения в помещении<li>
    <li>Прокладывание марщрута до нужной точки<li>
</ul> 
<h4>Особенность проекта в следующем:</h4>
<ul>
 <li>Автоматическая генерация документов;</li>
 <li>Позиционирование по WiFi сгналам в помещении;</li>
 <li>Подача заявок онлайн из личного кабинета;</li>  
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
h4>Демо</h4>
<p>Демо сервиса доступно по адресу: https://01fe1d0d97fb.ngrok.io/api/v1/ </p>
<p>Демо UI системы по адресу: https://kreolz.github.io/#/ </p>
<p>Реквизиты администратора (работника УК): логин: admin пароль: admin</p>
<p>Реквизиты пользователя (ИП): логин: ivanov пароль: ivanov</p>

СРЕДА ЗАПУСКА BECKEND
------------
1) развертывание сервиса производилось на Windows-машине, однако может происходить и на linux-like средах, поддерживаемых .NET Core
2) требуется установленный паке Docker и docker-compose для автоматизации развёртывания проекта;
3) Разворачиваем mongo:
```
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
```
4) Простой ci процесс на bat:
```
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
```
5) Имеет смысл зарегистрировать сервис через sc
 
```sc create kontaktormn "C:\Users\fofin\Documents\Work_2020\2021\hack\full-sources\backend\kontaktor-network\kontaktor-network.Service\KONTAKTOR.Main.Service.exe"```

СРЕДА ЗАПУСКА FRONTEND
------------
### Установка fronend
...
~~~
Для запуска проекта необходим глобально установленный yarn. Установить его можно с помощью команды npm install -g yarn
git clone https://github.com/snezer/Kontactor.Pro100.studio склонировать себе проект 
cd frontend  перейти в папку с фронтом
yarn install установит все зависимости для проекта 
yarn run serve запустит проект в режиме разработки получить доступ можно по адресу localhost:8080
yarn run build запусти процесс сборки проекта для продакшена
...
~~~


РАЗРАБОТЧИКИ

<h4>Мусихин Алексей Руководитель https://t.me/writer22rus</h4>
<h4>Фокин Максим Full-stack https://t.me/snezer</h4>
<h4>Грибков Александр Full-stack https://t.me/alex_gbk</h4>
<h4>Костенко Виталий Front-end https://t.me/kreolz3245</h4>
<h4>Намаканова Наиля Дизайн https://t.me/n_namakonova</h4>
