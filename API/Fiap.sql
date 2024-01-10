DO $$ 
BEGIN
    IF NOT EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'pedido_status') THEN 
        CREATE TABLE public.pedido_status (
            id int4 NOT NULL,
            nome varchar NOT NULL,
            CONSTRAINT pedido_status_pk PRIMARY KEY (id)
        );

        -- Permissions
        ALTER TABLE public.pedido_status OWNER TO postgres;
        GRANT ALL ON TABLE public.pedido_status TO postgres;
    END IF; 
END $$;


DO $$ 
BEGIN
    IF NOT EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'pedido') THEN 
        CREATE TABLE public.pedido (
	pedido_id int NOT NULL,
	pedido_status_id int NOT NULL,
	created_at timestamp NULL,
	updated_at timestamp NULL,
	CONSTRAINT pedido_pk PRIMARY KEY (pedido_id),
	CONSTRAINT pedido_fk FOREIGN KEY (pedido_status_id) REFERENCES public.pedido_status(id)
);

        -- Permissions
        ALTER TABLE public.pedido OWNER TO postgres;
        GRANT ALL ON TABLE public.pedido TO postgres;
    END IF; 
END $$;






INSERT INTO public.pedido_status (id, nome) VALUES
(1, 'Recebido'),
(2, 'Em Preparação'),
(3, 'Pronto'),
(4, 'Finalizado')
ON CONFLICT (id) 
DO NOTHING;







