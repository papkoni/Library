Запуск приложения с помощью Docker Compose

Чтобы запустить приложение с помощью Docker Compose, выполните следующие шаги:

Убедитесь, что Docker и Docker Compose установлены на вашем компьютере.
Клонируйте репозиторий: git clone <ссылка-на-репозиторий>
Перейдите в каталог проекта: cd Library; cd backend;
Выполните следующую команду для запуска приложения: docker-compose up --build 
И проверять все на 5000 локалхосте через postman
Если нужно проверить авторизацию и аунтификацию, то нужно добавить [Authorize] или [Authorize(Roles = "Admin")], если нужно протестировать и роль 



