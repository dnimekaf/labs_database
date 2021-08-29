-- Table: public.students_courses

-- DROP TABLE public.students_courses;

CREATE TABLE IF NOT EXISTS public.students_courses
(
    student_id integer NOT NULL,
    course_id integer NOT NULL,
    CONSTRAINT students_courses_pkey PRIMARY KEY (student_id, course_id),
    CONSTRAINT fk_courses_students FOREIGN KEY (course_id)
    REFERENCES public.courses (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID,
    CONSTRAINT fk_students_courses FOREIGN KEY (student_id)
    REFERENCES public.students (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID
    )

    TABLESPACE pg_default;

ALTER TABLE public.students_courses
    OWNER to otus;
-- Index: fki_fk_courses_students

-- DROP INDEX public.fki_fk_courses_students;

CREATE INDEX fki_fk_courses_students
    ON public.students_courses USING btree
    (course_id ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: fki_fk_students_courses

-- DROP INDEX public.fki_fk_students_courses;

CREATE INDEX fki_fk_students_courses
    ON public.students_courses USING btree
    (student_id ASC NULLS LAST)
    TABLESPACE pg_default;