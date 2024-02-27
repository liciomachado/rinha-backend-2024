 CREATE TABLE public.clients (
     id SERIAL PRIMARY KEY,
     "limit" INTEGER NOT NULL,
     balance INTEGER NOT NULL
 );
 
CREATE TABLE public.transaction (
    id SERIAL PRIMARY KEY,
    value bigint NOT NULL,
    type text NOT NULL,
    description text NOT NULL,
    realized timestamp with time zone NOT NULL,
    "ClientId" INTEGER,
    CONSTRAINT "FK_transaction_clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES public.clients (id)
);

CREATE INDEX ids_transaction_ids_client_id ON public.transaction ("ClientId");


INSERT INTO public.clients (id, balance, "limit")
VALUES (1, 0, 100000);
INSERT INTO public.clients (id, balance, "limit")
VALUES (2, 0, 80000);
INSERT INTO public.clients (id, balance, "limit")
VALUES (3, 0, 1000000);
INSERT INTO public.clients (id, balance, "limit")
VALUES (4, 0, 10000000);
INSERT INTO public.clients (id, balance, "limit")
VALUES (5, 0, 500000);
