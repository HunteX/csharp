CREATE TABLE "public"."Professions"
(
    id   INT GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(128) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE "public"."Teachers"
(
    id            INT GENERATED ALWAYS AS IDENTITY,
    firstname     VARCHAR(128) NOT NULL,
    lastname      VARCHAR(128) NOT NULL,
    middlename    VARCHAR(128) NOT NULL,
    email         VARCHAR(128) NOT NULL,
    profession_id INT          NOT NULL,
    PRIMARY KEY (id),
    CONSTRAINT fk_profession FOREIGN KEY (profession_id) REFERENCES "public"."Professions" (id)
);

CREATE TABLE "public"."Lessons"
(
    id          INT GENERATED ALWAYS AS IDENTITY,
    name        VARCHAR(128)  NOT NULL,
    description VARCHAR(2048) NOT NULL,
    cancelled   BIT           NOT NULL,
    start_at    TIMESTAMP,
    PRIMARY KEY (id)
);

CREATE TABLE "public"."Teachers_Lessons"
(
    id         INT GENERATED ALWAYS AS IDENTITY,
    teacher_id INT NOT NULL,
    lesson_id  INT NOT NULL,
    PRIMARY KEY (id),
    CONSTRAINT fk_teacher FOREIGN KEY (teacher_id) REFERENCES "public"."Teachers" (id),
    CONSTRAINT fk_lesson FOREIGN KEY (lesson_id) REFERENCES "public"."Lessons" (id)
);