-- Table: public.lectures

-- DROP TABLE public.lectures;

CREATE TABLE IF NOT EXISTS public.lectures
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    course_id integer NOT NULL,
    description character varying COLLATE pg_catalog."default",
    date date NOT NULL,
    CONSTRAINT lectures_pkey PRIMARY KEY (id),
    CONSTRAINT fk_lectures_courses FOREIGN KEY (course_id)
    REFERENCES public.courses (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID
    )

    TABLESPACE pg_default;

ALTER TABLE public.lectures
    OWNER to otus;
-- Index: fki_fk_lectures_courses

-- DROP INDEX public.fki_fk_lectures_courses;

CREATE INDEX fki_fk_lectures_courses
    ON public.lectures USING btree
    (course_id ASC NULLS LAST)
    TABLESPACE pg_default;