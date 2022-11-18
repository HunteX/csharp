INSERT INTO "public"."Professions" ("name")
VALUES ('C#'),
       ('Javascript'),
       ('Kubernetes'),
       ('Algorithms'),
       ('Databases');

INSERT INTO "public"."Teachers" ("firstname", "lastname", "middlename", "email", "profession_id")
VALUES ('Иван', 'Иванов', 'Иванович', 'vanya@mail.com', 1),
       ('Сидор', 'Сидоров', 'Сидорович', 'sidor@mail.com', 2),
       ('Петр', 'Петров', 'Петрович', 'petya@mail.com', 3),
       ('Федор', 'Федоров', 'Федорович', 'fedya@mail.com', 4),
       ('Василий', 'Васильев', 'Василиевич', 'vasya@mail.com', 5);

INSERT INTO "public"."Lessons" ("name", "description", "cancelled", "start_at")
VALUES ('Многопоточное программирование', 'Ну там про потоки и всякое такое', '0', '2022-11-17 11:00:00.000000+00'),
       ('Javascript', 'Почему разработка на javascript приносит боль', '0', '2022-11-17 12:00:00.000000+00'),
       ('Пишем свой оператор', 'Зачем реализовывать свой оператор в k8s', '0', '2022-11-17 13:00:00.000000+00'),
       ('Сортировки', 'Рассмотрим все варианты сортировок и не уйдем пока не закончим', '1',
        '2022-11-17 14:00:00.000000+00'),
       ('Версионирование', 'MVCC в Postgres', '0', '2022-11-17 15:00:00.000000+00');

INSERT INTO "public"."Teachers_Lessons" ("teacher_id", "lesson_id")
VALUES (1, 1),
       (2, 1),
       (2, 2),
       (3, 3),
       (4, 4),
       (5, 5),
       (4, 5);