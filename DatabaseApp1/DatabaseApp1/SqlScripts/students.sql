-- Table: public.students

-- DROP TABLE public.students;

CREATE TABLE IF NOT EXISTS public.students
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    first_name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    last_name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    registration_date date NOT NULL,
    CONSTRAINT students_pkey PRIMARY KEY (id)
    )

    TABLESPACE pg_default;

ALTER TABLE public.students
    OWNER to otus;