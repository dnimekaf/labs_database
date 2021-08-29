-- Table: public.courses

-- DROP TABLE public.courses;

CREATE TABLE IF NOT EXISTS public.courses
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    course_name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    course_start date NOT NULL,
    course_end date NOT NULL,
    CONSTRAINT courses_pkey PRIMARY KEY (id)
    )

    TABLESPACE pg_default;

ALTER TABLE public.courses
    OWNER to otus;