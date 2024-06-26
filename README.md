﻿# Checats API Documentation

## Стэк технологий
#### Архитектура - REST API
1. ASP.NET + C#
2. СУБД - EFCore
3. БД - Postgres
4. Swagger для автодокументации
5. Контейнеризация - Docker
6. Проксирование - Nginx

## Что требуется реализовать

### Сущности
1. Сущность пользователя, которая хранит в себе его имя и хэш пароля.
2. Сущность поста, которая хранит в себе назание, контент-наполнение,
   Id автора и комментарии к этому посту.
3. Сущность комментария, которая хранит в себе коммент,
   Id автора и поста, к которому был адрессован коммент.

### Функционал

1. Получение пользователя по Id, по имени.
2. Хэширование паролей, аутентификация, авторизация.
3. Получение поста по Id и / или назаванию.
4. Создание и редактирование постов.
5. Создание комментариев.
6. Получение списка комментариев по Id поста.
7. Получение списка всех пользователей, списка всех постов.
8. Удаление постов, комментариев, пользователей.
9. Изменение пароля пользователя.

### Срок сдачи до 30.05.2024