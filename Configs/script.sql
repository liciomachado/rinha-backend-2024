 CREATE TABLE public.clients (
     id SERIAL PRIMARY KEY,
     "limit" bigint NOT NULL,
     balance bigint NOT NULL
 );
 
CREATE TABLE public.transaction (
    id SERIAL PRIMARY KEY,
    value bigint NOT NULL,
    type text NOT NULL,
    description text NOT NULL,
    realized timestamp with time zone NOT NULL,
    "ClientId" bigint,
    CONSTRAINT "FK_transaction_clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES public.clients (id)
);

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
